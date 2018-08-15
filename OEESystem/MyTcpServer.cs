using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace OEESystem
{
    /// <summary>
    /// 服务端
    /// </summary>
    public class MyTcpServer
    {

        private static Socket ServerSocket = null;//服务端  
        public Dictionary<string, MySession> dic_ClientSocket = new Dictionary<string, MySession>();//tcp客户端字典
        private Dictionary<string, Thread> dic_ClientThread = new Dictionary<string, Thread>();//线程字典,每新增一个连接就添加一条线程
        private Dictionary<string, ClientBlock> dic_ClientBlocks = new Dictionary<string, ClientBlock>();//ClientBlock字典，没新增一个连接就添加一个ClientBlock

        private bool Flag_Listen = true;//监听客户端连接的标志
        MyFunctions myFunc = new MyFunctions();//实例化工具类

        
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="port">端口号</param>
        public bool OpenServer(string ip ,int port)
        {
            try
            {
                    Flag_Listen = true;
                    // 创建负责监听的套接字，注意其中的参数；
                    ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ServerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);//server地址设置成允许重复使用
                    // 创建包含ip和端口号的网络节点对象；
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    try
                    {
                        // 将负责监听的套接字绑定到唯一的ip和端口上；
                        ServerSocket.Bind(endPoint);
                        myFunc.writeCommLog("服务端建立监听,绑定IP:" + endPoint.Address.ToString() + " 端口号:" + endPoint.Port.ToString());
                    }
                    catch (Exception ex)
                    {
                        myFunc.writeErrorLog("绑定IP端口号 异常:" + ex.ToString());
                        return false;
                    }
                    // 设置监听队列的长度；
                    ServerSocket.Listen(100);
                    // 创建负责监听的线程；
                    Thread Thread_ServerListen = new Thread(ListenConnecting);
                    Thread_ServerListen.IsBackground = true;
                    Thread_ServerListen.Name = "监听连接";
                    Thread_ServerListen.Start();

                    return true;
                
            }
            catch(Exception ex)
            {
                myFunc.writeErrorLog("服务端监听异常:"+ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseServer()
        {
            lock (dic_ClientSocket)
            {
                foreach (var item in dic_ClientSocket)
                {
                    item.Value.Close();//关闭每一个连接
                }
                dic_ClientSocket.Clear();//清除字典
            }
            lock (dic_ClientThread)
            {
                foreach (var item in dic_ClientThread)
                {
                    item.Value.Abort();//停止线程
                }
                dic_ClientThread.Clear();
            }
            Flag_Listen = false;
            //ServerSocket.Shutdown(SocketShutdown.Both);//服务端不能主动关闭连接,需要把监听到的连接逐个关闭
            if (ServerSocket != null)
            {
                ServerSocket = null;
            }

        }
        /// <summary>
        /// 监听客户端请求的方法；
        /// </summary>
        private void ListenConnecting()
        {
            List<string> ClientsIP = new List<string>();//存储IP地址
            myFunc.writeCommLog("服务端开始监听连接...");
            while (Flag_Listen)  // 持续不断的监听客户端的连接请求；
            {
                try
                {
                    Socket sokConnection = ServerSocket.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                    // 将与客户端连接的 套接字 对象添加到集合中；
                    string str_EndPoint = sokConnection.RemoteEndPoint.ToString();
                    string str_EndIp = str_EndPoint.Split(':')[0];
                    if (ClientsIP.Contains(str_EndPoint))
                    {
                        myFunc.writeCommLog("客户端:" + str_EndPoint + "重复连接");
                        foreach (string keystr in dic_ClientBlocks.Keys)
                        {
                            if (keystr.Contains(str_EndIp))//如果存在重复IP,清除旧有的连接,创建新连接
                            {
                                myFunc.writeCommLog("清除过期连接:" + keystr);
                                GlobalVar.ClientBlocks.Remove(dic_ClientBlocks[keystr]);//删除block
                                dic_ClientSocket.Remove(keystr);//删除客户端字典中该socket
                                //dic_ClientThread[keystr].Abort();//关闭线程
                                dic_ClientThread.Remove(keystr);//删除字典中该线程
                                dic_ClientBlocks.Remove(keystr);//删除该字典中的block
                                MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RecvClientDisConnect, (IntPtr)0, (IntPtr)0);
                                break;
                            }
                        }
                        ClientBlock client = new ClientBlock(sokConnection);
                        GlobalVar.ClientBlocks.Add(client);
                        MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RecvClientConnect, (IntPtr)0, (IntPtr)0);//发送窗口消息
                        MySession myTcpClient = new MySession() { TcpSocket = sokConnection };
                        //创建线程接收数据
                        Thread th_ReceiveData = new Thread(ReceiveData);
                        th_ReceiveData.IsBackground = true;
                        //th_ReceiveData.Start(myTcpClient);
                        //把线程及客户连接加入字典
                        dic_ClientThread.Add(str_EndPoint, th_ReceiveData);
                        dic_ClientSocket.Add(str_EndPoint, myTcpClient);
                        dic_ClientBlocks.Add(str_EndPoint, client);
                        ClientsIP.Add(str_EndPoint);
                    }
                    else
                    {
                        ClientBlock client = new ClientBlock(sokConnection);
                        GlobalVar.ClientBlocks.Add(client);
                        myFunc.writeCommLog("服务端监听到客户端连接:" + str_EndPoint + "\t创建BLOCK");
                        MyFunctions.SendMessage(GlobalVar.gl_IntPtr_MainWindow, GlobalVar.WM_RecvClientConnect, (IntPtr)0, (IntPtr)0);//发送窗口消息
                        MySession myTcpClient = new MySession() { TcpSocket = sokConnection };
                        //创建线程接收数据
                        Thread th_ReceiveData = new Thread(ReceiveData);
                        th_ReceiveData.IsBackground = true;
                        //th_ReceiveData.Start(myTcpClient);
                        //把线程及客户连接加入字典
                        dic_ClientThread.Add(str_EndPoint, th_ReceiveData);
                        dic_ClientSocket.Add(str_EndPoint, myTcpClient);
                        dic_ClientBlocks.Add(str_EndPoint, client);
                        ClientsIP.Add(str_EndPoint);
                    }
                }
                catch (Exception ex)
                {
                    myFunc.writeErrorLog("服务端监听 异常:" + ex.ToString());
                }
                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sokConnectionparn"></param>
        private void ReceiveData(object sokConnectionparn)
        {
            MySession tcpClient = sokConnectionparn as MySession;
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
                    }
                    catch
                    {
                        Flag_Receive = false;
                        // 从通信线程集合中删除被中断连接的通信线程对象；
                        string keystr = socketClient.RemoteEndPoint.ToString();
                        dic_ClientSocket.Remove(keystr);//删除客户端字典中该socket
                        dic_ClientThread[keystr].Abort();//关闭线程
                        dic_ClientThread.Remove(keystr);//删除字典中该线程
                        dic_ClientBlocks.Remove(keystr);//删除该字典中的block
                        GlobalVar.ClientBlocks.Remove(dic_ClientBlocks[keystr]);


                        tcpClient = null;
                        socketClient = null;
                        break;
                    }
                    byte[] buf = new byte[length];
                    Array.Copy(arrMsgRec, buf, length);
                    lock (tcpClient.m_Buffer)
                    {
                        string msg = System.Text.Encoding.UTF8.GetString(buf);
                        myFunc.writeCommLog("服务端接收数据:"+msg);
                        myFunc.ClientMsgWriteToLocation(msg);//写入本地
                        tcpClient.AddQueue(buf);
                    }
                    string strMsg = "";//回发数据
                    byte[] msg_Buf = System.Text.Encoding.UTF8.GetBytes(strMsg);
                    SendData(socketClient.RemoteEndPoint.ToString(),msg_Buf);//发送给客户端
                }
                catch(Exception ex)
                {
                    myFunc.writeErrorLog("服务端接收数据异常:"+ex.ToString());
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
            myFunc.writeCommLog("服务端发送数据到"+_endPoint+"\t内容:"+Encoding.UTF8.GetString(_buf));
            MySession myT = new MySession();
            if (dic_ClientSocket.TryGetValue(_endPoint, out myT))
            {
                myT.Send(_buf);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 会话端
    /// </summary>
    public class MySession
    {
        public Socket TcpSocket;//socket对象
        public List<byte> m_Buffer = new List<byte>();//数据缓存区

        public MySession()
        {

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buf"></param>
        public void Send(byte[] buf)
        {
            if (buf != null)
            {
                TcpSocket.Send(buf);
            }
        }
        /// <summary>
        /// 获取连接的ip
        /// </summary>
        /// <returns></returns>
        public string GetIp()
        {
            IPEndPoint clientipe = (IPEndPoint)TcpSocket.RemoteEndPoint;
            string _ip = clientipe.Address.ToString();
            return _ip;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            TcpSocket.Shutdown(SocketShutdown.Both);
        }
        /// <summary>
        /// 提取正确数据包
        /// </summary>
        public byte[] GetBuffer(int startIndex, int size)
        {
            byte[] buf = new byte[size];
            m_Buffer.CopyTo(startIndex, buf, 0, size);
            m_Buffer.RemoveRange(0, startIndex + size);
            return buf;
        }

        /// <summary>
        /// 添加队列数据
        /// </summary>
        /// <param name="buffer"></param>
        public void AddQueue(byte[] buffer)
        {
            m_Buffer.AddRange(buffer);
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearQueue()
        {
            m_Buffer.Clear();
        }
    }

}
