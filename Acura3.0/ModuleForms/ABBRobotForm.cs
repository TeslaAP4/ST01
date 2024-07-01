using ABBRobot;
using ABBRobot.Extern;
using Acura3._0.FunctionForms;
using Acura3._0.Properties;
using AcuraLibrary.Forms;
using JabilSDK;
using JabilSDK.Enums;
using JabilSDK.UserControlLib;
using SASDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ABBRobot.ABB_RobotControl;
using static Acura3._0.FunctionForms.LogForm;

namespace Acura3._0.ModuleForms
{
    public partial class ABBRobotForm : ModuleBaseForm
    {
        #region Form & Init
        public enum RobotAxis { All, XY, X, Y, Z, W }
        public enum RobotMove { JMove, LMove }
        public enum ToolChangeCheck { IsHead, IsSensor }
        private enum WorkPost
        {
            WP1 = 0,
            WP2,
            WP3
        }

        #region UI Init
        public enum ConveyorPost { WP1, WP2, WP3 }

        private class CrrCvyUIStatus
        {
            public bool isPresnt = false;
            public bool isMoving = false;
            public string staText = null;
        }
        private List<CrrCvyUIStatus> _CrrCvyUI = new List<CrrCvyUIStatus> { new CrrCvyUIStatus(), new CrrCvyUIStatus(), new CrrCvyUIStatus() };
        private string[] _CrrTurretHeadUI = new string[4] { "none", "none", "none", "none" };
        private int IsCurrentUPH = -1;
        private int CurrUPH_UI = 0;
        #endregion

        private int StdTime = 1;
        private bool isEndLotMode = false;
        public ABB_RobotControl ABB_Robot = new ABB_RobotControl();
        private DataGridView dgv_name;
        private string dgv_DataMember;
        private List<RBSeqItem> seqItem = new List<RBSeqItem>();
        private List<RBSeqItem> Feeder1PickSeqItem = new List<RBSeqItem>();
        private List<RBSeqItem> Feeder2PickSeqItem = new List<RBSeqItem>();
        private RBSeqItem currentSeq = new RBSeqItem();
        private RBSeqItem pickcurrentSeq = new RBSeqItem();
        private TurretHead turretHead = new TurretHead();
        public bool ManualArmChange = false;
        private int EnterIndex = -1;
        private double FeederDistance = 0.00;
        private bool MoveRobotFeeder = false;
        private bool extendDone = false;
        private bool throwDone = false;
        private bool extendPickDone = false;
        private bool pickDone = false;
        private int ToolChangesThrowRunIndex = 3;
        private int ToolThrowCheckSensorIndex = 3;
        private int ToolPickCheckSensorIndex = 3;
        private int ToolChangesPickRunIndex = 0;
        private int PlaceNGIndex = -1;
        private int RejectCount = 0;
        private int FailedCount = 0;
        private int PickFailedCount_PerTray = 0;
        private int _PrevHour = -1;
        private string CurrentToolBuffer = "";
        private JTimer TMRobotMove = new JTimer();
        private JTimer RunTMRobot = new JTimer();
        private JTimer RunTMVision = new JTimer();
        private double feederZ = 0.0;
        private double PalletZ = 0.0;
        private Arm feederArm = ABB_RobotControl.Arm.R;
        private RobotCollisionZone _RobotSafeZone;
        private List<RobotPosData> lRobotPost = new List<RobotPosData>();
        private eFeederType eSelectedFeederType = eFeederType.None;
        private int iSimulateBarcode = 10;
        private bool inPos = false;
        private string ImageFileName = "";
        private System.Timers.Timer uiRefreshThread = new System.Timers.Timer();
        private int[] UI_Counter = new int[5] { 0, 0, 0, 0, 0 };

        private bool PlaceEnd_RobotGotoSafe_Latch = false;
        private Task _houseKeeping;

        public bool bRobotFinishflow = false;
        public ABBRobotForm()
        {
            InitializeComponent();
            //bool IsConnect = ABB_Robot.Connect("System1");
            bool IsConnect = ABB_Robot.Connect("910-507361");
            //bool IsConnect = ABB_Robot.Connect("IRB910SC");
            if (IsConnect)
            {
                ABB_Robot.OnErrorReceived += OnErrorReceive;
            }
            SetDoubleBuffer(dgv_Seq);
            SetDoubleBuffer(dgv_Pick1Seq);
            SetDoubleBuffer(dgv_Pick2Seq);
            SetDoubleBuffer(gB_RobotArm);
            SetDoubleBuffer(gB_RobotMode);
            PopulateStackMenu(dgv_Seq);
            PopulateStackMenu(dgv_Pick1Seq);
            PopulateStackMenu(dgv_Pick2Seq);

            for (int x = 0; x < Handshake.Feeder.Length; x++)
            {
                Handshake.Feeder[x] = new _Handshake();
            }

            cbJogMode.SelectedItem = "JMove";
            cbJobArm.SelectedItem = "Right";
            cbChangeHead.SelectedItem = "Head 1";
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;

            //comment out this if using purge flow
            tcRecipeEditor.TabPages.Remove(tpRecipePurge);

            uiRefreshThread.Interval = 100;
            uiRefreshThread.Elapsed += uiRefreshThread_Tick;
            uiRefreshThread.Start();
        }

        ~ABBRobotForm()
        {
            uiRefreshThread.Stop();
            uiRefreshThread.Dispose();
            uiRefreshThread = null;
        }

        private void CoreEngine_FlowChartEnter(object sender, FlowLogArgs e)
        {
            if (e.Module != this.Name) return;
            if (sender is FlowChart)
            {
                FlowChart _flow = sender as FlowChart;
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Robot,[RobotCore]" + e.Name);
            }
        }

        private void FlowChartMessage_PauseRaise(object sender, EventArgs e)
        {
            MiddleLayer.StopRun();
        }

        private void FlowChartMessage_ResetRaise(object sender, EventArgs e)
        {
           RunTMRobot.Restart();
        }
        #endregion

        #region Override
        public override void StartUp()
        {
            switch (GetSettingValue("MSet", "lblTCurrent"))
            {
                case "None":
                    for (int x = 0; x < turretHead.turretHeads.Count; x++)
                    {
                        turretHead.turretHeads[x]._ToolConfig = ToolConfig.None;
                    }
                    break;
                case "Vusion_1_6":
                    for (int x = 0; x < turretHead.turretHeads.Count; x++)
                    {
                        turretHead.turretHeads[x]._ToolConfig = ToolConfig.Vusion_1_6;
                    }
                    break;
                case "Vusion_2_2":
                    for (int x = 0; x < turretHead.turretHeads.Count; x++)
                    {
                        turretHead.turretHeads[x]._ToolConfig = ToolConfig.Vusion_2_2;
                    }
                    break;
                case "Vusion_2_6":
                    for (int x = 0; x < turretHead.turretHeads.Count; x++)
                    {
                        turretHead.turretHeads[x]._ToolConfig = ToolConfig.Vusion_2_6;
                    }
                    break;
            }


            // Teong 31May2021
            foreach (var item in GlobalVar.RBSafeZoneList)
            {
                item.InitDataset(ref ABB_Robot);
            }
            ReadSettingData();


        }

        public override void ServoOff()
        {
            ABB_Robot.Disconnect();
        }

        public override void AlwaysRun()
        {
            //if (!ABB_Robot.IsAuto)
            //    JSDK.Alarm.Show("4002");

            if (!SysPara.isSettingRefresh)
            {
                try
                {
                    if (turretHead != null && turretHead.turretHeads.Count > 0)
                    {
                        turretHead.turretHeads[0].isBypassing = Convert.ToBoolean(SettingData.Tables["MSet"].Rows[0]["BypassHead1"]);
                        turretHead.turretHeads[1].isBypassing = Convert.ToBoolean(SettingData.Tables["MSet"].Rows[0]["BypassHead2"]);
                        turretHead.turretHeads[2].isBypassing = Convert.ToBoolean(SettingData.Tables["MSet"].Rows[0]["BypassHead3"]);
                        turretHead.turretHeads[3].isBypassing = Convert.ToBoolean(SettingData.Tables["MSet"].Rows[0]["BypassHead4"]);
                    }

                    // Teong 31May2021
                    (bool, string) safeToMove = (false, "");
                    foreach (var item in GlobalVar.RBSafeZoneList)
                    {
                        safeToMove = item.SafeToMove();
                        _RobotSafeZone = item;
                        if (!safeToMove.Item1 && (MiddleLayer.IsManualRun() || SysPara.SystemRun))
                        {
                            JSDK.Alarm.Show("4002", safeToMove.Item2);
                        }
                        else
                        {
                            JSDK.Alarm.Show("4021", safeToMove.Item2);
                        }
                    }

                    #region dbFeeder
                    //------------------dbFeeder---------------------
                    if (!SysPara.Simulation && !SysPara.IsDryRun) //Bypass IO - NEED REMOVE
                    {
                        #region Feeder 1
                        bool IsBypassLeftFeeder = GetSettingValue("MSet", "BypassFeeder1");
                        dbEngineFeeder1.isOnline = !IsBypassLeftFeeder;
                        dbEngineFeeder1.isAlarm = false;
                        dbEngineFeeder1.isReady = !IsBypassLeftFeeder;
                        dbEngineFeeder1.isBusy = !IsBypassLeftFeeder;

                        #endregion

                        #region Feeder 2
                        bool IsBypassRightFeeder = GetSettingValue("MSet", "BypassFeeder2");
                        dbEngineFeeder2.isOnline = !IsBypassRightFeeder;
                        dbEngineFeeder2.isAlarm = false;
                        dbEngineFeeder2.isReady = !IsBypassRightFeeder;
                        dbEngineFeeder2.isBusy = !IsBypassRightFeeder;
                        #endregion

                    }
                    //------------------------------------------------
                    #endregion
                }
                catch (Exception) { }
            }
        }

