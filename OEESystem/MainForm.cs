using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace OEESystem
{
    public partial class MainForm : Form
    {
        MyFunctions myFunc = new MyFunctions();

        Thread syncTime_TH = null;//同步时间线程
        Thread updateLogToServer_TH = null;//上传日志到服务器线程
        //Thread checkBlocksCount_TH = null;//检测block数量线程

        public MainForm()
        {
            UpdateClass UC = new UpdateClass();
            //UC.GetVersion();
            InitializeComponent();
            // this.TransparencyKey = Color.White;
            //this.Opacity = 90;
            //this.WindowState = FormWindowState.Maximized;    //最大化窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //设置flowLayoutPanel的双缓冲属性，减少重绘控件时的闪烁
            flowLayoutPanel_Clients.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(flowLayoutPanel_Clients, true, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //string content = "{" + "\"id\"" + ": " + "\"ICT-01\"" + ", " + "\"product\"" + ": " + "\"SN123\"" + ", " + "\"person\"" + ": " + "\"E123\"" + ", " + "\"time\"" + ": " + "\"2018-01-01T10:20:38.233+08:00\"" + "," + "\"items\"" + ":[{" + "\"code\"" + ": " + "\"R001\"" + ", " + "\"value\"" + ": " + "\"1000.0\"" + ", " + "\"time\"" + ": " + "\"2018-01-01T10:20:38.233+08:00\"" + "},{" + "\"code\"" + ": " + "\"R002\"" + ", " + "\"value\"" + ": " + "\"10.0\"" + ", " + "\"time\"" + ": " + "\"2018-01-01T10:20:38.233+08:00\"" + "}]}";
            //myFunc.updateJsonToServer(content);
            myFunc.writeCommLog("服务端开启");
            GlobalVar.gl_AppAlive = true;
            GlobalVar.gl_IntPtr_MainWindow = this.Handle;//获取主窗口句柄
            loadConfig();//初始化
            controlsInital();//控件初始化
            startThreads();//开启线程
        }

        private void startThreads()
        {
            //同步时间 线程
            syncTime_TH = new Thread(syncTimeFromServer);
            syncTime_TH.IsBackground = true;
            syncTime_TH.Name = "同步时间";
            syncTime_TH.Start();

            //上传详报 线程
            updateLogToServer_TH = new Thread(updateLogToServer);
            updateLogToServer_TH.IsBackground = true;
            updateLogToServer_TH.Name = "上传详报";
            updateLogToServer_TH.Start();

            

            //检测block 线程
            //checkBlocksCount_TH = new Thread(checkBlocksCount);
            //checkBlocksCount_TH.IsBackground = true;
            //checkBlocksCount_TH.Start();
        }

        private void controlsInital()
        {
            myFunc.writeCommLog("初始化控件...");
            textBox_ServerIP.Text = GlobalVar.gl_ServerIP;
            textBox_ServerPort.Text = GlobalVar.gl_ServerPort.ToString();
            //添加表头
            this.listView_CennectClient.Columns.Add("", 26, HorizontalAlignment.Center);
            this.listView_CennectClient.Columns.Add("设备名", 112, HorizontalAlignment.Center);
            this.listView_CennectClient.Columns.Add("网络终端地址", 136, HorizontalAlignment.Center);
            this.listView_CennectClient.Columns.Add("连接时间",266,HorizontalAlignment.Center);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color FColor =  ColorTranslator.FromHtml("#80d0a7"); //Color.SteelBlue;
            Color TColor =  ColorTranslator.FromHtml("#13547a");//Color.Gold;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.BackwardDiagonal);//窗口渐变色
            //b = new LinearGradientBrush(this.ClientRectangle,FColor,TColor,96.7f,true);
            g.FillRectangle(b, this.ClientRectangle);
        }

        private void toolStripLabel_StartWatch_Click(object sender, EventArgs e)
        {
            MyTcpServer server = new MyTcpServer();
            if (toolStripLabel_StartWatch.Text == "开始监听")
            {
                string path = Application.StartupPath + @"\config.ini";
                if (textBox_ServerIP.Text != "" && textBox_ServerPort.Text != "")
                {
                    GlobalVar.gl_ServerIP = textBox_ServerIP.Text.Trim();//服务端IP
                    GlobalVar.gl_ServerPort = int.Parse(textBox_ServerPort.Text.Trim());//服务端端口号
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_Section_Socket, GlobalVar.gl_Key_ServerIP, GlobalVar.gl_ServerIP, path);
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_Section_Socket, GlobalVar.gl_Key_ServerPort, GlobalVar.gl_ServerPort.ToString(), path);
                }
                setControlEnable(false);
                if (server.OpenServer(GlobalVar.gl_ServerIP, GlobalVar.gl_ServerPort)) 
                { 
                    toolStripLabel_StartWatch.Text = "停止监听";
                    toolStripLabel_StartWatch.BackColor = Color.LimeGreen;
                }
            }
            else
            {
                if (GlobalVar.gl_AdminLogin)
                {
                    server.CloseServer();//关闭服务
                    toolStripLabel_StartWatch.Text = "开始监听";
                    toolStripLabel_StartWatch.BackColor = Color.Gray;
                }
                else
                {
                    myFunc.warningShow("当前没有权限操作此项！\r\n如若要操作此项，请先登录管理员模式！");
                    //MessageBox.Show("当前没有权限操作此项！\r\n如若要操作此项，请先登录管理员模式！","WARNING",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }


        /// <summary>
        ///     设置控件enable属性
        /// </summary>
        private void setControlEnable(bool enable)
        {
            GlobalVar.gl_AdminLogin = enable;
            textBox_ServerIP.Enabled = enable;
            textBox_ServerPort.Enabled = enable;
        }


        /// <summary>
        ///     初始化配置文件
        /// </summary>
        private void loadConfig()
        {
            GetSoftVersion();//获取版本信息
            string path = Application.StartupPath + @"\config.ini";
            StringBuilder str = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_Section_Socket,GlobalVar.gl_Key_ServerIP,"",str,50,path);//服务端IP地址
            GlobalVar.gl_ServerIP=str.ToString().Trim();//服务端IP
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_Section_Socket,GlobalVar.gl_Key_ServerPort,"",str,50,path);//服务端端口号
            GlobalVar.gl_ServerPort = int.Parse(str.ToString().Trim());//端口号
            myFunc.writeCommLog("初始化配置文件...");
        }

        private void GetSoftVersion()
        {
            string NowVersion = "V1.0";
            string NowVersion_File = "V1.0";
            object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false);
            if (attributes.Length > 0)
            {
                if (attributes.Length > 0)
                {
                    NowVersion_File = ((System.Reflection.AssemblyFileVersionAttribute)attributes[0]).Version;//文件集版本
                    NowVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); //读取程序集版本
            
                }
            }
            label_Version.Text = "      程序集版本:"+NowVersion+"        文件集版本:"+NowVersion_File;
            myFunc.writeCommLog("初始化文件版本信息...");
        }


        private void 管理员登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (管理员登录ToolStripMenuItem.Text == "管理员登录")
                {
                    Login adminlogin = new Login();
                    if (adminlogin.ShowDialog() != DialogResult.OK)
                    {
                    }
                    else
                    {
                        setControlEnable(true);
                        管理员登录ToolStripMenuItem.Text = "管理员登出";
                    }
                }
                else
                {
                    setControlEnable(false);
                    管理员登录ToolStripMenuItem.Text = "管理员登录";

                }
            }
            catch { }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            myFunc.writeCommLog("服务端关闭...");
            System.Environment.Exit(0);
        }

        #region 线程方法
        /// <summary>
        ///     上传本地存储的LOG文件
        /// </summary>
        private void updateLogToServer()
        {
            string log_Dir = Application.StartupPath + @"\Update";//上传log存储路径
            int WaitTime = 10000;//10秒
            myFunc.writeCommLog("开始 新增详报到服务器 线程");
            while (GlobalVar.gl_AppAlive)
            {
                try
                {
                    DirectoryInfo TheFolder = new DirectoryInfo(log_Dir);  //获取文件名称
                    FileInfo[] files = TheFolder.GetFiles();
                    if (files.Length > 0)
                    {
                        myFunc.InsertSimpleResult(files);
                        Thread.Sleep(1000);
                    }
                    else Thread.Sleep(WaitTime);
                }
                catch (NotSupportedException nse) { Thread.Sleep(WaitTime); }
                catch (DirectoryNotFoundException dnfe) { Thread.Sleep(WaitTime); }
                catch (Exception ex)
                {
                    myFunc.writeErrorLog("新增详报到服务器 线程异常:" + ex.ToString());
                }

            }
        }
        /// <summary>
        ///     同步服务器时间
        /// </summary>
        private void syncTimeFromServer()
        {
            myFunc.writeCommLog("开始 同步时间 线程");
            while (GlobalVar.gl_AppAlive)
            {
                myFunc.syncTimeFromServer();
                //this.BeginInvoke(new Action(() =>
                //{
                //   // toolStripLabel_syncTime.Text = GlobalVar.gl_syncTime;//显示标准时间
                //}));
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        ///     检测block数量
        /// </summary>
        private void checkBlocksCount()
        {
            myFunc.writeCommLog("开始 刷新BLOCK 线程");
            while (GlobalVar.gl_AppAlive)
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.flowLayoutPanel_Clients.Controls.Clear();//清除panel中所有的控件
                        #region 列表刷新
                        this.listView_CennectClient.Items.Clear();
                        #endregion
                        if (GlobalVar.ClientBlocks.Count > 0)
                        {
                            int i = 0;
                            foreach (ClientBlock block in GlobalVar.ClientBlocks)
                            {
                                if (!this.flowLayoutPanel_Clients.Contains(block))
                                {
                                    i++;
                                    this.flowLayoutPanel_Clients.Controls.Add(block);
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = i.ToString();
                                    lvi.SubItems.Add(block.ClientName);
                                    lvi.SubItems.Add(block.ClientSocket);
                                    this.listView_CennectClient.Items.Add(lvi);
                                }
                            }
                        }
                    }));
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    myFunc.writeErrorLog("刷新BLOCK线程 异常:" + ex.ToString());
                }
            }
        }
        #endregion


        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case GlobalVar.WM_RecvClientConnect://接收到客户端连接，增加Block
                        this.flowLayoutPanel_Clients.Controls.Clear();//清除panel中所有的控件

                        #region 列表刷新
                        this.listView_CennectClient.Items.Clear();
                        #endregion
                        if (GlobalVar.ClientBlocks.Count > 0)
                        {
                            int i = 0;
                            foreach (ClientBlock block in GlobalVar.ClientBlocks)
                            {
                                if (!this.flowLayoutPanel_Clients.Contains(block))
                                {
                                    i++;
                                    this.flowLayoutPanel_Clients.Controls.Add(block);
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = i.ToString();
                                    lvi.SubItems.Add(block.ClientName);
                                    lvi.SubItems.Add(block.ClientSocket);
                                    lvi.SubItems.Add(GlobalVar.gl_syncTime);
                                    this.listView_CennectClient.Items.Add(lvi);
                                }
                            }
                        } 
                        break;
                    case GlobalVar.WM_RecvClientDisConnect://客户端断开连接，删除Block
                        this.flowLayoutPanel_Clients.Controls.Clear();//清除panel中所有的控件
                        #region 列表刷新
                        this.listView_CennectClient.Items.Clear();
                        #endregion
                        if (GlobalVar.ClientBlocks.Count > 0)
                        {
                            int i = 0;
                            foreach (ClientBlock block in GlobalVar.ClientBlocks)
                            {
                                if (!this.flowLayoutPanel_Clients.Contains(block))
                                {
                                    i++;
                                    this.flowLayoutPanel_Clients.Controls.Add(block);
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = i.ToString();
                                    lvi.SubItems.Add(block.ClientName);
                                    lvi.SubItems.Add(block.ClientSocket);
                                    lvi.SubItems.Add(GlobalVar.gl_syncTime);
                                    this.listView_CennectClient.Items.Add(lvi);
                                }
                            }
                        }
                        break;
                    case GlobalVar.WM_Refreash:
                        #region 列表刷新
                        this.listView_CennectClient.Items.Clear();
                        #endregion
                        if (GlobalVar.ClientBlocks.Count > 0)
                        {
                            int i = 0;
                            foreach (ClientBlock block in GlobalVar.ClientBlocks)
                            {
                                    i++;
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = i.ToString();
                                    lvi.SubItems.Add(block.ClientName);
                                    lvi.SubItems.Add(block.ClientSocket);
                                    lvi.SubItems.Add(GlobalVar.gl_syncTime);
                                    this.listView_CennectClient.Items.Add(lvi);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                myFunc.writeErrorLog("刷新异常:"+ex.ToString());
            }
            base.WndProc(ref m);
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case GlobalVar.WM_CopyData:
                    try
                    {
                        COPYDATASTRUCT cds = new COPYDATASTRUCT();
                        Type t = cds.GetType();
                        cds = (COPYDATASTRUCT)m.GetLParam(t);
                        string receiveInfo = cds.lpData;
                        this.BeginInvoke(new Action(() =>
                        {
                            txt_Communication.AppendText(receiveInfo.Trim()+"\t\r");
                        }));
                    }
                    catch(System.Exception ex)
                    {
                       // myFunc.writeErrorLog("消息显示异常:"+ex.ToString());
                    }
                    break;
                default:
                    break;
            }
            base.DefWndProc(ref m);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVar.gl_AdminLogin)
            {
                System.Environment.Exit(0);
            }
            else
            {
                myFunc.warningShow("当前没有权限操作此项！\r\n如若要操作此项，请先登录管理员模式！");
                e.Cancel = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReadLogs rl = new ReadLogs();
            rl.Show();
        }

    }




   

}
