using AcuraLibrary.Forms;
using CFX.Structures.JAG;
using JabilSDK;
using JabilSDK.Controls;
using JabilSDK.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SystemCommon.Communication;
using static Acura3._0.FunctionForms.LogForm;

namespace Acura3._0.ModuleForms
{
    public partial class ReturnConveyorForm : ModuleBaseForm
    {
        #region Forms & Init
        private enum ConveyorState
        {
            Stop = 0,
            Forward,
            FastForward
        }

        private int StdTime = 100;
        private JTimer RunTM_BC = new JTimer();
        public bool StopRunFlag = false;
        public bool BottomConveyorAlarm = false;
        public bool BottomConveyorIsLoading = false;
        public bool BottomConveyorIsUnloading = false;

        public ReturnConveyorForm()
        {
            InitializeComponent();
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
            if (MiddleLayer.SystemF.isEMO)
                ExecuteBottomConveyor(ConveyorState.Stop);

            if (IB_BtmCvyError.IsOn() && !SysPara.Simulation)
            {
                JSDK.Alarm.Show("9500");
            }

            if (SysPara.SystemInitialOk && !BottomConveyorAlarm && SysPara.SystemMode==RunMode.RUN)
            {
                fcBtmMainFlow.TaskRun();
            }

            if (StopRunFlag && !SysPara.SystemInitialOk)
            {
                ExecuteBottomConveyor(ConveyorState.Stop);
                OB_SMEMA_BTM_DW_MachineRdy.Off();
                OB_SMEMA_BTM_UP_Available.Off();
                StopRunFlag = false;
            }
        }

        public override void InitialReset()
        {
            fcInitBtmFlow.TaskReset();
            fcBtmMainFlow.TaskReset();

            OB_SMEMA_BTM_DW_MachineRdy.Off();
            OB_SMEMA_BTM_UP_Available.Off();
            ExecuteBottomConveyor(ConveyorState.Stop);
            BottomConveyorAlarm = false;
            StopRunFlag = false;

        }

        public override void RunReset()
        {

        }

        public override void Initial()
        {
            fcInitBtmFlow.TaskRun();
        }

        public override void Run()
        {

        }

        public override void StartRun()
        {
            RunTM_BC.Restart();

        }

        public override void StopRun()
        {
            StopRunFlag = true;
        }
        #endregion

        #region Functions
        private bool Delay(JTimer timer, int TimeOut)
        {
            bool ret = false;
            if (timer.IsOn(TimeOut))
            {
                ret = true;
            }
            return ret;
        }

        private void ExecuteBottomConveyor(ConveyorState RunState)
        {
            switch (RunState)
            {
                case ConveyorState.Stop:
                    OB_BtmCvy_Forward.Off();
                    break;
                case ConveyorState.FastForward:
                    OB_BtmCvy_Forward.On();
                    break;
            }
        }
        #endregion

        #region Button & Event
        #endregion

        #region UI Update
        private void CvyTim_UpdateUI_Tick(object sender, EventArgs e)
        {

        }
        #endregion

