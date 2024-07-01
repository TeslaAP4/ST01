namespace Acura3._0.MENUForms
{
    partial class MaintenanceForm
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
            this.tcMaintenance = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tcMaintenance.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMaintenance
            // 
            this.tcMaintenance.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcMaintenance.Controls.Add(this.tabPage1);
            this.tcMaintenance.Controls.Add(this.tabPage2);
            this.tcMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMaintenance.ItemSize = new System.Drawing.Size(250, 50);
            this.tcMaintenance.Location = new System.Drawing.Point(0, 0);
            this.tcMaintenance.Margin = new System.Windows.Forms.Padding(0);
            this.tcMaintenance.Name = "tcMaintenance";
            this.tcMaintenance.SelectedIndex = 0;
            this.tcMaintenance.Size = new System.Drawing.Size(722, 522);
            this.tcMaintenance.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcMaintenance.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 54);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(714, 464);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Module #1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 54);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(714, 464);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Module #2";
            // 
            // MaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(722, 522);
            this.Controls.Add(this.tcMaintenance);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MaintenanceForm";
            this.Text = "Maintenance";
            this.Load += new System.EventHandler(this.MaintenanceForm_Load);
            this.tcMaintenance.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMaintenance;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}