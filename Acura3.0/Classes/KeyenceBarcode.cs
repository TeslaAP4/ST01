using NPClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acura3._0.Classes
{
  public  class KeyenceBarcode
    {
        /// <summary>
        /// 扫码枪Ip
        /// </summary>
       public string IP = "192.168.1.1";
        /// <summary>
        /// 扫码枪Port
        /// </summary>
       public string Port = "9600";

        /// <summary>
        /// 扫码结果
        /// </summary>
        public string S_ScanResult;

        //扫码枪1实例化
        public TCPCLient T_KeyenceBarcode = new TCPCLient();

        //扫码枪连接
        public bool ConnectBarcode()
        {
            if (T_KeyenceBarcode.ConnectStatus())
            {
                return true;
            }
            else
            {
                T_KeyenceBarcode.Disconnect();
                int iPort = Convert.ToInt32(Port);
                bool B_ScanConnect = T_KeyenceBarcode.Connect(IP, iPort);
                return B_ScanConnect;
            }
        }

        /// <summary>
        /// 触发扫码
        /// </summary>
        /// <returns></returns>
        public bool TrigerBarcodeEx()
        {
            bool B_Triger = false;
            if (T_KeyenceBarcode.ConnectStatus())
            {
                int length = T_KeyenceBarcode.tcpClient.Client.Available;
                if (length > 0)
                {
                    byte[] data = new byte[length];
                    T_KeyenceBarcode.tcpClient.Client.Receive(data);
                }
                B_Triger = T_KeyenceBarcode.Sent("LON" + "\r\n");
            }
            else
            {
                T_KeyenceBarcode.Disconnect();
                ConnectBarcode();
                if (T_KeyenceBarcode.ConnectStatus())
                {
                    int length = T_KeyenceBarcode.tcpClient.Client.Available;
                    if (length > 0)
                    {
                        byte[] data = new byte[length];
                        T_KeyenceBarcode.tcpClient.Client.Receive(data);
                    }
                    B_Triger = T_KeyenceBarcode.Sent("LON" + "\r\n");
                }
                else
                {
                    T_KeyenceBarcode.Sent("LOFF " + "\r\n");//？？？
                    B_Triger = false;
                }
            }
            return B_Triger;
        }

        /// <summary>
        /// 获取1扫码结果
        /// </summary>
        /// <returns></returns>
        public bool GetBarcodeResult()
        {
            bool B_Result = false;
            S_ScanResult = "";
            if (T_KeyenceBarcode.ConnectStatus())
            {
                string S_Result = T_KeyenceBarcode.Receive().Replace("\r", "").Replace("ERROR", "").Replace("NULL", "");
                if (S_Result.Length > 5)
                {
                    S_ScanResult = S_Result;
                    B_Result = true;
                }
                else
                {
                    B_Result = false;
                }
            }
            return B_Result;
        }
    }
}
