using Acura3._0.Classes;
using Acura3._0.FunctionForms;
using AcuraLibrary;
using AcuraLibrary.Forms;
//using CFX.Structures.JAG;
using JabilSDK;
using JabilSDK.Controls;
using JabilSDK.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemCommon.Communication;
using static Acura3._0.FunctionForms.LogForm;

namespace Acura3._0.ModuleForms
{
    public partial class ConveyorForm : ModuleBaseForm
    {
        #region data
        public bool MachineAvailable1;  //工位1出料信号
        public bool MachineAvailable2;  //工位2出料信号
        public bool Station1Start;   //工位1开始工作信号
        public bool Station2Start;   //工位2开始工作信号
        public bool Dryrun1;   //工位1空跑有料
        public bool Dryrun2;   //工位2空跑有料
        public bool Stationwork1;  //工位1出料运行信号
        public bool Stationwork1Comp;
        public bool Stationwork2;  //工位2出料运行信号
        public bool Stationwork2Comp;
        public bool UpMachineAvailable_SMEMA;  //按钮模拟要料smema
        public bool DownMachineReady_SMEMA;  //按钮模拟出料smema
        public bool StationMachineIn1;  //工位1入料信号
        public bool StationMachineIn2;  //工位2入料信号
        public bool ConveyorBStation1RobotStart1;  //流道B工位1机器手开始信号
        public bool ConveyorBStation1RobotComp1;  //流道B工位1机器手完成信号
        public bool ConveyorBStation2RobotStart2;  //流道B工位2机器手开始信号
        public bool ConveyorBStation2RobotComp2;  //流道B工位2机器手完成信号
        public bool ConveyorBStart1;
        public bool ConveyorBStart2;
        public bool ConveyorStation1NoEmpty;
        public bool ConveyorStation2NoEmpty;

        public bool Gantry1NGProduct = false;
        public bool Gantry2NGProduct = false;

        public bool B_Gantry1LoadingFlow = false;
        public bool B_Gantry2LoadingFlow = false;

        public bool B_Gantry1Out = false;
        public bool B_Gantry2Out = false;
        public bool B_Cash1In = false;
        public bool B_Cash1Out = false;
        public bool B_3DIn = false;
        public bool B_3DOut = false;
        public bool B_Cash2In = false;
        public bool B_Cash2Out = false;
        public bool B_PnPIn = false;

        public bool ByPass => MiddleLayer.SystemF.GetSettingValue("PSet", "ByPass");
        public bool Dryrun => MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
        public bool DisableRFID => GetSettingValue("PSet", "DisableRFID");

        public int CylinderTimeOut => GetSettingValue("PSet", "CTimeOut");
        public int InTimeOut => GetSettingValue("PSet", "InTimeOut");

        public bool RFID;


        Fanuc_RobotControl fanuc_RobotControl = new Fanuc_RobotControl();
        JTimer J_AxisAutoTm = new JTimer();
        public JTimer Conveyor1Timeout = new JTimer();

        public JTimer Conveyor2Timeout = new JTimer();
        int Inindex = 0;
        #endregion

        #region Forms & Init
        public MESForm MesF = new MESForm();
        public ConveyorForm()
        {
            InitializeComponent();
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetTimerRaise;
            SetDoubleBuffer(plProductionSetting);
            A_syGoleRFID.ReceiveHandler();
            B_syGoleRFID.ReceiveHandler();
        }

        private void FlowChartMessage_ResetTimerRaise(object sender, EventArgs e)
        {
            RunTM.Restart();
        }
        #endregion

        #region Override Method     
        public override void AfterProductionSetting()
        {

        }

        public override void IntoProductionSettingPage()
        {

        }

        public override void AlwaysRun()
        {
            #region Conveyor Alarm
            if (IB_Station1_MotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5023");
            }
            if (IB_SF1ConveyorMotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5024");
            }
            if (IB_Station2_MotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5025");
            }
            if (IB_SF2ConveyorMotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5026");
            }
            if (IB_Conveyor1_MotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5027");
            }
            if (IB_Conveyor2_MotorAlarm.IsOff())
            {
                JSDK.Alarm.Show("5028");
            }
            #endregion
        }

        public override void InitialReset()
        {
            flowChart0_11.TaskReset();
        }

        public override void RunReset()
        {
            flowChart117.TaskReset();
            flowChart32.TaskReset();
        }

        public override void Initial()
        {
            flowChart0_11.TaskRun();
        }

        public override void Run()
        {
            flowChart117.TaskRun();
            flowChart32.TaskRun();
        }

        public override void StartRun()
        {
            RunTM.Restart();
        }

        public override void StopRun()
        {

        }
        #endregion

