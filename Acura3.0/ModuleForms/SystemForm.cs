using Acura3._0.Forms;
using Acura3._0.FunctionForms;
using AcuraLibrary.Forms;
using JabilSDK;
using JabilSDK.Controls;
using SASDK;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using static Acura3._0.Forms.SignalTowerForm;
using System.Drawing;

namespace Acura3._0.ModuleForms
{
    public partial class SystemForm : ModuleBaseForm
    {
        public MESForm MesF = new MESForm();

        private bool StatusChange_Start = false;
        private bool StatusChange_Stop = false;
        private bool StatusChange_AlarmReset = false;
        private bool StatusChange_ModeSwitch = false;
        private bool StatusChange_SafetyDoor = false;
        private bool StatusChange_Emergency = false;
        private bool StatusChange_AirPressure = false;
        public bool isEMO = false;
        private JTimer BlinkTM = new JTimer();
        private JTimer BuzzerTM = new JTimer();
        private bool BlinkIsOn = false;
        private const int BlinkTime = 500;
        public bool BuzzerNeedWork = false;

        private string CurrentRecipe = null;//Teong CFX 

        public List<MotorParameter> MotorMaximumParameter = new List<MotorParameter>();
        public struct MotorParameter
        {
            public string Port;
            public double MaximumSpeed;
        }

        public enum WorkStatus
        {
            WaitProcess,
            Processing,
            Pass,
            Fail

        }

        public SystemForm()
        {
            InitializeComponent();
            MesF.TopLevel = false;
            //groupBox1.Height = panel1.Height = MesF.Height;
            // groupBox1.Width = panel1.Width = MesF.Width;
            //this.panel1.Controls.Add(MesF);
            MesF.Show();
            FlowChartMessage.MessageFormRaise += FlowChartMessage_MessageFormRaise;
            MessageForm.MuteRaise += MessageForm_MuteRaise;
            //SetDoubleBuffer(panel1);
            SetDoubleBuffer(panel2);
            //SetDoubleBuffer(ucDBConfigurator1);
            //SetDoubleBuffer(ucDBConfigurator2);
        }

        public void ShowStatus(int Station, WorkStatus workStatus)
        {
            Label label = new Label();
            switch (Station)
            {
                case 1:
                    label = L_Station1ShowStatus;
                    break;
                case 2:
                    label = L_Station2ShowStatus;
                    break;
                case 3:
                    label = L_Station3ShowStatus;
                    break;
                case 4:
                    label = L_Station4ShowStatus;
                    break;
                default:
                    break;
            }
            switch (workStatus)
            {
                case WorkStatus.WaitProcess:
                    label.BackColor = Color.White;
                    label.Text = "Wait Process";
                    break;
                case WorkStatus.Processing:
                    label.BackColor = Color.Yellow;
                    label.Text = "Processing";
                    break;
                case WorkStatus.Pass:
                    label.BackColor = Color.Green;
                    label.Text = "Pass";
                    break;
                case WorkStatus.Fail:
                    label.BackColor = Color.Red;
                    label.Text = "Fail";
                    break;
            }

        }

        private void MessageForm_MuteRaise(object sender, EventArgs e)
        {
            BuzzOff();
            //MiddleLayer.SystemF.BuzzOff();
        }

        private void FlowChartMessage_MessageFormRaise(object sender, EventArgs e)
        {
            BuzzerNeedWork = true;            
        }

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        public override void AfterRecipeEditor()
        {
            //MesF.WriteMesTisConfig(SysPara.MESDirectory + "\\" + SysPara.RecipeName);
        }