        #region Initialize Flow 
        private FCResultType fcInitBtmFlow_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor,Initialize Flow Start");
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (GetSettingValue("PSet", "BypassConveyor"))
                return FCResultType.CASE1;

            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmCheckPart_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (BottomConveyorIsUnloading)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Go Unloading Flow");
                return FCResultType.CASE2;
            }
            else if (BottomConveyorIsLoading)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Go Loading Flow");
                return FCResultType.CASE1;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init No Part On Conveyor");
                return FCResultType.NEXT;
            }
        }

        private FCResultType fcInitBtmLoadWaitSMEMADownStream_FlowRun(object sender, EventArgs e)
        {
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (isSMEMA)
            {
                OB_SMEMA_BTM_DW_MachineRdy.On();
            }
            if (isSMEMA && IB_BtmBoardOut.IsOff(100) && IB_SMEMA_BTM_DN_Available.IsOn(100))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Turn On Conveyor");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            if (!isSMEMA)
            {
                if (IB_BtmBoardIn.IsOn(1000))
                {
                    RunTM_BC.Restart();
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Turn On Conveyor");
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmLoadConvRun_FlowRun(object sender, EventArgs e)
        {
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (isSMEMA)
            {
                OB_SMEMA_BTM_DW_MachineRdy.On();
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            if (Delay(RunTM_BC, 500))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Wait Board In On");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmLoadWaitBoardInOn_FlowRun(object sender, EventArgs e)
        {
            int LoadTime = GetSettingValue("PSet", "PalletLoadTimeout");
            if (Delay(RunTM_BC, LoadTime))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Load Time Out");
                JSDK.Alarm.Show("9503");
                ExecuteBottomConveyor(ConveyorState.Stop);
                OB_SMEMA_BTM_DW_MachineRdy.Off();
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (isSMEMA) OB_SMEMA_BTM_DW_MachineRdy.On();
            if (IB_BtmBoardIn.IsOn(500))
            {
                if (Delay(RunTM_BC, 100))
                {
                    RunTM_BC.Restart();
                    if (isSMEMA)
                    {
                        OB_SMEMA_BTM_DW_MachineRdy.Off();
                        //OB_SMEMA_BTM_UP_PalletAvali.On();
                    }
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Wait Board In Off");
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmLoadConvStop_FlowRun(object sender, EventArgs e)
        {
            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (IB_BtmBoardOut.IsOn())
            {
                //Stop board
                ExecuteBottomConveyor(ConveyorState.Stop);
                if (Delay(RunTM_BC, 500))
                {
                    RunTM_BC.Restart();
                    if (isSMEMA)
                    {
                        OB_SMEMA_BTM_DW_MachineRdy.Off();
                    }
                    BottomConveyorIsLoading = false;
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Check Downstream Board Available");
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmUnloadConvRun_FlowRun(object sender, EventArgs e)
        {
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (isSMEMA) OB_SMEMA_BTM_UP_Available.On();
            ExecuteBottomConveyor(ConveyorState.FastForward);
            MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Turn On Conveyor And Wait Board Out On");
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmUnloadWaitBoardOutOn_FlowRun(object sender, EventArgs e)
        {
            int UnloadTime = GetSettingValue("PSet", "PalletUnloadTimeout");
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (Delay(RunTM_BC, UnloadTime))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Unload Time Out");
                JSDK.Alarm.Show("9504");
                ExecuteBottomConveyor(ConveyorState.Stop);
                OB_SMEMA_BTM_UP_Available.Off();
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            if (isSMEMA) OB_SMEMA_BTM_UP_Available.On();
            if (IB_BtmBoardOut.IsOn(500))
            {
                ExecuteBottomConveyor(ConveyorState.FastForward);
                if (isSMEMA)
                    OB_SMEMA_BTM_UP_Available.On();
                RunTM_BC.Restart();
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Wait Board Out Off");
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcinitBtmUnloadConvStop_FlowRun(object sender, EventArgs e)
        {
            int UnloadTime = GetSettingValue("PSet", "PalletUnloadTimeout");
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");

            ExecuteBottomConveyor(ConveyorState.FastForward);
            if (isSMEMA) OB_SMEMA_BTM_UP_Available.On();
            if (IB_BtmBoardOut.IsOff(5000))
            {
                //Stop board
                ExecuteBottomConveyor(ConveyorState.Stop);
                if (isSMEMA)
                    OB_SMEMA_BTM_UP_Available.Off();
                BottomConveyorIsUnloading = false;
                RunTM_BC.Restart();
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init End");
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
        private FCResultType fcInitBtmCheckSensor_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            if (IB_BtmBoardOut.IsOff())
            {
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }
            else
            {
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
        }
        private FCResultType fcInitBtmRunConv_FlowRun(object sender, EventArgs e)
        {
            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (IB_BtmBoardOut.IsOn() || Delay(RunTM_BC, 10000))
            {
                ExecuteBottomConveyor(ConveyorState.Stop);
                if (isSMEMA)
                {
                    OB_SMEMA_BTM_DW_MachineRdy.Off();
                }
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Init Turn Off Conveyor");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmFlowEnd_FlowRun(object sender, EventArgs e)
        {
            if (!bInitialOk)
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Initialize Flow Done");
            bInitialOk = true;
            return FCResultType.IDLE;
        }

        private FCResultType fcInitBtmLoadWaitStart_FlowRun(object sender, EventArgs e)
        {
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmUnloadWaitStart_FlowRun(object sender, EventArgs e)
        {
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        #endregion

        #region Main Flow
        private FCResultType fcBtmMainFlow_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (IB_BtmBoardOut.IsOn())
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Start Wait Upstream SMEMA");
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }
            else
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Start Wait Downstream SMEMA");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
        }

        private FCResultType fcBtmMainWaitSmema_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun)
            {
                OB_SMEMA_BTM_DW_MachineRdy.On();
            }
            if (isDryrun)
            {
                RunTM_BC.Restart();
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                return FCResultType.NEXT;
            }

            if (isSMEMA && IB_BtmBoardOut.IsOff(100) && IB_SMEMA_BTM_DN_Available.IsOn(100))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            if (!isSMEMA)
            {
                if (IB_BtmBoardIn.IsOn(1000))
                {
                    RunTM_BC.Restart();
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainCovRun_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Wait Board In On");
                ExecuteBottomConveyor(ConveyorState.FastForward);
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun)
            {
                OB_SMEMA_BTM_DW_MachineRdy.On();
                BottomConveyorIsLoading = true;
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            if (Delay(RunTM_BC, 500))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Wait Board In On");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainWaitBoardInOn_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            int LoadTime = GetSettingValue("PSet", "PalletLoadTimeout");

            if (Delay(RunTM_BC, LoadTime))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Loading Time Out");
                JSDK.Alarm.Show("9501");
                ExecuteBottomConveyor(ConveyorState.Stop);
                OB_SMEMA_BTM_DW_MachineRdy.Off();
                BottomConveyorAlarm = true;
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (isSMEMA) OB_SMEMA_BTM_DW_MachineRdy.On();
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                if (Delay(RunTM_BC, 5000))
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Wait Board In Off");
                    ExecuteBottomConveyor(ConveyorState.FastForward);
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (IB_BtmBoardIn.IsOn(500))
            {
                //if (Delay(RunTM_BC, 500))
                if (Delay(RunTM_BC, 100))
                {
                    RunTM_BC.Restart();
                    if (isSMEMA)
                    {
                        OB_SMEMA_BTM_DW_MachineRdy.On();
                    }
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Wait Board In Off");
                    return FCResultType.NEXT;
                }
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainCovStop_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                if (Delay(RunTM_BC, 5000))
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Check Downstream SMEMA Board Available");
                    ExecuteBottomConveyor(ConveyorState.Stop);
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (IB_BtmBoardIn.IsOff())
            {
                //Stop board
                ExecuteBottomConveyor(ConveyorState.Stop);
                BottomConveyorIsLoading = false;
                if (Delay(RunTM_BC, 500))
                {
                    RunTM_BC.Restart();
                    if (isSMEMA)
                    {
                        OB_SMEMA_BTM_DW_MachineRdy.Off();
                    }
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Check Downstream SMEMA Board Available");
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainWaitDownSmema_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun) OB_SMEMA_BTM_UP_Available.On();
            if ((isSMEMA && IB_SMEMA_BTM_UP_MachineRdy.IsOn()) || MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                BottomConveyorIsUnloading = true;
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Double Check Available Signal");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            if (!isSMEMA)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                RunTM_BC.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType fcBtmDelay_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun) OB_SMEMA_BTM_UP_Available.On();
            if (Delay(RunTM_BC, 1000))
            {
                if ((isSMEMA && IB_SMEMA_BTM_UP_MachineRdy.IsOn()) || MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
                {
                    BottomConveyorIsUnloading = true;
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                else if (!isSMEMA)
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor");
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                else
                {
                    BottomConveyorIsUnloading = false;
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Retry And Wait Signal");
                    RunTM_BC.Restart();
                    return FCResultType.CASE1;
                }
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainCovRun1_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun) OB_SMEMA_BTM_UP_Available.On();

            ExecuteBottomConveyor(ConveyorState.FastForward);
            MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Turn On Conveyor And Wait Board Out On");
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType fcBtmMainWaitBoardOutOn_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            int UnloadTime = GetSettingValue("PSet", "PalletUnloadTimeout");
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");

            if (Delay(RunTM_BC, UnloadTime))
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Unloading Time Out");
                JSDK.Alarm.Show("9502");
                ExecuteBottomConveyor(ConveyorState.Stop);
                OB_SMEMA_BTM_UP_Available.Off();
                BottomConveyorAlarm = true;
                RunTM_BC.Restart();
                return FCResultType.CASE1;
            }

            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun) OB_SMEMA_BTM_UP_Available.On();
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                if (Delay(RunTM_BC, 5000))
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow Wait Board Out Off");
                    ExecuteBottomConveyor(ConveyorState.Stop);
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            if (IB_BtmBoardOut.IsOn(500))
            {
                ExecuteBottomConveyor(ConveyorState.FastForward);
                if (isSMEMA)
                    OB_SMEMA_BTM_UP_Available.On();
                RunTM_BC.Restart();
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Wait Board Out Off");
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcBtmMainCovStop1_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.Simulation)
            {
                if (Delay(RunTM_BC, StdTime))
                {
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }

            int UnloadTime = GetSettingValue("PSet", "PalletUnloadTimeout");
            bool isSMEMA = GetSettingValue("PSet", "UseSMEMA");

            ExecuteBottomConveyor(ConveyorState.FastForward);
            bool isDryrun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");
            if (isSMEMA && !isDryrun) OB_SMEMA_BTM_UP_Available.On();
            if (MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"))
            {
                if (Delay(RunTM_BC, 10000))
                {
                    MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow End");
                    ExecuteBottomConveyor(ConveyorState.Stop);
                    RunTM_BC.Restart();
                    return FCResultType.NEXT;
                }
                return FCResultType.IDLE;
            }
            if (IB_BtmBoardOut.IsOff())
                BottomConveyorIsUnloading = false;
            if (IB_BtmBoardOut.IsOff(1500))
            {
                //Stop board
                ExecuteBottomConveyor(ConveyorState.Stop);
                if (isSMEMA)
                    OB_SMEMA_BTM_UP_Available.Off();
                RunTM_BC.Restart();
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Bottom Conveyor, Main Flow End");
                return FCResultType.NEXT;
            }

            return FCResultType.IDLE;
        }

        private FCResultType fcBtmWaitStartButton1_FlowRun(object sender, EventArgs e)
        {
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType fcBtmWaitStartButton2_FlowRun(object sender, EventArgs e)
        {
            RunTM_BC.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType fcBtmMainEnd_FlowRun(object sender, EventArgs e)
        {
            OB_SMEMA_BTM_UP_Available.Off();
            OB_SMEMA_BTM_DW_MachineRdy.Off();
            fcBtmMainFlow.TaskReset();
            return FCResultType.IDLE;
        }

        #endregion

    }
}
