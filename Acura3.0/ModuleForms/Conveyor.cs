using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcuraLibrary.Forms;
using JabilSDK.Enums;
using JabilSDK;
using Acura3._0.Classes;

namespace Acura3._0.ModuleForms
{
    public partial class Conveyor : ModuleBaseForm
    {
        public Conveyor()
        {
            InitializeComponent();
        }

        public override void InitialReset()
        {
            flowChart90.TaskReset();
            flowChart45.TaskReset();
            flowChart89.TaskReset();

            B_Conveyor1_Station1_FeedFlow = false;
            B_Conveyor1_Station2_FeedFlow = false;
            B_Conveyor1_Station1_DischargeFlow = false;
            B_Conveyor1_Station2_DischargeFlow = false;
            B_Simulate_LowerMachineReady = false;
            B_Simulate_UpperMachineAvailable = false;
            B_Conveyor2_IsReady = false;
            B_Gantry1_IsAvaliable = false;
            B_Gantry2_IsAvaliable = false;
            B_Gantry1_IsWorking = false;
            B_Gantry2_IsWorKing = false;
            B_Gantry1_CanWork = false;
            B_Gantry2_CanWork = false;
            B_Scara_CanWork = false;
            B_Scara_WorkFinish = false;
            B_sixAxisRobot_CanWork = false;
            B_sixAxisRobot_WorkFinish = false;
            B_Conveyor2_Sattion1_Availbale = false;
            B_Conveyor2_Sattion2_Availbale = false;
            B_Conveyor2_Sattion3_Availbale = false;
            B_Conveyor2_Station2_Ready = false;
            B_Conveyor2_Station3_Ready = false;
            OB_LocalMachineAvailable_SMEMA.Off();
            OB_LocalMachineReady_SMEMA.Off();
            OB_LocalMachineWorkNG_SMEMA.Off();
        }

        public override void Initial()
        {
            flowChart90.TaskRun();
            flowChart45.TaskRun();
            flowChart89.TaskRun();

        }

        public override void RunReset()
        {
            OB_Conveyor1_MotorForward.On();
            OB_Conveyor2_MotorForward.On();
        }

        public override void Run()
        {

        }

        public override void StartRun()
        {

        }

        public override void StopRun()
        {
            OB_Conveyor1_MotorForward.Off();
            OB_Conveyor2_MotorForward.Off();
        }

        public bool B_Conveyor1_Station1_FeedFlow = false;
        public bool B_Conveyor1_Station2_FeedFlow = false;
        public bool B_Conveyor1_Station1_DischargeFlow = false;
        public bool B_Conveyor1_Station2_DischargeFlow = false;

        //Conveyor simulate signal  for smema
        //public bool B_Simulate_LocalMachineReady = false;
        //public bool B_Simulate_LocalMachineAvailable = false;
        public bool B_Simulate_UpperMachineAvailable = false;
        public bool B_Simulate_LowerMachineReady = false;

        public bool B_Conveyor2_IsReady = false;
        public bool B_Gantry1_IsAvaliable = false;
        public bool B_Gantry2_IsAvaliable = false;


        public bool B_Gantry1_IsWorking = false;
        public bool B_Gantry2_IsWorKing = false;

        public bool B_Gantry1_CanWork = false;
        public bool B_Gantry2_CanWork = false;

        /// <summary>
        /// conveyor to scara worksignal
        /// </summary>
        public bool B_Scara_CanWork = false;

        /// <summary>
        /// scara to conveyor work finish signal
        /// </summary>
        public bool B_Scara_WorkFinish = false;

        /// <summary>
        /// conveyor to sixaxisRobot work signal
        /// </summary>
        public bool B_sixAxisRobot_CanWork = false;

        /// <summary>
        /// sixAxisRobot to conveyor work finish signal
        /// </summary>
        public bool B_sixAxisRobot_WorkFinish = false;


        public bool B_Conveyor2_Sattion1_Availbale = false;

        public bool B_Conveyor2_Sattion2_Availbale = false;

        public bool B_Conveyor2_Sattion3_Availbale = false;

        public bool B_Conveyor2_Station2_Ready = false;

        public bool B_Conveyor2_Station3_Ready = false;


        /// <summary>
        /// 初始化流程时间实例化
        /// </summary>
        public JTimer J_Initial1 = new JTimer();

        public JTimer J_Initial2 = new JTimer();

        /// <summary>
        /// 自动流程时间实例化
        /// </summary>
        public JTimer J_AutoRun1 = new JTimer();
        public JTimer J_AutoRun2 = new JTimer();

        #region RFID
        SyGoleRFID RFID1 = new SyGoleRFID();
        SyGoleRFID RFID2 = new SyGoleRFID();
        SyGoleRFID RFID3 = new SyGoleRFID();
        SyGoleRFID RFID4 = new SyGoleRFID();

