using ABBRobot;
using Acura3._0.ModuleForms;
using SASDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    public partial class AutoTeachRBForm : Form
    {
        DataSet backup;
        ListView lvPointBackup = new ListView();
        //string tableName = "RSeq";
        string tableName;
        public string dataString = "";
        public bool Commited = false;
        RobotPosDataList dataList = new RobotPosDataList();
        public bool Complete = false;
        private double MaximumSpeed = 1000;
        public AutoTeachRBForm()
        {
            InitializeComponent();
        }

        public AutoTeachRBForm(DataGridView dgv, double MaximumSpeed, ref Task ManualTask, ref CancellationTokenSource StopManualTask, ref DataSet RecipeData)
        {
            InitializeComponent();
            cbCol.SelectedItem = "1";
            cbRow.SelectedItem = "1";

          
            //tp1.SafeZone = MiddleLayer.AbbF.rbSafe;
            //tp2.SafeZone = MiddleLayer.AbbF.rbSafe;
            //tp3.SafeZone = MiddleLayer.AbbF.rbSafe;

            tableName = dgv.DataMember.ToString();
            RecipeDataAutoTeach = RecipeData.Copy();
            backup = RecipeDataAutoTeach.Copy();
            dgvData.DataSource = RecipeDataAutoTeach;
            dgvData.DataMember = dgv.DataMember.ToString();
            this.MaximumSpeed = MaximumSpeed;
            dgvData.Refresh();
        }


        private void wpReference_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

        }

        public void ReadAllTeachPoint(string Path, string ModuleName)
        {

            List<Control> availControls = GetControls(panel1);
            foreach (var control in availControls)
            {
                if (control is RobotTeachPoint)
                {
                    var LoadD = (RobotTeachPoint)control;
                    LoadD.LoadRecipe(Path, ModuleName);
                }
            }
        }

        public void SaveAllTeachPoint(string ModuleName)
        {
            DataSet ds = new DataSet();
            bool save = false;
            List<Control> availControls = GetControls(panel1);
            foreach (var control in availControls)
            {
                if (control is RobotTeachPoint)
                {
                    var saveD = (RobotTeachPoint)control;
                    //saveD.saveRecipe(Path);

                    ds.Merge(saveD.GetRecipe(ModuleName));
                    save = true;
                    saveD.SetRecipeTableName();
                }


            }
            if (save)
            {
                try
                {
                    string path = SysPara.RecipeDataDirectory + "\\" + "AutoTeaching\\" + "AutoTeachRB.uc";
                    FileInfo fiTmp1 = new FileInfo(path);
                    if (fiTmp1.Directory.Exists == false)
                        fiTmp1.Directory.Create();
                    ds.WriteXml(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }


        }

        public DataSet GetRecipe()
        {
            return RecipeDataAutoTeach;
        }

        public static List<Control> GetControls(Control form)
        {
            var controlList = new List<Control>();

            foreach (Control childControl in form.Controls)
            {
                controlList.AddRange(GetControls(childControl));
                controlList.Add(childControl);
            }
            return controlList;
        }

        public IEnumerable<T> FindControls<T>(Control control) where T : Control
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => FindControls<T>(ctrl))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == typeof(T)).Cast<T>();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ReadAllTeachPoint(SysPara.RecipeDataDirectory + "\\" + "AutoTeaching\\" + "AutoTeachRB.uc", "AutoTeach");
        }

        private void wpMode_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            lvPointBackup.Items.Clear();
            reset();
            lvPoint.Items.Clear();
            dataList.Post.Clear();
            int col = Convert.ToInt32(cbCol.SelectedItem);
            int Row = Convert.ToInt32(cbRow.SelectedItem);
            double CompensateX = (tp3.GetValue(RobotTeachPoint.AxisName.X) - tp2.GetValue(RobotTeachPoint.AxisName.X));
            double offsetX = (tp2.GetValue(RobotTeachPoint.AxisName.X) - tp1.GetValue(RobotTeachPoint.AxisName.X));
            double CompensateY = (tp2.GetValue(RobotTeachPoint.AxisName.Y) - tp1.GetValue(RobotTeachPoint.AxisName.Y));
            double offsetY = (tp3.GetValue(RobotTeachPoint.AxisName.Y) - tp2.GetValue(RobotTeachPoint.AxisName.Y));
            double offsetZ = (tp3.GetValue(RobotTeachPoint.AxisName.Z) - tp1.GetValue(RobotTeachPoint.AxisName.Z));
            double offsetW = (tp3.GetValue(RobotTeachPoint.AxisName.W) - tp1.GetValue(RobotTeachPoint.AxisName.W));
            bool isZOffset = cbAllowZOffset.Checked;
            bool isWOffset = cbAllowWOffset.Checked;
            int pocket = 0;
            if (col > 1)
            {
                offsetX = offsetX / (col - 1);
                CompensateY = CompensateY / (col - 1);
            }

            if (Row > 1)
            {
                offsetY = offsetY / (Row - 1);
                CompensateX = CompensateX / (Row - 1);

            }
            if (isZOffset)
                offsetZ = offsetZ / ((Row * col) - 1);
            else
                offsetZ = 0;
            if (isWOffset)
                offsetW = offsetW / ((Row * col) - 1);
            else
                offsetW = 0;

            tbOffsetX.Text = Math.Round(offsetX, 2).ToString();
            tbOffsetY.Text = Math.Round(offsetY, 2).ToString();
            tbRelX.Text = Math.Round(CompensateX, 2).ToString();
            tbRelY.Text = Math.Round(CompensateY, 2).ToString();
            Pos3D[,] dataPoint = new Pos3D[Row, col];
            int count = 0;
            if (rbMode1.Checked)
            {
                for (int r = 0; r < Row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {

                        dataPoint[r, c].x = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.X) + calc(offsetX, c) + calc(CompensateX, r), 2);
                        dataPoint[r, c].y = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.Y) + calc(offsetY, r) + calc(CompensateY, c), 2);
                        dataPoint[r, c].z = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.Z) + calc(offsetZ, pocket), 2);
                        dataPoint[r, c].w = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.W) + calc(offsetW, pocket), 2);
                        pocket++;
                        //AddListView("Point_" + (count + 1) + "_(Row=" + (r + 1) + "," + "Col=" + (c + 1) + ")", dataPoint[r, c].x, dataPoint[r, c].y);
                        //  count++;
                    }
                }
                double[] tmpX = new double[col];
                double[] tmpY = new double[col];
                for (int r = 0; r < Row; r++)
                {

                    if (r % 2 != 0)
                    {
                        for (int x = 0; x < tmpX.Length; x++)
                        {
                            tmpX[x] = dataPoint[r, x].x;
                            tmpY[x] = dataPoint[r, x].y;
                        }
                        Array.Reverse(tmpX);
                        Array.Reverse(tmpY);

                        for (int j = 0; j < col; j++)
                        {
                            dataPoint[r, j].x = tmpX[j];
                            dataPoint[r, j].y = tmpY[j];
                        }
                    }


                }

                for (int r = 0; r < Row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        AddListView("P_" + (count + 1) + "_(R=" + (r + 1) + "," + "C=" + (c + 1) + ")", dataPoint[r, c].x, dataPoint[r, c].y, dataPoint[r, c].z, dataPoint[r, c].w, tp1.GetArm() == ABB_RobotControl.Arm.L ? "Left" : "Right");
                        count++;
                    }
                }
            }
            else if (rbMode2.Checked)
            {
                for (int r = 0; r < Row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        dataPoint[r, c].x = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.X) + calc(offsetX, c) + calc(CompensateX, r), 2);
                        dataPoint[r, c].y = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.Y) + calc(offsetY, r) + calc(CompensateY, c), 2);
                        dataPoint[r, c].z = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.Z) + calc(offsetZ, pocket), 2);
                        dataPoint[r, c].w = Math.Round(tp1.GetValue(RobotTeachPoint.AxisName.W) + calc(offsetW, pocket), 2);
                        pocket++;
                        AddListView("P_" + (count + 1) + "_(R=" + (r + 1) + "," + "C=" + (c + 1) + ")", dataPoint[r, c].x, dataPoint[r, c].y, dataPoint[r, c].z, dataPoint[r, c].w, tp1.GetArm() == ABB_RobotControl.Arm.L ? "Left" : "Right");
                        count++;
                    }
                }
            }
            else if (rbMode3.Checked)
            {

            }


            dataString = XmlHelper.SerializeToXmlString<RobotPosDataList>(dataList);
            backup = RecipeDataAutoTeach.Copy();

        }

        private void AddListView(string pointName, double X, double Y, double Z, double W, string Arm)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = pointName;
            lvi.UseItemStyleForSubItems = false;
            lvi.SubItems.Add(X.ToString());
            lvi.SubItems.Add(Y.ToString());
            lvi.SubItems.Add(Z.ToString());
            lvi.SubItems.Add(W.ToString());
            lvi.SubItems.Add(Arm);
            lvPoint.Items.Add(lvi);
            dataList.Post.Add(new ABB_RobotControl.RobotPosData { X = X, Y = Y, Z = Z, R = W });
        }

        private double calc(double X, int index)
        {
            return X * index;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {

        }

        private void wpFinish_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Commited = true;
            Complete = true;
            SaveAllTeachPoint("AutoTeach");
        }

        private void btnJog_Click(object sender, EventArgs e)
        {
            MiddleLayer.MotorJogF.Show();
        }

        private void wpDataTable_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            //flpTable.Controls.Clear();
            //foreach (DataTable table in RecipeData1.Tables)
            //{
            //   flpTable.Controls.Add(CreateRadioButton(table.TableName));
            //}
        }

        public RadioButton CreateRadioButton(string Name)
        {
            RadioButton rdo = new RadioButton();
            Font font = new Font("Segoe UI", 13.8f);
            rdo.Name = "rb" + Name;
            rdo.Text = Name;
            rdo.Font = font;
            rdo.AutoSize = true;
            rdo.CheckedChanged += Rdo_CheckedChanged;
            return rdo;
        }

        private void Rdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked)
            {
                tableName = rdo.Text;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            try
            {
                int speed = 300;
                int zSpeed = 300;
                if (!int.TryParse(txtSpeed.Text, out speed) || !int.TryParse(txtZSpeed.Text, out zSpeed))
                {
                    MessageBox.Show(new Form { TopMost = true }, "Error on getting Speed Data");
                    return;
                }
                if (lvPoint.SelectedIndices.Count == 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Please select the source");
                    return;
                }
                if (!CheckSpeed())
                {
                    MessageBox.Show(new Form { TopMost = true }, $"Cannot More Than Robot Maximum Speed[{MaximumSpeed}]");
                    return;
                }
                int selectedIndex = lvPoint.SelectedIndices[0];
                int selectedDesIndex = dgvData.CurrentRow.Index;
                if (selectedDesIndex == -1)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Please select the Destination");
                    return;
                }
                var sourceX = lvPoint.SelectedItems[0].SubItems[1].Text;
                var sourceY = lvPoint.SelectedItems[0].SubItems[2].Text;
                var sourceZ = lvPoint.SelectedItems[0].SubItems[3].Text;
                var sourceW = lvPoint.SelectedItems[0].SubItems[4].Text;
                var sourceArm = lvPoint.SelectedItems[0].SubItems[5].Text;

                if (lvPoint.Items[selectedIndex].BackColor == Color.LightGray)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Point has been Taken");
                    return;
                }
                if (dgvData.DataSource == RecipeDataAutoTeach)
                {
                    DataTable dt = RecipeDataAutoTeach.Tables[tableName];
                    DataRow dr = dt.Rows[selectedDesIndex];
                    dr["X"] = sourceX;
                    dr["Y"] = sourceY;
                    dr["Z"] = sourceZ;
                    dr["W"] = sourceW;
                    dr["Arm"] = sourceArm;
                }
                else
                {
                    DataTable dt = backup.Tables[tableName];
                    DataRow dr = dt.Rows[selectedDesIndex];
                    dr["X"] = sourceX;
                    dr["Y"] = sourceY;
                    dr["Z"] = sourceZ;
                    dr["W"] = sourceW;
                    dr["Arm"] = sourceArm;
                }

                foreach (ListViewItem.ListViewSubItem lvi in lvPoint.Items[selectedIndex].SubItems)
                {
                    lvi.BackColor = Color.LightGray;
                }
                lvPoint.Items[selectedIndex].BackColor = Color.LightGray;
                dgvData.Rows[selectedDesIndex].DefaultCellStyle.BackColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
            }

        }

        private void btnAll_Click_1(object sender, EventArgs e)
        {
            int speed = 300;
            int zSpeed = 300;
            if (!int.TryParse(txtSpeed.Text, out speed) || !int.TryParse(txtZSpeed.Text, out zSpeed))
            {
                MessageBox.Show(new Form { TopMost = true }, "Error on getting Speed Data");
                return;
            }

            if (!CheckSpeed())
            {
                MessageBox.Show(new Form { TopMost = true }, $"Cannot More Than Robot Maximum Speed[{MaximumSpeed}]");
                return;
            }

            try
            {
                int count = 0;
                foreach (ListViewItem lvw in lvPoint.Items)
                {
                    ListViewItem lvwback = (ListViewItem)lvw.Clone();
                    lvPointBackup.Items.Add(lvwback);
                    DataTable dt = RecipeDataAutoTeach.Tables[tableName];
                    DataRow dr = dt.NewRow();
                    int dgvRow = dgvData.Rows.Count;
                    dr["X"] = Convert.ToDouble(lvw.SubItems[1].Text);
                    dr["Y"] = Convert.ToDouble(lvw.SubItems[2].Text);
                    dr["Z"] = Convert.ToDouble(lvw.SubItems[3].Text);
                    dr["W"] = Convert.ToDouble(lvw.SubItems[4].Text);
                    dr["Arm"] = lvw.SubItems[5].Text;
                    dr["PointName"] = GenerateName(lvw.SubItems[0].Text, dgvRow);
                    //Depend on the Dataset in Rseq
                    dr["Mode"] = "Place";
                    dr["B.Delay"] = 0;
                    dr["A.Delay"] = 0;
                    dr["Index"] = 0;
                    dr["Stack"] = "None";
                    dr["Move"] = "LMove";
                    dr["Speed"] = speed;
                    dr["ZSpeed"] = zSpeed;
                    dr["Enabled"] = true;
                    dt.Rows.Add(dr);
                    //dgvData.Refresh();
                    lvPoint.Items.Remove(lvw);
                    dgvData.Rows[dgvRow].DefaultCellStyle.BackColor = Color.LightGray;
                    count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
            }

        }

        private string GenerateName(string Name, int row)
        {
            if (DuplicationCheck(dgvData, Name, row))
            {
                return GenerateName(Name + "_" + GenerateID(), row);
            }
            else
            {
                return Name;
            }
        }

        private string GenerateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(5)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        private bool DuplicationCheck(DataGridView gr, string PointName, int row)
        {
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                if (gr[0, i].Value.ToString().Equals(PointName) && row != i) return true;
            }
            return false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            try
            {
                RecipeDataAutoTeach = backup.Copy();
                dgvData.DataSource = backup;
                dgvData.DataSource = RecipeDataAutoTeach;


                if (dgvData.InvokeRequired)
                {
                    dgvData.Invoke((MethodInvoker)delegate ()
                    {
                        dgvData.Update();
                        dgvData.Refresh();
                    });
                }
                else
                {
                    dgvData.Update();
                    dgvData.Refresh();
                }

                foreach (ListViewItem lvi in lvPoint.Items)
                {
                    lvi.BackColor = Color.White;
                    foreach (ListViewItem.ListViewSubItem lvi1 in lvi.SubItems)
                    {
                        lvi1.BackColor = Color.White;
                    }
                }
                foreach (ListViewItem lvw in lvPointBackup.Items)
                {
                    lvPointBackup.Items.Remove(lvw);
                    lvPoint.Items.Add(lvw);
                }
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private bool CheckSpeed()
        {
            int speed = 300;
            int zSpeed = 300;
            if (!int.TryParse(txtSpeed.Text, out speed) || !int.TryParse(txtZSpeed.Text, out zSpeed))
            {

                return false;
            }
            if (speed > MaximumSpeed || zSpeed > MaximumSpeed)
            {
                return false;
            }
            return true;
        }

        private void cbAllowZOffset_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbAllowWOffset_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                int speed = 300;
                int zSpeed = 300;
                if (!int.TryParse(txtSpeed.Text, out speed) || !int.TryParse(txtZSpeed.Text, out zSpeed))
                {
                    MessageBox.Show(new Form { TopMost = true }, "Error on getting Speed Data");
                    return;
                }

                if (!CheckSpeed())
                {
                    MessageBox.Show(new Form { TopMost = true }, $"Cannot More Than Robot Maximum Speed[{MaximumSpeed}]");
                    return;
                }
                if (dgvData.DataSource == RecipeDataAutoTeach)
                {
                    DataTable dt = RecipeDataAutoTeach.Tables[tableName];
                    for (int x = 0; x < lvPoint.Items.Count; x++)
                    {
                        try
                        {
                            DataRow DestItem = dt.Rows[x];
                            ListViewItem sourceItem = lvPoint.Items[x];
                            DestItem["X"] = Convert.ToDouble(sourceItem.SubItems[1].Text);
                            DestItem["Y"] = Convert.ToDouble(sourceItem.SubItems[2].Text);
                            DestItem["Z"] = Convert.ToDouble(sourceItem.SubItems[3].Text);
                            DestItem["W"] = Convert.ToDouble(sourceItem.SubItems[4].Text);
                            DestItem["Arm"] = sourceItem.SubItems[5].Text.ToString();
                        }
                        catch (Exception)
                        {
                            continue;
                        }


                    }
                }
                else
                {
                    DataTable dt = backup.Tables[tableName];
                    for (int x = 0; x < lvPoint.Items.Count; x++)
                    {
                        try
                        {
                            DataRow DestItem = dt.Rows[x];
                            ListViewItem sourceItem = lvPoint.Items[x];
                            DestItem["X"] = Convert.ToDouble(sourceItem.SubItems[1].Text);
                            DestItem["Y"] = Convert.ToDouble(sourceItem.SubItems[2].Text);
                            DestItem["Z"] = Convert.ToDouble(sourceItem.SubItems[3].Text);
                            DestItem["W"] = Convert.ToDouble(sourceItem.SubItems[4].Text);
                            DestItem["Arm"] = sourceItem.SubItems[5].Text.ToString();
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, ex.Message);
            }
        }
    }
}
