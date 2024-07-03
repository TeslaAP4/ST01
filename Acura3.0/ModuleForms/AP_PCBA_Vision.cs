using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acura3._0.Classes;
using Acura3.New.Device;
using AcuraLibrary.Forms;
using JabilSDK;
using JabilSDK.Enums;
using NPFanucRobotDLL;
using Cognex.VisionPro;
using System.IO;

namespace Acura3._0.ModuleForms
{
    public partial class AP_PCBA_Vision : ModuleBaseForm
    {
        public AP_PCBA_Vision()
        {
            InitializeComponent();
            MessageForm.MuteRaise += MessageForm_MuteRaise;
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);
            ReadProductData();
        }

        public DateTime CTStartTime;
        public DateTime TaktStartTime;
        public int I_NGCount = 1;
        public AlarmClass.AlarmDataClass AlarmContent = new AlarmClass.AlarmDataClass();
        public List<VppComp.VisionControl> VisionTaskList = new List<VppComp.VisionControl>();
        public List<CogRecordDisplay> CogRecordList = new List<CogRecordDisplay>();
        public bool B_DetectionResult = false;
        public bool B_HeightMeasureResult = false;
        public List<bool> B_HeightResultList = new List<bool>();
        public double HeightLimit_max => GetRecipeValue("RSet", "HeightLimit_Max");

        public double HeightLimit_min => GetRecipeValue("RSet", "HeightLimit_Min");


        public bool B_ShowStatus = false;
        /// <summary>
        /// Screw floating height data list 
        /// </summary>
        public List<string> HeightArrayList = new List<string>();

        ///// <summary>
        ///// single screw floating height result list
        ///// </summary>
        //public List<bool> B_SingleHeightResult = new List<bool>();

        #region Local Method
        private void MessageForm_MuteRaise(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.BuzzOff();
        }

        private void FlowChartMessage_PauseRaise(object sender, EventArgs e)
        {
            MiddleLayer.StopRun();
        }

        private void FlowChartMessage_ResetRaise(object sender, EventArgs e)
        {
            RunTM.Restart();
        }

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
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

        #endregion

        public override void InitialReset()
        {
            flowChart1.TaskReset();

            detection_Index = 1;
            heightMeasure_Index = 1;
            I_SnapNGtimes = 0;
            B_PickPCBFlow = false;
            B_PutToConveryPCBFlow = false;
            B_DetectionResult = false;
        }

        public override void Initial()
        {
            flowChart1.TaskRun();
        }

        public override void RunReset()
        {
            flowChart9.TaskReset();
            flowChart2_1.TaskReset();
            flowChart3_1.TaskReset();
            CTStartTime = DateTime.Now;
            TaktStartTime = DateTime.Now;
        }

        public override void Run()
        {
            flowChart9.TaskRun();
            flowChart2_1.TaskRun();
            flowChart3_1.TaskRun();
        }

        public override void ServoOn()
        {

        }

        public override void ServoOff()
        {

        }

        public override void StopRun()
        {
            F_Robot.RobotPause(OB_Robot_Maintain);
            MiddleLayer.SystemF.DelayMs(300);
            OB_Robot_Maintain.On();
        }

        public override void StartRun()
        {
            F_Robot.RobotContinue(OB_Robot_Maintain, OB_Robot_Start);
        }

        public override void AlwaysRun()
        {
            if (IB_Robot_AbnormalAlarm.IsOn())
            {
                JSDK.Alarm.Show("4180");
            }
            if (IB_Robot_BatteryAlarm.IsOn())
            {
                JSDK.Alarm.Show("4181");
            }
            if (IB_Robot_EStop.IsOff())
            {
                JSDK.Alarm.Show("4182");
            }
        }

        public bool DisableCCD { get => GetSettingValue("PSet", "DisableCCD"); }
        public bool DisableLaser { get => GetSettingValue("PSet", "DisableLaser"); }
        public bool B_byPassVisionResult { get => GetSettingValue("PSet", "ByPassVisionResult"); }
        public bool B_byPassHeightResult { get => GetSettingValue("PSet", "ByPassScrewHeightResult"); }

        /// <summary>
        /// 初始化流程时间实例化
        /// </summary>
        public JTimer J_Initial = new JTimer();

