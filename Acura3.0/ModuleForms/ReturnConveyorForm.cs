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
            //if (MiddleLayer.SystemF.isEMO)
            //    ExecuteBottomConveyor(ConveyorState.Stop);
            
            //if (SysPara.SystemInitialOk && !BottomConveyorAlarm && SysPara.SystemMode==RunMode.RUN)
            //{
            //}

            //if (StopRunFlag && !SysPara.SystemInitialOk)
            //{
            //    ExecuteBottomConveyor(ConveyorState.Stop);
               
            //    StopRunFlag = false;
            //}
        }
        JTimer JTimer1 = new JTimer();
        public override void InitialReset()
        {
            fcInitBtmFlow.TaskReset();
            //ExecuteBottomConveyor(ConveyorState.Stop);
            BottomConveyorAlarm = false;
            StopRunFlag = false;

        }

        public override void RunReset()
        {
            fcStartFlow.TaskReset();
        }

        public override void Initial()
        {
            fcInitBtmFlow.TaskRun();
        }

        public override void Run()
        {
            fcStartFlow.TaskRun();
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
            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmCheckSensor_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmRunConv_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitBtmFlowEnd_FlowRun(object sender, EventArgs e)
        {
            if (!bInitialOk)
            {
                MiddleLayer.LogF.AddLog(LogType.EventFlow, "Robot,Initialize Flow Done");
            }
            bInitialOk = true;                                                      
            return FCResultType.IDLE;
        }


        #endregion

        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            //JSDK.Alarm.Show("2019", " ReturnConveyorFormAbnormal");
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            if (b_Abnormal)
            {
                JSDK.Alarm.Show("9033", " ReturnConveyorFormAbnormal");
                fcM_Fail.Content = "Error";
                b_Abnormal = false;
                JTimer1.Restart();
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        public bool b_Abnormal;
        private void button1_Click(object sender, EventArgs e)
        {
            b_Abnormal = true;
        }

        private FCResultType fcStartFlow_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
    }
}