        public override void AfterProductionSetting()
        {
            MesF.WriteMesTisConfig(SysPara.MESDirectory + "\\" + SysPara.RecipeName);
        }
        public override void InitialReset()
        {
           
        }
        public override void AlwaysRun()
        {
            if (CurrentRecipe != SysPara.RecipeName)
            {
                //Teong CFX
                CurrentRecipe = SysPara.RecipeName;
                //DBEngineCFX.RecipeActivated(SysPara.RecipeName);
            }

            if (SysPara.Simulation)
                return;

            #region BlinkTimer
            if (BlinkTM.IsOn(BlinkTime))
            {
                BlinkIsOn = !BlinkIsOn;
                BlinkTM.Restart();
            }
            #endregion

            #region MaintainMode Scan
            if (IB_Front_ManualSwtitch1.IsOn() || IB_Front_ManualSwtitch2.IsOn() || IB_Back_ManualSwtitch.IsOn())
            {
                if (!StatusChange_ModeSwitch)
                {
                    MiddleLayer.StopRun();
                    //BackupMotorMaximumSpeed();
                    //SwitchMotorToMaintenanceSpeed();
                    OB_FluorescentLight1.On();
                    OB_FluorescentLight2.On();
                    MiddleLayer.AP_PCBA_V.OB_Robot_Maintain.On();
                    MiddleLayer.CoverF.OB_Robot_Maintain.On();
                    //OB_DoorInterlock.On();
                    StatusChange_ModeSwitch = true;
                }
                SysPara.IsMaintenanceMode = true;
                SysPara.MState.IsMaintenanceMode = true;
            }
            else
            {
                if (StatusChange_ModeSwitch)
                {
                    MiddleLayer.StopRun();
                    //RestoreMotorMaximumSpeed();
                    //
                    //OB_DoorInterlock.Off();
                    StatusChange_ModeSwitch = false;
                }
                SysPara.IsMaintenanceMode = false;
                SysPara.MState.IsMaintenanceMode = false;
            }
            #endregion

            #region Emergency Stop Scan
            if (IB_SafetyReady.IsOff())
            {
                if (StatusChange_Emergency)
                {
                    SysPara.SystemRun = false;
                    MiddleLayer.SetModulesStop();
                    SysPara.SystemMode = RunMode.IDLE;
                    SysPara.SystemInitialOk = false;
                    StatusChange_Emergency = false;

                }
                JSDK.Alarm.Show("1020");
                isEMO = true;
            }
            else
            {
                if (!StatusChange_Emergency)
                    StatusChange_Emergency = true;
                isEMO = false;
            }
            #endregion

            #region Safety door Scan
            if (IB_ConveyorADoor.IsOff() || IB_ConveyorBDoor.IsOff())
            {
                if (!StatusChange_SafetyDoor)
                {
                    OB_FluorescentLight1.Off();
                    OB_FluorescentLight2.Off();
                    StatusChange_SafetyDoor = true;
                }
                if (IB_Front_ManualSwtitch1.IsOff() || IB_Front_ManualSwtitch2.IsOff() || IB_Back_ManualSwtitch.IsOff())
                {
                    if (!GetSettingValue("PSet", "DisableSafetyDoor"))
                    {
                        if (IB_ConveyorADoor.IsOff())
                        {
                            JSDK.Alarm.Show("1023");
                        }
                        if (IB_ConveyorBDoor.IsOff())
                        {
                            JSDK.Alarm.Show("1024");
                        }
                    }
                }                       
            }
            else
            {
                if (StatusChange_SafetyDoor)
                {
                    JSDK.Alarm.Clear();
                    OB_FluorescentLight1.On();
                    OB_FluorescentLight2.On();
                    StatusChange_SafetyDoor = false;
                }
            }
            #endregion

            #region Air Pressure Scan
            if (!IB_AirPressure.IsOn())
            {
                if (StatusChange_AirPressure)
                    StatusChange_AirPressure = false;

                SysPara.SystemMode = RunMode.IDLE; //Zax
                SysPara.SystemInitialOk = false;
                JSDK.Alarm.Show("1022");
            }
            else
            {
                if (!StatusChange_AirPressure)
                {
                    JSDK.Alarm.Clear();
                    StatusChange_AirPressure = true;
                }
            }
            #endregion

            #region Start Button Scan

            if ((IB_BtnStart1.IsOn()||IB_BtnStart2.IsOn()|| IB_BtnStart3.IsOn()) && !SysPara.IsMaintenanceMode)
            {
                if (StatusChange_Start)
                {
                    StatusChange_Start = false;
                    RefreshDifferentThreadUI(MiddleLayer.MainF.btnStart, () =>
                    {
                        if (MiddleLayer.MainF.btnStart.Enabled)
                            MiddleLayer.MainF.btnStart.PerformClick();

                    });
                }
            }
            else
            {
                if (!StatusChange_Start)
                    StatusChange_Start = true;
            }
            #endregion

            #region Stop Button Scan
            if (IB_BtnStop1.IsOff()|| IB_BtnStop2.IsOff()|| IB_BtnStop3.IsOff())
            {
                if (StatusChange_Stop)
                {
                    StatusChange_Stop = false;
                    RefreshDifferentThreadUI(MiddleLayer.MainF.btnPause, () =>
                    {
                        if (MiddleLayer.MainF.btnPause.Enabled)
                            MiddleLayer.MainF.btnPause.PerformClick();
                    });
                }
                BuzzOff();
            }
            else
            {
                if (!StatusChange_Stop)
                    StatusChange_Stop = true;
            }
            #endregion

            #region Alarm Reset Button Scan
            if (IB_BtnAlarmReset1.IsOn()|| IB_BtnAlarmReset2.IsOn()|| IB_BtnAlarmReset2.IsOn())
            {
                if (StatusChange_AlarmReset)
                {
                    StatusChange_AlarmReset = false;
                    RefreshDifferentThreadUI(MiddleLayer.MainF.btnAlarmReset, () =>
                    {
                        MiddleLayer.MainF.btnAlarmReset.PerformClick();
                    });

                }
                BuzzOff();
                BuzzerTM.Restart();
            }
            else
            {
                if (!StatusChange_AlarmReset)
                    StatusChange_AlarmReset = true;
            }
            #endregion

            #region Button Light
            if (SysPara.SystemInitialOk)
            {
                if (SysPara.IsMaintenanceMode)
                {
                    if (BlinkIsOn)
                    {
                        OB_Start_Stop_ButtonLED1.On();
                        OB_Start_Stop_ButtonLED2.On();
                        OB_Start_Stop_ButtonLED3.On();
                    }
                    else
                    {
                        OB_Start_Stop_ButtonLED1.Off();
                        OB_Start_Stop_ButtonLED2.Off();
                        OB_Start_Stop_ButtonLED3.Off();
                    }
                }
                else
                {
                    OB_Start_Stop_ButtonLED1.On();
                    OB_Start_Stop_ButtonLED2.On();
                    OB_Start_Stop_ButtonLED3.On();
                }
            }
            else
            {
                OB_Start_Stop_ButtonLED1.Off();
                OB_Start_Stop_ButtonLED2.Off();
                OB_Start_Stop_ButtonLED3.Off();
            }
            #endregion

            #region Signal Tower Control 
            if (SysPara.MState.IsMaintenanceMode)
            {
                MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MessageError);
                SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.SDT); //Maintenance
            }
            else if (JSDK.Alarm.IsError)
            {
                if (SysPara.MState.IsMachineNoMaterial)
                {
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineNoMaterial);
                    if (!BuzzerNeedWork)
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_Repair); //Under Repair
                    else
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_ChangeOfConsumables); //Feeder No Material
                }
                else
                {
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MessageError);
                    if (!BuzzerNeedWork)
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_Repair); //Under Repair
                    else
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD); //ALARM
                }
            }
            else if (SysPara.SystemRun)
            {
                if (SysPara.SystemMode == RunMode.RUN)
                {
                    if (SysPara.MState.IsMachineNoMaterial)
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineNoMaterial);
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_ChangeOfConsumables); //Feeder No Material
                    }
                    else if (SysPara.MState.IsMachineBlocked)
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineBlocked);
                        SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.SBY_NoProductBlocked); //Blocked
                    }
                    else if (SysPara.MState.IsMachineStarving)
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineStarving);
                        SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.SBY_NoProductStarved); //Starving
                    }
                    else if (SysPara.MState.IsMachineRetryProcess)
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineRetryProcess);
                        SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.PRD); //Run(for Retry process)
                    }
                    else if (SysPara.MState.IsMachineLowMaterial)
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineLowMaterial);
                        SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.PRD_RegularWork); //Low Material
                    }
                    else
                    {
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineRunning);
                        SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.PRD); //Run
                    }
                }
                else if (SysPara.SystemMode == RunMode.INITIAL)
                {
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineInitialize);
                    SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.PRD); //Run
                }
            }
            else // Stop
            {
                if (SysPara.MState.IsMachineNoMaterial)
                {
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerStatusType.MachineNoMaterial);
                    if (!BuzzerNeedWork)
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_Repair); //Under Repair
                    else
                        SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.USD_ChangeOfConsumables); //Feeder No Material
                }
                else
                {
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(JSDK.Alarm.IsWarning ? SignalTowerStatusType.MessageWarning : SignalTowerStatusType.MachineIdle);
                    SysPara.CFX.StationStateChanged(JSDK.Alarm.IsWarning ? CFX.Structures.ResourceState.PRD_Engineering : CFX.Structures.ResourceState.NST); //IDLE
                }
            }

            //Control Signal Tower
            Control_SignalTower(MiddleLayer.SignalTowerF.GreenLightStatus, OB_GreenLight1);
            Control_SignalTower(MiddleLayer.SignalTowerF.YellowLightStatus, OB_YellowLight1);
            Control_SignalTower(MiddleLayer.SignalTowerF.RedLightStatus, OB_RedLight1);
            Control_SignalTower(MiddleLayer.SignalTowerF.GreenLightStatus, OB_GreenLight2);
            Control_SignalTower(MiddleLayer.SignalTowerF.YellowLightStatus, OB_YellowLight2);
            Control_SignalTower(MiddleLayer.SignalTowerF.RedLightStatus, OB_RedLight2);

            if (MiddleLayer.SignalTowerF.bHaveChangeStatus)
            {
                BuzzerNeedWork = true;
                MiddleLayer.SignalTowerF.bHaveChangeStatus = false;
            }
            if (BuzzerNeedWork && !SysPara.isSettingRefresh && !GetSettingValue("PSet", "DisableSafetyBuzzer") && BuzzerTM.IsOn(1000))
            {
                Control_SignalTower(MiddleLayer.SignalTowerF.BuzzerStatus, OB_Buzzer1);
                Control_SignalTower(MiddleLayer.SignalTowerF.BuzzerStatus, OB_Buzzer2);
            }
            else
                BuzzerNeedWork = false;
            #endregion
            /*
            #region Signal Tower Control
            if (SysPara.SystemRun)
            {
                if (JSDK.Alarm.IsError)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MessageError);
                else if (JSDK.Alarm.IsWarning)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MessageWarning);
                else if (JSDK.Alarm.IsInformation)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MessageInformation);
                else
                {
                    if (SysPara.SystemMode == RunMode.RUN)
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MachineRunning);
                    if (SysPara.SystemMode == RunMode.INITIAL)
                        MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MachineInitialize);
                }
            }
            else
            {
                if (JSDK.Alarm.IsError)
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MessageError);
                else
                    MiddleLayer.SignalTowerF.SwitchSignalTowerStatus(SignalTowerForm.SignalTowerStatusType.MachineIdle);
            }
            //Refresh Output Status
            Control_SignalTower(MiddleLayer.SignalTowerF.GreenLightStatus, OB_GreenLight);
            Control_SignalTower(MiddleLayer.SignalTowerF.YellowLightStatus, OB_YellowLight);
            Control_SignalTower(MiddleLayer.SignalTowerF.RedLightStatus, OB_RedLight);
            if (MiddleLayer.SignalTowerF.bHaveChangeStatus)
            {
                BuzzerNeedWork = true;
                MiddleLayer.SignalTowerF.bHaveChangeStatus = false;
            }
            if (BuzzerNeedWork && !SysPara.isSettingRefresh && !GetSettingValue("PSet", "DisableSafetyBuzzer") && BuzzerTM.IsOn(1000))
                Control_SignalTower(MiddleLayer.SignalTowerF.BuzzerStatus, OB_Buzzer);
            else
                BuzzerNeedWork = false;
            #endregion*/
        }

        public override void StartRun()
        {
            if (bInitialOk)
            {
                MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineRun);
                //DBEngineCFX.StationStateChanged(SASDK.DBEngine.ucDBEngineCFX.StateChange.Productive, DateTime.Now); //Teong CFX
                //if (SysPara.EnableJAGConnectivity)
                //{
                //    MiddleLayer.SystemF.MesF.SendMachineStatus(LogForm.MachineStatusType.MachineRun);
                //}
            }
        }

        public override void StopRun()
        {
            MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineIdle);
            if (SysPara.EnableJAGConnectivity)
            {
                MiddleLayer.SystemF.MesF.SendMachineStatus(MachineStatusType.MachineIdle);
            }
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

        public void BackupMotorMaximumSpeed()
        {
            MotorMaximumParameter.Clear();
            foreach (ControlBaseInterface control in JSDK.ControlList)
                if (control is Motor)
                {
                    MotorParameter d = new MotorParameter();
                    d.Port = ((Motor)control).Port;
                    d.MaximumSpeed = ((Motor)control).MaximumSpeed;
                    MotorMaximumParameter.Add(d);
                }
            //MotorParameter robotSpeed = new MotorParameter();
            //robotSpeed.Port = "1";
            //robotSpeed.MaximumSpeed = MiddleLayer.AbbF.ABB_Robot.WorkSpeed;
            //MotorMaximumParameter.Add(new MotorParameter { Port = "1", MaximumSpeed = MiddleLayer.AbbF.ABB_Robot.WorkSpeed });
            //MotorMaximumParameter.Add(new MotorParameter { Port = "2", MaximumSpeed = MiddleLayer.AbbF.ABB_Robot.WorkRSpeed });
        }

        public void RestoreMotorMaximumSpeed()
        {
            foreach (ControlBaseInterface control in JSDK.ControlList)
                if (control is Motor)
                {
                    foreach (MotorParameter d in MotorMaximumParameter)
                    {
                        string port = ((Motor)control).Port;
                        if (port == d.Port)
                            ((Motor)control).MaximumSpeed = d.MaximumSpeed;

                        //if(d.Port == "1")
                        //{
                        //    MiddleLayer.AbbF.ABB_Robot.WorkSpeed = d.MaximumSpeed;
                        //}
                    }
                }

            //foreach (MotorParameter d in MotorMaximumParameter)
            //{
            //    if (d.Port == "1")
            //        MiddleLayer.AbbF.ABB_Robot.WorkSpeed = d.MaximumSpeed;
            //    else if (d.Port == "2")
            //        MiddleLayer.AbbF.ABB_Robot.WorkRSpeed = d.MaximumSpeed;
            //}
        }

        private void SwitchMotorToMaintenanceSpeed()
        {
            foreach (ControlBaseInterface control in JSDK.ControlList)
                if (control is Motor)
                    if (((Motor)control).MaximumSpeed > 33)
                    {
                        ((Motor)control).MaximumSpeed = 33;
                    }

            //MiddleLayer.AbbF.ABB_Robot.WorkSpeed = 33;
            //MiddleLayer.AbbF.ABB_Robot.WorkRSpeed = 33;
        }

        private void Control_SignalTower(Status status, Output OB)
        {
            switch (status)
            {
                case Status.Off:
                    OB.Off();
                    break;
                case Status.On:
                    OB.On();
                    break;
                case Status.Blink:
                    if (BlinkIsOn)
                        OB.On();
                    else
                        OB.Off();
                    break;
            }
        }

        public void BuzzOff()
        {
            BuzzerNeedWork = false;
            //MiddleLayer.SystemF.DBEngineCFX.FaultAcknowledged(SysPara.UserName);
            OB_Buzzer1.Off();
            OB_Buzzer2.Off();
        }

        public void BuzzOn()
        {
            BuzzerNeedWork = true;
            OB_Buzzer1.On();
            OB_Buzzer2.On();
        }


        public bool DelayMs(int delayMilliseconds)
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

        private void UIShowTimer_Tick(object sender, EventArgs e)
        {
            lblLastCycleTime.Text = SysPara.Station1Para.LastCT;
            lblLastTakTime.Text = SysPara.Station1Para.LastTaktTime;
            lblCycleTime.Text = SysPara.Station1Para.CurrCT;
            lblTaktTime.Text = SysPara.Station1Para.CurrTaktTime;

            label27.Text = SysPara.Station2Para.LastCT;
            label20.Text = SysPara.Station2Para.LastTaktTime;
            label32.Text = SysPara.Station2Para.CurrCT;
            label18.Text = SysPara.Station2Para.CurrTaktTime;

            label55.Text = SysPara.Station3Para.LastCT;
            label50.Text = SysPara.Station3Para.LastTaktTime;
            label57.Text = SysPara.Station3Para.CurrCT;
            label48.Text = SysPara.Station3Para.CurrTaktTime;

            label67.Text = SysPara.Station4Para.LastCT;
            label62.Text = SysPara.Station4Para.LastTaktTime;
            label69.Text = SysPara.Station4Para.CurrCT;
            label60.Text = SysPara.Station4Para.CurrTaktTime;
        }
    }
}
