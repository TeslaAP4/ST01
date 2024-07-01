namespace AcuraLibrary.Forms
{
    partial class ModuleBaseForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMaintenance = new System.Windows.Forms.TabPage();
            this.plMaintenance = new System.Windows.Forms.Panel();
            this.tpProductionSetting = new System.Windows.Forms.TabPage();
            this.plProductionSetting = new System.Windows.Forms.Panel();
            this.tpRecipeEditor = new System.Windows.Forms.TabPage();
            this.plRecipeEditor = new System.Windows.Forms.Panel();
            this.tpFlowInitial = new System.Windows.Forms.TabPage();
            this.plFlowInitial = new System.Windows.Forms.Panel();
            this.tpFlowAuto = new System.Windows.Forms.TabPage();
            this.plFlowAuto = new System.Windows.Forms.Panel();
            this.tpMachineStatus = new System.Windows.Forms.TabPage();
            this.plMachineStatus = new System.Windows.Forms.Panel();
            this.tpMotionSetup = new System.Windows.Forms.TabPage();
            this.plMotionSetup = new System.Windows.Forms.Panel();
            this.tpMotorControl = new System.Windows.Forms.TabPage();
            this.plMotorControl = new System.Windows.Forms.Panel();
            this.SettingData = new System.Data.DataSet();
            this.MSet = new System.Data.DataTable();
            this.PSet = new System.Data.DataTable();
            this.RecipeData = new System.Data.DataSet();
            this.RSet = new System.Data.DataTable();
            this.tabControl1.SuspendLayout();
            this.tpMaintenance.SuspendLayout();
            this.tpProductionSetting.SuspendLayout();
            this.tpRecipeEditor.SuspendLayout();
            this.tpFlowInitial.SuspendLayout();
            this.tpFlowAuto.SuspendLayout();
            this.tpMachineStatus.SuspendLayout();
            this.tpMotionSetup.SuspendLayout();
            this.tpMotorControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpMaintenance);
            this.tabControl1.Controls.Add(this.tpProductionSetting);
            this.tabControl1.Controls.Add(this.tpRecipeEditor);
            this.tabControl1.Controls.Add(this.tpFlowInitial);
            this.tabControl1.Controls.Add(this.tpFlowAuto);
            this.tabControl1.Controls.Add(this.tpMachineStatus);
            this.tabControl1.Controls.Add(this.tpMotionSetup);
            this.tabControl1.Controls.Add(this.tpMotorControl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(118, 50);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(813, 749);
            this.tabControl1.TabIndex = 1;
            // 
            // tpMaintenance
            // 
            this.tpMaintenance.BackColor = System.Drawing.Color.Black;
            this.tpMaintenance.Controls.Add(this.plMaintenance);
            this.tpMaintenance.Location = new System.Drawing.Point(4, 54);
            this.tpMaintenance.Name = "tpMaintenance";
            this.tpMaintenance.Padding = new System.Windows.Forms.Padding(3);
            this.tpMaintenance.Size = new System.Drawing.Size(805, 691);
            this.tpMaintenance.TabIndex = 0;
            this.tpMaintenance.Text = "Maintenance";
            // 
            // plMaintenance
            // 
            this.plMaintenance.AutoScroll = true;
            this.plMaintenance.BackColor = System.Drawing.Color.White;
            this.plMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMaintenance.Location = new System.Drawing.Point(3, 3);
            this.plMaintenance.Name = "plMaintenance";
            this.plMaintenance.Size = new System.Drawing.Size(799, 685);
            this.plMaintenance.TabIndex = 1;
            // 
            // tpProductionSetting
            // 
            this.tpProductionSetting.BackColor = System.Drawing.Color.Black;
            this.tpProductionSetting.Controls.Add(this.plProductionSetting);
            this.tpProductionSetting.Location = new System.Drawing.Point(4, 54);
            this.tpProductionSetting.Name = "tpProductionSetting";
            this.tpProductionSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpProductionSetting.Size = new System.Drawing.Size(805, 798);
            this.tpProductionSetting.TabIndex = 1;
            this.tpProductionSetting.Text = "Production Setting";
            // 
            // plProductionSetting
            // 
            this.plProductionSetting.AutoScroll = true;
            this.plProductionSetting.BackColor = System.Drawing.Color.White;
            this.plProductionSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plProductionSetting.Location = new System.Drawing.Point(3, 3);
            this.plProductionSetting.Name = "plProductionSetting";
            this.plProductionSetting.Size = new System.Drawing.Size(799, 792);
            this.plProductionSetting.TabIndex = 2;
            // 
            // tpRecipeEditor
            // 
            this.tpRecipeEditor.BackColor = System.Drawing.Color.Black;
            this.tpRecipeEditor.Controls.Add(this.plRecipeEditor);
            this.tpRecipeEditor.Location = new System.Drawing.Point(4, 54);
            this.tpRecipeEditor.Name = "tpRecipeEditor";
            this.tpRecipeEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tpRecipeEditor.Size = new System.Drawing.Size(805, 798);
            this.tpRecipeEditor.TabIndex = 2;
            this.tpRecipeEditor.Text = "Recipe Editor";
            // 
            // plRecipeEditor
            // 
            this.plRecipeEditor.AutoScroll = true;
            this.plRecipeEditor.AutoSize = true;
            this.plRecipeEditor.BackColor = System.Drawing.Color.White;
            this.plRecipeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRecipeEditor.Location = new System.Drawing.Point(3, 3);
            this.plRecipeEditor.Name = "plRecipeEditor";
            this.plRecipeEditor.Size = new System.Drawing.Size(799, 792);
            this.plRecipeEditor.TabIndex = 2;
            // 
            // tpFlowInitial
            // 
            this.tpFlowInitial.BackColor = System.Drawing.Color.Black;
            this.tpFlowInitial.Controls.Add(this.plFlowInitial);
            this.tpFlowInitial.Location = new System.Drawing.Point(4, 54);
            this.tpFlowInitial.Name = "tpFlowInitial";
            this.tpFlowInitial.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlowInitial.Size = new System.Drawing.Size(805, 798);
            this.tpFlowInitial.TabIndex = 3;
            this.tpFlowInitial.Text = "Flow Initial";
            // 
            // plFlowInitial
            // 
            this.plFlowInitial.AutoScroll = true;
            this.plFlowInitial.AutoSize = true;
            this.plFlowInitial.BackColor = System.Drawing.Color.White;
            this.plFlowInitial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFlowInitial.Location = new System.Drawing.Point(3, 3);
            this.plFlowInitial.Name = "plFlowInitial";
            this.plFlowInitial.Size = new System.Drawing.Size(799, 792);
            this.plFlowInitial.TabIndex = 3;
            // 
            // tpFlowAuto
            // 
            this.tpFlowAuto.BackColor = System.Drawing.Color.Black;
            this.tpFlowAuto.Controls.Add(this.plFlowAuto);
            this.tpFlowAuto.Location = new System.Drawing.Point(4, 54);
            this.tpFlowAuto.Name = "tpFlowAuto";
            this.tpFlowAuto.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlowAuto.Size = new System.Drawing.Size(805, 798);
            this.tpFlowAuto.TabIndex = 4;
            this.tpFlowAuto.Text = "Flow Auto";
            // 
            // plFlowAuto
            // 
            this.plFlowAuto.AutoScroll = true;
            this.plFlowAuto.BackColor = System.Drawing.Color.White;
            this.plFlowAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFlowAuto.Location = new System.Drawing.Point(3, 3);
            this.plFlowAuto.Name = "plFlowAuto";
            this.plFlowAuto.Size = new System.Drawing.Size(799, 792);
            this.plFlowAuto.TabIndex = 4;
            // 
            // tpMachineStatus
            // 
            this.tpMachineStatus.BackColor = System.Drawing.Color.Black;
            this.tpMachineStatus.Controls.Add(this.plMachineStatus);
            this.tpMachineStatus.Location = new System.Drawing.Point(4, 54);
            this.tpMachineStatus.Name = "tpMachineStatus";
            this.tpMachineStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tpMachineStatus.Size = new System.Drawing.Size(805, 798);
            this.tpMachineStatus.TabIndex = 5;
            this.tpMachineStatus.Text = "Machine Status";
            // 
            // plMachineStatus
            // 
            this.plMachineStatus.AutoScroll = true;
            this.plMachineStatus.BackColor = System.Drawing.Color.White;
            this.plMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMachineStatus.Location = new System.Drawing.Point(3, 3);
            this.plMachineStatus.Name = "plMachineStatus";
            this.plMachineStatus.Size = new System.Drawing.Size(799, 792);
            this.plMachineStatus.TabIndex = 5;
            // 
            // tpMotionSetup
            // 
            this.tpMotionSetup.BackColor = System.Drawing.Color.Black;
            this.tpMotionSetup.Controls.Add(this.plMotionSetup);
            this.tpMotionSetup.Location = new System.Drawing.Point(4, 54);
            this.tpMotionSetup.Name = "tpMotionSetup";
            this.tpMotionSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tpMotionSetup.Size = new System.Drawing.Size(805, 798);
            this.tpMotionSetup.TabIndex = 6;
            this.tpMotionSetup.Text = "Motion Setup";
            // 
            // plMotionSetup
            // 
            this.plMotionSetup.AutoScroll = true;
            this.plMotionSetup.BackColor = System.Drawing.Color.White;
            this.plMotionSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMotionSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plMotionSetup.Location = new System.Drawing.Point(3, 3);
            this.plMotionSetup.Name = "plMotionSetup";
            this.plMotionSetup.Size = new System.Drawing.Size(799, 792);
            this.plMotionSetup.TabIndex = 6;
            // 
            // tpMotorControl
            // 
            this.tpMotorControl.BackColor = System.Drawing.Color.Black;
            this.tpMotorControl.Controls.Add(this.plMotorControl);
            this.tpMotorControl.Location = new System.Drawing.Point(4, 54);
            this.tpMotorControl.Name = "tpMotorControl";
            this.tpMotorControl.Padding = new System.Windows.Forms.Padding(3);
            this.tpMotorControl.Size = new System.Drawing.Size(805, 798);
            this.tpMotorControl.TabIndex = 7;
            this.tpMotorControl.Text = "Motor Control";
            // 
            // plMotorControl
            // 
            this.plMotorControl.AutoScroll = true;
            this.plMotorControl.BackColor = System.Drawing.Color.White;
            this.plMotorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMotorControl.Location = new System.Drawing.Point(3, 3);
            this.plMotorControl.Name = "plMotorControl";
            this.plMotorControl.Size = new System.Drawing.Size(799, 792);
            this.plMotorControl.TabIndex = 7;
            // 
            // SettingData
            // 
            this.SettingData.DataSetName = "Setting";
            this.SettingData.Tables.AddRange(new System.Data.DataTable[] {
            this.MSet,
            this.PSet});
            // 
            // MSet
            // 
            this.MSet.TableName = "MSet";
            // 
            // PSet
            // 
            this.PSet.TableName = "PSet";
            // 
            // RecipeData
            // 
            this.RecipeData.DataSetName = "Recipe";
            this.RecipeData.Tables.AddRange(new System.Data.DataTable[] {
            this.RSet});
            // 
            // RSet
            // 
            this.RSet.TableName = "RSet";
            // 
            // ModuleBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 749);
            this.Controls.Add(this.tabControl1);
            this.Name = "ModuleBaseForm";
            this.Text = "ModuleBaseForm";
            this.tabControl1.ResumeLayout(false);
            this.tpMaintenance.ResumeLayout(false);
            this.tpProductionSetting.ResumeLayout(false);
            this.tpRecipeEditor.ResumeLayout(false);
            this.tpRecipeEditor.PerformLayout();
            this.tpFlowInitial.ResumeLayout(false);
            this.tpFlowInitial.PerformLayout();
            this.tpFlowAuto.ResumeLayout(false);
            this.tpMachineStatus.ResumeLayout(false);
            this.tpMotionSetup.ResumeLayout(false);
            this.tpMotorControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMaintenance;
        public System.Windows.Forms.Panel plMaintenance;
        private System.Windows.Forms.TabPage tpProductionSetting;
        public System.Windows.Forms.Panel plProductionSetting;
        private System.Windows.Forms.TabPage tpRecipeEditor;
        public System.Windows.Forms.Panel plRecipeEditor;
        private System.Windows.Forms.TabPage tpFlowInitial;
        public System.Windows.Forms.Panel plFlowInitial;
        private System.Windows.Forms.TabPage tpFlowAuto;
        public System.Windows.Forms.Panel plFlowAuto;
        private System.Windows.Forms.TabPage tpMachineStatus;
        public System.Windows.Forms.Panel plMachineStatus;
        private System.Windows.Forms.TabPage tpMotionSetup;
        public System.Windows.Forms.Panel plMotionSetup;
        private System.Windows.Forms.TabPage tpMotorControl;
        public System.Windows.Forms.Panel plMotorControl;
        public System.Data.DataSet SettingData;
        public System.Data.DataTable MSet;
        public System.Data.DataTable PSet;
        public System.Data.DataSet RecipeData;
        public System.Data.DataTable RSet;
    }
}