using JabilSDK.Controls;
using SASDK.RUserControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SASDK
{
    public partial class ucTeachPoint : UserControl
    {
        public Dictionary<AxisName, Motor> MotorName = new Dictionary<AxisName, Motor>();
        private Task ManualTask;
        public CancellationTokenSource StopManualTask = new CancellationTokenSource();
        private MotorSpeedData[] SpeedData = new MotorSpeedData[10];
        private BackgroundWorker blinker;
        private ToolTip btnToolTip = new ToolTip();
        private string toolTipText = "";

        public enum AxisName
        {
            none,
            X,
            Y,
            Z,
            W1,
            W2,
            W3,
            W4

        }
        private bool _SafeToMove = true;
        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("Safety Interlock")]
        public bool SafeToMove
        {
            get
            {
                return _SafeToMove;
            }
            set
            {
                _SafeToMove = value;
            }
        }

        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("Enable Z Safety check whether XY is on Position")]
        public bool ZSafeCheck
        {
            get;
            set;
        }


        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("Prompt Error when XY not in Position.Action will be ignore")]
        public bool PromptZSafeMessage
        {
            get;
            set;
        }

        private bool _MultiGO_Button = true;
        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("Enable MultiGO button")]
        public bool MultiGO_Button
        {
            get
            {
                return _MultiGO_Button;
            }
            set
            {
                _MultiGO_Button = value;
                btnMgoto.Visible = value;

                if (value)
                {
                    btnGoto.Dock = DockStyle.Left;
                    btnMgoto.Dock = DockStyle.Right;
                }
                else
                {
                    btnGoto.Dock = DockStyle.Fill;

                }
            }
        }

        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("Position of Z Safe.When Moving XY,will check Z is on safe zone")]
        public ucTeachPoint SafeZone
        {
            get;
            set;
        }



        [Browsable(true)]
        [Category("#JabilSDK")]
        [Description("")]
        public string TeachPointName
        {
            get
            {
                return gbTeachPoint.Text;
            }
            set
            {
                gbTeachPoint.Text = value;
            }
        }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR1 { get; set; }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR2 { get; set; }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR3 { get; set; }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR4 { get; set; }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR5 { get; set; }

        [Browsable(true)]
        [Category("#JabilSDKMotor")]
        [Description("")]
        public Motor MOTOR6 { get; set; }

        private AxisName _Motor1_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor1_Name
        {
            get
            {
                return _Motor1_Name;
            }
            set
            {
                Delete(_Motor1_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor1_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR1);
                    }
                }



            }
        }

        private AxisName _Motor2_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor2_Name
        {
            get
            {
                return _Motor2_Name;
            }
            set
            {
                Delete(_Motor2_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor2_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR2);
                    }
                }
            }
        }

        private AxisName _Motor3_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor3_Name
        {
            get
            {
                return _Motor3_Name;
            }
            set
            {
                Delete(_Motor3_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor3_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR3);
                    }
                }
            }
        }

        private AxisName _Motor4_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor4_Name
        {
            get
            {
                return _Motor4_Name;
            }
            set
            {
                Delete(_Motor4_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor4_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR4);
                    }
                }


            }
        }

        private AxisName _Motor5_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor5_Name
        {
            get
            {
                return _Motor5_Name;
            }
            set
            {
                Delete(_Motor5_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor5_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR5);
                    }
                }
            }
        }

        private AxisName _Motor6_Name;
        [Browsable(true)]
        [Category("#JabilSDKMotorName")]
        [Description("")]
        public AxisName Motor6_Name
        {
            get
            {
                return _Motor6_Name;
            }
            set
            {
                Delete(_Motor6_Name.ToString());
                if (MotorName.ContainsKey(value))
                {
                    MotorName.Remove(value);
                }
                _Motor6_Name = value;
                if (value != AxisName.none)
                {
                    AddNew(value.ToString());
                    if (!MotorName.ContainsKey(value))
                    {
                        MotorName.Add(value, MOTOR6);
                    }
                }


            }
        }


        public ucTeachPoint()
        {
            InitializeComponent();

            for (int x = 0; x < SpeedData.Length; x++)
            {
                SpeedData[x] = new MotorSpeedData();
            }
            blinker = new BackgroundWorker();
            blinker.WorkerSupportsCancellation = true;
            blinker.DoWork += blinker_DoWork;
            btnToolTip.ShowAlways = true;
            btnToolTip.ToolTipTitle = "Current Encoder";
            SetDoubleBuffer(this);
            SetDoubleBuffer(dataGridView1);
            dataGridView1.ClearSelection();
        }

        //public void saveRecipe(string RecipePath)
        //{
        //    FileInfo fiTmp1 = new FileInfo(RecipePath);
        //    if (fiTmp1.Directory.Exists == false)
        //        fiTmp1.Directory.Create();
        //    RecipeData.AcceptChanges();

        //    DataSet ds = new DataSet(this.Name);
        //    ds = RecipeData;
        //    ds.WriteXml(RecipePath);

        //}

        public void LoadRecipe(string RecipePath, string ModuleName)
        {
            if (File.Exists(RecipePath))
            {
                DataSet ds = new DataSet(this.Name);
                DataTable newdt = new DataTable();

                // ds = RecipeData;

                ds.ReadXml(RecipePath);

                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == this.Name + "_" + ModuleName)
                    {
                        //RecipeData.Clear();
                        dataGridView1.Update();
                        for (int i = 0; i < RecipeData.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (RecipeData.Tables[0].Rows[i][0].ToString() == dt.Rows[j][0].ToString())
                                {
                                    RecipeData.Tables[0].Rows[i][1] = Math.Round(Convert.ToDouble(dt.Rows[j][1]), 3);
                                    break;
                                }
                                else
                                {
                                    RecipeData.Tables[0].Rows[i][1] = 0.00;
                                }
                            }
                        }



                        //  RecipeData.Tables.Remove("tp_TeachPoint");
                        //  newdt = dt.Copy();
                        //  newdt.TableName = "tp_TeachPoint";
                        //  RecipeData.Tables.Add(newdt);
                        RecipeData.AcceptChanges();
                        dataGridView1.DataSource = RecipeData;
                        dataGridView1.Refresh();
                        dataGridView1.ClearSelection();
                        break;
                    }
                }
            }
            else
            {
                FileInfo fiTmp1 = new FileInfo(RecipePath);
                if (fiTmp1.Directory.Exists == false)
                    fiTmp1.Directory.Create();
            }
            RecipeData.AcceptChanges();
        }

        public DataSet GetRecipe(string ModuleName)
        {
            RecipeData.Namespace = this.Name + "_" + ModuleName;
            RecipeData.Tables[0].TableName = this.Name + "_" + ModuleName;
            return RecipeData;
        }

        public DataSet SetRecipeTableName()
        {
            RecipeData.Namespace = this.Name;
            RecipeData.Tables[0].TableName = "tp_TeachPoint";
            return RecipeData;
        }

        //public double GetValue(AxisName Axis)
        //{
        //    double value = 0.0;
        //    DataGridView dgv = dataGridView1;
        //    DataTable dt = RecipeData.Tables["tp_TeachPoint"];

        //    int rowIndex = Find(Axis.ToString());
        //    if (rowIndex == -1)
        //    {
        //        return 0.0;
        //    }
        //    DataRow dr = dt.Rows[rowIndex];
        //    value = Convert.ToDouble(dr["Encoder"]);
        //    return value;
        //}

        public double GetValue(AxisName Axis)
        {
            double value = 0.0;
            DataTable dt = RecipeData.Tables["tp_TeachPoint"];
            DataRow dr = dt.Rows.Cast<DataRow>().Where(s => s.Field<string>("AxisName") == Axis.ToString()).FirstOrDefault();

            if (dr != null)
            {
                value = Convert.ToDouble(dr["Encoder"]);
            }

            return value;
        }

        public void SetSafeInterlock(bool interlock)
        {
            SafeToMove = interlock;
        }

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        private bool GetRange(Motor element, double pos, double tolerance = 0.1)
        {
            return element.GetEncoderPosition() >= pos - tolerance && element.GetEncoderPosition() <= pos + tolerance;
        }

        private void blinker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (blinker.CancellationPending == true)
                {
                    gbTeachPoint.BackColor = Color.White;
                    e.Cancel = true;
                    return;
                }
                if (gbTeachPoint.InvokeRequired)
                {
                    gbTeachPoint.Invoke((Action)blink);
                }
                else
                {
                    blink();
                }
                Thread.Sleep(500); // Set fast to slow.
            }

        }

        private void blink()
        {
            if (gbTeachPoint.BackColor == Color.White)
                gbTeachPoint.BackColor = AcuraColors.Background;
            else
                gbTeachPoint.BackColor = Color.White;
        }

        private DataSet CreateDummyRecipe(DataSet _ds, int tableIndex)
        {
            DataRow dr = _ds.Tables[tableIndex].NewRow();
            for (int x = 0; x < _ds.Tables[tableIndex].Columns.Count; x++)
            {
                if (_ds.Tables[tableIndex].Columns[x].DataType == typeof(Int16) || _ds.Tables[tableIndex].Columns[x].DataType == typeof(Int32) ||
                    _ds.Tables[tableIndex].Columns[x].DataType == typeof(Int64) || _ds.Tables[tableIndex].Columns[x].DataType == typeof(int))
                {
                    dr[x] = 0;
                }
                else if (_ds.Tables[tableIndex].Columns[x].DataType == typeof(string) || _ds.Tables[tableIndex].Columns[x].DataType == typeof(String))
                {
                    dr[x] = "";
                }
                else if (_ds.Tables[tableIndex].Columns[x].DataType == typeof(bool) || _ds.Tables[tableIndex].Columns[x].DataType == typeof(Boolean))
                {
                    dr[x] = false;
                }
                else if (_ds.Tables[tableIndex].Columns[x].DataType == typeof(double) || _ds.Tables[tableIndex].Columns[x].DataType == typeof(Double) ||
                    _ds.Tables[tableIndex].Columns[x].DataType == typeof(float))
                {
                    dr[x] = 0.0;
                }
            }

            _ds.Tables[tableIndex].Rows.Add(dr);

            return _ds;
        }

        private void AddNew(string AxisName)
        {
            DataTable dt = RecipeData.Tables["tp_TeachPoint"];
            DataRow dr = dt.NewRow();
            int Rows = dt.Rows.Count + 1;
            dr["AxisName"] = AxisName;
            dr["Encoder"] = (double)0.0;
            dt.Rows.InsertAt(dr, Rows);

            dataGridView1.Refresh();
        }

        private void Delete(string AxisName)
        {
            DataGridView dgv = dataGridView1;
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(AxisName))
                {
                    rowIndex = row.Index;
                    break;
                }
            }


            if (rowIndex >= 0)
            {
                DataTable dt = RecipeData.Tables["tp_TeachPoint"];
                dt.Rows.RemoveAt(rowIndex);

            }
        }

        private int Find(string AxisName)
        {
            DataGridView dgv = dataGridView1;
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(AxisName))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            if (!SafeToMove)
            {
                MessageBox.Show("Position Not Safe to Move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            if (btnGoto.Text == "Stop")
            {
                UCCore.movingName = "";
                StopManualTask.Cancel();
                StopAllMotor();
                LoadCurrentSpeed();
                btnMgoto.Enabled = true;
                btnGoto.Text = "Goto";
                dataGridView1.Enabled = true;
                if (blinker.IsBusy)
                {
                    blinker.CancelAsync();
                }
                return;
            }
            DataGridView dgv = dataGridView1;
            if ((dgv.CurrentRow != null) && (dgv.CurrentRow.Index >= 0))
            {
                DataTable dt = RecipeData.Tables["tp_TeachPoint"];
                DataRow dr = dt.Rows[dataGridView1.CurrentRow.Index];
                saveCurrentSpeed();
                if (!blinker.IsBusy)
                {
                    blinker.RunWorkerAsync();
                }
                ExecuteManual(() => Manual_Single_Goto(dr));
                btnGoto.Text = "Stop";
                btnMgoto.Enabled = false;
                dataGridView1.Enabled = false;
                UCCore.movingName = this.Name;
                return;
            }


        }

        private void btnMgoto_Click(object sender, EventArgs e)
        {
            if (!SafeToMove)
            {
                MessageBox.Show("Position Not Safe to Move.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            if (btnMgoto.Text == "Stop")
            {
                UCCore.movingName = "";
                StopManualTask.Cancel();
                StopAllMotor();
                LoadCurrentSpeed();
                btnGoto.Enabled = true;
                dataGridView1.Enabled = true;
                btnMgoto.Text = "MGoto";
                if (blinker.IsBusy)
                {
                    blinker.CancelAsync();
                }
                return;
            }
            saveCurrentSpeed();
            if (!blinker.IsBusy)
            {
                blinker.RunWorkerAsync();
            }
            ExecuteManual(() => Manual_GotoXYZ());
            UCCore.movingName = this.Name;
            btnMgoto.Text = "Stop";
            dataGridView1.Enabled = false;
            btnGoto.Enabled = false;

            return;
        }

        private void ExecuteManual(Action ation)
        {
            if (ManualTask != null)
                if (ManualTask.Status == TaskStatus.Running)
                    return;
            StopManualTask.Dispose();
            StopManualTask = new CancellationTokenSource();
            ManualTask = Task.Factory.StartNew(ation, StopManualTask.Token);
        }

        private void saveCurrentSpeed()
        {
            try
            {
                if (MOTOR1 != null)
                {
                    SpeedData[0].SpeedRatio = MOTOR1.SpeedRatio;
                    SpeedData[0].WorkSpeed = MOTOR1.WorkSpeed;
                    SpeedData[0].Acceleration = MOTOR1.Acceleration;
                    SpeedData[0].Deceleration = MOTOR1.Deceleration;
                }

                if (MOTOR2 != null)
                {
                    SpeedData[1].SpeedRatio = MOTOR2.SpeedRatio;
                    SpeedData[1].WorkSpeed = MOTOR2.WorkSpeed;
                    SpeedData[1].Acceleration = MOTOR2.Acceleration;
                    SpeedData[1].Deceleration = MOTOR2.Deceleration;
                }

                if (MOTOR3 != null)
                {
                    SpeedData[2].SpeedRatio = MOTOR3.SpeedRatio;
                    SpeedData[2].WorkSpeed = MOTOR3.WorkSpeed;
                    SpeedData[2].Acceleration = MOTOR3.Acceleration;
                    SpeedData[2].Deceleration = MOTOR3.Deceleration;
                }

                if (MOTOR4 != null)
                {
                    SpeedData[3].SpeedRatio = MOTOR4.SpeedRatio;
                    SpeedData[3].WorkSpeed = MOTOR4.WorkSpeed;
                    SpeedData[3].Acceleration = MOTOR4.Acceleration;
                    SpeedData[3].Deceleration = MOTOR4.Deceleration;
                }

                if (MOTOR5 != null)
                {
                    SpeedData[4].SpeedRatio = MOTOR5.SpeedRatio;
                    SpeedData[4].WorkSpeed = MOTOR5.WorkSpeed;
                    SpeedData[4].Acceleration = MOTOR5.Acceleration;
                    SpeedData[4].Deceleration = MOTOR5.Deceleration;
                }

                if (MOTOR6 != null)
                {
                    SpeedData[5].SpeedRatio = MOTOR6.SpeedRatio;
                    SpeedData[5].WorkSpeed = MOTOR6.WorkSpeed;
                    SpeedData[5].Acceleration = MOTOR6.Acceleration;
                    SpeedData[5].Deceleration = MOTOR6.Deceleration;
                }
            }
            catch (Exception ex) { }
        }

        private void LoadCurrentSpeed()
        {
            try
            {
                if (MOTOR1 != null)
                {
                    MOTOR1.SpeedRatio = SpeedData[0].SpeedRatio;
                    MOTOR1.WorkSpeed = SpeedData[0].WorkSpeed;
                    MOTOR1.Acceleration = SpeedData[0].Acceleration;
                    MOTOR1.Deceleration = SpeedData[0].Deceleration;
                }

                if (MOTOR2 != null)
                {
                    MOTOR2.SpeedRatio = SpeedData[1].SpeedRatio;
                    MOTOR2.WorkSpeed = SpeedData[1].WorkSpeed;
                    MOTOR2.Acceleration = SpeedData[1].Acceleration;
                    MOTOR2.Deceleration = SpeedData[1].Deceleration;
                }

                if (MOTOR3 != null)
                {
                    MOTOR3.SpeedRatio = SpeedData[2].SpeedRatio;
                    MOTOR3.WorkSpeed = SpeedData[2].WorkSpeed;
                    MOTOR3.Acceleration = SpeedData[2].Acceleration;
                    MOTOR3.Deceleration = SpeedData[2].Deceleration;
                }

                if (MOTOR4 != null)
                {
                    MOTOR4.SpeedRatio = SpeedData[3].SpeedRatio;
                    MOTOR4.WorkSpeed = SpeedData[3].WorkSpeed;
                    MOTOR4.Acceleration = SpeedData[3].Acceleration;
                    MOTOR4.Deceleration = SpeedData[3].Deceleration;
                }

                if (MOTOR5 != null)
                {
                    MOTOR5.SpeedRatio = SpeedData[4].SpeedRatio;
                    MOTOR5.WorkSpeed = SpeedData[4].WorkSpeed;
                    MOTOR5.Acceleration = SpeedData[4].Acceleration;
                    MOTOR5.Deceleration = SpeedData[4].Deceleration;
                }

                if (MOTOR6 != null)
                {
                    MOTOR6.SpeedRatio = SpeedData[5].SpeedRatio;
                    MOTOR6.WorkSpeed = SpeedData[5].WorkSpeed;
                    MOTOR6.Acceleration = SpeedData[5].Acceleration;
                    MOTOR6.Deceleration = SpeedData[5].Deceleration;
                }
            }
            catch (Exception ex) { }

        }

        private void SetCurrentSpeed(int Ratio)
        {
            if (MOTOR1 != null)
            {
                MOTOR1.SpeedRatio = Ratio;
                MOTOR1.WorkSpeed = 300;
            }
            if (MOTOR2 != null)
            {
                MOTOR2.SpeedRatio = Ratio;
                MOTOR2.WorkSpeed = 300;
            }
            if (MOTOR3 != null)
            {
                MOTOR3.SpeedRatio = Ratio;
                MOTOR3.WorkSpeed = 300;
            }
            if (MOTOR4 != null)
            {
                MOTOR4.SpeedRatio = Ratio;
                MOTOR4.WorkSpeed = 300;
            }
            if (MOTOR5 != null)
            {
                MOTOR5.SpeedRatio = Ratio;
                MOTOR5.WorkSpeed = 300;
            }
            if (MOTOR6 != null)
            {
                MOTOR6.SpeedRatio = Ratio;
                MOTOR6.WorkSpeed = 300;
            }
        }

        private void StopAllMotor()
        {
            if (MOTOR1 != null)
            {
                MOTOR1.Stop();
            }
            if (MOTOR2 != null)
            {
                MOTOR2.Stop();
            }
            if (MOTOR3 != null)
            {
                MOTOR3.Stop();
            }
            if (MOTOR4 != null)
            {
                MOTOR4.Stop();
            }
            if (MOTOR5 != null)
            {
                MOTOR5.Stop();
            }
            if (MOTOR6 != null)
            {
                MOTOR6.Stop();
            }
        }

        private void Manual_GotoXYZ()
        {
            int ManualGotoIndex = 0;
            while (!StopManualTask.IsCancellationRequested)
            {
                switch (ManualGotoIndex)
                {
                    case 0:

                        try
                        {

                            SetCurrentSpeed(10);
                            btnGoto.Enabled = false;
                            dataGridView1.Enabled = false;
                        }
                        catch { }

                        ManualGotoIndex++;

                        break;

                    case 1:
                        if (SafeZone != null)
                        {
                            try
                            {
                                var motor = SafeZone.MotorName[AxisName.Z];
                                bool r1 = motor.Goto(SafeZone.GetValue(AxisName.Z));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error On SafeZone" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                ManualGotoIndex = 4;
                                break;
                            }
                        }
                        else
                        {
                            ManualGotoIndex++;
                        }

                        break;

                    case 2:
                        {
                            var motorX = MotorName[AxisName.X];
                            var motorY = MotorName[AxisName.Y];
                            bool r1 = motorX.Goto(GetValue(AxisName.X));
                            bool r2 = motorY.Goto(GetValue(AxisName.Y));
                            if (r1 && r2)
                                ManualGotoIndex++;
                        }
                        break;

                    case 3:
                        // var motorZ = MotorName[AxisName.Z];
                        //  bool r3 = motorZ.Goto(GetValue(AxisName.Z));
                        //  if (r3)
                        ManualGotoIndex++;
                        break;
                    case 4:
                        LoadCurrentSpeed();
                        btnGoto.Enabled = true;
                        dataGridView1.Enabled = true;
                        //    UCCore.movingName = "";
                        StopAllMotor();
                        StopManualTask.Cancel();
                        if (blinker.IsBusy)
                        {
                            blinker.CancelAsync();
                        }
                        btnMgoto.Text = "MGoto";
                        return;
                }
                Thread.Sleep(1);
            }


        }

        private void Manual_Single_Goto(DataRow row)
        {
            int ManualGotoIndex = 0;
            while (!StopManualTask.IsCancellationRequested)
            {
                switch (ManualGotoIndex)
                {
                    case 0:


                        try
                        {
                            SetCurrentSpeed(10);
                            btnMgoto.Enabled = false;
                            dataGridView1.Enabled = false;
                        }
                        catch { }



                        ManualGotoIndex++;




                        break;

                    case 1:

                        if (row["AxisName"].ToString() == AxisName.Z.ToString())
                        {
                            var motorZ = MotorName[AxisName.Z];
                            if (GetRange(motorZ, GetValue(AxisName.Z)))
                            {
                                ManualGotoIndex++;
                                break;
                            }
                        }
                        if (SafeZone != null)
                        {
                            try
                            {
                                var motor = MotorName[AxisName.Z];
                                bool r1 = motor.Goto(SafeZone.GetValue(AxisName.Z));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error On SafeZone" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                ManualGotoIndex = 4;
                                break;
                            }

                        }
                        else
                        {
                            ManualGotoIndex++;
                        }

                        break;

                    case 2:
                        {
                            if (row["AxisName"].ToString() == AxisName.X.ToString())
                            {
                                var motor = MotorName[AxisName.X];
                                bool r1 = motor.Goto(GetValue(AxisName.X));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.Y.ToString())
                            {
                                var motor = MotorName[AxisName.Y];
                                bool r1 = motor.Goto(GetValue(AxisName.Y));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.Z.ToString())
                            {
                                var motorX = MotorName[AxisName.X];
                                var motorY = MotorName[AxisName.Y];
                                var motorZ = MotorName[AxisName.Z];
                                if (ZSafeCheck)
                                {
                                    if (!GetRange(motorX, GetValue(AxisName.X)) || !GetRange(motorY, GetValue(AxisName.Y)))
                                    {
                                        if (motorZ.GetEncoderPosition() == SafeZone.GetValue(AxisName.Z))
                                        {
                                            if (!PromptZSafeMessage)
                                            {
                                                var movX = motorX.Goto(GetValue(AxisName.X));
                                                var movY = motorY.Goto(GetValue(AxisName.Y));
                                            }
                                            else
                                            {
                                                MessageBox.Show("Axis X and Y not in Position." + Environment.NewLine + "Please Manual Move Axis X and Y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                                ManualGotoIndex = 4;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!PromptZSafeMessage)
                                            {
                                                bool rr1 = motorZ.Goto(SafeZone.GetValue(AxisName.Z));
                                            }
                                            else
                                            {
                                                MessageBox.Show("Axis X and Y not in Position." + Environment.NewLine + "Please Manual Move Axis X and Y", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                                ManualGotoIndex = 4;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!ZSafeCheck || GetRange(motorX, GetValue(AxisName.X)) && GetRange(motorY, GetValue(AxisName.Y)))
                                {
                                    bool r1 = motorZ.Goto(GetValue(AxisName.Z));
                                    if (r1)
                                    {
                                        ManualGotoIndex++;
                                    }
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.W1.ToString())
                            {
                                var motor = MotorName[AxisName.W1];
                                bool r1 = motor.Goto(GetValue(AxisName.W1));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.W2.ToString())
                            {
                                var motor = MotorName[AxisName.W2];
                                bool r1 = motor.Goto(GetValue(AxisName.W2));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.W3.ToString())
                            {
                                var motor = MotorName[AxisName.W3];
                                bool r1 = motor.Goto(GetValue(AxisName.W3));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                            else if (row["AxisName"].ToString() == AxisName.W4.ToString())
                            {
                                var motor = MotorName[AxisName.W4];
                                bool r1 = motor.Goto(GetValue(AxisName.W4));
                                if (r1)
                                {
                                    ManualGotoIndex++;
                                }
                            }
                        }
                        break;

                    case 3:

                        ManualGotoIndex++;
                        break;
                    case 4:
                        LoadCurrentSpeed();
                        btnMgoto.Enabled = true;
                        dataGridView1.Enabled = true;
                        UCCore.movingName = "";
                        StopAllMotor();
                        StopManualTask.Cancel();
                        if (blinker.IsBusy)
                        {
                            blinker.CancelAsync();
                        }
                        btnGoto.Text = "Goto";
                        return;
                }
                Thread.Sleep(1);
            }


        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridView dgv = dataGridView1;
                DataTable dt = RecipeData.Tables["tp_TeachPoint"];
                DataRow dr = dt.Rows[dgv.CurrentRow.Index];
                var data = dr["AxisName"].ToString();

                if (data == AxisName.X.ToString())
                {
                    var motor = MotorName[AxisName.X];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();

                }
                else if (data == AxisName.Y.ToString())
                {
                    var motor = MotorName[AxisName.Y];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();

                }
                else if (data == AxisName.Z.ToString())
                {
                    var motor = MotorName[AxisName.Z];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();
                }
                else if (data == AxisName.W1.ToString())
                {
                    var motor = MotorName[AxisName.W1];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();
                }
                else if (data == AxisName.W2.ToString())
                {
                    var motor = MotorName[AxisName.W2];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();
                }
                else if (data == AxisName.W3.ToString())
                {
                    var motor = MotorName[AxisName.W3];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();
                }
                else if (data == AxisName.W4.ToString())
                {
                    var motor = MotorName[AxisName.W4];
                    if (motor != null)
                        dr["Encoder"] = Math.Round(motor.GetCommandPosition(), 3);
                    dataGridView1.Refresh();
                }


            }
        }

        private void ucTeachPoint_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void tUiupdate_Tick(object sender, EventArgs e)
        {

            #region Control Enable
            if (UCCore.movingName != "" && UCCore.movingName != this.Name)
            {
                Enabled = false;
                blinker.CancelAsync();
                gbTeachPoint.BackColor = Color.White;
            }
            else
            {
                Enabled = true;

            }
            #endregion

            #region Tooltips
            toolTipText = "";
            if (MOTOR1 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR1
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR1.GetEncoderPosition() + Environment.NewLine;
                }
            }
            if (MOTOR2 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR2
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR2.GetEncoderPosition() + Environment.NewLine;
                }
            }
            if (MOTOR3 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR3
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR3.GetEncoderPosition() + Environment.NewLine;
                }
            }
            if (MOTOR4 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR4
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR4.GetEncoderPosition() + Environment.NewLine;
                }
            }
            if (MOTOR5 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR5
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR5.GetEncoderPosition() + Environment.NewLine;
                }
            }
            if (MOTOR6 != null)
            {
                var keys = from entry in MotorName
                           where entry.Value == MOTOR6
                           select entry.Key;
                foreach (var key in keys)
                {
                    toolTipText += key.ToString() + " : " + MOTOR6.GetEncoderPosition() + Environment.NewLine;
                }
            }
            btnToolTip.SetToolTip(btnGoto, toolTipText);
            btnToolTip.SetToolTip(btnMgoto, toolTipText);
            #endregion

            #region Cancel Btn
            //if (CancelButton!=null && CancelButton.IsOn())
            //{
            //    if (StatusChange_Stop)
            //    {
            //        LoadCurrentSpeed();
            //        btnMgoto.Enabled = true;
            //        btnGoto.Enabled = true;
            //        dataGridView1.Enabled = true;
            //        UCCore.movingName = "";
            //        StopAllMotor();
            //        StopManualTask.Cancel();
            //        if (blinker.IsBusy)
            //        {
            //            blinker.CancelAsync();
            //        }
            //        btnGoto.Text = "Goto";
            //        btnMgoto.Text = "MGoto";
            //    }

            //}
            //else
            //{
            //    if (!StatusChange_Stop)
            //        StatusChange_Stop = true;
            //} 
            #endregion

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            //if (!Double.TryParse(e.FormattedValue.ToString(), out double dresult) && headerText.Equals("Encoder"))
            //{
            //    dataGridView1.Rows[e.RowIndex].ErrorText =
            //      "Only Number";
            //    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
            //    e.Cancel = true;
            //}
            //else
            //{
            //    if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
            //    {
            //        dataGridView1.Rows[e.RowIndex].ErrorText = "";
            //        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            //    }

            //}
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (e.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";

                e.ThrowException = false;
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == '-' || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
            //allow only one dot
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;

            }
        }
    }


    public class Data
    {
        public string AxisName;
        public double Encoder;
    }

    public class MotorSpeedData
    {
        public double SpeedRatio;
        public double WorkSpeed;
        public double Acceleration;
        public double Deceleration;
    }
}