        private void btnConnect_RFID_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP1");
            ushort Port = GetRecipeValue("RSet", "RFID_Port1");
            string ID = GetRecipeValue("RSet", "RFID_ID1");
            if (!RFID1.connect)
            {
                RFID1.RFID_Connect(IP, Port, ID);
            }
            btnConnect_RFID.BackColor = RFID1.connect ? Color.LimeGreen : Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP2");
            ushort Port = GetRecipeValue("RSet", "RFID_Port2");
            string ID = GetRecipeValue("RSet", "RFID_ID2");
            if (!RFID2.connect)
            {
                RFID2.RFID_Connect(IP, Port, ID);
            }
            button4.BackColor = RFID2.connect ? Color.LimeGreen : Color.Red;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP3");
            ushort Port = GetRecipeValue("RSet", "RFID_Port3");
            string ID = GetRecipeValue("RSet", "RFID_ID3");
            if (!RFID3.connect)
            {
                RFID3.RFID_Connect(IP, Port, ID);
            }
            button9.BackColor = RFID3.connect ? Color.LimeGreen : Color.Red;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP4");
            ushort Port = GetRecipeValue("RSet", "RFID_Port4");
            string ID = GetRecipeValue("RSet", "RFID_ID4");
            if (!RFID4.connect)
            {
                RFID4.RFID_Connect(IP, Port, ID);
            }
            button14.BackColor = RFID4.connect ? Color.LimeGreen : Color.Red;
        }


        public string RFID_Read1()
        {
            string IP = GetRecipeValue("RSet", "RFID_IP1");
            if (!RFID1.connect)
            {
                ushort Port = GetRecipeValue("RSet", "RFID_Port1");
                string ID = GetRecipeValue("RSet", "RFID_ID1");
                RFID1.RFID_Connect(IP, Port, ID);
            }
            return RFID1.RFID_ReadDataTostring(IP);
        }

        public string RFID_Read2()
        {
            string IP = GetRecipeValue("RSet", "RFID_IP2");
            if (!RFID1.connect)
            {
                ushort Port = GetRecipeValue("RSet", "RFID_Port2");
                string ID = GetRecipeValue("RSet", "RFID_ID2");
                RFID2.RFID_Connect(IP, Port, ID);
            }
            return RFID2.RFID_ReadDataTostring(IP);
        }

        public string RFID_Read3()
        {
            string IP = GetRecipeValue("RSet", "RFID_IP3");
            if (!RFID3.connect)
            {
                ushort Port = GetRecipeValue("RSet", "RFID_Port3");
                string ID = GetRecipeValue("RSet", "RFID_ID3");
                RFID3.RFID_Connect(IP, Port, ID);
            }
            return RFID3.RFID_ReadDataTostring(IP);
        }

        public string RFID_Read4()
        {
            string IP = GetRecipeValue("RSet", "RFID_IP4");
            if (!RFID4.connect)
            {
                ushort Port = GetRecipeValue("RSet", "RFID_Port4");
                string ID = GetRecipeValue("RSet", "RFID_ID4");
                RFID4.RFID_Connect(IP, Port, ID);
            }
            return RFID4.RFID_ReadDataTostring(IP);
        }

        private void btnDisConnect_RFID_Click(object sender, EventArgs e)
        {
            RFID1.RFID_DisConnect();
            btnConnect_RFID.BackColor = Color.Red;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            RFID2.RFID_DisConnect();
            button4.BackColor = Color.Red;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RFID3.RFID_DisConnect();
            button9.BackColor = Color.Red;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            RFID4.RFID_DisConnect();
            button14.BackColor = Color.Red;
        }

        private void btnRead_RFID_Click(object sender, EventArgs e)
        {
            tb_RFID.Text += RFID_Read1() + Environment.NewLine;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += RFID_Read2() + Environment.NewLine;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox2.Text += RFID_Read3() + Environment.NewLine;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox3.Text += RFID_Read4() + Environment.NewLine;
        }

