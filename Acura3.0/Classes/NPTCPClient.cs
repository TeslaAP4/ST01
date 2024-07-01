using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// V1.0  song
/// V2.0  add send and recieve byte array function 202011151100
/// </summary>

namespace NPClient
{
    public class TCPCLient
    {
     
        // tcp client  通信对象
        public  TcpClient tcpClient = new TcpClient();
        public  NetworkStream stream = null;

        /// <summary>
        ///Reconnect server 重连服务端
        /// </summary>
        public  bool Reconnect(string strIP, int intPort)
        {
            try
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
                tcpClient = new TcpClient(strIP, intPort);
                if (tcpClient.Connected)
                {
                    stream = tcpClient.GetStream();
                    stream.WriteTimeout = 100;//写入超时0.1秒
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }


        /// <summary>
        ///disconnect server 断开服务端
        /// </summary>
        public   void Disconnect()
        {
            try
            {
                if (tcpClient != null)
                {
                    stream.Dispose();
                    tcpClient.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Connect satus 连接状态
        /// </summary>
        public  bool ConnectStatus()
        {
            try
            {
                return tcpClient.Connected;
            }
            catch (Exception)
            {

                return false;
            }

        }


        /// <summary>
        /// Connect 连接服务端
        /// </summary>
        public  bool Connect(string strIP, int intPort)
        {
            try
            {
                     tcpClient = new TcpClient(strIP, intPort);
                //获取网络流
                NetworkStream networkStream = tcpClient.GetStream();
                if (tcpClient.Connected)
                {
                    stream = tcpClient.GetStream();
                    stream.WriteTimeout = 100;//写入超时0.1秒
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// Sent Byte array 发送byte类型数组数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Sent(byte[] data)
        {
            try
            {
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Sent String 发送字符串型数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public  bool Sent(string message)
        {
            try
            {
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        /// <summary>
        /// Wait Receive byte data，Delay TM No Longer Than 60000,program will force to 500 接受byte数组数据类型，等待固定时间，超时返回空
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public Byte[] ReceiveByte(int intTMOut)
        {
            if (intTMOut <= 0 || intTMOut >= 60000)
            {
                intTMOut = 500;
            }
            stream.ReadTimeout = intTMOut;
            Byte[] data = new Byte[1024];
            String responseData = String.Empty;

            try
            {
                int bytes = stream.Read(data, 0, data.Length);
                byte[] dataReseice = new byte[bytes];
                for (int i = 0; i < bytes; i++)
                {
                    dataReseice[i] = data[i];
                }
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                if (responseData != null)
                {
                    return dataReseice;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// Receive byte array 直接获取端口byte数组数据没有则返回空
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public Byte[] ReceiveByte()
        {
            stream.ReadTimeout = 1;
            Byte[] data = new Byte[1024];
            String responseData = String.Empty;

            try
            {
                int bytes = stream.Read(data, 0, data.Length);
                byte[] dataReseice = new byte[bytes];
                for (int i = 0; i < bytes; i++)
                {
                    dataReseice[i] = data[i];
                }
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                if (responseData != null)
                {
                    return dataReseice;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        /// <summary>
        ///Wait Receive String  ,Delay TM No Longer Than 60000,program will force to 500 接受字符串类型数据，等待固定时间，超时反馈空
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public  string Receive(int intTMOut)
        {
            if (intTMOut<=0 || intTMOut>=60000)
            {
                intTMOut = 500;
            }
            stream.ReadTimeout = intTMOut;
            Byte[] data = new Byte[1024];
            String responseData = String.Empty;
 
                try
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                    if (responseData != null)
                    {
                        return responseData;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult== -2146232800)
                    {
                        return "Err,TimeOut";
                    }
                    return "Err";
                }
            return "Err";
        }
        /// <summary>
        /// Receive directly 直接获取端口字符串数据没有则返回空
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public string Receive()
        {
            stream.ReadTimeout = 1;
            Byte[] data = new Byte[1024];
            String responseData = String.Empty;

                try
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                    if (responseData != null)
                    {
                        return responseData;
                    }
                }
                catch (Exception ex)
                {

                string a = ex.ToString();
                    if (ex.HResult == -2146232800)
                    {
                        return "Null";
                    }
                    return "Err,Fail";
                }
            return "Err";
        }
    }
}

