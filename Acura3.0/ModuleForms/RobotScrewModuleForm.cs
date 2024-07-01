using Acura3._0.Classes;
using AcuraLibrary.Forms;
using JabilSDK;
using NPFanucRobotDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OPTControl;

namespace Acura3._0.ModuleForms
{
    public partial class RobotScrewModuleForm : ModuleBaseForm  
    {
        public RobotScrewModuleForm()
        {
            InitializeComponent();

            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);

            #region 螺丝曲线图控件1
            ScrewData1.BackColor = Color.WhiteSmoke;
            ScrewData1.BackGradientStyle = GradientStyle.TopBottom;
            ScrewData1.BackSecondaryColor = Color.White;
            ScrewData1.BorderlineColor = Color.FromArgb(26, 59, 105);
            ScrewData1.BorderlineDashStyle = ChartDashStyle.Solid;
            ScrewData1.BorderlineWidth = 2;
            ScrewData1.BorderSkin.SkinStyle = BorderSkinStyle.None;

            ChartArea chartArea = new ChartArea("TighteningCurves");
            chartArea.BackColor = Color.Gainsboro;
            chartArea.BackGradientStyle = GradientStyle.TopBottom;
            chartArea.BackSecondaryColor = Color.White;
            chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            ScrewData1.ChartAreas.Add(chartArea);

            Title title = new Title("Torque And Angle Curves");
            title.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(26, 59, 105);
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.ShadowOffset = 2;
            ScrewData1.Titles.Add(title);

            Legend legend = new Legend("Series");
            legend.Alignment = StringAlignment.Far;
            legend.LegendStyle = LegendStyle.Row;
            legend.Docking = Docking.Bottom;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            legend.IsTextAutoFit = false;
            ScrewData1.Legends.Add(legend);

            Series seriesTorque = new Series("Torque");
            seriesTorque.ChartType = SeriesChartType.FastLine;
            seriesTorque.ChartArea = "TighteningCurves";
            seriesTorque.BorderColor = Color.FromArgb(224, 64, 10);
            seriesTorque.ShadowColor = Color.Black;
            seriesTorque.BorderWidth = 2;
            seriesTorque.XValueType = ChartValueType.Time;
            seriesTorque.YValueType = ChartValueType.Double;
            seriesTorque.YAxisType = AxisType.Primary;
            seriesTorque.XAxisType = AxisType.Primary;
            ScrewData1.Series.Add(seriesTorque);

            Series seriesAngle = new Series("Angle");
            seriesAngle.ChartType = SeriesChartType.FastLine;
            seriesAngle.ChartArea = "TighteningCurves";
            seriesAngle.BorderColor = Color.FromArgb(180, 26, 59, 105);
            seriesAngle.ShadowColor = Color.Black;
            seriesAngle.BorderWidth = 2;
            seriesAngle.XValueType = ChartValueType.Time;
            seriesAngle.YValueType = ChartValueType.Double;
            seriesAngle.YAxisType = AxisType.Secondary;
            seriesAngle.XAxisType = AxisType.Primary;
            ScrewData1.Series.Add(seriesAngle);

            chartArea.AxisX.Title = "Time";
            chartArea.AxisX.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisX.LabelStyle.Format = "s.f";
            chartArea.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineWidth = 2;
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.ScrollBar.LineColor = Color.Black;
            chartArea.AxisX.ScrollBar.Size = 10;

            chartArea.AxisY.Title = "Torque";
            chartArea.AxisY.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.LineWidth = 2;
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.ScrollBar.LineColor = Color.Black;
            chartArea.AxisY.ScrollBar.Size = 10;

            chartArea.AxisY2.Title = "Angle";
            chartArea.AxisY2.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisY2.IsLabelAutoFit = false;
            chartArea.AxisY2.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            chartArea.AxisY2.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY2.LineWidth = 2;
            chartArea.AxisY2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chartArea.AxisY2.ScrollBar.LineColor = Color.Black;
            chartArea.AxisY2.ScrollBar.Size = 10;
            #endregion

            GetSerialPort(C_Com);
        }

        #region Override Method

        public override void AlwaysRun()
        {
        }

        public override void InitialReset()
        {
        }

        public override void Initial()
        {
        }

        public override void RunReset()
        {
        }

        public override void Run()
        {
        }

        public override void StartRun()
        {
            RunTM.Restart();
        }

        public override void StopRun()
        {
        }

        public override void ServoOn()
        {
        }

        public override void ServoOff()
        {
        }

        public override void SetSpeed(int SpeedRatio)
        {

        }

        #endregion

        #region Local Method
        private void FlowChartMessage_PauseRaise(object sender, EventArgs e)
        {
            MiddleLayer.StopRun();
        }

        private void FlowChartMessage_ResetRaise(object sender, EventArgs e)
        {
            RunTM.Restart();
        }

        private void SetDoubleBuffer(Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        #endregion

        #region 延时函数
        // 延时函数
        public static bool B_DelayMs(int delayMilliseconds)
        {
            DateTime now = DateTime.Now;
            Double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.TotalMilliseconds + spand.Seconds * 1000;
                Application.DoEvents();
            }
            while (s < delayMilliseconds);
            return true;
        }