        private void btnWrite_RFID_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP1");
            RFID1.RFID_Write(IP, tb_RFID.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP2");
            RFID2.RFID_Write(IP, richTextBox1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP3");
            RFID3.RFID_Write(IP, richTextBox2.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string IP = GetRecipeValue("RSet", "RFID_IP4");
            RFID4.RFID_Write(IP, richTextBox3.Text);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            tb_RFID.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
        }





        #endregion

        private FCResultType flowChart45_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart45.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart51_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;

            string IP = GetRecipeValue("RSet", "RFID_IP1");
            ushort Port = GetRecipeValue("RSet", "RFID_Port1");
            string ID = GetRecipeValue("RSet", "RFID_ID1");
            if (!RFID1.connect)
            {
                RFID1.RFID_Connect(IP, Port, ID);
            }
            if (RFID1.connect)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart51.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart52_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.B_Gantry1_InitFinish)
            {
                SysPara.B_Gantry1_InitFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart52.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart53_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart53.Text} finish", true);
            if (IB_Conveyor1_Station1_JackingCylinderUp.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart54_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart54.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart55_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart55.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart56_FlowRun(object sender, EventArgs e)
        {
            OB_Station1_MotorReverse.On();
            MiddleLayer.McuPcba1F.OB_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart56.Text} finish", true);
            J_Initial1.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart59_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(1500);
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart59.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial1.IsOn(SysPara.IO_OverTime))
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart59.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart224_FlowRun(object sender, EventArgs e)
        {
            OB_Station1_MotorReverse.Off();
            MiddleLayer.McuPcba1F.OB_MotorReverse.Off();
            J_Initial1.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart224.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart60_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.Off())
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart60.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial1.IsOn(SysPara.IO_OverTime))
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart60.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart61_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            if (IB_Conveyor1_Station1_StopCylinderUp.IsOn())
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart61.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial1.IsOn(SysPara.IO_OverTime))
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart61.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart62_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart62.Text} finish", true);
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart64_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart64.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart65_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart67_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart67.Text} finish", true);
            if (MiddleLayer.McuPcba1F.IB_BoardStop.IsOn())
            {
                return FCResultType.CASE1;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart68_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.On())
            {
                J_Initial1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart68.Text} finish", true);
                return FCResultType.CASE1;
            }
            if (J_Initial1.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart68.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart69_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart69.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart66_FlowRun(object sender, EventArgs e)
        {
            SysPara.B_Conveyor1_Station1_InitFinish = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart66.Text} finish", true);
            return FCResultType.IDLE;
        }

        private FCResultType flowChart89_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart89.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart88_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.CASE1;
        }

        private FCResultType flowChart87_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;

            string IP = GetRecipeValue("RSet", "RFID_IP2");
            ushort Port = GetRecipeValue("RSet", "RFID_Port2");
            string ID = GetRecipeValue("RSet", "RFID_ID2");
            if (!RFID2.connect)
            {
                RFID2.RFID_Connect(IP, Port, ID);
            }
            if (RFID2.connect)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart87.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart86_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.B_Gantry2_InitFinish)
            {
                SysPara.B_Gantry2_InitFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart52.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart85_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart85.Text} finish", true);
            if (IB_Conveyor1_Station2_JackingCylinderUp.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart84_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart84.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart76_FlowRun(object sender, EventArgs e)
        {
            OB_Station2_MotorReverse.On();
            MiddleLayer.McuPcba2F.OB_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart76.Text} finish", true);
            J_Initial2.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart83_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart83.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart79_FlowRun(object sender, EventArgs e)
        {
            if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(1500);
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart79.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart79.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart236_FlowRun(object sender, EventArgs e)
        {
            OB_Station2_MotorReverse.Off();
            MiddleLayer.McuPcba2F.OB_MotorReverse.Off();
            J_Initial2.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart236.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart80_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.Off())
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart80.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart80.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart81_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.Off();
            if (IB_Conveyor1_Station2_StopCylinderUp.IsOn())
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart81.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart61.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart71_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart71.Text} finish", true);
            if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart72_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart72.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart74_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart82_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart82.Text} finish", true);
            if (MiddleLayer.McuPcba2F.IB_BoardStop.IsOn())
            {
                return FCResultType.CASE1;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart75_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.On())
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart75.Text} finish", true);
                return FCResultType.CASE1;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart75.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart70_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart70.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart73_FlowRun(object sender, EventArgs e)
        {
            SysPara.B_Conveyor1_Station2_InitFinish = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart73.Text} finish", true);
            return FCResultType.IDLE;
        }

        private FCResultType flowChart90_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart90.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart91_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart92_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;

            string IP = GetRecipeValue("RSet", "RFID_IP3");
            ushort Port = GetRecipeValue("RSet", "RFID_Port3");
            string ID = GetRecipeValue("RSet", "RFID_ID3");
            if (!RFID3.connect)
            {
                RFID3.RFID_Connect(IP, Port, ID);
            }
            if (RFID3.connect)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart92.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart93_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;

            string IP = GetRecipeValue("RSet", "RFID_IP4");
            ushort Port = GetRecipeValue("RSet", "RFID_Port4");
            string ID = GetRecipeValue("RSet", "RFID_ID4");
            if (!RFID4.connect)
            {
                RFID4.RFID_Connect(IP, Port, ID);
            }
            if (RFID4.connect)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart93.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart94_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.B_disableRobotModule)
            {
                return FCResultType.NEXT;
            }
            if (SysPara.B_ScaraInitFInish && SysPara.B_SixAxisRobot_InitFinish)
            {
                SysPara.B_ScaraInitFInish = false;
                SysPara.B_SixAxisRobot_InitFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart94.Text} finish", true);
                J_Initial2.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart95_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor2_Station1_Jacking.Off() && CYL_Conveyor2_Station3_Jacking.Off())
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart95.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart95.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart125_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station1_StopCylinder.Off();
            OB_Conveyor2_Station2_StopCylinder.Off();
            OB_Conveyor2_Station3_StopCylinder.Off();
            if (IB_Conveyor2_Station1_StopCylinderUp.IsOn() && IB_Conveyor2_Station2_StopCylinderUp.IsOn() && IB_Conveyor2_Station3_StopCylinderUp.IsOn())
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart125.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_Initial2.IsOn(SysPara.IO_OverTime))
            {
                J_Initial2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart125.Text} overtime", false);
                JSDK.Alarm.Show("");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart126_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.B_Conveyor1_Station1_InitFinish && SysPara.B_Conveyor1_Station2_InitFinish)
            {
                bInitialOk = true;
                SysPara.B_Conveyor1_Station1_InitFinish = false;
                SysPara.B_Conveyor1_Station2_InitFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart126.Text} finish", true);
                return FCResultType.IDLE;
            }
            return FCResultType.IDLE;
        }


        #region Conveyor1 main flow

        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart2.Text} finish", true);
                return FCResultType.NEXT;
            }

            if (IB_Conveyor1_Staiton1_BoardStop.IsOn() && IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                return FCResultType.CASE3;
            }
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn() && !IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                return FCResultType.CASE1;
            }
            if (!IB_Conveyor1_Staiton1_BoardStop.IsOn() && IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE2;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart3.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart4.Text} finish", true);
                return FCResultType.NEXT;
            }
            B_Conveyor1_Station2_FeedFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart5.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (!B_Conveyor1_Station2_FeedFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart7.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station1_FeedFlow = true;

            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart9.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station1_FeedFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart10.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart8.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station1_FeedFlow = true;
            B_Conveyor1_Station2_FeedFlow = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart11.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station1_FeedFlow && !B_Conveyor1_Station2_FeedFlow)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart12.Text} finish", true);
                return FCResultType.CASE3;
            }
            return FCResultType.IDLE;
        }
        #endregion

        #region Converyor1 station1 feed flow

        private FCResultType flowChart39_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor1_Station1_FeedFlow)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart39.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart40_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            if (IB_Conveyor1_Station1_StopCylinderUp.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart40.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart40.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart42_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart42.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (!IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                if (SysPara.IsUseSMEMA)
                {
                    OB_LocalMachineReady_SMEMA.On();
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
                return FCResultType.NEXT;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
            return FCResultType.CASE1;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            B_Simulate_LowerMachineReady = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            B_Simulate_UpperMachineAvailable = true;
        }

        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} finish", true);
            if (B_Conveyor2_IsReady)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart13_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart13.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart14_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart14.Text} finish", true);
            if (B_Gantry1_IsAvaliable && !B_Gantry2_IsAvaliable)
            {
                return FCResultType.NEXT;
            }
            if (B_Gantry2_IsAvaliable)
            {
                return FCResultType.CASE1;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart15_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart15.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart17_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station1_DischargeFlow = true;

            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart17.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station1_DischargeFlow)
            {
                B_Gantry1_IsAvaliable = false;
                //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart16_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart16.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart18_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station2_DischargeFlow = true;
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart18.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart20_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station2_DischargeFlow)
            {
                B_Gantry2_IsAvaliable = false;
                //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart20.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart34_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart21_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart21.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart22_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart22.Text} finish", true);
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                return FCResultType.NEXT;
            }
            if (IB_UpMachineAvailable_SMEMA.IsOn() || B_Simulate_UpperMachineAvailable)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart22.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart33_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart33.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart23_FlowRun(object sender, EventArgs e)
        {
           // MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart23.Text} finish", true);
            if (!B_Conveyor1_Station2_FeedFlow && !B_Conveyor1_Station2_DischargeFlow && !B_Gantry2_IsWorKing)
            {
                return FCResultType.NEXT;
            }
            if (!B_Conveyor1_Station1_FeedFlow && !B_Conveyor1_Station1_DischargeFlow && !B_Gantry1_IsWorking)
            {
                return FCResultType.CASE1;
            }
            return FCResultType.CASE3;
        }

        private FCResultType flowChart103_FlowRun(object sender, EventArgs e)
        {
            B_Gantry1_CanWork = true;
            B_Gantry1_IsWorking = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart103.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart145_FlowRun(object sender, EventArgs e)
        {
            B_Gantry2_CanWork = true;
            B_Gantry2_IsWorKing = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart103.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart24_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart24.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart32_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart32.Text} finish", true);
            return FCResultType.CASE3;
        }

        private FCResultType flowChart25_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station2_FeedFlow = true;
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart25.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart26_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station2_FeedFlow)
            {
               // MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart26.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart27_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart27.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart28_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station1_FeedFlow = true;
           // MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart28.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart29_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor1_Station1_FeedFlow)
            {
               // MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart29.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart35_FlowRun(object sender, EventArgs e)
        {
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart35.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart36_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart30_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.CASE3;
        }

        private FCResultType flowChart37_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart38_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.CASE2;
        }

        private FCResultType flowChart44_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.On();
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart35.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart47_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart47.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart47.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart48_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart48.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart48.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart49_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart49.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (SysPara.IsUseSMEMA)
            {
                OB_LocalMachineReady_SMEMA.Off();
                B_Simulate_UpperMachineAvailable = false;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart49.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart50_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart50.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(1000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart50.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart96_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart96.Text} finish", true);
                return FCResultType.CASE1;
            }
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart96.Text} finish", true);
                return FCResultType.NEXT;
            }
            //RFID read
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart96.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart106_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart106.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart97_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun1.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart98_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.On())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart98.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart98.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart99_FlowRun(object sender, EventArgs e)
        {
            OB_Station1_MotorForward.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart99.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart100_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.McuPcba1F.OB_MotorForward.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart100.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart150_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart150.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba1F.IB_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart150.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart151_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart151.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba1F.IB_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart151.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart152_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
            }
            OB_Station1_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart152.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart153_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart153.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba1F.IB_BoardStop.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart153.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart101_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.Off())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart101.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart101.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart102_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.DelayMs(1500);
            MiddleLayer.McuPcba1F.OB_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart102.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart104_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass)
            {
                B_Gantry1_IsAvaliable = true;
            }
            B_Conveyor1_Station1_FeedFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart104.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart237_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart237.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart107_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor1_Station1_DischargeFlow)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart107.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart108_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart108.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart109_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.On())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart109.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart109.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_FlowRun(object sender, EventArgs e)
        {
            OB_Station1_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart110.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart111_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.McuPcba1F.OB_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart111.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart112_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart112.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba1F.IB_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart112.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart113_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart113.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba1F.IB_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart113.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart114_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
            }
            MiddleLayer.McuPcba1F.OB_MotorReverse.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart114.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart115_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(1000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart115.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(1000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart115.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart116_FlowRun(object sender, EventArgs e)
        {
            OB_Station1_MotorReverse.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart116.Text} finish", true);
            J_AutoRun1.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart117_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.On();
            if (IB_Conveyor1_Station1_StopCylinderDown.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart116.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart116.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart118_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station1_Jacking.Off())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart118.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart118.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart120_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor2_IsReady)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart120.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart121_FlowRun(object sender, EventArgs e)
        {
            //B_Gantry1_IsAvaliable = false;
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart121.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart122_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            if (IB_Conveyor1_Station1_StopCylinderUp.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart122.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart122.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart123_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart123.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart124_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station1_DischargeFlow = false;
            B_Gantry1_IsWorking = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart124.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart238_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart238.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart127_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor1_Station2_FeedFlow)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart127.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart128_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.On();
            if (IB_Conveyor1_Station2_StopCylinderUp.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart128.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart128.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart129_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart129.Text} finish", true);
                return FCResultType.NEXT;
            }

            if (!IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                if (SysPara.IsUseSMEMA)
                {
                    OB_LocalMachineReady_SMEMA.On();
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart129.Text} finish", true);
                return FCResultType.NEXT;
            }
            else
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart129.Text} finish", true);
                return FCResultType.CASE1;
            }
        }

        private FCResultType flowChart130_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart130.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart131_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart131.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart131.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart132_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart132.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart132.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart133_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart133.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (SysPara.IsUseSMEMA)
            {
                OB_LocalMachineReady_SMEMA.Off();
                B_Simulate_UpperMachineAvailable = false;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart133.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart135_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart135.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart134_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart134.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (SysPara.IsUseSMEMA)
            {
                if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
                {
                    MiddleLayer.SystemF.DelayMs(300);
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart134.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart136_FlowRun(object sender, EventArgs e)
        {
            //Rfid read

            if (SysPara.IsByPass)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart136.Text} finish", true);
                return FCResultType.CASE1;
            }
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart136.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart137_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun1.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart147_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart147.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart138_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.On())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart138.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart138.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart139_FlowRun(object sender, EventArgs e)
        {
            OB_Station2_MotorForward.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart139.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart140_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.McuPcba2F.OB_MotorForward.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart140.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart141_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart141.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba2F.IB_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart141.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart142_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart142.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba2F.IB_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart142.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart149_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.DelayMs(2000);
            OB_Station2_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart149.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart143_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart143.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba2F.IB_BoardStop.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart143.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart144_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.Off())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart144.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart144.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart148_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.DelayMs(1000);
            MiddleLayer.McuPcba2F.OB_MotorForward.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart148.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart146_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass)
            {
                B_Gantry2_IsAvaliable = true;
            }
            B_Conveyor1_Station2_FeedFlow = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart146.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart105_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor1_Station2_DischargeFlow)
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart105.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart154_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart154.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart155_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.On())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart155.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart155.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart160_FlowRun(object sender, EventArgs e)
        {
            OB_Station2_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart160.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart161_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.McuPcba2F.OB_MotorReverse.On();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart161.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart212_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart212.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba2F.IB_BoardIn.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart212.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart213_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart213.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (MiddleLayer.McuPcba2F.IB_BoardIn.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart213.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart225_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
            }
            MiddleLayer.McuPcba2F.OB_MotorReverse.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart225.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart226_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(1000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart226.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(500);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart226.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart227_FlowRun(object sender, EventArgs e)
        {
            OB_Station2_MotorReverse.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart227.Text} finish", true);
            J_AutoRun1.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart228_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.On();
            if (IB_Conveyor1_Station2_StopCylinderDown.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart228.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart228.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart229_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor1_Station2_Jacking.Off())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart229.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart229.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart231_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor2_IsReady)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart231.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart232_FlowRun(object sender, EventArgs e)
        {
            //B_Gantry2_IsAvaliable = false;
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart121.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart233_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.Off();
            if (IB_Conveyor1_Station2_StopCylinderUp.IsOn())
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart233.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun1.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun1.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart233.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart235_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor1_Station2_DischargeFlow = false;
            B_Gantry2_IsWorKing = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart235.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart239_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart239.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart240_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart240.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart41_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart41.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun2.Restart();
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart43.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station1_BoardStop.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} finish", true);
                return FCResultType.CASE1;
            }
            else
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} finish", true);
                return FCResultType.NEXT;
            }
        }

        private FCResultType flowChart156_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station1_StopCylinder.Off();
            if (IB_Conveyor2_Station1_StopCylinderUp.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart156.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart156.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart157_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_IsReady = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart157.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart158_FlowRun(object sender, EventArgs e)
        {
            if (B_Gantry1_IsAvaliable || B_Gantry2_IsAvaliable)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart158.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart162_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart162.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station1_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(500);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart162.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart163_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_IsReady = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart163.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart165_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart165.Text} finish", true);
            if (SysPara.IsByPass || SysPara.B_disableRobotModule)
            {
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart166_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station1_StopCylinder.On();
            if (IB_Conveyor2_Station1_StopCylinderDown.IsOn())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart166.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart166.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart167_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor2_Station1_Jacking.On())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart167.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart167.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart176_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass " + $"{this.Text} Module {flowChart176.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart168_FlowRun(object sender, EventArgs e)
        {
            B_Scara_CanWork = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart168.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart169_FlowRun(object sender, EventArgs e)
        {
            if (B_Scara_WorkFinish)
            {
                B_Scara_WorkFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart169.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart241_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart241.Text} finish", true);
                return FCResultType.NEXT;
            }
            //Rfid read
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart241.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart242_FlowRun(object sender, EventArgs e)
        {
            //get data
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart242.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart170_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Sattion1_Availbale = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart170.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart171_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor2_Station2_Ready)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart171.Text} finish", true);
                J_AutoRun2.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart172_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor2_Station1_Jacking.Off())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart172.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart172.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart174_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor2_Station2_Ready)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart174.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart175_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Sattion1_Availbale = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart175.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart177_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart177.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart178_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart178.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart179_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun2.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart179.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart180_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.Off();
            if (IB_Conveyor2_Station2_StopCylinderUp.IsOn())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart180.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart180.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart181_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart241.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station2_BoardStop.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart181.Text} finish", true);
                return FCResultType.CASE1;
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart182_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Station2_Ready = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart182.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart183_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor2_Sattion1_Availbale)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart183.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart185_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart185.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station2_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(500);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart185.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart186_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Station2_Ready = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart186.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart188_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart188.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart189_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Sattion2_Availbale = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart189.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart195_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart195.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart190_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor2_Station3_Ready)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart190.Text} finish", true);
                J_AutoRun2.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart191_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station2_StopCylinder.Off();
            if (IB_Conveyor2_Station2_StopCylinderDown.IsOn())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart191.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart191.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart192_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_MotorForward.On();
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart192.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart193_FlowRun(object sender, EventArgs e)
        {
            if (!B_Conveyor2_Station3_Ready)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart193.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart194_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Sattion2_Availbale = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart194.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart196_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart196.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart197_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun2.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart197.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart198_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun2.Restart();
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart198.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station3_BoardStop.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart198.Text} finish", true);
                return FCResultType.CASE1;
            }
            else
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart198.Text} finish", true);
                return FCResultType.NEXT;
            };
        }

        private FCResultType flowChart199_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station3_StopCylinder.Off();
            if (IB_Conveyor2_Station3_StopCylinderUp.IsOn())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart199.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart199.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart200_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Station3_Ready = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart200.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart201_FlowRun(object sender, EventArgs e)
        {
            if (B_Conveyor2_Sattion2_Availbale)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart201.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart203_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.SystemF.DelayMs(2000);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart203.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Station3_BoardStop.IsOn())
            {
                MiddleLayer.SystemF.DelayMs(500);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart203.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart204_FlowRun(object sender, EventArgs e)
        {
            B_Conveyor2_Station3_Ready = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart204.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart206_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsByPass || SysPara.B_disableRobotModule)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass " + $"{this.Text} Module {flowChart206.Text} finish", true);
                return FCResultType.CASE1;
            }
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart206.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart221_FlowRun(object sender, EventArgs e)
        {
            J_AutoRun2.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass " + $"{this.Text} Module {flowChart221.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart244_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart244.Text} finish", true);
                return FCResultType.NEXT;
            }
            //Rfid read
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart244.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart245_FlowRun(object sender, EventArgs e)
        {
            //get data
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart245.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart207_FlowRun(object sender, EventArgs e)
        {
            OB_Conveyor2_Station3_StopCylinder.On();
            if (IB_Conveyor2_Station3_StopCylinderDown.IsOn())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart207.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart207.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart208_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor2_Station3_Jacking.On())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart208.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart208.Text} overtime", false);
                JSDK.Alarm.Show(" ");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart209_FlowRun(object sender, EventArgs e)
        {
            B_sixAxisRobot_CanWork = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart209.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart210_FlowRun(object sender, EventArgs e)
        {
            if (B_sixAxisRobot_WorkFinish)
            {
                B_sixAxisRobot_WorkFinish = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart210.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart211_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart211.Text} finish", true);
            }
            if (SysPara.IsUseSMEMA && !SysPara.IsDryRun)
            {
                OB_LocalMachineAvailable_SMEMA.On();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart211.Text} finish", true);
            }
            return FCResultType.NEXT;
        }

        private FCResultType flowChart214_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart214.Text} finish", true);
            }
            if (SysPara.IsUseSMEMA && !SysPara.IsDryRun)
            {
                if (IB_DownMachineReady_SMEMA.IsOn() || B_Simulate_LowerMachineReady)
                {
                    B_Simulate_LowerMachineReady = false;
                    J_AutoRun2.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart214.Text} finish", true);
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart215_FlowRun(object sender, EventArgs e)
        {
            if (CYL_Conveyor2_Station3_Jacking.Off())
            {
                J_AutoRun2.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart215.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AutoRun2.IsOn(SysPara.IO_OverTime))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart215.Text} overtime", false);
                JSDK.Alarm.Show(" ");
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart217_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart217.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Boardout.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart217.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart218_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart218.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Conveyor2_Boardout.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart218.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart219_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.IsDryRun && !SysPara.IsUseSMEMA)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "DryRun Mode: " + $"{this.Text} Module {flowChart219.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_DownMachineReady_SMEMA.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart219.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart220_FlowRun(object sender, EventArgs e)
        {
            OB_LocalMachineAvailable_SMEMA.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart220.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart222_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart223_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart247_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart247.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart248_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart248.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart250_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart250.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart249_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Mode: " + $"{this.Text} Module {flowChart249.Text} finish", true);
            return FCResultType.CASE1;
        }



        private bool MachineReady1;  //工位1要料信号
        private bool MachineReady2;  //工位2要料信号
        private bool MachineAvailable1;  //工位1出料信号
        private bool MachineAvailable2;  //工位2出料信号
        private bool Station1Start;   //工位1开始工作信号
        private bool Station2Start;   //工位2开始工作信号
        private bool MachineAvaiable1Comp1;   //工位1出料完成信号
        private bool MachineAvaiable1Comp2;   //工位2出料完成信号
        private int StationIndex;   //判断出料工位

        /// <summary>
        /// Conveyor Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private FCResultType flowChart1_FlowRun_2(object sender, EventArgs e)
        {
            OB_Conveyor1_MotorForward.On();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_UpMachineAvailable_SMEMA.IsOn())
            {
                if (MachineReady1 && IB_Conveyor1_Staiton1_BoardStop.IsOff())
                {
                    OB_LocalMachineReady_SMEMA.On();
                    MachineReady1 = false;
                    return FCResultType.NEXT;
                }
                if (MachineReady2 && IB_Conveyor1_Staiton2_BoardStop.IsOff())
                {
                    OB_LocalMachineReady_SMEMA.On();
                    MachineReady2 = false;
                    return FCResultType.CASE1;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_FlowRun_1(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                CYL_Conveyor1_Station1_Jacking.On();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_FlowRun_1(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.Off();
            if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                CYL_Conveyor1_Station2_Jacking.On();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_Conveyor1_Station1_JackingCylinderUp.IsOn())
            {
                OB_Station1_MotorForward.On();
                MiddleLayer.McuPcba1F.OB_MotorForward.On();
                if (MiddleLayer.McuPcba1F.IB_BoardStop.IsOn())
                {
                    OB_Station1_MotorForward.Off();
                    MiddleLayer.McuPcba1F.OB_MotorForward.Off();
                    Station1Start = true;
                    CYL_Conveyor1_Station1_Jacking.Off();

                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station1_JackingCylinderUp.IsOn())
            {
                OB_Station2_MotorForward.On();
                MiddleLayer.McuPcba2F.OB_MotorForward.On();
                if (MiddleLayer.McuPcba2F.IB_BoardStop.IsOn())
                {
                    OB_Station2_MotorForward.Off();
                    MiddleLayer.McuPcba2F.OB_MotorForward.Off();
                    Station2Start = true;
                    CYL_Conveyor1_Station2_Jacking.Off();

                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart8_FlowRun_1(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        private FCResultType flowChart9_FlowRun_1(object sender, EventArgs e)
        {
            if (MachineAvailable1 && IB_Conveyor1_Staiton1_BoardStop.IsOff())
            {
                MachineAvailable1 = false;
                return FCResultType.NEXT;
            }
            if (MachineAvailable2 && IB_Conveyor1_Staiton2_BoardStop.IsOff())
            {
                MachineAvailable2 = false;
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart11_FlowRun_1(object sender, EventArgs e)
        {
            OB_Conveyor1_Station1_StopCylinder.Off();
            CYL_Conveyor1_Station1_Jacking.On();
            if (IB_Conveyor1_Station1_JackingCylinderUp.IsOn())
            {
                OB_Station1_MotorReverse.On();
                MiddleLayer.McuPcba1F.OB_MotorReverse.On();

                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart10_FlowRun_1(object sender, EventArgs e)
        {
            OB_Conveyor1_Station2_StopCylinder.Off();
            CYL_Conveyor1_Station2_Jacking.On();
            if (IB_Conveyor1_Station2_JackingCylinderUp.IsOn())
            {
                OB_Station2_MotorReverse.On();
                MiddleLayer.McuPcba2F.OB_MotorReverse.On();

                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart12_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_Conveyor1_Staiton1_BoardStop.IsOn())
            {
                System.Threading.Thread.Sleep(1000);
                OB_Station1_MotorReverse.Off();
                MiddleLayer.McuPcba1F.OB_MotorReverse.Off();
                CYL_Conveyor1_Station1_Jacking.Off();
                MachineAvaiable1Comp1 = true;
                StationIndex = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart7_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_Conveyor1_Staiton2_BoardStop.IsOn())
            {
                System.Threading.Thread.Sleep(1000);
                OB_Station2_MotorReverse.Off();
                MiddleLayer.McuPcba2F.OB_MotorReverse.Off();
                CYL_Conveyor1_Station2_Jacking.Off();
                MachineAvaiable1Comp2 = true;
                StationIndex = 2;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart13_FlowRun_1(object sender, EventArgs e)
        {
            if (StationIndex == 1 && IB_Conveyor1_Staiton2_BoardStop.IsOff() && IB_Conveyor2_Station1_BoardStop.IsOff())
            {
                OB_Conveyor1_Station1_StopCylinder.On();
                OB_Conveyor1_Station2_StopCylinder.On();
                if (IB_Conveyor1_Station1_StopCylinderDown.IsOn())
                {
                    OB_Conveyor1_Station1_StopCylinder.Off();
                    return FCResultType.NEXT;
                }
            }
            if (StationIndex == 2 && IB_Conveyor2_Station1_BoardStop.IsOff())
            {
                OB_Conveyor1_Station2_StopCylinder.On();
                if (IB_Conveyor1_Station2_StopCylinderDown.IsOn())
                {
                    OB_Conveyor2_Station1_StopCylinder.Off();
                    return FCResultType.NEXT;
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart14_FlowRun_1(object sender, EventArgs e)
        {
            if (IB_Conveyor2_Station1_BoardStop.IsOn())
            {
                StationIndex = 0;
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }
    }
    #endregion
}
