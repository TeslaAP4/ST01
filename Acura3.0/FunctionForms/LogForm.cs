using AcuraLibrary.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    public partial class LogForm : Form
    {
        //add log caption
        private string[] Caption = new string[]
        {
            "Error Type,Error Code,Error Message", //AlarmLog
            "Module, Flow/User, Operation Information", //OperationLog //load+serial unload+serial
            "Product Identity,Start Work Time,End Work Time,Result,Data", //ProductionLog
            "Machine Status Event",
            "Module, Event Information",
            "Module, Start Time, End Time, Duration, Flow Name"

        };
        public enum MachineStatusType
        {
            AcuraExecution,
            AcuraShutdown,
            MachineIdle,
            MachineInitialization,
            MachineRun,
            MachineDown,
            MachineStraved,
            MachineUnstraved,
            MachineBlock,
            MachineUnblock,
        }

        public enum LogType
        {
            Alarm = 0,
            Operation,
            Production,
            MachineStatusEvent,
            EventFlow,
            FlowChart
        };

        private struct LogDataType
        {
            public LogType mType;
            public string sMsg;
            public bool bSaveToFile;
        }
        private string sSaveLogFilePath = System.IO.Directory.GetCurrentDirectory() + "\\Log";
        private List<LogDataType> MsgList = new List<LogDataType>();
        public ListView[] lvArray;
        private BackgroundWorker bgWorker = new BackgroundWorker();
        private bool bgWork = true;
        public LogForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            #region add TabPage
            lvArray = new ListView[Enum.GetNames(typeof(LogType)).Count()];
            TabPage LogSearchPage = tabLogForm;
            tabLogPage.TabPages.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(LogType)).Count(); i++)
            {
                ListView _lv = new ListView();
                lvArray[i] = _lv;
                _lv.Dock = DockStyle.Fill;
                _lv.BackColor = SystemColors.Window;
                _lv.GridLines = true;
                _lv.FullRowSelect = true;
                _lv.View = View.Details;
                _lv.Scrollable = true;

                SetDoubleBuffer(_lv);
                string[] SplitCaption = Caption[i].Split(',');
                for (int j = 0; j < SplitCaption.Length + 2; j++)
                {
                    if (j == 0)
                        _lv.Columns.Add("Date", 100, HorizontalAlignment.Left);
                    else if (j == 1)
                        _lv.Columns.Add("Time", 100, HorizontalAlignment.Left);
                    else
                        _lv.Columns.Add(SplitCaption[j - 2], 100, HorizontalAlignment.Left);

                }
                _lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                TabPage Tp = new TabPage((Enum.GetNames(typeof(LogType)))[i].ToString());
                Tp.Controls.Add(_lv);
                tabLogPage.Controls.Add(Tp);
            }
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            #endregion

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += BgWorker_DoWork;
            if (!bgWorker.IsBusy)
            {
                bgWork = true;
                bgWorker.RunWorkerAsync();
            }
            //Zax 7/15
            TCPIPCtrl.AddEventLog += AddEventLogRaise;
            CoreEngine.FlowChartLeave += CoreEngine_FlowLogRaise;
        }

        private void CoreEngine_FlowLogRaise(object sender, FlowLogArgs e)
        {
            AddLog(LogType.FlowChart, e.ToString());
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bgWork == true)
            {
                try
                {
                    if (MsgList.Count != 0)
                    {
                        LogDataType mLog = MsgList.First();
                        MsgList.RemoveAt(0);
                        DgvAddLogMsg(lvArray[(int)mLog.mType], mLog.sMsg);
                        if (mLog.bSaveToFile)
                            SaveLogToFile(mLog.mType, mLog.sMsg);
                    }
                }
                catch (Exception) { }
                Thread.Sleep(5);
            }
        }

        public void DisposeLog()
        {
            bgWork = false;
            bgWorker.CancelAsync();
        }
        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }
        private void AddEventLogRaise(object sender, EventArgs e)
        {
            AddLog(LogType.EventFlow, sender.ToString());
        }

        //DataGridView新增Log訊息
        public delegate void DALM(DataGridView Dgv, string sMsg); //委派宣告
        public void DgvAddLogMsg(ListView lv, string sMsg)
        {
            try
            {
                if(sMsg != null)
                    lv.Invoke(new Action(() =>
                    {
                        lv.BeginUpdate();
                        string[] strArray = sMsg.Split(',');
                        ListViewItem itm;

                        itm = new ListViewItem(strArray);
                        lv.Items.Add(itm);
                        //lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        //若大於1000筆資料,則自動刪除第一筆
                        if (lv.Items.Count > 1000)
                            lv.Items.RemoveAt(0);
                        lv.EndUpdate();
                    }));

            }
            catch { }

        }
        //將Log資料寫入檔案
        private void SaveLogToFile(LogType LogType, string sMsg)
        {
            try
            {
                DateTime DT = DateTime.Now;
                //檔案存檔路徑 >> 程式開啟路徑\Log\Log類別名稱\
                string sFileDirectory = sSaveLogFilePath + "\\" + LogType.ToString() + "\\" + DT.ToString("yyyy") + "\\";
                //檔案存檔名稱 >> 日期+.副檔名
                string sFileName = LogType.ToString() + "_" + DateTime.Now.ToString("MMdd") + ".csv";
                //檔案的位置
                string sFilePath = sFileDirectory + sFileName;
                //判別檔案路徑是否存在,若不存在則自動新增
                if (!Directory.Exists(sFileDirectory))
                    Directory.CreateDirectory(sFileDirectory);
                //判斷檔案是否存在,若不存在則自動新增檔案
                bool bFileExists = File.Exists(sFilePath);
                if (!bFileExists)
                {
                    StreamWriter sw = new StreamWriter(sFilePath, false);
                    sw.Close();
                }
                //若檔案不存在,則根據站別寫入標題欄
                if (!bFileExists)
                    File.AppendAllText(sFilePath, "Date,Time," + Caption[(int)LogType] + Environment.NewLine, Encoding.UTF8);
                File.AppendAllText(sFilePath, sMsg + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception) { }
        }

        public void AddMachineStatusEvent(MachineStatusType MachineStatus)
        {
            AddLog(LogType.MachineStatusEvent, MachineStatus.ToString());
        }
        //別的模組呼叫加入Log訊息
        public void AddLog(LogType LogType, string sMsg)
        {
           bool bSaveToFile = MiddleLayer.SystemF.GetSettingValue("Pset", "EnableDebugMode");
            LogDataType mLog = new LogDataType
            {
                mType = LogType,
                sMsg = string.Format("{0},{1},{2}", DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("hh:mm:ss:fff"), sMsg),
                bSaveToFile = bSaveToFile
            };
            MsgList.Add(mLog);
        }

     

        private void tabLogPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabLogPage.SelectedIndex < DgvArray.Length)
            //{
            //    DgvArray[tabLogPage.SelectedIndex].AutoResizeRows();
            //    DgvArray[tabLogPage.SelectedIndex].AutoResizeColumns();
            //}
        }
        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lvArray[(int)LogType.Alarm].Invoke(new Action(() =>
            {
                lvArray[(int)LogType.Alarm].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.EventFlow].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.FlowChart].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.MachineStatusEvent].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.Operation].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.Production].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }));
            this.Visible = false;
            e.Cancel = true;

        }

        private void LogForm_Shown(object sender, EventArgs e)
        {
            lvArray[(int)LogType.Alarm].Invoke(new Action(() =>
            {
                lvArray[(int)LogType.Alarm].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.EventFlow].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.FlowChart].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.MachineStatusEvent].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.Operation].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvArray[(int)LogType.Production].AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }));
        }
    }
}
