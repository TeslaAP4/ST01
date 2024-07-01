using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Acura3.Classes
{
   public class TCPCLient
    {
        // tcp通信对象
        private TcpClient tcpClient = new TcpClient();
        private NetworkStream stream = null;

        /// <summary>
        /// 重连服务端
        /// </summary>
        public bool Reconnect(string strIP, int intPort)
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
        /// 断开服务端
        /// </summary>
        public void Disconnect()
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
        /// 连接状态
        /// </summary>
        public bool ConnectStatus()
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
        /// 连接服务端
        /// </summary>
        public bool Connect(string strIP, int intPort)
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
        /// 获取数据   反馈数值M0，头1，头2，头3。。。。头n；顺序是从主（1500）1，副2（1550），副3（1550），
        //  M0,-000001938,+000001718,-099999998 三位小数，超过正负10，99999998则读取失败，超读取头范围。
        /// </summary>
        /// 
        private string GetData(int intTMOut)
        {
            try
            {
                Send("M0\r\n");
                return Receive(intTMOut);
            }
            catch (Exception)
            {
                return "Err";
            }

        }

        public bool Send(string message)
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
        /// 超时MS不得超过60000超过强制改为500
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public string Receive(int intTMOut)
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
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                if (responseData != null)
                {
                    return responseData;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146232800)
                {
                    return "Err,TimeOut";
                }
                return "Err";
            }
            return "Err";
        }
        /// <summary>
        /// 直接读取
        /// </summary>
        /// <param name="intTMOut"></param>
        /// <returns></returns>
        public string Receive()
        {
            stream.ReadTimeout = 10;
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
                if (ex.HResult == -2146232800)
                {
                    return "Err";
                }
                return "Err,Fail";
            }
            return "Err";
        }
    }
}