        #region Functions
        #region RFID
        public enum RFIDResult
        {
            OK,
            NG,
            NA
        }
        public void WriteToRFID(int processid,bool result, SyGoleRFID syGoleRFID)
        {
            syGoleRFID.RFID_Clear(GetRecipeValue("RSet", "A_RFIDId"));
            string data = $"{processid}{(result ? "OK" : "NG")}";
            syGoleRFID.RFID_Write(GetRecipeValue("RSet", "A_RFIDId"),data , "0", data.Length.ToString());
        }

        public RFIDResult ReadRFID(int processid, SyGoleRFID syGoleRFID)
        {
            string result= syGoleRFID.RFID_ReadDataString(GetRecipeValue("RSet", "A_RFIDId"), "", "0", "100");
            result = result.Replace(" ", "").Replace("\0","");

            if (result.IndexOf(processid.ToString())<0)
                return RFIDResult.NA;
            if (result.Substring(result.IndexOf(processid.ToString())+2) == "OK")
            {
                return RFIDResult.OK;
            }
            else
                return RFIDResult.NG;
        }

        /// <summary>
        /// 返回两个指定字符串之间的字符串
        /// </summary>
        /// <param name="sourse"></param>
        /// <param name="startstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public string MidStrEx_New(string sourse, string startstr, string endstr)
        {
            Regex rg = new Regex("(?<=(" + startstr + "))[.\\s\\S]*?(?=(" + endstr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(sourse).Value;
            //Regex rg = new Regex("(?<=" + startstr + "))[.\\s\\S]*?(?=(" + endstr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            //return rg.Match(sourse).Value;
        }
        #endregion

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        private bool Delay(JTimer timer, int TimeOut)
        {
            bool ret = false;
            if (timer.IsOn(TimeOut))
            {
                ret = true;
            }
            return ret;
        }


        public bool C_DelayMs(int delayMilliseconds)
        {
            DateTime now = DateTime.Now;
            Double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.TotalMilliseconds + spand.Seconds * 1000;
                Application.DoEvents();
            }
            while (s < delayMilliseconds);
            return true;
        }

        #endregion

        #region UI Update
        private void CvyTim_UpdateUI_Tick(object sender, EventArgs e)
        {

        }
        #endregion

        #region RFID
        public SyGoleRFID A_syGoleRFID = new SyGoleRFID();
        public SyGoleRFID B_syGoleRFID = new SyGoleRFID();
        public SyGoleRFID C_syGoleRFID = new SyGoleRFID();
        public SyGoleRFID D_syGoleRFID = new SyGoleRFID();

        List<MyRFIDDataStruct> RFIDDataStructList1 = new List<MyRFIDDataStruct>();
        List<MyRFIDDataStruct> RFIDDataStructList2 = new List<MyRFIDDataStruct>();

        public struct MyRFIDDataStruct
        {
            public SyGoleRFID SyGoleRFID;
            public bool ResultBool;
            public String RFIDReadData;

            public MyRFIDDataStruct(SyGoleRFID syGoleRFID, bool resultBool, string rFIDReadData)
            {
                SyGoleRFID = syGoleRFID;
                ResultBool = resultBool;
                RFIDReadData = rFIDReadData;
            }
        }

        public MyRFIDDataStruct myRFID1 = new MyRFIDDataStruct();
        public MyRFIDDataStruct myRFID2 = new MyRFIDDataStruct();
        public MyRFIDDataStruct myRFID3 = new MyRFIDDataStruct();
        public MyRFIDDataStruct myRFID4 = new MyRFIDDataStruct();


