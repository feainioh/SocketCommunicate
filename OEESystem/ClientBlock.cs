using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using System.IO;

namespace OEESystem
{
    public partial class ClientBlock : UserControl
    {
        MyFunctions myFunc = new MyFunctions();
        private string str_EndPoint;
        private Socket sokConnection;
        MySession tcpClient = null;
        /// <summary>
        ///     设备名称
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public string ClientName = string.Empty;
        /// <summary>
        ///     设备地址
        /// </summary>
        /// <remarks>
        ///     
        /// </remarks>
        public string ClientSocket = string.Empty;




        public ClientBlock()
        {
        }

        public ClientBlock(string str_EndPoint)
        {
            // TODO: Complete member initialization
            this.str_EndPoint = str_EndPoint;
        }

        public ClientBlock(Socket sokConnection)
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            this.sokConnection = sokConnection;
            ClientSocket = textBox_IP.Text = sokConnection.RemoteEndPoint.ToString();
            ClientName = btn_ClientName.Text;
            myFunc.writeCommLog("远程终端:" + textBox_IP.Text + "已连接");
            MySession myTcpClient = new MySession() { TcpSocket = sokConnection };
            //创建线程接收数据
            Thread th_ReceiveData = new Thread(ReceiveData);
            th_ReceiveData.IsBackground = true;
            th_ReceiveData.Name = textBox_IP.Text + "通信";
            th_ReceiveData.Start(myTcpClient);
        }//

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sokConnectionparn"></param>
        private void ReceiveData(object sokConnectionparn)
        {
            tcpClient = sokConnectionparn as MySession;
            Socket socketClient = tcpClient.TcpSocket;
            bool Flag_Receive = true;

            while (Flag_Receive)
            {
                try
                {
                    // 定义一个2M的缓存区；
                    byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                    // 将接受到的数据存入到输入  arrMsgRec中；
                    int length = -1;
                    try
                    {
                        length = socketClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                        if (length == 0)
                        {
                            Flag_Receive = false;
                            // 从通信线程集合中删除被中断连接的通信线程对象；
                            string keystr = socketClient.RemoteEndPoint.ToString();
                            GlobalVar.ClientBlocks.Remove(this);
                            myFunc.writeCommLog("客户端:" + keystr + "断开连接，清除BLOCK");
                            MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RecvClientDisConnect, (IntPtr)0, (IntPtr)0);

                            tcpClient = null;
                            socketClient = null;
                            break;
                        }
                    }
                    catch
                    {
                        Flag_Receive = false;
                        // 从通信线程集合中删除被中断连接的通信线程对象；
                        string keystr = socketClient.RemoteEndPoint.ToString();
                        GlobalVar.ClientBlocks.Remove(this);
                        myFunc.writeCommLog("客户端:" + keystr + "断开连接，清除BLOCK");
                        MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RecvClientDisConnect, (IntPtr)0, (IntPtr)0);

                        tcpClient = null;
                        socketClient = null;
                        break;
                    }
                    byte[] buf = new byte[length];
                    Array.Copy(arrMsgRec, buf, length);
                    lock (tcpClient.m_Buffer)
                    {
                        string msg = System.Text.Encoding.UTF8.GetString(buf);
                        this.BeginInvoke(new Action(() =>
                        {
                            msg = msg.Replace("\0", "").Replace("\r\n", "");
                            richTextBox_Conmunication.AppendText("接收:" + msg.Trim() + "\r\n");
                        }));

                        myFunc.writeCommLog("服务端接收" + ClientSocket + "数据:" + msg);
                        //myFunc.ClientMsgWriteToLocation(msg);//写入本地
                        QueryMsg(socketClient.RemoteEndPoint.ToString(), msg);//处理接收到的数据
                    }
                    //                     string strMsg = "S001";//回发数据
                    //                     byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
                    //                     SendData(socketClient.RemoteEndPoint.ToString(), msg_Buf);//发送给客户端
                }
                catch (Exception ex)
                {
                    myFunc.writeErrorLog("服务端接收" + ClientSocket + "数据异常:" + ex.ToString());
                }
                Thread.Sleep(100);
            }
        }


        /// <summary>
        /// 发送数据给指定的客户端
        /// </summary>
        /// <param name="_endPoint">客户端套接字</param>
        /// <param name="_buf">发送的数组</param>
        /// <returns></returns>
        public bool SendData(string _endPoint, byte[] _buf)
        {
            myFunc.writeCommLog("服务端发送数据到" + _endPoint + "\t内容:" + Encoding.UTF8.GetString(_buf));
            MySession myT = new MySession();
            try
            {
                tcpClient.Send(_buf);
                return true;
            }
            catch (Exception ex)
            {
                myFunc.writeErrorLog("服务端响应客户端" + _endPoint + "异常:" + ex.ToString());
                return false;
            }
        }


        private void btn_ClientName_Click(object sender, EventArgs e)
        {

        }

        private void ClientBlock_Load(object sender, EventArgs e)
        {
            richTextBox_Conmunication.Clear();
            //Round(this.Region);
        }


        private void richTextBox_Conmunication_TextChanged(object sender, EventArgs e)
        {
            richTextBox_Conmunication.SelectionStart = richTextBox_Conmunication.Text.Length; //Set the current caret position at the end
            richTextBox_Conmunication.ScrollToCaret(); //Now scroll it automatically
        }

        private void ClientBlock_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color TColor = ColorTranslator.FromHtml("#00dbde"); //Color.SteelBlue;
            Color FColor = ColorTranslator.FromHtml("#fc00ff");//Color.Gold;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);//窗口渐变色
            //b = new LinearGradientBrush(this.ClientRectangle,FColor,TColor,96.7f,true);
            g.FillRectangle(b, this.ClientRectangle);
        }

        #region
        // 圆角代码
        public void Round(System.Drawing.Region region)
        {
            // -----------------------------------------------------------------------------------------------
            // 已经是.net提供给我们的最容易的改窗体的属性了(以前要自己调API)
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = 0;
            int y = 0;
            int thisWidth = this.Width;
            int thisHeight = this.Height;
            int angle = 45;
            if (angle > 0)
            {
                System.Drawing.Graphics g = CreateGraphics();
                oPath.AddArc(x, y, angle, angle, 180, 90);                                 // 左上角
                oPath.AddArc(thisWidth - angle, y, angle, angle, 270, 90);                 // 右上角
                oPath.AddArc(thisWidth - angle, thisHeight - angle, angle, angle, 0, 90);  // 右下角
                oPath.AddArc(x, thisHeight - angle, angle, angle, 90, 90);                 // 左下角
                oPath.CloseAllFigures();
                Region = new System.Drawing.Region(oPath);
            }
            // -----------------------------------------------------------------------------------------------
            else
            {
                oPath.AddLine(x + angle, y, thisWidth - angle, y);                         // 顶端
                oPath.AddLine(thisWidth, y + angle, thisWidth, thisHeight - angle);        // 右边
                oPath.AddLine(thisWidth - angle, thisHeight, x + angle, thisHeight);       // 底边
                oPath.AddLine(x, y + angle, x, thisHeight - angle);                        // 左边
                oPath.CloseAllFigures();
                Region = new System.Drawing.Region(oPath);
            }
        }
        #endregion

        #region 处理消息
        private bool allowCom = false;
        private void QueryMsg(string endPoint, string msg)
        {
            string msg_Cass = msg.Substring(msg.IndexOf('@') + 1, 2);//指令种类
            switch (msg_Cass)
            {
                case "TH"://客户端握手
                    string strMsg = "S001TH00#";//回发数据
                    byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
                    SendData(endPoint, msg_Buf);//发送给客户端
                    myFunc.writeCommLog("与" + ClientSocket + "握手成功...");
                    int id_Len = msg.IndexOf("@") - 1;
                    this.ClientName = msg.Substring(1, id_Len);
                    MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_Refreash, (IntPtr)0, (IntPtr)0);//刷新设备列表
                    this.BeginInvoke(new Action(() =>
                    {
                        btn_ClientName.Text = this.ClientName;
                        richTextBox_Conmunication.AppendText("发送:" + strMsg + "\r\n");
                    }));
                    allowCom = true;
                    break;
                case "MG"://客户端发送测试信息
                    #region MG
                    if (allowCom)
                    {
                        try
                        {
                            string messgae = msg.Substring(msg.IndexOf("MG") + 2, msg.IndexOf("#") - msg.IndexOf("MG") - 2);
                            string[] items = messgae.Split(',');
                            Root root1 = new Root();
                            root1.id = items[0];
                            root1.product = items[1];
                            root1.person = items[2];
                            root1.time = GlobalVar.gl_syncTime;
                            List<ItemsItem> list = new List<ItemsItem>();
                            foreach (string temp in items)
                            {
                                if (temp.Contains(':'))
                                {
                                    string[] array = temp.Split(':');
                                    if (array.Length == 2)
                                    {
                                        if (array[0].Equals("status", StringComparison.OrdinalIgnoreCase))//状态
                                        {
                                            ItemsItem item = new ItemsItem();
                                            item.code = array[1];
                                            item.value = "";
                                            item.time = root1.time;
                                            list.Add(item);
                                        }
                                        else//测试项
                                        {
                                            ItemsItem item = new ItemsItem();
                                            item.code = array[0];
                                            if (array[1] == "") item.value = "";
                                            else item.value = array[1];
                                            item.time = root1.time;
                                            list.Add(item);
                                        }
                                    }
                                }
                            }
                            root1.items = list;
                            WriteSimpleResult(root1);//写简报
                            string json_root = JsonConvert.SerializeObject(root1);//序列化数据
                            myFunc.writeCommLog("获取" + ClientSocket + "上传json格式数据:" + json_root);
                            strMsg = "";
                            if (WriteToLocal(json_root))//将json写入到本地
                                strMsg = "S001MG201#";//回发数据
                            else strMsg = "S001MG201#";
                            msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
                            SendData(endPoint, msg_Buf);//发送给客户端
                            this.BeginInvoke(new Action(() =>
                            {
                                richTextBox_Conmunication.AppendText("发送:" + strMsg + "\r\n");
                            }));
                        }
                        catch (Exception ex)
                        {
                            myFunc.writeErrorLog(ClientSocket + "通信数据异常:" + ex.ToString());
                        }
                    }
                    #endregion
                    break;
                case "CG"://C4测试软件本地log日志
                    strMsg = "S001CG201#";//回发
                    if (allowCom)
                    {
                        try
                        {
                            string messgae = msg.Substring(msg.IndexOf("MG") + 2, msg.IndexOf("#"));
                            string[] arraystr = messgae.Split(',');
                            if (arraystr.Length == 15)//规定数量
                            {
                                string dirpath = Application.StartupPath + @"\Station_Log\" + this.ClientName + @"\";
                                WriteLocalC4Log(dirpath, arraystr[0], arraystr[1], arraystr[2], arraystr[3], arraystr[4], arraystr[5], arraystr[6], arraystr[7], arraystr[8], arraystr[9], arraystr[10], arraystr[11], arraystr[12], arraystr[13], arraystr[14]);
                            }

                            msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
                            SendData(endPoint, msg_Buf);//发送给客户端
                            this.BeginInvoke(new Action(() =>
                            {
                                richTextBox_Conmunication.AppendText("发送:" + strMsg + "\r\n");
                            }));
                        }
                        catch (Exception ex)
                        {
                            myFunc.writeErrorLog("通信数据异常:" + ex.ToString());
                        }
                    }
                    break;
                default:
                    break;
            }
            #region old 
            //             if (msg.Contains("TH00#"))//客户端握手
            //             {
            //                 string strMsg = "S001TH00#";//回发数据
            //                 byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
            //                 SendData(endPoint, msg_Buf);//发送给客户端
            //                 myFunc.writeCommLog("握手成功...");
            //                 int id_Len = msg.IndexOf("TH00#") - 1;
            //                 this.ClientName = msg.Substring(1, id_Len);
            //                 MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_Refreash, (IntPtr)0, (IntPtr)0);//刷新设备列表
            //                 this.BeginInvoke(new Action(() =>
            //                 {
            //                     btn_ClientName.Text = this.ClientName;
            //                     richTextBox_Conmunication.AppendText("发送:" + strMsg + "\r\n");
            //                 }));
            //                 allowCom = true;
            //             }
            //             else if (msg.Contains("MG"))//客户端发送测试信息
            //             {
            //                 if (allowCom)
            //                 {
            //                     try
            //                     {
            //                         string messgae = msg.Substring(msg.IndexOf("MG") + 2, msg.Length - msg.IndexOf("MG") - 3);
            //                         string[] items = messgae.Split(',');
            //                         Root root1 = new Root();
            //                         root1.id = items[0];
            //                         if (items[1] != "")
            //                             root1.product = items[1];
            //                         if (items[2] != "")
            //                             root1.person = items[2];
            //                         root1.time = GlobalVar.gl_syncTime;
            //                         List<ItemsItem> list = new List<ItemsItem>();
            //                         foreach (string temp in items)
            //                         {
            //                             if (temp.Contains(':'))
            //                             {
            //                                 string[] array = temp.Split(':');
            //                                 if (array.Length == 2)
            //                                 {
            //                                     if (array[0].Equals("status", StringComparison.OrdinalIgnoreCase))//状态
            //                                     {
            // 
            //                                     }
            //                                     else//测试项
            //                                     {
            //                                         ItemsItem item = new ItemsItem();
            //                                         item.code = array[0];
            //                                         item.value = array[1];
            //                                         item.time = root1.time;
            //                                         list.Add(item);
            //                                     }
            //                                 }
            //                             }
            //                         }
            //                         root1.items = list;
            //                         WriteSimpleResult(root1);//写简报
            //                         string json_root = JsonConvert.SerializeObject(root1);//序列化数据
            //                         myFunc.writeCommLog("获取json格式数据:" + json_root);
            //                         string strMsg = "";
            //                         if (WriteToLocal(json_root))//将json写入到本地
            //                             strMsg = "S001MG201#";//回发数据
            //                         else strMsg = "S001MG201#";
            //                         byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
            //                         SendData(endPoint, msg_Buf);//发送给客户端
            //                         this.BeginInvoke(new Action(() =>
            //                         {
            //                             richTextBox_Conmunication.AppendText("发送:" + strMsg + "\r\n");
            //                         }));
            //                     }
            //                     catch (Exception ex)
            //                     {
            //                         myFunc.writeErrorLog("通信数据异常:" + ex.ToString());
            //                         //                             string strMsg = "S001MG500#";//回发数据
            //                         //                             byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
            //                         //                             SendData(endPoint, msg_Buf);//发送给客户端
            //                     }
            //                 }
            // 
            //             }
            //             else if (msg.Contains("C4")) { }
            #endregion
        }

        private void WriteLocalC4Log(string dirpath, string p, string p_2, string p_3, string p_4, string p_5, string p_6, string p_7, string p_8, string p_9, string p_10, string p_11, string p_12, string p_13, string p_14, string p_15)
        {
            WriteToCSVFile(dirpath, p, p_2, p_3, p_4, p_5, p_6, p_7, p_8, p_9, p_10, p_11, p_12, p_13, p_14, p_15);//写入到本地      
        }

        /// <summary>
        ///     将接收到的信息写入到本地简报
        /// </summary>
        /// <param name="root1" type="OEESystem.Root">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void WriteSimpleResult(Root root1)
        {
            try
            {
                string dirpath = Application.StartupPath + @"\log\Simple\";
                string filepath = dirpath + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
                bool CsvCreate = false;
                string _logfile = filepath + ".csv";
                CsvCreate = File.Exists(_logfile);

                FileStream FS = new FileStream(_logfile, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter SW = new StreamWriter(FS, Encoding.Default);

                if (!CsvCreate)
                    SW.Write(string.Format("机台编号,品目号,作业员,日期,数据项\r\n"));
                string items = string.Empty;
                foreach (ItemsItem item in root1.items)
                {
                    if (item.value != null)
                        items += "'" + item.code + ":" + item.value + "',";
                    else
                        items += "'Error:" + item.code + "',";
                }
                string writestr = string.Format("{0},{1},{2},{3},{4}\r\n",
                root1.id,
                root1.product,
                root1.person,
                root1.time,
                items
                );
                SW.Write(writestr);
                SW.Close();
                SW.Dispose();
            }
            catch (Exception ex)
            {
                myFunc.writeErrorLog("WriteCSV Error:" + ex.ToString());
            }
        }

        /// <summary>
        ///     将json格式数据写入到本地
        /// </summary>
        /// <param name="json_Str" type="string">
        ///     <para>
        ///         json格式数据
        ///     </para>
        /// </param>
        private bool WriteToLocal(string json_Str)
        {
            try
            {
                myFunc.writeCommLog("json数据写入本地详报");
                string log_Dir = Application.StartupPath + @"\Update";
                string file_Path = log_Dir + @"\" + this.ClientName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                if (!Directory.Exists(log_Dir)) Directory.CreateDirectory(log_Dir);
                FileStream fs = new FileStream(file_Path, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(json_Str);
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                myFunc.writeErrorLog("json数据写入异常:" + ex.ToString());
                return false;
            }
        }

        #endregion

        private void WriteToCSVFile(string dirpath, string SiteID, string Product, string TesterID, string date, string time, string gl_LotNo, string loginMode, string stopTime, string restarttime, string downTime, string keyword, string status, string ErrorCode, string ErrorMessage, string Message)
        {
            try
            {
                //string dirpath = Application.StartupPath + @"\Station_Log\" + TesterID + "\\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string filepath = dirpath + @"\" + DateTime.Now.ToString("yyyyMMdd") + ".csv";//文件路径

                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                StringBuilder strValue = new StringBuilder();
                FileStream fs = new FileStream(filepath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                bool CsvCreate = File.Exists(filepath);

                FileStream FS = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter SW = new StreamWriter(FS, Encoding.Default);

                if (!CsvCreate)
                    SW.Write(string.Format("SiteID,Product,Tester,DATE,TIME,LOT NAME,LOGIN MODE,STOP TIME,RESTART TIME,DOWN TIME,SUBJECT,STATUS,Error_Code,Error Message,MESSAGE\r\n"));
                string writestr = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}\r\n",
                SiteID,
                Product,
                TesterID,
                date,
                time,
                gl_LotNo,
                stopTime,
                restarttime,
                downTime,
                keyword,
                status,
                ErrorCode,
                ErrorMessage,
                Message
                );
                SW.Write(writestr);
                SW.Close();
                SW.Dispose();
            }
            catch (System.Exception ex)
            {
                myFunc.writeErrorLog("写C4本地日志文件异常:" + ex.ToString());
            }
        }

    }

    /// <summary>
    ///     json数据类
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    public class ItemsItem
    {
        /// <summary>
        /// 测试项信息
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 测试项值（状态）
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public string time { get; set; }

    }
    public class Root
    {
        /// <summary>
        /// 机台编号
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 品目
        /// </summary>
        public string product { get; set; }

        /// <summary>
        /// 作业员
        /// </summary>
        public string person { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 测试项数组
        /// </summary>
        public List<ItemsItem> items { get; set; }

    }
}
