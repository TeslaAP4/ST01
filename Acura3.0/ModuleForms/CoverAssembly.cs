using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acura3._0.Classes;
using AcuraLibrary.Forms;
using Cognex.VisionPro;
using JabilSDK;
using JabilSDK.Enums;
using NPFanucRobotDLL;

namespace Acura3._0.ModuleForms
{
    public partial class CoverAssembly : ModuleBaseForm
    {

        public DateTime CTStartTime;
        public DateTime TaktStartTime;
        public CoverAssembly()
        {
            InitializeComponent();
            MessageForm.MuteRaise += MessageForm_MuteRaise;
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);
            ReadProductData();
        }
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

        #endregion
        public override void InitialReset()
        {
            flowChart1.TaskReset();

            B_Feeder_PickFlow = false;
            B_Buffer_PickFlow = false;
            B_Assembly_PlaceFlow = false;
            B_VisionPick_Flow = false;
            B_VisionPlace_Flow = false;
            B_Buffer_PlaceFlow = false;

            feeder_Snap_index = 1;
            feeder_Pick_index = 1;
            workplace_Snap_index = 1;
            I_SnapNGtimes = 0;
            num = 0;
            offSetX_Pick = 0;
            offSetY_Pick = 0;
            offSetA_Pick = 0;
            offSetX_Place = 0;
            offSetY_Place = 0;
            offSetA_Place = 0;
        }

        public override void Initial()
        {
            flowChart1.TaskRun();
        }

        public override void RunReset()
        {
            flowChart5.TaskReset();
            flowChart18.TaskReset();
            flowChart41.TaskReset();
            flowChart78.TaskReset();
            flowChart83.TaskReset();
            flowChart39.TaskReset();
            flowChart40.TaskReset();
            CTStartTime = DateTime.Now;
            TaktStartTime = DateTime.Now;
        }

