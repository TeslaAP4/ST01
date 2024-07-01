using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;


namespace AlphaRap.Classes
{
    /// <summary>
    /// 一个很简单的PF通信类 
    /// PF4002采用open protocol revision 1.0 理论是要6.0 可是我测试后只能用1.0
    /// 只实现简单的 通信建立 订阅最后扭力数据 通信断开 KeepAlive功能
    /// Johnson Xu@2019年6月10号
    /// </summary>
    public class EthernetPF
    {
        public bool TighteningResultUpdated = false;
        public MID.LastTighteningResult LastTighteningResult;
        public int Port = 4545;
        public string IP = "192.168.99.99";
        public long KeepAliveTick
        {
            get { return KeepAliveTimer.ElapsedMilliseconds; }
        }

        public SimpleTcpClient Controller; //PF4000只能作服务器
        private Stopwatch KeepAliveTimer=Stopwatch.StartNew();

        //先发送MID0001等待MID0002返回确认OK 如果收到MID0004代表不OK
        //需要选择是Application Level acknowledging 还是 Link Level acknowledging
        //MID0003 通信结束

        // Request messages
        // Command messages
        // Subscription messages
        // Keep alive

        //Establishing contact
        //Prerequisite: The controller has an IP address and listens to port 4545.
        //1. The controller listens to port 4545 acting as a server.
        //2. The integrator connects to the controller acting as client.
        //3. The controller accepts the connection.
        //4. The integrator sends MID 0001 Communication start.
        //5. The controller answers MID 0002 Communication start acknowledge with Cell ID 0001, Channel ID 04 and Controller name Airbag.

        public bool Connect()
        {
            Controller = new SimpleTcpClient();

            Controller.DataReceived += OnPackageReceived;
            Controller.DelimiterDataReceived += OnPackageReceived;

            return Controller.Connect(IP,Port);
        }

        public void Disconnect()
        {
            try
            {
                Controller.DataReceived -= OnPackageReceived;
                Controller.DelimiterDataReceived -= OnPackageReceived;
                Controller.Disconnect();
                Controller.Dispose(); //may be bug
            }
            catch (Exception e)
            {
                //JabilSDK.JLog.Instance.Add(JabilSDK.JLog.Type.Net, $"Fastener Disconnect Exceptioin: {e.Message}");
            }

        }

        public void CommunicationAlive()
        {
            KeepAliveTimer = Stopwatch.StartNew();
        }

        public string SendAndWaitForResponse(string Message, TimeSpan Timeout)
        {
            try
            {
                //System.Threading.Thread.Sleep(100);

                //Console.WriteLine($"Send: {Message}");
                Message Response = Controller.WriteLineAndGetReply(Message, Timeout);

                //Console.WriteLine((Response != null) ? $"Response: {Response.MessageString}" : "Response null");

                if (Response != null)
                {
                    CommunicationAlive();

                    //解析MID
                    if (MID.HeaderParser(Response.Data) == 4)
                    {
                        MID.MID0004Error ErrorMessage = MID.MID0004Parser(Response.Data);//MID0004
                        //Console.WriteLine($"MID{ErrorMessage.Mid:D4} " +
                        //    $"{Enum.GetName(typeof(MID.MID0004ErrorCode), ErrorMessage.Error)}");
                    }
                    return Response.MessageString;
                }

                return null;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Exception: {ex.Message}");
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }

        public void OnPackageReceived(object sender, Message message)
        {
            try
            {
                Console.WriteLine($"Message arrived: {message.MessageString}");

                if (61 == MID.HeaderParser(message.Data))
                {
                    Controller.WriteLine(MID.M0062); //如果socket断开 需要处理
                    //以前版本 MID0062    现在改为MID0061
                    LastTighteningResult = MID.MID0061Parser(message.Data);
                    TighteningResultUpdated = true;
                    CommunicationAlive();

                    //Console.WriteLine(
                    //    $"<TORQUE>{LastTighteningResult.TORQUE}\r\n" +
                    //    $"<ANGLE>{LastTighteningResult.ANGLE}\r\n" +
                    //    $"<Rundown Angle>{LastTighteningResult.RUNDOWN_ANGLE}\r\n" +
                    //    $"<TIGHTENING_STATUS>{LastTighteningResult.TIGHTENING_STATUS}\r\n");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public bool StartCommunication()
        {
            bool bRet = false;
            // se cambia a t respuesta 2 seg. de 1.5 antes, NP tiene 2
            string Response = SendAndWaitForResponse(MID.M0001, TimeSpan.FromSeconds(2));
            if (Response != null)
            {
                if (MID.HeaderParser(ASCIIEncoding.ASCII.GetBytes(Response)) == 2)
                {
                    bRet =true;
                }
            }
            else
            {
                bRet= false;
            }
            return bRet;



        }

        public bool StopCommunication()
        {
            // se cambia a t respuesta 10 seg. de8 antes, NP tiene 10
            string Response = this.SendAndWaitForResponse(MID.M0003, TimeSpan.FromSeconds(10));
            if (Response == null)
            {
                return false;
            }
            if (Response.Contains(MID.M0005))
            {
                return true;
            }

            return false;
        }

        // se cambia a t respuesta 5 seg. de 3 antes, NP tiene 5
        public bool SubscribeLastTighteningResult()
        {
            string Response = SendAndWaitForResponse(MID.M0060, TimeSpan.FromSeconds(5));
            if (Response == null)
            {
                return false;
            }
            if (Response.Contains(MID.M0005))
            {
                return true;
            }
            else if (Response.Contains(MID.M0004))
            {
                return false;
            }
            return false;
        }

        // se cambia a t respuesta 5 seg. de 3 antes, NP tiene 5
        public bool UnsubscribeLastTighteningResult()
        {
            string Response = SendAndWaitForResponse(MID.M0063, TimeSpan.FromSeconds(5));
            if (Response == null)
            {
                return false;
            }
            if (Response.Contains(MID.M0005))
            {
                return true;
            }
            else if (Response.Contains(MID.M0004))
            {
                return false;
            }
            return false;
        }


        public bool RequestOldTighteningResultUpload(int TighteningID)
        {
            try
            {
                string Response = SendAndWaitForResponse(MID.M0064 + $"{TighteningID:D10}", TimeSpan.FromSeconds(3));
                if (Response.Contains(MID.M0065))
                {
                    LastTighteningResult = MID.MID0065Parser(Encoding.ASCII.GetBytes(Response));
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

    }
}
