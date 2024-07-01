using AcuraLibrary.Classes;
using JabilSDK;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcuraLibrary.Forms
{
    public partial class TCPIPCtrl : UserControl
    {
        #region Init
        public enum CommProtocol { OneWay = 0, TwoWay }
        private Task ManualTask;
        private int AutoGotoIndex = 0;
        private int prev_AutoGotoIndex = 0;
        private int _retry = 1;
        public CancellationTokenSource StopManualTask = new CancellationTokenSource();
        private SocketClient _Client = new SocketClient();
        private Task _Task = null;
        public static event EventHandler AddEventLog;
        private Stopwatch _stopwatch = new Stopwatch();

        public static void NotifyEventLogRaise(object sender)
        {
            AddEventLog?.Invoke(sender, null);
        }

        [Browsable(true)]
        [Category("#SASDK")]
        [Description("")]
        public string ControllerName
        {
            get
            {
                return gbController.Text;
            }
            set
            {
                gbController.Text = value;
            }
        }

        [Browsable(true)]
        [Category("#SASDK")]
        [Description("Alarm Code if failed to connect")]
        public int FailedConnectAlarm
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("#SASDK")]
        [Description("Alarm Code if failed to retrieve data")]
        public int FailedRetrieveAlarm
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("#SASDK")]
        [Description("Module Name that display at Event Log")]
        public string ModuleName
        {
            get;
            set;
        }

        public TCPIPCtrl()
        {
            InitializeComponent();
            SetDoubleBuffer(this);
        }
        #endregion

        #region Functions
        public void LoadSetting(DataSet ds)
        {
            DataRow _NewRow = SettingData.Tables[0].NewRow();
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.TableName == "TCPIP_Ctrl_" + this.Name)
                {
                    if (SettingData.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= 6; i++)
                            SettingData.Tables[0].Rows[0][i] = dt.Rows[0][i];
                    }
                    else
                    {
                        SettingData.Tables[0].Rows.Add(_NewRow);
                        SettingData.Tables[0].Rows[0][0] = "127.0.0.1";
                        SettingData.Tables[0].Rows[0][1] = 80;
                        SettingData.Tables[0].Rows[0][2] = 0;
                        SettingData.Tables[0].Rows[0][3] = 1;
                        SettingData.Tables[0].Rows[0][4] = false;
                        SettingData.Tables[0].Rows[0][5] = false;
                        SettingData.Tables[0].Rows[0][6] = true;
                    }
                    SettingData.AcceptChanges();

                    txtIP.Text = GetValue("IPAddress");
                    txtPort.Text = GetValue("Port").ToString();
                    txtTimeout.Text = GetValue("Timeout").ToString();
                    cmbRetry.Text = GetValue("Retry").ToString();
                    chkBxDebugOnly.Checked = GetValue("DebugLogAllow");
                    chkBxEnable.Checked = GetValue("Enable");
                    if (GetValue("CommProtocol"))
                    {
                        rbProtocol_1.Checked = false;
                        rbProtocol_2.Checked = true;
                    }
                    else
                    {
                        rbProtocol_1.Checked = true;
                        rbProtocol_2.Checked = false;
                    }
                    return;
                }
            }

            SettingData.Tables[0].Rows.Add(_NewRow);
            if (SettingData.Tables[0].Rows.Count > 0)
            {
                SettingData.Tables[0].Rows[0][0] = "127.0.0.1";
                SettingData.Tables[0].Rows[0][1] = 80;
                SettingData.Tables[0].Rows[0][2] = 0;
                SettingData.Tables[0].Rows[0][3] = 1;
                SettingData.Tables[0].Rows[0][4] = false;
                SettingData.Tables[0].Rows[0][5] = false;
                SettingData.Tables[0].Rows[0][6] = true;
            }
            SettingData.AcceptChanges();
        }

        public DataSet GetSetting(string ModuleName)
        {
            SettingData.Tables[0].Rows.Remove(SettingData.Tables[0].Rows[0]);
            SettingData.Tables[0].Rows.Add(SettingData.Tables[0].NewRow());
            if (SettingData.Tables[0].Rows.Count > 0)
            {
                SettingData.Tables[0].Rows[0][0] = txtIP.Text;
                SettingData.Tables[0].Rows[0][1] = string.IsNullOrWhiteSpace(txtPort.Text) ? 0 : int.Parse(txtPort.Text);
                SettingData.Tables[0].Rows[0][2] = string.IsNullOrWhiteSpace(txtTimeout.Text) ? 0 : int.Parse(txtTimeout.Text);
                SettingData.Tables[0].Rows[0][3] = string.IsNullOrWhiteSpace(cmbRetry.Text) ? 1 : int.Parse(cmbRetry.Text); 
                SettingData.Tables[0].Rows[0][4] = rbProtocol_2.Checked;
                SettingData.Tables[0].Rows[0][5] = chkBxDebugOnly.Checked;
                SettingData.Tables[0].Rows[0][6] = chkBxEnable.Checked;
            }
            
            SettingData.Namespace = "TCPIP_Ctrl_" + this.Name;
            SettingData.Tables[0].TableName = "TCPIP_Ctrl_" + this.Name;
            return SettingData;
        }

        public DataSet SetSettingTableName()
        {
            SettingData.Namespace = this.Name;
            SettingData.Tables[0].TableName = "TCPIP_Ctrl_MSet";
            return SettingData;
        }

        private dynamic GetValue(string ColumnName)
        {
            dynamic value = null;
            DataTable dt = SettingData.Tables["TCPIP_Ctrl_MSet"];

            if (dt.Columns.IndexOf(ColumnName) >= 0)
            {
                try
                {
                    value = dt.Rows[0][ColumnName];
                    if (value.ToString() == "")
                    {
                        value = GetDefaultValue(dt.Columns[ColumnName].DataType);
                    }
                }
                catch (Exception)
                {
                    value = GetDefaultValue(dt.Columns[ColumnName].DataType);
                }
            }
            else
                MessageBox.Show("Setting data was not found the column, ColumnName=\"" + ColumnName + "\"");
            
            return value;
        }

        private dynamic GetDefaultValue(Type tp)
        {
            dynamic dmc = null;
            if (tp == typeof(Boolean))
                dmc = false;
            else if (tp == typeof(Int16))
                dmc = 0;
            else if (tp == typeof(Int32))
                dmc = 0;
            else if (tp == typeof(Int64))
                dmc = 0;
            else if (tp == typeof(double))
                dmc = 0;
            else if (tp == typeof(byte))
                dmc = 0;
            else if (tp == typeof(string))
                dmc = "null";
            return dmc;
        }

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        private void ExecuteManual(Action ation)
        {
            if (ManualTask != null)
                if (ManualTask.Status == TaskStatus.Running)
                    return;
            StopManualTask.Dispose();
            StopManualTask = new CancellationTokenSource();
            ManualTask = Task.Factory.StartNew(ation, StopManualTask.Token);
        }

        #endregion

        #region Client Function
        public bool ManualTrigger(out string val, bool isConnectOnly = false, string msg = "")
        {
            int ManualGotoIndex = 0;
            string _val = null;
            val = _val;
            string _ip = GetValue("IPAddress");
            int _port = GetValue("Port");
            int _timeout = GetValue("Timeout");
            int _retryCount = GetValue("Retry");
            CommProtocol _protocol = GetValue("CommProtocol") ? CommProtocol.TwoWay : CommProtocol.OneWay;
            bool _isDebugLog = GetValue("DebugLogAllow");
            msg = Regex.Unescape(msg); //Unescape special character like "\"
            try
            {
                while (true)
                {
                    switch (ManualGotoIndex)
                    {
                        case 0:
                            IsError = false;
                            _retry = 1;
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} manual flow started.");
                            if (_Client.IsConnected)
                                _Client.Disconnect();
                            ManualGotoIndex++;
                            break;
                        case 1:
                            if (!_Client.IsConnected)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} connecting...");
                                _Client.Connect(_ip, _port, _timeout);
                            }
                            if (_Client.IsConnected)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} connectivity: " + (_Client.IsConnected ? "Connected" : "Disconnected") + ".");

                                if (!isConnectOnly)
                                    ManualGotoIndex++;
                                else 
                                    return _Client.IsConnected;  
                            }
                            else if (_retry <= _retryCount) {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} connectivity retry: {_retry.ToString()}");
                                _retry++;
                            }
                            else
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} failed to connect host.");
                                IsError = true;
                                JSDK.Alarm.Show(FailedConnectAlarm.ToString());
                                return false;
                            }
                            break;
                        case 2:
                            if (!_Client.IsConnected)
                            {
                                ManualGotoIndex = 1;
                            }
                            else
                            {
                                ManualGotoIndex++;
                            }
                            break;
                        case 3:
                            _Client.Send(msg);
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} trigger message to host. Message: {msg}");
                            ManualGotoIndex++;
                            break;
                        case 4:
                            if (_protocol == CommProtocol.OneWay)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} communication protocol: One Way. Manual flow completed.");
                                return true;
                            }

                            if (_Client.Receive(out _val, _timeout))
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} communication protocol: Two Way. Awaiting message from host.");
                                ManualGotoIndex++;
                            }
                            else if (_retry <= _retryCount)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} receiver retry: {_retry.ToString()}");
                                _retry++;
                            }
                            else
                            {
                                val = "Failed to receive data";
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} {val}. Manual flow completed.");
                                IsError = true;
                                JSDK.Alarm.Show(FailedRetrieveAlarm.ToString());
                                return false;
                            }
                            break;
                        case 5:
                            val = _val;
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} received message: {val}. Manual flow completed.");
                            return true;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                if (_isDebugLog)
                    NotifyEventLogRaise($"{ModuleName},{ControllerName} error: {e.Message}. Manual flow is incomplete.");
                IsError = true;
                JSDK.Alarm.Show(FailedConnectAlarm.ToString());
                return false;
            }
        }

        private bool AutoTrigger(out string val, bool isConnectOnly = false, CommProtocol _protocol = CommProtocol.TwoWay, string msg = "")
        {
            string _val = null;
            val = _val;
            string _ip = GetValue("IPAddress");
            int _port = GetValue("Port");
            int _timeout = GetValue("Timeout");
            int _retryCount = GetValue("Retry");
            bool _isDebugLog = GetValue("DebugLogAllow");
            msg = Regex.Unescape(msg); //Unescape special character like "\"
            try
            {
                switch (AutoGotoIndex)
                {
                    case 0:
                        _retry = 1;
                        IsError = false;
                        if (_Task != null)
                            _Task.Dispose();
                        _Task = null;
                        _stopwatch.Restart();

                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} auto flow started.");

                        if (_Client.IsConnected)
                            _Client.Disconnect();
                        AutoGotoIndex = 1;
                        break;
                    case 1:
                        if (!_Client.IsConnected)
                        {
                            if (_Task == null)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} connecting...");
                                _Task = Task.Run(() => _Client.AsyncConnect(_ip, _port, _timeout));
                            }  
                        }

                        if (_stopwatch.ElapsedMilliseconds > 1000 && _Task.IsCompleted || _Task.IsFaulted)
                            AutoGotoIndex = 2;
                        break;
                    case 2:
                        if (_Client.IsConnected)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} connectivity: connected.");

                            if (isConnectOnly)
                                AutoGotoIndex = 10;
                            else
                                AutoGotoIndex = 3;
                        }
                        else if (_retry <= _retryCount)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} connectivity retry: {_retry.ToString() }");
                            _retry++;
                            AutoGotoIndex = 1;
                        }
                        else
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} failed to connect host.");
                            IsError = true;
                            AutoGotoIndex = 10;
                            JSDK.Alarm.Show(FailedConnectAlarm.ToString());
                        }
                        break;
                    case 3:
                        _Task = Task.Run(() => _Client.AsyncSend(msg));
                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} trigger message to host. Message: {msg}");
                        AutoGotoIndex = 4;
                        _stopwatch.Restart();
                        break;
                    case 4:
                        if (_stopwatch.ElapsedMilliseconds > 200)
                        {
                            if (_Task.Status != TaskStatus.Running && _Client.IsError)
                            {
                                throw new Exception();
                            }
                            else if (_Task.IsCompleted && !_Client.IsError)
                            {
                                if (_protocol == CommProtocol.OneWay)
                                {
                                    if (_isDebugLog)
                                        NotifyEventLogRaise($"{ModuleName},{ControllerName} communication protocol: One Way. Auto flow completed.");
                                    AutoGotoIndex = 10;
                                }
                                else
                                    AutoGotoIndex = 5;
                            }
                        }   
                        break;
                    case 5:
                        _Task = Task.Run(() => _Client.AsyncReceive(_timeout));
                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} communication protocol: Two Way. Awaiting message from host.");
                        AutoGotoIndex = 6;
                        _stopwatch.Restart();
                        break;
                    case 6:
                        if (_stopwatch.ElapsedMilliseconds > 200)
                        {
                            if (_Task.IsCompleted && _Client.IsReceivedReady)
                                AutoGotoIndex++;
                            else if (_Task.Status != TaskStatus.Running && _Client.IsError && _retry <= _retryCount)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} receiver retry: {_retry.ToString()}");
                                AutoGotoIndex = 5;
                                _retry++;
                            }
                            else if (_Task.Status != TaskStatus.Running)
                            {
                                AutoGotoIndex = 10;
                                val = "Failed to receive data";
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} - {val}. Auto flow completed.");
                                IsError = true;
                                JSDK.Alarm.Show(FailedRetrieveAlarm.ToString());
                            }
                        }  
                        break;
                    case 7:
                        val = _Client.output;
                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} received message: {_Client.output}. Auto flow completed.");
                        AutoGotoIndex = 10;
                        break;
                    case 10:
                        _stopwatch.Stop();
                        val = _Client.output;
                        if(_Task != null)
                            _Task.Dispose();
                        _Task = null;
                        AutoGotoIndex = 0;
                        return !IsError;
                }   
            }
            catch (Exception e)
            {
                _stopwatch.Stop();
                if (_Task != null)
                    _Task.Dispose();
                _Task = null;
                AutoGotoIndex = 0;
                if (_isDebugLog)
                    NotifyEventLogRaise($"{ModuleName},{ControllerName} error: {e.Message}. Auto flow is incomplete.");
                IsError = true;
                JSDK.Alarm.Show(FailedConnectAlarm.ToString());
            }

            if (IsError)
            {
                _stopwatch.Stop();
                if (_Task != null)
                    _Task.Dispose();
                _Task = null;
                AutoGotoIndex = 0;
            }
            return false;
        }

        public bool Connect()
        {
            return AutoTrigger(out string val, true);
        }

        public void Disconnect()
        {
            _Client.Disconnect();
        }

        /// <summary>One way communication protocol will be used in this Send method. E.g. for connection purpose.</summary>
        public bool Send(string msg)
        {
            bool isSuccess = AutoTrigger(out string _val, false, CommProtocol.OneWay, msg);
            return isSuccess;
        }

        /// <summary>Send method will be triggered based on setting data on Communication Protocol.</summary>
        public bool Send(out string val, string msg)
        {
            CommProtocol protocol = GetValue("CommProtocol") ? CommProtocol.TwoWay : CommProtocol.OneWay;
            bool isSuccess = AutoTrigger(out string _val, false, protocol, msg);
            val = _val;
            return isSuccess;
        }

        /// <summary>Alternative method to set programmatically instead of using setting data.</summary>
        public bool Send(out string val, string msg, CommProtocol protocol)
        {
            bool isSuccess = AutoTrigger(out string _val, false, protocol, msg);
            val = _val;
            return isSuccess;
        }

        public bool IsConnected
        {
            get { return _Client.IsConnected; }
            set { }
        }

        public bool IsError { get; set; }

        public bool IsEnable {
            get
            {
                return GetValue("Enable") ;
            }
            set { }
        }

        //Updated 18052022
        public bool AutoTriggerNew(out string val, bool isConnectOnly = false, bool isReconnect = false, CommProtocol _protocol = CommProtocol.TwoWay, string msg = "")
        {
            string _val = null;
            val = _val;
            string _ip = GetValue("IPAddress");
            int _port = GetValue("Port");
            int _timeout = GetValue("Timeout");
            int _retryCount = GetValue("Retry");
            bool _isDebugLog = GetValue("DebugLogAllow");
            msg = Regex.Unescape(msg); //Unescape special character like "\"
            
            try
            {
                switch (AutoGotoIndex)
                {
                    case 0: //Start
                        _retry = 0;
                        IsError = false;
                        if (_Task != null && (_Task.IsCompleted || _Task.IsFaulted))
                            _Task.Dispose();
                        _Task = null;                        

                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} Auto flow started.");

                        if (_Client.IsConnected)
                        {
                            if (!_Client.SocketConnectedVerification() || isReconnect) //SocketConnectedVerification = true if connection is still active
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Going disconnect");
                                AutoGotoIndex = 1;
                            }
                            else
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Connected");
                                AutoGotoIndex = 20;
                            }
                        }
                        else
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} Going connect");
                            AutoGotoIndex = 10;
                        }
                        _stopwatch.Restart();
                        break;

                    case 1: //Start disconnect
                        if (_Task == null)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} Disconnecting...");
                            _Task = Task.Run(() => _Client.DisconnectNew());
                            AutoGotoIndex = 2;
                            _stopwatch.Restart();
                        }
                        break;

                    case 2: //Poll disconnect completion
                        if (_Task.IsCompleted || _Task.IsFaulted)
                        {
                            _Task = null;

                            if (_Client.IsError_Disconnect)
                            {
                                prev_AutoGotoIndex = AutoGotoIndex;
                                AutoGotoIndex = 99;
                            }
                            else
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Disconnected");
                                AutoGotoIndex = 3;
                            }
                            _stopwatch.Restart();
                        }
                        break;

                    case 3: //Delay before connect
                        if(_stopwatch.ElapsedMilliseconds > (_retry * 40))
                        {
                            AutoGotoIndex = 10;
                            _stopwatch.Restart();
                        }
                        break;

                    case 10: //Start connect
                        if (_Task == null)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} Connecting...");
                            _Task = Task.Run(() => _Client.SyncConnectNew(_ip, _port, _timeout));
                            AutoGotoIndex = 11;
                            _stopwatch.Restart();
                        }
                        break;

                    case 11: //Poll connect completion
                        if (_Task.IsCompleted || _Task.IsFaulted)
                        {
                            _Task = null;

                            if (_Client.IsError_Connect)
                            {
                                prev_AutoGotoIndex = AutoGotoIndex;
                                AutoGotoIndex = 99;
                            }
                            else
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Connected");
                                if (isConnectOnly)
                                    AutoGotoIndex = 100;
                                else
                                    AutoGotoIndex = 12;
                            }
                            _stopwatch.Restart();
                        }
                        break;

                    case 12: //Delay before send
                        if (_stopwatch.ElapsedMilliseconds > (_retry * 30))
                        {
                            AutoGotoIndex = 20;
                            _stopwatch.Restart();
                        }
                        break;

                    case 20: //Start send
                        if (_Task == null)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} Send message: {msg}");
                            _Task = Task.Run(() => _Client.SyncSendNew(msg, _timeout));
                            AutoGotoIndex = 21;
                            _stopwatch.Restart();
                        }
                        break;

                    case 21: //Poll send completion
                        if (_Task.IsCompleted || _Task.IsFaulted)
                        {
                            _Task = null;

                            if (_Client.IsError_Send)
                            {
                                prev_AutoGotoIndex = AutoGotoIndex;
                                AutoGotoIndex = 99;
                            }
                            else
                            {
                                if (_protocol == CommProtocol.OneWay)
                                {
                                    if (_isDebugLog)
                                        NotifyEventLogRaise($"{ModuleName},{ControllerName} Communication protocol: One Way. Auto flow completed.");
                                    AutoGotoIndex = 100;
                                }
                                else
                                    AutoGotoIndex = 30;
                            }
                            _stopwatch.Restart();
                        }
                        break;

                    case 30: //Start receive
                        if (_Task == null)
                        {
                            if (_isDebugLog)
                                NotifyEventLogRaise($"{ModuleName},{ControllerName} Communication protocol: Two Way. Awaiting message.");
                            _Task = Task.Run(() => _Client.SyncReceiveNew(_timeout));
                            AutoGotoIndex = 31;
                            _stopwatch.Restart();
                        }
                        break;

                    case 31: //Poll receive completion
                        if (_Task.IsCompleted || _Task.IsFaulted)
                        {
                            _Task = null;

                            if (_Client.IsError_Receive)
                            {                                
                                prev_AutoGotoIndex = AutoGotoIndex;
                                AutoGotoIndex = 99;
                            }
                            else if (_Client.IsReceivedReady)
                            {
                                if (_isDebugLog)
                                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Received message: {_Client.output}.");
                                AutoGotoIndex = 100;
                            }
                            _stopwatch.Restart();
                        }                   
                        break;

                    case 99: //Retry 
                        _retry++;
                        _stopwatch.Reset();                       
                        if (_Task != null && (_Task.IsCompleted || _Task.IsFaulted))
                            _Task.Dispose();
                        _Task = null;

                        if (_isDebugLog)
                        {
                            string ErrorMessage = "";
                            switch(prev_AutoGotoIndex)
                            {
                                case 2:
                                    ErrorMessage = _Client.ErrorMessage_Disconnect;
                                    break;
                                case 11:
                                    ErrorMessage = _Client.ErrorMessage_Connect;
                                    break;
                                case 21:
                                    ErrorMessage = _Client.ErrorMessage_Send;
                                    break;
                                case 31:
                                    ErrorMessage = _Client.ErrorMessage_Receive;
                                    break;
                            }

                            NotifyEventLogRaise($"{ModuleName},{ControllerName} Error: {ErrorMessage} - state: {prev_AutoGotoIndex} - retry: {_retry}");
                        }

                        if(_retry > _retryCount)
                        {
                            AutoGotoIndex = 0;
                            IsError = true;
                            JSDK.Alarm.Show(FailedRetrieveAlarm.ToString());
                            return false;
                        }
                        else
                        {                            
                            AutoGotoIndex = 1;
                        }                        
                        break;

                    case 100: //End 
                        _stopwatch.Reset();
                        if (_Task != null && (_Task.IsCompleted || _Task.IsFaulted))
                            _Task.Dispose();
                        _Task = null;
                        AutoGotoIndex = 0;
                        val = _Client.output;

                        if (_isDebugLog)
                            NotifyEventLogRaise($"{ModuleName},{ControllerName} Auto flow completed.");

                        return !IsError;
                }
            }
            catch (Exception e)
            {
                _stopwatch.Reset();
                if (_Task != null && (_Task.IsCompleted || _Task.IsFaulted))
                    _Task.Dispose();
                _Task = null;
                AutoGotoIndex = 0;
                if (_isDebugLog)
                    NotifyEventLogRaise($"{ModuleName},{ControllerName} error: {e.Message}. Auto flow is incomplete.");
                IsError = true;
                JSDK.Alarm.Show(FailedConnectAlarm.ToString());
            }

            //External timeout monitoring, slightly longer timeout to differentiate from socket timeout
            if (_stopwatch.ElapsedMilliseconds > _timeout + 150) 
            {
                if (_isDebugLog)
                    NotifyEventLogRaise($"{ModuleName},{ControllerName} Auto flow timeout - state: {AutoGotoIndex}");
                prev_AutoGotoIndex = AutoGotoIndex;
                AutoGotoIndex = 99;
            }

            return false;
        }

        public void SyncDisconnectNew()
        {
            if(_Client.IsConnected)
                _Client.DisconnectNew();
        }
        #endregion

        #region Button & Event

        private void LblStatus_TextChanged(object sender, EventArgs e)
        {
            int _lines = LblStatus.Lines.Length;
            if (_lines >= 100)
                LblStatus.Text = LblStatus.Text.Remove(0, LblStatus.Lines[0].Length + 1); 
        }

        private void tmLoad_Tick(object sender, EventArgs e)
        {
            if (!_Client.IsConnected)
            {
                gBOutput.BackColor = Color.DarkGray;
                gBOutput.Text = "Disconnected";
                LblStatus.BackColor = Color.DarkGray;
                LblStatus.Text = "Status";
                btnConnect.Text = "Connect";
            }
            else
            {
                gBOutput.BackColor = Color.LimeGreen;
                gBOutput.Text = "Connected";
                LblStatus.BackColor = Color.LimeGreen;
                btnConnect.Text = "Disconnect";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_Client.IsConnected)
            {
                Disconnect();
            }
            else
            {
                //ExecuteManual(() => TriggerClient(true));
                ManualTrigger(out string val, true);
                //Connect();
            }
                
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {
            //ExecuteManual(() => TriggerClient(false, txtMsg.Text));
            ManualTrigger(out string val, false, txtMsg.Text);//Send(out string val, txtMsg.Text);
            //Send(out string val, txtMsg.Text);
            LblStatus.Text = $"{LblStatus.Text}\n{DateTime.Now.ToString("MMM-dd-yyyy")}: {val}";
        }

        private void chkBxEnable_CheckedChanged(object sender, EventArgs e)
        {
            gBMain.Enabled = ((CheckBox)sender).Checked;
            gBOutput.Enabled = ((CheckBox)sender).Checked;
            chkBxEnable.Enabled = true;
        }
        #endregion
    }
}
