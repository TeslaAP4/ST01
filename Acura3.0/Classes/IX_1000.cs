using Acura3.Classes;
//using Acura3.New.Other;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Acura3.New.Device
{
    public class IX_1000
    {

        public string IP { get; set; }

        public int Port { get; set; }

        public string NewLine { get; set; } = "\r\n";

        public bool Status { get => Client.ConnectStatus(); }

        public TCPCLient Client { get; set; }

        public IX_1000(string iP, int port)
        {
            IP = iP;
            Port = port;
            Client = new TCPCLient();
        }

        public string Trigger()
        {
            if (!Connect()) return null;

            //Client.Receive();

            Client.Send("SW,01,161,+000000000" + NewLine);
            string cmdResult1 = Client.Receive(1000);
            if (string.IsNullOrEmpty(cmdResult1) || cmdResult1.Contains("ER"))
                return null;

            Client.Send("SW,01,161,+000000001" + NewLine);
            string cmdResult = Client.Receive(1000);
            if (string.IsNullOrEmpty(cmdResult) || cmdResult.Contains("ER"))
                return null;

            Thread.Sleep(1200);

            Client.Send("M0" + NewLine);
            string cmdResult2 = Client.Receive(1000);
            if (string.IsNullOrEmpty(cmdResult2) || cmdResult2.Contains("ER"))
                return null;

            return cmdResult2;
        }

        public bool SelectProcess(int number)
        {
            if (!Connect()) return false;

            Client.Receive();

            Client.Send("SW,01,163,+00000000" + number.ToString() + NewLine);
            string cmdResult1 = Client.Receive(1000);
            if (string.IsNullOrEmpty(cmdResult1) || cmdResult1.Contains("ER"))
                return false;

            Thread.Sleep(500);
            Client.Send("SR,01,164" + NewLine);
            string cmdResult = Client.Receive(1000);
            if (string.IsNullOrEmpty(cmdResult) || cmdResult.Contains("ER"))
                return false;

            if (cmdResult.Split(',')[3][cmdResult.Split(',')[3].Length - 3] == '1')
                return true;

            return false;
        }

        public bool Connect()
        {
            if (!Status)
            {
                Client.Disconnect();
                Client.Connect(IP, Port);
            }

            return Status;
        }

        public bool Disconnect()
        {
            if (Status)
            {
                Client.Disconnect();
            }

            return Status;
        }
    }
}