        public override void StartRun()
        {
            RunTMRobot.Restart();
        }

        public override void StopRun()
        {
            pB_W1.Tag = null;
            pB_W2.Tag = null;
            pB_W3.Tag = null;

            StopManualTask.Cancel();
            ABB_Robot.SoftMotionStop();
            ABB_Robot.ResetPointer();
            ABB_Robot.StartRapid();
        }

        public override void InitialReset()
        {
            #region Reset Handshake
            Handshake.RobotToCov_Init.Reset();
            Handshake.RobotToConv.Reset();
            eSelectedFeederType = eFeederType.Left;
            MiddleLayer.CvyF.rbWait = 0;//TODO need to be 0
            PlaceEnd_RobotGotoSafe_Latch = false;
            SysPara.TaktTime.Reset();

            #endregion

            #region Reset Task
            fcInitialStart.TaskReset();
            isEndLotMode = false;
            SysPara.EndLotMode = false;
            #endregion

            #region Robot
            ABB_Robot.ResetPointer();
            ABB_Robot.ServoOn();
            ABB_Robot.StartRapid();
            ResetRobotPostStage();
            SetCurrentSpeed(MiddleLayer.SystemF.GetSettingValue("PSet", "MachineSpeedRatio"));

            #endregion

            #region UI
            pB_W1.Tag = null;
            pB_W2.Tag = null;
            pB_W3.Tag = null;
            #endregion

        }

        public override void Initial()
        {
            fcInitialStart.TaskRun();
        }

        public override void RunReset()
        {
            fcStartFlow.TaskReset();
            ResetRobotPostStage();
        }

        public override void Run()
        {
            fcStartFlow.TaskRun();
        }

        public override void BeforeProductionSetting()
        {
            double _RbtSpd = String.IsNullOrWhiteSpace(txtRbtSpd.Text) ? GetSettingValue("MSet", "RobotSpeed") : double.Parse(txtRbtSpd.Text);
            double _RbtRSpd = String.IsNullOrWhiteSpace(txtRbtRSpd.Text) ? GetSettingValue("MSet", "RobotRSpeed") : double.Parse(txtRbtRSpd.Text); //Zax ZSpeed
            double _MaxRbtSpd = String.IsNullOrWhiteSpace(txtRbtSpd.Text) ? GetSettingValue("MSet", "MaxRobotSpeed") : double.Parse(txtMaxRbtSpd.Text);
            if (_RbtSpd > _MaxRbtSpd || _RbtRSpd > _MaxRbtSpd)
            {
                bIsSettingClean = false;
                throw new Exception("Robot Speed / Robot RSpeed cannot exceed the maximum robot speed!");
            }
            else
                bIsSettingClean = true;
        }

        public override void AfterProductionSetting()
        {
        }

        public override void BeforRecipeEditor()
        {
        }

        public override void AfterRecipeEditor()
        {
        }

        #endregion

        #region Robot Function
        private bool IsPluse = false;
        public Arm jogArm = ABBRobot.ABB_RobotControl.Arm.R;
        private double Speed = 0.0;
        private string axis = "";
        private bool bWork = false;
        private int rbMoveStage = 0;
        private int tmpSpeedRatio;
        private bool rbError = false;
        private double tmpWorkSpeed;
        private double tmpWorkRSpeed;
        private const double AllowGap = 0.05;

        private void SaveCurrentSpeed()
        {
            try
            {
                if (ABB_Robot != null)
                {
                    tmpSpeedRatio = ABB_Robot.SpeedRatio;
                    tmpWorkSpeed = ABB_Robot.WorkSpeed;
                    tmpWorkRSpeed = ABB_Robot.WorkRSpeed; //Zax - RSpeed
                }

            }
            catch (Exception) { }
        }

        private void LoadCurrentSpeed()
        {
            try
            {
                if (ABB_Robot != null)
                {
                    ABB_Robot.SpeedRatio = tmpSpeedRatio;
                    ABB_Robot.WorkSpeed = tmpWorkSpeed;
                    ABB_Robot.WorkRSpeed = tmpWorkRSpeed; //Zax - RSpeed
                }

            }
            catch (Exception) { }
        }

        private void SetCurrentSpeed(int Ratio)
        {
            if (ABB_Robot != null)
            {
                ABB_Robot.SpeedRatio = Ratio;
                ABB_Robot.WorkSpeed = SysPara.IsMaintenanceMode ? 200 : GetSettingValue("MSet", "RobotSpeed");
                ABB_Robot.WorkRSpeed = SysPara.IsMaintenanceMode ? 200 : GetSettingValue("MSet", "RobotRSpeed");//Zax - RSpeed;
            }
        }

        private void StopAllMotor()
        {
            ABB_Robot.ServoOff();
            ABB_Robot.ServoOn();
            ABB_Robot.ResetPointer();
            ABB_Robot.StartRapid();
            //ABBRobot.Halt();
        }

        private void OnErrorReceive(int ErrorNumber, string ErrorTitle)
        {
            if (ErrorNumber == 436) return;
            JSDK.Alarm.Show("4000", "[Robot Error] " + ErrorTitle);
            lblLog.Text = $"[{ErrorNumber}] [{ErrorTitle}]";
        }

