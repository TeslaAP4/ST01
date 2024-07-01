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
    public partial class GantryForm : ModuleBaseForm
    {
        #region Constructor

        public GantryForm()
        {
            InitializeComponent();
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);
        }
        #endregion

        #region Variable declaration
        public bool B_ScrewFasten = false;
        public bool B_Press = false;
        public bool B_ScrewVision = false;
        public bool B_ScrewRequest = false;
        public bool B_ThrowScrew = false;
        public int I_NGCount = 1;
        public int I_ScrewCount = 1;

        public static bool SafetyPos = false;
        public static bool SafetyScrew = false;
        /// <summary>
        /// 声明轴的使用,L只做Z轴的一些用法
        /// </summary>
        AxisPos AxisZ;
        /// <summary>
        /// 声明轴的使用,L只做XY轴的一些用法
        /// </summary>
        AxisPos AxisXY;
        #endregion

        #region Forms
        public MESForm MesF = new MESForm();
        private MotorControlForm pMotorCtrlFrm = new MotorControlForm();
        #endregion

        #region Fields & Properties
        private int STime = 1000;
        private JTimer DelayReset = new JTimer();
        private JTimer RunTM = new JTimer();
        #endregion

        #region Funtion
        ///// <summary>
        ///// 工位1自动移动
        ///// </summary>
        ///// <param name="XPost">X点位</param>
        ///// <param name="YPost">Y点位</param>
        ///// <param name="ZPost">Z点位</param>
        ///// <param name="ZSafety">Z安全点</param>
        ///// <returns></returns>
        //public static bool B_GantryMoveL(double XPost, double YPost, double ZPost, double ZSafety)
        //{
        //    if (!SafetyScrew)
        //    {
        //        if (MiddleLayer.GantryF.IB_Screw1Lift_Up.IsOn() && MiddleLayer.GantryF.IB_Screw1Balance_Up.IsOn())
        //        {
        //            SafetyScrew = true;
        //        }
        //        else
        //        {
        //            JabilSDK.JSDK.Alarm.Show("0244");
        //        }
        //    }
        //    bool InPosition = false;
        //    if (!SafetyPos)
        //    {
        //        if (MiddleLayer.GantryF.MTR_H1L_Z.GetCommandPosition() <= ZSafety)
        //        {
        //            bool a = MiddleLayer.GantryF.MTR_H1L_X.Goto(XPost);
        //            bool b = MiddleLayer.GantryF.MTR_H1L_Y.Goto(YPost);
        //            if (a && b)
        //            {
        //                SafetyPos = true;
        //            }
        //        }
        //        else
        //        {
        //            MiddleLayer.GantryF.MTR_H1L_Z.Goto(ZSafety);
        //        }
        //    }
        //    else
        //    {
        //        if (MiddleLayer.GantryF.MTR_H1L_Z.Goto(ZPost))
        //        {
        //            SafetyScrew = false;
        //            SafetyPos = false;
        //            InPosition = true;
        //        }
        //    }
        //    return InPosition;
        //}
        #endregion

        #region Override Method

        public override void AlwaysRun()
        {
        }

        public override void InitialReset()
        {
      

        }

        public override void Initial()
        {
            
        }

        public override void RunReset()
        {
           
        }

        public override void Run()
        { 
        }

        public override void StartRun()
        {
            RunTM.Restart();
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
            RunTM.Restart();
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
        #endregion

        #region UI
        private void Tim_UpdateUI_Tick(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region Initial flow

        #endregion

        #region Axis

        /// <summary>
        /// Axis Pos Struct
        /// </summary>
        public struct AxisPos
        {
            public double x;
            public double y;
            public double z;
        }

        public List<AxisPos> AxisPosList = new List<AxisPos>();

        public void GetAxisPos()
        {
            AxisPosList.Clear();
            DataTable D_dt = RecipeData.Tables["T_StressPos"];
            for (int i = 0; i < D_dt.Rows.Count; i++)
            {
                DataRow D_dr = D_dt.Rows[i];
                AxisPos Stressscrewpos = new AxisPos
                {
                    x = Convert.ToDouble(D_dr[1]),
                    y = Convert.ToDouble(D_dr[2]),
                    z = Convert.ToDouble(D_dr[3]),
                };
                AxisPosList.Add(Stressscrewpos);
            }
        }

        /// <summary>
        /// Manual Move Axis bool
        /// </summary>
        public bool B_AxisManualMove = false;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Add Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataTable D_dt = RecipeData.Tables["T_StressPos"];
            int I_Rows = D_dt.Rows.Count;
            DataRow D_dr = D_dt.NewRow();
            D_dr[0] = (I_Rows + 1);
            D_dr[1] = MTR_H1L_X.GetCommandPosition().ToString("F3");
            D_dr[2] = MTR_H1L_Y.GetCommandPosition().ToString("F3");
            D_dr[3] = MTR_H1L_Z.GetCommandPosition().ToString("F3");
            D_dt.Rows.Add(D_dr);
            //LogShow(SysPara.UserName + "  " + "Add screw points the number" + D_dr[0] + "point" + "X:" + D_dr[1] + "  " + "Y:" + D_dr[2] + "  " + "Z:" + D_dr[3], true);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Insert Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_StressPos"];
                int I_Rows = D_dt.Rows.Count;
                DataRow D_dr = D_dt.NewRow();
                D_dr[0] = (I_Rows + 1);
                D_dr[1] = MTR_H1L_X.GetCommandPosition().ToString("F3");
                D_dr[2] = MTR_H1L_Y.GetCommandPosition().ToString("F3");
                D_dr[3] = MTR_H1L_Z.GetCommandPosition().ToString("F3");
                D_dt.Rows.InsertAt(D_dr, D_dgv.CurrentRow.Index);
                //LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " insert point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                //LogShow(SysPara.UserName + "  " + "insert faied, datasheet have no data", false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure delete Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_StressPos"];
                D_dt.Rows.RemoveAt(D_dgv.CurrentRow.Index);
               // LogShow(SysPara.UserName + "  " + "delete stress point", true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
               // LogShow(SysPara.UserName + "  " + "delete faied, datasheet have no data", false);
            }
        }

        private void btnRepalce_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Replace Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_StressPos"];
                DataRow D_dr = D_dt.Rows[D_dgv.CurrentRow.Index];
                D_dr[0] = D_dr[0];
                D_dr[1] = MTR_H1L_X.GetCommandPosition().ToString("F3");
                D_dr[2] = MTR_H1L_Y.GetCommandPosition().ToString("F3");
                D_dr[3] = MTR_H1L_Z.GetCommandPosition().ToString("F3");
               // LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " replace point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
               // LogShow(SysPara.UserName + "  " + "replace faied, datasheet have no data", false);
            }
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
        }


        #endregion
        private void btnConnectScrew_Click(object sender, EventArgs e)
        {
           
        }

        #region 主流程
        private FCResultType flowChart1_1_FlowRun(object sender, EventArgs e)
        {
            B_Press = true;
            return FCResultType.NEXT;
        }

        private FCResultType flowChart1_2_FlowRun(object sender, EventArgs e)
        {
            if (!B_Press)
            {
                B_ScrewVision = true;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_3_FlowRun(object sender, EventArgs e)
        {
            if (!B_ScrewVision)
            {
                B_ScrewFasten = true;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_4_FlowRun(object sender, EventArgs e)
        {
            if (!B_ScrewVision)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_5_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        #region 打螺丝处理
        private FCResultType flowChart2_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ScrewFasten)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_2_FlowRun(object sender, EventArgs e)
        {
            B_ScrewRequest = true;
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_17_FlowRun(object sender, EventArgs e)
        {
            GetAxisPos();
            AxisZ = AxisPosList[0];
            bool bFinsh = MTR_H1L_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_3_FlowRun(object sender, EventArgs e)
        {
            AxisXY = AxisPosList[1];
            bool bFinshx = MTR_H1L_X.Goto(AxisXY.x);
            bool bFinshy = MTR_H1L_X.Goto(AxisXY.y);
            if (bFinshx && bFinshy)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_4_FlowRun(object sender, EventArgs e)
        {
            if (!B_ScrewRequest)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_5_FlowRun(object sender, EventArgs e)
        {
            AxisXY = AxisPosList[1];
            bool bFinshx = MTR_H1L_Z.Goto(AxisXY.z);
            if (bFinshx)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_6_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_7_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewStart.Off();
            OB_ScrewStop.Off();
            OB_ScrewProgram_1.Off();
            OB_ScrewProgram_2.Off();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_8_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewStart.On();
            OB_ScrewProgram_1.On();
            if (IB_ScrewFeeder_Running.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart2_10_FlowRun(object sender, EventArgs e)
        {
            if (IB_Screw_OK.IsOn() || IB_Screw_NG.IsOn())
            {
                if (IB_Screw_OK.IsOn())
                {
                    return FCResultType.NEXT;
                }
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_11_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_12_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_13_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_14_FlowRun(object sender, EventArgs e)
        {
            GetAxisPos();
            AxisZ = AxisPosList[0];
            bool bFinsh = MTR_H1L_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_15_FlowRun(object sender, EventArgs e)
        {
            I_ScrewCount++;
            if (I_ScrewCount > 5)
            {
                I_ScrewCount = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart2_16_FlowRun(object sender, EventArgs e)
        {
            B_ScrewFasten = false;
            return FCResultType.NEXT;
        }
        #endregion

        #region 顶升轴处理
        private FCResultType flowChart3_1_FlowRun(object sender, EventArgs e)
        {
            if (B_Press)
            {
                return FCResultType.NEXT;

            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_2_FlowRun(object sender, EventArgs e)
        {
            if (true)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_3_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_4_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        #region 视觉处理
        private FCResultType flowChart4_1_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_7_FlowRun(object sender, EventArgs e)
        {
            GetAxisPos();
            AxisZ = AxisPosList[0];
            bool bFinsh = MTR_H1L_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_2_FlowRun(object sender, EventArgs e)
        {
            AxisXY = AxisPosList[2];
            bool bFinshx = MTR_H1L_X.Goto(AxisXY.x);
            bool bFinshy = MTR_H1L_X.Goto(AxisXY.y);
            bool bFinshz = MTR_H1L_Z.Goto(AxisXY.z);
            if (bFinshx && bFinshy && bFinshz)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_3_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_4_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_5_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_6_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        #region 出钉处理
        private FCResultType flowChart5_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ScrewRequest)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_2_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewFeeder_SplitNail.On();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart5_3_FlowRun(object sender, EventArgs e)
        {
            if (IB_ScrewFeeder_Ready.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_4_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.On();
            if (IB_ScrewFeedCylinderExtend.IsOn() && IB_ScrewFeedCylinderRetract.IsOff())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_5_FlowRun(object sender, EventArgs e)
        {
            if (IB_ScrewVacuum.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart5_6_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart5_7_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion

        #region 抛钉处理
    
        private FCResultType flowChart6_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ThrowScrew)
            {

                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_2_FlowRun(object sender, EventArgs e)
        {
            GetAxisPos();
            AxisZ = AxisPosList[0];
            bool bFinsh = MTR_H1L_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_3_FlowRun(object sender, EventArgs e)
        {
            AxisXY = AxisPosList[10];
            bool bFinshx = MTR_H1L_X.Goto(AxisXY.x);
            bool bFinshy = MTR_H1L_X.Goto(AxisXY.y);
            if (bFinshx && bFinshy)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_4_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewVacuum.Off();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart6_5_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_6_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_7_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }
        #endregion
    }
}