        private void B_RFIDAConnect_Click(object sender, EventArgs e)
        {
            if (A_syGoleRFID.connect)
            {
                TextDataShow("Device connected", R_RFIDADataShow, true);
                return;
            }
            if (A_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "A_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "A_RFIDPort")), GetRecipeValue("RSet", "A_RFIDId")))
            {
                B_RFIDAConnect.Enabled = false;
                TextDataShow("Connection successful", R_RFIDADataShow, true);
                return;
            }
            TextDataShow("Connection failure", R_RFIDADataShow, false);
        }


        private void B_RFIDBConnect_Click(object sender, EventArgs e)
        {
            if (B_syGoleRFID.connect)
            {
                TextDataShow("Device connected", R_RFIDBDataShow, true);
                return;
            }
            if (B_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "B_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "B_RFIDPort")), GetRecipeValue("RSet", "B_RFIDId")))
            {
                B_RFIDBConnect.Enabled = false;
                TextDataShow("Connection successful", R_RFIDBDataShow, true);
                return;
            }
            TextDataShow("Connection failure", R_RFIDBDataShow, false);
        }


        private void B_RFIDCConnect_Click(object sender, EventArgs e)
        {
            if (C_syGoleRFID.connect)
            {
                TextDataShow("Device connected", R_RFIDCDataShow, true);
                return;
            }
            if (C_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "C_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "C_RFIDPort")), GetRecipeValue("RSet", "C_RFIDId")))
            {
                B_RFIDCConnect.Enabled = false;
                TextDataShow("Connection successful", R_RFIDCDataShow, true);
                return;
            }
            TextDataShow("Connection failure", R_RFIDCDataShow, false);
        }


        private void B_RFIDDConnect_Click(object sender, EventArgs e)
        {
            if (D_syGoleRFID.connect)
            {
                TextDataShow("Device connected", R_RFIDDDataShow, true);
                return;
            }
            if (D_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "D_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "D_RFIDPort")), GetRecipeValue("RSet", "D_RFIDId")))
            {
                B_RFIDDConnect.Enabled = false;
                TextDataShow("Connection successful", R_RFIDDDataShow, true);
                return;
            }
            TextDataShow("Connection failure", R_RFIDDDataShow, false);
        }


        private void B_RFIDADisconnect_Click(object sender, EventArgs e)
        {
            A_syGoleRFID.RFID_DisConnect();
            B_RFIDAConnect.Enabled = true;
            TextDataShow("Disconnect", R_RFIDADataShow, true);
        }


        private void B_RFIDBDisconnect_Click(object sender, EventArgs e)
        {
            B_syGoleRFID.RFID_DisConnect();
            B_RFIDBConnect.Enabled = true;
            TextDataShow("Disconnect", R_RFIDBDataShow, true);
        }


        private void B_RFIDCDisconnect_Click(object sender, EventArgs e)
        {
            C_syGoleRFID.RFID_DisConnect();
            B_RFIDCConnect.Enabled = true;
            TextDataShow("Disconnect", R_RFIDCDataShow, true);
        }


        private void B_RFIDDDisconnect_Click(object sender, EventArgs e)
        {
            D_syGoleRFID.RFID_DisConnect();
            B_RFIDDConnect.Enabled = true;
            TextDataShow("Disconnect", R_RFIDDDataShow, true);
        }


        private void B_RFIDAReadUID_Click(object sender, EventArgs e)
        {
            if (!A_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDADataShow, false);
                return;
            }
            TextDataShow("UID read successfully: " + A_syGoleRFID.RFID_ReadUID(GetRecipeValue("RSet", "A_RFIDId")), R_RFIDADataShow, true);
        }


        private void B_RFIDBReadUID_Click(object sender, EventArgs e)
        {
            if (!B_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDBDataShow, false);
                return;
            }
            TextDataShow("UID read successfully: " + B_syGoleRFID.RFID_ReadUID(GetRecipeValue("RSet", "B_RFIDId")), R_RFIDBDataShow, true);
        }


        private void B_RFIDCReadUID_Click(object sender, EventArgs e)
        {
            if (!C_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDCDataShow, false);
                return;
            }
            TextDataShow("UID read successfully: " + C_syGoleRFID.RFID_ReadUID(GetRecipeValue("RSet", "C_RFIDId")), R_RFIDCDataShow, true);
        }


        private void B_RFIDDReadUID_Click(object sender, EventArgs e)
        {
            if (!D_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDDDataShow, false);
                return;
            }
            TextDataShow("UID read successfully: " + D_syGoleRFID.RFID_ReadUID(GetRecipeValue("RSet", "D_RFIDId")), R_RFIDDDataShow, true);
        }


        private void B_RFIDAReadData_Click(object sender, EventArgs e)
        {
            if (!A_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDADataShow, false);
                return;
            }
            string pos = T_RFIDAOAdderss.Text == "" ? "0" : T_RFIDAOAdderss.Text;
            string len = T_RFIDAEndAddress.Text == "" ? "16" : T_RFIDAEndAddress.Text;
            TextDataShow("Read data successfully: " + A_syGoleRFID.RFID_ReadDataString(GetRecipeValue("RSet", "A_RFIDId"), "", pos, len), R_RFIDADataShow, true);
        }


