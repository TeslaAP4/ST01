using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using AcuraLibrary.Forms;
using Acura3._0.Classes;
using Acura3._0.FunctionForms;
using JabilSDK;
using JabilSDK.Controls;
using JabilSDK.Enums;
using JabilSDK.Forms;
using System.Threading;
using static Acura3._0.FunctionForms.LogForm;
using AcuraLibrary;

namespace Acura3._0.ModuleForms
{
    public partial class Feeder2Form : ModuleBaseForm
    {
        #region Constructor
        public Feeder2Form()
        {
            InitializeComponent();
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);
        }
        #endregion

        #region Forms
        private MotorControlForm pMotorCtrlFrm = new MotorControlForm();
        #endregion

        #region Fields & Properties
        private int STime = 1000;
        private JTimer DelayReset = new JTimer();
        private JTimer RunTMFeeder = new JTimer();
        bool InitDone = false;
        public int CurrentMtrYIndex = 1;
        public bool isMotorAlarm
        {
            get { return SysPara.isMotorAlarmFeeder2; }
        }

        const eFeederType SelectedFeederType = eFeederType.Right;
        const string sFeederType = "Feeder2";
        const string sBypassFeeder = "BypassFeeder2";

        public bool BypassCurtainSensor => GetSettingValue("PSet", "BypassCurtainSensor");
        public bool SafeToLoadFeeder = false; //true when feeder is inside machine or bypassed curtain sensor
        private bool ReActivateFeeder = false;
        #endregion

        #region Override Method

        public override void AlwaysRun()
        {
            if (!SysPara.isSettingRefresh)
            {
                //try
                //{
                //    bool IsBypassFeeder = MiddleLayer.AbbF.GetSettingValue("MSet", sBypassFeeder);


                //}
                //catch { }
            }
        }

        public override void InitialReset()
        {
            // Reset Homing
            
            //Reset Task
            fcInitStart.TaskReset();
            fcStartFlow.TaskReset();
            SetSpeed(5);

            ////reset handshake
            //BindingFlags bindingFlags = BindingFlags.Public |
            //                BindingFlags.NonPublic |
            //                BindingFlags.Instance |
            //                BindingFlags.Static;
            //foreach (FieldInfo field in typeof(Handshake).GetFields(bindingFlags))
            //{
            //    if (field.GetValue(null) is _Handshake)
            //    {
            //        _Handshake _hs = (_Handshake)field.GetValue(null);
            //        _hs.Reset();
            //    }
            //}

            //Handshake.Feeder[1].Reset();

            //if (BypassCurtainSensor)
            //    SafeToLoadFeeder = true;
            //else
            //    SafeToLoadFeeder = false;

        }

        public override void Initial()
        {
            //fcInitStart.TaskRun();

          
                fcInitStart.TaskRun();
            
        }

        public override void RunReset()
        {
   

        }

        public override void Run()
        {
            //fcFeederStartFlow.TaskRun();
            fcStartFlow.TaskRun();
            if (!SafeToLoadFeeder)
            {
             
            }
            else
            {
            }
        }

        public override void StartRun()
        {
            RunTMFeeder.Restart();
        }

        public override void StopRun()
        {
        }

        public override void ServoOn()
        {
        }

        public override void ServoOff()
        {
        }

        public override void SetSpeed(int SpeedRatio)
        {
        }

        #endregion

        #region Local Method
        private void FlowChartMessage_PauseRaise(object sender, EventArgs e)
        {
            MiddleLayer.StopRun();
        }

        private void FlowChartMessage_ResetRaise(object sender, EventArgs e)
        {
            RunTMFeeder.Restart();
        }

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

        private bool MoveToPost(Motor mtr, double Post)
        {
            bool InPosition = false;
            bool r1 = mtr.Goto(Post);
            if (r1)
            {
                InPosition = true;
            }
            return InPosition;
        }

        public void ManualMoveYAxis(double pos)
        {
            SetSpeed(10);
            int ManualGotoIndex = 0;
            RunTMFeeder.Restart();
            while (!StopManualTask.IsCancellationRequested)
            {
                Thread.Sleep(10);
            }
        }
        #endregion

        #region UI
        private void Tim_UpdateUI_Tick(object sender, EventArgs e)
        {
      
        }
        #endregion

        #region Event/Button
        private void btn_ManualClick(object sender, EventArgs e)
        {
            if (SysPara.bSaftyReady)
            {
                MessageBox.Show("Please Enable Power");
                return;
            }
            pMotorCtrlFrm.Initial(sender);
            pMotorCtrlFrm.ShowDialog();
        }

        private void btnGotoLoadPos_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos2_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos3_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos4_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos5_Click(object sender, EventArgs e)
        {
        }

        private void btnYGoPickPos6_Click(object sender, EventArgs e)
        {
        }

        private void btnGotoUnloadPos_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_DoubleClick_1(object sender, EventArgs e)
        {
        }

        private void textBox7_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox40_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox41_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox42_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox44_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox43_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox9_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox5_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox6_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBox8_DoubleClick(object sender, EventArgs e)
        {
        }

        private void btn_ReActivateFeeder_Click(object sender, EventArgs e)
        {
            ReActivateFeeder = true;
        }

        private void btn_AccessFeederIO_Click(object sender, EventArgs e)
        {

        }
        private void btn_FeederIO_LoaderTrayHolder_Extend_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_FeederIO_LoaderTrayHolder_Retract_Click(object sender, EventArgs e)
        {
         
        }

        private void btn_FeederIO_Lifter1_Up_Click(object sender, EventArgs e)
        {
        
        }

        private void btn_FeederIO_Lifter2_Up_Click(object sender, EventArgs e)
        {
         
        }
        #endregion

        #region Initial flow
        private FCResultType fcInitStart_FlowRun(object sender, EventArgs e)
        {
           
            return FCResultType.NEXT;
        }

        private FCResultType fcInitCheckUnloadFull_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        
        private FCResultType fcInitLockExtend_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        
        private FCResultType fcInitLifterDown_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        
        private FCResultType fcInitHomeMotorY_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitGoLoadPos_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType fcInitEnd_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            return FCResultType.IDLE;
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
                JSDK.Alarm.Show("9033", " Feeder2abnormal");
                fcM_Fail.Content = "Error";
                b_Abnormal = false;
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        public bool b_Abnormal;
        private void button1_Click(object sender, EventArgs e)
        {
            b_Abnormal = true;
        }

        public void AlarmShow(string Code)
        {
            JSDK.Alarm.Show(Code);
           
        }
    }
}