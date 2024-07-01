namespace Acura3._0.FunctionForms
{
    partial class AutoTeachSummary
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAutoTeach = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvPoint = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.W = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Goto = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAutoTeach
            // 
            this.btnAutoTeach.Location = new System.Drawing.Point(435, 586);
            this.btnAutoTeach.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAutoTeach.Name = "btnAutoTeach";
            this.btnAutoTeach.Size = new System.Drawing.Size(148, 61);
            this.btnAutoTeach.TabIndex = 3;
            this.btnAutoTeach.Text = "Auto Teach";
            this.btnAutoTeach.UseVisualStyleBackColor = true;
            this.btnAutoTeach.Click += new System.EventHandler(this.btnAutoTeach_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(602, 586);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(148, 61);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvPoint
            // 
            this.dgvPoint.AllowUserToAddRows = false;
            this.dgvPoint.AllowUserToDeleteRows = false;
            this.dgvPoint.AllowUserToResizeColumns = false;
            this.dgvPoint.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPoint.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.X,
            this.Y,
            this.Z,
            this.W,
            this.Goto});
            this.dgvPoint.Location = new System.Drawing.Point(18, 19);
            this.dgvPoint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvPoint.MultiSelect = false;
            this.dgvPoint.Name = "dgvPoint";
            this.dgvPoint.RowTemplate.Height = 35;
            this.dgvPoint.Size = new System.Drawing.Size(732, 557);
            this.dgvPoint.TabIndex = 5;
            this.dgvPoint.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPoint_CellContentClick);
            this.dgvPoint.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvPoint_CellValidating);
            // 
            // Index
            // 
            this.Index.FillWeight = 155.4279F;
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // X
            // 
            this.X.FillWeight = 37.69126F;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.X.Width = 120;
            // 
            // Y
            // 
            this.Y.FillWeight = 37.69126F;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Y.Width = 120;
            // 
            // Z
            // 
            this.Z.FillWeight = 37.69126F;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Z.Width = 120;
            // 
            // W
            // 
            this.W.FillWeight = 37.69126F;
            this.W.HeaderText = "W";
            this.W.Name = "W";
            this.W.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.W.Width = 120;
            // 
            // Goto
            // 
            this.Goto.HeaderText = "Go to";
            this.Goto.Name = "Goto";
            // 
            // AutoTeachSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 654);
            this.Controls.Add(this.dgvPoint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAutoTeach);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AutoTeachSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoTeachSummary";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoTeachSummary_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAutoTeach;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn W;
        private System.Windows.Forms.DataGridViewButtonColumn Goto;
    }
}