        /// <summary>
        /// 自动流程时间实例化
        /// </summary>
        public JTimer J_AutoRun = new JTimer();

        public Fanuc_RobotControl F_Robot = new Fanuc_RobotControl();

        public IX_1000 IX_Keyence;

        string IP => GetRecipeValue("RSet", "IX_IP");
        int Port => GetRecipeValue("RSet", "IX_Port");

        public bool B_PickPCBFlow { get; set; } = false;

        public bool B_PutToConveryPCBFlow { get; set; } = false;

        public int detection_Index = 1;
        public int heightMeasure_Index = 1;

        #region RobotAlarm
        public void RobotAlarm( FlowChartMessage flow)
        {
            try
            {
                double AlarmCode = F_Robot.GetAlarmID();
                if (AlarmCode == 0)
                {
                    JSDK.Alarm.Show("4213");
                    flow.Title = "Robot move overtime alarm !";
                    flow.Content = "4213: 4-axis robot move overtime";
                }
                else
                {
                    JSDK.Alarm.Show((4109 + AlarmCode).ToString());
                    if (flow != null && JSDK.Alarm.IsExistInSummary((4109 + AlarmCode).ToString(), ref AlarmContent))
                    {
                        flow.Title = AlarmContent.Content;
                        flow.Content = AlarmCode + "-" + AlarmContent.Content;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region //RobotStart

        public bool RobotStart()
        {
            OB_Robot_Maintain.Off();
            OB_Robot_Program1.Off();
            MiddleLayer.SystemF.DelayMs(1000);
            OB_Robot_Stop.On();
            MiddleLayer.SystemF.DelayMs(1000);
            OB_Robot_Stop.Off();
            OB_Robot_Start.Off();
            OB_Robot_Maintain.On();
            OB_Robot_Enable.On();
            OB_Robot_Reset.On();
            MiddleLayer.SystemF.DelayMs(1000);
            OB_Robot_Reset.Off();
            MiddleLayer.SystemF.DelayMs(1000);
            OB_Robot_Start.On();
            MiddleLayer.SystemF.DelayMs(1000);
            OB_Robot_Start.Off();
            OB_Robot_Program1.On();
            return true;
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = ConnectIX() ? Color.LimeGreen : Color.Red;
        }

        public bool ConnectIX()
        {
            IX_Keyence = new IX_1000(IP, Port);
            if (!IX_Keyence.Status)
            {
                return IX_Keyence.Connect();
            }
            return true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            btnConnect.BackColor = F_Robot.ConnectToRobot() ? Color.LimeGreen : Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IX_Keyence != null)
            {
                if (IX_Keyence.Disconnect())
                {
                    button2.BackColor = Color.Red;
                }
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (F_Robot.Robot.Disconnect())
            {
                btnConnect.BackColor = Color.Transparent;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = IX_Keyence.Trigger();
            if (result != null)
            {
                richTextBox1.Text += result + Environment.NewLine;
            }
            else
            {
                richTextBox1.Text += "Null" + Environment.NewLine;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void B_ReadR_Click(object sender, EventArgs e)
        {
            if (F_Robot.ReflashRobotR())
            {
                int RNum = Convert.ToInt32(N_ReadRAdress.Value);
                T_ReadData.Text = F_Robot.D_ReadRobotR[RNum].ToString();
            }
        }

        private void B_WriteR_Click(object sender, EventArgs e)
        {
            if (F_Robot.WriteToRobotR(Convert.ToInt32(N_WriteRAdress.Value), Convert.ToDouble(N_WriteData.Value)))
            {
                MessageBox.Show("Write Finish");
            }
            else
            {
                MessageBox.Show("Write Failed");
            }
        }

        private void B_WritePR_Click(object sender, EventArgs e)
        {
            double[] d = { 1, 2, 3, 0, 0, 6 };
            d[0] = Convert.ToDouble(N_WritePR1.Value);
            d[1] = Convert.ToDouble(N_WritePR2.Value);
            d[2] = Convert.ToDouble(N_WritePR3.Value);
            d[5] = Convert.ToDouble(N_WritePR6.Value);
            if (F_Robot.WriteToRobotPR(Convert.ToInt16(N_WritePRAdress.Value), d))
            {
                MessageBox.Show("Write Finish");
            }
            else
            {
                MessageBox.Show("Write Failed");
            }
        }

        private void B_ReadPR_Click(object sender, EventArgs e)
        {
            int PRNum = Convert.ToInt16(N_ReadPRAddress.Value);
            F_Robot.ReadFromRobotPR(PRNum);
            T_ReadPRX.Text = F_Robot.D_ReadRobotPR[PRNum, 0].ToString();
            T_ReadPRY.Text = F_Robot.D_ReadRobotPR[PRNum, 1].ToString();
            T_ReadPRZ.Text = F_Robot.D_ReadRobotPR[PRNum, 2].ToString();
            T_ReadPRU.Text = F_Robot.D_ReadRobotPR[PRNum, 5].ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedRatio.Text = trackBar1.Value.ToString();
        }

        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            VisionTaskList.Add(Vision_DetectionTask1);
            VisionTaskList.Add(Vision_DetectionTask2);
            VisionTaskList.Add(Vision_DetectionTask3);
            VisionTaskList.Add(Vision_DetectionTask4);
            CogRecordList.Add(MiddleLayer.RecordF.cogRecord_Robot1);
            CogRecordList.Add(MiddleLayer.RecordF.cogRecord_Robot2);
            CogRecordList.Add(MiddleLayer.RecordF.cogRecord_Robot3);
            CogRecordList.Add(MiddleLayer.RecordF.cogRecord_Robot4);
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            if (ConnectIX())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} failed", false);
            JSDK.Alarm.Show("4200");
            flowChartMessage1.Title = "Connect failed alarm !";
            flowChartMessage1.Content = "4200: Keyence height measure sensor connect failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            if (F_Robot.ConnectToRobot())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3.Text} failed", false);
            JSDK.Alarm.Show("4201");
            flowChartMessage2.Title = "Connect failed alarm !";
            flowChartMessage2.Content = "4201: 4-Axis robot connect failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            RobotStart();
            J_Initial.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial.IsOn(SysPara.Robot_Overtime))
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} overtime", false);
                JSDK.Alarm.Show("4210");
                flowChartMessage8.Title = "Robot move overtime alarm !";
                flowChartMessage8.Content = "4210: 4-axis robot initial overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                if (F_Robot.SetDryRun(1))
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} failed", false);
                JSDK.Alarm.Show("4204");
                flowChartMessage6.Title = "Set robot dryrun mode failed alarm !";
                flowChartMessage6.Content = "4203: Acura write dryrun mode to 4-axis robot failed";
                return FCResultType.CASE2;
            }
            else
            {
                if (F_Robot.SetDryRun(0))
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} failed", false);
                JSDK.Alarm.Show("4205");
                flowChartMessage6.Title = "Set robot dryrun mode failed alarm !";
                flowChartMessage6.Content = "4205: Acura write normal run mode to 4-axis robot failed";
                return FCResultType.CASE2;
            }
        }

        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart7.Text} finish", true);
            return FCResultType.IDLE;
        }

        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            int speed = GetSettingValue("MSet", "RobotSpeedRatio");
            F_Robot.SetSpeed(speed);
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart9.Text} finish", true);
            return FCResultType.NEXT;
        }

        public string time;
        public DateTime TimeStart;
        public String CT;
        int num = 0;
        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.ConveyorBStation1RobotStart1 || B_Start)
            {
                MiddleLayer.SystemF.ShowStatus(3, SystemForm.WorkStatus.Processing);
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                TimeStart = DateTime.Now;
                B_Start = false;
                MiddleLayer.ConveyorF.ConveyorBStation1RobotStart1 = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart10.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            B_PickPCBFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart12.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            if (!B_PickPCBFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart11.Text} finish", true);
                if (!B_DetectionResult)
                {
                    return FCResultType.CASE1;
                }
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart14_FlowRun(object sender, EventArgs e)
        {
            B_PutToConveryPCBFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart14.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart13_FlowRun(object sender, EventArgs e)
        {
            if (!B_PutToConveryPCBFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart13.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart15_FlowRun(object sender, EventArgs e)
        {
            if (B_DetectionResult && B_HeightMeasureResult)
            {
                MiddleLayer.ConveyorF.myRFID3.ResultBool = true;
                MiddleLayer.SystemF.ShowStatus(3, SystemForm.WorkStatus.Pass);
            }
            else
            {
                MiddleLayer.SystemF.ShowStatus(3, SystemForm.WorkStatus.Fail);
                MiddleLayer.ConveyorF.myRFID3.ResultBool = false;
            }
            T_UpdateStatus.Enabled = true;
            MiddleLayer.ConveyorF.ConveyorBStation1RobotComp1 = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart15.Text} finish", true);
            J_AutoRun.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart17_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart17.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart16_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart16.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_1_FlowRun(object sender, EventArgs e)
        {
            if (B_PickPCBFlow)
            {
                detection_Index = 1;
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_2_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(detection_Index))
            {
                if (F_Robot.SetRobotTask(3))
                {
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} failed", false);
                JSDK.Alarm.Show("4212");
                flowChartMessage11.Title = "Set robot taskindex failed alarm !";
                flowChartMessage11.Content = "4212: Acura write task index to 4-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_3_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_3.Text} overtime", false);
                RobotAlarm(flowChartMessage12);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_4_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_4.Text} finish", true);
            return FCResultType.NEXT;
        }

        public ICogImage imageSnap;
        public ICogRecord imageShow;
        public Dictionary<string, object> D_result = new Dictionary<string, object>();
        bool B_Snap = false;
        int I_SnapNGtimes = 0;

        private FCResultType flowChart2_5_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart2_5.Text} finish", true);
                return FCResultType.NEXT;
            }

            RefreshDifferentThreadUI(VisionTaskList[detection_Index - 1], () =>
            {
                if (VisionTaskList[detection_Index - 1].Snap(out imageSnap))
                {
                    B_Snap = true;
                }
                else
                {
                    B_Snap = false;
                }
            });
            if (B_Snap)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_5.Text} finish", true);
                return FCResultType.NEXT;
            }
            I_SnapNGtimes++;
            return FCResultType.CASE2;
        }

        private FCResultType flowChart2_6_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD)
            {
                B_DetectionResult = true;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart2_6.Text} finish", true);
                return FCResultType.NEXT;
            }

            RefreshDifferentThreadUI(VisionTaskList[detection_Index - 1], () =>
            {
                if (VisionTaskList[detection_Index - 1].AutoRun(out D_result, out imageShow))
                {
                    RefreshDifferentThreadUI(CogRecordList[detection_Index - 1], () =>
                     {
                         CogRecordList[detection_Index - 1].Record = imageShow;
                         CogRecordList[detection_Index - 1].AutoFit = true;
                         CogRecordList[detection_Index - 1].Fit();
                     });
                    B_DetectionResult = (bool)D_result["Result"];
                }
                else
                {
                    B_DetectionResult = false;
                }
            });

            if (B_DetectionResult || B_byPassVisionResult)
            {
                B_DetectionResult = true;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            B_DetectionResult = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_6.Text} Vision failed", false);
            JSDK.Alarm.Show("8001");
            flowChartMessage14.Title = "Vision run failed alarm !";
            flowChartMessage14.Content = "8001: 4-axis vision run failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart2_7_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD)
            {
                MiddleLayer.SystemF.DelayMs(1000);
            }
            OB_CCDLight.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_7.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_8_FlowRun(object sender, EventArgs e)
        {
            detection_Index++;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_8.Text} finish", true);
            if (detection_Index > 4)
            {
                detection_Index = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart2_50_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_50.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart2_11_FlowRun(object sender, EventArgs e)
        {
            B_PickPCBFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_11.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_1_FlowRun(object sender, EventArgs e)
        {
            if (B_PutToConveryPCBFlow)
            {
                B_HeightMeasureResult = true;
                HeightArrayList.Clear();
                heightMeasure_Index = 1;
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_2_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(heightMeasure_Index))
            {
                if (F_Robot.SetRobotTask(4))
                {
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_2.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_2.Text} failed", false);
                JSDK.Alarm.Show("4212");
                flowChartMessage15.Title = "Set robot taskindex failed alarm !";
                flowChartMessage15.Content = "4212: Acura write task index to 4-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_3_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                I_NGCount = 1;
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_3.Text} overtime", false);
                //JSDK.Alarm.Show("4213");
                //flowChartMessage16.Title = "Robot move overtime alarm !";
                //flowChartMessage16.Content = "4213: 4-axis robot move overtime";
                RobotAlarm(flowChartMessage16);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_4_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableLaser)
            {
                B_HeightMeasureResult = true;
                MiddleLayer.SystemF.DelayMs(1000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart3_4.Text} finish", true);
                return FCResultType.NEXT;
            }

            string result = IX_Keyence.Trigger();
            if (result != null&&result!="")
            {
                string[] str = result.Split(',');
                double Result = 0;
                if (double.TryParse(str[str.Length - 1],out double result1))
                {
                    Result = result1 / 100;
                }
                else
                {
                    I_NGCount++;
                    if (I_NGCount<4)
                    {
                        return FCResultType.CASE1;
                       
                    }
                    I_NGCount = 1;
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_4.Text} Laser measure NG", false);
                    JSDK.Alarm.Show("8005");
                    flowChartMessage16.Title = "Laser measure NG !";
                    flowChartMessage16.Content = "8005: 4-axis robot laser measure NG";
                    return FCResultType.CASE2;
                }
                //double Result = Convert.ToDouble(str[str.Length - 1]) / 100;
                if (Result >= HeightLimit_min && Result <= HeightLimit_max)
                {
                    B_HeightMeasureResult &= true;
                    HeightArrayList.Add(Result.ToString());
                }
                else
                {
                    I_NGCount++;
                    if (I_NGCount < 4)
                    {
                        return FCResultType.CASE1;

                    }
                    I_NGCount = 1;
                    //B_HeightMeasureResult &= false;
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_4.Text} Laser measure NG", false);
                    JSDK.Alarm.Show("8006");
                    flowChartMessage17.Title = "Laser measure data over limit !";
                    flowChartMessage17.Content = "8006: 4-axis robot laser measure data over limit";
                    return FCResultType.CASE2;
                }
            }
            else
            {
                I_NGCount++;
                if (I_NGCount < 4)
                {
                    return FCResultType.CASE1;

                }
                I_NGCount = 1;
                //B_HeightMeasureResult &= false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_4.Text} Laser measure NG", false);
                JSDK.Alarm.Show("8005");
                flowChartMessage16.Title = "Laser measure NG !";
                flowChartMessage16.Content = "8005: 4-axis robot laser measure NG";
                return FCResultType.CASE2;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_4.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_5_FlowRun(object sender, EventArgs e)
        {
            heightMeasure_Index++;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_5.Text} finish", true);
            if (heightMeasure_Index > 8)
            {
                heightMeasure_Index = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart3_6_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_6.Text} finish", true);
            B_PutToConveryPCBFlow = false;
            if (SysPara.IsDryRun || DisableCCD || DisableLaser)
            {
                return FCResultType.NEXT;
            }
            DataShowUI();

            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_53_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRecipe(1))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} failed", false);
            JSDK.Alarm.Show("4206");
            flowChartMessage4.Title = "Set robot recipe failed alarm !";
            flowChartMessage4.Content = "4206: Acura write recipe to 4-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart28_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotMode(1))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart28.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart28.Text} failed", false);
            JSDK.Alarm.Show("4203");
            flowChartMessage5.Title = "Set robot auto mode failed alarm !";
            flowChartMessage5.Content = "4203: Acura write auto mode to 4-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotTask(1))
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} failed", false);
            JSDK.Alarm.Show("4207");
            flowChartMessage7.Title = "Set robot run task failed alarm !";
            flowChartMessage7.Content = "4207: Acura write initial task to 4-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart29_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_51_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_51.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetSpeed(5))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart33.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart33.Text} failed", false);
            JSDK.Alarm.Show("4214");
            flowChartMessage3.Title = "Set robot speed failed alarm !";
            flowChartMessage3.Content = "4214: Acura write initial speed to 4-axis robot failed";
            return FCResultType.CASE2;
        }

        bool B_Start = false;
        private void button5_Click(object sender, EventArgs e)
        {
            B_Start = true;
        }

        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(1) && F_Robot.SetRobotTask(3))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart35.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart35.Text} failed", false);
                JSDK.Alarm.Show("4212");
                flowChartMessage9.Title = "Set robot taskindex failed alarm !";
                flowChartMessage9.Content = "4212: Acura write task index to 4-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart36.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart36.Text} overtime", false);
                JSDK.Alarm.Show("4213");
                flowChartMessage10.Title = "Robot move overtime alarm !";
                flowChartMessage10.Content = "4213: 4-axis robot move overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {
            if (I_SnapNGtimes >= 3)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart32.Text} CCD Snap NG", false);
                JSDK.Alarm.Show("8000");
                flowChartMessage13.Title = "CCD snap NG alarm !";
                flowChartMessage13.Content = "8000: 4-axis CCD snap failed over 3 times";
                return FCResultType.CASE2;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart3_54_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        public void DataShowUI()
        {
            num++;
            CT = DateTime.Now.Subtract(TimeStart).TotalSeconds.ToString("F2");
            string[] data = new string[6];
            data[0] = num.ToString();
            data[1] = time;
            data[2] = B_DetectionResult ? "OK" : "NG";
            if (B_DetectionResult)
            {
                if (HeightArrayList.Count > 1)
                {
                    for (int i = 0; i < HeightArrayList.Count; i++)
                    {
                        if (i == HeightArrayList.Count - 1)
                        {
                            data[3] += HeightArrayList[i];
                        }
                        else
                            data[3] += HeightArrayList[i] + "  ,";
                        //data[3] += HeightArrayList[HeightArrayList.Count - 1];
                    }
                }
                else if (HeightArrayList.Count == 1)
                {
                    data[3] = HeightArrayList[0];
                }
            }
            data[4] = CT;
            data[5] = B_HeightMeasureResult && B_DetectionResult ? "Pass" : "Fail";

            RefreshDifferentThreadUI(dataGridView1, () =>
            {
                textBox28.Text = Guid.NewGuid().ToString();
                textBox31.Text = CT;
                dataGridView1.Rows.Insert(0, data);
                dataGridView1.Rows[0].DefaultCellStyle.BackColor = B_HeightMeasureResult && B_DetectionResult ? Color.LimeGreen : Color.Red;
                dataGridView1.Refresh();
                dataGridView1.Update();
                if (B_HeightMeasureResult && B_DetectionResult)
                {
                    textBox29.Text = (Convert.ToInt32(textBox29.Text == "" ? "0" : textBox29.Text) + 1).ToString();
                }
                else
                {
                    textBox30.Text = (Convert.ToInt32(textBox30.Text == "" ? "0" : textBox30.Text) + 1).ToString();
                }
                SaveProductData();
            });
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox30.Clear();
            SaveProductData();
            ReadProductData();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox29.Clear();
            SaveProductData();
            ReadProductData();
        }

        INIHelper ProductionIni = new INIHelper($"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\AP_PCBAmodule.ini");
        public void SaveProductData()
        {
            try
            {
                //string ProductionPath = $"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\{this.Text} module.ini";
                //ProductionIni = new INIHelper(ProductionPath);
                ProductionIni.WriteIniFile("ProductData", "OK", textBox29.Text);
                ProductionIni.WriteIniFile("ProductData", "NG", textBox30.Text);
            }
            catch (Exception ex)
            {
            }
        }

        public void ReadProductData()
        {
            try
            {
                //string ProductionPath = $"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\{this.Text} module.ini";
                if (File.Exists($"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\AP_PCBAmodule.ini"))
                {
                    textBox29.Text = ProductionIni.ReadIniFile("ProductData", "OK", "0");
                    textBox30.Text = ProductionIni.ReadIniFile("ProductData", "NG", "0");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private FCResultType flowChart42_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            B_HeightMeasureResult &= false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_50_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.DelayMs(300);
            return FCResultType.NEXT;
        }

        private void T_UpdateStatus_Tick(object sender, EventArgs e)
        {
            //MiddleLayer.SystemF.ShowStatus(3, SystemForm.WorkStatus.WaitProcess);
            T_UpdateStatus.Enabled = false;
        }
    }
}

