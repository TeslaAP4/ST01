using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Acura3._0.Classes
{
   public class AtlasLibrary1
    {
        private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        Socket clientSocket;
        public bool bConnectStatus;
        private string sReadData = null;
        System.Timers.Timer time;
        public Atlas atlas = new Atlas();
        
        private readonly object read = new object();
        private const string StartCommunicateSend = "002000010060        \0";
        private const string KeepAliveSend = "002099990010        \0";
        private const string UnSubscribeSend = "002900090010        120100100\0";
        private const string SubscribeSend = "006000080010        1201001310000000000000000000000000000001\0";
        private const string EndCommunicateSend = "002000030010        \0";
        private const string TighteningResult = "006000080010        1201001310000000000000000000000000000001 \0";
        public TypeCollection1 ScrewData;

        /// <summary>
        /// 扭力集合
        /// </summary>
        public List<double> realTorques = new List<double>();
        /// <summary>
        /// 角度集合
        /// </summary>
        public List<double> realAngle = new List<double>();
        /// <summary>
        /// 打螺丝时间
        /// </summary>
        public List<double> screwTime = new List<double>();
        /// <summary>
        /// 单个扭力
        /// </summary>
        public string Torque;
        /// <summary>
        /// 单个角度
        /// </summary>
        public string Angle;
        public struct _Result
        {
            public string TotalResultString;
            public string PeekTorque;
            public string TotalAngle;
            public bool TotalStatus;
        }

        public _Result Atlas_ResultStruct = new _Result();

       /// <summary>
       /// atlas连接
       /// </summary>
       /// <param name="IP"></param>
       /// <param name="Port"></param>
        public void Atlas_Connect(string IP, int Port)
        {
            if (!bConnectStatus)
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(IP);
                try
                {
                    //clientSocket.Connect(new IPEndPoint(ip, Port)); //配置服务器IP与端口 
                    clientSocket.SendTimeout = 200;
                    clientSocket.ReceiveTimeout = 200;
                    clientSocket.BeginConnect(ip, Port, new AsyncCallback(CallBackMethod), clientSocket);
                    if (TimeoutObject.WaitOne(1000, false))
                    {
                        bConnectStatus = true;
                        //MessageBox.Show("网络正常");
                        String A = SendCommand(StartCommunicateSend);
                        time = new System.Timers.Timer(Convert.ToInt32(5000));//实例化Timer类，设置间隔时间为7000毫秒；
                        time.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
                        time.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                        time.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
                        time.Start();
                    }
                    else
                    {
                        bConnectStatus = false;
                        //MessageBox.Show("连接超时");
                    }
                }
                catch
                {
                    bConnectStatus = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            bConnectStatus = false;
            if (time != null)
            {
                time.Stop();
                time.Enabled = false;
                time.AutoReset = false;
            }
            if (clientSocket != null)
            {
                clientSocket.Disconnect(true);
            }
        }

        private void CallBackMethod(IAsyncResult asyncresult)
        {
            //使阻塞的线程继续        
            TimeoutObject.Set();
        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            // time.Stop();
            SendCommand(KeepAliveSend, 50);
            // time.Start();
        }


        public string SendCommand(string commandStr, int timeout = 0)
        {
            lock (read)
            {

                Stopwatch timer = new Stopwatch();
                timer.Reset();

                try
                {
                    sReadData = "";
                    if (!bConnectStatus) // 未連線則先連線
                    {
                        return "";


                    }
                    if (bConnectStatus && clientSocket != null)
                    {
                        string lon = commandStr;   // CR is terminator
                        Byte[] command = ASCIIEncoding.ASCII.GetBytes(lon);
                        clientSocket.Send(command);


                    }
                    Thread.Sleep((int)timeout);
                    ReceiveData();
                    sReadData.Trim();
                    return sReadData;
                }
                catch (Exception ex)
                {
                    bConnectStatus = false;

                    return "ERROR";
                }
            }
        }


        public void ReceiveData()
        {
            try
            {

                byte[] result = new byte[1024];
                int receiveLength = clientSocket.Receive(result);
                sReadData = sReadData + Encoding.ASCII.GetString(result, 0, receiveLength).Trim();
                // MessageBox.Show(sReadData);
            }
            catch (SocketException ex)
            {

            }

        }

        public int MID0006_Tightening_Program_Upload()
        {
            string SpeedString = "0";
            try
            {

                string MID0006 = "003600060010        2501001070001301 \0";

                string returnstring;
                returnstring = SendCommand(MID0006, 500);
                returnstring = returnstring + SendCommand(MID0006, 500);
                SpeedString = returnstring.Substring(returnstring.LastIndexOf("30100001020000002230104001020000002030101004021010002") + 53, 4);


            }
            catch (SocketException ex)
            {

            }
            return Convert.ToInt32(SpeedString);
        }


        public void MID0006_Tightening_Program_Download(int Speed)
        {
            try
            {

                string MID0006 = "003600060010        2501001070001301 \0";

                string returnstring = "";
                string returnstring2 = "";
                string returnstring3 = "";
                int index = 0;
                bool flag = true;

                returnstring = SendCommand(MID0006, 500);
                returnstring = returnstring + SendCommand(MID0006, 500);

                index = returnstring.IndexOf("30108001060000001030101004021010001", index + 20);
                flag = index > 10;
                if (flag)
                {
                    returnstring2 = returnstring.Substring(index + 35, 4);
                    returnstring = returnstring.Replace("30108001060000001030101004021010001" + returnstring2, "30108001060000001030101004021010001" + String.Format("{0:D4}", Speed));
                }



                index = returnstring.IndexOf("30104001020000002030101004021010002", index + 20);
                flag = index > 10;
                if (flag)
                {
                    returnstring2 = returnstring.Substring(index + 35, 4);
                    returnstring = returnstring.Replace("30104001020000002030101004021010002" + returnstring2, "30104001020000002030101004021010002" + String.Format("{0:D4}", Speed));
                }


                returnstring = returnstring.Replace("18702501", "18702500");
                returnstring = returnstring.Replace("\0", " \0");
                string returnstring4 = SendCommand(returnstring, 500);

            }
            catch (SocketException ex)
            {

            }

        }
        
        public _Result Atlas_ReadTighteningResult(int timeout = 0)
        {
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Stop();
                timer.Reset();

                Atlas_ResultStruct.PeekTorque = "0";
                Atlas_ResultStruct.TotalAngle = "0";
                String returnstring = "";
                String Torquestring;
                Decimal dData;
                while (!returnstring.Contains("30202") || !returnstring.Contains("30230") || !returnstring.Contains("30231")
                    || (returnstring.IndexOf("30202") + 17 + 12) >= returnstring.Length
                    || (returnstring.IndexOf("30230") + 17 + 12) >= returnstring.Length
                    || (returnstring.IndexOf("30231") + 17 + 12) >= returnstring.Length)
                {

                    SendCommand(UnSubscribeSend, 20);
                    SendCommand(UnSubscribeSend, 20);
                    SendCommand(UnSubscribeSend, 20);
                    returnstring = SendCommand(SubscribeSend, timeout);

                    if (timer.ElapsedMilliseconds > 5000)
                    {
                        Atlas_ResultStruct.PeekTorque = "";
                        Atlas_ResultStruct.TotalAngle = "";
                        Atlas_ResultStruct.TotalStatus = false;
                        return Atlas_ResultStruct;

                    }

                }
                Atlas_ResultStruct.TotalResultString = returnstring;
                int q = 0;
                ////////////////////////////////////////////////////////////////////////////status////////////////////////////////////////////
                //q = returnstring.IndexOf("30202");

                //if (q < 70)
                //{

                //    Torquestring = returnstring.Substring(q, returnstring.Length - q - 2);
                //    q = Torquestring.IndexOf("30202");
                //}

                //if (q != -1 && (q + 17) < returnstring.Length)
                //{
                //    Torquestring = returnstring.Substring(q + 17, 1);
                //    dData = Convert.ToDecimal(Decimal.Parse(Torquestring.ToString(), System.Globalization.NumberStyles.Float));
                //    Torquestring = (dData).ToString();
                //}
                //else
                //{

                //    Torquestring = "";
                //    dData = 0;
                //}

                //Atlas_ResultStruct.TotalStatus = dData == 1 ? true : false;
                Atlas_ResultStruct.TotalStatus = 1 == 1 ? true : false;
                ////////////////////////////////////////////////////////////////////////////Torque////////////////////////////////////////////
                q = returnstring.IndexOf("30230");
                if (q != -1 && (q + 17) < returnstring.Length)
                {
                    Torquestring = returnstring.Substring(q + 17, 12);
                    dData = Convert.ToDecimal(Decimal.Parse(Torquestring.ToString(), System.Globalization.NumberStyles.Float));
                    Torquestring = (dData / 1000).ToString();
                }
                else
                {
                    Torquestring = "";
                    dData = 0;
                }
                Atlas_ResultStruct.PeekTorque = Torquestring.ToString();
                double _dataT = Convert.ToDouble(Torquestring);
                ////////////////////////////////////////////////////////////////////////////Angle////////////////////////////////////////////
                q = returnstring.IndexOf("30231");
                if (q != -1 && (q + 17) < returnstring.Length)
                {
                    Torquestring = returnstring.Substring(q + 17, 12);
                    dData = Convert.ToDecimal(Decimal.Parse(Torquestring.ToString(), System.Globalization.NumberStyles.Float));
                    Torquestring = (dData).ToString();
                }
                else
                {
                    Torquestring = "";
                    dData = 0;
                }
                Atlas_ResultStruct.TotalAngle = Torquestring.ToString();
                //  MessageBox.Show(returnstring);


                double _dataa = Convert.ToDouble(Torquestring);

                return Atlas_ResultStruct;
            }
            catch
            {

                Atlas_ResultStruct.PeekTorque = "99999";
                Atlas_ResultStruct.TotalAngle = "99999";
                return Atlas_ResultStruct;


            }
        }
        
        /// <summary>
        /// 储存数据
        /// </summary>
        /// <param name="SavePath">数据路径</param>
        /// <param name="IsOk">OK/NG</param>
        /// <param name="sn">条码</param>
        public void StoreDataAndOutput(string SavePath, bool IsOk, string sn)
        {
            double t = Convert.ToDouble(atlas.CurveTorques.Max());
            Torque = (t / 100).ToString("F3");
            Angle = Convert.ToDouble(atlas.CurveAngles.Max()).ToString("F1");
            string[] sData = new string[4];
            sData[0] = sn;
            for (int i = 0; i < atlas.CurveTorques.Count; i++)
            {
                double tt = atlas.CurveTorques[i];
                sData[1] += (tt / 100).ToString("F3") + " ";
            }
            for (int i = 0; i < atlas.CurveAngles.Count; i++)
            {
                sData[2] += atlas.CurveAngles[i].ToString("F1") + " ";
            }
            realAngle = atlas.CurveAngles;
            sData[3] = IsOk == true ? "OK" : "NG";
            ScrewData.SavePath = SavePath;//设置路径
            ScrewData.SaveDataToFile(sData);//保存文件
            double Time = 0;
            DateTime timeBase = new DateTime(2020, 1, 1, 0, 0, 0);
            double timeCoefficientOA = timeBase.AddSeconds(0.003).ToOADate() - timeBase.ToOADate();
            for (int i = 0; i < atlas.CurveTorques.Count + 1; i++)
            {
                Time += timeCoefficientOA;
                screwTime.Add(Time);
            }
            realTorques.Clear();
            for (int i = 0; i < atlas.CurveTorques.Count; i++)
            {
                string s = atlas.CurveTorques[i].ToString("F3");
                double d = double.Parse(s) / 100;
                realTorques.Add(d);
            }
        }
    }
}
