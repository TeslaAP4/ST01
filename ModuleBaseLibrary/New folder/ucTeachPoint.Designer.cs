namespace SASDK
{
    partial class ucTeachPoint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbTeachPoint = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGoto = new System.Windows.Forms.Button();
            this.btnMgoto = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.axisNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.encoderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipeData = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.tUiupdate = new System.Windows.Forms.Timer(this.components);
            this.gbTeachPoint.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTeachPoint
            // 
            this.gbTeachPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbTeachPoint.BackColor = System.Drawing.Color.White;
            this.gbTeachPoint.Controls.Add(this.panel1);
            this.gbTeachPoint.Controls.Add(this.dataGridView1);
            this.gbTeachPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.gbTeachPoint.Location = new System.Drawing.Point(0, 0);
            this.gbTeachPoint.Margin = new System.Windows.Forms.Padding(0);
            this.gbTeachPoint.Name = "gbTeachPoint";
            this.gbTeachPoint.Padding = new System.Windows.Forms.Padding(0);
            this.gbTeachPoint.Size = new System.Drawing.Size(247, 246);
            this.gbTeachPoint.TabIndex = 0;
            this.gbTeachPoint.TabStop = false;
            this.gbTeachPoint.Text = "TeachPointName";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGoto);
            this.panel1.Controls.Add(this.btnMgoto);
            this.panel1.Location = new System.Drawing.Point(10, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 40);
            this.panel1.TabIndex = 10;
            // 
            // btnGoto
            // 
            this.btnGoto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGoto.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGoto.Location = new System.Drawing.Point(0, 0);
            this.btnGoto.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(108, 40);
            this.btnGoto.TabIndex = 7;
            this.btnGoto.Text = "Goto";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // btnMgoto
            // 
            this.btnMgoto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMgoto.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMgoto.Location = new System.Drawing.Point(120, 0);
            this.btnMgoto.Margin = new System.Windows.Forms.Padding(0);
            this.btnMgoto.Name = "btnMgoto";
            this.btnMgoto.Size = new System.Drawing.Size(108, 40);
            this.btnMgoto.TabIndex = 9;
            this.btnMgoto.Text = "MGoto";
            this.btnMgoto.UseVisualStyleBackColor = true;
            this.btnMgoto.Click += new System.EventHandler(this.btnMgoto_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.axisNameDataGridViewTextBoxColumn,
            this.encoderDataGridViewTextBoxColumn});
            this.dataGridView1.DataMember = "tp_TeachPoint";
            this.dataGridView1.DataSource = this.RecipeData;
            this.dataGridView1.Location = new System.Drawing.Point(10, 35);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(228, 158);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // axisNameDataGridViewTextBoxColumn
            // 
            this.axisNameDataGridViewTextBoxColumn.DataPropertyName = "AxisName";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.axisNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.axisNameDataGridViewTextBoxColumn.HeaderText = "Axis";
            this.axisNameDataGridViewTextBoxColumn.Name = "axisNameDataGridViewTextBoxColumn";
            this.axisNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.axisNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.axisNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.axisNameDataGridViewTextBoxColumn.Width = 60;
            // 
            // encoderDataGridViewTextBoxColumn
            // 
            this.encoderDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.encoderDataGridViewTextBoxColumn.DataPropertyName = "Encoder";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.encoderDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.encoderDataGridViewTextBoxColumn.HeaderText = "Encoder";
            this.encoderDataGridViewTextBoxColumn.Name = "encoderDataGridViewTextBoxColumn";
            this.encoderDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.encoderDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RecipeData
            // 
            this.RecipeData.DataSetName = "Recipe";
            this.RecipeData.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.dataTable1.TableName = "tp_TeachPoint";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "AxisName";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Encoder";
            this.dataColumn2.DataType = typeof(double);
            // 
            // tUiupdate
            // 
            this.tUiupdate.Enabled = true;
            this.tUiupdate.Interval = 10;
            this.tUiupdate.Tick += new System.EventHandler(this.tUiupdate_Tick);
            // 
            // ucTeachPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.gbTeachPoint);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucTeachPoint";
            this.Size = new System.Drawing.Size(247, 246);
            this.Load += new System.EventHandler(this.ucTeachPoint_Load);
            this.gbTeachPoint.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTeachPoint;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Data.DataSet RecipeData;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.Button btnMgoto;
        private System.Windows.Forms.Timer tUiupdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn encoderDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel1;
    }
}