        #endregion

        #region Atlas
        AtlasLibrary1 ScrewDriver = new AtlasLibrary1();
        private void btnConnectScrew_Click(object sender, EventArgs e)
        {
            ScrewDriver.Atlas_Connect(GetSettingValue("RSet", "ScrewIP"), GetSettingValue("PSet", "ScrewPort"));
            if (ScrewDriver.bConnectStatus)
            {
                btnConnectScrew.BackColor = Color.Green;
            }
            else
            {
                btnConnectScrew.BackColor = Color.Red;
            }
        }

        private void btnDisScrew_Click(object sender, EventArgs e)
        {
            ScrewDriver.Disconnect();
            btnConnectScrew.BackColor = Color.Transparent;
        }
        #endregion

        #region Robot
        public Fanuc_RobotControl fanuc_Robot = new Fanuc_RobotControl();
        private void B_ConnectRobot_Click(object sender, EventArgs e)
        {
            if (fanuc_Robot.ConnectToRobot())
            {
                B_ConnectResult.Text = "ConnectSuccess";
                B_ConnectResult.BackColor = Color.Green;
            }
            else
            {
                B_ConnectResult.Text = "ConnectFail";
                B_ConnectResult.BackColor = Color.Red;
            }
        }
        private void B_ReadR_Click(object sender, EventArgs e)
        {
            if (fanuc_Robot.ReflashRobotR())
            {
                int RNum = Convert.ToInt32(N_ReadRAdress.Value);
                T_ReadData.Text = fanuc_Robot.D_ReadRobotR[RNum].ToString();
            }
        }
        private void B_WritePR_Click(object sender, EventArgs e)
        {
            double[] d = { 1, 2, 3, 4, 5, 6 };
            d[0] = Convert.ToDouble(N_WritePRX.Value);
            d[1] = Convert.ToDouble(N_WritePRY.Value);
            d[2] = Convert.ToDouble(N_WritePRZ.Value);
            d[3] = Convert.ToDouble(N_WritePRA.Value);
            d[4] = Convert.ToDouble(N_WritePRB.Value);
            d[5] = Convert.ToDouble(N_WritePRC.Value);
            if (fanuc_Robot.WriteToRobotPR(Convert.ToInt16(N_WritePRAdress.Value), d))
            {
                B_WritePR.BackColor = Color.Green;
            }
            else
            {
                B_WritePR.BackColor = Color.Red;
            }
        }
        private void B_WriteR_Click(object sender, EventArgs e)
        {
            if (fanuc_Robot.WriteToRobotR(Convert.ToInt32(N_WriteRAdress.Value), Convert.ToDouble(N_WriteData.Value)))
            {
                B_WriteR.BackColor = Color.Green;
            }
            else
            {
                B_WriteR.BackColor = Color.Red;
            }
        }
        private void B_ReadPR_Click(object sender, EventArgs e)
        {
            int PRNum = Convert.ToInt16(N_ReadPRAddress.Value);
            fanuc_Robot.ReadFromRobotPR(PRNum);
            T_ReadPRX.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 0].ToString();
            T_ReadPRY.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 1].ToString();
            T_ReadPRZ.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 2].ToString();
            T_ReadPRA.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 3].ToString();
            T_ReadPRB.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 4].ToString();
            T_ReadPRC.Text = fanuc_Robot.D_ReadRobotPR[PRNum, 5].ToString();
        }
        #endregion
        
        #region Axis
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Add Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataTable D_dt = RecipeData.Tables["T_JackingShaft"];
            int I_Rows = D_dt.Rows.Count;
            DataRow D_dr = D_dt.NewRow();
            D_dr[0] = (I_Rows + 1);
            D_dr[1] = "";
            D_dr[2] = "";
            D_dt.Rows.Add(D_dr);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure delete Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_PcbPoint;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_JackingShaft"];
                D_dt.Rows.RemoveAt(D_dgv.CurrentRow.Index);
                //LogShow(SysPara.UserName + "  " + "delete stress point", true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                //LogShow(SysPara.UserName + "  " + "delete faied, datasheet have no data", false);
            }
        }

        private void btnRepalce_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Replace Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_PcbPoint;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_JackingShaft"];
                DataRow D_dr = D_dt.Rows[D_dgv.CurrentRow.Index];
                D_dr[0] = D_dr[0];
                D_dr[1] = "";//AxisX.GetCommandPosition().ToString("F3");
                D_dr[2] = "";// AxisY.GetCommandPosition().ToString("F3");
                //LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " replace point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                // LogShow(SysPara.UserName + "  " + "replace faied, datasheet have no data", false);
            }
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Confirm to move to the selected point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_PcbPoint;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                AxisPos axisPos = GetPointList(D_dgv.CurrentRow.Cells[2].Value.ToString());
                double a = axisPos.Position;
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
            }
        }

        // Relevant point data
        public static List<AxisPos> L_FlipAxisPosData = new List<AxisPos>();

        /// <summary>
        /// Axis Pos Struct
        /// </summary>
        public struct AxisPos
        {
            public string Index;
            public double Position;
            public string Remark;
        }
        
        /// <summary>
        /// Get data for a point
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns></returns>
        public static AxisPos GetPointList(string pointName)
        {
            LoadHPointList();
            AxisPos modelHPostData = new AxisPos();
            for (int i = 0; i < L_FlipAxisPosData.Count; i++)
            {
                if (L_FlipAxisPosData[i].Remark == pointName)
                {
                    modelHPostData = L_FlipAxisPosData[i];
                }
            }
            return modelHPostData;
        }
        

        /// <summary>
        /// Data in a table in RecipeData
        /// </summary>
        public static void LoadHPointList()
        {
            L_FlipAxisPosData.Clear();
            System.Data.DataTable H1dt = MiddleLayer.ScrewFastenModuleF.RecipeData.Tables["T_JackingShaft"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = H1dt.Rows[i];
                AxisPos ToSolderPoint = new AxisPos
                {

                    Index = dr["Index"].ToString(),
                    Position = Convert.ToDouble(dr["Position"]),
                    Remark = dr["Remark"].ToString()
                };
                L_FlipAxisPosData.Add(ToSolderPoint);
            }
        }

        #endregion

        #region  PressureSensor
        ReadCOM readCOM = new ReadCOM();
        public  List<PsData> L_FlipPsData = new List<PsData>();

        public struct PsData
        {
            public string Name;
            public string Command;
        }

        /// <summary>
        /// Get serial port list
        /// </summary>
        private void B_Refresh_Click(object sender, EventArgs e)
        {
            GetSerialPort(C_Com);
            TextDataShow("Refreshing Com succeeded", R_PsText, false);
        }
        private void B_PsAdd_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Add?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataTable D_dt = RecipeData.Tables["T_PressureSensor "];
            int I_Rows = D_dt.Rows.Count;
            DataRow D_dr = D_dt.NewRow();
            D_dr[0] = "";
            D_dr[1] = "";
            D_dt.Rows.Add(D_dr);
            TextDataShow("Add successfully", R_PsText, true);
        }

        private void B_PsDelete_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_Ps;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_PressureSensor"];
                D_dt.Rows.RemoveAt(D_dgv.CurrentRow.Index);
                TextDataShow("Successfully Deleted", R_PsText, true);
            }
            else
            {
                TextDataShow("faied, datasheet have no data", R_PsText, false);
            }
        }

        public  void LoadHPsList()
        {
            L_FlipAxisPosData.Clear();
            System.Data.DataTable H1dt = MiddleLayer.ScrewFastenModuleF.RecipeData.Tables["T_PressureSensor"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = H1dt.Rows[i];
                PsData psData = new PsData
                {

                    Name = dr["Name"].ToString(),
                    Command = dr["Command"].ToString()
                };
                L_FlipPsData.Add(psData);
            }
        }
        private void GetSerialPort(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            Microsoft.Win32.RegistryKey keyCom = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox.Items.Add(sValue);
                    C_Com.Text = C_Com.Items[C_Com.Items.Count - 1].ToString();
                }
            }
        }
        public string Ps_Command(string Name)
        {
            LoadHPsList();
            foreach (PsData item in L_FlipPsData)
            {
                if (item.Name.ToString() == Name)
                {
                    return item.Command.ToString();
                }
            }
            return "";
        }

        public static void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }
        public void TextDataShow(string Textshow, RichTextBox richText, bool OK)
        {
            RefreshDifferentThreadUI(richText, () =>
            {
                if (richText.TextLength > 10000)
                {
                    richText.Clear();
                }
                if (OK == true)
                    richText.SelectionColor = Color.Green;
                else
                    richText.SelectionColor = Color.Red;
                richText.AppendText(Textshow + Environment.NewLine);
                richText.SelectionStart = richText.TextLength; richText.ScrollToCaret();
            });
        }
        
        private void B_PsConnect_Click(object sender, EventArgs e)
        {
            if (readCOM.ConnectionState)
            {
                TextDataShow("Device connected", R_PsText, true);
                return;
            }
            if (readCOM.ConnectCom(GetRecipeValue("RSet", "Com")))
            {
                TextDataShow("Connection Successful", R_PsText, true);
                return;
            }
            TextDataShow("Connection Failure", R_PsText, false);
        }

        private void B_PsDisConnect_Click(object sender, EventArgs e)
        {
            readCOM.DisconnectCom();
            TextDataShow("Disconnect", R_PsText, true);
        }

        private void B_PSRead_Click(object sender, EventArgs e)
        {
            if (!readCOM.ConnectionState)
            {
                TextDataShow("Device not connected", R_PsText, false);
                return;
            }
            DataGridView D_dgv = D_Ps;
            if (D_dgv.CurrentRow != null)
            {
                TextDataShow(readCOM.ReadContent_Work(D_dgv.CurrentRow.Cells[1].Value.ToString()), R_PsText, true);
            }
            else
            {
                TextDataShow("faied, datasheet have no data", R_PsText, false);
            }
        }
        #endregion
    }
}
