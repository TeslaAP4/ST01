using Acura3._0.ModuleForms;
using SASDK;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Acura3._0.ModuleForms.ABBRobotForm;

namespace Acura3._0.FunctionForms
{
    public partial class AutoTeachSummary : Form
    {
        private Task ManualTask;
        private CancellationTokenSource StopManualTask;
        public string dataString = "";
        public AutoTeachSummary()
        {
            InitializeComponent();
        }
        public AutoTeachSummary(string data, ref Task ManualTask, ref CancellationTokenSource StopManualTask)
        {
            InitializeComponent();
            this.ManualTask = ManualTask;
            this.StopManualTask = StopManualTask;
            var data1 = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(data);
            SetDoubleBuffer(dgvPoint);
            for (int x = 0; x < data1.Post.Count; x++)
            {
                AddListView(x.ToString(), data1.Post[x].X, data1.Post[x].Y, data1.Post[x].Z, data1.Post[x].R);
            }

        }
        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }
        private void btnAutoTeach_Click(object sender, EventArgs e)
        {
            //AutoTeachRBForm form = new AutoTeachRBForm(GetSettingValue("MSet", "MaxRobotSpeed"),ref ManualTask, ref StopManualTask);
            //form.ShowDialog();

            //if (form.Commited && form.dataString != null && form.dataString != "")
            //{
            //    dgvPoint.Rows.Clear();
            //    var data1 = XmlHelper.DeserializeFromXmlString<RobotPosDataList>(form.dataString);
            //    for (int x = 0; x < data1.Post.Count; x++)
            //    {
            //        AddListView(x.ToString(), data1.Post[x].X, data1.Post[x].Y, data1.Post[x].Z, data1.Post[x].R);
            //    }
            //}

        }

        private void AddListView(string pointName, double X, double Y, double Z, double W)
        {

            this.dgvPoint.Rows.Add(pointName, X, Y, Z, W, "Go To");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoTeachSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            ABBRobot.Extern.GlobalVar.movingName = "";
            RobotPosDataList datalist = new RobotPosDataList();
            foreach (DataGridViewRow row in dgvPoint.Rows)
            {
                datalist.Post.Add(new ABBRobot.ABB_RobotControl.RobotPosData
                {
                    X = Convert.ToDouble(row.Cells["X"].Value),
                    Y = Convert.ToDouble(row.Cells["Y"].Value),
                    Z = Convert.ToDouble(row.Cells["Z"].Value),
                    R = Convert.ToDouble(row.Cells["W"].Value)
                });

            }
            dataString = XmlHelper.SerializeToXmlString<RobotPosDataList>(datalist);
        }

        private void dgvPoint_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            double i;
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                if (!double.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    dgvPoint.Rows[e.RowIndex].ErrorText =
                     "Must Be Numeric Value";
                    dgvPoint.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    dgvPoint.Rows[e.RowIndex].ErrorText = "";
                    dgvPoint.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }

            }

        }

        private void dgvPoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                ABBRobot.ABB_RobotControl.RobotPosData posData = new ABBRobot.ABB_RobotControl.RobotPosData();
                posData.X = Convert.ToDouble(dgvPoint[1, e.RowIndex].Value);
                posData.Y = Convert.ToDouble(dgvPoint[2, e.RowIndex].Value);
                posData.Z = Convert.ToDouble(dgvPoint[3, e.RowIndex].Value);
                posData.R = Convert.ToDouble(dgvPoint[4, e.RowIndex].Value);
                DialogResult dialogResult = MessageBox.Show($"Go to Index {e.RowIndex}?", "Confirmation", MessageBoxButtons.YesNo);

                //if (dialogResult == DialogResult.Yes)
                //{
                //    MiddleLayer.AbbF.ExecuteManual(() => MiddleLayer.AbbF.MoveToRobotPostManual(ABBRobotForm.RobotAxis.All, posData.X, posData.Y, posData.Z, posData.R, true, RobotMove.JMove));
                //}

            }
        }
    }
}
