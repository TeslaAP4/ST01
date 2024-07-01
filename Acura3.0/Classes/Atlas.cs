using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Acura3._0.Classes
{
    public class Atlas
    {
        public struct _Result
        {
            public string TotalResultString;

            public string PeekTorque;

            public string TotalAngle;

            public bool TotalStatus;
        }

        private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        public Socket clientSocket;

        private bool _connectStatus;

        private bool _bCurveTorques;

        private bool _bCurveAngle;

        private bool _bScrewResult;

        private string _IP = "127.0.0.1";

        private int _Port = 5000;

        private byte[] bufferOld = new byte[0];

        private byte[] bufferNew = new byte[0];

        private Stopwatch stopwatch = new Stopwatch();

        private bool bMID005 = false;

        private AutoResetEvent MID5Event = new AutoResetEvent(false);

        private bool bMID002 = false;

        private AutoResetEvent MID2Event = new AutoResetEvent(false);

        public List<double> CurveTorques = new List<double>();

        public List<double> CurveAngles = new List<double>();

        public List<double> CurveTimes = new List<double>();

        private double _Torques;

        private double _Angles;

        private Thread _rxThread = null;

        private readonly object read = new object();

        private const string StartCommunicateSend = "002000010060        \0";

        private const string SubscribeCurveSend = "006700080010        0900001380                             02001002        \0";

        private const string SubscribeResultSend = "006000080010        1201001310000000000000000000000000000001\0";

        private const string GetCurveAngleSend = "004600060010        09000021700000000000010000\0";

        private const string GetCurveTorquesSend = "004600060010        09000021700000000000020000\0";

        private const string GetResultSend = "006000080010        1201001310000000000000000000000000000001\0";

        public _Result Atlas_ResultStruct = default(_Result);

        public bool ConnectStatus
        {
            get
            {
                return _connectStatus;
            }
            set
            {
                _connectStatus = value;
            }
        }

        public bool bCurveTorques
        {
            get
            {
                return _bCurveTorques;
            }
            set
            {
                _bCurveTorques = value;
            }
        }

        public bool bCurveAngle
        {
            get
            {
                return _bCurveAngle;
            }
            set
            {
                _bCurveAngle = value;
            }
        }

        public bool bScrewResult
        {
            get
            {
                return _bScrewResult;
            }
            set
            {
                _bScrewResult = value;
            }
        }

        public string IP
        {
            set
            {
                _IP = value;
            }
        }

        public int Port
        {
            set
            {
                _Port = value;
            }
        }

        public double Torques
        {
            get
            {
                return _Torques;
            }
            set
            {
                _Torques = value;
            }
        }

        public double Angles
        {
            get
            {
                return _Angles;
            }
            set
            {
                _Angles = value;
            }
        }

        public void Atlas_Connect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(_IP);
                clientSocket.SendTimeout = 200;
                clientSocket.ReceiveTimeout = 200;
                clientSocket.BeginConnect(address, _Port, CallBackMethod, clientSocket);
                Thread.Sleep(500);
                if (TimeoutObject.WaitOne(1000, false))
                {
                    StartRxThread();
                    _connectStatus = true;
                    Thread.Sleep(200);
                    if (!StartCommunicateEvent())
                    {
                        Disconnect();
                        _connectStatus = false;
                    }
                    if (!SubscribeEvent("006700080010        0900001380                             02001002        \0"))
                    {
                        Disconnect();
                        _connectStatus = false;
                    }
                    if (!SubscribeEvent("006000080010        1201001310000000000000000000000000000001\0"))
                    {
                        _connectStatus = false;
                        Disconnect();
                    }
                }
                else
                {
                    _connectStatus = false;
                }
            }
            catch
            {
                _connectStatus = false;
            }
        }

        private void StartRxThread()
        {
            if (_rxThread == null)
            {
                _rxThread = new Thread(ReceiveData);
                _rxThread.IsBackground = true;
                _rxThread.Start();
            }
        }

        public void Disconnect()
        {
            _connectStatus = false;
            if (_rxThread != null)
            {
                _rxThread.Join();
                _rxThread = null;
            }
            if (clientSocket != null)
            {
                int available = clientSocket.Available;
                if (available > 0)
                {
                    byte[] buffer = new byte[available];
                    clientSocket.Receive(buffer);
                }
                if (clientSocket.Connected)
                {
                    clientSocket.Disconnect(true);
                }
                clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = null;
                _connectStatus = false;
            }
        }

        private void CallBackMethod(IAsyncResult asyncresult)
        {
            TimeoutObject.Set();
        }

        public bool SendCommand(string commandStr, int timeout = 0)
        {
            try
            {
                if (!_connectStatus)
                {
                    Atlas_Connect();
                }
                if (clientSocket != null && clientSocket.Connected)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(commandStr);
                    clientSocket.Send(bytes);
                }
                return true;
            }
            catch (Exception)
            {
                _connectStatus = false;
                return false;
            }
        }

        public void ReceiveData()
        {
            while (_connectStatus)
            {
                try
                {
                    int available = clientSocket.Available;
                    byte[] array = new byte[available];
                    if (available != 0)
                    {
                        int count = clientSocket.Receive(array);
                        string @string = Encoding.ASCII.GetString(array, 0, count);
                        stopwatch.Stop();
                        stopwatch.Reset();
                        stopwatch.Restart();
                        stopwatch.Start();
                        BuffIsIatact(array, stopwatch);
                        goto end_IL_0007;
                    }
                    if (stopwatch.ElapsedMilliseconds <= 100)
                    {
                        goto IL_0099;
                    }
                    goto IL_0099;
                IL_0099:
                    Thread.Sleep(10);
                    if (bufferOld.Length != 0)
                    {
                        BuffIsIatact(array, stopwatch);
                    }
                    if (!clientSocket.Connected)
                    {
                        Disconnect();
                    }
                end_IL_0007:;
                }
                catch (SocketException)
                {
                }
            }
        }

        private void BuffIsIatact(byte[] buff, Stopwatch sw)
        {
            long elapsedMilliseconds = sw.ElapsedMilliseconds;
            if (sw.ElapsedMilliseconds < 50)
            {
                int num = buff.Length + bufferOld.Length;
                bufferNew = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    if (i < bufferOld.Length && bufferOld.Length != 0)
                    {
                        bufferNew[i] = bufferOld[i];
                    }
                    else
                    {
                        bufferNew[i] = buff[i - bufferOld.Length];
                    }
                }
                bufferOld = new byte[num];
                bufferOld = bufferNew;
            }
            else
            {
                try
                {
                    string @string = Encoding.ASCII.GetString(bufferOld, 0, 10);
                    int num2 = Convert.ToInt32(@string.Substring(0, 4));
                    string string2 = Encoding.ASCII.GetString(bufferOld, num2, 1);
                    if (string2 == "\0")
                    {
                        num2++;
                    }
                    byte[] buffer = bufferOld.Skip(0).Take(num2).ToArray();
                    int num3 = bufferOld.Length - num2;
                    byte[] array = bufferOld.Skip(num2).Take(num3).ToArray();
                    bufferOld = new byte[num3 + 1];
                    bufferOld = array;
                    DataAnalysis(buffer);
                }
                catch (Exception)
                {
                    bufferOld = new byte[0];
                }
            }
        }

        private void DataAnalysis(byte[] buffer)
        {
            int num = HeaderParser(buffer);
            if (num == 900)
            {
                string @string = Encoding.ASCII.GetString(buffer, 0, 151);
                string string2 = Encoding.ASCII.GetString(buffer, 49, 3);
                int num2 = 0;
                int num3 = 0;
                byte[] arbyte;
                if (string2 == "001")
                {
                    num3 = Convert.ToInt32(@string.Substring(81, 2));
                    num2 = Convert.ToInt32(@string.Substring(124, 5));
                    arbyte = buffer.Skip(130).Take(num2 * 2).ToArray();
                }
                else
                {
                    num3 = Convert.ToInt32(@string.Substring(99, 2));
                    num2 = Convert.ToInt32(@string.Substring(146, 5));
                    arbyte = buffer.Skip(152).Take(num2 * 2).ToArray();
                }
                switch (num3)
                {
                    case 1:
                        CurveAngles.Clear();
                        ArrbyteToArrInt(arbyte, 0.54, ref CurveAngles, 2);
                        _bCurveAngle = true;
                        break;
                    case 2:
                        CurveTorques.Clear();
                        ArrbyteToArrInt(arbyte, 0.00125, ref CurveTorques, 2);
                        _bCurveTorques = true;
                        break;
                }
            }
            if (num == 1202)
            {
                string string3 = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                int num4 = string3.IndexOf("30231");
                int num5 = string3.IndexOf("30230");
                try
                {
                    string value = string3.Substring(num4 + 17, 12);
                    _Angles = Convert.ToDouble(value);
                    string value2 = string3.Substring(num5 + 17, 12);
                    _Torques = Convert.ToDouble(value2);
                    if (_Torques > 50.0)
                    {
                        string text = string3.Substring(num5 + 17, string3.Length - num5 - 17);
                        int num6 = text.IndexOf("30230");
                        if (num6 > -1)
                        {
                            string value3 = text.Substring(num6 + 17, 12);
                            _Torques = Convert.ToDouble(value3);
                        }
                    }
                    _bScrewResult = true;
                }
                catch (Exception)
                {
                }
            }
            if (num == 5)
            {
                MID5Event.Set();
                bMID005 = true;
            }
            if (num == 2)
            {
                MID2Event.Set();
                bMID002 = true;
            }
        }

        private void ArrbyteToArrInt(byte[] arbyte, double pid, ref List<double> curevdata, int byteTointCount = 2)
        {
            if (arbyte.Length % byteTointCount != 0 || byteTointCount < 1)
            {
                Console.WriteLine("传入字节数与byteTointCount数量有差别");
            }
            int num = arbyte.Length / byteTointCount;
            for (int i = 0; i < num; i++)
            {
                string text = arbyte[i * 2].ToString("X6");
                text = text.Substring(4, 2);
                string text2 = arbyte[i * 2 + 1].ToString("X6");
                text2 = text2.Substring(4, 2);
                string value = text2 + text;
                curevdata.Add((double)Convert.ToInt16(value, 16) * pid);
            }
        }

        private int HeaderParser(byte[] Data)
        {
            return int.Parse(Encoding.ASCII.GetString(Data, 4, 4));
        }

        public bool SubscribeEvent(string strSend)
        {
            bMID005 = false;
            SendCommand(strSend, 0);
            if (MID5Event.WaitOne(500))
            {
                return bMID005;
            }
            bMID005 = false;
            SendCommand(strSend, 0);
            if (MID5Event.WaitOne(500))
            {
                return bMID005;
            }
            bMID005 = false;
            SendCommand(strSend, 0);
            if (MID5Event.WaitOne(500))
            {
                return bMID005;
            }
            return false;
        }

        public bool StartCommunicateEvent()
        {
            bMID002 = false;
            SendCommand("002000010060        \0", 0);
            if (MID2Event.WaitOne(500))
            {
                return bMID002;
            }
            bMID002 = false;
            SendCommand("002000010060        \0", 0);
            if (MID2Event.WaitOne(500))
            {
                return bMID002;
            }
            bMID002 = false;
            SendCommand("002000010060        \0", 0);
            if (MID2Event.WaitOne(500))
            {
                return bMID002;
            }
            return false;
        }

        public bool GetScrewResult(int num)
        {
            switch (num)
            {
                case 1:
                    return SendCommand("004600060010        09000021700000000000010000\0", 0);
                case 2:
                    return SendCommand("004600060010        09000021700000000000020000\0", 0);
                case 3:
                    return SendCommand("006000080010        1201001310000000000000000000000000000001\0", 0);
                default:
                    return false;
            }
        }

        private bool IsConnected(Socket s, int wait_time)
        {
            try
            {
                bool flag = s.Poll(wait_time, SelectMode.SelectRead);
                bool flag2 = s.Available == 0;
                if (flag & flag2)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
