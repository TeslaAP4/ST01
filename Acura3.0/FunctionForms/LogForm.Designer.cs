namespace Acura3._0.FunctionForms
{
    partial class LogForm
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
            this.tabLogPage = new System.Windows.Forms.TabControl();
            this.tabLogForm = new System.Windows.Forms.TabPage();
            this.tabLogPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabLogPage
            // 
            this.tabLogPage.Controls.Add(this.tabLogForm);
            this.tabLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogPage.Location = new System.Drawing.Point(0, 0);
            this.tabLogPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabLogPage.Name = "tabLogPage";
            this.tabLogPage.SelectedIndex = 0;
            this.tabLogPage.Size = new System.Drawing.Size(800, 450);
            this.tabLogPage.TabIndex = 1;
            // 
            // tabLogForm
            // 
            this.tabLogForm.Location = new System.Drawing.Point(4, 22);
            this.tabLogForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabLogForm.Name = "tabLogForm";
            this.tabLogForm.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabLogForm.Size = new System.Drawing.Size(792, 424);
            this.tabLogForm.TabIndex = 0;
            this.tabLogForm.Text = "Log Form";
            this.tabLogForm.UseVisualStyleBackColor = true;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabLogPage);
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogForm";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.tabLogPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabLogPage;
        private System.Windows.Forms.TabPage tabLogForm;
    }
}
