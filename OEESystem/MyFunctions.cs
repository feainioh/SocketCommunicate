using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using Newtonsoft.Json;//解析json
using System.Diagnostics;

namespace OEESystem
{
    class MyFunctions
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam );

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd ,int wMsg,IntPtr wParam,ref COPYDATASTRUCT lParam);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        /// <summary>
        ///     写正常日志
        /// </summary>
        /// <param name="Logstr" type="string">
        ///     <para>
        ///         日志内容
        ///     </para>
        /// </param>
        public  void writeCommLog(string Logstr)
        {
            try
            {
                string dir_Path = Application.StartupPath + @"\log\Common\" + DateTime.Now.ToString("yyyy-MM-dd");
                string file_Path = dir_Path + "\\"+DateTime.Now.ToString("yyyyMMdd_HH") + ".txt";
                if (!Directory.Exists(dir_Path))
                {
                    Directory.CreateDirectory(dir_Path);
                }
                if (!File.Exists(file_Path))
                {
                    File.Create(file_Path);
                }
                StreamWriter sw = new StreamWriter(file_Path, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")+"\t\t"+ Logstr);
                sw.Close();
                deleteLogFiles(dir_Path);
            }
            catch {  }
        }

        /// <summary>
        ///     写异常日志
        /// </summary>
        /// <param name="Logstr" type="string">
        ///     <para>
        ///         日志内容
        ///     </para>
        /// </param>
        public void writeErrorLog(string Logstr)
        {
            try
            {
                string dir_Path = Application.StartupPath + @"\log\Error\";
                string file_Path = dir_Path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!Directory.Exists(dir_Path))
                {
                    Directory.CreateDirectory(dir_Path);
                }
                if (!File.Exists(file_Path))
                {
                    File.Create(file_Path);
                }
                StreamWriter sw = new StreamWriter(file_Path, true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\t\t" + Logstr);
                sw.Close();
                deleteLogFiles(dir_Path);
            }
            catch{ }
        }

        /// <summary>
        ///     删除过期日志
        /// </summary>
        /// <param name="dir_Path" type="string">
        ///     <para>
        ///         日志目录
        ///     </para>
        /// </param>
        private  void deleteLogFiles(string dir_Path)
        {
            DirectoryInfo dir = new DirectoryInfo(dir_Path);
            FileInfo[] files = dir.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                if ((DateTime.Now.Subtract(file.LastWriteTime)).Days > 60)
                {
                    file.Delete();
                }
            }
        }



        /// <summary>
        ///     新增简报到服务器
        /// </summary>
        /// <param name="files" type="System.IO.FileInfo[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        internal void InsertSimpleResult(FileInfo[] Files)
        {
            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    string content = File.ReadAllText(Files[i].FullName);
                    if (string.IsNullOrEmpty(content.Trim('\0')))
                    {
                        File.Delete(Files[i].FullName);//内容为空 直接删除该文件
                        return;
                    }
                    if (updateJsonToServer(content)) File.Delete(Files[i].FullName);//写入数据库成功后删除该文件
                    else
                    {
                        string dir_path = Application.StartupPath + "\\" + GlobalVar.ErrorText + "\\";
                        if(!Directory.Exists(dir_path)) Directory.CreateDirectory(dir_path);
                        File.Copy(Files[i].FullName, dir_path + Files[i].Name,true);//数据有问题的移动到问题文件夹
                        File.Delete(Files[i].FullName);
                    }
                }
                catch (Exception ex)
                {
                    writeErrorLog("上传详报异常：" + ex.ToString());
                }
            }
        }

        public bool updateJsonToServer(string content)
        {
            try  
            {                  
                byte[] data = Encoding.UTF8.GetBytes(content);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://192.168.208.101/iot-api/v1/value");  
                request.KeepAlive = false;  
                request.Method = "POST";  
                request.ContentType = "application/json";  
                request.ContentLength = data.Length;  
                Stream sm = request.GetRequestStream();  
                sm.Write(data, 0, data.Length);  
                sm.Close();
                MessageToMainForm("发送json数据:" + content);//同步显示通信消息
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    writeCommLog("上传数据，服务器响应:"+(int)response.StatusCode);
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                    writeErrorLog("服务器响应异常:"+ex.ToString());
                }
                Stream streamResponse = response.GetResponseStream();  
                StreamReader streamRead = new StreamReader(streamResponse, Encoding.UTF8);  
                string result = streamRead.ReadToEnd();
                int status = (int)response.StatusCode;//获取状态码
                response.Close();
                MessageToMainForm("上传数据，服务器响应:" + result);
                ServerRecv m = JsonConvert.DeserializeObject<ServerRecv>(result);
                if(status==201||m.code=="CREATED"||m.message =="done")return true;
                else return false;  
            }  
            catch (Exception ex)  
            {  
                writeErrorLog("上传详报到服务器异常"+ex.ToString());
                return false;
            }    
        }

        public void MessageToMainForm(string content)
        {
            writeCommLog(content);
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            cds.dwData = (IntPtr)0;
            cds.cbData = content.Length+10;
            cds.lpData = content;
            SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_CopyData, (IntPtr)0, ref cds);
        }

        /// <summary>
        ///     与服务器同步时间
        /// </summary>
        public  void  syncTimeFromServer()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://192.168.208.101/iot-api/v1/time");
                request.KeepAlive = false;
                request.Method = "GET";
                
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                GlobalVar.gl_syncTime = myStreamReader.ReadToEnd();
                string time = GlobalVar.gl_syncTime.Substring(0,GlobalVar.gl_syncTime.IndexOf('['));
                GlobalVar.gl_syncTime = time;
                myStreamReader.Close();
                myResponseStream.Close();
                //Response.Write(retString);

                //MessageBox.Show(content);
            }catch(System.Exception ex )
            {
                writeErrorLog("同步服务器时间异常"+ex.ToString());
            }
        }

        internal void ClientMsgWriteToLocation(string msg)
        {
            throw new NotImplementedException();
        }

        internal void warningShow(string msg)
        {
            Warning warn = new Warning(msg);
            warn.ShowDialog();
        }

        string userName = "mmcs\\santec";
        string passWord = "Mektec01!";
        string netPath = @"\\192.168.208.237\share";
        //登录网络共享，每天要登录一次否则会断开
        public void LoadShare()
        {
            Process proc = new Process();
            try
            {
                string dosLine = @"net use " + netPath + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                //dosLine += "\r\n" + "del update.bat";
                //File.WriteAllText("update.bat", dosLine, ASCIIEncoding.Default);
                //System.Diagnostics.Process.Start("update.bat");

                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                //GlobalVar.gl_lastLoadNetTime = DateTime.Now;  //成功登录共享盘时间
            }
            catch (Exception ex)
            {
                writeErrorLog("登录共享盘异常：" + ex.ToString());
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
        }

    }

    /// <summary>
    ///     服务器响应json类
    /// </summary>
    /// <remarks>
    ///     
    /// </remarks>
    class ServerRecv
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }
}
