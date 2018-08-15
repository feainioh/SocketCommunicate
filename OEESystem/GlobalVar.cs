using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEESystem
{
    class GlobalVar
    {
        /************************************全局变量*****************************************************/
        public static string gl_appName = "OEE稼动率信息采集系统";//当前程序名称
        public static string gl_password = "Santec1234";//管理员密码
        public static bool gl_AdminLogin=false;//默认是operator
        public static string gl_ExcelSavePath = "";//数据存储路径
        public static bool gl_AppAlive=false;//判断程序是否运行中
        public static string gl_syncTime = "";//同步服务器时间
        public static IntPtr gl_IntPtr_MainWindow;//主窗口句柄
        public static string ErrorText = @"\ErrorTxt\";
        public static string gl_updateLog = @"\\192.168.208.237\share\临时文件夹\0712\LQZ\LQZ\";//上传日志到237网盘



        /************************************窗口消息变量**************************************************/
        public const int WM_CopyData = 0x004A;//发送服务器通信 消息
        public const int WM_RecvClientConnect = 0x0400 + 1;//客户端连接到服务端
        public const int WM_RecvClientDisConnect = 0x0400 + 2;//客户端断开连接
        public const int WM_SendToServer = 0x0400 + 3;//上传数据到服务器
        public const int WM_RecvFromaServer = 0x400 + 4;//服务器响应数据
        public const int WM_Refreash = 0x400 + 5;//刷新设备连接列表
        public const int WM_ShowCOMLose = 0x400 + 6;               //Main窗口显示串口丢失


        /************************************SOCKET通信变量**********************************************/
        public static string gl_ServerIP="127.0.0.1";//服务端IP地址
        public static int gl_ServerPort=18889;//服务端端口号



        /*************************************配置文件变量*************************************************/
        public static string gl_Section_Socket="SOCKET";

        public static string gl_Key_ServerIP = "Server_IP";
        public static string gl_Key_ServerPort = "Server_Port";


        /**************************************控件全局变量************************************************/
        public static List<ClientBlock> ClientBlocks = new List<ClientBlock>();//客户端控件

    }
}
