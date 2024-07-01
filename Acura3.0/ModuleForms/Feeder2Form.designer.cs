
namespace Acura3._0.ModuleForms
{
    partial class Feeder2Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Feeder2Form));
            this.fcInitStart = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitSkip = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitEnd = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitCheckUnloadFull = new JabilSDK.UserControlLib.FlowChart();
            this.fcM_InitUnloadTrayFull = new AcuraLibrary.Forms.FlowChartMessage();
            this.fcInitLockExtend = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitLifterDown = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitHomeMotorY = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitGoLoadPos = new JabilSDK.UserControlLib.FlowChart();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.btnGotoUnloadPos = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.btnGotoLoadPos = new System.Windows.Forms.Button();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_EnCoder_Y = new System.Windows.Forms.Label();
            this.motorJog1 = new JabilSDK.Controls.MotorJog();
            this.motorJog2 = new JabilSDK.Controls.MotorJog();
            this.Tim_UpdateUI = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataColumn6 = new System.Data.DataColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.flowChart7 = new JabilSDK.UserControlLib.FlowChart();
            this.fcStartFlow = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart1 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart2 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart3 = new JabilSDK.UserControlLib.FlowChart();
            this.fcM_Fail = new AcuraLibrary.Forms.FlowChartMessage();
            this.flowChart4 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart5 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart6 = new JabilSDK.UserControlLib.FlowChart();
            this.plProductionSetting.SuspendLayout();
            this.plRecipeEditor.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            this.plFlowAuto.SuspendLayout();
            this.plMotionSetup.SuspendLayout();
            this.plMotorControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaintenance
            // 
            this.plMaintenance.Margin = new System.Windows.Forms.Padding(3);
            this.plMaintenance.Size = new System.Drawing.Size(1307, 985);
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.Controls.Add(this.checkBox1);
            this.plProductionSetting.Margin = new System.Windows.Forms.Padding(3);
            this.plProductionSetting.Size = new System.Drawing.Size(1068, 800);
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.Controls.Add(this.groupBox1);
            this.plRecipeEditor.Margin = new System.Windows.Forms.Padding(3);
            this.plRecipeEditor.Size = new System.Drawing.Size(1068, 800);
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.Controls.Add(this.fcM_InitUnloadTrayFull);
            this.plFlowInitial.Controls.Add(this.fcInitSkip);
            this.plFlowInitial.Controls.Add(this.fcInitGoLoadPos);
            this.plFlowInitial.Controls.Add(this.fcInitCheckUnloadFull);
            this.plFlowInitial.Controls.Add(this.fcInitLifterDown);
            this.plFlowInitial.Controls.Add(this.fcInitLockExtend);
            this.plFlowInitial.Controls.Add(this.fcInitEnd);
            this.plFlowInitial.Controls.Add(this.fcInitHomeMotorY);
            this.plFlowInitial.Controls.Add(this.fcInitStart);
            this.plFlowInitial.Margin = new System.Windows.Forms.Padding(3);
            this.plFlowInitial.Size = new System.Drawing.Size(1068, 800);
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.Controls.Add(this.button2);
            this.plFlowAuto.Controls.Add(this.flowChart7);
            this.plFlowAuto.Controls.Add(this.flowChart6);
            this.plFlowAuto.Controls.Add(this.flowChart5);
            this.plFlowAuto.Controls.Add(this.flowChart4);
            this.plFlowAuto.Controls.Add(this.flowChart3);
            this.plFlowAuto.Controls.Add(this.flowChart2);
            this.plFlowAuto.Controls.Add(this.flowChart1);
            this.plFlowAuto.Controls.Add(this.fcStartFlow);
            this.plFlowAuto.Controls.Add(this.fcM_Fail);
            this.plFlowAuto.Margin = new System.Windows.Forms.Padding(3);
            this.plFlowAuto.Size = new System.Drawing.Size(1307, 985);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Enabled = false;
            this.plMachineStatus.Margin = new System.Windows.Forms.Padding(3);
            this.plMachineStatus.Size = new System.Drawing.Size(1068, 800);
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.Controls.Add(this.groupBox3);
            this.plMotionSetup.Margin = new System.Windows.Forms.Padding(3);
            this.plMotionSetup.Size = new System.Drawing.Size(1068, 800);
            // 
            // plMotorControl
            // 
            this.plMotorControl.Controls.Add(this.groupBox5);
            this.plMotorControl.Margin = new System.Windows.Forms.Padding(3);
            this.plMotorControl.Size = new System.Drawing.Size(1068, 800);
            // 
            // MSet
            // 
            this.MSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3});
            // 
            // PSet
            // 
            this.PSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6});
            // 
            // RSet
            // 
            this.RSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn4,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13});
            // 
            // fcInitStart
            // 
            this.fcInitStart.BackColor = System.Drawing.Color.White;
            this.fcInitStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitStart.CASE1 = this.fcInitSkip;
            this.fcInitStart.CASE2 = null;
            this.fcInitStart.CASE3 = null;
            this.fcInitStart.Location = new System.Drawing.Point(265, 27);
            this.fcInitStart.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitStart.Name = "fcInitStart";
            this.fcInitStart.NEXT = this.fcInitCheckUnloadFull;
            this.fcInitStart.Size = new System.Drawing.Size(241, 33);
            this.fcInitStart.TabIndex = 9;
            this.fcInitStart.Text = "Initial start";
            this.fcInitStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitStart.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitStart_FlowRun);
            // 
            // fcInitSkip
            // 
            this.fcInitSkip.BackColor = System.Drawing.Color.White;
            this.fcInitSkip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitSkip.CASE1 = null;
            this.fcInitSkip.CASE2 = null;
            this.fcInitSkip.CASE3 = null;
            this.fcInitSkip.Location = new System.Drawing.Point(81, 162);
            this.fcInitSkip.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitSkip.Name = "fcInitSkip";
            this.fcInitSkip.NEXT = this.fcInitEnd;
            this.fcInitSkip.Size = new System.Drawing.Size(93, 33);
            this.fcInitSkip.TabIndex = 113;
            this.fcInitSkip.Text = "Skip";
            this.fcInitSkip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fcInitEnd
            // 
            this.fcInitEnd.BackColor = System.Drawing.Color.White;
            this.fcInitEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitEnd.CASE1 = null;
            this.fcInitEnd.CASE2 = null;
            this.fcInitEnd.CASE3 = null;
            this.fcInitEnd.Location = new System.Drawing.Point(265, 297);
            this.fcInitEnd.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitEnd.Name = "fcInitEnd";
            this.fcInitEnd.NEXT = null;
            this.fcInitEnd.Size = new System.Drawing.Size(241, 33);
            this.fcInitEnd.TabIndex = 13;
            this.fcInitEnd.Text = "End";
            this.fcInitEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitEnd.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitEnd_FlowRun);
            // 
            // fcInitCheckUnloadFull
            // 
            this.fcInitCheckUnloadFull.BackColor = System.Drawing.Color.White;
            this.fcInitCheckUnloadFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitCheckUnloadFull.CASE1 = this.fcM_InitUnloadTrayFull;
            this.fcInitCheckUnloadFull.CASE2 = null;
            this.fcInitCheckUnloadFull.CASE3 = null;
            this.fcInitCheckUnloadFull.Location = new System.Drawing.Point(265, 72);
            this.fcInitCheckUnloadFull.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitCheckUnloadFull.Name = "fcInitCheckUnloadFull";
            this.fcInitCheckUnloadFull.NEXT = this.fcInitLockExtend;
            this.fcInitCheckUnloadFull.Size = new System.Drawing.Size(241, 33);
            this.fcInitCheckUnloadFull.TabIndex = 97;
            this.fcInitCheckUnloadFull.Text = "Check Unload Full";
            this.fcInitCheckUnloadFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitCheckUnloadFull.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitCheckUnloadFull_FlowRun);
            // 
            // fcM_InitUnloadTrayFull
            // 
            this.fcM_InitUnloadTrayFull.BackColor = System.Drawing.Color.White;
            this.fcM_InitUnloadTrayFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcM_InitUnloadTrayFull.ButtonInitializeText = "Initialize";
            this.fcM_InitUnloadTrayFull.ButtonRetryText = "Retry";
            this.fcM_InitUnloadTrayFull.ButtonSkipText = "Skip";
            this.fcM_InitUnloadTrayFull.CASE1 = this.fcInitCheckUnloadFull;
            this.fcM_InitUnloadTrayFull.CASE2 = null;
            this.fcM_InitUnloadTrayFull.CASE3 = null;
            this.fcM_InitUnloadTrayFull.Content = "Unload Tray Full";
            this.fcM_InitUnloadTrayFull.HideButtonInitialize = true;
            this.fcM_InitUnloadTrayFull.HideButtonMute = false;
            this.fcM_InitUnloadTrayFull.HideButtonPause = false;
            this.fcM_InitUnloadTrayFull.HideButtonRetry = false;
            this.fcM_InitUnloadTrayFull.HideButtonSkip = true;
            this.fcM_InitUnloadTrayFull.Location = new System.Drawing.Point(536, 72);
            this.fcM_InitUnloadTrayFull.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcM_InitUnloadTrayFull.Name = "fcM_InitUnloadTrayFull";
            this.fcM_InitUnloadTrayFull.NEXT = null;
            this.fcM_InitUnloadTrayFull.Size = new System.Drawing.Size(205, 33);
            this.fcM_InitUnloadTrayFull.TabIndex = 114;
            this.fcM_InitUnloadTrayFull.Text = "Unload Tray Full";
            this.fcM_InitUnloadTrayFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcM_InitUnloadTrayFull.Title = "Feeder 2 Error";
            // 
            // fcInitLockExtend
            // 
            this.fcInitLockExtend.BackColor = System.Drawing.Color.White;
            this.fcInitLockExtend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitLockExtend.CASE1 = null;
            this.fcInitLockExtend.CASE2 = null;
            this.fcInitLockExtend.CASE3 = null;
            this.fcInitLockExtend.Location = new System.Drawing.Point(265, 117);
            this.fcInitLockExtend.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitLockExtend.Name = "fcInitLockExtend";
            this.fcInitLockExtend.NEXT = this.fcInitLifterDown;
            this.fcInitLockExtend.Size = new System.Drawing.Size(241, 33);
            this.fcInitLockExtend.TabIndex = 64;
            this.fcInitLockExtend.Text = "Holder Extend";
            this.fcInitLockExtend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitLockExtend.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitLockExtend_FlowRun);
            // 
            // fcInitLifterDown
            // 
            this.fcInitLifterDown.BackColor = System.Drawing.Color.White;
            this.fcInitLifterDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitLifterDown.CASE1 = null;
            this.fcInitLifterDown.CASE2 = null;
            this.fcInitLifterDown.CASE3 = null;
            this.fcInitLifterDown.Location = new System.Drawing.Point(265, 162);
            this.fcInitLifterDown.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitLifterDown.Name = "fcInitLifterDown";
            this.fcInitLifterDown.NEXT = this.fcInitHomeMotorY;
            this.fcInitLifterDown.Size = new System.Drawing.Size(241, 33);
            this.fcInitLifterDown.TabIndex = 65;
            this.fcInitLifterDown.Text = "Lifter Down";
            this.fcInitLifterDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitLifterDown.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitLifterDown_FlowRun);
            // 
            // fcInitHomeMotorY
            // 
            this.fcInitHomeMotorY.BackColor = System.Drawing.Color.White;
            this.fcInitHomeMotorY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitHomeMotorY.CASE1 = null;
            this.fcInitHomeMotorY.CASE2 = null;
            this.fcInitHomeMotorY.CASE3 = null;
            this.fcInitHomeMotorY.Location = new System.Drawing.Point(265, 207);
            this.fcInitHomeMotorY.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitHomeMotorY.Name = "fcInitHomeMotorY";
            this.fcInitHomeMotorY.NEXT = this.fcInitGoLoadPos;
            this.fcInitHomeMotorY.Size = new System.Drawing.Size(241, 33);
            this.fcInitHomeMotorY.TabIndex = 11;
            this.fcInitHomeMotorY.Text = "Home Motor Y";
            this.fcInitHomeMotorY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitHomeMotorY.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitHomeMotorY_FlowRun);
            // 
            // fcInitGoLoadPos
            // 
            this.fcInitGoLoadPos.BackColor = System.Drawing.Color.White;
            this.fcInitGoLoadPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitGoLoadPos.CASE1 = null;
            this.fcInitGoLoadPos.CASE2 = null;
            this.fcInitGoLoadPos.CASE3 = null;
            this.fcInitGoLoadPos.Location = new System.Drawing.Point(265, 252);
            this.fcInitGoLoadPos.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.fcInitGoLoadPos.Name = "fcInitGoLoadPos";
            this.fcInitGoLoadPos.NEXT = this.fcInitEnd;
            this.fcInitGoLoadPos.Size = new System.Drawing.Size(241, 33);
            this.fcInitGoLoadPos.TabIndex = 111;
            this.fcInitGoLoadPos.Text = "Go Load Pos";
            this.fcInitGoLoadPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitGoLoadPos.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitGoLoadPos_FlowRun);
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "MtrYSpeed";
            this.dataColumn1.DataType = typeof(double);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "MtrYAcc";
            this.dataColumn2.DataType = typeof(double);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "MtrYDcc";
            this.dataColumn3.DataType = typeof(double);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "LoadPos";
            this.dataColumn4.DataType = typeof(double);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "BypassBuffer";
            this.dataColumn5.DataType = typeof(bool);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox16);
            this.groupBox1.Controls.Add(this.groupBox13);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(1044, 516);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor Y";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 193);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(307, 73);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pick Position";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.RecipeData, "RSet.PickPos", true));
            this.textBox1.Location = new System.Drawing.Point(8, 29);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 30);
            this.textBox1.TabIndex = 2;
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(149, 27);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Goto";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnYGoPickPos_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.textBox9);
            this.groupBox16.Controls.Add(this.btnGotoUnloadPos);
            this.groupBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox16.Location = new System.Drawing.Point(8, 113);
            this.groupBox16.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox16.Size = new System.Drawing.Size(307, 73);
            this.groupBox16.TabIndex = 3;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Unload Position";
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.RecipeData, "RSet.UnloadPos", true));
            this.textBox9.Location = new System.Drawing.Point(8, 29);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(132, 30);
            this.textBox9.TabIndex = 2;
            this.textBox9.DoubleClick += new System.EventHandler(this.textBox9_DoubleClick);
            // 
            // btnGotoUnloadPos
            // 
            this.btnGotoUnloadPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoUnloadPos.Location = new System.Drawing.Point(149, 27);
            this.btnGotoUnloadPos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGotoUnloadPos.Name = "btnGotoUnloadPos";
            this.btnGotoUnloadPos.Size = new System.Drawing.Size(133, 35);
            this.btnGotoUnloadPos.TabIndex = 1;
            this.btnGotoUnloadPos.Text = "Goto";
            this.btnGotoUnloadPos.UseVisualStyleBackColor = true;
            this.btnGotoUnloadPos.Click += new System.EventHandler(this.btnGotoUnloadPos_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.textBox7);
            this.groupBox13.Controls.Add(this.btnGotoLoadPos);
            this.groupBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.Location = new System.Drawing.Point(8, 32);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox13.Size = new System.Drawing.Size(307, 73);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Load Position";
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.RecipeData, "RSet.LoadPos", true));
            this.textBox7.Location = new System.Drawing.Point(8, 32);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(132, 30);
            this.textBox7.TabIndex = 2;
            this.textBox7.DoubleClick += new System.EventHandler(this.textBox7_DoubleClick);
            // 
            // btnGotoLoadPos
            // 
            this.btnGotoLoadPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotoLoadPos.Location = new System.Drawing.Point(149, 30);
            this.btnGotoLoadPos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGotoLoadPos.Name = "btnGotoLoadPos";
            this.btnGotoLoadPos.Size = new System.Drawing.Size(133, 35);
            this.btnGotoLoadPos.TabIndex = 1;
            this.btnGotoLoadPos.Text = "Goto";
            this.btnGotoLoadPos.UseVisualStyleBackColor = true;
            this.btnGotoLoadPos.Click += new System.EventHandler(this.btnGotoLoadPos_Click);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "UnloadPos";
            this.dataColumn7.DataType = typeof(double);
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "PickPos";
            this.dataColumn8.DataType = typeof(double);
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "PickPos2";
            this.dataColumn9.DataType = typeof(double);
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "PickPos3";
            this.dataColumn10.DataType = typeof(double);
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "PickPos4";
            this.dataColumn11.DataType = typeof(double);
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "PickPos5";
            this.dataColumn12.DataType = typeof(double);
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "PickPos6";
            this.dataColumn13.DataType = typeof(double);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(19, 27);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(1116, 84);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Motor Y";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "MSet.MtrYDcc", true));
            this.textBox2.Location = new System.Drawing.Point(868, 31);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(140, 34);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "MSet.MtrYAcc", true));
            this.textBox3.Location = new System.Drawing.Point(520, 32);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(140, 34);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "MSet.MtrYSpeed", true));
            this.textBox4.Location = new System.Drawing.Point(181, 32);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(140, 34);
            this.textBox4.TabIndex = 1;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(689, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "Deceleration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Acceleration";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 35);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "Max Speed";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbl_EnCoder_Y);
            this.groupBox5.Controls.Add(this.motorJog1);
            this.groupBox5.Controls.Add(this.motorJog2);
            this.groupBox5.Location = new System.Drawing.Point(23, 28);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(380, 120);
            this.groupBox5.TabIndex = 67;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Motor Y";
            // 
            // lbl_EnCoder_Y
            // 
            this.lbl_EnCoder_Y.Location = new System.Drawing.Point(7, 57);
            this.lbl_EnCoder_Y.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_EnCoder_Y.Name = "lbl_EnCoder_Y";
            this.lbl_EnCoder_Y.Size = new System.Drawing.Size(189, 28);
            this.lbl_EnCoder_Y.TabIndex = 65;
            this.lbl_EnCoder_Y.Text = "000.000";
            this.lbl_EnCoder_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // motorJog1
            // 
            this.motorJog1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("motorJog1.BackgroundImage")));
            this.motorJog1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.motorJog1.Direction = JabilSDK.Controls.MotorJog.DirectionType.Reverse;
            this.motorJog1.JogDirection = JabilSDK.Controls.MotorJog.JogDirectionType.JogP;
            this.motorJog1.Location = new System.Drawing.Point(204, 33);
            this.motorJog1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.motorJog1.MaximumSpeed = 30;
            this.motorJog1.MoveMode = JabilSDK.Controls.MotorJog.MoveModeType.Jog;
            this.motorJog1.Name = "motorJog1";
            this.motorJog1.RelativeDirection = JabilSDK.Controls.MotorJog.RelativeDirectionType.Positive;
            this.motorJog1.RelativeDistance = 0D;
            this.motorJog1.Size = new System.Drawing.Size(80, 75);
            this.motorJog1.SpeedRatio = 0D;
            this.motorJog1.TabIndex = 64;
            this.motorJog1.UseVisualStyleBackColor = true;
            this.motorJog1.WorkMotor = null;
            // 
            // motorJog2
            // 
            this.motorJog2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("motorJog2.BackgroundImage")));
            this.motorJog2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.motorJog2.Direction = JabilSDK.Controls.MotorJog.DirectionType.Forward;
            this.motorJog2.JogDirection = JabilSDK.Controls.MotorJog.JogDirectionType.JogN;
            this.motorJog2.Location = new System.Drawing.Point(293, 31);
            this.motorJog2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.motorJog2.MaximumSpeed = 30;
            this.motorJog2.MoveMode = JabilSDK.Controls.MotorJog.MoveModeType.Jog;
            this.motorJog2.Name = "motorJog2";
            this.motorJog2.RelativeDirection = JabilSDK.Controls.MotorJog.RelativeDirectionType.Negative;
            this.motorJog2.RelativeDistance = 0D;
            this.motorJog2.Size = new System.Drawing.Size(80, 75);
            this.motorJog2.SpeedRatio = 0D;
            this.motorJog2.TabIndex = 63;
            this.motorJog2.UseVisualStyleBackColor = true;
            this.motorJog2.WorkMotor = null;
            // 
            // Tim_UpdateUI
            // 
            this.Tim_UpdateUI.Enabled = true;
            this.Tim_UpdateUI.Interval = 1000;
            this.Tim_UpdateUI.Tick += new System.EventHandler(this.Tim_UpdateUI_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.BypassCurtainSensor", true));
            this.checkBox1.Location = new System.Drawing.Point(27, 23);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(251, 33);
            this.checkBox1.TabIndex = 103;
            this.checkBox1.Text = "Bypass Curtain Sensor";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "BypassCurtainSensor";
            this.dataColumn6.DataType = typeof(bool);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(606, 653);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 86);
            this.button2.TabIndex = 184;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowChart7
            // 
            this.flowChart7.BackColor = System.Drawing.Color.White;
            this.flowChart7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart7.CASE1 = null;
            this.flowChart7.CASE2 = null;
            this.flowChart7.CASE3 = null;
            this.flowChart7.Location = new System.Drawing.Point(280, 386);
            this.flowChart7.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart7.Name = "flowChart7";
            this.flowChart7.NEXT = this.fcStartFlow;
            this.flowChart7.Size = new System.Drawing.Size(136, 33);
            this.flowChart7.TabIndex = 176;
            this.flowChart7.Text = "Loop";
            this.flowChart7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fcStartFlow
            // 
            this.fcStartFlow.BackColor = System.Drawing.Color.White;
            this.fcStartFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcStartFlow.CASE1 = null;
            this.fcStartFlow.CASE2 = null;
            this.fcStartFlow.CASE3 = null;
            this.fcStartFlow.Location = new System.Drawing.Point(513, 245);
            this.fcStartFlow.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcStartFlow.Name = "fcStartFlow";
            this.fcStartFlow.NEXT = this.flowChart1;
            this.fcStartFlow.Size = new System.Drawing.Size(259, 33);
            this.fcStartFlow.TabIndex = 183;
            this.fcStartFlow.Text = "Start";
            this.fcStartFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcStartFlow.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcStartFlow_FlowRun);
            // 
            // flowChart1
            // 
            this.flowChart1.BackColor = System.Drawing.Color.White;
            this.flowChart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart1.CASE1 = null;
            this.flowChart1.CASE2 = null;
            this.flowChart1.CASE3 = null;
            this.flowChart1.Location = new System.Drawing.Point(513, 292);
            this.flowChart1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart1.Name = "flowChart1";
            this.flowChart1.NEXT = this.flowChart2;
            this.flowChart1.Size = new System.Drawing.Size(259, 33);
            this.flowChart1.TabIndex = 182;
            this.flowChart1.Text = "1";
            this.flowChart1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowChart2
            // 
            this.flowChart2.BackColor = System.Drawing.Color.White;
            this.flowChart2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart2.CASE1 = null;
            this.flowChart2.CASE2 = null;
            this.flowChart2.CASE3 = null;
            this.flowChart2.Location = new System.Drawing.Point(513, 339);
            this.flowChart2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart2.Name = "flowChart2";
            this.flowChart2.NEXT = this.flowChart3;
            this.flowChart2.Size = new System.Drawing.Size(259, 33);
            this.flowChart2.TabIndex = 181;
            this.flowChart2.Text = "2";
            this.flowChart2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowChart3
            // 
            this.flowChart3.BackColor = System.Drawing.Color.White;
            this.flowChart3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart3.CASE1 = this.fcM_Fail;
            this.flowChart3.CASE2 = null;
            this.flowChart3.CASE3 = null;
            this.flowChart3.Location = new System.Drawing.Point(513, 386);
            this.flowChart3.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart3.Name = "flowChart3";
            this.flowChart3.NEXT = this.flowChart4;
            this.flowChart3.Size = new System.Drawing.Size(259, 33);
            this.flowChart3.TabIndex = 180;
            this.flowChart3.Text = "3";
            this.flowChart3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart3.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.flowChart3_FlowRun);
            // 
            // fcM_Fail
            // 
            this.fcM_Fail.BackColor = System.Drawing.Color.White;
            this.fcM_Fail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcM_Fail.ButtonInitializeText = "Initialize";
            this.fcM_Fail.ButtonRetryText = "Retry";
            this.fcM_Fail.ButtonSkipText = "Bypass";
            this.fcM_Fail.CASE1 = null;
            this.fcM_Fail.CASE2 = null;
            this.fcM_Fail.CASE3 = null;
            this.fcM_Fail.Content = "Press Initialize to Continue";
            this.fcM_Fail.HideButtonInitialize = false;
            this.fcM_Fail.HideButtonMute = false;
            this.fcM_Fail.HideButtonPause = false;
            this.fcM_Fail.HideButtonRetry = true;
            this.fcM_Fail.HideButtonSkip = false;
            this.fcM_Fail.Location = new System.Drawing.Point(850, 292);
            this.fcM_Fail.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcM_Fail.Name = "fcM_Fail";
            this.fcM_Fail.NEXT = this.fcStartFlow;
            this.fcM_Fail.Size = new System.Drawing.Size(177, 37);
            this.fcM_Fail.TabIndex = 175;
            this.fcM_Fail.Text = "Fail";
            this.fcM_Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcM_Fail.Title = "Feerder 2 Error";
            // 
            // flowChart4
            // 
            this.flowChart4.BackColor = System.Drawing.Color.White;
            this.flowChart4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart4.CASE1 = null;
            this.flowChart4.CASE2 = null;
            this.flowChart4.CASE3 = null;
            this.flowChart4.Location = new System.Drawing.Point(513, 433);
            this.flowChart4.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart4.Name = "flowChart4";
            this.flowChart4.NEXT = this.flowChart5;
            this.flowChart4.Size = new System.Drawing.Size(259, 33);
            this.flowChart4.TabIndex = 179;
            this.flowChart4.Text = "4";
            this.flowChart4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowChart5
            // 
            this.flowChart5.BackColor = System.Drawing.Color.White;
            this.flowChart5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart5.CASE1 = null;
            this.flowChart5.CASE2 = null;
            this.flowChart5.CASE3 = null;
            this.flowChart5.Location = new System.Drawing.Point(513, 480);
            this.flowChart5.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart5.Name = "flowChart5";
            this.flowChart5.NEXT = this.flowChart6;
            this.flowChart5.Size = new System.Drawing.Size(259, 33);
            this.flowChart5.TabIndex = 178;
            this.flowChart5.Text = "5";
            this.flowChart5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowChart6
            // 
            this.flowChart6.BackColor = System.Drawing.Color.White;
            this.flowChart6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart6.CASE1 = null;
            this.flowChart6.CASE2 = null;
            this.flowChart6.CASE3 = null;
            this.flowChart6.Location = new System.Drawing.Point(513, 527);
            this.flowChart6.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart6.Name = "flowChart6";
            this.flowChart6.NEXT = this.flowChart7;
            this.flowChart6.Size = new System.Drawing.Size(259, 33);
            this.flowChart6.TabIndex = 177;
            this.flowChart6.Text = "6";
            this.flowChart6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Feeder2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 1049);
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "Feeder2Form";
            this.Text = "Feeder2Form";
            this.plProductionSetting.ResumeLayout(false);
            this.plProductionSetting.PerformLayout();
            this.plRecipeEditor.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            this.plFlowAuto.ResumeLayout(false);
            this.plMotionSetup.ResumeLayout(false);
            this.plMotorControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private JabilSDK.UserControlLib.FlowChart fcInitHomeMotorY;
        private JabilSDK.UserControlLib.FlowChart fcInitStart;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private JabilSDK.UserControlLib.FlowChart fcInitEnd;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Button btnGotoUnloadPos;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button btnGotoLoadPos;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbl_EnCoder_Y;
        private JabilSDK.Controls.MotorJog motorJog1;
        private JabilSDK.Controls.MotorJog motorJog2;
        private System.Windows.Forms.Timer Tim_UpdateUI;
        private JabilSDK.UserControlLib.FlowChart fcInitLockExtend;
        private JabilSDK.UserControlLib.FlowChart fcInitLifterDown;
        private JabilSDK.UserControlLib.FlowChart fcInitCheckUnloadFull;
        private JabilSDK.UserControlLib.FlowChart fcInitGoLoadPos;
        private JabilSDK.UserControlLib.FlowChart fcInitSkip;
        private AcuraLibrary.Forms.FlowChartMessage fcM_InitUnloadTrayFull;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Data.DataColumn dataColumn6;
        private System.Windows.Forms.Button button2;
        private JabilSDK.UserControlLib.FlowChart flowChart7;
        private JabilSDK.UserControlLib.FlowChart fcStartFlow;
        private JabilSDK.UserControlLib.FlowChart flowChart1;
        private JabilSDK.UserControlLib.FlowChart flowChart2;
        private JabilSDK.UserControlLib.FlowChart flowChart3;
        private AcuraLibrary.Forms.FlowChartMessage fcM_Fail;
        private JabilSDK.UserControlLib.FlowChart flowChart4;
        private JabilSDK.UserControlLib.FlowChart flowChart5;
        private JabilSDK.UserControlLib.FlowChart flowChart6;
    }
}