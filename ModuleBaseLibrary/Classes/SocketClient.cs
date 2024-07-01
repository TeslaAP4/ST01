using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace AcuraLibrary.Classes
{
    public class SocketClient
    {
        #region init
        public int ReciveMaxLength = 8192;
        private Socket socket = null;

        public string output { get; set; }

        public bool IsConnected
        {
            get
            {
                if (socket != null)
                {
                    return socket.Connected;
                }
                return false;
            }
        }

        public bool IsReceivedBusy { get; set; } = false;

        public bool IsReceivedReady
        {
            get
            {
                return !string.IsNullOrWhiteSpace(output);
            }
        }

        public bool IsError
        {
            get;
            set;
        }

        public bool IsError_Disconnect
        {
            get;
            set;
        }

        public bool IsError_Connect
        {
            get;
            set;
        }

        public bool IsError_Send
        {
            get;
            set;
        }

        public bool IsError_Receive
        {
            get;
            set;
        }

        public void Disconnect()
        {
            try
            {
                socket.Close();
                socket.Dispose();
                socket = null;
            }
            catch { }
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string ErrorMessage_Disconnect
        {
            get;
            set;
        }

        public string ErrorMessage_Connect
        {
            get;
            set;
        }

        public string ErrorMessage_Send
        {
            get;
            set;
        }

        public string ErrorMessage_Receive
        {
            get;
            set;
        }

        #endregion

        #region Manual Triggering
        public bool Connect(string strIP, int port, int timeout, bool PopUpMsg = false)
        {
            try
            {
                if (!IsConnected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = timeout
                    };
                    IAsyncResult connResult = socket.BeginConnect(strIP, port, null, null);
                    connResult.AsyncWaitHandle.WaitOne(1000, true); 
                    if (!connResult.IsCompleted)
                        Disconnect();                            
                    return socket.Connected;
                }
            }
            catch (Exception e)
            {
                if (PopUpMsg)
                    MessageBox.Show($"Exception: {e}");
                else
                    throw e;
            }
            return false;
        }

        public void Send(string cmd, bool PopUpMsg = false)
        {
            try
            {
                if (IsConnected)
                {
                    byte[] command = Encoding.ASCII.GetBytes(cmd);
                    socket.Send(command);
                }
            }
            catch (Exception e)
            {
                if(PopUpMsg)
                    MessageBox.Show($"Exception: {e}");
                else
                    throw e;
            }
        }

        public bool Receive(out string strRecv, int timeout, bool PopUpMsg = false)
        {
            strRecv = "";
            try
            {
                if (IsConnected)
                {
                    byte[] bytes = new byte[ReciveMaxLength];
                    socket.ReceiveTimeout = timeout;
                    int count = socket.Receive(bytes);
					strRecv = Encoding.UTF8.GetString(bytes, 0, count).TrimEnd('\n').TrimEnd('\r');
					return true;
                }
            }
            catch (Exception)
            {
                return false;
                //if (PopUpMsg)
                //    MessageBox.Show($"Exception: {e}");
                //else
                //    throw e;
            }
            return false;
            
        }
        #endregion

        #region Async Triggering
        public void AsyncConnect(string strIP, int port, int timeout, bool PopUpMsg = false)
        {
            try
            {
                IsError = false;
                if (!IsConnected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = timeout
                    };
                    IAsyncResult connResult = socket.BeginConnect(strIP, port, null, null);
                    connResult.AsyncWaitHandle.WaitOne(timeout, true);
                }
            }
            catch (Exception e)
            {
                IsError = true;
            }
        }

        public void AsyncSend(string cmd, bool PopUpMsg = false)
        {
            try
            {
                IsError = false;
                if (IsConnected)
                {
                    byte[] command = Encoding.ASCII.GetBytes(cmd);
                    Task.Run(() => socket.Send(command));
                }
            }
            catch (Exception e)
            {
                IsError = true;
            }
        }

        public void AsyncReceive(int timeout)
        {
            try
            {
                IsError = false;
                if (IsConnected)
                {
                    IsReceivedBusy = true;
                    output = "";
                    byte[] bytes = new byte[ReciveMaxLength];
                    socket.ReceiveTimeout = timeout;
                    int count = socket.Receive(bytes);
                    IsReceivedBusy = false;
                    output = Encoding.UTF8.GetString(bytes, 0, count).TrimEnd('\n').TrimEnd('\r');
                }
            }
            catch (Exception e)
            {
                IsError = true;
            }

        }
        #endregion

        #region New Socket Library
        public bool SocketConnectedVerification()
        {
            bool r1 = socket.Poll(20, SelectMode.SelectRead);
            bool r2 = (socket.Available == 0);
            if (r1 && r2)
                return false;
            else
                return true;
        }

        public void DisconnectNew()
        {
            try
            {
                IsError_Disconnect = false;
                ErrorMessage_Disconnect = "";

                if (socket != null)
                {
                    socket.Close();
                    socket.Dispose();
                    socket = null;
                }
            }
            catch(Exception e)
            {
                IsError_Disconnect = true;
                ErrorMessage_Disconnect = e.Message;
            }
        }

        public void SyncConnectNew(string strIP, int port, int timeout, bool PopUpMsg = false)
        {
            try
            {
                IsError_Connect = false;
                ErrorMessage_Connect = "";

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = timeout,
                    ReceiveTimeout = timeout
                };
                IAsyncResult connResult = socket.BeginConnect(strIP, port, null, null);
                if (!connResult.AsyncWaitHandle.WaitOne(timeout, true)) //blocking while waiting for result
                {
                    IsError_Connect = true;
                    ErrorMessage_Connect = "Connect Timeout";
                }
            }
            catch (Exception e)
            {
                IsError_Connect = true;
                ErrorMessage_Connect = e.Message;
            }
        }

        public void SyncSendNew(string cmd, int timeout = 1000)
        {
            try
            {
                IsError_Send = false;
                ErrorMessage_Send = "";

                byte[] command = Encoding.ASCII.GetBytes(cmd);
                socket.SendTimeout = timeout;
                socket.Send(command);
            }
            catch (Exception e)
            {
                IsError_Send = true;
                ErrorMessage_Send = e.Message;
            }
        }

        public void SyncReceiveNew(int timeout = 1000)
        {
            try
            {
                IsError_Receive = false;
                ErrorMessage_Receive = "";

                IsReceivedBusy = true;
                output = "";
                byte[] bytes = new byte[ReciveMaxLength];
                socket.ReceiveTimeout = timeout;
                int count = socket.Receive(bytes);
                IsReceivedBusy = false;
                output = Encoding.UTF8.GetString(bytes, 0, count).TrimEnd('\n').TrimEnd('\r');
            }
            catch (Exception e)
            {
                IsReceivedBusy = false;
                IsError_Receive = true;
                ErrorMessage_Receive = e.Message;
            }
        }
        #endregion
    }
}