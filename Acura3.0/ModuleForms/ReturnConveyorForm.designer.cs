namespace Acura3._0.ModuleForms
{
    partial class ReturnConveyorForm
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
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.chkEnableSmema = new System.Windows.Forms.CheckBox();
            this.chkBypassCvy = new System.Windows.Forms.CheckBox();
            this.dataColumn15 = new System.Data.DataColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLoadTimeout = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUnloadTimeout = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.toolTpCvy = new System.Windows.Forms.ToolTip(this.components);
            this.CvyTim_UpdateUI = new System.Windows.Forms.Timer(this.components);
            this.fc_Main_Flow = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_Task_Flow = new JabilSDK.UserControlLib.FlowChart();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.fc_Main_Null = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_CheckState_WP1 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_UnloadBoardFlow_WP1 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_LoadBoardFlow_WP1 = new JabilSDK.UserControlLib.FlowChart();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.fc_Main_Null2 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_CheckState_WP2 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_UnloadBoardFlow_WP2 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_LoadBoardFlow_WP2 = new JabilSDK.UserControlLib.FlowChart();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.fc_Main_Null3 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_CheckState_WP3 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_UnloadBoardFlow_WP3 = new JabilSDK.UserControlLib.FlowChart();
            this.fc_Main_LoadBoardFlow_WP3 = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmCheckSensor = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmRunConv = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmFlowEnd = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmFlow = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBypassBtm = new JabilSDK.UserControlLib.FlowChart();
            this.fcM_Fail = new AcuraLibrary.Forms.FlowChartMessage();
            this.fcStartFlow = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart1 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart2 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart3 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart4 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart5 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart6 = new JabilSDK.UserControlLib.FlowChart();
            this.flowChart7 = new JabilSDK.UserControlLib.FlowChart();
            this.button1 = new System.Windows.Forms.Button();
            this.plProductionSetting.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            this.plFlowAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaintenance
            // 
            this.plMaintenance.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plMaintenance.Size = new System.Drawing.Size(1205, 991);
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.Controls.Add(this.groupBox14);
            this.plProductionSetting.Controls.Add(this.groupBox15);
            this.plProductionSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plProductionSetting.Size = new System.Drawing.Size(1068, 800);
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.AutoScroll = false;
            this.plRecipeEditor.Enabled = false;
            this.plRecipeEditor.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plRecipeEditor.Size = new System.Drawing.Size(1068, 800);
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.Controls.Add(this.fcInitBypassBtm);
            this.plFlowInitial.Controls.Add(this.fcInitBtmCheckSensor);
            this.plFlowInitial.Controls.Add(this.fcInitBtmRunConv);
            this.plFlowInitial.Controls.Add(this.fcInitBtmFlowEnd);
            this.plFlowInitial.Controls.Add(this.fcInitBtmFlow);
            this.plFlowInitial.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plFlowInitial.Size = new System.Drawing.Size(1068, 800);
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.Controls.Add(this.button1);
            this.plFlowAuto.Controls.Add(this.flowChart7);
            this.plFlowAuto.Controls.Add(this.flowChart6);
            this.plFlowAuto.Controls.Add(this.flowChart5);
            this.plFlowAuto.Controls.Add(this.flowChart4);
            this.plFlowAuto.Controls.Add(this.flowChart3);
            this.plFlowAuto.Controls.Add(this.flowChart2);
            this.plFlowAuto.Controls.Add(this.flowChart1);
            this.plFlowAuto.Controls.Add(this.fcStartFlow);
            this.plFlowAuto.Controls.Add(this.fcM_Fail);
            this.plFlowAuto.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plFlowAuto.Size = new System.Drawing.Size(1205, 991);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Enabled = false;
            this.plMachineStatus.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMachineStatus.Size = new System.Drawing.Size(1068, 800);
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.Enabled = false;
            this.plMotionSetup.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMotionSetup.Size = new System.Drawing.Size(1068, 800);
            // 
            // plMotorControl
            // 
            this.plMotorControl.Enabled = false;
            this.plMotorControl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMotorControl.Size = new System.Drawing.Size(1068, 800);
            // 
            // PSet
            // 
            this.PSet.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn15,
            this.dataColumn17,
            this.dataColumn18});
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "UseSMEMA";
            this.dataColumn5.ColumnName = "UseSMEMA";
            this.dataColumn5.DataType = typeof(bool);
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "InPostDelay";
            this.dataColumn6.ColumnName = "InPostDelay";
            this.dataColumn6.DataType = typeof(int);
            // 
            // chkEnableSmema
            // 
            this.chkEnableSmema.AutoSize = true;
            this.chkEnableSmema.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.UseSMEMA", true));
            this.chkEnableSmema.Location = new System.Drawing.Point(20, 35);
            this.chkEnableSmema.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkEnableSmema.Name = "chkEnableSmema";
            this.chkEnableSmema.Size = new System.Drawing.Size(181, 33);
            this.chkEnableSmema.TabIndex = 10;
            this.chkEnableSmema.Text = "Enable SMEMA ";
            this.chkEnableSmema.UseVisualStyleBackColor = true;
            // 
            // chkBypassCvy
            // 
            this.chkBypassCvy.AutoSize = true;
            this.chkBypassCvy.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.BypassConveyor", true));
            this.chkBypassCvy.Location = new System.Drawing.Point(20, 70);
            this.chkBypassCvy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkBypassCvy.Name = "chkBypassCvy";
            this.chkBypassCvy.Size = new System.Drawing.Size(201, 33);
            this.chkBypassCvy.TabIndex = 95;
            this.chkBypassCvy.Text = "Bypass Conveyor";
            this.toolTpCvy.SetToolTip(this.chkBypassCvy, "For Debug Purpose Only\r\nEnable this wil ignored Initial & Auto flow");
            this.chkBypassCvy.UseVisualStyleBackColor = false;
            // 
            // dataColumn15
            // 
            this.dataColumn15.ColumnName = "BypassConveyor";
            this.dataColumn15.DataType = typeof(bool);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(448, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 29);
            this.label10.TabIndex = 99;
            this.label10.Text = "ms";
            // 
            // txtLoadTimeout
            // 
            this.txtLoadTimeout.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "PSet.PalletLoadTimeOut", true));
            this.txtLoadTimeout.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoadTimeout.Location = new System.Drawing.Point(289, 55);
            this.txtLoadTimeout.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtLoadTimeout.Name = "txtLoadTimeout";
            this.txtLoadTimeout.Size = new System.Drawing.Size(136, 34);
            this.txtLoadTimeout.TabIndex = 97;
            this.txtLoadTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 59);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(222, 29);
            this.label12.TabIndex = 98;
            this.label12.Text = "Product Load Timeout";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(448, 107);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 29);
            this.label13.TabIndex = 102;
            this.label13.Text = "ms";
            // 
            // txtUnloadTimeout
            // 
            this.txtUnloadTimeout.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.SettingData, "PSet.PalletUnloadTimeOut", true));
            this.txtUnloadTimeout.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnloadTimeout.Location = new System.Drawing.Point(289, 102);
            this.txtUnloadTimeout.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtUnloadTimeout.Name = "txtUnloadTimeout";
            this.txtUnloadTimeout.Size = new System.Drawing.Size(136, 34);
            this.txtUnloadTimeout.TabIndex = 100;
            this.txtUnloadTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(17, 105);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(241, 29);
            this.label14.TabIndex = 101;
            this.label14.Text = "Product Unload Timeout";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "PalletUnloadTimeOut";
            this.dataColumn17.ColumnName = "PalletUnloadTimeOut";
            this.dataColumn17.DataType = typeof(int);
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "PalletLoadTimeOut";
            this.dataColumn18.ColumnName = "PalletLoadTimeOut";
            this.dataColumn18.DataType = typeof(int);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.chkEnableSmema);
            this.groupBox14.Controls.Add(this.chkBypassCvy);
            this.groupBox14.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(7, 2);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox14.Size = new System.Drawing.Size(1275, 134);
            this.groupBox14.TabIndex = 103;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Main Setting";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label12);
            this.groupBox15.Controls.Add(this.txtLoadTimeout);
            this.groupBox15.Controls.Add(this.label10);
            this.groupBox15.Controls.Add(this.label13);
            this.groupBox15.Controls.Add(this.label14);
            this.groupBox15.Controls.Add(this.txtUnloadTimeout);
            this.groupBox15.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox15.Location = new System.Drawing.Point(7, 141);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox15.Size = new System.Drawing.Size(1275, 215);
            this.groupBox15.TabIndex = 19;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Conveyor Setting";
            // 
            // toolTpCvy
            // 
            this.toolTpCvy.IsBalloon = true;
            this.toolTpCvy.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTpCvy.ToolTipTitle = "AcuRA 3.0 Info";
            // 
            // CvyTim_UpdateUI
            // 
            this.CvyTim_UpdateUI.Enabled = true;
            this.CvyTim_UpdateUI.Interval = 1000;
            this.CvyTim_UpdateUI.Tick += new System.EventHandler(this.CvyTim_UpdateUI_Tick);
            // 
            // fc_Main_Flow
            // 
            this.fc_Main_Flow.BackColor = System.Drawing.Color.White;
            this.fc_Main_Flow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_Flow.CASE1 = null;
            this.fc_Main_Flow.CASE2 = null;
            this.fc_Main_Flow.CASE3 = null;
            this.fc_Main_Flow.Location = new System.Drawing.Point(383, 20);
            this.fc_Main_Flow.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_Flow.Name = "fc_Main_Flow";
            this.fc_Main_Flow.NEXT = this.fc_Main_Task_Flow;
            this.fc_Main_Flow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_Flow.Size = new System.Drawing.Size(208, 35);
            this.fc_Main_Flow.TabIndex = 7;
            this.fc_Main_Flow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_Task_Flow
            // 
            this.fc_Main_Task_Flow.BackColor = System.Drawing.Color.White;
            this.fc_Main_Task_Flow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_Task_Flow.CASE1 = null;
            this.fc_Main_Task_Flow.CASE2 = null;
            this.fc_Main_Task_Flow.CASE3 = null;
            this.fc_Main_Task_Flow.Location = new System.Drawing.Point(383, 70);
            this.fc_Main_Task_Flow.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_Task_Flow.Name = "fc_Main_Task_Flow";
            this.fc_Main_Task_Flow.NEXT = null;
            this.fc_Main_Task_Flow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_Task_Flow.Size = new System.Drawing.Size(208, 35);
            this.fc_Main_Task_Flow.TabIndex = 14;
            this.fc_Main_Task_Flow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox19
            // 
            this.groupBox19.Location = new System.Drawing.Point(10, 115);
            this.groupBox19.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox19.Size = new System.Drawing.Size(305, 217);
            this.groupBox19.TabIndex = 13;
            this.groupBox19.TabStop = false;
            // 
            // fc_Main_Null
            // 
            this.fc_Main_Null.BackColor = System.Drawing.Color.White;
            this.fc_Main_Null.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_Null.CASE1 = null;
            this.fc_Main_Null.CASE2 = null;
            this.fc_Main_Null.CASE3 = null;
            this.fc_Main_Null.Location = new System.Drawing.Point(264, 97);
            this.fc_Main_Null.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_Null.Name = "fc_Main_Null";
            this.fc_Main_Null.NEXT = this.fc_Main_CheckState_WP1;
            this.fc_Main_Null.Size = new System.Drawing.Size(24, 26);
            this.fc_Main_Null.TabIndex = 12;
            this.fc_Main_Null.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_CheckState_WP1
            // 
            this.fc_Main_CheckState_WP1.BackColor = System.Drawing.Color.White;
            this.fc_Main_CheckState_WP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_CheckState_WP1.CASE1 = this.fc_Main_UnloadBoardFlow_WP1;
            this.fc_Main_CheckState_WP1.CASE2 = null;
            this.fc_Main_CheckState_WP1.CASE3 = null;
            this.fc_Main_CheckState_WP1.Location = new System.Drawing.Point(117, 29);
            this.fc_Main_CheckState_WP1.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_CheckState_WP1.Name = "fc_Main_CheckState_WP1";
            this.fc_Main_CheckState_WP1.NEXT = this.fc_Main_LoadBoardFlow_WP1;
            this.fc_Main_CheckState_WP1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_CheckState_WP1.Size = new System.Drawing.Size(122, 35);
            this.fc_Main_CheckState_WP1.TabIndex = 8;
            this.fc_Main_CheckState_WP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_UnloadBoardFlow_WP1
            // 
            this.fc_Main_UnloadBoardFlow_WP1.BackColor = System.Drawing.Color.White;
            this.fc_Main_UnloadBoardFlow_WP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_UnloadBoardFlow_WP1.CASE1 = null;
            this.fc_Main_UnloadBoardFlow_WP1.CASE2 = null;
            this.fc_Main_UnloadBoardFlow_WP1.CASE3 = null;
            this.fc_Main_UnloadBoardFlow_WP1.Location = new System.Drawing.Point(117, 153);
            this.fc_Main_UnloadBoardFlow_WP1.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_UnloadBoardFlow_WP1.Name = "fc_Main_UnloadBoardFlow_WP1";
            this.fc_Main_UnloadBoardFlow_WP1.NEXT = this.fc_Main_Null;
            this.fc_Main_UnloadBoardFlow_WP1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_UnloadBoardFlow_WP1.Size = new System.Drawing.Size(122, 53);
            this.fc_Main_UnloadBoardFlow_WP1.TabIndex = 11;
            this.fc_Main_UnloadBoardFlow_WP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_LoadBoardFlow_WP1
            // 
            this.fc_Main_LoadBoardFlow_WP1.BackColor = System.Drawing.Color.White;
            this.fc_Main_LoadBoardFlow_WP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_LoadBoardFlow_WP1.CASE1 = null;
            this.fc_Main_LoadBoardFlow_WP1.CASE2 = null;
            this.fc_Main_LoadBoardFlow_WP1.CASE3 = null;
            this.fc_Main_LoadBoardFlow_WP1.Location = new System.Drawing.Point(11, 86);
            this.fc_Main_LoadBoardFlow_WP1.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_LoadBoardFlow_WP1.Name = "fc_Main_LoadBoardFlow_WP1";
            this.fc_Main_LoadBoardFlow_WP1.NEXT = this.fc_Main_Null;
            this.fc_Main_LoadBoardFlow_WP1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_LoadBoardFlow_WP1.Size = new System.Drawing.Size(136, 48);
            this.fc_Main_LoadBoardFlow_WP1.TabIndex = 9;
            this.fc_Main_LoadBoardFlow_WP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox20
            // 
            this.groupBox20.Location = new System.Drawing.Point(331, 115);
            this.groupBox20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox20.Size = new System.Drawing.Size(305, 217);
            this.groupBox20.TabIndex = 14;
            this.groupBox20.TabStop = false;
            // 
            // fc_Main_Null2
            // 
            this.fc_Main_Null2.BackColor = System.Drawing.Color.White;
            this.fc_Main_Null2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_Null2.CASE1 = null;
            this.fc_Main_Null2.CASE2 = null;
            this.fc_Main_Null2.CASE3 = null;
            this.fc_Main_Null2.Location = new System.Drawing.Point(264, 97);
            this.fc_Main_Null2.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_Null2.Name = "fc_Main_Null2";
            this.fc_Main_Null2.NEXT = this.fc_Main_CheckState_WP2;
            this.fc_Main_Null2.Size = new System.Drawing.Size(24, 26);
            this.fc_Main_Null2.TabIndex = 16;
            this.fc_Main_Null2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_CheckState_WP2
            // 
            this.fc_Main_CheckState_WP2.BackColor = System.Drawing.Color.White;
            this.fc_Main_CheckState_WP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_CheckState_WP2.CASE1 = this.fc_Main_UnloadBoardFlow_WP2;
            this.fc_Main_CheckState_WP2.CASE2 = null;
            this.fc_Main_CheckState_WP2.CASE3 = null;
            this.fc_Main_CheckState_WP2.Location = new System.Drawing.Point(117, 29);
            this.fc_Main_CheckState_WP2.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_CheckState_WP2.Name = "fc_Main_CheckState_WP2";
            this.fc_Main_CheckState_WP2.NEXT = this.fc_Main_LoadBoardFlow_WP2;
            this.fc_Main_CheckState_WP2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_CheckState_WP2.Size = new System.Drawing.Size(122, 35);
            this.fc_Main_CheckState_WP2.TabIndex = 13;
            this.fc_Main_CheckState_WP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_UnloadBoardFlow_WP2
            // 
            this.fc_Main_UnloadBoardFlow_WP2.BackColor = System.Drawing.Color.White;
            this.fc_Main_UnloadBoardFlow_WP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_UnloadBoardFlow_WP2.CASE1 = null;
            this.fc_Main_UnloadBoardFlow_WP2.CASE2 = null;
            this.fc_Main_UnloadBoardFlow_WP2.CASE3 = null;
            this.fc_Main_UnloadBoardFlow_WP2.Location = new System.Drawing.Point(117, 153);
            this.fc_Main_UnloadBoardFlow_WP2.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_UnloadBoardFlow_WP2.Name = "fc_Main_UnloadBoardFlow_WP2";
            this.fc_Main_UnloadBoardFlow_WP2.NEXT = this.fc_Main_Null2;
            this.fc_Main_UnloadBoardFlow_WP2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_UnloadBoardFlow_WP2.Size = new System.Drawing.Size(122, 53);
            this.fc_Main_UnloadBoardFlow_WP2.TabIndex = 15;
            this.fc_Main_UnloadBoardFlow_WP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_LoadBoardFlow_WP2
            // 
            this.fc_Main_LoadBoardFlow_WP2.BackColor = System.Drawing.Color.White;
            this.fc_Main_LoadBoardFlow_WP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_LoadBoardFlow_WP2.CASE1 = null;
            this.fc_Main_LoadBoardFlow_WP2.CASE2 = null;
            this.fc_Main_LoadBoardFlow_WP2.CASE3 = null;
            this.fc_Main_LoadBoardFlow_WP2.Location = new System.Drawing.Point(11, 86);
            this.fc_Main_LoadBoardFlow_WP2.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_LoadBoardFlow_WP2.Name = "fc_Main_LoadBoardFlow_WP2";
            this.fc_Main_LoadBoardFlow_WP2.NEXT = this.fc_Main_Null2;
            this.fc_Main_LoadBoardFlow_WP2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_LoadBoardFlow_WP2.Size = new System.Drawing.Size(136, 48);
            this.fc_Main_LoadBoardFlow_WP2.TabIndex = 14;
            this.fc_Main_LoadBoardFlow_WP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox21
            // 
            this.groupBox21.Location = new System.Drawing.Point(652, 115);
            this.groupBox21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox21.Size = new System.Drawing.Size(305, 217);
            this.groupBox21.TabIndex = 15;
            this.groupBox21.TabStop = false;
            // 
            // fc_Main_Null3
            // 
            this.fc_Main_Null3.BackColor = System.Drawing.Color.White;
            this.fc_Main_Null3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_Null3.CASE1 = null;
            this.fc_Main_Null3.CASE2 = null;
            this.fc_Main_Null3.CASE3 = null;
            this.fc_Main_Null3.Location = new System.Drawing.Point(264, 97);
            this.fc_Main_Null3.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_Null3.Name = "fc_Main_Null3";
            this.fc_Main_Null3.NEXT = this.fc_Main_CheckState_WP3;
            this.fc_Main_Null3.Size = new System.Drawing.Size(24, 26);
            this.fc_Main_Null3.TabIndex = 20;
            this.fc_Main_Null3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_CheckState_WP3
            // 
            this.fc_Main_CheckState_WP3.BackColor = System.Drawing.Color.White;
            this.fc_Main_CheckState_WP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_CheckState_WP3.CASE1 = this.fc_Main_UnloadBoardFlow_WP3;
            this.fc_Main_CheckState_WP3.CASE2 = null;
            this.fc_Main_CheckState_WP3.CASE3 = null;
            this.fc_Main_CheckState_WP3.Location = new System.Drawing.Point(117, 29);
            this.fc_Main_CheckState_WP3.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_CheckState_WP3.Name = "fc_Main_CheckState_WP3";
            this.fc_Main_CheckState_WP3.NEXT = this.fc_Main_LoadBoardFlow_WP3;
            this.fc_Main_CheckState_WP3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_CheckState_WP3.Size = new System.Drawing.Size(122, 35);
            this.fc_Main_CheckState_WP3.TabIndex = 17;
            this.fc_Main_CheckState_WP3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_UnloadBoardFlow_WP3
            // 
            this.fc_Main_UnloadBoardFlow_WP3.BackColor = System.Drawing.Color.White;
            this.fc_Main_UnloadBoardFlow_WP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_UnloadBoardFlow_WP3.CASE1 = null;
            this.fc_Main_UnloadBoardFlow_WP3.CASE2 = null;
            this.fc_Main_UnloadBoardFlow_WP3.CASE3 = null;
            this.fc_Main_UnloadBoardFlow_WP3.Location = new System.Drawing.Point(117, 153);
            this.fc_Main_UnloadBoardFlow_WP3.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_UnloadBoardFlow_WP3.Name = "fc_Main_UnloadBoardFlow_WP3";
            this.fc_Main_UnloadBoardFlow_WP3.NEXT = this.fc_Main_Null3;
            this.fc_Main_UnloadBoardFlow_WP3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_UnloadBoardFlow_WP3.Size = new System.Drawing.Size(122, 53);
            this.fc_Main_UnloadBoardFlow_WP3.TabIndex = 19;
            this.fc_Main_UnloadBoardFlow_WP3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fc_Main_LoadBoardFlow_WP3
            // 
            this.fc_Main_LoadBoardFlow_WP3.BackColor = System.Drawing.Color.White;
            this.fc_Main_LoadBoardFlow_WP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fc_Main_LoadBoardFlow_WP3.CASE1 = null;
            this.fc_Main_LoadBoardFlow_WP3.CASE2 = null;
            this.fc_Main_LoadBoardFlow_WP3.CASE3 = null;
            this.fc_Main_LoadBoardFlow_WP3.Location = new System.Drawing.Point(11, 86);
            this.fc_Main_LoadBoardFlow_WP3.Margin = new System.Windows.Forms.Padding(8);
            this.fc_Main_LoadBoardFlow_WP3.Name = "fc_Main_LoadBoardFlow_WP3";
            this.fc_Main_LoadBoardFlow_WP3.NEXT = this.fc_Main_Null3;
            this.fc_Main_LoadBoardFlow_WP3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fc_Main_LoadBoardFlow_WP3.Size = new System.Drawing.Size(136, 48);
            this.fc_Main_LoadBoardFlow_WP3.TabIndex = 18;
            this.fc_Main_LoadBoardFlow_WP3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fcInitBtmCheckSensor
            // 
            this.fcInitBtmCheckSensor.BackColor = System.Drawing.Color.White;
            this.fcInitBtmCheckSensor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmCheckSensor.CASE1 = this.fcInitBtmRunConv;
            this.fcInitBtmCheckSensor.CASE2 = null;
            this.fcInitBtmCheckSensor.CASE3 = null;
            this.fcInitBtmCheckSensor.Location = new System.Drawing.Point(440, 99);
            this.fcInitBtmCheckSensor.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcInitBtmCheckSensor.Name = "fcInitBtmCheckSensor";
            this.fcInitBtmCheckSensor.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBtmCheckSensor.Size = new System.Drawing.Size(311, 40);
            this.fcInitBtmCheckSensor.TabIndex = 64;
            this.fcInitBtmCheckSensor.Text = "Check Sensor";
            this.fcInitBtmCheckSensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmCheckSensor.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmCheckSensor_FlowRun);
            // 
            // fcInitBtmRunConv
            // 
            this.fcInitBtmRunConv.BackColor = System.Drawing.Color.White;
            this.fcInitBtmRunConv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmRunConv.CASE1 = null;
            this.fcInitBtmRunConv.CASE2 = null;
            this.fcInitBtmRunConv.CASE3 = null;
            this.fcInitBtmRunConv.Location = new System.Drawing.Point(803, 134);
            this.fcInitBtmRunConv.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcInitBtmRunConv.Name = "fcInitBtmRunConv";
            this.fcInitBtmRunConv.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBtmRunConv.Size = new System.Drawing.Size(230, 40);
            this.fcInitBtmRunConv.TabIndex = 63;
            this.fcInitBtmRunConv.Text = "Run Conveyor";
            this.fcInitBtmRunConv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmRunConv.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmRunConv_FlowRun);
            // 
            // fcInitBtmFlowEnd
            // 
            this.fcInitBtmFlowEnd.BackColor = System.Drawing.Color.White;
            this.fcInitBtmFlowEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmFlowEnd.CASE1 = null;
            this.fcInitBtmFlowEnd.CASE2 = null;
            this.fcInitBtmFlowEnd.CASE3 = null;
            this.fcInitBtmFlowEnd.Location = new System.Drawing.Point(440, 175);
            this.fcInitBtmFlowEnd.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcInitBtmFlowEnd.Name = "fcInitBtmFlowEnd";
            this.fcInitBtmFlowEnd.NEXT = null;
            this.fcInitBtmFlowEnd.Size = new System.Drawing.Size(311, 40);
            this.fcInitBtmFlowEnd.TabIndex = 52;
            this.fcInitBtmFlowEnd.Text = "Bottom Initialize End";
            this.fcInitBtmFlowEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmFlowEnd.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmFlowEnd_FlowRun);
            // 
            // fcInitBtmFlow
            // 
            this.fcInitBtmFlow.BackColor = System.Drawing.Color.White;
            this.fcInitBtmFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmFlow.CASE1 = this.fcInitBypassBtm;
            this.fcInitBtmFlow.CASE2 = null;
            this.fcInitBtmFlow.CASE3 = null;
            this.fcInitBtmFlow.Location = new System.Drawing.Point(440, 28);
            this.fcInitBtmFlow.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcInitBtmFlow.Name = "fcInitBtmFlow";
            this.fcInitBtmFlow.NEXT = this.fcInitBtmCheckSensor;
            this.fcInitBtmFlow.Size = new System.Drawing.Size(311, 40);
            this.fcInitBtmFlow.TabIndex = 51;
            this.fcInitBtmFlow.Text = "Bottom Initialize Flow";
            this.fcInitBtmFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmFlow.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmFlow_FlowRun);
            // 
            // fcInitBypassBtm
            // 
            this.fcInitBypassBtm.BackColor = System.Drawing.Color.White;
            this.fcInitBypassBtm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBypassBtm.CASE1 = null;
            this.fcInitBypassBtm.CASE2 = null;
            this.fcInitBypassBtm.CASE3 = null;
            this.fcInitBypassBtm.Location = new System.Drawing.Point(101, 99);
            this.fcInitBypassBtm.Margin = new System.Windows.Forms.Padding(11, 8, 11, 8);
            this.fcInitBypassBtm.Name = "fcInitBypassBtm";
            this.fcInitBypassBtm.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBypassBtm.Size = new System.Drawing.Size(259, 40);
            this.fcInitBypassBtm.TabIndex = 66;
            this.fcInitBypassBtm.Text = "Bypass Conveyor";
            this.fcInitBypassBtm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.fcM_Fail.Location = new System.Drawing.Point(744, 286);
            this.fcM_Fail.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.fcM_Fail.Name = "fcM_Fail";
            this.fcM_Fail.NEXT = this.fcStartFlow;
            this.fcM_Fail.Size = new System.Drawing.Size(177, 37);
            this.fcM_Fail.TabIndex = 172;
            this.fcM_Fail.Text = "Fail";
            this.fcM_Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcM_Fail.Title = "ReturnConveyorForm Error";
            // 
            // fcStartFlow
            // 
            this.fcStartFlow.BackColor = System.Drawing.Color.White;
            this.fcStartFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcStartFlow.CASE1 = null;
            this.fcStartFlow.CASE2 = null;
            this.fcStartFlow.CASE3 = null;
            this.fcStartFlow.Location = new System.Drawing.Point(407, 239);
            this.fcStartFlow.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcStartFlow.Name = "fcStartFlow";
            this.fcStartFlow.NEXT = this.flowChart1;
            this.fcStartFlow.Size = new System.Drawing.Size(259, 33);
            this.fcStartFlow.TabIndex = 173;
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
            this.flowChart1.Location = new System.Drawing.Point(407, 286);
            this.flowChart1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart1.Name = "flowChart1";
            this.flowChart1.NEXT = this.flowChart2;
            this.flowChart1.Size = new System.Drawing.Size(259, 33);
            this.flowChart1.TabIndex = 173;
            this.flowChart1.Text = "1";
            this.flowChart1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart1.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.flowChart1_FlowRun);
            // 
            // flowChart2
            // 
            this.flowChart2.BackColor = System.Drawing.Color.White;
            this.flowChart2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart2.CASE1 = null;
            this.flowChart2.CASE2 = null;
            this.flowChart2.CASE3 = null;
            this.flowChart2.Location = new System.Drawing.Point(407, 333);
            this.flowChart2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart2.Name = "flowChart2";
            this.flowChart2.NEXT = this.flowChart3;
            this.flowChart2.Size = new System.Drawing.Size(259, 33);
            this.flowChart2.TabIndex = 173;
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
            this.flowChart3.Location = new System.Drawing.Point(407, 380);
            this.flowChart3.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart3.Name = "flowChart3";
            this.flowChart3.NEXT = this.flowChart4;
            this.flowChart3.Size = new System.Drawing.Size(259, 33);
            this.flowChart3.TabIndex = 173;
            this.flowChart3.Text = "3";
            this.flowChart3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flowChart3.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.flowChart3_FlowRun);
            // 
            // flowChart4
            // 
            this.flowChart4.BackColor = System.Drawing.Color.White;
            this.flowChart4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart4.CASE1 = null;
            this.flowChart4.CASE2 = null;
            this.flowChart4.CASE3 = null;
            this.flowChart4.Location = new System.Drawing.Point(407, 427);
            this.flowChart4.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart4.Name = "flowChart4";
            this.flowChart4.NEXT = this.flowChart5;
            this.flowChart4.Size = new System.Drawing.Size(259, 33);
            this.flowChart4.TabIndex = 173;
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
            this.flowChart5.Location = new System.Drawing.Point(407, 474);
            this.flowChart5.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart5.Name = "flowChart5";
            this.flowChart5.NEXT = this.flowChart6;
            this.flowChart5.Size = new System.Drawing.Size(259, 33);
            this.flowChart5.TabIndex = 173;
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
            this.flowChart6.Location = new System.Drawing.Point(407, 521);
            this.flowChart6.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart6.Name = "flowChart6";
            this.flowChart6.NEXT = this.flowChart7;
            this.flowChart6.Size = new System.Drawing.Size(259, 33);
            this.flowChart6.TabIndex = 173;
            this.flowChart6.Text = "6";
            this.flowChart6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowChart7
            // 
            this.flowChart7.BackColor = System.Drawing.Color.White;
            this.flowChart7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowChart7.CASE1 = null;
            this.flowChart7.CASE2 = null;
            this.flowChart7.CASE3 = null;
            this.flowChart7.Location = new System.Drawing.Point(174, 380);
            this.flowChart7.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.flowChart7.Name = "flowChart7";
            this.flowChart7.NEXT = this.fcStartFlow;
            this.flowChart7.Size = new System.Drawing.Size(136, 33);
            this.flowChart7.TabIndex = 173;
            this.flowChart7.Text = "Loop";
            this.flowChart7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 647);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 86);
            this.button1.TabIndex = 174;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReturnConveyorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1221, 1055);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ReturnConveyorForm";
            this.Text = "ReturnConveyorForm";
            this.plProductionSetting.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            this.plFlowAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Windows.Forms.CheckBox chkEnableSmema;
        private System.Windows.Forms.CheckBox chkBypassCvy;
        private System.Data.DataColumn dataColumn15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUnloadTimeout;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLoadTimeout;
        private System.Windows.Forms.Label label12;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.ToolTip toolTpCvy;
        private System.Windows.Forms.Timer CvyTim_UpdateUI;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmCheckSensor;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmRunConv;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmFlowEnd;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmFlow;
        private JabilSDK.UserControlLib.FlowChart fc_Main_Flow;
        private JabilSDK.UserControlLib.FlowChart fc_Main_Task_Flow;
        private System.Windows.Forms.GroupBox groupBox19;
        private JabilSDK.UserControlLib.FlowChart fc_Main_Null;
        private JabilSDK.UserControlLib.FlowChart fc_Main_CheckState_WP1;
        private JabilSDK.UserControlLib.FlowChart fc_Main_UnloadBoardFlow_WP1;
        private JabilSDK.UserControlLib.FlowChart fc_Main_LoadBoardFlow_WP1;
        private System.Windows.Forms.GroupBox groupBox20;
        private JabilSDK.UserControlLib.FlowChart fc_Main_Null2;
        private JabilSDK.UserControlLib.FlowChart fc_Main_CheckState_WP2;
        private JabilSDK.UserControlLib.FlowChart fc_Main_UnloadBoardFlow_WP2;
        private JabilSDK.UserControlLib.FlowChart fc_Main_LoadBoardFlow_WP2;
        private System.Windows.Forms.GroupBox groupBox21;
        private JabilSDK.UserControlLib.FlowChart fc_Main_Null3;
        private JabilSDK.UserControlLib.FlowChart fc_Main_CheckState_WP3;
        private JabilSDK.UserControlLib.FlowChart fc_Main_UnloadBoardFlow_WP3;
        private JabilSDK.UserControlLib.FlowChart fc_Main_LoadBoardFlow_WP3;
        private JabilSDK.UserControlLib.FlowChart fcInitBypassBtm;
        private JabilSDK.UserControlLib.FlowChart flowChart5;
        private JabilSDK.UserControlLib.FlowChart flowChart4;
        private JabilSDK.UserControlLib.FlowChart flowChart3;
        private JabilSDK.UserControlLib.FlowChart flowChart2;
        private JabilSDK.UserControlLib.FlowChart flowChart1;
        private JabilSDK.UserControlLib.FlowChart fcStartFlow;
        private AcuraLibrary.Forms.FlowChartMessage fcM_Fail;
        private JabilSDK.UserControlLib.FlowChart flowChart6;
        private JabilSDK.UserControlLib.FlowChart flowChart7;
        private System.Windows.Forms.Button button1;
    }
}