        private void B_RFIDBReadData_Click(object sender, EventArgs e)
        {
            if (!B_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDBDataShow, false);
                return;
            }
            string pos = T_RFIDBOAdderss.Text == "" ? "0" : T_RFIDBOAdderss.Text;
            string len = T_RFIDBEndAddress.Text == "" ? "16" : T_RFIDBEndAddress.Text;
            TextDataShow("Read data successfully: " + B_syGoleRFID.RFID_ReadDataString(GetRecipeValue("RSet", "B_RFIDId"), "", pos, len), R_RFIDBDataShow, true);
        }


        private void B_RFIDCReadData_Click(object sender, EventArgs e)
        {
            if (!C_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDCDataShow, false);
                return;
            }
            string pos = T_RFIDCOAdderss.Text == "" ? "0" : T_RFIDCOAdderss.Text;
            string len = T_RFIDCEndAddress.Text == "" ? "16" : T_RFIDCEndAddress.Text;
            TextDataShow("Read data successfully: " + C_syGoleRFID.RFID_ReadDataString(GetRecipeValue("RSet", "C_RFIDId"), "", pos, len), R_RFIDCDataShow, true);
        }


        private void B_RFIDDReadData_Click(object sender, EventArgs e)
        {
            if (!D_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDDDataShow, false);
                return;
            }
            string pos = T_RFIDDOAdderss.Text == "" ? "0" : T_RFIDDOAdderss.Text;
            string len = T_RFIDDEndAddress.Text == "" ? "16" : T_RFIDDEndAddress.Text;
            TextDataShow("Read data successfully: " + D_syGoleRFID.RFID_ReadDataString(GetRecipeValue("RSet", "D_RFIDId"), "", pos, len), R_RFIDDDataShow, true);
        }


        private void B_RFIDAWriteData_Click(object sender, EventArgs e)
        {
            if (!A_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDADataShow, false);
                return;
            }
            if (T_RFIDAWriteData.Text == "")
            {
                TextDataShow("Parameter error", R_RFIDADataShow, false);
                return;
            }
            string pos = T_RFIDAOAdderss.Text == "" ? "0" : T_RFIDAOAdderss.Text;
            string len = T_RFIDAEndAddress.Text == "" ? "16" : T_RFIDAEndAddress.Text;
            TextDataShow("Write data successfully: " + A_syGoleRFID.RFID_Write(GetRecipeValue("RSet", "A_RFIDId"), T_RFIDAWriteData.Text, pos, len), R_RFIDADataShow, true);
        }


        private void B_RFIDBWriteData_Click(object sender, EventArgs e)
        {
            if (!B_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDBDataShow, false);
                return;
            }
            if (T_RFIDBWriteData.Text == "")
            {
                TextDataShow("Parameter error", R_RFIDBDataShow, false);
                return;
            }
            string pos = T_RFIDBOAdderss.Text == "" ? "0" : T_RFIDBOAdderss.Text;
            string len = T_RFIDBEndAddress.Text == "" ? "16" : T_RFIDBEndAddress.Text;
            TextDataShow("Write data successfully: " + B_syGoleRFID.RFID_Write(GetRecipeValue("RSet", "B_RFIDId"), T_RFIDBWriteData.Text, pos, len), R_RFIDBDataShow, true);
        }


        private void B_RFIDCWriteData_Click(object sender, EventArgs e)
        {
            if (!C_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDCDataShow, false);
                return;
            }
            if (T_RFIDCWriteData.Text == "")
            {
                TextDataShow("Parameter error", R_RFIDCDataShow, false);
                return;
            }
            string pos = T_RFIDCOAdderss.Text == "" ? "0" : T_RFIDCOAdderss.Text;
            string len = T_RFIDCEndAddress.Text == "" ? "16" : T_RFIDCEndAddress.Text;
            TextDataShow("Write data successfully: " + C_syGoleRFID.RFID_Write(GetRecipeValue("RSet", "C_RFIDId"), T_RFIDCWriteData.Text, pos, len), R_RFIDCDataShow, true);
        }


        private void B_RFIDDWriteData_Click(object sender, EventArgs e)
        {
            if (!D_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDDDataShow, false);
                return;
            }
            if (T_RFIDDWriteData.Text == "")
            {
                TextDataShow("Parameter error", R_RFIDDDataShow, false);
                return;
            }
            string pos = T_RFIDDOAdderss.Text == "" ? "0" : T_RFIDDOAdderss.Text;
            string len = T_RFIDDEndAddress.Text == "" ? "16" : T_RFIDDEndAddress.Text;
            TextDataShow("Write data successfully: " + D_syGoleRFID.RFID_Write(GetRecipeValue("RSet", "D_RFIDId"), T_RFIDDWriteData.Text, pos, len), R_RFIDDDataShow, true);
        }


        private void B_RFIDAClearData_Click(object sender, EventArgs e)
        {
            if (!A_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDADataShow, false);
                return;
            }
            TextDataShow("Succeeded in clearing data: " + A_syGoleRFID.RFID_Clear(GetRecipeValue("RSet", "A_RFIDId")), R_RFIDADataShow, true);
        }


        private void B_RFIDBClearData_Click(object sender, EventArgs e)
        {
            if (!B_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDBDataShow, false);
                return;
            }
            TextDataShow("Succeeded in clearing data: " + B_syGoleRFID.RFID_Clear(GetRecipeValue("RSet", "B_RFIDId")), R_RFIDBDataShow, true);
        }


        private void B_RFIDCClearData_Click(object sender, EventArgs e)
        {
            if (!C_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDCDataShow, false);
                return;
            }
            TextDataShow("Succeeded in clearing data: " + C_syGoleRFID.RFID_Clear(GetRecipeValue("RSet", "C_RFIDId")), R_RFIDCDataShow, true);
        }


        private void B_RFIDDClearData_Click(object sender, EventArgs e)
        {
            if (!D_syGoleRFID.connect)
            {
                TextDataShow("Device not connected", R_RFIDDDataShow, false);
                return;
            }
            TextDataShow("Succeeded in clearing data: " + D_syGoleRFID.RFID_Clear(GetRecipeValue("RSet", "D_RFIDId")), R_RFIDDDataShow, true);
        }


        public static void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }


        public void TextDataShow(string Textshow, RichTextBox richText, bool OK)
        {
            RefreshDifferentThreadUI(richText, () =>
            {
                if (richText.TextLength > 10000)
                {
                    richText.Clear();
                }
                if (OK == true)
                    richText.SelectionColor = Color.Green;
                else
                    richText.SelectionColor = Color.Red;
                richText.AppendText(Textshow + Environment.NewLine);
                richText.SelectionStart = richText.TextLength; richText.ScrollToCaret();
            });
        }
        #endregion

        private FCResultType flowChart0_11_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.Off();
            OB_Conveyor1_MotorReverse.Off();
            OB_Conveyor2_MotorForward.Off();
            OB_Conveyor2_MotorReverse.Off();
            OB_Station1_MotorForward.Off();
            OB_Station1_MotorReverse.Off();
            OB_Station2_MotorForward.Off();
            OB_Station2_MotorReverse.Off();
            output2.Off();
            output1.Off();
            OB_MotorForward.Off();
            OB_MotorReverse.Off();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart71_FlowRun(object sender, EventArgs e)
        {
            if (IB_BoardIn.IsOn() || IB_BoardStop.IsOn() || IB_Conveyor1_Staiton1_BoardStop.IsOn() || IB_Conveyor1_Staiton2_BoardStop.IsOn() || IB_Conveyor2_Station1_BoardStop.IsOn()
                || IB_Conveyor2_Station2_BoardStop.IsOn() || IB_Conveyor2_Station3_BoardStop.IsOn() || IB_Conveyor2_Boardout.IsOn())
            {
                JSDK.Alarm.Show("5029");
                Conveyor1Timeout.Restart();
                return FCResultType.IDLE;
            }
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }
        private FCResultType flowChart55_FlowRun(object sender, EventArgs e)
        {
            if (DisableRFID)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (A_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "A_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "A_RFIDPort")), GetRecipeValue("RSet", "A_RFIDId")))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart56_FlowRun(object sender, EventArgs e)
        {
            if (DisableRFID)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (A_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "B_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "B_RFIDPort")), GetRecipeValue("RSet", "B_RFIDId")))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart57_FlowRun(object sender, EventArgs e)
        {
            if (DisableRFID)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (A_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "C_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "C_RFIDPort")), GetRecipeValue("RSet", "C_RFIDId")))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart58_FlowRun(object sender, EventArgs e)
        {
            if (DisableRFID)
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (A_syGoleRFID.RFID_Connect(GetRecipeValue("RSet", "D_RFIDIp"), ushort.Parse(GetRecipeValue("RSet", "D_RFIDPort")), GetRecipeValue("RSet", "D_RFIDId")))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart66_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_Station1_Jacking.Off();
            if (IB_Conveyor1_Station1_JackingCylinderDown.IsOn() && IB_Conveyor1_Station1_JackingCylinderUp.IsOff())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5107");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart68_FlowRun(object sender, EventArgs e)
        {
            Gantry1NGProduct = false;
            Gantry2NGProduct = false;

            B_Gantry1Out = false;
            B_Gantry2Out = false;
            B_Cash1In = true;
            B_Cash1Out = false;
            B_3DIn = true;
            B_3DOut = false;
            B_Cash2In = true;
            B_Cash2Out = false;
            B_PnPIn = true;

            ConveyorStation1NoEmpty = false;
            ConveyorStation2NoEmpty = false;
            MachineAvailable1 = false;
            MachineAvailable2 = false;
            Station1Start = false;
            Station2Start = false;
            Dryrun1 = false;
            Dryrun2 = false;

            Stationwork1 = false;
            Stationwork1Comp = false;
            Stationwork2 = false;
            Stationwork2Comp = false;
            UpMachineAvailable_SMEMA = false;
            DownMachineReady_SMEMA = false;
            StationMachineIn1 = false;
            StationMachineIn2 = false;
            ConveyorBStation1RobotStart1 = false;
            ConveyorBStation1RobotComp1 = false;
            ConveyorBStation2RobotStart2 = false;
            ConveyorBStation2RobotComp2 = false;
            ConveyorBStart1 = false;
            ConveyorBStart2 = false;
            bInitialOk = true;
            RFID = false;// MiddleLayer.SystemF.GetSettingValue("PSet", "UseRFID");
            RFIDDataStructList1.Clear();
            RFIDDataStructList2.Clear();

            OB_Conveyor1_MotorForward.Off();
            OB_Conveyor1_MotorReverse.Off();
            OB_Conveyor2_MotorForward.Off();
            OB_Conveyor2_MotorReverse.Off();
            OB_Station1_MotorForward.Off();
            OB_Station1_MotorReverse.Off();
            OB_Station2_MotorForward.Off();
            OB_Station2_MotorReverse.Off();
            output2.Off();
            output1.Off();
            OB_MotorForward.Off();
            OB_MotorReverse.Off();

            OB_LocalMachineReady_SMEMA.Off();
            OB_LocalMachineWorkNG_SMEMA.Off();
            OB_LocalMachineAvailable_SMEMA.Off();
            Conveyor1Timeout.Restart();
            Conveyor2Timeout.Restart();

            return FCResultType.IDLE;
        }

        private FCResultType flowChart93_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station_StopCylinder.On();
            if (IB_Conveyor2_Station_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5162");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart67_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.On();
            if (IB_Conveyor1_Station1_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5109");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }


        #region autoflow



        int Outindex = 0;

        private FCResultType flowChart104_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station_BoardStop.IsOff())
            {
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }



        private FCResultType flowChart91_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station1_BoardStop.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(InTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5150");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart90_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station_StopCylinder.Off();
            if (IB_Conveyor2_Station_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5163");
            }
            return FCResultType.IDLE;
        }



        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station3_StopCylinder.Off();
            if (IB_Conveyor2_Station3_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5161");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart63_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station3_BoardStop.IsOn(200) || (Dryrun && C_DelayMs(500)))
            {
                Conveyor1Timeout.Restart();
                ConveyorBStart2 = false;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
            if (RFIDDataStructList2.Count > 0)
            {
                if (IB_Conveyor2_Station2_BoardStop.IsOn() && IB_Conveyor2_Station3_BoardStop.IsOff())
                {
                    Conveyor1Timeout.Restart();
                    return FCResultType.CASE1;
                }

                if (IB_Conveyor2_Station3_BoardStop.IsOn(200) || (Dryrun && C_DelayMs(500)))
                {
                    Conveyor1Timeout.Restart();
                    ConveyorBStart2 = false;
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            if (ByPass /*|| !MiddleLayer.AP_PCBA_V.B_DetectionResult||!MiddleLayer.AP_PCBA_V.B_HeightMeasureResult*/)
            {
                return FCResultType.CASE1;
            }
            if (Dryrun)
            {
                return FCResultType.NEXT;
            }
            if (!DisableRFID)
            {
                if (RFIDDataStructList2[0].ResultBool)
                {
                    Conveyor1Timeout.Restart();
                    myRFID4 = new MyRFIDDataStruct(D_syGoleRFID, true, "OK");
                    RFIDDataStructList2.RemoveAt(0);
                    return FCResultType.NEXT;
                }
                else
                {
                    MiddleLayer.SystemF.ShowStatus(4, SystemForm.WorkStatus.Fail);
                    Conveyor1Timeout.Restart();
                    myRFID4 = new MyRFIDDataStruct(D_syGoleRFID, false, "NG");
                    RFIDDataStructList2.RemoveAt(0);
                    return FCResultType.CASE1;
                }
            }

            if (ReadRFID(15, D_syGoleRFID) == RFIDResult.NA)
            {
                RFIDDataStructList2.RemoveAt(0);
                Conveyor1Timeout.Restart();
                return FCResultType.CASE1;
            }
            else if (ReadRFID(15, D_syGoleRFID) == RFIDResult.NG)
            {
                RFIDDataStructList2.RemoveAt(0);
                Conveyor1Timeout.Restart();
                return FCResultType.CASE1;
            }
            else if (ReadRFID(15, D_syGoleRFID) == RFIDResult.OK)
            {
                RFIDDataStructList2.RemoveAt(0);
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart64_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor2_Station3_Jacking.On();
            if (IB_Conveyor2_Station3_JackingCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                ConveyorBStation2RobotStart2 = true;
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5155");
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart37_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.On();
            if (IB_Conveyor2_Station2_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5164");
            }
            return FCResultType.IDLE;
        }
        private FCResultType flowChart88_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station3_BoardStop.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(InTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5151");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart89_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.Off();
            if (IB_Conveyor2_Station2_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5165");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart34_FlowRun(object sender, EventArgs e)
        {
            if (ConveyorBStation2RobotComp2)
            {
                CYL_Conveyor2_Station3_Jacking.Off();
                if (IB_Conveyor2_Station3_JackingCylinderDown.IsOn())
                {
                    Conveyor1Timeout.Restart();
                    ConveyorBStation2RobotComp2 = false;
                    return FCResultType.NEXT;
                }
                if (Conveyor1Timeout.IsOn(CylinderTimeOut))
                {
                    Conveyor1Timeout.Restart();
                    JSDK.Alarm.Show("5157");
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            if (Dryrun)
            {
                return FCResultType.NEXT;
            }
            OB_LocalMachineAvailable_SMEMA.On();
            if (IB_DownMachineReady_SMEMA.IsOn() || DownMachineReady_SMEMA)  //按钮模拟下机要料
            {
                DownMachineReady_SMEMA = false;
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart52_FlowRun(object sender, EventArgs e)
        {
            B_PnPIn = true;
            OB_Conveyor2_Station3_StopCylinder.On();
            //OB_Conveyor2_Station2_StopCylinder.On();
            if (/*IB_Conveyor2_Station2_StopCylinderDown.IsOn() &&*/ IB_Conveyor2_Station3_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }

            if (Conveyor1Timeout.IsOn(CylinderTimeOut))
            {
                //if (IB_Conveyor2_Station2_StopCylinderDown.IsOff())
                //{
                //    JSDK.Alarm.Show("5164");
                //}
                if (IB_Conveyor2_Station3_StopCylinderDown.IsOff())
                {
                    JSDK.Alarm.Show("5159");
                }
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            if (Conveyor1Timeout.IsOn(500))
            {
                OB_Conveyor2_Station3_StopCylinder.Off();
                //OB_Conveyor2_Station2_StopCylinder.Off();
                if (IB_Conveyor2_Station3_StopCylinderUp.IsOn()/* && IB_Conveyor2_Station2_StopCylinderUp.IsOn()*/)
                {
                    Conveyor1Timeout.Restart();
                    return FCResultType.NEXT;
                }

                if (Conveyor1Timeout.IsOn(CylinderTimeOut))
                {
                    if (IB_Conveyor2_Station3_StopCylinderUp.IsOff())
                    {
                        JSDK.Alarm.Show("5161");
                    }
                    //if (IB_Conveyor2_Station2_StopCylinderUp.IsOff())
                    //{
                    //    JSDK.Alarm.Show("5165");
                    //}
                    Conveyor1Timeout.Restart();

                }
            }
            return FCResultType.IDLE;
        }

        private bool Conveyor2Boardout = false;
        private FCResultType flowChart53_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Boardout.IsOn() || Conveyor2Boardout || (Dryrun && C_DelayMs(1000)))
            {
                Conveyor2Boardout = false;
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(InTimeOut))
            {
                JSDK.Alarm.Show("5166");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Boardout.IsOff() || (Dryrun && C_DelayMs(1000)))
            {
                OB_LocalMachineAvailable_SMEMA.Off();
                OB_LocalMachineWorkNG_SMEMA.Off();
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }

            if (Conveyor1Timeout.IsOn(InTimeOut))
            {
                Conveyor1Timeout.Restart();
                JSDK.Alarm.Show("5166");
            }
            return FCResultType.IDLE;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DownMachineReady_SMEMA = true;
        }

        private FCResultType flowChart54_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Conveyor2Boardout = true;
        }

        private FCResultType flowChart65_FlowRun(object sender, EventArgs e)
        {
            OB_LocalMachineWorkNG_SMEMA.On();
            return FCResultType.NEXT;
        }



        private FCResultType flowChart72_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_Station2_Jacking.Off();
            if (IB_Conveyor1_Station2_JackingCylinderDown.IsOn() && IB_Conveyor1_Station2_JackingCylinderUp.IsOff())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5108");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart73_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor2_Station1_Jacking.Off();
            if (IB_Conveyor2_Station1_JackingCylinderDown.IsOn() && IB_Conveyor2_Station1_JackingCylinderUp.IsOff())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5156");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }













        private FCResultType flowChart74_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor2_Station3_Jacking.Off();
            if (IB_Conveyor2_Station3_JackingCylinderDown.IsOn() && IB_Conveyor2_Station3_JackingCylinderUp.IsOff())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5157");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart75_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.On();
            if (IB_Conveyor1_Station2_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5110");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart76_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station1_StopCylinder.On();
            if (IB_Conveyor2_Station1_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5158");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart77_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.On();
            if (IB_Conveyor2_Station2_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5164");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart78_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.On();
            OB_Conveyor1_MotorReverse.Off();
            OB_Conveyor2_MotorForward.On();
            OB_Conveyor2_MotorReverse.Off();
            OB_MotorForward.On();
            OB_MotorReverse.Off();
            OB_Station1_MotorForward.On();
            OB_Station1_MotorReverse.Off();
            OB_Station2_MotorForward.On();
            OB_Station2_MotorReverse.Off();
            output2.On();
            output1.Off();
            if (Conveyor1Timeout.IsOn(5000))
            {
                OB_Conveyor1_MotorForward.Off();
                OB_Conveyor1_MotorReverse.Off();
                OB_Conveyor2_MotorForward.Off();
                OB_Conveyor2_MotorReverse.Off();
                OB_MotorForward.Off();
                OB_MotorReverse.Off();
                OB_Station1_MotorForward.Off();
                OB_Station1_MotorReverse.Off();
                OB_Station2_MotorForward.Off();
                OB_Station2_MotorReverse.Off();
                Conveyor1Timeout.Restart();
                output2.Off();
                output1.On();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart79_FlowRun(object sender, EventArgs e)
        {
            if (IB_BoardIn.IsOn() || IB_BoardStop.IsOn() || IB_Conveyor1_Staiton1_BoardStop.IsOn() || IB_Conveyor1_Staiton2_BoardStop.IsOn() || IB_Conveyor2_Station1_BoardStop.IsOn()
    || IB_Conveyor2_Station2_BoardStop.IsOn() || IB_Conveyor2_Station3_BoardStop.IsOn() || IB_Conveyor2_Boardout.IsOn())
            {
                JSDK.Alarm.Show("5029");
                Conveyor1Timeout.Restart();
                return FCResultType.IDLE;
            }
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }


        private FCResultType flowChart98_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            if (IB_Conveyor1_Station1_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5111");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart97_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.Off();
            if (IB_Conveyor1_Station2_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5112");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart94_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station_StopCylinder.Off();
            if (IB_Conveyor2_Station_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5163");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart95_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station1_StopCylinder.Off();
            if (IB_Conveyor2_Station1_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5160");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart96_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.Off();
            if (IB_Conveyor2_Station2_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5165");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart99_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station3_StopCylinder.On();
            if (IB_Conveyor2_Station3_StopCylinderDown.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5159");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station3_StopCylinder.Off();
            if (IB_Conveyor2_Station3_StopCylinderUp.IsOn())
            {
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor1Timeout.IsOn(10000))
            {
                JSDK.Alarm.Show("5161");
                Conveyor1Timeout.Restart();
            }
            return FCResultType.IDLE;
        }








      










        #region Cash1
 
















        #endregion

        #region Cash2
        private FCResultType flowChart117_FlowRun(object sender, EventArgs e)
        {
            Conveyor2Timeout.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {(flowChart117.Text)} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart118_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.Off();
            if (IB_Conveyor2_Station2_StopCylinderUp.IsOn())
            {
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor2Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor2Timeout.Restart();
                JSDK.Alarm.Show("5165");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart119_FlowRun(object sender, EventArgs e)
        {
            B_Cash2In = true;
            Conveyor2Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart120_FlowRun(object sender, EventArgs e)
        {
            if (B_3DOut )
            {
                B_3DOut = false;
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart121_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station2_BoardStop.IsOn())
            {
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart122_FlowRun(object sender, EventArgs e)
        {
            B_Cash2Out = true;
            Conveyor2Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart123_FlowRun(object sender, EventArgs e)
        {
            if (B_PnPIn)
            {
                B_PnPIn = false;
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart129_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.On();
            if (IB_Conveyor2_Station2_StopCylinderDown.IsOn())
            {
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (Conveyor2Timeout.IsOn(CylinderTimeOut))
            {
                Conveyor2Timeout.Restart();
                JSDK.Alarm.Show("5164");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart130_FlowRun(object sender, EventArgs e)
        {
            if (Conveyor2Timeout.IsOn(500))
            {
                Conveyor2Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart131_FlowRun(object sender, EventArgs e)
        {
            Conveyor2Timeout.Restart();
            return FCResultType.NEXT;
        }
        #endregion

    






        private FCResultType flowChart89_FlowRun_1(object sender, EventArgs e)
        {
            //B_PnPIn = true;
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart90_FlowRun_1(object sender, EventArgs e)
        {
            if (B_Cash2Out)
            {
                B_Cash2Out = false;
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

    

  


 





        private FCResultType flowChart145_FlowRun(object sender, EventArgs e)
        {
            if (DisableRFID)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart145.Text} finish", true);
                Conveyor1Timeout.Restart();
                return FCResultType.NEXT;
            }
            if (myRFID4.ResultBool)
            {
                WriteToRFID(16, true,D_syGoleRFID);
            }
            else
                WriteToRFID(16, false,D_syGoleRFID);
            Conveyor1Timeout.Restart();
            return FCResultType.NEXT;
        }




 


        private void tabPage15_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private FCResultType flowChart116_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
    }
    #endregion

}