        public override void Run()
        {
            flowChart5.TaskRun();
            flowChart18.TaskRun();
            flowChart41.TaskRun();
            flowChart78.TaskRun();
            flowChart83.TaskRun();
            flowChart39.TaskRun();
            flowChart40.TaskRun();
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
                JSDK.Alarm.Show("4183");
            }
            if (IB_Robot_BatteryAlarm.IsOn())
            {
                JSDK.Alarm.Show("4184");
            }
            if (IB_Robot_EStop.IsOff())
            {
                JSDK.Alarm.Show("4185");
            }
        }

        public bool CoverDryRun { get => GetSettingValue("PSet", "CoverDryRun"); }

        public bool B_byPassVisionResult { get => GetSettingValue("PSet", "ByPassVisionResult"); }

        public bool DisableCCD { get => GetSettingValue("PSet", "DisableCCD"); }

        public double offSetX_Min { get => GetRecipeValue("RSet", "offSetX_Min"); }

        public double offSetX_Max { get => GetRecipeValue("RSet", "offSetX_Max"); }

        public double offSetY_Min { get => GetRecipeValue("RSet", "offSetY_Min"); }

        public double offSetY_Max { get => GetRecipeValue("RSet", "offSetY_Max"); }

        public double offSetA_Min { get => GetRecipeValue("RSet", "offSetA_Min"); }

        public double offSetA_Max { get => GetRecipeValue("RSet", "offSetA_Max"); }

        /// <summary>
        /// 初始化流程时间实例化
        /// </summary>
        public JTimer J_Initial = new JTimer();

        /// <summary>
        /// 自动流程时间实例化
        /// </summary>
        public JTimer J_AutoRun = new JTimer();

        #region RobotAlarm
        public AlarmClass.AlarmDataClass AlarmContent = new AlarmClass.AlarmDataClass();
        public void RobotAlarm(FlowChartMessage flow)
        {
            try
            {
                double AlarmCode = F_Robot.GetAlarmID();
                if (AlarmCode == 0)
                {
                    JSDK.Alarm.Show("4239");
                    flow.Title = "Robot move overtime alarm !";
                    flow.Content = "4239: 6-axis robot move overtime";
                }
                else
                {
                    JSDK.Alarm.Show((4149 + AlarmCode).ToString());
                    if (flow != null && JSDK.Alarm.IsExistInSummary((4149 + AlarmCode).ToString(), ref AlarmContent))
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

        #region //机器人启动参数

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

        public bool B_Feeder_PickFlow = false;

        public bool B_Buffer_PickFlow = false;

        public bool B_VisionPick_Flow = false;

        public bool B_VisionPlace_Flow = false;

        public bool B_Assembly_PlaceFlow = false;

        public bool B_Buffer_PlaceFlow = false;

        public bool B_Auto_NinePoint_Calibration = false;

        public bool B_VaccumON = false;

        int feeder_Snap_index = 1;
        int feeder_Pick_index = 1;
        int workplace_Snap_index = 1;

        int calibration_Index = 1;

        int num = 0;
        public Fanuc_RobotControl F_Robot = new Fanuc_RobotControl();

        public double offSetX_Pick;

        public double offSetY_Pick;

        public double offSetA_Pick;

        public double offSetX_Place;

        public double offSetY_Place;

        public double offSetA_Place;

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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            btnConnect.BackColor = F_Robot.ConnectToRobot() ? Color.LimeGreen : Color.Red;
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (F_Robot.Robot.Disconnect())
            {
                btnConnect.BackColor = Color.Transparent;
            }
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
            double[] d = { 1, 2, 3, 4, 5, 6 };
            d[0] = Convert.ToDouble(N_WritePR1.Value);
            d[1] = Convert.ToDouble(N_WritePR2.Value);
            d[2] = Convert.ToDouble(N_WritePR3.Value);
            d[3] = Convert.ToDouble(N_WritePR4.Value);
            d[4] = Convert.ToDouble(N_WritePR5.Value);
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
            T_ReadPRA.Text = F_Robot.D_ReadRobotPR[PRNum, 3].ToString();
            T_ReadPRB.Text = F_Robot.D_ReadRobotPR[PRNum, 4].ToString();
            T_ReadPRC.Text = F_Robot.D_ReadRobotPR[PRNum, 5].ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedRatio.Text = trackBar1.Value.ToString();
        }

        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            J_Initial.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            if (F_Robot.ConnectToRobot())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} finish", true);
                return FCResultType.NEXT;
            }
            J_Initial.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} overtime", false);
            JSDK.Alarm.Show("4202");
            flowChartMessage1.Title = "Connect failed alarm !";
            flowChartMessage1.Content = "4202: 6-Axis robot connect failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            RobotStart();
            J_Initial.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart29_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRecipe(1))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart29.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart29.Text} failed", false);
            JSDK.Alarm.Show("4230");
            flowChartMessage3.Title = "Set robot recipe failed alarm !";
            flowChartMessage3.Content = "4230: Acura write recipe to 6-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart67_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotMode(1))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart67.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart67.Text} failed", false);
            JSDK.Alarm.Show("4231");
            flowChartMessage4.Title = "Set robot auto mode failed alarm !";
            flowChartMessage4.Content = "4231: Acura write auto mode to 6-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart77_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun /*|| CoverDryRun*/)
            {
                if (F_Robot.SetDryRun(1))
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart77.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart77.Text} failed", false);
                JSDK.Alarm.Show("4232");
                flowChartMessage5.Title = "Set robot dryrun mode failed alarm !";
                flowChartMessage5.Content = "4232: Acura write dryrun mode to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            else
            {
                if (F_Robot.SetDryRun(0))
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart77.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart77.Text} failed", false);
                JSDK.Alarm.Show("4233");
                flowChartMessage5.Title = "Set robot dryrun mode failed alarm !";
                flowChartMessage5.Content = "4233: Acura write normal run mode to 6-axis robot failed";
                return FCResultType.CASE2;
            }
        }

        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.ReflashRobotR())
            {
                if (F_Robot.d_ReadRobotR[35] == 1)
                {
                    B_VaccumON = true;
                }
                else
                {
                    B_VaccumON = false;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {
            if (B_VaccumON)
            {
                F_Robot.SetTaskIndex(2);
            }
            else
            {
                F_Robot.SetTaskIndex(1);
            }

            if (F_Robot.SetRobotTask(1))
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} failed", false);
            JSDK.Alarm.Show("4236");
            flowChartMessage6.Title = "Set robot run task failed alarm !";
            flowChartMessage6.Content = "4236: Acura write initial task to 6-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart7.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial.IsOn(SysPara.Robot_Overtime))
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart7.Text} alarm", false);
                JSDK.Alarm.Show("4243");
                flowChartMessage7.Title = "Robot move overtime alarm !";
                flowChartMessage7.Content = "4243: 6-axis robot initial overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart75_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart75.Text} finish", true);
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            int speed = GetSettingValue("MSet", "RobotSpeedRatio");
            F_Robot.SetSpeed(speed);
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
            if (B_VaccumON)
            {
                B_VaccumON = false;
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart53_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || CoverDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart53.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_CoverFeeder_Ready.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart53.Text} finish", true);
                return FCResultType.NEXT;
            }
            else
            {
                if (IB_Buffer1_SensorON.IsOn() || IB_Buffer2_SensorON.IsOn())
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart53.Text} finish", true);
                    return FCResultType.CASE1;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart13_FlowRun(object sender, EventArgs e)
        {
            B_Feeder_PickFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart13.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {
            if (!B_Feeder_PickFlow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart10.Text} finish", true);
                if (IB_CoverFeeder_LackMaterial.IsOn() && (IB_Buffer1_SensorON.IsOff() || IB_Buffer2_SensorON.IsOff()))
                {
                    return FCResultType.CASE1;
                }
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        public string time;
        public DateTime TimeStart;
        public String CT;

        private FCResultType flowChart25_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.ConveyorBStation2RobotStart2 || B_Start)
            {
                MiddleLayer.SystemF.ShowStatus(4, SystemForm.WorkStatus.Processing);
                time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                TimeStart = DateTime.Now;
                B_Start = false;
                MiddleLayer.ConveyorF.ConveyorBStation2RobotStart2 = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart25.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart30_FlowRun(object sender, EventArgs e)
        {
            B_Assembly_PlaceFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart30.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            if (!B_Assembly_PlaceFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart12.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.ConveyorF.myRFID4.ResultBool = true;
            T_UpdateStatus.Enabled = true;
            MiddleLayer.SystemF.ShowStatus(4, SystemForm.WorkStatus.Pass);
            MiddleLayer.ConveyorF.ConveyorBStation2RobotComp2 = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart9.Text} finish", true);
            J_AutoRun.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart15_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotTask(2))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart15.Text} finish", true);
                return FCResultType.NEXT;

            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart15.Text} overtime", false);
                JSDK.Alarm.Show("4235");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart18_FlowRun(object sender, EventArgs e)
        {
            if (B_Feeder_PickFlow)
            {
                offSetX_Pick = 0;
                offSetY_Pick = 0;
                offSetA_Pick = 0;
                feeder_Snap_index = 1;
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart18.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(feeder_Pick_index == 1 ? feeder_Snap_index * feeder_Pick_index : feeder_Snap_index + feeder_Pick_index))
            {
                if (F_Robot.SetRobotTask(3))
                {
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} failed", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage10.Title = "Set robot run task failed alarm !";
                flowChartMessage10.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart20_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                if (SysPara.IsDryRun || DisableCCD)
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart20.Text} finish", true);
                    return FCResultType.NEXT;
                }
                if (F_Robot.ReflashRobotR())
                {
                    if (workplace_Snap_index == 1)
                    {
                        RobotX1 = F_Robot.d_ReadRobotR[41];
                        RobotY1 = F_Robot.d_ReadRobotR[42];
                    }
                    else
                    {
                        RobotX2 = F_Robot.d_ReadRobotR[41];
                        RobotY2 = F_Robot.d_ReadRobotR[42];
                    }
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart20.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart20.Text} overtime", false);
                //JSDK.Alarm.Show("4239");
                //flowChartMessage11.Title = "Robot move overtime alarm !";
                //flowChartMessage11.Content = "4239: 6-axis robot move overtime";
                RobotAlarm(flowChartMessage11);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart21_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart21.Text} finish", true);
            return FCResultType.NEXT;
        }

        public ICogImage imageSnap;
        public ICogRecord imageShow;
        public Dictionary<string, object> D_result = new Dictionary<string, object>();

        bool B_Snap = false;
        int I_SnapNGtimes = 0;
        bool B_VisionOK = false;
        bool B_Feeder_TheSecondTray_VisonNG = false;

        private FCResultType flowChart22_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD || CoverDryRun)
            {
                MiddleLayer.SystemF.DelayMs(500);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart22.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (feeder_Snap_index == 1 || feeder_Snap_index == 3)
            {
                RefreshDifferentThreadUI(Vision_CoverPickTask1, () =>
                {
                    if (Vision_CoverPickTask1.Snap(out imageSnap))
                    {
                        B_Snap = true;
                    }
                    else
                    {
                        B_Snap = false;
                    }
                });
            }
            else
            {
                RefreshDifferentThreadUI(Vision_CoverPickTask2, () =>
                {
                    if (Vision_CoverPickTask2.Snap(out imageSnap))
                    {
                        B_Snap = true;
                    }
                    else
                    {
                        B_Snap = false;
                    }
                });
            }
            if (B_Snap)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart22.Text} finish", true);
                return FCResultType.NEXT;
            }
            I_SnapNGtimes++;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart22.Text} finish", true);
            return FCResultType.CASE2;
        }

        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            if (I_SnapNGtimes >= 3)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart36.Text} CCD Snap NG", false);
                JSDK.Alarm.Show("8002");
                flowChartMessage22.Title = "CCD snap NG alarm !";
                flowChartMessage22.Content = "8002: 6-axis Robot CCD snap failed over 3 times";
                return FCResultType.CASE2;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart23_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart23.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart24_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD || CoverDryRun)
            {
                MiddleLayer.SystemF.DelayMs(400);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart24.Text} finish", true);
                feeder_Snap_index++;
                if (feeder_Snap_index > 2)
                {
                    feeder_Snap_index = 1;
                    return FCResultType.NEXT;
                }
                return FCResultType.CASE1;
            }
            if (feeder_Pick_index == 1)
            {
                RefreshDifferentThreadUI(Vision_CoverPickTask1, () =>
                {
                    if (Vision_CoverPickTask1.AutoRun(out D_result, out imageShow))
                    {
                        RefreshDifferentThreadUI(MiddleLayer.RecordF.cogRecord_FeederPick, () =>
                        {
                            MiddleLayer.RecordF.cogRecord_FeederPick.Record = imageShow;
                            MiddleLayer.RecordF.cogRecord_FeederPick.AutoFit = true;
                            MiddleLayer.RecordF.cogRecord_FeederPick.Fit();
                        });
                        B_VisionOK = true;
                    }
                    else
                    {
                        B_VisionOK = false;
                    }
                });
            }
            else
            {
                RefreshDifferentThreadUI(Vision_CoverPickTask2, () =>
                {
                    Vision_CoverPickTask2.SetInput("X1", Convert.ToDouble(D_result["X"]) + RobotX2 - RobotX1);
                    Vision_CoverPickTask2.SetInput("Y1", Convert.ToDouble(D_result["Y"]) + RobotY2 - RobotY1);
                    if (Vision_CoverPickTask2.RunTopCamera(out PickMark, out imageShow))
                    {
                        offSetX_Pick = Math.Round(PickMark.x, 3);
                        offSetY_Pick = Math.Round(PickMark.y, 3);
                        offSetA_Pick = Math.Round(PickMark.r, 3);
                        RefreshDifferentThreadUI(MiddleLayer.RecordF.cogRecord_FeederPick, () =>
                        {
                            MiddleLayer.RecordF.cogRecord_FeederPick.Record = imageShow;
                            MiddleLayer.RecordF.cogRecord_FeederPick.AutoFit = true;
                            MiddleLayer.RecordF.cogRecord_FeederPick.Fit();
                        });
                        B_VisionOK = true;
                    }
                    else
                    {
                        B_VisionOK = false;
                    }
                });
            }
            if (B_VisionOK || B_byPassVisionResult)
            {
                if (CheckVision_Pick_Limit() || B_byPassVisionResult)
                {
                    feeder_Snap_index++;
                    if (feeder_Snap_index > 2)
                    {
                        feeder_Snap_index = 1;
                        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} finish", true);
                        return FCResultType.NEXT;
                    }
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} finish", true);
                    return FCResultType.CASE1;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} offset out range", false);
                JSDK.Alarm.Show("8004");
                flowChartMessage25.Title = "offset out range alarm !";
                flowChartMessage25.Content = "8004: 6-axis vision offset out range";
                return FCResultType.CASE2;
            }
            else
            {
                feeder_Snap_index = 1;
                feeder_Pick_index++;
                if (feeder_Pick_index > 2)
                {
                    feeder_Pick_index = 1;
                    B_Feeder_TheSecondTray_VisonNG = true;
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} finish", true);
                return FCResultType.CASE3;
            }
        }

        public bool CheckVision_Pick_Limit()
        {
            bool a = offSetX_Pick >= offSetX_Min && offSetX_Pick <= offSetX_Max ? true : false;
            bool b = offSetY_Pick >= offSetY_Min && offSetY_Pick <= offSetY_Max ? true : false;
            bool c = offSetA_Pick >= offSetA_Min && offSetA_Pick <= offSetA_Max ? true : false;
            return a && b && c ? true : false;
        }

        public bool CheckVision_Place_Limit()
        {
            bool a = offSetX_Place >= offSetX_Min && offSetX_Place <= offSetX_Max ? true : false;
            bool b = offSetY_Place >= offSetY_Min && offSetY_Place <= offSetY_Max ? true : false;
            bool c = offSetA_Place >= offSetA_Min && offSetA_Place <= offSetA_Max ? true : false;
            return a && b && c ? true : false;
        }

        private FCResultType flowChart38_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart38.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart26_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || B_byPassVisionResult || DisableCCD)
            {
                offSetX_Pick = 0;
                offSetY_Pick = 0;
                offSetA_Pick = 0;
            }

            if (F_Robot.SetTaskIndex(feeder_Pick_index))
            {
                if (F_Robot.SetVisionResultX(offSetX_Pick) && F_Robot.SetVisionResultY(offSetY_Pick) && F_Robot.SetVisionResultA(offSetA_Pick))
                {
                    if (F_Robot.SetRobotTask(/*4*/6))
                    {
                        J_AutoRun.Restart();
                        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart26.Text} finish", true);
                        return FCResultType.NEXT;
                    }
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart26.Text} failed", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage14.Title = "Set robot run task failed alarm !";
                flowChartMessage14.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart28_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart28.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart28.Text} overtime", false);
                //JSDK.Alarm.Show("4239");
                //flowChartMessage15.Title = "Robot move overtime alarm !";
                //flowChartMessage15.Content = "4239: 6-axis robot move overtime";
                RobotAlarm(flowChartMessage15);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            feeder_Pick_index++;
            if (feeder_Pick_index > 2)
            {
                feeder_Pick_index = 1;
            }
            B_Feeder_PickFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart35.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart37_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart37.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart39_FlowRun(object sender, EventArgs e)
        {
            if (B_Buffer_PickFlow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart39.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart41_FlowRun(object sender, EventArgs e)
        {
            if (B_Assembly_PlaceFlow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart41.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart42_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(workplace_Snap_index))
            {
                if (F_Robot.SetRobotTask(7))
                {
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} failed", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage12.Title = "Set robot run task failed alarm !";
                flowChartMessage12.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                if (SysPara.IsDryRun || DisableCCD)
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} finish", true);
                    return FCResultType.NEXT;
                }
                if (F_Robot.ReflashRobotR())
                {
                    if (workplace_Snap_index == 1)
                    {
                        RobotX1 = F_Robot.d_ReadRobotR[41];
                        RobotY1 = F_Robot.d_ReadRobotR[42];
                    }
                    else
                    {
                        RobotX2 = F_Robot.d_ReadRobotR[41];
                        RobotY2 = F_Robot.d_ReadRobotR[42];
                    }
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} overtime", false);
                JSDK.Alarm.Show("4239");
                flowChartMessage13.Title = "Robot move overtime alarm !";
                flowChartMessage13.Content = "4239: 6-axis robot move overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart44_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart44.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart45_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD)
            {
                MiddleLayer.SystemF.DelayMs(400);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart45.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (workplace_Snap_index == 1)
            {
                RefreshDifferentThreadUI(Vision_CoverPlaceTask1, () =>
                {
                    if (Vision_CoverPlaceTask1.Snap(out imageSnap))
                    {
                        B_Snap = true;
                    }
                    else
                    {
                        B_Snap = false;
                    }
                });
            }
            else
            {
                RefreshDifferentThreadUI(Vision_CoverPlaceTask2, () =>
                {
                    if (Vision_CoverPlaceTask2.Snap(out imageSnap))
                    {
                        B_Snap = true;
                    }
                    else
                    {
                        B_Snap = false;
                    }
                });
            }
            if (B_Snap)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart45.Text} finish", true);
                return FCResultType.NEXT;
            }
            I_SnapNGtimes++;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart45.Text} finish", true);
            return FCResultType.CASE2;
        }

        private FCResultType flowChart47_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart47.Text} finish", true);
            return FCResultType.NEXT;
        }

        VppComp.Point3D PickMark = null;
        VppComp.Point3D PlaceMark = null;
        Dictionary<string,object> placemake1=new Dictionary<string, object>();
        VppComp.Point3D placemake2 = new VppComp.Point3D();
        object place1 = null;
        object place2 = null;
 
        private FCResultType flowChart48_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || DisableCCD)
            {
                workplace_Snap_index++;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart48.Text} finish", true);
                if (workplace_Snap_index > 2)
                {
                    workplace_Snap_index = 1;
                    return FCResultType.NEXT;
                }
                return FCResultType.CASE1;
            }


            CogRecordDisplay crd = MiddleLayer.RecordF.cogRecord_Place;
            if (workplace_Snap_index == 1)
            {
                RefreshDifferentThreadUI(Vision_CoverPlaceTask1, () =>
                {
                    
                    if (Vision_CoverPlaceTask1.Run(out place1, ref crd))
                    {
                        placemake1 =  (Dictionary<string, object>)place1  ;
                        RefreshDifferentThreadUI(MiddleLayer.RecordF.cogRecord_Place, () =>
                        {
                            MiddleLayer.RecordF.cogRecord_Place.Record = imageShow;
                            MiddleLayer.RecordF.cogRecord_Place.AutoFit = true;
                            MiddleLayer.RecordF.cogRecord_Place.Fit();
                        });
                        B_VisionOK = true;
                    }
                    else
                    {
                        B_VisionOK = false;
                    }
                });
                MiddleLayer.RecordF.cogRecord_Place = crd;
            }
            else
            {
                RefreshDifferentThreadUI(Vision_CoverPlaceTask2, () =>
                {

                    
                    Vision_CoverPlaceTask2.SetInput("X1", (double)placemake1["X"] + RobotX2 - RobotX1);
                    Vision_CoverPlaceTask2.SetInput("Y1", (double)placemake1["Y"] + RobotY2 - RobotY1);
                    if (Vision_CoverPlaceTask2.Run(out place2, ref crd))
                    {
                        placemake2 =(VppComp.Point3D) place2;
                        offSetX_Place = Math.Round(placemake2.x, 2);
                        offSetY_Place = Math.Round(placemake2.y, 2);
                        offSetA_Place = Math.Round(placemake2.r, 2);
                        RefreshDifferentThreadUI(MiddleLayer.RecordF.cogRecord_Place, () =>
                        {
                            MiddleLayer.RecordF.cogRecord_Place.Record = imageShow;
                            MiddleLayer.RecordF.cogRecord_Place.AutoFit = true;
                            MiddleLayer.RecordF.cogRecord_Place.Fit();
                        });
                        B_VisionOK = true;
                    }
                    else
                    {
                        B_VisionOK = false;
                    }
                   
                });
                MiddleLayer.RecordF.cogRecord_Place = crd;
            }
            if (B_VisionOK || B_byPassVisionResult)
            {
                if (CheckVision_Place_Limit() || B_byPassVisionResult)
                {
                    workplace_Snap_index++;
                    if (workplace_Snap_index > 2)
                    {
                        workplace_Snap_index = 1;
                        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart48.Text} finish", true);
                        return FCResultType.NEXT;
                    }
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart48.Text} finish", true);
                    return FCResultType.CASE1;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart48.Text} offset out range ", false);
                JSDK.Alarm.Show("8004");
                flowChartMessage26.Title = "offset out range alarm !";
                flowChartMessage26.Content = "8004: 6-axis vision offset out range";
                return FCResultType.CASE2;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart48.Text} Vision vpp run error ", false);
            JSDK.Alarm.Show("8003");
            flowChartMessage24.Title = "Vpp error !";
            flowChartMessage24.Content = "8003: 6-axis place cover vision run error";
            return FCResultType.CASE3;
        }

        private FCResultType flowChart50_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun || B_byPassVisionResult || DisableCCD)
            {
                offSetX_Place = 0;
                offSetY_Place = 0;
                offSetA_Place = 0;
            }
            if (F_Robot.SetTaskIndex(1))
            {
                if (F_Robot.SetVisionResultX(offSetX_Place) && F_Robot.SetVisionResultY(offSetY_Place) && F_Robot.SetVisionResultA(offSetA_Place))
                {
                    if (F_Robot.SetRobotTask(8))
                    {
                        J_AutoRun.Restart();
                        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart50.Text} finish", true);
                        return FCResultType.NEXT;
                    }
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart50.Text} overtime", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage18.Title = "Set robot run task failed alarm !";
                flowChartMessage18.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart52_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart52.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart52.Text} overtime", false);
                //JSDK.Alarm.Show("4239");
                //flowChartMessage19.Title = "Robot move overtime alarm !";
                //flowChartMessage19.Content = "4239: 6-axis robot move overtime";
                RobotAlarm(flowChartMessage19);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart58_FlowRun(object sender, EventArgs e)
        {
            B_Assembly_PlaceFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart58.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart59_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart59.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart49_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart49.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart11.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart11.Text} overtime", false);
                JSDK.Alarm.Show("4239");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart32.Text} finish", true);
            if (SysPara.IsDryRun || DisableCCD || B_byPassVisionResult)
            {
                return FCResultType.NEXT;
            }
            DataShowUI();
            return FCResultType.NEXT;
        }

        public void DataShowUI()
        {
            num++;
            CT = DateTime.Now.Subtract(TimeStart).TotalSeconds.ToString("F2");
            string[] data = new string[6];
            data[0] = num.ToString();
            data[1] = time;
            data[2] = $"X：{offSetX_Pick} , Y：{offSetY_Pick} , A：{offSetA_Pick}";
            data[3] = $"X：{offSetX_Place} , Y：{offSetY_Place} , A：{offSetA_Place}";
            data[4] = CT;
            data[5] = B_VisionOK ? "Pass" : "Fail";

            RefreshDifferentThreadUI(dataGridView1, () =>
            {
                textBox28.Text = Guid.NewGuid().ToString();
                textBox31.Text = CT;
                dataGridView1.Rows.Insert(0, data);
                dataGridView1.Rows[0].DefaultCellStyle.BackColor = B_VisionOK ? Color.LimeGreen : Color.Red;
                dataGridView1.Refresh();
                dataGridView1.Update();
                //if (B_VisionOK)
                //{
                //    textBox29.Text = (Convert.ToInt32(textBox29.Text) + 1).ToString();
                //}
                //else
                //{
                //    textBox30.Text = (Convert.ToInt32(textBox30.Text) + 1).ToString();
                //}
                SaveProductData();
            });
        }

        INIHelper ProductionIni;
        public void SaveProductData()
        {
            try
            {
                string ProductionPath = $"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\{this.Text} module.ini";
                ProductionIni = new INIHelper(ProductionPath);
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
                string ProductionPath = $"{System.IO.Directory.GetCurrentDirectory()}\\ProductionData\\{this.Text} module.ini";
                if (File.Exists(ProductionPath))
                {
                    textBox29.Text = ProductionIni.ReadIniFile("ProductData", "OK", textBox29.Text);
                    textBox30.Text = ProductionIni.ReadIniFile("ProductData", "NG", textBox30.Text);
                }
            }
            catch (Exception ex)
            {

            }
        }

        bool bPick = false;
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure to start auto 9-points calibration ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DialogResult reult1 = MessageBox.Show("Check robot flange have no product ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult1 == DialogResult.No)
            {
                return;
            }
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    bPick = true;
                    break;
                case 1:
                    bPick = false;
                    break;
            }
            calibration_Index = 1;
            B_Auto_NinePoint_Calibration = true;
            flowChart101.TaskReset();
            Task.Run(() =>
            {
                while (B_Auto_NinePoint_Calibration)
                {
                    flowChart101.TaskRun();
                }
            });
            button4.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            calibration_Index = 1;
            B_Auto_NinePoint_Calibration = false;
            button4.Enabled = true;
        }

        private FCResultType flowChart101_FlowRun(object sender, EventArgs e)
        {
            calibration_Index = 1;
            return FCResultType.NEXT;
        }

        private FCResultType flowChart16_FlowRun(object sender, EventArgs e)
        {
            F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            if (F_Robot.ConnectToRobot())
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart16.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial.IsOn(SysPara.IO_OverTime))
            {
                J_Initial.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart16.Text} overtime", false);
                JSDK.Alarm.Show("4202");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart17_FlowRun(object sender, EventArgs e)
        {
            if (RobotStart())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart27_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRecipe(1))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart76_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(1))
            {
                if (F_Robot.SetRobotMode(1))
                {
                    F_Robot.SetSpeed(5);
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotTask(1))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart34_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart51_FlowRun(object sender, EventArgs e)
        {
            if (bPick)
            {
                if (F_Robot.WriteToRobotR(2, calibration_Index) && F_Robot.WriteToRobotR(1, 9))
                {
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            else
            {
                if (F_Robot.WriteToRobotR(2, calibration_Index) && F_Robot.WriteToRobotR(1, 10))
                {
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
        }

        private FCResultType flowChart54_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart55_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.On();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart56_FlowRun(object sender, EventArgs e)
        {
            //


            return FCResultType.NEXT;
        }

        private FCResultType flowChart57_FlowRun(object sender, EventArgs e)
        {
            OB_CCDLight.Off();
            return FCResultType.NEXT;
        }

        Cognex.VisionPro.ICogRecord image;
        bool result = false;

        private FCResultType flowChart60_FlowRun(object sender, EventArgs e)
        {
            if (bPick)
            {
                RefreshDifferentThreadUI(Vision_Calibration, () =>
                {
                    if (Vision_Calibration.SetCalibPoint(Vision_CoverPickTask2.CameraSN, Vision_CoverPickTask2.WokId, calibration_Index - 1, RobotX, RobotY, out image))
                    {
                        RefreshDifferentThreadUI(cog_Auto9PointCalibration, () =>
                        {
                            cog_Auto9PointCalibration.Record = image;
                            cog_Auto9PointCalibration.AutoFit = true;
                            cog_Auto9PointCalibration.Fit();
                        });
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                });
            }
            else
            {
                RefreshDifferentThreadUI(Vision_Calibration, () =>
                {
                    if (Vision_Calibration.SetCalibPoint(Vision_CoverPlaceTask2.CameraSN, Vision_CoverPlaceTask2.WokId, calibration_Index - 1, RobotX, RobotY, out image))
                    {
                        RefreshDifferentThreadUI(cog_Auto9PointCalibration, () =>
                        {
                            cog_Auto9PointCalibration.Record = image;
                            cog_Auto9PointCalibration.AutoFit = true;
                            cog_Auto9PointCalibration.Fit();
                        });
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                });
            }
            if (result)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
            //MiddleLayer.SystemF.DelayMs(1000);
            //return FCResultType.NEXT;
        }

        private FCResultType flowChart62_FlowRun(object sender, EventArgs e)
        {
            calibration_Index++;
            if (calibration_Index >= 10)
            {
                calibration_Index = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart63_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.CASE1;
        }

        private FCResultType flowChart64_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetRobotTask(2))
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart65_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart66_FlowRun(object sender, EventArgs e)
        {
            B_Auto_NinePoint_Calibration = false;
            button4.Enabled = true;
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetSpeed(5))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4.Text} failed", false);
            JSDK.Alarm.Show("4241");
            flowChartMessage2.Title = "Set robot speed failed alarm !";
            flowChartMessage2.Content = "4241: Acura write initial speed to 6-axis robot failed";
            return FCResultType.CASE2;
        }

        bool B_Start = false;
        private void button5_Click(object sender, EventArgs e)
        {
            F_Robot.RobotContinue(OB_Robot_Maintain, OB_Robot_Start);
        }

        bool B_Feeder = false;
        private void button1_Click(object sender, EventArgs e)
        {
            B_Feeder = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            F_Robot.RobotPause(OB_Robot_Maintain);
        }

        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            B_VisionPick_Flow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart31.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart61_FlowRun(object sender, EventArgs e)
        {
            if (!B_VisionPick_Flow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart61.Text} finish", true);
                if (B_Feeder_TheSecondTray_VisonNG)
                {
                    B_Feeder_TheSecondTray_VisonNG = false;
                    return FCResultType.CASE3;
                }
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart73_FlowRun(object sender, EventArgs e)
        {
            B_VisionPlace_Flow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart73.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart74_FlowRun(object sender, EventArgs e)
        {
            if (!B_VisionPlace_Flow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart74.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart78_FlowRun(object sender, EventArgs e)
        {
            if (B_VisionPick_Flow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart78.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart81_FlowRun(object sender, EventArgs e)
        {
            B_VisionPick_Flow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart81.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart82_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart82.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart83_FlowRun(object sender, EventArgs e)
        {
            if (B_VisionPlace_Flow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart83.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart84_FlowRun(object sender, EventArgs e)
        {
            B_VisionPlace_Flow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart84.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart85_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart85.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart79_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart79.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart80_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart80.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart68_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart68.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart14_FlowRun(object sender, EventArgs e)
        {
            B_Buffer_PickFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart14.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart70_FlowRun(object sender, EventArgs e)
        {
            if (!B_Buffer_PickFlow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart70.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart86_FlowRun(object sender, EventArgs e)
        {
            int i = IB_Buffer1_SensorON.IsOn() ? 1 : 2;
            if (F_Robot.SetTaskIndex(i))
            {
                if (F_Robot.SetRobotTask(6))
                {
                    //if (F_Robot.GetSafeMove() == eRobotSafeStatus.SafeToMove)
                    //{
                    //    J_AutoRun.Restart();
                    //    if (F_Robot.SetSafeMove(1))
                    //    {
                    //        J_AutoRun.Restart();
                    //        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart26.Text} finish", true);
                    //        return FCResultType.NEXT;
                    //    }
                    //    else
                    //    {
                    //        MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart26.Text} failed", false);
                    //        JSDK.Alarm.Show("4234");
                    //        return FCResultType.IDLE;
                    //    }
                    //}
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart86.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart86.Text} failed", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage16.Title = "Set robot run task failed alarm !";
                flowChartMessage16.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart87_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart87.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart87.Text} overtime", false);
                JSDK.Alarm.Show("4239");
                flowChartMessage17.Title = "Robot move overtime alarm !";
                flowChartMessage17.Content = "4239: 6-axis robot move overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart88_FlowRun(object sender, EventArgs e)
        {
            B_Buffer_PickFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart88.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart89_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart89.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart71_FlowRun(object sender, EventArgs e)
        {
            B_Buffer_PlaceFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart71.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart69_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart69.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart72_FlowRun(object sender, EventArgs e)
        {
            if (!B_Buffer_PlaceFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart72.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart40_FlowRun(object sender, EventArgs e)
        {
            if (B_Buffer_PlaceFlow)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart40.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart90_FlowRun(object sender, EventArgs e)
        {
            int i = IB_Buffer1_SensorON.IsOff() ? 1 : 2;
            if (F_Robot.SetTaskIndex(i))
            {
                if (F_Robot.SetRobotTask(5))
                {
                    J_AutoRun.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart90.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            if (J_AutoRun.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart90.Text} failed", false);
                JSDK.Alarm.Show("4238");
                flowChartMessage20.Title = "Set robot run task failed alarm !";
                flowChartMessage20.Content = "4238: Acura write task index to 6-axis robot failed";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart91_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart91.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart91.Text} overtime", false);
                JSDK.Alarm.Show("4239");
                flowChartMessage21.Title = "Robot move overtime alarm !";
                flowChartMessage21.Content = "4239: 6-axis robot move overtime";
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart92_FlowRun(object sender, EventArgs e)
        {
            B_Buffer_PlaceFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart92.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart93_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart93.Text} finish", true);
            return FCResultType.NEXT;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            B_Start = true;
        }

        private FCResultType flowChart94_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.SetTaskIndex(1) && F_Robot.SetRobotTask(7))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart94.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart94.Text} failed", false);
            JSDK.Alarm.Show("4238");
            flowChartMessage8.Title = "Set robot run task index failed alarm !";
            flowChartMessage8.Content = "4238: Acura write task index to 6-axis robot failed";
            return FCResultType.CASE2;
        }

        private FCResultType flowChart95_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.GetCurrentTaskState() == eRobotState.Done)
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart95.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun.IsOn(SysPara.Robot_Overtime))
            {
                J_AutoRun.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart95.Text} overtime", false);
                //JSDK.Alarm.Show("4239");
                //flowChartMessage9.Title = "Robot move overtime alarm !";
                //flowChartMessage9.Content = "4239: 6-axis robot move overtime";
                RobotAlarm(flowChartMessage9);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        double RobotX, RobotY, RobotX1, RobotY1, RobotX2, RobotY2;

        private FCResultType flowChart97_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart97.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart98_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        bool B_CoverAssemblyFlow = false;
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Sure to Start CoverAssembly Flow ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            B_CoverAssemblyFlow = true;
            feeder_Snap_index = 1;
            feeder_Pick_index = 1;
            workplace_Snap_index = 1;
            I_SnapNGtimes = 0;
            num = 0;
            offSetX_Pick = 0;
            offSetY_Pick = 0;
            offSetA_Pick = 0;
            offSetX_Place = 0;
            offSetY_Place = 0;
            offSetA_Place = 0;
            //F_Robot.RobotIP = GetRecipeValue("RSet", "RobotIP");
            //F_Robot.ConnectToRobot();
            RobotStart();
            F_Robot.SetSpeed(10);
            flowChart5.TaskReset();
            flowChart18.TaskReset();
            flowChart41.TaskReset();
            flowChart78.TaskReset();
            flowChart83.TaskReset();
            flowChart39.TaskReset();
            flowChart40.TaskReset();
            Task.Factory.StartNew(() =>
            {
                while (B_CoverAssemblyFlow)
                {
                    flowChart5.TaskRun();
                    flowChart18.TaskRun();
                    flowChart41.TaskRun();
                    flowChart78.TaskRun();
                    flowChart83.TaskRun();
                    flowChart39.TaskRun();
                    flowChart40.TaskRun();
                }
            });
            button3.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            B_CoverAssemblyFlow = false;
            button3.Enabled = true;
        }

        private void T_UpdateStatus_Tick(object sender, EventArgs e)
        {
            //MiddleLayer.SystemF.ShowStatus(4, SystemForm.WorkStatus.WaitProcess);
            T_UpdateStatus.Enabled = false;
        }

        private FCResultType flowChart99_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart99.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart100_FlowRun(object sender, EventArgs e)
        {
            OB_CoverFeeder_PickFinish.On();
            OB_CoverFeeder_SafeHeight.On();
            MiddleLayer.SystemF.DelayMs(200);
            OB_CoverFeeder_PickFinish.Off();
            OB_CoverFeeder_SafeHeight.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart100.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart102_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart46_FlowRun(object sender, EventArgs e)
        {
            if (I_SnapNGtimes >= 3)
            {
                I_SnapNGtimes = 0;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart46.Text} CCD Snap NG", false);
                JSDK.Alarm.Show("8002");
                flowChartMessage23.Title = "CCD snap NG alarm !";
                flowChartMessage23.Content = "8002: 6-axis Robot CCD snap failed over 3 times";
                return FCResultType.CASE2;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart96_FlowRun(object sender, EventArgs e)
        {
            if (F_Robot.ReflashRobotR())
            {
                F_Robot.SetSpeed(10);
                RobotX = F_Robot.d_ReadRobotR[41];
                RobotY = F_Robot.d_ReadRobotR[42];
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
    }
}