        private void RobotJogDown(object sender, MouseEventArgs e)
        {
            try
            {
                IsPluse = (((Button)sender).Text[1].ToString() == "+");
                Speed = Convert.ToDouble(txtRobotJogSpeed.Text);
                axis = ((Button)sender).Text[0].ToString();

                SysPara.SystemMode = RunMode.IDLE;
                SysPara.SystemInitialOk = false;
                if (!cbStepMode.Checked)
                {
                    bWork = true;
                    if (!bgJog.IsBusy)
                    {
                        bgJog.RunWorkerAsync();
                    }
                }
                else
                {
                    if (Speed != 0 && axis != "")
                        RBJog(IsPluse, Speed, axis, jogArm);
                }
                // RBJog(IsPluse, Speed, axis);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RobotJogUp(object sender, MouseEventArgs e)
        {
            tJog.Enabled = false;
            // ABB_Robot.ServoOff();
            //ABB_Robot.ServoOn();
            //ABB_Robot.ResetPointer();
            //ABB_Robot.StartRapid();
            IsPluse = false;
            Speed = 0.0;
            axis = "";
            bWork = false;
            if (bgJog.IsBusy)
            {
                bgJog.CancelAsync();
            }
        }

        public void RBJog(bool IsPluse, double Speed, string axis, Arm arm)
        {
            string Mode = cbJogMode.SelectedItem.ToString();
            ABB_Robot.WorkSpeed = 200;
            ABB_Robot.WorkRSpeed = 200; //Zax - RSpeed
            ABB_Robot.IsManualJog = true;
            if (Mode == "JMove")
            {
                switch (axis)
                {
                    case "X":
                        ABB_Robot.MoveJ(ABB_Robot.GetRobotPos().X + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "Y":
                        ABB_Robot.MoveJ(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "Z":
                        ABB_Robot.MoveJ(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "W":
                        ABB_Robot.MoveJ(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R + (IsPluse ? Speed : -Speed), arm);
                        break;
                }
            }
            else if (Mode == "LMove")
            {
                switch (axis)
                {
                    case "X":
                        ABB_Robot.MoveL(ABB_Robot.GetRobotPos().X + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "Y":
                        ABB_Robot.MoveL(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "Z":
                        ABB_Robot.MoveL(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z + (IsPluse ? Speed : -Speed), ABB_Robot.GetRobotPos().R, arm);
                        break;
                    case "W":
                        ABB_Robot.MoveL(ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y, ABB_Robot.GetRobotPos().Z, ABB_Robot.GetRobotPos().R + (IsPluse ? Speed : -Speed), arm);
                        break;
                }
            }
            ABB_Robot.IsManualJog = false;
        }

        // Teong 31May2021
        private void RobotJogLostFocus(object sender, EventArgs e)
        {
            //Teong 31May2021
            IsPluse = false;
            Speed = 0.0;
            axis = "";
            bWork = false;
            if (bgJog.IsBusy)
            {
                bgJog.CancelAsync();
            }
        }

        private void btnServoOn_Click(object sender, EventArgs e)
        {
            lblLog.Text = "";
            ABB_Robot.ServoOn();
            ABB_Robot.ResetPointer();
            ABB_Robot.StartRapid();
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            ABB_Robot.ServoOff();
            ABB_Robot.StopRapid();
            ABB_Robot.ResetPointer();
        }

        private void bgJog_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (bWork)
            {
                if (Speed != 0 && axis != "")
                    RBJog(IsPluse, Speed, axis, jogArm);
                Thread.Sleep(50);
            }
        }

        /// <summary>Move all position with safety check. Recommended to use this method.</summary>
        public bool MoveToRobotPost(RobotAxis Axis, double X, double Y, double Z, double W, bool isCheckSafeZ, RobotMove move, Arm arm, int TimeOut = 15000)
        {
            bool InPosition = false;
            string _ErrMsg = null;
            if (!InPosition && TMRobotMove.IsOn(TimeOut))
            {
                // Teong 25Aug2021
                TMRobotMove.Restart();
                JSDK.Alarm.Show("4001", $"Robot Move To Target [{X},{Y},{Z},{W}] Time Out. RobotPostStage : {rbMoveStage}. {_ErrMsg}");
                ResetRobotPostStage();
            }
            return InPosition;
        }

        // Teong 25Aug2021
        /// <summary>Move all position without much safety check. Fastest but dangerous, need to make sure current design suitable to use this method. </summary>
        public bool MoveToRobotPost(double X, double Y, double Z, double W, RobotMove move, Arm arm, int TimeOut = 15000)
        {
            bool InPosition = false;
            string _ErrMsg = null;
            switch (rbMoveStage)
            {
                //case 0:
                //    //Check Robot is Servo on
                //    if (!ABB_Robot.IsRunning)
                //    {
                //        ABB_Robot.ServoOn();
                //        ABB_Robot.ResetPointer();
                //        ABB_Robot.StartRapid();
                //        TMRobotMove.Restart();
                //        MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot Servo Off.Turning On Complete.");
                //        rbMoveStage = 5;
                //    }
                //    else
                //    {
                //        MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot Servo On.");
                //        TMRobotMove.Restart();
                //        rbMoveStage = 5;
                //    }
                //    break;
                case 0://5:
                    //Check all Cylinder is Retract
                    break;
                case 10:
                    //Check if in position, Return True
                    if (ABB_Robot.GetRobotPos().X == X &&
                    ABB_Robot.GetRobotPos().Y == Y &&
                    ABB_Robot.GetRobotPos().Z == Z &&
                    ABB_Robot.GetRobotPos().R == W)
                    {
                        MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot is in Target Position. Skip Moving and return Complete.");
                        TMRobotMove.Restart();
                        rbMoveStage = 999;
                        break;
                    }
                    else
                    {
                        //CXT 11092022
                        ABB_Robot.Reset_LastMoveTimer();

                        MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot not in Target Position.");
                        TMRobotMove.Restart();
                        rbMoveStage = 15;
                    }
                    break;
                case 15:
                    //Move XYZW to target
                    bool r2 = rbMove(move, X, Y, Z, W, arm);
                    if (r2)
                    {
                        MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot Move all axis to Target Pos Complete.");
                        TMRobotMove.Restart();
                        rbMoveStage = 999;
                    }
                    _ErrMsg = $"Robot failed  to move all axis to Target Pos.";//Zax 9/1/21 - Error Msg
                    break;
                case 999:
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot Move Complete.");
                    InPosition = true;
                    TMRobotMove.Restart();
                    ResetRobotPostStage();
                    break;
                case 995:
                    // Teong 31May2021
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,[MoveToRobotPost] Robot Move Error.");
                    InPosition = false;
                    rbError = true;
                    TMRobotMove.Restart();
                    ResetRobotPostStage();
                    break;
            }

            if (!InPosition && TMRobotMove.IsOn(TimeOut))
            {
                // Teong 25Aug2021
                TMRobotMove.Restart();
                JSDK.Alarm.Show("4001", $"Robot Move To Target [{X},{Y},{Z},{W}] Time Out. RobotPostStage : {rbMoveStage}. {_ErrMsg}");
                ResetRobotPostStage();
            }
            return InPosition;
        }

        // Teong 31May2021
        public void MoveToRobotPostManual(RobotAxis Axis, double X, double Y, double Z, double W, bool isCheckSafeZ, RobotMove move, Arm arm, bool IsHomeNeeded = false)
        {
            GlobalVar.movingName = "Manual";
            bool HomeDone = !IsHomeNeeded;
            rbError = false;
            ResetRobotPostStage();
            SaveCurrentSpeed();
            SetCurrentSpeed(33);
            if (ABB_Robot != null)
            {
                ABB_Robot.WorkSpeed = 200;
                ABB_Robot.WorkRSpeed = 200;
            }
            while (!StopManualTask.IsCancellationRequested)
            {
                if (IsHomeNeeded && !HomeDone)
                {
                    HomeDone = ABB_Robot.MoveHome(rbSafe.GetValue(RobotTeachPoint.AxisName.X), rbSafe.GetValue(RobotTeachPoint.AxisName.Y), rbSafe.GetValue(RobotTeachPoint.AxisName.Z), rbSafe.GetValue(RobotTeachPoint.AxisName.W), rbSafe.GetArm());
                }
                if (HomeDone && (MoveToRobotPost(Axis, X, Y, Z, W, isCheckSafeZ, move, arm) || rbError))
                {
                    StopManualTask.Cancel();
                    LoadCurrentSpeed();
                    GlobalVar.movingName = "";
                    rbError = false;
                }
                Thread.Sleep(10);
            }
            LoadCurrentSpeed();
            GlobalVar.movingName = "";
        }

        public bool MoveToRobotPostArray(List<RobotPosData> Posdata, bool isCheckSafeZ)
        {
            bool InPosition = false;
            double X = Posdata[Posdata.Count - 1].X;
            double Y = Posdata[Posdata.Count - 1].Y;
            double Z = Posdata[Posdata.Count - 1].Z;
            double R = Posdata[Posdata.Count - 1].R;
            switch (rbMoveStage)
            {
                case 0:
                    if (ABB_Robot.GetRobotPos().X == X &&
                          ABB_Robot.GetRobotPos().Y == Y &&
                          ABB_Robot.GetRobotPos().Z == Z &&
                          ABB_Robot.GetRobotPos().R == R)
                    {
                        rbMoveStage = 999;
                        break;
                    }

                    if (isCheckSafeZ)
                    {
                        TMRobotMove.Restart();
                        rbMoveStage = 5;
                    }
                    else
                    {
                        TMRobotMove.Restart();
                        rbMoveStage = 25;
                    }

                    break;
                case 5:
                    if (GetRange(ABB_Robot.GetRobotPos().Z, rbSafe.GetValue(RobotTeachPoint.AxisName.Z)))
                    {
                        TMRobotMove.Restart();
                        rbMoveStage = 25;
                    }
                    else
                    {
                        TMRobotMove.Restart();
                        rbMoveStage = 10;
                    }
                    break;
                case 10:
                    Posdata.Insert(0,
                        new RobotPosData
                        {
                            X = ABB_Robot.GetRobotPos().X,
                            Y = ABB_Robot.GetRobotPos().Y,
                            Z = rbSafe.GetValue(RobotTeachPoint.AxisName.Z),
                            R = ABB_Robot.GetRobotPos().R
                        });
                    TMRobotMove.Restart();
                    rbMoveStage = 25;
                    break;
                case 25:
                    bool r4 = ABB_Robot.MoveArrayJ(Posdata);
                    if (r4)
                    {
                        rbMoveStage = 999;
                    }
                    if (TMRobotMove.IsOn(20000))
                    {
                        TMRobotMove.Restart();
                        JSDK.Alarm.Show("4001", $"Robot Move To Target [{X},{Y},{Z},{R}] Time Out.");
                    }
                    break;

                case 999:
                    InPosition = true;
                    lRobotPost.Clear();
                    TMRobotMove.Restart();
                    ResetRobotPostStage();
                    break;
            }
            return InPosition;
        }

        private void ResetRobotPostStage()
        {
            TMRobotMove.Restart();
            rbMoveStage = 0;
        }

        private bool rbMove(RobotMove move, double X, double Y, double Z, double R, Arm arm)
        {
            bool ret = false;
            if (move == RobotMove.JMove)
            {
                ret = ABB_Robot.MoveJ(X, Y, Z, R, arm);
            }
            else if (move == RobotMove.LMove)
            {
                ret = ABB_Robot.MoveL(X, Y, Z, R, arm);
            }

            return ret;
        }

        private void btnZGotoSafe_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            ExecuteManual(() => MoveToRobotPostManual(RobotAxis.Z, ABB_Robot.GetRobotPos().X, ABB_Robot.GetRobotPos().Y, rbSafe.GetValue(RobotTeachPoint.AxisName.Z), ABB_Robot.GetRobotPos().R, true, RobotMove.JMove, jogArm));
        }

        //Zax 8/25/21 - Current machine have limited area, cannot allow operator to move long distance.
        private void txtRobotJogSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //double _distance = 0.11;
            //if (double.TryParse(txtRobotJogSpeed.Text, out _distance))
            //{
            //    if (_distance <= 0.10 || _distance > 10)
            //    {
            //        txtRobotJogSpeed.Text = _distance <= 0.10 ? "0.11" : (_distance >= 10 ? "10" : _distance.ToString());
            //        MessageBox.Show("WARNING: Robot only allow move distance within 0.11mm - 10mm.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        e.Cancel = true;
            //    }
            //}
            //else
            //{
            //    txtRobotJogSpeed.Text = "0.11";
            //    MessageBox.Show("ERROR: Invalid string input!\nPlease enter numeric value.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    e.Cancel = true;
            //}
        }

        //Zax 8/5/21
        private void cbJobArm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualArmChange)
            {
                StopManualTask.Cancel();
                SysPara.SystemMode = RunMode.IDLE;
                SysPara.SystemInitialOk = false;

                DialogResult dialogResult = MessageBox.Show("Confirm to change arm?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    RobotPosData currentPost = ABB_Robot.GetRobotPos();
                    if (cbJobArm.SelectedItem.ToString() == "Right")
                        jogArm = ABB_RobotControl.Arm.R;
                    else
                        jogArm = ABB_RobotControl.Arm.L;
                    ExecuteManual(() => MoveToRobotPostManual(RobotAxis.All, currentPost.X, currentPost.Y, currentPost.Z, currentPost.R, true, cbJogMode.SelectedItem.ToString() == "JMove" ? RobotMove.JMove : RobotMove.LMove, jogArm, true));
                }
                else
                {
                    ManualArmChange = false;
                    cbJobArm.SelectedItem = ABB_Robot.CurrentArm == ABB_RobotControl.Arm.R ? "Right" : "Left";
                    ManualArmChange = true;
                }
            }
        }

        //Zax 8/5/21
        private void btnChgHead_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            SysPara.SystemMode = RunMode.IDLE;
            SysPara.SystemInitialOk = false;

            RobotPosData target = new RobotPosData()
            {
                X = double.Parse(txtH1XPos.Text),
                Y = double.Parse(txtH1YPos.Text),
                Z = double.Parse(txtH1ZPos.Text),
                R = double.Parse(txtH1WPos.Text)
            };
            switch (cbChangeHead.SelectedItem)
            {
                case "H2":
                    target = turretHead.IndexTHead2(target, rbCalibHead1, rbCalibHead2);
                    //_target = turretHead.IndexTHead(current, rbCalibHead1, rbCalibHead2);
                    //current = turretHead.IndexTHead2D(current, _target, (rbCalibHead2.GetValue(RobotTeachPoint.AxisName.W) - 90) + _target.R - 90);
                    break;
                case "H3":
                    target = turretHead.IndexTHead2(target, rbCalibHead1, rbCalibHead3);
                    //_target = turretHead.IndexTHead(current, rbCalibHead1, rbCalibHead3);
                    //current = turretHead.IndexTHead2D(current, _target, (rbCalibHead3.GetValue(RobotTeachPoint.AxisName.W) - 180) + _target.R - 180);
                    break;
                case "H4":
                    target = turretHead.IndexTHead2(target, rbCalibHead1, rbCalibHead4);
                    //_target = turretHead.IndexTHead(current, rbCalibHead1, rbCalibHead4);
                    //current = turretHead.IndexTHead2D(current, _target, (rbCalibHead2.GetValue(RobotTeachPoint.AxisName.W) - 270) + _target.R - 270);
                    break;
            }

            ExecuteManual(() => MoveToRobotPostManual(RobotAxis.All, target.X, target.Y, target.Z, target.R, false, RobotMove.JMove, jogArm));
        }

        //Zax 8/9/21
        private void txtH1Pos_DoubleClick(object sender, EventArgs e)
        {
            RobotPosData robData = ABB_Robot.GetRobotPos();
            txtH1XPos.Text = robData.X.ToString("F2");
            txtH1YPos.Text = robData.Y.ToString("F2");
            txtH1ZPos.Text = robData.Z.ToString("F2");
            txtH1WPos.Text = robData.R.ToString("F2");
        }
        #endregion

        #region Seq Context Menu
        private void msSeq_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                dgv_name = ((ContextMenuStrip)sender).SourceControl as DataGridView;
                dgv_DataMember = dgv_name.DataMember.ToString();

                visionToolStripMenuItem.Visible = false;
                autoTeachToolStripMenuItem.Visible = true;
                if (dgv_name.SelectedCells.Count > 0 && dgv_name.SelectedCells[0].OwningRow.Index < 0)
                {
                    teachToolStripMenuItem.Visible = false;
                    deleteToolStripMenuItem.Visible = false;
                    visionToolStripMenuItem.Visible = false;
                    autoTeachToolStripMenuItem.Visible = false;
                }
                else if (dgv_name.SelectedCells.Count > 0 && dgv_name.SelectedCells[0].OwningRow.Index > 0 && dgv_name.SelectedCells[0].OwningRow.Cells[1].Value.Equals("Vision"))
                {
                    visionToolStripMenuItem.Visible = true;
                }
                //else if (dgv_name.SelectedCells.Count > 0 &&
                //    (dgv_name.SelectedCells[0].OwningRow.Cells[1].Value.Equals("Place")
                //    || dgv_name.SelectedCells[0].OwningRow.Cells[1].Value.Equals("Pick")))
                //{
                //    autoTeachToolStripMenuItem.Visible = true;
                //}

                else
                {
                    teachToolStripMenuItem.Visible = true;
                    deleteToolStripMenuItem.Visible = true;
                    visionToolStripMenuItem.Visible = false;
                    autoTeachToolStripMenuItem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RBSeqItem _item = new RBSeqItem
            {
                PointName = null,
                Mode = "Point",
                X = ABB_Robot.GetRobotPos().X,
                Y = ABB_Robot.GetRobotPos().Y,
                Z = ABB_Robot.GetRobotPos().Z,
                W = ABB_Robot.GetRobotPos().R,
                BDelay = 0,
                ADelay = 0,
                Index = 0,
                Move = "JMove",
                Stack = "None",
                Arm = ABB_Robot.CurrentArm == ABB_RobotControl.Arm.R ? "Right" : "Left",
                Speed = 300,
                ZSpeed = 300,
                Enabled = true,
                ID = GenerateID(),
                TeachPost = ""

            };

            AddNewRow_Seq(dgv_name, 0, _item, false, false);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                int Rows = dgv.CurrentRow.Index;
                RBSeqItem _item = new RBSeqItem
                {
                    PointName = null,
                    Mode = "Point",
                    X = ABB_Robot.GetRobotPos().X,
                    Y = ABB_Robot.GetRobotPos().Y,
                    Z = ABB_Robot.GetRobotPos().Z,
                    W = ABB_Robot.GetRobotPos().R,
                    BDelay = 0,
                    ADelay = 0,
                    Index = 0,
                    Stack = "None",
                    Move = "JMove",
                    Arm = ABB_Robot.CurrentArm == ABB_RobotControl.Arm.R ? "Right" : "Left",
                    Speed = 300,
                    ZSpeed = 300,
                    Enabled = true,
                    ID = GenerateID(),
                    TeachPost = ""
                };
                AddNewRow_Seq(dgv_name, Rows, _item, false, true);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Delete?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    dt.Rows.RemoveAt(dgv.CurrentRow.Index);
                }
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Teach All Axis?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    dr["X"] = ABB_Robot.GetRobotPos().X;
                    dr["Y"] = ABB_Robot.GetRobotPos().Y;
                    dr["Z"] = ABB_Robot.GetRobotPos().Z;
                    dr["W"] = ABB_Robot.GetRobotPos().R;
                    dr["Arm"] = ABB_Robot.CurrentArm == ABB_RobotControl.Arm.R ? "Right" : "Left";
                }
            }
        }

        private void xToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Teach Only X Axis?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    dr["X"] = ABB_Robot.GetRobotPos().X;
                }
            }

        }

        private void yToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Teach Only Y Axis?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    dr["Y"] = ABB_Robot.GetRobotPos().Y;
                }
            }
        }

        private void zToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Teach Only Z Axis?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    dr["Z"] = ABB_Robot.GetRobotPos().Z;
                }
            }
        }

        private void wToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to Teach Only W Axis?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    dr["W"] = ABB_Robot.GetRobotPos().R;
                }
            }
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xYZWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.All, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void xYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.XY, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.X, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.Y, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void zToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.Z, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void wToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            DataGridView dgv = dgv_name;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables[dgv_DataMember];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                ExecuteManual(() => MoveToRobotPostManual(RobotAxis.W, (double)dr["X"], (double)dr["Y"], (double)dr["Z"], (double)dr["W"], true, RobotMove.JMove, _arm));
            }
        }

        private void AddNewRow_Seq(DataGridView dgv, int index, RBSeqItem item, bool isControlUpDwn, bool isInsert)
        {
            int totalRows = dgv.Rows.Count;
            DataTable dt = RecipeData.Tables[dgv.DataMember];
            int Rows = dt.Rows.Count;
            DataRow dr = dt.NewRow();
            dr["PointName"] = item.PointName;
            dr["Mode"] = item.Mode;
            dr["X"] = item.X;
            dr["Y"] = item.Y;
            dr["Z"] = item.Z;
            dr["W"] = item.W;
            dr["B.Delay"] = item.BDelay;
            dr["A.Delay"] = item.ADelay;
            dr["Index"] = item.Index;
            dr["Stack"] = item.Stack;
            dr["Move"] = item.Move;
            dr["Arm"] = item.Arm;
            dr["Speed"] = item.Speed;
            dr["ZSpeed"] = item.ZSpeed;
            dr["Enabled"] = item.Enabled;
            dr["ID"] = item.ID;
            dr["TeachPost"] = item.TeachPost;
            if (index == 0 && !isControlUpDwn)
            {
                if (isInsert)
                {
                    index++;
                }
                else
                {
                    index = totalRows + 1;
                }

                dr["PointName"] = "P_" + Rows;
                dt.Rows.InsertAt(dr, index);
                return;
            }
            if (index != 0 && !isControlUpDwn)
            {
                dr["PointName"] = "P_" + Rows;
                index++;
            }

            if (isControlUpDwn)
            {
                dr["PointName"] = item.PointName;
            }

            dt.Rows.InsertAt(dr, index);
            // dt.AcceptChanges();
        }

        #region Auto Teach Index
        private void autoTeachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataGridView dgv = dgv_name;
            //if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            //{
            //    DataTable dt = RecipeData.Tables["RSeq"];
            //    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
            //    AutoTeachSummary summary = new AutoTeachSummary(dr["TeachPost"].ToString(), ref ManualTask, ref StopManualTask);
            //    summary.ShowDialog();
            //    if (summary.dataString != null && summary.dataString != "")
            //    {
            //        dr["TeachPost"] = summary.dataString;
            //    }
            //}
            try
            {
                AutoTeachRBForm autoTeachFrm = new AutoTeachRBForm(dgv_name, GetSettingValue("MSet", "MaxRobotSpeed"), ref ManualTask, ref StopManualTask, ref RecipeData);
                autoTeachFrm.ShowDialog();

                if (autoTeachFrm.Complete)
                {
                    RecipeData = autoTeachFrm.GetRecipe().Copy();
                    dgv_name.DataSource = autoTeachFrm.GetRecipe();
                    dgv_name.DataSource = RecipeData;


                    if (dgv_name.InvokeRequired)
                    {
                        dgv_name.Invoke((MethodInvoker)delegate ()
                        {
                            dgv_name.Update();
                            dgv_name.Refresh();
                        });
                    }
                    else
                    {
                        dgv_name.Update();
                        dgv_name.Refresh();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void allAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"X :{data.Post[Index].X}" + Environment.NewLine +
                       $"Y :{data.Post[Index].Y}" + Environment.NewLine +
                       $"Z :{data.Post[Index].Z}" + Environment.NewLine +
                       $"W :{data.Post[Index].R}" + Environment.NewLine +
                       $"Arm :{dr["Arm"].ToString()}" + Environment.NewLine, "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.All, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void xYAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"X :{data.Post[Index].X}" + Environment.NewLine +
                       $"Y :{data.Post[Index].Y}" + Environment.NewLine +
                       $"Arm :{dr["Arm"].ToString()}" + Environment.NewLine, "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.XY, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void xIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"X :{data.Post[Index].X}" + Environment.NewLine +
                       $"Arm :{dr["Arm"].ToString()}", "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.X, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void yIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"Y :{data.Post[Index].Y}" + Environment.NewLine +
                        $"Arm :{dr["Arm"].ToString()}", "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.Y, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void zIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"Z :{data.Post[Index].Z}" + Environment.NewLine +
                       $"Arm :{dr["Arm"].ToString()}", "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.Z, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void wIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopManualTask.Cancel();
                DataGridView dgv = dgv_name;
                if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
                {
                    DataTable dt = RecipeData.Tables[dgv_DataMember];
                    DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                    var data = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(dr["TeachPost"].ToString());
                    var Index = Convert.ToInt32(dr["Index"]);
                    DialogResult dialogResult = MessageBox.Show("Confirm Go To:" + Environment.NewLine +
                       $"W :{data.Post[Index].R}" + Environment.NewLine +
                       $"Arm :{dr["Arm"].ToString()}", "Confirmation", MessageBoxButtons.YesNo);
                    Arm _arm = (string)dr["Arm"] == "Right" ? ABB_RobotControl.Arm.R : ABB_RobotControl.Arm.L;
                    if (dialogResult == DialogResult.Yes)
                    {
                        ExecuteManual(() => MoveToRobotPostManual(RobotAxis.W, data.Post[Index].X, data.Post[Index].Y, data.Post[Index].Z, data.Post[Index].R, true, RobotMove.JMove, _arm));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion
        #endregion

        #region Data Grid View Seq
        private void dgv_Seq_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }
        }

        private void btnLpsUp_Click(object sender, EventArgs e)
        {
            DGVMoveUp();
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGVMoveDown();
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGVMoveUp();
        }

        private void btnLpsDwn_Click(object sender, EventArgs e)
        {
            DGVMoveDown();
        }

        private void btnOpenMenu_Click(object sender, EventArgs e)
        {
            msSeq.Show(MousePosition);
        }

        private void DGVMoveUp()
        {
            DataGridView dgv = dgv_Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["RSeq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()

                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex - 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DGVMoveDown()
        {
            DataGridView dgv = dgv_Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["RSeq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()
                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex + 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DGVMoveUpPick1()
        {
            DataGridView dgv = dgv_Pick1Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["Pick1Rseq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()

                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex - 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DGVMoveDownPick1()
        {
            DataGridView dgv = dgv_Pick1Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["Pick1Rseq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()
                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex + 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DGVMoveUpPick2()
        {
            DataGridView dgv = dgv_Pick2Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["Pick2Rseq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()

                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex - 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DGVMoveDownPick2()
        {
            DataGridView dgv = dgv_Pick2Seq;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                DataTable dt = RecipeData.Tables["Pick2Rseq"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];

                RBSeqItem _item = new RBSeqItem
                {
                    PointName = dr["PointName"].ToString(),
                    Mode = dr["Mode"].ToString(),
                    X = Convert.ToDouble(dr["X"]),
                    Y = Convert.ToDouble(dr["Y"]),
                    Z = Convert.ToDouble(dr["Z"]),
                    W = Convert.ToDouble(dr["W"]),
                    BDelay = Convert.ToInt32(dr["B.Delay"]),
                    ADelay = Convert.ToInt32(dr["A.Delay"]),
                    Index = Convert.ToInt32(dr["Index"]),
                    Stack = dr["Stack"].ToString(),
                    Move = dr["Move"].ToString(),
                    Arm = dr["Arm"].ToString(),
                    Speed = Convert.ToInt32(dr["Speed"]),
                    ZSpeed = Convert.ToInt32(dr["ZSpeed"]),
                    Enabled = Convert.ToBoolean(dr["Enabled"]),
                    ID = dr["ID"].ToString(),
                    TeachPost = dr["TeachPost"].ToString()
                };
                //validate

                dt.Rows.Remove(dr);
                AddNewRow_Seq(dgv, rowIndex + 1, _item, true, false);

                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgv_Seq_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dataGrid = (sender as DataGridView);
            PopulateStackMenu(dataGrid);
        }

        private void dgv_Seq_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            string headerText = view.Columns[e.ColumnIndex].HeaderText;
            DataGridView dgv = view;
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()) && headerText.Equals("Name"))
            {
                dgv.Rows[e.RowIndex].ErrorText =
                    "Name must not be empty";
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                bIsRecipeClean = false;
                e.Cancel = true;
            }
            else if (DuplicationCheck(dgv, e.FormattedValue.ToString(), e.RowIndex) && headerText.Equals("Name"))
            {
                dgv.Rows[e.RowIndex].ErrorText =
                   "Name duplication detected";
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                bIsRecipeClean = false;
                e.Cancel = true;
            }
            else if (headerText.Equals("Speed") || headerText.Equals("ZSpeed"))
            {
                int _Spd = -1;
                if (!int.TryParse(e.FormattedValue.ToString(), out _Spd))
                {
                    MessageBox.Show("ERROR: Invalid string input!\nPlease enter numeric value.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    bIsRecipeClean = false;
                    e.Cancel = true;
                }
                else if (_Spd > GetSettingValue("MSet", "MaxRobotSpeed"))
                {
                    MessageBox.Show("ERROR: RobotSpeed cannot exceed the maximum robot speed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    bIsRecipeClean = false;
                    e.Cancel = true;
                }
                else
                {
                    dgv.Rows[e.RowIndex].ErrorText = "";
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    bIsRecipeClean = true;
                }
            }
            else
            {
                dgv.Rows[e.RowIndex].ErrorText = "";
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                bIsRecipeClean = true;
            }
        }

        public void PopulateStackMenu(DataGridView data)
        {
            for (int x = 0; x < data.Rows.Count; x++)
            {
                DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)data.Rows[x].Cells[9];
                if (cb != null)
                {
                    cb.Items.Clear();
                    cb.Items.Add("None");
                    if (data.Rows[x].Cells[1].Value.ToString() == "Point" || data.Rows[x].Cells[0].Value.ToString() == "Point Offset")
                        foreach (var item in GetVisionOffsetName(data))
                            cb.Items.Add(item);
                }
            }
        }

        public List<string> GetVisionOffsetName(DataGridView data)
        {
            List<string> ret = new List<string>();
            for (int x = 0; x < data.Rows.Count; x++)
            {
                var mode = data.Rows[x].Cells[1].Value.ToString();
                if (mode == "Vision Offset")
                {
                    ret.Add(data.Rows[x].Cells[0].Value.ToString());
                }

            }
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DGVMoveUpPick1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DGVMoveDownPick1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DGVMoveUpPick2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DGVMoveDownPick2();
        }

        #endregion

        #region Function/Method/Event
        //------------------------Method------------------------------------
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

        private bool GetRange(double pos1, double pos2, double tolerance = 0.1)
        {
            
            return pos1 >= pos2 - tolerance && pos1 <= pos2 + tolerance;
        }

        private bool DuplicationCheck(DataGridView gr, string PointName, int row)
        {
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                if (gr[0, i].Value.ToString().Equals(PointName) && row != i) return true;
            }
            return false;
        }

        private string GenerateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(15)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        private string CastPosDataToXML()
        {

            List<RobotPosData> data = new List<RobotPosData>();
            RobotPosDataList data1 = new RobotPosDataList();
            for (int x = 0; x < 12; x++)
            {
                //RobotPosData data1 = new RobotPosData();
                data.Add(new RobotPosData { });
            }
            data1.Post = data;
            return XmlHelper.SerializeToXmlString<RobotPosDataList>(data1);
        }

        private RobotPosDataList CastXMLToPost(string data)
        {
            return XmlHelper.DeserializeFromXmlString<RobotPosDataList>(data);
        }

        public List<RBSeqItem> CompileRecipe(string tableName)
        {
            List<RBSeqItem> listSeq = new List<RBSeqItem>();
            DataTable dt = RecipeData.Tables[tableName];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["Enabled"]))
                {

                    RBSeqItem _seq = new RBSeqItem();
                    _seq.PointName = dt.Rows[i]["PointName"].ToString();
                    _seq.Mode = dt.Rows[i]["Mode"].ToString();
                    _seq.X = Convert.ToDouble(dt.Rows[i]["X"]);
                    _seq.Y = Convert.ToDouble(dt.Rows[i]["Y"]);
                    _seq.Z = Convert.ToDouble(dt.Rows[i]["Z"]);
                    _seq.W = Convert.ToDouble(dt.Rows[i]["W"]);
                    _seq.BDelay = Convert.ToInt32(dt.Rows[i]["B.Delay"]);
                    _seq.ADelay = Convert.ToInt32(dt.Rows[i]["A.Delay"]);
                    _seq.Index = i;
                    _seq.Stack = dt.Rows[i]["Stack"].ToString();
                    _seq.Move = dt.Rows[i]["Move"].ToString();
                    _seq.Arm = dt.Rows[i]["Arm"].ToString();
                    _seq.Speed = Convert.ToInt32(dt.Rows[i]["Speed"]);
                    _seq.ZSpeed = Convert.ToInt32(dt.Rows[i]["ZSpeed"]);
                    _seq.Enabled = Convert.ToBoolean(dt.Rows[i]["Enabled"]);
                    _seq.ID = dt.Rows[i]["ID"].ToString();
                    listSeq.Add(_seq);
                }
            }
            return listSeq;
        }

        private RBSeqItem RecipeDequeue(ref List<RBSeqItem> seq)
        {
            RBSeqItem _seq = new RBSeqItem();
            if (seq.Count > 0)
            {
                _seq = seq[0];
                seq.RemoveAt(0);
            }
            return _seq;

        }

        private RBSeqItem RecipePeak(ref List<RBSeqItem> seq)
        {
            RBSeqItem _seq = new RBSeqItem();
            if (seq.Count > 0)
            {
                _seq = seq[0];
            }
            return _seq;

        }

        private void AddCurrentListView(RBSeqItem currentSeq)
        {
          
        }

        private bool checkCurrentHeadPartExist(bool ignoreAlarm = false)
        {
            bool r1 = false;
            //TODO Modified to off vacuum once not detecting part
            //Zax 8/25/21
            turretHead.SetPartPreset(turretHead.GetActiveHead(), r1);
            if (!r1 && !ignoreAlarm)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, $"Robot,Robot Part detected missing under Turret Head {turretHead.GetActiveHead() + 1}.");
                JSDK.Alarm.Show("4020", $"Robot Part detected missing under Turret Head {turretHead.GetActiveHead() + 1}");
            }
            return r1;
        }

        //Zax 8/5/2021
        public int getCurrentHead()
        {
            int _Head = -1;
            RobotPosData RobotData = ABB_Robot.GetRobotPos();
            if (Math.Abs(RobotData.R - rbCalibHead1.GetValue(RobotTeachPoint.AxisName.W)) < AllowGap)
                _Head = 1;
            else if (Math.Abs(RobotData.R - rbCalibHead2.GetValue(RobotTeachPoint.AxisName.W)) < AllowGap)
                _Head = 2;
            else if (Math.Abs(RobotData.R - rbCalibHead3.GetValue(RobotTeachPoint.AxisName.W)) < AllowGap)
                _Head = 3;
            else if (Math.Abs(RobotData.R - rbCalibHead4.GetValue(RobotTeachPoint.AxisName.W)) < AllowGap)
                _Head = 4;
            return _Head;
        }

        //------------------------Event------------------------------------

        private void btnMotionStop_Click(object sender, EventArgs e)
        {
            StopManualTask.Cancel();
            ABB_Robot.SoftMotionStop();
        }

        //Zax 7/19/21
        private void chkKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("IMPORTANT! In order to auto re-position virtual keyboard need to run current application as Administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            MiddleLayer.coreEngine._vKeyboard.bIsEnabled = ((CheckBox)sender).Checked;
            SettingData.Tables["MSet"].Rows[0]["EnableVirtualKeyboard"] = ((CheckBox)sender).Checked;
        }

        
        //Zax 7/14/21 

        private void btnResetReject_Click(object sender, EventArgs e)
        {
            SettingData.Tables["MSet"].Rows[0]["RejectCurrentCount"] = 0;
            RejectCount = 0;
            Txt_RejectCurrentCount.Text = "0";
        }

        private void btn_MachineLight_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region UI Update
        public void resetPalletUI()
        {
            for (int i = 0; i < _CrrCvyUI.Count(); i++)
                _CrrCvyUI[i] = new CrrCvyUIStatus();

            CurrUPH_UI = 0;
            lbl_InPos_W1.Text = "";
            pB_InPos_W1.Image = Resources.ledoff;
            pbPallet_W1.Image = Resources.Pallet_inactive_dark;

            lbl_InPos_W2.Text = "";
            pB_InPos_W2.Image = Resources.ledoff;
            pbPallet_W2.Image = Resources.Pallet_inactive_dark;

            lbl_InPos_W3.Text = "";
            pB_InPos_W3.Image = Resources.ledoff;
            pbPallet_W3.Image = Resources.Pallet_inactive_dark;

            pB_Pallet_1.Image = Resources.ledoff;
            pB_Pallet_2.Image = Resources.ledoff;
            pB_Pallet_3.Image = Resources.ledoff;
            pB_Pallet_4.Image = Resources.ledoff;
            pB_Pallet_5.Image = Resources.ledoff;
            pB_Pallet_6.Image = Resources.ledoff;
            pB_Pallet_7.Image = Resources.ledoff;
            pB_Pallet_8.Image = Resources.ledoff;
            pB_Pallet_9.Image = Resources.ledoff;
            pB_Pallet_10.Image = Resources.ledoff;
            pB_Pallet_11.Image = Resources.ledoff;
            pB_Pallet_12.Image = Resources.ledoff;

        }

        public void setPalletUI(ConveyorPost _WP, bool _isPresnt, string _staText = null, bool _isMoving = true)
        {
            _isMoving = _isPresnt ? false : _isMoving;
            switch (_WP)
            {
                case ConveyorPost.WP1: //WP1
                    if (_CrrCvyUI[(int)ConveyorPost.WP1].isPresnt != _isPresnt || _CrrCvyUI[(int)ConveyorPost.WP1].isMoving != _isMoving || _CrrCvyUI[(int)ConveyorPost.WP1].staText != _staText)
                    {
                        _CrrCvyUI[(int)ConveyorPost.WP1].isPresnt = _isPresnt;
                        _CrrCvyUI[(int)ConveyorPost.WP1].isMoving = _isMoving;
                        _CrrCvyUI[(int)ConveyorPost.WP1].staText = _staText;
                        pB_W1.Tag = _isMoving ? "cvy1" : null;
                        lbl_InPos_W1.Text = _staText;
                        pB_InPos_W1.Image = _isPresnt ? Resources.ledon : Resources.ledoff;
                        pbPallet_W1.Image = _isPresnt ? Resources.Pallet_shadow : Resources.Pallet_inactive_dark;
                    }
                    break;
                case ConveyorPost.WP2: //WP2
                    if (_CrrCvyUI[(int)ConveyorPost.WP2].isPresnt != _isPresnt || _CrrCvyUI[(int)ConveyorPost.WP2].isMoving != _isMoving || _CrrCvyUI[(int)ConveyorPost.WP2].staText != _staText)
                    {
                        _CrrCvyUI[(int)ConveyorPost.WP2].isPresnt = _isPresnt;
                        _CrrCvyUI[(int)ConveyorPost.WP2].isMoving = _isMoving;
                        _CrrCvyUI[(int)ConveyorPost.WP2].staText = _staText;
                        pB_W2.Tag = _isMoving ? "cvy1" : null;
                        lbl_InPos_W2.Text = _staText;
                        pB_InPos_W2.Image = _isPresnt ? Resources.ledon : Resources.ledoff;
                        pbPallet_W2.Image = _isPresnt ? Resources.Pallet_shadow : Resources.Pallet_inactive_dark;

                        if (_isMoving)
                        {
                            CurrUPH_UI = 0;
                            pB_Pallet_1.Image = Resources.ledoff;
                            pB_Pallet_2.Image = Resources.ledoff;
                            pB_Pallet_3.Image = Resources.ledoff;
                            pB_Pallet_4.Image = Resources.ledoff;
                            pB_Pallet_5.Image = Resources.ledoff;
                            pB_Pallet_6.Image = Resources.ledoff;
                            pB_Pallet_7.Image = Resources.ledoff;
                            pB_Pallet_8.Image = Resources.ledoff;
                            pB_Pallet_9.Image = Resources.ledoff;
                            pB_Pallet_10.Image = Resources.ledoff;
                            pB_Pallet_11.Image = Resources.ledoff;
                            pB_Pallet_12.Image = Resources.ledoff;
                        }
                    }
                    break;
                case ConveyorPost.WP3: //WP3
                    if (_CrrCvyUI[(int)ConveyorPost.WP3].isPresnt != _isPresnt || _CrrCvyUI[(int)ConveyorPost.WP3].isMoving != _isMoving || _CrrCvyUI[(int)ConveyorPost.WP3].staText != _staText)
                    {
                        _CrrCvyUI[(int)ConveyorPost.WP3].isPresnt = _isPresnt;
                        _CrrCvyUI[(int)ConveyorPost.WP3].isMoving = _isMoving;
                        _CrrCvyUI[(int)ConveyorPost.WP3].staText = _staText;
                        pB_W3.Tag = _isMoving ? "cvy1" : null;
                        lbl_InPos_W3.Text = _staText;
                        pB_InPos_W3.Image = _isPresnt ? Resources.ledon : Resources.ledoff;
                        pbPallet_W3.Image = _isPresnt ? Resources.Pallet_shadow : Resources.Pallet_inactive_dark;
                    }
                    break;
            }
        }

        private void setProductUI()
        {
            int _crrUPH = SysPara.UPH_Count % 12 == 0 && IsCurrentUPH == 11 ? 12 : SysPara.UPH_Count % 12;
            if (_crrUPH != 0 && IsCurrentUPH != _crrUPH)
            {
                IsCurrentUPH = _crrUPH;//SysPara.UPH_Count % 12 == 0 && IsCurrentUPH == 11 ? 12 : SysPara.UPH_Count % 12;
                CurrUPH_UI++;
                switch (CurrUPH_UI)
                {
                    case 1:
                        pB_Pallet_1.Image = Resources.ledon;
                        break;
                    case 2:
                        pB_Pallet_2.Image = Resources.ledon;
                        break;
                    case 3:
                        pB_Pallet_3.Image = Resources.ledon;
                        break;
                    case 4:
                        pB_Pallet_4.Image = Resources.ledon;
                        break;
                    case 5:
                        pB_Pallet_5.Image = Resources.ledon;
                        break;
                    case 6:
                        pB_Pallet_6.Image = Resources.ledon;
                        break;
                    case 7:
                        pB_Pallet_7.Image = Resources.ledon;
                        break;
                    case 8:
                        pB_Pallet_8.Image = Resources.ledon;
                        break;
                    case 9:
                        pB_Pallet_9.Image = Resources.ledon;
                        break;
                    case 10:
                        pB_Pallet_10.Image = Resources.ledon;
                        break;
                    case 11:
                        pB_Pallet_11.Image = Resources.ledon;
                        break;
                    case 12:
                        pB_Pallet_12.Image = Resources.ledon;
                        break;
                }
            }
        }

        private void uiRefresh_Tick(object sender, EventArgs e)
        {
            uiRefresh.Enabled = false;
            
            #region Feeders
            if (dbEngineFeeder1.isError)
            {
                lblFeeder1.Text = "Error";
                lblFeeder1.ForeColor = Color.Red;
                pBFeeder1.Image = Resources.Error64x64;
            }
            else if (dbEngineFeeder1.isOnline)
            {
                lblFeeder1.Text = dbEngineFeeder1.isBypass ? "Bypass" : "Online";
                lblFeeder1.ForeColor = Color.LimeGreen;
                pBFeeder1.Image = Resources.Tick64x64;
            }
            else
            {
                lblFeeder1.Text = "Offline";
                lblFeeder1.ForeColor = Color.Silver;
                pBFeeder1.Image = Resources.Error64x64;
            }

            if (dbEngineFeeder2.isError)
            {
                lblFeeder2.Text = "Error";
                lblFeeder2.ForeColor = Color.Red;
                pBFeeder2.Image = Resources.Error64x64;
            }
            else if (dbEngineFeeder2.isOnline)
            {
                lblFeeder2.Text = dbEngineFeeder2.isBypass ? "Bypass" : "Online";
                lblFeeder2.ForeColor = Color.LimeGreen;
                pBFeeder2.Image = Resources.Tick64x64;
            }
            else
            {
                lblFeeder2.Text = "Offline";
                lblFeeder2.ForeColor = Color.Silver;
                pBFeeder2.Image = Resources.Error64x64;
            }

           
            #region Feeders Reject Count
            lblPurgeBin1.Text = RejectCount.ToString();
            #endregion
            #endregion

            #region Conveyors

            if (SysPara.SystemRun)
            {
                if (Handshake.RobotToConv.Busy)
                {
                    setPalletUI(ConveyorPost.WP2, true, "Executing");
                    setProductUI();
                }

                if (pB_W1.Tag != null)
                    switch (pB_W1.Tag.ToString())
                    {
                        case "cvy1":
                            pB_W1.Image = Resources.lightCvy2;
                            pB_W1.Tag = "cvy2";
                            break;
                        case "cvy2":
                            pB_W1.Image = Resources.lightCvy3;
                            pB_W1.Tag = "cvy3";
                            break;
                        case "cvy3":
                            pB_W1.Image = Resources.lightCvy1;
                            pB_W1.Tag = "cvy1";
                            break;
                        default:
                            pB_W1.Image = Resources.lightCvy1;
                            pB_W1.Tag = null;
                            break;
                    }

                if (pB_W2.Tag != null)
                    switch (pB_W2.Tag.ToString())
                    {
                        case "cvy1":
                            pB_W2.Image = Resources.lightCvy2;
                            pB_W2.Tag = "cvy2";
                            break;
                        case "cvy2":
                            pB_W2.Image = Resources.lightCvy3;
                            pB_W2.Tag = "cvy3";
                            break;
                        case "cvy3":
                            pB_W2.Image = Resources.lightCvy1;
                            pB_W2.Tag = "cvy1";
                            break;
                        default:
                            pB_W2.Image = Resources.lightCvy1;
                            pB_W2.Tag = null;
                            break;

                    }

                if (pB_W3.Tag != null)
                    switch (pB_W3.Tag.ToString())
                    {
                        case "cvy1":
                            pB_W3.Image = Resources.lightCvy2;
                            pB_W3.Tag = "cvy2";
                            break;
                        case "cvy2":
                            pB_W3.Image = Resources.lightCvy3;
                            pB_W3.Tag = "cvy3";
                            break;
                        case "cvy3":
                            pB_W3.Image = Resources.lightCvy1;
                            pB_W3.Tag = "cvy1";
                            break;
                        default:
                            pB_W3.Image = Resources.lightCvy1;
                            pB_W3.Tag = null;
                            break;
                    }
            }
            #endregion

            #region TCPIP
            if (MiddleLayer.CvyF.tpBarcode.IsConnected)
            {
                lblBarcode.Text = "Online";
                lblBarcode.ForeColor = Color.LimeGreen;
                pBBarcode.Image = Resources.Tick64x64;
            }
            else
            {
                lblBarcode.Text = "Offline";
                lblBarcode.ForeColor = Color.Silver;
                pBBarcode.Image = Resources.Error64x64;
            }

            if (MiddleLayer.CvyF.M2M_Server != null && MiddleLayer.CvyF.M2M_Server.IsRunning)
            {
                lblM2M.Text = "Online";
                lblM2M.ForeColor = Color.LimeGreen;
                pBM2M.Image = Resources.Tick64x64;
                lblM2MClient.Text = MiddleLayer.CvyF.M2M_Server.CurClientCount.ToString();
            }
            else
            {
                lblM2M.Text = "Offline";
                lblM2M.ForeColor = Color.Silver;
                pBM2M.Image = Resources.Error64x64;
                lblM2MClient.Text = "0";
            }
            #endregion

            lblLastCycleTime.Text = (Handshake.Conveyor[(int)ConveyorPost.WP2].CT_LastWorkFlow / (double)1000).ToString("F2") + " s"; ;
            lblCycleTime.Text = (Handshake.Conveyor[(int)ConveyorPost.WP2].CT_WorkFlow.GetCurrentTime() / (double)1000).ToString("F2") + " s";
            lblPassCount.Text = SysPara.WorkOK_Count.ToString();
            lblFailCount.Text = SysPara.WorkNG_Count.ToString();

            #region Pallet Status
            if (SysPara.PalletList[(int)ConveyorPost.WP2].Count > 0 && SysPara.PalletList[(int)ConveyorPost.WP2].Peek().PalletID != null)
            {
                lblPalletStatus.Text = SysPara.PalletList[(int)ConveyorPost.WP2].Peek().Status ? "Pass" : "Null";
                lblPalletStatus.ForeColor = SysPara.PalletList[(int)ConveyorPost.WP2].Peek().Status ? Color.LimeGreen : Color.White;
                lblPalletInfo.Text = "Pallet ID: " + SysPara.PalletList[(int)ConveyorPost.WP2].Peek().PalletID.ToString();
            }
            else if (SysPara.PalletList[(int)ConveyorPost.WP1].Count > 0 && SysPara.PalletList[(int)ConveyorPost.WP1].Peek().PalletID != null)
            {
                lblPalletStatus.Text = SysPara.PalletList[(int)ConveyorPost.WP1].Peek().Status ? "Pass" : "Null";
                lblPalletStatus.ForeColor = SysPara.PalletList[(int)ConveyorPost.WP1].Peek().Status ? Color.LimeGreen : Color.White;
                lblPalletInfo.Text = "Pallet ID: " + SysPara.PalletList[(int)ConveyorPost.WP1].Peek().PalletID.ToString();
            }
            else
            {
                lblPalletStatus.Text = "Null";
                lblPalletStatus.ForeColor = Color.White;
                lblPalletInfo.Text = "Pallet ID: Null";
            }
            #endregion

            #region UPH
            int CrrHour = DateTime.Now.Hour;
            if(_PrevHour != CrrHour)
            {
                SysPara.UPH_Count = 0;
                _PrevHour = CrrHour;
            }
            lblUPH.Text = SysPara.UPH_Count.ToString();
            #endregion

            #region End Lot Mode
            btnEndLot.Enabled = SysPara.SystemInitialOk && SysPara.SystemRun && !SysPara.isBtnStartEnable;
            btnEndLot.FlatAppearance.MouseOverBackColor = btnEndLot.Enabled ? AcuraColors.MouseOver : AcuraColors.Disable;
            btnEndLot.BackColor = btnEndLot.Enabled ? (btnEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
            pBEndLot.Enabled = btnEndLot.Enabled;
            pBEndLot.BackColor = btnEndLot.Enabled ? (pBEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
            lblEndLot.Enabled = btnEndLot.Enabled;
            lblEndLot.BackColor = btnEndLot.Enabled ? (lblEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
            lblEndLot.Text = SysPara.EndLotMode ? "ON" : "OFF";
            #endregion

            uiRefresh.Enabled = true;
        }

        private void uiRefreshThread_Tick(object sender, EventArgs e)
        {
            uiRefreshThread.Enabled = true;

            try
            {
                #region TCPIP
                if (MiddleLayer.CvyF.tpBarcode.IsConnected)
                {
                    lblBarcode.Text = "Online";
                    lblBarcode.ForeColor = Color.LimeGreen;
                    pBBarcode.Image = Resources.Tick64x64;
                }
                else
                {
                    lblBarcode.Text = "Offline";
                    lblBarcode.ForeColor = Color.Silver;
                    pBBarcode.Image = Resources.Error64x64;
                }

                if (MiddleLayer.CvyF.M2M_Server != null && MiddleLayer.CvyF.M2M_Server.IsRunning)
                {
                    lblM2M.Text = "Online";
                    lblM2M.ForeColor = Color.LimeGreen;
                    pBM2M.Image = Resources.Tick64x64;
                    lblM2MClient.Text = MiddleLayer.CvyF.M2M_Server.CurClientCount.ToString();
                }
                else if (MiddleLayer.CvyF.M2M_Client != null && MiddleLayer.CvyF.M2M_Client.IsRunning && MiddleLayer.CvyF.M2M_Client.Connected)
                {
                    lblM2M.Text = "Client";
                    lblM2M.ForeColor = Color.LimeGreen;
                    pBM2M.Image = Resources.Tick64x64;
                }
                else
                {
                    lblM2M.Text = "Offline";
                    lblM2M.ForeColor = Color.Silver;
                    pBM2M.Image = Resources.Error64x64;
                    lblM2MClient.Text = "0";
                }
                #endregion

                lblLastCycleTime.Text = (Handshake.Conveyor[(int)ConveyorPost.WP2].CT_LastWorkFlow / (double)1000).ToString("F2") + " s"; ;
                lblCycleTime.Text = (Handshake.Conveyor[(int)ConveyorPost.WP2].CT_WorkFlow.GetCurrentTime() / (double)1000).ToString("F2") + " s";
                lblPassCount.Text = SysPara.WorkOK_Count.ToString();
                lblFailCount.Text = SysPara.WorkNG_Count.ToString();
                lblFPY.Text = ((double)SysPara.WorkOK_Count / (SysPara.WorkOK_Count + SysPara.WorkNG_Count)).ToString("0.00 %");
                lblLastTakTime.Text = (SysPara.TaktTime.CT_LastWorkFlow / (double)1000).ToString("F2") + " s"; ;
                lblTaktTime.Text = (SysPara.TaktTime.CT_WorkFlow.GetCurrentTime() / (double)1000).ToString("F2") + " s"; ;

                #region Pallet Status
                if (SysPara.PalletList[(int)ConveyorPost.WP2].Count > 0 && SysPara.PalletList[(int)ConveyorPost.WP2].Peek().PalletID != null)
                {
                    lblPalletStatus.Text = SysPara.PalletList[(int)ConveyorPost.WP2].Peek().Status ? "Pass" : "Null";
                    lblPalletStatus.ForeColor = SysPara.PalletList[(int)ConveyorPost.WP2].Peek().Status ? Color.LimeGreen : Color.White;
                    lblPalletInfo.Text = "Pallet ID: " + SysPara.PalletList[(int)ConveyorPost.WP2].Peek().PalletID.ToString();
                }
                else if (SysPara.PalletList[(int)ConveyorPost.WP1].Count > 0 && SysPara.PalletList[(int)ConveyorPost.WP1].Peek().PalletID != null)
                {
                    lblPalletStatus.Text = SysPara.PalletList[(int)ConveyorPost.WP1].Peek().Status ? "Pass" : "Null";
                    lblPalletStatus.ForeColor = SysPara.PalletList[(int)ConveyorPost.WP1].Peek().Status ? Color.LimeGreen : Color.White;
                    lblPalletInfo.Text = "Pallet ID: " + SysPara.PalletList[(int)ConveyorPost.WP1].Peek().PalletID.ToString();
                }
                else
                {
                    lblPalletStatus.Text = "Null";
                    lblPalletStatus.ForeColor = Color.White;
                    lblPalletInfo.Text = "Pallet ID: Null";
                }
                #endregion

                #region UPH
                int CrrHour = DateTime.Now.Hour;
                if (_PrevHour != CrrHour)
                {
                    MiddleLayer.LogF.AddLog(LogType.Production, $"UPH, {SysPara.UPH_Count.ToString()}"); //Log UPH, removable
                    SysPara.UPH_Count = 0;
                    _PrevHour = CrrHour;

                    //Async HouseKeeping //11/28/2022 - add bit to control task
                    if (_houseKeeping == null || (_houseKeeping != null && (_houseKeeping.IsCompleted || _houseKeeping.IsFaulted)))
                        _houseKeeping = Task.Run(() => MiddleLayer.CvyF.HouseKeeping());
                }
                lblUPH.Text = SysPara.UPH_Count.ToString();
                #endregion

                #region End Lot Mode
                btnEndLot.Enabled = SysPara.SystemInitialOk && SysPara.SystemRun && !SysPara.isBtnStartEnable;
                btnEndLot.FlatAppearance.MouseOverBackColor = btnEndLot.Enabled ? AcuraColors.MouseOver : AcuraColors.Disable;
                btnEndLot.BackColor = btnEndLot.Enabled ? (btnEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
                pBEndLot.Enabled = btnEndLot.Enabled;
                pBEndLot.BackColor = btnEndLot.Enabled ? (pBEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
                lblEndLot.Enabled = btnEndLot.Enabled;
                lblEndLot.BackColor = btnEndLot.Enabled ? (lblEndLot.BackColor == AcuraColors.MouseOver ? AcuraColors.MouseOver : AcuraColors.Background) : AcuraColors.Disable;
                lblEndLot.Text = SysPara.EndLotMode ? "ON" : "OFF";
                #endregion

                #region M2M Listbox Display (Invoke)
                if (MiddleLayer.CvyF.UpdateM2MListboxDisplay)
                {
                    MiddleLayer.CvyF.UpdateM2MListboxDisplay = false;

                    //M2M Folder
                    List<FileInfo> sortedFiles_M2M = new DirectoryInfo(MiddleLayer.CvyF.M2M_FolderPath).GetFiles().OrderByDescending(f => f.CreationTime).ToList();

                    if (sortedFiles_M2M.Count >= 3)
                        JSDK.Alarm.Show("6010");

                    if (MiddleLayer.CvyF.lb_M2M.InvokeRequired)
                    {
                        MiddleLayer.CvyF.lb_M2M.BeginInvoke(new Action(() =>
                        {
                            MiddleLayer.CvyF.lb_M2M.BeginUpdate();
                            MiddleLayer.CvyF.lb_M2M.Items.Clear();
                            foreach (var SF_M2M in sortedFiles_M2M)
                            {
                                MiddleLayer.CvyF.lb_M2M.Items.Add(Path.GetFileNameWithoutExtension(SF_M2M.Name));
                            }
                            MiddleLayer.CvyF.lb_M2M.EndUpdate();
                        }));
                    }
                    else
                    {
                        MiddleLayer.CvyF.lb_M2M.BeginUpdate();
                        MiddleLayer.CvyF.lb_M2M.Items.Clear();
                        foreach (var SF_M2M in sortedFiles_M2M)
                        {
                            MiddleLayer.CvyF.lb_M2M.Items.Add(Path.GetFileNameWithoutExtension(SF_M2M.Name));
                        }
                        MiddleLayer.CvyF.lb_M2M.EndUpdate();
                    }

                    //M2M_Backup Folder
                    List<FileInfo> sortedFiles_M2M_Backup = new DirectoryInfo(MiddleLayer.CvyF.M2M_Backup_FolderPath).GetFiles().OrderByDescending(f => f.CreationTime).ToList();

                    if (sortedFiles_M2M_Backup.Count > 30)
                    {
                        sortedFiles_M2M_Backup.RemoveRange(30, sortedFiles_M2M_Backup.Count - 30);
                    }

                    if (MiddleLayer.CvyF.lb_M2M_Backup.InvokeRequired)
                    {
                        MiddleLayer.CvyF.lb_M2M_Backup.BeginInvoke(new Action(() =>
                        {
                            MiddleLayer.CvyF.lb_M2M_Backup.BeginUpdate();
                            MiddleLayer.CvyF.lb_M2M_Backup.Items.Clear();
                            foreach (var SF_M2M_Backup in sortedFiles_M2M_Backup)
                            {
                                MiddleLayer.CvyF.lb_M2M_Backup.Items.Add(Path.GetFileNameWithoutExtension(SF_M2M_Backup.Name));
                            }
                            MiddleLayer.CvyF.lb_M2M_Backup.EndUpdate();
                        }));
                    }
                    else
                    {
                        MiddleLayer.CvyF.lb_M2M_Backup.BeginUpdate();
                        MiddleLayer.CvyF.lb_M2M_Backup.Items.Clear();
                        foreach (var SF_M2M_Backup in sortedFiles_M2M_Backup)
                        {
                            MiddleLayer.CvyF.lb_M2M_Backup.Items.Add(Path.GetFileNameWithoutExtension(SF_M2M_Backup.Name));
                        }
                        MiddleLayer.CvyF.lb_M2M_Backup.EndUpdate();
                    }
                }
                #endregion

                #region M2M Data Display (Invoke)
                if (MiddleLayer.CvyF.Update_M2M_DataDisplay)
                {
                    MiddleLayer.CvyF.Update_M2M_DataDisplay = false;

                    if (MiddleLayer.CvyF.rtb_M2M_DataDisplay.InvokeRequired)
                    {
                        MiddleLayer.CvyF.rtb_M2M_DataDisplay.BeginInvoke(new Action(() =>
                        {
                            MiddleLayer.CvyF.rtb_M2M_DataDisplay.Text = MiddleLayer.CvyF.M2M_DataDisplay;
                        }));
                    }
                    else
                    {
                        MiddleLayer.CvyF.rtb_M2M_DataDisplay.Text = MiddleLayer.CvyF.M2M_DataDisplay;
                    }
                }
                #endregion
            }
            catch (Exception) { }

            uiRefreshThread.Enabled = true;
        }

        #endregion

        #region Flow Chart Init
        private FCResultType fcInitialStart_FlowRun(object sender, EventArgs e)
        {
      
            return FCResultType.NEXT;
        }

        private FCResultType fcInitRetractTurretHead_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitCheckPartPresent_FlowRun(object sender, EventArgs e)
        {
           
            return FCResultType.NEXT;
        }

        private FCResultType fcInitMoveZtoSafe_FlowRun(object sender, EventArgs e)
        {
         
            return FCResultType.NEXT;
        }

        private FCResultType fcInitGotoHoming_FlowRun(object sender, EventArgs e)
        {
           
            return FCResultType.NEXT;
        }

        private FCResultType fcInitCheckTool_FlowRun(object sender, EventArgs e)
        {
                return FCResultType.NEXT;
        }

        private FCResultType fcInitEnd_FlowRun(object sender, EventArgs e)
        {
            if (!bInitialOk)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Robot,Initialize Flow Done");
            }
            bInitialOk = true;
            return FCResultType.IDLE;
        }

        private FCResultType fcInitTriggerPurge_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitWaitPurge_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        private FCResultType fcStartFlow_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            if (b_Abnormal)
            {
                JSDK.Alarm.Show("9036", " ABBRobotFormAbnormal");
                fcM_Fail.Content = "Error";
                b_Abnormal = false;
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        public bool b_Abnormal;
        private void button8_Click(object sender, EventArgs e)
        {
            b_Abnormal = true;
        }
    }

    public class RBSeqItem
    {
        public string PointName;
        public string Mode;
        public double X;
        public double Y;
        public double Z;
        public double W;
        public int BDelay;
        public int ADelay;
        public int Index;
        public string Move = "JMove";
        public string Arm = "Right";
        public string Stack;
        public int Speed;
        public int ZSpeed;
        public bool Enabled;
        public string ID;
        public string TeachPost;
    }

    public class RobotPosDataList
    {
        public List<RobotPosData> Post = new List<RobotPosData>();
    }

}