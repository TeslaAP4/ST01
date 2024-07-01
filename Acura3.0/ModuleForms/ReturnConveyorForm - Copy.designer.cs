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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IB_BtmCvyError = new JabilSDK.Controls.Input();
            this.IB_BtmBoardIn = new JabilSDK.Controls.Input();
            this.OB_BtmCvy_Forward = new JabilSDK.Controls.Output();
            this.IB_BtmBoardOut = new JabilSDK.Controls.Input();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IB_SMEMA_BTM_DN_Available = new JabilSDK.Controls.Input();
            this.IB_SMEMA_BTM_UP_MachineRdy = new JabilSDK.Controls.Input();
            this.OB_SMEMA_BTM_DW_MachineRdy = new JabilSDK.Controls.Output();
            this.OB_SMEMA_BTM_UP_Available = new JabilSDK.Controls.Output();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fcBtmMainWaitBoardOutOn = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmWaitStartButton2 = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainCovRun1 = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainCovStop1 = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainEnd = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainWaitBoardInOn = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmWaitStartButton1 = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainCovRun = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainCovStop = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainWaitDownSmema = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmDelay = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainNull = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainWaitSmema = new JabilSDK.UserControlLib.FlowChart();
            this.fcBtmMainFlow = new JabilSDK.UserControlLib.FlowChart();
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
            this.fcInitBtmUnloadWaitStart = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmUnloadConvRun = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmUnloadWaitBoardOutOn = new JabilSDK.UserControlLib.FlowChart();
            this.fcinitBtmUnloadConvStop = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmLoadWaitStart = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmLoadConvRun = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmLoadWaitBoardInOn = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmLoadConvStop = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmLoadWaitSMEMADownStream = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmCheckPart = new JabilSDK.UserControlLib.FlowChart();
            this.fcInitBtmFlow = new JabilSDK.UserControlLib.FlowChart();
            this.plMaintenance.SuspendLayout();
            this.plProductionSetting.SuspendLayout();
            this.plFlowInitial.SuspendLayout();
            this.plFlowAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.SuspendLayout();
            // 
            // plMaintenance
            // 
            this.plMaintenance.Controls.Add(this.groupBox8);
            this.plMaintenance.Controls.Add(this.groupBox2);
            this.plMaintenance.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plMaintenance.Size = new System.Drawing.Size(1068, 856);
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.Controls.Add(this.groupBox14);
            this.plProductionSetting.Controls.Add(this.groupBox15);
            this.plProductionSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plProductionSetting.Size = new System.Drawing.Size(1068, 856);
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.AutoScroll = false;
            this.plRecipeEditor.Enabled = false;
            this.plRecipeEditor.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plRecipeEditor.Size = new System.Drawing.Size(1068, 856);
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.Controls.Add(this.fcInitBtmCheckSensor);
            this.plFlowInitial.Controls.Add(this.fcInitBtmRunConv);
            this.plFlowInitial.Controls.Add(this.fcInitBtmUnloadWaitStart);
            this.plFlowInitial.Controls.Add(this.fcInitBtmLoadWaitStart);
            this.plFlowInitial.Controls.Add(this.fcInitBtmUnloadWaitBoardOutOn);
            this.plFlowInitial.Controls.Add(this.fcinitBtmUnloadConvStop);
            this.plFlowInitial.Controls.Add(this.fcInitBtmUnloadConvRun);
            this.plFlowInitial.Controls.Add(this.fcInitBtmLoadWaitBoardInOn);
            this.plFlowInitial.Controls.Add(this.fcInitBtmLoadConvStop);
            this.plFlowInitial.Controls.Add(this.fcInitBtmLoadWaitSMEMADownStream);
            this.plFlowInitial.Controls.Add(this.fcInitBtmLoadConvRun);
            this.plFlowInitial.Controls.Add(this.fcInitBtmCheckPart);
            this.plFlowInitial.Controls.Add(this.fcInitBtmFlowEnd);
            this.plFlowInitial.Controls.Add(this.fcInitBtmFlow);
            this.plFlowInitial.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plFlowInitial.Size = new System.Drawing.Size(1068, 856);
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.Controls.Add(this.tabControl3);
            this.plFlowAuto.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.plFlowAuto.Size = new System.Drawing.Size(1068, 856);
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.Enabled = false;
            this.plMachineStatus.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMachineStatus.Size = new System.Drawing.Size(1068, 856);
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.Enabled = false;
            this.plMotionSetup.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMotionSetup.Size = new System.Drawing.Size(1068, 856);
            // 
            // plMotorControl
            // 
            this.plMotorControl.Enabled = false;
            this.plMotorControl.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.plMotorControl.Size = new System.Drawing.Size(1068, 856);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox2.Size = new System.Drawing.Size(435, 279);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conveyor Control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IB_BtmCvyError);
            this.groupBox1.Controls.Add(this.IB_BtmBoardIn);
            this.groupBox1.Controls.Add(this.OB_BtmCvy_Forward);
            this.groupBox1.Controls.Add(this.IB_BtmBoardOut);
            this.groupBox1.Location = new System.Drawing.Point(13, 37);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox1.Size = new System.Drawing.Size(401, 229);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Work Post 1";
            // 
            // IB_BtmCvyError
            // 
            this.IB_BtmCvyError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IB_BtmCvyError.CardType = JabilSDK.Enums.InputCardType.BECKHOFF;
            this.IB_BtmCvyError.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IB_BtmCvyError.Location = new System.Drawing.Point(20, 165);
            this.IB_BtmCvyError.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.IB_BtmCvyError.Name = "IB_BtmCvyError";
            this.IB_BtmCvyError.Port = "00511";
            this.IB_BtmCvyError.ReverseStatus = true;
            this.IB_BtmCvyError.Size = new System.Drawing.Size(363, 43);
            this.IB_BtmCvyError.TabIndex = 6;
            this.IB_BtmCvyError.Text = "Conveyor Error";
            this.IB_BtmCvyError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IB_BtmBoardIn
            // 
            this.IB_BtmBoardIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IB_BtmBoardIn.CardType = JabilSDK.Enums.InputCardType.BECKHOFF;
            this.IB_BtmBoardIn.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IB_BtmBoardIn.Location = new System.Drawing.Point(19, 37);
            this.IB_BtmBoardIn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.IB_BtmBoardIn.Name = "IB_BtmBoardIn";
            this.IB_BtmBoardIn.Port = "00509";
            this.IB_BtmBoardIn.Size = new System.Drawing.Size(175, 43);
            this.IB_BtmBoardIn.TabIndex = 8;
            this.IB_BtmBoardIn.Text = "Board In";
            this.IB_BtmBoardIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OB_BtmCvy_Forward
            // 
            this.OB_BtmCvy_Forward.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OB_BtmCvy_Forward.CardType = JabilSDK.Enums.OutputCardType.BECKHOFF;
            this.OB_BtmCvy_Forward.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OB_BtmCvy_Forward.Location = new System.Drawing.Point(20, 110);
            this.OB_BtmCvy_Forward.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.OB_BtmCvy_Forward.Name = "OB_BtmCvy_Forward";
            this.OB_BtmCvy_Forward.Port = "01113";
            this.OB_BtmCvy_Forward.Size = new System.Drawing.Size(363, 43);
            this.OB_BtmCvy_Forward.TabIndex = 23;
            this.OB_BtmCvy_Forward.Text = "Conveyor Forward";
            this.OB_BtmCvy_Forward.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IB_BtmBoardOut
            // 
            this.IB_BtmBoardOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IB_BtmBoardOut.CardType = JabilSDK.Enums.InputCardType.BECKHOFF;
            this.IB_BtmBoardOut.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IB_BtmBoardOut.Location = new System.Drawing.Point(208, 37);
            this.IB_BtmBoardOut.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.IB_BtmBoardOut.Name = "IB_BtmBoardOut";
            this.IB_BtmBoardOut.Port = "00510";
            this.IB_BtmBoardOut.Size = new System.Drawing.Size(175, 43);
            this.IB_BtmBoardOut.TabIndex = 22;
            this.IB_BtmBoardOut.Text = "Board Out";
            this.IB_BtmBoardOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.IB_SMEMA_BTM_DN_Available);
            this.groupBox8.Controls.Add(this.IB_SMEMA_BTM_UP_MachineRdy);
            this.groupBox8.Controls.Add(this.OB_SMEMA_BTM_DW_MachineRdy);
            this.groupBox8.Controls.Add(this.OB_SMEMA_BTM_UP_Available);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(447, 14);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Size = new System.Drawing.Size(619, 190);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "SMEMA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Upstream Handshake";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "Downstream Handshake";
            // 
            // IB_SMEMA_BTM_DN_Available
            // 
            this.IB_SMEMA_BTM_DN_Available.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IB_SMEMA_BTM_DN_Available.CardType = JabilSDK.Enums.InputCardType.BECKHOFF;
            this.IB_SMEMA_BTM_DN_Available.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IB_SMEMA_BTM_DN_Available.Location = new System.Drawing.Point(318, 122);
            this.IB_SMEMA_BTM_DN_Available.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.IB_SMEMA_BTM_DN_Available.Name = "IB_SMEMA_BTM_DN_Available";
            this.IB_SMEMA_BTM_DN_Available.Port = "00201";
            this.IB_SMEMA_BTM_DN_Available.Size = new System.Drawing.Size(257, 43);
            this.IB_SMEMA_BTM_DN_Available.TabIndex = 12;
            this.IB_SMEMA_BTM_DN_Available.Text = "Product Available";
            this.IB_SMEMA_BTM_DN_Available.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IB_SMEMA_BTM_UP_MachineRdy
            // 
            this.IB_SMEMA_BTM_UP_MachineRdy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IB_SMEMA_BTM_UP_MachineRdy.CardType = JabilSDK.Enums.InputCardType.BECKHOFF;
            this.IB_SMEMA_BTM_UP_MachineRdy.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IB_SMEMA_BTM_UP_MachineRdy.Location = new System.Drawing.Point(32, 67);
            this.IB_SMEMA_BTM_UP_MachineRdy.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.IB_SMEMA_BTM_UP_MachineRdy.Name = "IB_SMEMA_BTM_UP_MachineRdy";
            this.IB_SMEMA_BTM_UP_MachineRdy.Port = "00200";
            this.IB_SMEMA_BTM_UP_MachineRdy.Size = new System.Drawing.Size(257, 43);
            this.IB_SMEMA_BTM_UP_MachineRdy.TabIndex = 4;
            this.IB_SMEMA_BTM_UP_MachineRdy.Text = "Machine Ready";
            this.IB_SMEMA_BTM_UP_MachineRdy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OB_SMEMA_BTM_DW_MachineRdy
            // 
            this.OB_SMEMA_BTM_DW_MachineRdy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OB_SMEMA_BTM_DW_MachineRdy.CardType = JabilSDK.Enums.OutputCardType.BECKHOFF;
            this.OB_SMEMA_BTM_DW_MachineRdy.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OB_SMEMA_BTM_DW_MachineRdy.Location = new System.Drawing.Point(318, 66);
            this.OB_SMEMA_BTM_DW_MachineRdy.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.OB_SMEMA_BTM_DW_MachineRdy.Name = "OB_SMEMA_BTM_DW_MachineRdy";
            this.OB_SMEMA_BTM_DW_MachineRdy.Port = "00901";
            this.OB_SMEMA_BTM_DW_MachineRdy.Size = new System.Drawing.Size(257, 43);
            this.OB_SMEMA_BTM_DW_MachineRdy.TabIndex = 9;
            this.OB_SMEMA_BTM_DW_MachineRdy.Text = "Machine Ready";
            this.OB_SMEMA_BTM_DW_MachineRdy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OB_SMEMA_BTM_UP_Available
            // 
            this.OB_SMEMA_BTM_UP_Available.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OB_SMEMA_BTM_UP_Available.CardType = JabilSDK.Enums.OutputCardType.BECKHOFF;
            this.OB_SMEMA_BTM_UP_Available.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OB_SMEMA_BTM_UP_Available.Location = new System.Drawing.Point(32, 122);
            this.OB_SMEMA_BTM_UP_Available.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.OB_SMEMA_BTM_UP_Available.Name = "OB_SMEMA_BTM_UP_Available";
            this.OB_SMEMA_BTM_UP_Available.Port = "00900";
            this.OB_SMEMA_BTM_UP_Available.Size = new System.Drawing.Size(257, 43);
            this.OB_SMEMA_BTM_UP_Available.TabIndex = 7;
            this.OB_SMEMA_BTM_UP_Available.Text = "Product Available";
            this.OB_SMEMA_BTM_UP_Available.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.ItemSize = new System.Drawing.Size(118, 50);
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1068, 856);
            this.tabControl3.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 54);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage3.Size = new System.Drawing.Size(1060, 798);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Main";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.fcBtmMainWaitBoardOutOn);
            this.panel2.Controls.Add(this.fcBtmMainWaitBoardInOn);
            this.panel2.Controls.Add(this.fcBtmDelay);
            this.panel2.Controls.Add(this.fcBtmWaitStartButton2);
            this.panel2.Controls.Add(this.fcBtmWaitStartButton1);
            this.panel2.Controls.Add(this.fcBtmMainNull);
            this.panel2.Controls.Add(this.fcBtmMainEnd);
            this.panel2.Controls.Add(this.fcBtmMainCovStop1);
            this.panel2.Controls.Add(this.fcBtmMainCovRun1);
            this.panel2.Controls.Add(this.fcBtmMainWaitDownSmema);
            this.panel2.Controls.Add(this.fcBtmMainCovStop);
            this.panel2.Controls.Add(this.fcBtmMainWaitSmema);
            this.panel2.Controls.Add(this.fcBtmMainFlow);
            this.panel2.Controls.Add(this.fcBtmMainCovRun);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1052, 794);
            this.panel2.TabIndex = 18;
            // 
            // fcBtmMainWaitBoardOutOn
            // 
            this.fcBtmMainWaitBoardOutOn.BackColor = System.Drawing.Color.White;
            this.fcBtmMainWaitBoardOutOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainWaitBoardOutOn.CASE1 = this.fcBtmWaitStartButton2;
            this.fcBtmMainWaitBoardOutOn.CASE2 = null;
            this.fcBtmMainWaitBoardOutOn.CASE3 = null;
            this.fcBtmMainWaitBoardOutOn.Location = new System.Drawing.Point(388, 540);
            this.fcBtmMainWaitBoardOutOn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainWaitBoardOutOn.Name = "fcBtmMainWaitBoardOutOn";
            this.fcBtmMainWaitBoardOutOn.NEXT = this.fcBtmMainCovStop1;
            this.fcBtmMainWaitBoardOutOn.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainWaitBoardOutOn.TabIndex = 40;
            this.fcBtmMainWaitBoardOutOn.Text = "Wait Board Out On";
            this.fcBtmMainWaitBoardOutOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainWaitBoardOutOn.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainWaitBoardOutOn_FlowRun);
            // 
            // fcBtmWaitStartButton2
            // 
            this.fcBtmWaitStartButton2.BackColor = System.Drawing.Color.White;
            this.fcBtmWaitStartButton2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmWaitStartButton2.CASE1 = null;
            this.fcBtmWaitStartButton2.CASE2 = null;
            this.fcBtmWaitStartButton2.CASE3 = null;
            this.fcBtmWaitStartButton2.Location = new System.Drawing.Point(789, 508);
            this.fcBtmWaitStartButton2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmWaitStartButton2.Name = "fcBtmWaitStartButton2";
            this.fcBtmWaitStartButton2.NEXT = this.fcBtmMainCovRun1;
            this.fcBtmWaitStartButton2.Size = new System.Drawing.Size(209, 35);
            this.fcBtmWaitStartButton2.TabIndex = 37;
            this.fcBtmWaitStartButton2.Text = "Wait Start Button";
            this.fcBtmWaitStartButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmWaitStartButton2.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmWaitStartButton2_FlowRun);
            // 
            // fcBtmMainCovRun1
            // 
            this.fcBtmMainCovRun1.BackColor = System.Drawing.Color.White;
            this.fcBtmMainCovRun1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainCovRun1.CASE1 = null;
            this.fcBtmMainCovRun1.CASE2 = null;
            this.fcBtmMainCovRun1.CASE3 = null;
            this.fcBtmMainCovRun1.Location = new System.Drawing.Point(388, 463);
            this.fcBtmMainCovRun1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainCovRun1.Name = "fcBtmMainCovRun1";
            this.fcBtmMainCovRun1.NEXT = this.fcBtmMainWaitBoardOutOn;
            this.fcBtmMainCovRun1.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainCovRun1.TabIndex = 32;
            this.fcBtmMainCovRun1.Text = "Bottom Conveyor Run";
            this.fcBtmMainCovRun1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainCovRun1.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainCovRun1_FlowRun);
            // 
            // fcBtmMainCovStop1
            // 
            this.fcBtmMainCovStop1.BackColor = System.Drawing.Color.White;
            this.fcBtmMainCovStop1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainCovStop1.CASE1 = null;
            this.fcBtmMainCovStop1.CASE2 = null;
            this.fcBtmMainCovStop1.CASE3 = null;
            this.fcBtmMainCovStop1.Location = new System.Drawing.Point(388, 624);
            this.fcBtmMainCovStop1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainCovStop1.Name = "fcBtmMainCovStop1";
            this.fcBtmMainCovStop1.NEXT = this.fcBtmMainEnd;
            this.fcBtmMainCovStop1.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainCovStop1.TabIndex = 33;
            this.fcBtmMainCovStop1.Text = "Bottom Conveyor Stop";
            this.fcBtmMainCovStop1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainCovStop1.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainCovStop1_FlowRun);
            // 
            // fcBtmMainEnd
            // 
            this.fcBtmMainEnd.BackColor = System.Drawing.Color.White;
            this.fcBtmMainEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainEnd.CASE1 = null;
            this.fcBtmMainEnd.CASE2 = null;
            this.fcBtmMainEnd.CASE3 = null;
            this.fcBtmMainEnd.Location = new System.Drawing.Point(388, 694);
            this.fcBtmMainEnd.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcBtmMainEnd.Name = "fcBtmMainEnd";
            this.fcBtmMainEnd.NEXT = null;
            this.fcBtmMainEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fcBtmMainEnd.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainEnd.TabIndex = 34;
            this.fcBtmMainEnd.Text = "Bottom Main End";
            this.fcBtmMainEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainEnd.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainEnd_FlowRun);
            // 
            // fcBtmMainWaitBoardInOn
            // 
            this.fcBtmMainWaitBoardInOn.BackColor = System.Drawing.Color.White;
            this.fcBtmMainWaitBoardInOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainWaitBoardInOn.CASE1 = this.fcBtmWaitStartButton1;
            this.fcBtmMainWaitBoardInOn.CASE2 = null;
            this.fcBtmMainWaitBoardInOn.CASE3 = null;
            this.fcBtmMainWaitBoardInOn.Location = new System.Drawing.Point(388, 235);
            this.fcBtmMainWaitBoardInOn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainWaitBoardInOn.Name = "fcBtmMainWaitBoardInOn";
            this.fcBtmMainWaitBoardInOn.NEXT = this.fcBtmMainCovStop;
            this.fcBtmMainWaitBoardInOn.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainWaitBoardInOn.TabIndex = 39;
            this.fcBtmMainWaitBoardInOn.Text = "Wait Board In On";
            this.fcBtmMainWaitBoardInOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainWaitBoardInOn.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainWaitBoardInOn_FlowRun);
            // 
            // fcBtmWaitStartButton1
            // 
            this.fcBtmWaitStartButton1.BackColor = System.Drawing.Color.White;
            this.fcBtmWaitStartButton1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmWaitStartButton1.CASE1 = null;
            this.fcBtmWaitStartButton1.CASE2 = null;
            this.fcBtmWaitStartButton1.CASE3 = null;
            this.fcBtmWaitStartButton1.Location = new System.Drawing.Point(789, 198);
            this.fcBtmWaitStartButton1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmWaitStartButton1.Name = "fcBtmWaitStartButton1";
            this.fcBtmWaitStartButton1.NEXT = this.fcBtmMainCovRun;
            this.fcBtmWaitStartButton1.Size = new System.Drawing.Size(209, 35);
            this.fcBtmWaitStartButton1.TabIndex = 36;
            this.fcBtmWaitStartButton1.Text = "Wait Start Button";
            this.fcBtmWaitStartButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmWaitStartButton1.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmWaitStartButton1_FlowRun);
            // 
            // fcBtmMainCovRun
            // 
            this.fcBtmMainCovRun.BackColor = System.Drawing.Color.White;
            this.fcBtmMainCovRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainCovRun.CASE1 = null;
            this.fcBtmMainCovRun.CASE2 = null;
            this.fcBtmMainCovRun.CASE3 = null;
            this.fcBtmMainCovRun.Location = new System.Drawing.Point(388, 161);
            this.fcBtmMainCovRun.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainCovRun.Name = "fcBtmMainCovRun";
            this.fcBtmMainCovRun.NEXT = this.fcBtmMainWaitBoardInOn;
            this.fcBtmMainCovRun.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainCovRun.TabIndex = 27;
            this.fcBtmMainCovRun.Text = "Bottom Conveyor Run";
            this.fcBtmMainCovRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainCovRun.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainCovRun_FlowRun);
            // 
            // fcBtmMainCovStop
            // 
            this.fcBtmMainCovStop.BackColor = System.Drawing.Color.White;
            this.fcBtmMainCovStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainCovStop.CASE1 = null;
            this.fcBtmMainCovStop.CASE2 = null;
            this.fcBtmMainCovStop.CASE3 = null;
            this.fcBtmMainCovStop.Location = new System.Drawing.Point(388, 311);
            this.fcBtmMainCovStop.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainCovStop.Name = "fcBtmMainCovStop";
            this.fcBtmMainCovStop.NEXT = this.fcBtmMainWaitDownSmema;
            this.fcBtmMainCovStop.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainCovStop.TabIndex = 30;
            this.fcBtmMainCovStop.Text = "Bottom Conveyor Stop";
            this.fcBtmMainCovStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainCovStop.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainCovStop_FlowRun);
            // 
            // fcBtmMainWaitDownSmema
            // 
            this.fcBtmMainWaitDownSmema.BackColor = System.Drawing.Color.White;
            this.fcBtmMainWaitDownSmema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainWaitDownSmema.CASE1 = null;
            this.fcBtmMainWaitDownSmema.CASE2 = null;
            this.fcBtmMainWaitDownSmema.CASE3 = null;
            this.fcBtmMainWaitDownSmema.Location = new System.Drawing.Point(388, 388);
            this.fcBtmMainWaitDownSmema.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainWaitDownSmema.Name = "fcBtmMainWaitDownSmema";
            this.fcBtmMainWaitDownSmema.NEXT = this.fcBtmDelay;
            this.fcBtmMainWaitDownSmema.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainWaitDownSmema.TabIndex = 31;
            this.fcBtmMainWaitDownSmema.Text = "Wait SMEMA Upstream";
            this.fcBtmMainWaitDownSmema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainWaitDownSmema.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainWaitDownSmema_FlowRun);
            // 
            // fcBtmDelay
            // 
            this.fcBtmDelay.BackColor = System.Drawing.Color.White;
            this.fcBtmDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmDelay.CASE1 = this.fcBtmMainWaitDownSmema;
            this.fcBtmDelay.CASE2 = null;
            this.fcBtmDelay.CASE3 = null;
            this.fcBtmDelay.Location = new System.Drawing.Point(789, 391);
            this.fcBtmDelay.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmDelay.Name = "fcBtmDelay";
            this.fcBtmDelay.NEXT = this.fcBtmMainCovRun1;
            this.fcBtmDelay.Size = new System.Drawing.Size(209, 35);
            this.fcBtmDelay.TabIndex = 38;
            this.fcBtmDelay.Text = "Delay";
            this.fcBtmDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmDelay.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmDelay_FlowRun);
            // 
            // fcBtmMainNull
            // 
            this.fcBtmMainNull.BackColor = System.Drawing.Color.White;
            this.fcBtmMainNull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainNull.CASE1 = null;
            this.fcBtmMainNull.CASE2 = null;
            this.fcBtmMainNull.CASE3 = null;
            this.fcBtmMainNull.Location = new System.Drawing.Point(267, 161);
            this.fcBtmMainNull.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcBtmMainNull.Name = "fcBtmMainNull";
            this.fcBtmMainNull.NEXT = this.fcBtmMainWaitDownSmema;
            this.fcBtmMainNull.Size = new System.Drawing.Size(66, 43);
            this.fcBtmMainNull.TabIndex = 35;
            this.fcBtmMainNull.Text = "Null";
            this.fcBtmMainNull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fcBtmMainWaitSmema
            // 
            this.fcBtmMainWaitSmema.BackColor = System.Drawing.Color.White;
            this.fcBtmMainWaitSmema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainWaitSmema.CASE1 = null;
            this.fcBtmMainWaitSmema.CASE2 = null;
            this.fcBtmMainWaitSmema.CASE3 = null;
            this.fcBtmMainWaitSmema.Location = new System.Drawing.Point(388, 95);
            this.fcBtmMainWaitSmema.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcBtmMainWaitSmema.Name = "fcBtmMainWaitSmema";
            this.fcBtmMainWaitSmema.NEXT = this.fcBtmMainCovRun;
            this.fcBtmMainWaitSmema.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainWaitSmema.TabIndex = 29;
            this.fcBtmMainWaitSmema.Text = "Wait SMEMA Downstream";
            this.fcBtmMainWaitSmema.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainWaitSmema.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainWaitSmema_FlowRun);
            // 
            // fcBtmMainFlow
            // 
            this.fcBtmMainFlow.BackColor = System.Drawing.Color.White;
            this.fcBtmMainFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcBtmMainFlow.CASE1 = this.fcBtmMainNull;
            this.fcBtmMainFlow.CASE2 = null;
            this.fcBtmMainFlow.CASE3 = null;
            this.fcBtmMainFlow.Location = new System.Drawing.Point(388, 28);
            this.fcBtmMainFlow.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcBtmMainFlow.Name = "fcBtmMainFlow";
            this.fcBtmMainFlow.NEXT = this.fcBtmMainWaitSmema;
            this.fcBtmMainFlow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fcBtmMainFlow.Size = new System.Drawing.Size(373, 43);
            this.fcBtmMainFlow.TabIndex = 28;
            this.fcBtmMainFlow.Text = "Bottom Main Start";
            this.fcBtmMainFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcBtmMainFlow.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcBtmMainFlow_FlowRun);
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
            this.chkEnableSmema.Location = new System.Drawing.Point(20, 37);
            this.chkEnableSmema.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkEnableSmema.Name = "chkEnableSmema";
            this.chkEnableSmema.Size = new System.Drawing.Size(220, 33);
            this.chkEnableSmema.TabIndex = 10;
            this.chkEnableSmema.Text = "Enable SMEMA ";
            this.chkEnableSmema.UseVisualStyleBackColor = true;
            // 
            // chkBypassCvy
            // 
            this.chkBypassCvy.AutoSize = true;
            this.chkBypassCvy.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.SettingData, "PSet.BypassConveyor", true));
            this.chkBypassCvy.Location = new System.Drawing.Point(20, 75);
            this.chkBypassCvy.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.chkBypassCvy.Name = "chkBypassCvy";
            this.chkBypassCvy.Size = new System.Drawing.Size(233, 33);
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
            this.label10.Location = new System.Drawing.Point(448, 63);
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
            this.txtLoadTimeout.Location = new System.Drawing.Point(289, 59);
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
            this.label12.Location = new System.Drawing.Point(17, 63);
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
            this.label13.Location = new System.Drawing.Point(448, 114);
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
            this.txtUnloadTimeout.Location = new System.Drawing.Point(289, 108);
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
            this.label14.Location = new System.Drawing.Point(17, 112);
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
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(7, 2);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox14.Size = new System.Drawing.Size(1275, 143);
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
            this.groupBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox15.Location = new System.Drawing.Point(7, 150);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox15.Size = new System.Drawing.Size(1275, 229);
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
            this.fcInitBtmCheckSensor.Location = new System.Drawing.Point(440, 479);
            this.fcInitBtmCheckSensor.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcInitBtmCheckSensor.Name = "fcInitBtmCheckSensor";
            this.fcInitBtmCheckSensor.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBtmCheckSensor.Size = new System.Drawing.Size(311, 43);
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
            this.fcInitBtmRunConv.Location = new System.Drawing.Point(824, 500);
            this.fcInitBtmRunConv.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcInitBtmRunConv.Name = "fcInitBtmRunConv";
            this.fcInitBtmRunConv.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBtmRunConv.Size = new System.Drawing.Size(230, 43);
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
            this.fcInitBtmFlowEnd.Location = new System.Drawing.Point(440, 555);
            this.fcInitBtmFlowEnd.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcInitBtmFlowEnd.Name = "fcInitBtmFlowEnd";
            this.fcInitBtmFlowEnd.NEXT = null;
            this.fcInitBtmFlowEnd.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmFlowEnd.TabIndex = 52;
            this.fcInitBtmFlowEnd.Text = "Bottom Initialize End";
            this.fcInitBtmFlowEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmFlowEnd.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmFlowEnd_FlowRun);
            // 
            // fcInitBtmUnloadWaitStart
            // 
            this.fcInitBtmUnloadWaitStart.BackColor = System.Drawing.Color.White;
            this.fcInitBtmUnloadWaitStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmUnloadWaitStart.CASE1 = null;
            this.fcInitBtmUnloadWaitStart.CASE2 = null;
            this.fcInitBtmUnloadWaitStart.CASE3 = null;
            this.fcInitBtmUnloadWaitStart.Location = new System.Drawing.Point(1009, 226);
            this.fcInitBtmUnloadWaitStart.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmUnloadWaitStart.Name = "fcInitBtmUnloadWaitStart";
            this.fcInitBtmUnloadWaitStart.NEXT = this.fcInitBtmUnloadConvRun;
            this.fcInitBtmUnloadWaitStart.Size = new System.Drawing.Size(137, 35);
            this.fcInitBtmUnloadWaitStart.TabIndex = 62;
            this.fcInitBtmUnloadWaitStart.Text = "Wait Start Button";
            this.fcInitBtmUnloadWaitStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmUnloadWaitStart.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmUnloadWaitStart_FlowRun);
            // 
            // fcInitBtmUnloadConvRun
            // 
            this.fcInitBtmUnloadConvRun.BackColor = System.Drawing.Color.White;
            this.fcInitBtmUnloadConvRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmUnloadConvRun.CASE1 = null;
            this.fcInitBtmUnloadConvRun.CASE2 = null;
            this.fcInitBtmUnloadConvRun.CASE3 = null;
            this.fcInitBtmUnloadConvRun.Location = new System.Drawing.Point(652, 192);
            this.fcInitBtmUnloadConvRun.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmUnloadConvRun.Name = "fcInitBtmUnloadConvRun";
            this.fcInitBtmUnloadConvRun.NEXT = this.fcInitBtmUnloadWaitBoardOutOn;
            this.fcInitBtmUnloadConvRun.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmUnloadConvRun.TabIndex = 58;
            this.fcInitBtmUnloadConvRun.Text = "Bottom Conveyor Run";
            this.fcInitBtmUnloadConvRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmUnloadConvRun.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmUnloadConvRun_FlowRun);
            // 
            // fcInitBtmUnloadWaitBoardOutOn
            // 
            this.fcInitBtmUnloadWaitBoardOutOn.BackColor = System.Drawing.Color.White;
            this.fcInitBtmUnloadWaitBoardOutOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmUnloadWaitBoardOutOn.CASE1 = this.fcInitBtmUnloadWaitStart;
            this.fcInitBtmUnloadWaitBoardOutOn.CASE2 = null;
            this.fcInitBtmUnloadWaitBoardOutOn.CASE3 = null;
            this.fcInitBtmUnloadWaitBoardOutOn.Location = new System.Drawing.Point(652, 257);
            this.fcInitBtmUnloadWaitBoardOutOn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmUnloadWaitBoardOutOn.Name = "fcInitBtmUnloadWaitBoardOutOn";
            this.fcInitBtmUnloadWaitBoardOutOn.NEXT = this.fcinitBtmUnloadConvStop;
            this.fcInitBtmUnloadWaitBoardOutOn.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmUnloadWaitBoardOutOn.TabIndex = 60;
            this.fcInitBtmUnloadWaitBoardOutOn.Text = "Wait Board Out On";
            this.fcInitBtmUnloadWaitBoardOutOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmUnloadWaitBoardOutOn.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmUnloadWaitBoardOutOn_FlowRun);
            // 
            // fcinitBtmUnloadConvStop
            // 
            this.fcinitBtmUnloadConvStop.BackColor = System.Drawing.Color.White;
            this.fcinitBtmUnloadConvStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcinitBtmUnloadConvStop.CASE1 = null;
            this.fcinitBtmUnloadConvStop.CASE2 = null;
            this.fcinitBtmUnloadConvStop.CASE3 = null;
            this.fcinitBtmUnloadConvStop.Location = new System.Drawing.Point(652, 327);
            this.fcinitBtmUnloadConvStop.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcinitBtmUnloadConvStop.Name = "fcinitBtmUnloadConvStop";
            this.fcinitBtmUnloadConvStop.NEXT = this.fcInitBtmFlowEnd;
            this.fcinitBtmUnloadConvStop.Size = new System.Drawing.Size(311, 43);
            this.fcinitBtmUnloadConvStop.TabIndex = 59;
            this.fcinitBtmUnloadConvStop.Text = "Bottom Conveyor Stop";
            this.fcinitBtmUnloadConvStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcinitBtmUnloadConvStop.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcinitBtmUnloadConvStop_FlowRun);
            // 
            // fcInitBtmLoadWaitStart
            // 
            this.fcInitBtmLoadWaitStart.BackColor = System.Drawing.Color.White;
            this.fcInitBtmLoadWaitStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmLoadWaitStart.CASE1 = null;
            this.fcInitBtmLoadWaitStart.CASE2 = null;
            this.fcInitBtmLoadWaitStart.CASE3 = null;
            this.fcInitBtmLoadWaitStart.Location = new System.Drawing.Point(57, 300);
            this.fcInitBtmLoadWaitStart.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmLoadWaitStart.Name = "fcInitBtmLoadWaitStart";
            this.fcInitBtmLoadWaitStart.NEXT = this.fcInitBtmLoadConvRun;
            this.fcInitBtmLoadWaitStart.Size = new System.Drawing.Size(137, 35);
            this.fcInitBtmLoadWaitStart.TabIndex = 61;
            this.fcInitBtmLoadWaitStart.Text = "Wait Start Button";
            this.fcInitBtmLoadWaitStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmLoadWaitStart.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmLoadWaitStart_FlowRun);
            // 
            // fcInitBtmLoadConvRun
            // 
            this.fcInitBtmLoadConvRun.BackColor = System.Drawing.Color.White;
            this.fcInitBtmLoadConvRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmLoadConvRun.CASE1 = null;
            this.fcInitBtmLoadConvRun.CASE2 = null;
            this.fcInitBtmLoadConvRun.CASE3 = null;
            this.fcInitBtmLoadConvRun.Location = new System.Drawing.Point(228, 257);
            this.fcInitBtmLoadConvRun.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmLoadConvRun.Name = "fcInitBtmLoadConvRun";
            this.fcInitBtmLoadConvRun.NEXT = this.fcInitBtmLoadWaitBoardInOn;
            this.fcInitBtmLoadConvRun.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmLoadConvRun.TabIndex = 54;
            this.fcInitBtmLoadConvRun.Text = "Bottom Conveyor Run";
            this.fcInitBtmLoadConvRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmLoadConvRun.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmLoadConvRun_FlowRun);
            // 
            // fcInitBtmLoadWaitBoardInOn
            // 
            this.fcInitBtmLoadWaitBoardInOn.BackColor = System.Drawing.Color.White;
            this.fcInitBtmLoadWaitBoardInOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmLoadWaitBoardInOn.CASE1 = this.fcInitBtmLoadWaitStart;
            this.fcInitBtmLoadWaitBoardInOn.CASE2 = null;
            this.fcInitBtmLoadWaitBoardInOn.CASE3 = null;
            this.fcInitBtmLoadWaitBoardInOn.Location = new System.Drawing.Point(228, 327);
            this.fcInitBtmLoadWaitBoardInOn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmLoadWaitBoardInOn.Name = "fcInitBtmLoadWaitBoardInOn";
            this.fcInitBtmLoadWaitBoardInOn.NEXT = this.fcInitBtmLoadConvStop;
            this.fcInitBtmLoadWaitBoardInOn.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmLoadWaitBoardInOn.TabIndex = 57;
            this.fcInitBtmLoadWaitBoardInOn.Text = "Wait Board In On";
            this.fcInitBtmLoadWaitBoardInOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmLoadWaitBoardInOn.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmLoadWaitBoardInOn_FlowRun);
            // 
            // fcInitBtmLoadConvStop
            // 
            this.fcInitBtmLoadConvStop.BackColor = System.Drawing.Color.White;
            this.fcInitBtmLoadConvStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmLoadConvStop.CASE1 = null;
            this.fcInitBtmLoadConvStop.CASE2 = null;
            this.fcInitBtmLoadConvStop.CASE3 = null;
            this.fcInitBtmLoadConvStop.Location = new System.Drawing.Point(228, 401);
            this.fcInitBtmLoadConvStop.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmLoadConvStop.Name = "fcInitBtmLoadConvStop";
            this.fcInitBtmLoadConvStop.NEXT = this.fcInitBtmFlowEnd;
            this.fcInitBtmLoadConvStop.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmLoadConvStop.TabIndex = 56;
            this.fcInitBtmLoadConvStop.Text = "Bottom Conveyor Stop";
            this.fcInitBtmLoadConvStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmLoadConvStop.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmLoadConvStop_FlowRun);
            // 
            // fcInitBtmLoadWaitSMEMADownStream
            // 
            this.fcInitBtmLoadWaitSMEMADownStream.BackColor = System.Drawing.Color.White;
            this.fcInitBtmLoadWaitSMEMADownStream.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmLoadWaitSMEMADownStream.CASE1 = null;
            this.fcInitBtmLoadWaitSMEMADownStream.CASE2 = null;
            this.fcInitBtmLoadWaitSMEMADownStream.CASE3 = null;
            this.fcInitBtmLoadWaitSMEMADownStream.Location = new System.Drawing.Point(228, 192);
            this.fcInitBtmLoadWaitSMEMADownStream.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fcInitBtmLoadWaitSMEMADownStream.Name = "fcInitBtmLoadWaitSMEMADownStream";
            this.fcInitBtmLoadWaitSMEMADownStream.NEXT = this.fcInitBtmLoadConvRun;
            this.fcInitBtmLoadWaitSMEMADownStream.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmLoadWaitSMEMADownStream.TabIndex = 55;
            this.fcInitBtmLoadWaitSMEMADownStream.Text = "Wait SMEMA Downstream";
            this.fcInitBtmLoadWaitSMEMADownStream.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmLoadWaitSMEMADownStream.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmLoadWaitSMEMADownStream_FlowRun);
            // 
            // fcInitBtmCheckPart
            // 
            this.fcInitBtmCheckPart.BackColor = System.Drawing.Color.White;
            this.fcInitBtmCheckPart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmCheckPart.CASE1 = this.fcInitBtmLoadWaitSMEMADownStream;
            this.fcInitBtmCheckPart.CASE2 = this.fcInitBtmUnloadConvRun;
            this.fcInitBtmCheckPart.CASE3 = null;
            this.fcInitBtmCheckPart.Location = new System.Drawing.Point(440, 92);
            this.fcInitBtmCheckPart.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcInitBtmCheckPart.Name = "fcInitBtmCheckPart";
            this.fcInitBtmCheckPart.NEXT = this.fcInitBtmCheckSensor;
            this.fcInitBtmCheckPart.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmCheckPart.TabIndex = 53;
            this.fcInitBtmCheckPart.Text = "Check Part Present";
            this.fcInitBtmCheckPart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmCheckPart.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmCheckPart_FlowRun);
            // 
            // fcInitBtmFlow
            // 
            this.fcInitBtmFlow.BackColor = System.Drawing.Color.White;
            this.fcInitBtmFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fcInitBtmFlow.CASE1 = null;
            this.fcInitBtmFlow.CASE2 = null;
            this.fcInitBtmFlow.CASE3 = null;
            this.fcInitBtmFlow.Location = new System.Drawing.Point(440, 30);
            this.fcInitBtmFlow.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.fcInitBtmFlow.Name = "fcInitBtmFlow";
            this.fcInitBtmFlow.NEXT = this.fcInitBtmCheckPart;
            this.fcInitBtmFlow.Size = new System.Drawing.Size(311, 43);
            this.fcInitBtmFlow.TabIndex = 51;
            this.fcInitBtmFlow.Text = "Bottom Initialize Flow";
            this.fcInitBtmFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fcInitBtmFlow.FlowRun += new JabilSDK.UserControlLib.FlowChart.FlowRunEvent(this.fcInitBtmFlow_FlowRun);
            // 
            // ReturnConveyorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1084, 922);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ReturnConveyorForm";
            this.Text = "ReturnConveyorForm";
            this.plMaintenance.ResumeLayout(false);
            this.plProductionSetting.ResumeLayout(false);
            this.plFlowInitial.ResumeLayout(false);
            this.plFlowAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private JabilSDK.Controls.Input IB_SMEMA_BTM_DN_Available;
        private JabilSDK.Controls.Output OB_SMEMA_BTM_DW_MachineRdy;
        private System.Windows.Forms.Label label1;
        private JabilSDK.Controls.Output OB_SMEMA_BTM_UP_Available;
        private JabilSDK.Controls.Input IB_SMEMA_BTM_UP_MachineRdy;
        private JabilSDK.Controls.Input IB_BtmBoardIn;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Windows.Forms.CheckBox chkEnableSmema;
        private System.Windows.Forms.GroupBox groupBox8;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private JabilSDK.Controls.Output OB_BtmCvy_Forward;
        private JabilSDK.Controls.Input IB_BtmBoardOut;
        private JabilSDK.Controls.Input IB_BtmCvyError;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmCheckSensor;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmRunConv;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmFlowEnd;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmUnloadWaitStart;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmUnloadConvRun;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmUnloadWaitBoardOutOn;
        private JabilSDK.UserControlLib.FlowChart fcinitBtmUnloadConvStop;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmLoadWaitStart;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmLoadConvRun;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmLoadWaitBoardInOn;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmLoadConvStop;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmLoadWaitSMEMADownStream;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmCheckPart;
        private JabilSDK.UserControlLib.FlowChart fcInitBtmFlow;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel2;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainWaitBoardOutOn;
        private JabilSDK.UserControlLib.FlowChart fcBtmWaitStartButton2;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainCovRun1;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainCovStop1;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainEnd;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainWaitBoardInOn;
        private JabilSDK.UserControlLib.FlowChart fcBtmWaitStartButton1;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainCovRun;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainCovStop;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainWaitDownSmema;
        private JabilSDK.UserControlLib.FlowChart fcBtmDelay;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainNull;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainWaitSmema;
        private JabilSDK.UserControlLib.FlowChart fcBtmMainFlow;
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
    }
}