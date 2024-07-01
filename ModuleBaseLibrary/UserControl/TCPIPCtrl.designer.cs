namespace AcuraLibrary.Forms
{
    partial class TCPIPCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbController = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gBOutput = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LblStatus = new System.Windows.Forms.RichTextBox();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkBxEnable = new System.Windows.Forms.CheckBox();
            this.gBMain = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.chkBxDebugOnly = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbProtocol_2 = new System.Windows.Forms.RadioButton();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.rbProtocol_1 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbRetry = new System.Windows.Forms.ComboBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SettingData = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tmLoad = new System.Windows.Forms.Timer(this.components);
            this.gbController.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gBOutput.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gBMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbController
            // 
            this.gbController.BackColor = System.Drawing.Color.White;
            this.gbController.Controls.Add(this.tableLayoutPanel1);
            this.gbController.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbController.Location = new System.Drawing.Point(0, 0);
            this.gbController.Margin = new System.Windows.Forms.Padding(0);
            this.gbController.Name = "gbController";
            this.gbController.Padding = new System.Windows.Forms.Padding(0);
            this.gbController.Size = new System.Drawing.Size(809, 371);
            this.gbController.TabIndex = 0;
            this.gbController.TabStop = false;
            this.gbController.Text = "ControllerName";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Controls.Add(this.gBOutput, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 349);
            this.tableLayoutPanel1.TabIndex = 136;
            // 
            // gBOutput
            // 
            this.gBOutput.BackColor = System.Drawing.Color.DarkGray;
            this.gBOutput.Controls.Add(this.label3);
            this.gBOutput.Controls.Add(this.txtMsg);
            this.gBOutput.Controls.Add(this.groupBox3);
            this.gBOutput.Controls.Add(this.btnTrigger);
            this.gBOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBOutput.Location = new System.Drawing.Point(405, 3);
            this.gBOutput.Name = "gBOutput";
            this.gBOutput.Size = new System.Drawing.Size(396, 338);
            this.gBOutput.TabIndex = 125;
            this.gBOutput.TabStop = false;
            this.gBOutput.Text = "Disconnected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 23);
            this.label3.TabIndex = 127;
            this.label3.Text = "Input";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(109, 25);
            this.txtMsg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(279, 29);
            this.txtMsg.TabIndex = 126;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LblStatus);
            this.groupBox3.Location = new System.Drawing.Point(7, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(384, 227);
            this.groupBox3.TabIndex = 125;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Receiver";
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.Color.DarkGray;
            this.LblStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblStatus.Location = new System.Drawing.Point(3, 25);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.ReadOnly = true;
            this.LblStatus.Size = new System.Drawing.Size(378, 199);
            this.LblStatus.TabIndex = 128;
            this.LblStatus.Text = "Status";
            this.LblStatus.TextChanged += new System.EventHandler(this.LblStatus_TextChanged);
            // 
            // btnTrigger
            // 
            this.btnTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrigger.Location = new System.Drawing.Point(7, 67);
            this.btnTrigger.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(135, 35);
            this.btnTrigger.TabIndex = 115;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = false;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gBMain, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(396, 338);
            this.tableLayoutPanel2.TabIndex = 126;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkBxEnable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 24);
            this.panel1.TabIndex = 125;
            // 
            // chkBxEnable
            // 
            this.chkBxEnable.AutoSize = true;
            this.chkBxEnable.Checked = true;
            this.chkBxEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxEnable.Location = new System.Drawing.Point(10, 0);
            this.chkBxEnable.Name = "chkBxEnable";
            this.chkBxEnable.Size = new System.Drawing.Size(81, 27);
            this.chkBxEnable.TabIndex = 135;
            this.chkBxEnable.Text = "Enable";
            this.chkBxEnable.UseVisualStyleBackColor = true;
            this.chkBxEnable.CheckedChanged += new System.EventHandler(this.chkBxEnable_CheckedChanged);
            // 
            // gBMain
            // 
            this.gBMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gBMain.Controls.Add(this.label15);
            this.gBMain.Controls.Add(this.chkBxDebugOnly);
            this.gBMain.Controls.Add(this.label5);
            this.gBMain.Controls.Add(this.rbProtocol_2);
            this.gBMain.Controls.Add(this.txtPort);
            this.gBMain.Controls.Add(this.rbProtocol_1);
            this.gBMain.Controls.Add(this.label6);
            this.gBMain.Controls.Add(this.label2);
            this.gBMain.Controls.Add(this.btnConnect);
            this.gBMain.Controls.Add(this.txtIP);
            this.gBMain.Controls.Add(this.label11);
            this.gBMain.Controls.Add(this.cmbRetry);
            this.gBMain.Controls.Add(this.txtTimeout);
            this.gBMain.Controls.Add(this.label9);
            this.gBMain.Controls.Add(this.label1);
            this.gBMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBMain.Location = new System.Drawing.Point(3, 33);
            this.gBMain.Name = "gBMain";
            this.gBMain.Size = new System.Drawing.Size(390, 302);
            this.gBMain.TabIndex = 126;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 154);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 23);
            this.label15.TabIndex = 119;
            this.label15.Text = "Retry";
            // 
            // chkBxDebugOnly
            // 
            this.chkBxDebugOnly.AutoSize = true;
            this.chkBxDebugOnly.Location = new System.Drawing.Point(180, 260);
            this.chkBxDebugOnly.Name = "chkBxDebugOnly";
            this.chkBxDebugOnly.Size = new System.Drawing.Size(158, 27);
            this.chkBxDebugOnly.TabIndex = 134;
            this.chkBxDebugOnly.Text = "Allow Debug Log";
            this.chkBxDebugOnly.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 23);
            this.label5.TabIndex = 110;
            this.label5.Text = "IP Address";
            // 
            // rbProtocol_2
            // 
            this.rbProtocol_2.AutoSize = true;
            this.rbProtocol_2.Location = new System.Drawing.Point(263, 206);
            this.rbProtocol_2.Name = "rbProtocol_2";
            this.rbProtocol_2.Size = new System.Drawing.Size(97, 27);
            this.rbProtocol_2.TabIndex = 133;
            this.rbProtocol_2.Text = "Two Way";
            this.toolTip1.SetToolTip(this.rbProtocol_2, "Send input and receive output to/from host");
            this.rbProtocol_2.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(144, 54);
            this.txtPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(163, 29);
            this.txtPort.TabIndex = 117;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rbProtocol_1
            // 
            this.rbProtocol_1.AutoSize = true;
            this.rbProtocol_1.Checked = true;
            this.rbProtocol_1.Location = new System.Drawing.Point(144, 206);
            this.rbProtocol_1.Name = "rbProtocol_1";
            this.rbProtocol_1.Size = new System.Drawing.Size(96, 27);
            this.rbProtocol_1.TabIndex = 132;
            this.rbProtocol_1.TabStop = true;
            this.rbProtocol_1.Text = "One Way";
            this.toolTip1.SetToolTip(this.rbProtocol_1, "Send input to host only");
            this.rbProtocol_1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 23);
            this.label6.TabIndex = 116;
            this.label6.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 46);
            this.label2.TabIndex = 131;
            this.label2.Text = "Communication\r\nProtocol";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(9, 255);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(135, 35);
            this.btnConnect.TabIndex = 114;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(144, 6);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(233, 29);
            this.txtIP.TabIndex = 109;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 23);
            this.label11.TabIndex = 112;
            this.label11.Text = "Timeout";
            // 
            // cmbRetry
            // 
            this.cmbRetry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(230)))));
            this.cmbRetry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRetry.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbRetry.FormattingEnabled = true;
            this.cmbRetry.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbRetry.Location = new System.Drawing.Point(144, 151);
            this.cmbRetry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbRetry.Name = "cmbRetry";
            this.cmbRetry.Size = new System.Drawing.Size(163, 31);
            this.cmbRetry.TabIndex = 120;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(144, 102);
            this.txtTimeout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(163, 29);
            this.txtTimeout.TabIndex = 111;
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 23);
            this.label9.TabIndex = 113;
            this.label9.Text = "ms";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 23);
            this.label1.TabIndex = 121;
            this.label1.Text = "times";
            // 
            // SettingData
            // 
            this.SettingData.DataSetName = "Setting";
            this.SettingData.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn1,
            this.dataColumn2});
            this.dataTable1.TableName = "TCPIP_Ctrl_MSet";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "IPAddress";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Port";
            this.dataColumn4.DataType = typeof(int);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Timeout";
            this.dataColumn5.DataType = typeof(int);
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "Retry";
            this.dataColumn6.DataType = typeof(int);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "CommProtocol";
            this.dataColumn7.DataType = typeof(bool);
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "DebugLogAllow";
            this.dataColumn1.DataType = typeof(bool);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Enable";
            this.dataColumn2.DataType = typeof(bool);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "AcuRA 3.0 Info";
            // 
            // tmLoad
            // 
            this.tmLoad.Enabled = true;
            this.tmLoad.Interval = 500;
            this.tmLoad.Tick += new System.EventHandler(this.tmLoad_Tick);
            // 
            // TCPIPCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.gbController);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TCPIPCtrl";
            this.Size = new System.Drawing.Size(809, 371);
            this.gbController.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gBOutput.ResumeLayout(false);
            this.gBOutput.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gBMain.ResumeLayout(false);
            this.gBMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbController;
        private System.Data.DataSet SettingData;
        private System.Data.DataTable dataTable1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRetry;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gBOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rbProtocol_2;
        private System.Windows.Forms.RadioButton rbProtocol_1;
        private System.Windows.Forms.Label label2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Windows.Forms.Timer tmLoad;
        private System.Data.DataColumn dataColumn1;
        private System.Windows.Forms.CheckBox chkBxDebugOnly;
        private System.Windows.Forms.CheckBox chkBxEnable;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.RichTextBox LblStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel gBMain;
    }
}
