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
using System.Threading;
using JabilSDK.Enums;
using JabilSDK.Forms;
using AlphaRap.Classes;
using System.Security.Cryptography;
using System.Security.Claims;
using static DPFP.Verification.Verification;
using System.IO;
using Cognex.VisionPro;

namespace Acura3._0.ModuleForms
{
    public partial class MyForm : ModuleBaseForm
    {
        #region variable 
        public DateTime CTStartTime;
        public DateTime TaktStartTime;
        public ScrewData screwData;
        public PressureData pressureData;
        public DateTime screwStartTime;
        public AlarmClass.AlarmDataClass AlarmContent = new AlarmClass.AlarmDataClass();

        public bool B_ThrowScrewtestFlow = false;
        public bool B_PosFlow = false;
        public bool B_ScrewFasten = false;
        public bool B_Press = false;
        public bool B_ScrewVision = false;
        public bool B_ScrewRequest = false;
        public bool B_ThrowScrew = false;
        public bool B_GetScrewData = false;
        public int I_NGCount = 1;
        public int I_ScrewCount = 1;
        public int I_VisionCount = 1;
        public int I_9VisionCount = 1;
        public int I_ThrowScrewtestCount = 1;
        public int I_ThrowScrewtestOkCount = 0;
        public int I_ThrowScrewtestNgCount = 0;

        public bool SafetyPos = false;
        public bool B_ScrewRequestResult = true;

        public bool B_GantryResult = false;

        public double D_DifferenceValue = 0;

        /// <summary>
        /// Auto Flow Time
        /// </summary>
        public JTimer J_AxisAutoTm = new JTimer();
        /// <summary>
        /// Initial Flow Time
        /// </summary>
        public JTimer J_AxisIniTm = new JTimer();
        /// <summary>
        /// Request Screw Flow Time
        /// </summary>
        public JTimer J_ScrewAutoTm = new JTimer();

        /// <summary>
        /// Auto Calibration Time
        /// </summary>
        JTimer J_Auto9pointTm = new JTimer();

        /// <summary>
        /// ThrowScrew Test Time
        /// </summary>
        JTimer J_ThrowScrewtestTm = new JTimer();

        #endregion

        #region MachineSetting

        public bool B_DryRun { get => MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun"); }

        public bool B_GantryDryRun { get => MiddleLayer.SystemF.GetSettingValue("PSet", "GantryDryrun1"); }

        public bool B_ByPassScrew { get => GetSettingValue("PSet", "ByPassScrew"); }

        public bool B_ByPassVision { get => GetSettingValue("PSet", "ByPassVision"); }

        public bool B_ByPassPressure { get => GetSettingValue("PSet", "ByPassPressure"); }

        public bool B_ByPassDisplacement { get => GetSettingValue("PSet", "ByPassDisplacement"); }
        #endregion

        public MyForm()
        {
            InitializeComponent();
            MessageForm.MuteRaise += MessageForm_MuteRaise;
            FlowChartMessage.PauseRaise += FlowChartMessage_PauseRaise;
            FlowChartMessage.ResetTimerRaise += FlowChartMessage_ResetRaise;
            SetDoubleBuffer(plProductionSetting);
            Thread PFAlivethread = new Thread(KeepAlive);
            PFAlivethread.IsBackground = true;
            PFAlivethread.Start();
            #region 螺丝曲线图控件1
            //ScrewData1.BackColor = Color.WhiteSmoke;
            //ScrewData1.BackGradientStyle = GradientStyle.TopBottom;
            //ScrewData1.BackSecondaryColor = Color.White;
            //ScrewData1.BorderlineColor = Color.FromArgb(26, 59, 105);
            //ScrewData1.BorderlineDashStyle = ChartDashStyle.Solid;
            //ScrewData1.BorderlineWidth = 2;
            //ScrewData1.BorderSkin.SkinStyle = BorderSkinStyle.None;

            //ChartArea chartArea = new ChartArea("TighteningCurves");
            //chartArea.BackColor = Color.Gainsboro;
            //chartArea.BackGradientStyle = GradientStyle.TopBottom;
            //chartArea.BackSecondaryColor = Color.White;
            //chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.BorderDashStyle = ChartDashStyle.Solid;
            //ScrewData1.ChartAreas.Add(chartArea);

            //Title title = new Title("Torque And Angle Curves");
            //title.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold);
            //title.ForeColor = Color.FromArgb(26, 59, 105);
            //title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            //title.ShadowOffset = 2;
            //ScrewData1.Titles.Add(title);

            //Legend legend = new Legend("Series");
            //legend.Alignment = StringAlignment.Far;
            //legend.LegendStyle = LegendStyle.Row;
            //legend.Docking = Docking.Bottom;
            //legend.BackColor = Color.Transparent;
            //legend.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //legend.IsTextAutoFit = false;
            //ScrewData1.Legends.Add(legend);

            //Series seriesTorque = new Series("Torque");
            //seriesTorque.ChartType = SeriesChartType.FastLine;
            //seriesTorque.ChartArea = "TighteningCurves";
            //seriesTorque.BorderColor = Color.FromArgb(224, 64, 10);
            //seriesTorque.ShadowColor = Color.Black;
            //seriesTorque.BorderWidth = 2;
            //seriesTorque.XValueType = ChartValueType.Time;
            //seriesTorque.YValueType = ChartValueType.Double;
            //seriesTorque.YAxisType = AxisType.Primary;
            //seriesTorque.XAxisType = AxisType.Primary;
            //ScrewData1.Series.Add(seriesTorque);

            //Series seriesAngle = new Series("Angle");
            //seriesAngle.ChartType = SeriesChartType.FastLine;
            //seriesAngle.ChartArea = "TighteningCurves";
            //seriesAngle.BorderColor = Color.FromArgb(180, 26, 59, 105);
            //seriesAngle.ShadowColor = Color.Black;
            //seriesAngle.BorderWidth = 2;
            //seriesAngle.XValueType = ChartValueType.Time;
            //seriesAngle.YValueType = ChartValueType.Double;
            //seriesAngle.YAxisType = AxisType.Secondary;
            //seriesAngle.XAxisType = AxisType.Primary;
            //ScrewData1.Series.Add(seriesAngle);

            //chartArea.AxisX.Title = "Time";
            //chartArea.AxisX.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisX.LabelStyle.Format = "s.f";
            //chartArea.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisX.LineWidth = 2;
            //chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisX.ScrollBar.LineColor = Color.Black;
            //chartArea.AxisX.ScrollBar.Size = 10;

            //chartArea.AxisY.Title = "Torque";
            //chartArea.AxisY.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisY.IsLabelAutoFit = false;
            //chartArea.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisY.LineWidth = 2;
            //chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisY.ScrollBar.LineColor = Color.Black;
            //chartArea.AxisY.ScrollBar.Size = 10;

            //chartArea.AxisY2.Title = "Angle";
            //chartArea.AxisY2.TitleFont = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisY2.IsLabelAutoFit = false;
            //chartArea.AxisY2.LabelStyle.Font = new Font("Trebuchet MS", 8.25F, FontStyle.Bold);
            //chartArea.AxisY2.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisY2.LineWidth = 2;
            //chartArea.AxisY2.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            //chartArea.AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //chartArea.AxisY2.ScrollBar.LineColor = Color.Black;
            //chartArea.AxisY2.ScrollBar.Size = 10;
            #endregion

            GetSerialPort(C_Com);
        }

        #region Override Method

        public override void AlwaysRun()
        {
            if (IB_ScrewFeeder_LackMaterial.IsOn())
            {
                JSDK.Alarm.Show("3043");
            }
        }

        public override void InitialReset()
        {
            MTR_X.HomeReset();
            MTR_Y.HomeReset();
            MTR_Z.HomeReset();
            MTR_Jacking.HomeReset();
            flowChart0_1.TaskReset();
        }

        public override void Initial()
        {
            flowChart0_1.TaskRun();
        }

        public override void RunReset()
        {
            flowChart1_1.TaskReset();
            flowChart2_1.TaskReset();
            flowChart3_1.TaskReset();
            flowChart4_1.TaskReset();
            flowChart5_1.TaskReset();
            flowChart6_1.TaskReset();
            flowChart7_1.TaskReset();
            CTStartTime = DateTime.Now;
            TaktStartTime = DateTime.Now;
        }

        public override void Run()
        {
            flowChart1_1.TaskRun();
            flowChart2_1.TaskRun();
            flowChart3_1.TaskRun();
            flowChart4_1.TaskRun();
            flowChart5_1.TaskRun();
            flowChart6_1.TaskRun();
            flowChart7_1.TaskRun();
        }

        public override void StartRun()
        {
            RunTM.Restart();
        }

        public override void StopRun()
        {
            MTR_X.Stop();
            MTR_Y.Stop();
            MTR_Z.Stop();
            MTR_Jacking.Stop();
        }

        public override void ServoOn()
        {
            MTR_X.ServoOn();
            MTR_Y.ServoOn();
            MTR_Z.ServoOn();
            MTR_Jacking.ServoOn();

        }

        public override void ServoOff()
        {
            MTR_X.ServoOff();
            MTR_Y.ServoOff();
            MTR_Z.ServoOff();
            MTR_Jacking.ServoOff();
        }

        public override void SetSpeed(int SpeedRatio)
        {
            SetAxisSpeed();
        }

        #endregion

        #region UI

        private void btn_Gantry1Axis_Click(object sender, EventArgs e)
        {
            MotorControlForm MCF = new MotorControlForm();
            MCF.Initial(sender);
            MCF.ShowDialog();
        }


        private void T_CurrPos_Tick(object sender, EventArgs e)
        {
            if (plMotorControl.Visible)
            {
                T_McPosX.Text = MTR_X.GetCommandPosition().ToString("F3");
                T_McPosY.Text = MTR_Y.GetCommandPosition().ToString("F3");
                T_McPosZ.Text = MTR_Z.GetCommandPosition().ToString("F3");
                T_McPosJacking.Text = MTR_Jacking.GetCommandPosition().ToString("F3");
            }
        }
        #endregion

        #region Function
        public bool CalculateDisplacemeng(double value, double Currvalue)
        {
            double StandardValue = value + GetRecipeValue("RSet", "DisplacementStandard");
            if (StandardValue > GetRecipeValue("RSet", "DisplacementMax") || StandardValue < GetRecipeValue("RSet", "DisplacementMin"))
            {
                return false;
            }
            return true;
        }
        public static void SaveLog(string Textshow)
        {
            string sFileNamePerson = "C://Log//Gantry1" +
                    DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
                    DateTime.Now.Day.ToString("00");
            string strFileNamePerson = sFileNamePerson + "\\" + DateTime.Now.ToString("D") + ".txt";
            if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
            {
                Directory.CreateDirectory(sFileNamePerson);
            }
            string sAppend = DateTime.Now.ToString() + " " + Textshow + Environment.NewLine;
            File.AppendAllText(strFileNamePerson, sAppend);
        }

        #region //视觉显示

        /// <summary>
        /// 触发工位1视觉后显示到Display
        /// </summary>
        /// <param name="cogrecorddisplay">加载的视觉界面</param>
        /// <param name="icogrecord">输出给软件的图</param>
        public void VisionShow(CogRecordDisplay cogrecorddisplay, ICogRecord icogrecord)
        {
            RefreshDifferentThreadUI(cogrecorddisplay, () =>
            {
                cogrecorddisplay.Record = icogrecord;
                cogrecorddisplay.AutoFit = true;
                cogrecorddisplay.Fit();
            });
        }

        #endregion
        public void KeepAlive()
        {
            while (true)
            {
                if (PFClient.Controller != null && PFClient.Controller._client != null && !PFClient.Controller._client.Connected)
                {
                    PFClient.Disconnect();
                    PFClient.IP = GetRecipeValue("RSet", "ScrewIP");
                    PFClient.Port = GetRecipeValue("RSet", "ScrewPort");
                    BConnect();
                }
                if (PFClient.Controller != null && PFClient.Controller._client != null && PFClient.Controller._client.Connected && PFClient.KeepAliveTick > 7000)
                {
                    PFKeepAlive();
                    PFClient.CommunicationAlive();
                }
                Thread.Sleep(1000);
            }

        }

        public void GnatryAlarm(string AlarmCode, FlowChartMessage flow)
        {
            try
            {
                JSDK.Alarm.Show(AlarmCode);
                if (flow != null && JSDK.Alarm.IsExistInSummary(AlarmCode, ref AlarmContent))
                {
                    flow.Title = AlarmContent.Content;
                    flow.Content = AlarmCode + "-" + AlarmContent.Content;
                }

            }
            catch (Exception ex)
            {
            }

        }
        public bool DelayMs(int delayMilliseconds)
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
        public struct ScrewData
        {
            public int screwName;
            public double finalTorque;
            public double Angle;
            public double cycleTime;
            public int NumbleOfTurns;
            public double DisplacementValue;
            public bool State;
        }
        //写入螺丝数据
        public void WriteScrewData(ScrewData screwData)
        {
            RefreshDifferentThreadUI(D_ScrewResultsShow, () =>
            {
                if (screwData.screwName == 1)
                {
                    D_ScrewResultsShow.Rows.Clear();
                }
                int index = D_ScrewResultsShow.Rows.Add();
                D_ScrewResultsShow.ClearSelection();
                D_ScrewResultsShow.Rows[index].Cells[0].Value = screwData.screwName.ToString();
                D_ScrewResultsShow.Rows[index].Cells[1].Value = screwData.finalTorque.ToString("f3");
                D_ScrewResultsShow.Rows[index].Cells[2].Value = screwData.Angle.ToString("f3");
                D_ScrewResultsShow.Rows[index].Cells[3].Value = screwData.cycleTime.ToString();
                D_ScrewResultsShow.Rows[index].Cells[4].Value = screwData.NumbleOfTurns.ToString();
                D_ScrewResultsShow.Rows[index].Cells[5].Value = screwData.DisplacementValue.ToString();
                D_ScrewResultsShow.Rows[index].Cells[6].Value = screwData.State ? "OK" : "NG";
                if (screwData.State) { D_ScrewResultsShow.Rows[index].DefaultCellStyle.BackColor = Color.Green; }
                else { D_ScrewResultsShow.Rows[index].DefaultCellStyle.BackColor = Color.Red; }
                D_ScrewResultsShow.Rows[index].Selected = true;
                D_ScrewResultsShow.FirstDisplayedScrollingRowIndex = index;
            });
        }

        //保存螺丝数据
        //public void SaveCSV(string filePath, ScrewData screwData, string header)
        //{
        //    try
        //    {
        //        string folderPath = filePath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
        //        string str = string.Empty;
        //        //string folderPath = Path.GetDirectoryName(filePath);
        //        if (Directory.Exists(filePath) == false)
        //        {
        //            Directory.CreateDirectory(filePath);
        //        }
        //        if (!File.Exists(folderPath))
        //        {
        //            if (header != "")
        //            {
        //                //str = "" + "\r\n" + header + "\r\n" + "max" + "\r\n" + "min" + "\r\n";
        //                str = header + "\r\n";
        //            }
        //        }
        //        str += screwData.Time.ToString() + "," + screwData.MatCode.ToString() + "," + screwData.ScrewDirver.ToString() + "," +
        //               screwData.PointIndex.ToString() + "," + screwData.Torque.ToString("f3") + "," + screwData.Angle.ToString("f3") + "," +
        //               screwData.Result.ToString();

        //        //按日期存储
        //        //DateTime dt = DateTime.Now;
        //        //string str_Date = DateTime.Now.ToString();
        //        //dataStr = str_Date +"," + dataStr;
        //        StreamWriter sw = new StreamWriter(folderPath, true, Encoding.UTF8);
        //        sw.WriteLine(str);
        //        //sw.Write(str);
        //        sw.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}


        //public static void RefreshDifferentThreadUI(Control control, Action action)
        //{
        //    if (control.InvokeRequired)
        //    {
        //        Action refreshUI = new Action(action);
        //        control.Invoke(refreshUI);
        //    }
        //    else
        //    {
        //        action.Invoke();
        //    }
        //}

        public struct PressureData
        {
            public int SensorNum;
            public double PressureLimit;
            public double CurPressureValue;
            public string Time;
            public bool state;
        }
        //写入压力数据
        public void WritepressureData(PressureData pressureData)
        {
            RefreshDifferentThreadUI(D_PressureResultsShow, () =>
            {
                int index = D_PressureResultsShow.Rows.Add();
                D_PressureResultsShow.ClearSelection();
                D_PressureResultsShow.Rows[index].Cells[0].Value = pressureData.SensorNum.ToString();
                D_PressureResultsShow.Rows[index].Cells[1].Value = pressureData.Time;
                D_PressureResultsShow.Rows[index].Cells[2].Value = pressureData.PressureLimit.ToString("f3");
                D_PressureResultsShow.Rows[index].Cells[3].Value = pressureData.CurPressureValue.ToString("f3");
                if (pressureData.state) { D_PressureResultsShow.Rows[index].DefaultCellStyle.BackColor = Color.Green; }
                else { D_PressureResultsShow.Rows[index].DefaultCellStyle.BackColor = Color.Red; }
                D_PressureResultsShow.Rows[index].Selected = true;
                D_PressureResultsShow.FirstDisplayedScrollingRowIndex = index;
            });
        }

        public void SetAxisSpeed()
        {
            MTR_X.WorkSpeed = GetSettingValue("MSet", "WorkSpeedX");
            MTR_Y.WorkSpeed = GetSettingValue("MSet", "WorkSpeedY");
            MTR_Z.WorkSpeed = GetSettingValue("MSet", "WorkSpeedZ");
            MTR_Jacking.WorkSpeed = GetSettingValue("MSet", "WorkSpeedAxisJacking");

            MTR_X.Acceleration = GetSettingValue("MSet", "AccelerationX");
            MTR_Y.Acceleration = GetSettingValue("MSet", "AccelerationY");
            MTR_Z.Acceleration = GetSettingValue("MSet", "AccelerationZ");
            MTR_Jacking.Acceleration = GetSettingValue("MSet", "AccelerationAxisJacking");

            MTR_X.Deceleration = GetSettingValue("MSet", "DecelerationX");
            MTR_Y.Deceleration = GetSettingValue("MSet", "DecelerationY");
            MTR_Z.Deceleration = GetSettingValue("MSet", "DecelerationZ");
            MTR_Jacking.Deceleration = GetSettingValue("MSet", "DecelerationAxisJacking");

            MTR_X.SpeedRatio = GetSettingValue("MSet", "AxisSpeedRatio");
            MTR_Y.SpeedRatio = GetSettingValue("MSet", "AxisSpeedRatio");
            MTR_Z.SpeedRatio = GetSettingValue("MSet", "AxisSpeedRatio");
            MTR_Jacking.SpeedRatio = GetSettingValue("MSet", "AxisSpeedRatio");
        }

        public void InitialStatus()
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            OB_ScrewStart.Off();
            OB_ScrewStop.Off();
            OB_ScrewProgram_1.Off();
            OB_ScrewProgram_2.Off();
            OB_ScrewFeeder_SplitNail.Off();
            OB_ScrewFeeder_Reset.Off();
            OB_ScrewFeeder_CoverOpen.Off();
            OB_CCDLight.Off();
            OB_ModuleAlram_Light.Off();

            OB_ScrewArrived1.Off();
            B_ScrewFasten = false;
            B_Press = false;
            B_ScrewVision = false;
            B_ScrewRequest = false;
            B_ThrowScrew = false;
            B_GetScrewData = false;
            I_NGCount = 1;
            I_ScrewCount = 1;
            I_VisionCount = 1;
        }


        #endregion

        #region Local Method
        private void MessageForm_MuteRaise(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.BuzzOff();
        }

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
        public EthernetPF PFClient = new EthernetPF();
        public bool bSubscribe;
        public bool bComStart;
        public bool bConnect;
        /// <summary>
        /// 电批连接方法
        /// </summary>
        public void BConnect()
        {
            bConnect = PFClient.Connect();
            bComStart = PFClient.StartCommunication();
            bSubscribe = PFClient.SubscribeLastTighteningResult();
            PFKeepAlive();
            PFClient.CommunicationAlive();
        }

        public bool PFKeepAlive()
        {
            string Response = PFClient.SendAndWaitForResponse(MID.M9999, TimeSpan.FromSeconds(5));

            if (Response == null)
            {
                return false;
            }
            if (Response.Contains("00209999"))//00209999
            {
                PFClient.CommunicationAlive();
                return true;
            }
            PFClient.CommunicationAlive();
            return false;
        }
        private void btnConnectScrew_Click(object sender, EventArgs e)
        {
            PFClient.IP = GetRecipeValue("RSet", "ScrewIP");
            PFClient.Port = GetRecipeValue("RSet", "ScrewPort");
            BConnect();
            //bConnect = true;
            //bComStart = true;
            //bSubscribe = true;
            if (bConnect && bComStart && bSubscribe)
            {
                btnConnectScrew.BackColor = Color.Green;
            }
            else
            {
                btnConnectScrew.BackColor = Color.Red;
            }
            //ScrewDriver.Atlas_Connect(GetRecipeValue("RSet", "ScrewIP"), GetRecipeValue("RSet", "ScrewPort"));
            //if (ScrewDriver.bConnectStatus)
            //{
            //    btnConnectScrew.BackColor = Color.Green;
            //}
            //else
            //{
            //    btnConnectScrew.BackColor = Color.Red;
            //}
        }

        private void btnDisScrew_Click(object sender, EventArgs e)
        {
            PFClient.Disconnect();
            bConnect = false;
            btnConnectScrew.BackColor = Color.White;
            //ScrewDriver.Disconnect();
            //btnConnectScrew.BackColor = Color.Transparent;
        }

        private void btn_ReadScrewData_Click(object sender, EventArgs e)
        {
            PFClient.SubscribeLastTighteningResult();
            T_ScrewData.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 结果:" + PFClient.LastTighteningResult.TIGHTENING_STATUS.ToString() + " 扭力:" + PFClient.LastTighteningResult.TORQUE.ToString() + " 角度:" + PFClient.LastTighteningResult.ANGLE.ToString() + "\r\n");
            //Task.Factory.StartNew(() =>
            //{
            //    AtlasLibrary1._Result result = ScrewDriver.Atlas_ReadTighteningResult();
            //    RefreshDifferentThreadUI(T_ScrewData, () =>
            //    {
            //        T_ScrewData.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 结果:" + result.TotalStatus.ToString() + " 扭力:" + result.PeekTorque.ToString() + " 角度:" + result.TotalAngle.ToString());
            //    });
            //    //T_ScrewData.Invoke(() =>
            //    //{
            //    //    T_ScrewData.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 结果:" + result.TotalStatus.ToString() + " 扭力:" + result.PeekTorque.ToString() + " 角度:" + result.TotalAngle.ToString());
            //    //});

            //});
        }



        #endregion

        #region Move
        /// <summary>
        /// 自动移动
        /// </summary>
        /// <param name="XPost">X点位</param>
        /// <param name="YPost">Y点位</param>
        /// <param name="ZPost">Z点位</param>
        /// <param name="ZSafety">Z安全点</param>
        /// <returns></returns>
        public bool B_GantryMoveL(double XPost, double YPost, double ZPost, double ZSafety)
        {
            bool InPosition = false;
            if (!SafetyPos)
            {
                if (MTR_Z.GetCommandPosition() - 0.05 <= ZSafety)
                {
                    bool a = MTR_X.Goto(XPost);
                    bool b = MTR_Y.Goto(YPost);
                    if (a && b)
                    {
                        SafetyPos = true;
                    }
                }
                else
                {
                    MTR_Z.Goto(ZSafety);
                }
            }
            else
            {
                if (MTR_Z.Goto(ZPost))
                {
                    SafetyPos = false;
                    InPosition = true;
                }
            }
            return InPosition;
        }
        #endregion

        #region JackingAxis

        /// <summary>
        /// Axis Pos Struct
        /// </summary>
        public struct JackingAxisPos
        {
            public string Index;
            public double Position;
            public string Remark;
        }
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
            D_dr[1] = MTR_Jacking.GetCommandPosition().ToString("F3");
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
                D_dr[1] = MTR_Jacking.GetCommandPosition().ToString("F3");
                // AxisY.GetCommandPosition().ToString("F3");
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
                SetAxisSpeed();
                JackingAxisPos axisPos = GetJackingPointList(D_dgv.CurrentRow.Cells[2].Value.ToString());
                B_AxisManualMove = false;
                while (!B_AxisManualMove)
                {
                    if (MTR_Jacking.Goto(axisPos.Position))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        MessageBox.Show("Move Finsh", "", MessageBoxButtons.OK);
                        //LogShow(SysPara.UserName + "  " + "X:" + axisPos.x + " " + "Y:" + axisPos.y + " " + "Z:" + axisPos.z, true);
                    }
                    if (J_AxisAutoTm.IsOn(50000))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        //LogShow(SysPara.UserName + "  " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], false);
                        JSDK.Alarm.Show("0205");
                    }
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
            }
        }

        // Relevant point data
        public static List<JackingAxisPos> JackingAxisPosData = new List<JackingAxisPos>();


        /// <summary>
        /// Get data for a point
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns></returns>
        public JackingAxisPos GetJackingPointList(string pointName)
        {
            LoadJackingPointList();
            JackingAxisPos modelHPostData = new JackingAxisPos();
            for (int i = 0; i < JackingAxisPosData.Count; i++)
            {
                if (JackingAxisPosData[i].Remark == pointName)
                {
                    modelHPostData = JackingAxisPosData[i];
                }
            }
            return modelHPostData;
        }


        /// <summary>
        /// Data in a table in RecipeData
        /// </summary>
        public void LoadJackingPointList()
        {
            JackingAxisPosData.Clear();
            System.Data.DataTable H1dt = RecipeData.Tables["T_JackingShaft"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = H1dt.Rows[i];
                JackingAxisPos ToSolderPoint = new JackingAxisPos
                {

                    Index = dr["Index"].ToString(),
                    Position = Convert.ToDouble(dr["AxisJacking"]),
                    Remark = dr["Annotation"].ToString()
                };
                JackingAxisPosData.Add(ToSolderPoint);
            }
        }

        #endregion

        #region  PressureSensor
        ReadCOM readCOM = new ReadCOM();
        public List<PsData> L_FlipPsData = new List<PsData>();

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

        public void LoadHPsList()
        {
            L_FlipPsData.Clear();
            System.Data.DataTable H1dt = RecipeData.Tables["T_PressureSensor"];
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

        private void B_PsClear_Click(object sender, EventArgs e)
        {
            R_PsText.Clear();
        }
        #endregion

        #region ScrewPos

        public static List<ScrewAxisPos> AxisPosList = new List<ScrewAxisPos>();

        /// <summary>
        /// Axis Pos Struct
        /// </summary>
        public struct ScrewAxisPos
        {
            public double x;
            public double y;
            public double z;
            public string Annotation;
        }

        /// <summary>
        /// Get data for a point
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns></returns>
        public ScrewAxisPos GetScrewPointList(string pointName)
        {
            LoadScrewPointList();
            ScrewAxisPos modelHPostData = new ScrewAxisPos();
            for (int i = 0; i < AxisPosList.Count; i++)
            {
                if (AxisPosList[i].Annotation == pointName)
                {
                    modelHPostData = AxisPosList[i];
                }
            }
            return modelHPostData;
        }

        /// <summary>
        /// Data in a table in RecipeData
        /// </summary>
        public void LoadScrewPointList()
        {
            AxisPosList.Clear();
            System.Data.DataTable H1dt = RecipeData.Tables["T_ScrewPos"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = H1dt.Rows[i];
                ScrewAxisPos ToSolderPoint = new ScrewAxisPos
                {

                    x = Convert.ToDouble(dr["AxisX"]),
                    y = Convert.ToDouble(dr["AxisY"]),
                    z = Convert.ToDouble(dr["AxisZ"]),
                    Annotation = dr["Annotation"].ToString()
                };
                AxisPosList.Add(ToSolderPoint);
            }
        }

        /// <summary>
        /// Manual Move Axis bool
        /// </summary>
        public bool B_AxisManualMove = false;
        private void btnAddScrewPos_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Add Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataTable D_dt = RecipeData.Tables["T_ScrewPos"];
            int I_Rows = D_dt.Rows.Count;
            DataRow D_dr = D_dt.NewRow();
            D_dr[0] = (I_Rows + 1);
            D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
            D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
            D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
            D_dt.Rows.Add(D_dr);
            //LogShow(SysPara.UserName + "  " + "Add screw points the number" + D_dr[0] + "point" + "X:" + D_dr[1] + "  " + "Y:" + D_dr[2] + "  " + "Z:" + D_dr[3], true);
        }

        private void btnInsertScrewPos_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Insert Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_ScrewPos"];
                int I_Rows = D_dt.Rows.Count;
                DataRow D_dr = D_dt.NewRow();
                D_dr[0] = (I_Rows + 1);
                D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
                D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
                D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
                D_dt.Rows.InsertAt(D_dr, D_dgv.CurrentRow.Index);
                //LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " insert point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                //LogShow(SysPara.UserName + "  " + "insert faied, datasheet have no data", false);
            }
        }

        private void btnDeleteScrewPos_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure delete Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_ScrewPos"];
                D_dt.Rows.RemoveAt(D_dgv.CurrentRow.Index);
                // LogShow(SysPara.UserName + "  " + "delete stress point", true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                // LogShow(SysPara.UserName + "  " + "delete faied, datasheet have no data", false);
            }
        }

        private void btnRepalceScrewPos_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Replace Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_ScrewPos"];
                DataRow D_dr = D_dt.Rows[D_dgv.CurrentRow.Index];
                D_dr[0] = D_dr[0];
                D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
                D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
                D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
                // LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " replace point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                // LogShow(SysPara.UserName + "  " + "replace faied, datasheet have no data", false);
            }
        }

        private void btnGotoScrewPos_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Confirm to move to the selected point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_StressScrewPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                SetAxisSpeed();
                ScrewAxisPos axisPos = GetScrewPointList(D_dgv.CurrentRow.Cells[4].Value.ToString());
                ScrewAxisPos SafeaxisPos = GetScrewPointList("SafePos");
                B_AxisManualMove = false;
                SafetyPos = false;
                J_AxisAutoTm.Restart();
                while (!B_AxisManualMove)
                {
                    if (B_GantryMoveL(axisPos.x, axisPos.y, axisPos.z, SafeaxisPos.z))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        MessageBox.Show("Move Finsh", "", MessageBoxButtons.OK);
                        //LogShow(SysPara.UserName + "  " + "X:" + axisPos.x + " " + "Y:" + axisPos.y + " " + "Z:" + axisPos.z, true);
                    }
                    if (J_AxisAutoTm.IsOn(50000))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        //LogShow(SysPara.UserName + "  " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], false);
                        JSDK.Alarm.Show("0205");
                    }
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
            }
        }
        #endregion

        #region 9point
        public static List<ScrewAxisPos> Axis9Pos = new List<ScrewAxisPos>();

        /// <summary>
        /// Get data for a point
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns></returns>
        public ScrewAxisPos Get9PointList(string pointName)
        {
            Load9PointList();
            ScrewAxisPos modelHPostData = new ScrewAxisPos();
            for (int i = 0; i < Axis9Pos.Count; i++)
            {
                if (Axis9Pos[i].Annotation == pointName)
                {
                    modelHPostData = Axis9Pos[i];
                }
            }
            return modelHPostData;
        }


        /// <summary>
        /// Data in a table in RecipeData
        /// </summary>
        public void Load9PointList()
        {
            Axis9Pos.Clear();
            System.Data.DataTable H1dt = RecipeData.Tables["T_AutoCalibration"];
            for (int i = 0; i < H1dt.Rows.Count; i++)
            {
                System.Data.DataRow dr = H1dt.Rows[i];
                ScrewAxisPos ToSolderPoint = new ScrewAxisPos
                {
                    x = Convert.ToDouble(dr["AxisX"]),
                    y = Convert.ToDouble(dr["AxisY"]),
                    z = Convert.ToDouble(dr["AxisZ"]),
                    Annotation = dr["Annotation"].ToString()
                };
                Axis9Pos.Add(ToSolderPoint);
            }
        }

        private void btn_Add9Point_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Add Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataTable D_dt = RecipeData.Tables["T_AutoCalibration"];
            int I_Rows = D_dt.Rows.Count;
            DataRow D_dr = D_dt.NewRow();
            D_dr[0] = (I_Rows + 1);
            D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
            D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
            D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
            D_dt.Rows.Add(D_dr);

        }

        private void btn_Insert9Point_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Insert Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_9PointPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_AutoCalibration"];
                int I_Rows = D_dt.Rows.Count;
                DataRow D_dr = D_dt.NewRow();
                D_dr[0] = (I_Rows + 1);
                D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
                D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
                D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
                D_dt.Rows.InsertAt(D_dr, D_dgv.CurrentRow.Index);

            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");

            }
        }

        private void btn_Replace9Point_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure Replace Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            try
            {
                DataGridView D_dgv = D_9PointPos;
                if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
                {
                    DataTable D_dt = RecipeData.Tables["T_AutoCalibration"];
                    DataRow D_dr = D_dt.Rows[D_dgv.CurrentRow.Index];
                    D_dr[0] = D_dr[0];
                    D_dr[1] = MTR_X.GetCommandPosition().ToString("F3");
                    D_dr[2] = MTR_Y.GetCommandPosition().ToString("F3");
                    D_dr[3] = MTR_Z.GetCommandPosition().ToString("F3");
                    // LogShow(SysPara.UserName + "  " + "StressPos datagridview Row " + D_dr[0] + " replace point " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], true);
                }
                else
                {
                    MessageBox.Show("faied, datasheet have no data");
                    // LogShow(SysPara.UserName + "  " + "replace faied, datasheet have no data", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Goto9Point_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Confirm to move to the selected point?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_9PointPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                SetAxisSpeed();
                ScrewAxisPos axisPos = Get9PointList(D_dgv.CurrentRow.Cells[4].Value.ToString());
                ScrewAxisPos SafeaxisPos = GetScrewPointList("SafePos");
                B_AxisManualMove = false;
                SafetyPos = false;
                J_AxisAutoTm.Restart();
                while (!B_AxisManualMove)
                {
                    if (B_GantryMoveL(axisPos.x, axisPos.y, axisPos.z, SafeaxisPos.z))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        MessageBox.Show("Move Finsh", "", MessageBoxButtons.OK);
                        //LogShow(SysPara.UserName + "  " + "X:" + axisPos.x + " " + "Y:" + axisPos.y + " " + "Z:" + axisPos.z, true);
                    }
                    if (J_AxisAutoTm.IsOn(50000))
                    {
                        B_AxisManualMove = true;
                        J_AxisAutoTm.Restart();
                        //LogShow(SysPara.UserName + "  " + "X:" + D_dr[1] + " " + "Y:" + D_dr[2] + " " + "Z:" + D_dr[3], false);
                        JSDK.Alarm.Show("0205");
                    }
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
            }
        }

        private void btn_Delete9Point_Click(object sender, EventArgs e)
        {
            DialogResult reult = MessageBox.Show("Sure delete Points ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

            if (reult == DialogResult.No)
            {
                return;
            }
            DataGridView D_dgv = D_9PointPos;
            if ((D_dgv.CurrentRow != null) && (D_dgv.CurrentRow.Index >= 0))
            {
                DataTable D_dt = RecipeData.Tables["T_AutoCalibration"];
                D_dt.Rows.RemoveAt(D_dgv.CurrentRow.Index);
                // LogShow(SysPara.UserName + "  " + "delete stress point", true);
            }
            else
            {
                MessageBox.Show("faied, datasheet have no data");
                // LogShow(SysPara.UserName + "  " + "delete faied, datasheet have no data", false);
            }
        }
        #endregion

        #region Initial Flow
        private FCResultType flowChart0_1_FlowRun(object sender, EventArgs e)
        {
            J_AxisIniTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_1.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart0_18_FlowRun(object sender, EventArgs e)
        {
            PFClient.IP = GetRecipeValue("RSet", "ScrewIP");
            PFClient.Port = GetRecipeValue("RSet", "ScrewPort");
            BConnect();
            if (bConnect && bComStart && bSubscribe)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_18.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_18.Text} overtime", false);
                JSDK.Alarm.Show("3000");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_20_FlowRun(object sender, EventArgs e)
        {
            if (B_ByPassPressure)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_20.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (readCOM.ConnectionState || readCOM.ConnectCom(GetRecipeValue("RSet", "Com")))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_20.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_20.Text} overtime", false);
                JSDK.Alarm.Show("3002");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_21_FlowRun(object sender, EventArgs e)
        {
            if (SysPara.newReadCOM.SPCom.ConnectStates() || SysPara.newReadCOM.ConnectCom(GetRecipeValue("RSet", "DisplacementCom")))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_21.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_21.Text} overtime", false);
                JSDK.Alarm.Show("3004");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_2_FlowRun(object sender, EventArgs e)
        {
            if (MTR_Z.Home())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_2.Text} overtime", false);
                JSDK.Alarm.Show("3006");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_3_FlowRun(object sender, EventArgs e)
        {
            bool a = MTR_X.Home();
            bool b = MTR_Y.Home();
            if (a && b)
            {
                SafetyPos = false;
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_3.Text} overtime", false);
                JSDK.Alarm.Show("3008");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_4_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList($"ThrowScrewPos");
            ScrewAxisPos AxisZ = GetScrewPointList($"SafePos");

            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_4.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_4.Text} overtime", false);
                JSDK.Alarm.Show("3012");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_5_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewVacuum.Off();
            J_AxisIniTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_5.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart0_6_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_6.Text} overtime", false);
                JSDK.Alarm.Show("3059");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_16_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.On();
            if (IB_ScrewNailingCylinderExtend.IsOn() && IB_ScrewNailingCylinderRetract.IsOff())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_16.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_16.Text} overtime", false);
                JSDK.Alarm.Show("3062");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_7_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_7.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_7.Text} overtime", false);
                JSDK.Alarm.Show("3061");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_19_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_19.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_19.Text} overtime", false);
                JSDK.Alarm.Show("3059");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_13_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_13.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_13.Text} overtime", false);
                JSDK.Alarm.Show("3058");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_8_FlowRun(object sender, EventArgs e)
        {
            if (J_AxisIniTm.IsOn(1000))
            {
                CYL_Conveyor1_ScrewNailingCylinder.Off();
                if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
                {
                    SafetyPos = false;
                    J_AxisIniTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_8.Text} finish", true);
                    return FCResultType.NEXT;
                }
                if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
                {
                    J_AxisIniTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_8.Text} overtime", false);
                    JSDK.Alarm.Show("3063");
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_17_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_17.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_17.Text} overtime", false);
                JSDK.Alarm.Show("3058");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_9_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList($"VisionPos1");
            ScrewAxisPos AxisZ = GetScrewPointList($"SafePos");
            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_9.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_9.Text} overtime", false);
                JSDK.Alarm.Show("3012");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_14_FlowRun(object sender, EventArgs e)
        {
            if (MTR_Jacking.Home())
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_14.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_14.Text} overtime", false);
                JSDK.Alarm.Show("3010");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_15_FlowRun(object sender, EventArgs e)
        {
            JackingAxisPos AxisZ = GetJackingPointList("SafePos");
            bool bFinsh = MTR_Jacking.Goto(AxisZ.Position);
            if (bFinsh)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_15.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_15.Text} overtime", false);
                JSDK.Alarm.Show("3012");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_10_FlowRun(object sender, EventArgs e)
        {
            InitialStatus();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_10.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart0_11_FlowRun(object sender, EventArgs e)
        {
            bInitialOk = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_11.Text} finish", true);
            return FCResultType.IDLE;
        }

        private FCResultType flowChart0_12_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_12.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisIniTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisIniTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart0_12.Text} overtime", false);
                JSDK.Alarm.Show("3063");
            }
            return FCResultType.IDLE;
        }
        #endregion

        #region Main Flow
        private FCResultType flowChart1_1_FlowRun(object sender, EventArgs e)
        {
            if (MiddleLayer.ConveyorF.Station2Start)
            {
                MiddleLayer.SystemF.ShowStatus(2, SystemForm.WorkStatus.Processing);
                MiddleLayer.ConveyorF.Station2Start = false;
                B_Press = true;
                I_ScrewCount = 1;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_2_FlowRun(object sender, EventArgs e)
        {
            if (!B_Press)
            {
                if (!B_GantryResult)
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_2.Text} finish", true);
                    return FCResultType.CASE1;
                }
                B_ScrewVision = true;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_3_FlowRun(object sender, EventArgs e)
        {
            if (!B_ScrewVision)
            {
                if (!B_GantryResult)
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_2.Text} finish", true);
                    return FCResultType.CASE1;
                }
                B_ScrewFasten = true;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_4_FlowRun(object sender, EventArgs e)
        {
            if (!B_ScrewFasten)
            {
                if (!B_GantryResult)
                {
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_2.Text} finish", true);
                    return FCResultType.CASE1;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_4.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart1_6_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_15.Text} finish", true);
            I_ScrewCount++;
            J_AxisAutoTm.Restart();
            if (I_ScrewCount > 8)
            {
                MiddleLayer.ConveyorF.myRFID2.ResultBool = true;
                MiddleLayer.SystemF.ShowStatus(2, SystemForm.WorkStatus.Pass);
                I_ScrewCount = 1;
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart1_5_FlowRun(object sender, EventArgs e)
        {
            T_UpdateStatus.Enabled = true;
            MiddleLayer.ConveyorF.MachineAvailable2 = true;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1_5.Text} finish", true);
            return FCResultType.NEXT;
        }
        private FCResultType flowChart54_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart54.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region Screw Flow
        private FCResultType flowChart2_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ScrewFasten)
            {
                I_NGCount = 1;
                B_GantryResult = true;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_2_FlowRun(object sender, EventArgs e)
        {
            if (I_ScrewCount != 1)
            {
                screwStartTime = DateTime.Now;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            B_ScrewRequest = true;
            screwStartTime = DateTime.Now;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} finish", true);
            return FCResultType.NEXT;
        }
        VppComp.Point3D point3D;
        private FCResultType flowChart2_17_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_17.Text} finish", true);
            return FCResultType.NEXT;
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            bool bFinsh = MTR_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                SafetyPos = false;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_17.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_17.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage4);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_3_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList($"ScrewPos{I_ScrewCount}");
            ScrewAxisPos AxisZ = GetScrewPointList($"SafePos");

            if (B_GantryMoveL(AxisXY.x + point3D.x, AxisXY.y + point3D.y, AxisXY.z, AxisZ.z))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_3.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage5);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_4_FlowRun(object sender, EventArgs e)
        {

            if (!B_ScrewRequest && B_ScrewRequestResult)
            {
                if (IB_ScrewVacuum.IsOff() && !B_DryRun)
                {
                    GnatryAlarm("3045", flowChartMessage1);
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_4.Text} overtime", false);
                    return FCResultType.CASE2;
                }
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_4.Text} finish", true);
                return FCResultType.NEXT;
            }
            else if (!B_ScrewRequest && !B_ScrewRequestResult)
            {
                I_NGCount++;
                if (I_NGCount > 3)
                {
                    I_NGCount = 1;
                    GnatryAlarm("3017", flowChartMessage1);
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_4.Text} overtime", false);
                    return FCResultType.CASE2;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_4.Text} overtime", false);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_20_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.On();
            if (IB_ScrewNailingCylinderExtend.IsOn() && IB_ScrewNailingCylinderRetract.IsOff())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_20.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_20.Text} overtime", false);
                JSDK.Alarm.Show("3056");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_6_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_6.Text} overtime", false);
                JSDK.Alarm.Show("3053");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_7_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewStart.Off();
            OB_ScrewStop.Off();
            OB_ScrewProgram_1.Off();
            OB_ScrewProgram_2.Off();
            DelayMs(300);
            OB_ScrewProgram_1.On();
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_7.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_8_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode :" + $"{this.Text} Module {flowChart2_8.Text} finish", true);
                return FCResultType.NEXT;
            }
            OB_ScrewStart.On();
            OB_ScrewProgram_1.On();
            J_AxisAutoTm.Restart();
            if (IB_Screw_Running.IsOn())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_8.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_8.Text} overtime", false);
                GnatryAlarm("3019", flowChartMessage2);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_10_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode :" + $"{this.Text} Module {flowChart2_10.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_Screw_OK.IsOn() || IB_Screw_NG.IsOn())
            {
                if (B_ByPassScrew)
                {
                    OB_ScrewStart.Off();
                    J_AxisAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "ByPass Screw Result Mode :" + $"{this.Text} Module {flowChart2_10.Text} finish", true);
                    return FCResultType.NEXT;
                }
                OB_ScrewStart.Off();
                if (IB_Screw_OK.IsOn())
                {
                    J_AxisAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_10.Text} finish", true);
                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_10.Text} screw result NG", false);
                JSDK.Alarm.Show("3021");
                J_AxisAutoTm.Restart();
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_11_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewVacuum.Off();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_11.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_12_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode :" + $"{this.Text} Module {flowChart2_12.Text} finish", true);
                return FCResultType.NEXT;
            }
            B_GetScrewData = true;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_12.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_22_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun || !B_GantryResult|| B_ByPassDisplacement)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode :" + $"{this.Text} Module {flowChart2_22.Text} finish", true);
                return FCResultType.NEXT;
            }
            string data = "";
            if (SysPara.newReadCOM.SPCom.ConnectStates())
            {
                data = SysPara.newReadCOM.ReadContent_COM("02 03 00 00 00 02 C4 38");
                if (data == "")
                    return FCResultType.IDLE;
                screwData.DisplacementValue = Convert.ToDouble(data);
                if ((screwData.DisplacementValue - GetRecipeValue("RSet", "DisplacementStandard")) < GetRecipeValue("RSet", "DisplacementMax")
                && (screwData.DisplacementValue - GetRecipeValue("RSet", "DisplacementStandard")) > GetRecipeValue("RSet", "DisplacementMin") || B_ByPassDisplacement)
                {
                    J_AxisAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_22.Text} finish", true);
                    return FCResultType.NEXT;
                }
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_22.Text} ---- Screw dispalcement height over limit", false);
                GnatryAlarm("3031", flowChartMessage17);
                return FCResultType.CASE1;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_22.Text} overtime", false);
                SysPara.newReadCOM.DisconnectCom3();
                SysPara.newReadCOM.ConnectCom(GetRecipeValue("RSet", "DisplacementCom"));
                GnatryAlarm("3029", flowChartMessage16);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_23_FlowRun(object sender, EventArgs e)
        {
            if (I_ScrewCount == 8 || !B_GantryResult)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            B_ScrewRequest = true;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_2.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart2_13_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_13.Text} finish", true);
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_13.Text} Screw Nailing Cylinder Retract overtime", false);
                JSDK.Alarm.Show("3057");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_21_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_21.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_21.Text} overtime", false);
                JSDK.Alarm.Show("3052");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_18_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                WriteScrewData(screwData);
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_18.Text} finish", true);
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_18.Text} overtime", false);
                JSDK.Alarm.Show("3055");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_14_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            bool bFinsh = MTR_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_14.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_14.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage6);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_15_FlowRun(object sender, EventArgs e)
        {
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_15.Text} finish", true);
            J_AxisAutoTm.Restart();
            if (I_ScrewCount > 7 || !B_GantryResult)
            {
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart2_19_FlowRun(object sender, EventArgs e)
        {
            JackingAxisPos AxisZ = GetJackingPointList("SafePos");
            bool bFinsh = MTR_Jacking.Goto(AxisZ.Position);
            if (bFinsh)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_19.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_19.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage7);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_16_FlowRun(object sender, EventArgs e)
        {
            B_ScrewFasten = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_16.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart43_FlowRun(object sender, EventArgs e)
        {
            B_ThrowScrew = true;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart43.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart44_FlowRun(object sender, EventArgs e)
        {
            if (!B_ThrowScrew)
            {
                B_ScrewRequest = true;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart44.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart2_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart1_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart1.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart12_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart12.Text} finish", true);
            return FCResultType.CASE1;
        }
        private FCResultType flowChart19_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart19.Text} finish", true);
            return FCResultType.CASE1;
        }

        private FCResultType flowChart6_FlowRun(object sender, EventArgs e)
        {
            B_GantryResult = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region Jacking Flow
        private FCResultType flowChart3_1_FlowRun(object sender, EventArgs e)
        {
            if (B_Press)
            {
                B_GantryResult = true;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_1.Text} finish", true);
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_2_FlowRun(object sender, EventArgs e)
        {
            JackingAxisPos AxisZ = GetJackingPointList("PreJackingPos");
            bool bFinsh = MTR_Jacking.Goto(AxisZ.Position);
            if (bFinsh)
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_2.Text} finish", true);
                MTR_Jacking.WorkSpeed = 1;
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_2.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage8);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_3_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_3.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_5_FlowRun(object sender, EventArgs e)
        {
            JackingAxisPos AxisZ = GetJackingPointList("JackingLimitPos");
            bool bFinsh = MTR_Jacking.Goto(AxisZ.Position);
            if ((!B_ByPassPressure) && !B_DryRun && !B_GantryDryRun)
            {
                if (readCOM.ConnectionState)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        dynamic Value = RecipeData.Tables["T_PressureSensor "].Rows[i]["Command", DataRowVersion.Original];
                        string data = readCOM.ReadContent_Work(Value.ToString());
                        if (double.TryParse(data, out double result))
                        {
                            if (Convert.ToDouble(data) > 50)
                            {
                                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_5.Text} overtime", false);
                                J_AxisAutoTm.Restart();
                                GnatryAlarm("3038", flowChartMessage15);
                                return FCResultType.CASE2;
                            }
                        }
                        else
                        {
                            GnatryAlarm("3075", flowChartMessage15);
                            readCOM.DisconnectCom();
                            readCOM.ConnectCom(GetRecipeValue("RSet", "Com"));
                            J_AxisAutoTm.Restart();
                            return FCResultType.CASE2;
                        }
                    }
                }
                else
                {
                    GnatryAlarm("3073", flowChartMessage19);
                    J_AxisAutoTm.Restart();
                    return FCResultType.CASE1;
                }
            }

            if (bFinsh)
            {
                if ((IB_PressureSensor1.IsOff() || IB_PressureSensor2.IsOff() || IB_PressureSensor3.IsOff() || IB_PressureSensor4.IsOff() || IB_PressureSensor5.IsOff()
    || IB_PressureSensor6.IsOff()) && !B_ByPassPressure && !B_DryRun && !B_GantryDryRun)
                {
                    GnatryAlarm("3023", flowChartMessage15);
                    J_AxisAutoTm.Restart();
                    return FCResultType.CASE2;
                }
                //JackingAxisPos AxisJacking = GetJackingPointList("JackingLimitPos");
                //D_DifferenceValue = AxisJacking.Position - MTR_Jacking.GetCommandPosition();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_5.Text} finish", true);
                //MTR_Jacking.Stop();
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_5.Text} overtime", false);
                J_AxisAutoTm.Restart();
                GnatryAlarm("3014", flowChartMessage15);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_6_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun||B_ByPassPressure)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (readCOM.ConnectionState)
            {
                for (int i = 0; i < 6; i++)
                {
                    dynamic Value = RecipeData.Tables["T_PressureSensor "].Rows[i]["Command", DataRowVersion.Original];
                    string data = readCOM.ReadContent_Work(Value.ToString());
                    pressureData.SensorNum = i + 1;
                    pressureData.PressureLimit = 10;
                    pressureData.CurPressureValue = Convert.ToDouble(data);
                    pressureData.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    switch (i)
                    {
                        case 0:
                            pressureData.state = IB_PressureSensor1.IsOn();
                            break;
                        case 1:
                            pressureData.state = IB_PressureSensor2.IsOn();
                            break;
                        case 2:
                            pressureData.state = IB_PressureSensor3.IsOn();
                            break;
                        case 3:
                            pressureData.state = IB_PressureSensor4.IsOn();
                            break;
                        case 4:
                            pressureData.state = IB_PressureSensor5.IsOn();
                            break;
                        case 5:
                            pressureData.state = IB_PressureSensor6.IsOn();
                            break;
                        default:
                            break;
                    }

                    WritepressureData(pressureData);
                }
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                readCOM.DisconnectCom();
                readCOM.ConnectCom(GetRecipeValue("RSet", "Com"));
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_6.Text} overtime", false);
                GnatryAlarm("3027", flowChartMessage9);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart3_4_FlowRun(object sender, EventArgs e)
        {
            SetAxisSpeed();
            B_Press = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3_4.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart4_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4.Text} finish", true);
            return FCResultType.CASE1;
        }
        private FCResultType flowChart21_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart21.Text} finish", true);
            return FCResultType.NEXT;
        }
        private FCResultType flowChart5_FlowRun(object sender, EventArgs e)
        {
            B_GantryResult = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region Vision Flow
        private FCResultType flowChart4_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ScrewVision)
            {
                B_GantryResult = true;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_7_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            bool bFinsh = MTR_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                SafetyPos = false;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_7.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_7.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage10);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_2_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList($"VisionPos{I_ScrewCount}");
            ScrewAxisPos AxisZ = GetScrewPointList($"SafePos");
            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_2.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage11);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_3_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun || B_ByPassVision)
            {
                OB_CCDLight.On();
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode : " + $"{this.Text} Module {flowChart4_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            bool B_OK = false;
            bool B_Ng = false;
            RefreshDifferentThreadUI(V_ScrewControl1, () =>
            {
                if (I_ScrewCount == 1)
                {
                    if (V_ScrewControl1.Snap(out ICogImage image))
                    {
                        B_OK = true;
                    }
                    else
                        B_Ng = true;
                }
                else
                {
                    if (V_ScrewControl2.Snap(out ICogImage image))
                    {
                        B_OK = true;
                    }
                    else
                        B_Ng = true;
                }


            });
            if (B_OK || B_Ng)
            {
                if (B_OK)
                {
                    J_AxisAutoTm.Restart();

                    return FCResultType.NEXT;
                }
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_3.Text} NG", false);
                GnatryAlarm("3035", flowChartMessage18);
                return FCResultType.CASE2;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_3.Text} overtime", false);
                GnatryAlarm("3035", flowChartMessage18);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }
        object PickMark1 = null;
        object PickMark2 = null;
        private FCResultType flowChart4_4_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun || B_ByPassVision)
            {
                point3D = new VppComp.Point3D { x = 0, y = 0 };
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + "Dryrun Mode : " + $"{this.Text} Module {flowChart4_4.Text} finish", true);
                OB_CCDLight.Off();
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            bool B_OK = false;
            bool B_Ng = false;
            bool B_Overvalue = false;
            RefreshDifferentThreadUI(V_ScrewControl1, () =>
            {
                CogRecordDisplay crd = MiddleLayer.RecordF.CogRecord_Gantry1;
                if (I_ScrewCount == 1)
                {

                    if (V_ScrewControl1.Run(out PickMark1, ref crd))
                    {
                        point3D = PickMark1 as VppComp.Point3D;
                        if (Math.Abs(point3D.x) > GetRecipeValue("RSet", "VisionMax") || Math.Abs(point3D.x) < GetRecipeValue("RSet", "VisionMin"))
                        {
                            GnatryAlarm("3037", flowChartMessage14);
                            B_Overvalue = true;
                        }
                        else
                            B_OK = true;
                    }
                    else
                    {
                        B_Ng = true;
                    }
                    MiddleLayer.RecordF.CogRecord_Gantry2 = crd;
                }
                else
                {

                    if (V_ScrewControl1.Run(out PickMark2, ref crd))
                    {
                        point3D = PickMark2 as VppComp.Point3D;
                        if (Math.Abs(point3D.y) > GetRecipeValue("RSet", "VisionMax") || Math.Abs(point3D.y) < GetRecipeValue("RSet", "VisionMin"))
                        {
                            GnatryAlarm("3037", flowChartMessage14);
                            B_Overvalue = true;
                        }
                        else
                            B_OK = true;
                    }
                    else
                    {
                        B_Ng = true;
                    }
                    MiddleLayer.RecordF.CogRecord_Gantry2 = crd;
                }
            });
            if (B_OK || B_Ng || B_Overvalue)
            {
                if (B_OK)
                {
                    J_AxisAutoTm.Restart();
                    return FCResultType.NEXT;
                }
                else if (B_Overvalue)
                {
                    J_AxisAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_4.Text} NG", false);
                    return FCResultType.CASE2;
                }
                GnatryAlarm("3035", flowChartMessage14);
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_4.Text} NG", false);
                return FCResultType.CASE2;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "Times")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_4.Text} overtime", false);
                GnatryAlarm("3035", flowChartMessage14);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart4_5_FlowRun(object sender, EventArgs e)
        {
            I_VisionCount++;
            if (I_VisionCount > 2)
            {
                I_VisionCount = 1;
                J_AxisAutoTm.Restart();
                //MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_5.Text} finish", true);
                return FCResultType.NEXT;
            }
            J_AxisAutoTm.Restart();
            return FCResultType.CASE1;
        }

        private FCResultType flowChart4_6_FlowRun(object sender, EventArgs e)
        {
            B_ScrewVision = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart4_6.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart3_FlowRun(object sender, EventArgs e)
        {
            B_GantryResult = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart3.Text} finish", true);
            return FCResultType.NEXT;
        }
        private FCResultType flowChart7_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart7.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart42_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart42.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region RequestScrew Flow
        private FCResultType flowChart5_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ScrewRequest)
            {
                OB_ScrewArrived1.Off();
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_2_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + " Dryrun Mode : " + $"{this.Text} Module {flowChart5_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_ScrewFeeder_Ready.IsOn())
            {
                OB_ScrewFeeder_SplitNail.On();
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_2.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage3);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_3_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                OB_ScrewArrived1.Off();
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + " Dryrun Mode : " + $"{this.Text} Module {flowChart5_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_ScrewArrived1.IsOn())
            {
                OB_ScrewVacuum.On();
                J_ScrewAutoTm.Restart();
                OB_ScrewArrived1.Off();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                JSDK.Alarm.Show("3049");
                J_ScrewAutoTm.Restart();
                OB_ScrewArrived1.Off();
                B_ScrewRequestResult = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_3.Text} overtime", false);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_4_FlowRun(object sender, EventArgs e)
        {
            if (J_ScrewAutoTm.IsOn(1500))
            {
                CYL_Conveyor1_ScrewFeedCylinder.On();
                if (IB_ScrewFeedCylinderExtend.IsOn() && IB_ScrewFeedCylinderRetract.IsOff())
                {
                    OB_ScrewVacuum.On();
                    J_ScrewAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_4.Text} finish", true);
                    return FCResultType.NEXT;
                }
                if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
                {
                    J_AxisAutoTm.Restart();
                    MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_4.Text} overtime", false);
                    JSDK.Alarm.Show("3054");
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_5_FlowRun(object sender, EventArgs e)
        {
            if (B_DryRun || B_GantryDryRun)
            {
                J_ScrewAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + " Dryrun Mode : " + $"{this.Text} Module {flowChart5_5.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (IB_ScrewVacuum.IsOn())
            {
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_5.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                JSDK.Alarm.Show("3047");
                B_ScrewRequestResult = false;
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_5.Text} overtime", false);
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart5_6_FlowRun(object sender, EventArgs e)
        {
            B_ScrewRequestResult = true;
            return FCResultType.NEXT;
        }

        private FCResultType flowChart5_7_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewArrived1.Off();
            OB_ScrewFeeder_SplitNail.Off();
            B_ScrewRequest = false;
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5_7.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart31_FlowRun(object sender, EventArgs e)
        {
            J_ScrewAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart31.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart55_FlowRun(object sender, EventArgs e)
        {
            J_ScrewAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart55.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region ThrowScrew Flow

        private FCResultType flowChart6_1_FlowRun(object sender, EventArgs e)
        {
            if (B_ThrowScrew)
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_1.Text} finish", true);
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_2_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            bool bFinsh = MTR_Z.Goto(AxisZ.z);
            if (bFinsh)
            {
                SafetyPos = false;
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_2.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_2.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage12);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_3_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList("ThrowScrewPos");
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_3.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_3.Text} overtime", false);
                GnatryAlarm("3015", flowChartMessage13);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_4_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewVacuum.Off();
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_4.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart6_5_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.On();
            if (IB_ScrewNailingCylinderExtend.IsOn() && IB_ScrewNailingCylinderRetract.IsOff())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_5.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_5.Text} overtime", false);
                JSDK.Alarm.Show("3056");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_8_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_8.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_8.Text} overtime", false);
                JSDK.Alarm.Show("3053");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_6_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_6.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_6.Text} overtime", false);
                JSDK.Alarm.Show("3057");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_9_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_9.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_9.Text} overtime", false);
                JSDK.Alarm.Show("3052");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_10_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_10.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_10.Text} overtime", false);
                JSDK.Alarm.Show("3055");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart6_7_FlowRun(object sender, EventArgs e)
        {
            B_ThrowScrew = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart6_7.Text} finish", true);
            return FCResultType.NEXT;
        }
        private FCResultType flowChart56_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart56.Text} finish", true);
            return FCResultType.NEXT;
        }
        #endregion

        #region Displacement
        private void btnConnectDisplacement_Click(object sender, EventArgs e)
        {
            if (SysPara.newReadCOM.SPCom.ConnectStates() || SysPara.newReadCOM.ConnectCom(GetRecipeValue("RSet", "DisplacementCom")))
            {
                //btnConnectDisplacement_Click(this, null);
                btnConnectDisplacement.BackColor = Color.Green;
            }
            else
                btnConnectDisplacement.BackColor = Color.Red;
        }

        private void btnDisConnectDisplacement_Click(object sender, EventArgs e)
        {
            SysPara.newReadCOM.DisconnectCom3();
            btnConnectDisplacement.BackColor = Color.Red;
        }

        private void btnReadData_Click(object sender, EventArgs e)
        {
            textBox37.Text = SysPara.newReadCOM.ReadContent_COM("02 03 00 00 00 02 C4 38");
        }


        #endregion

        #region Auto Calibration
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Start Auto Calibration?", "Auto Calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            B_PosFlow = true;
            flowChart100_1.TaskReset();
            Task.Factory.StartNew(() =>
            {
                while (B_PosFlow)
                {
                    flowChart100_1.TaskRun();
                    Thread.Sleep(0);
                    Application.DoEvents();
                }
            });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            B_PosFlow = false;
            StopRun();
        }
        private FCResultType flowChart100_1_FlowRun(object sender, EventArgs e)
        {
            J_Auto9pointTm.Restart();
            I_9VisionCount = 1;
            return FCResultType.NEXT;
        }

        private FCResultType flowChart100_2_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3055");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_3_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3053");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_4_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3057");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_5_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3052");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_6_FlowRun(object sender, EventArgs e)
        {
            if (MTR_Z.Home())
            {
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3006");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_7_FlowRun(object sender, EventArgs e)
        {
            bool a = MTR_X.Home();
            bool b = MTR_Y.Home();
            if (a && b)
            {
                SafetyPos = false;
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3008");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_8_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = Get9PointList($"CalibrationPos{I_9VisionCount}");
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");

            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                B_DelayMs(1000);
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_Auto9pointTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3012");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_9_FlowRun(object sender, EventArgs e)
        {
            bool B_OK = false;
            bool B_Ng = false;
            ScrewAxisPos AxisXY = Get9PointList($"CalibrationPos{I_9VisionCount}");
            RefreshDifferentThreadUI(C_9PosCcd1Show, () =>
            {
                if (H1_9Pos.SetCalibPoint(H1_9Pos.CameraSN, 1, I_9VisionCount, AxisXY.x, AxisXY.y, out ICogRecord cogRecord)/*&& H1_9Pos.SetCalibPoint(H1_9Pos.CameraSN, 2, I_9VisionCount, AxisXY.x, AxisXY.y, out ICogRecord cogRecord1)*/)
                {
                    VisionShow(C_9PosCcd1Show, cogRecord);
                    J_Auto9pointTm.Restart();
                    B_OK = true;
                }
                else
                    B_Ng = true;

            });
            if (B_OK || B_Ng)
            {
                if (B_OK)
                    return FCResultType.NEXT;
                JSDK.Alarm.Show("3032");
            }
            if (J_Auto9pointTm.IsOn(5000))
            {
                J_Auto9pointTm.Restart();
                JSDK.Alarm.Show("3032");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart100_10_FlowRun(object sender, EventArgs e)
        {
            J_Auto9pointTm.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart100_11_FlowRun(object sender, EventArgs e)
        {
            I_9VisionCount++;
            if (I_9VisionCount > 9)
            {
                I_9VisionCount = 1;
                J_Auto9pointTm.Restart();
                return FCResultType.NEXT;
            }
            J_Auto9pointTm.Restart();
            return FCResultType.CASE1;
        }

        private FCResultType flowChart100_12_FlowRun(object sender, EventArgs e)
        {
            B_PosFlow = false;
            StopRun();
            return FCResultType.IDLE;
        }

        private FCResultType flowChart23_FlowRun(object sender, EventArgs e)
        {
            J_Auto9pointTm.Restart();
            return FCResultType.NEXT;
        }
        #endregion

        #region GetScrewDataFlow
        private FCResultType flowChart7_1_FlowRun(object sender, EventArgs e)
        {
            if (B_GetScrewData)
            {
                B_GetScrewData = false;
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart7_2_FlowRun(object sender, EventArgs e)
        {
            if (PFClient.TighteningResultUpdated)
            {
                PFClient.TighteningResultUpdated = false;
                screwData.screwName = I_ScrewCount;
                screwData.finalTorque = PFClient.LastTighteningResult.TORQUE;
                screwData.Angle = PFClient.LastTighteningResult.ANGLE;
                screwData.cycleTime = (DateTime.Now - screwStartTime).TotalSeconds;
                screwData.NumbleOfTurns = 0;
                screwData.State = PFClient.LastTighteningResult.TIGHTENING_STATUS;
                J_AxisAutoTm.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart7_3_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart8_FlowRun(object sender, EventArgs e)
        {
            J_AxisAutoTm.Restart();
            return FCResultType.NEXT;
        }
        #endregion

        #region ThrowScrewSmallProgramFlow
        private FCResultType flowChart110_1_FlowRun(object sender, EventArgs e)
        {
            J_ThrowScrewtestTm.Restart();
            I_ThrowScrewtestCount = 1;
            I_ThrowScrewtestNgCount = 0;
            I_ThrowScrewtestOkCount = 0;


            return FCResultType.NEXT;
        }

        private FCResultType flowChart110_2_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3055");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_3_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3053");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_4_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3057");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_5_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3052");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_6_FlowRun(object sender, EventArgs e)
        {
            if (MTR_Z.Home())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3006");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_7_FlowRun(object sender, EventArgs e)
        {
            bool a = MTR_X.Home();
            bool b = MTR_Y.Home();
            if (a && b)
            {
                SafetyPos = false;
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IniAsixTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3008");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_8_FlowRun(object sender, EventArgs e)
        {
            if (IB_ScrewFeeder_Ready.IsOn())
            {
                OB_ScrewFeeder_SplitNail.On();
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_9_FlowRun(object sender, EventArgs e)
        {
            if (IB_ScrewArrived1.IsOn())
            {
                OB_ScrewVacuum.On();
                J_ThrowScrewtestTm.Restart();
                OB_ScrewArrived1.Off();
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                OB_ScrewArrived1.Off();
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_10_FlowRun(object sender, EventArgs e)
        {
            if (J_ThrowScrewtestTm.IsOn(500))
            {
                CYL_Conveyor1_ScrewFeedCylinder.On();
                if (IB_ScrewFeedCylinderExtend.IsOn() && IB_ScrewFeedCylinderRetract.IsOff())
                {
                    OB_ScrewVacuum.On();
                    J_ThrowScrewtestTm.Restart();
                    return FCResultType.NEXT;
                }
                if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
                {
                    J_ThrowScrewtestTm.Restart();
                }
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_11_FlowRun(object sender, EventArgs e)
        {
            if (IB_ScrewVacuum.IsOn())
            {
                I_ThrowScrewtestOkCount++;
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ScrewAutoTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.CASE1;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_12_FlowRun(object sender, EventArgs e)
        {
            ScrewAxisPos AxisXY = GetScrewPointList("ThrowScrewPos");
            ScrewAxisPos AxisZ = GetScrewPointList("SafePos");
            if (B_GantryMoveL(AxisXY.x, AxisXY.y, AxisXY.z, AxisZ.z))
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_ThrowScrewtestTm.Restart();
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_13_FlowRun(object sender, EventArgs e)
        {
            OB_ScrewVacuum.Off();
            J_ThrowScrewtestTm.Restart();
            return FCResultType.NEXT;
        }

        private FCResultType flowChart110_14_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.On();
            if (IB_ScrewNailingCylinderExtend.IsOn() && IB_ScrewNailingCylinderRetract.IsOff())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3056");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_15_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.Off();
            if (IB_ScrewLockCylinderExtend.IsOff() && IB_ScrewLockCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3053");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_16_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewNailingCylinder.Off();
            if (IB_ScrewNailingCylinderExtend.IsOff() && IB_ScrewNailingCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3057");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_17_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewLockCylinder.On();
            if (IB_ScrewLockCylinderExtend.IsOn() && IB_ScrewLockCylinderRetract.IsOff())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3052");
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart110_18_FlowRun(object sender, EventArgs e)
        {
            CYL_Conveyor1_ScrewFeedCylinder.Off();
            if (IB_ScrewFeedCylinderExtend.IsOff() && IB_ScrewFeedCylinderRetract.IsOn())
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            if (J_ThrowScrewtestTm.IsOn(GetSettingValue("PSet", "IOTimes")))
            {
                J_ThrowScrewtestTm.Restart();
                JSDK.Alarm.Show("3055");
            }
            return FCResultType.IDLE;
        }


        private FCResultType flowChart110_19_FlowRun(object sender, EventArgs e)
        {
            SaveLog($"总数：{I_ThrowScrewtestOkCount + I_ThrowScrewtestNgCount}  OK数：{I_ThrowScrewtestOkCount}  NG数：{I_ThrowScrewtestNgCount}");
            I_ThrowScrewtestCount++;
            if (I_ThrowScrewtestCount > Convert.ToInt32(textBox3.Text))
            {
                J_ThrowScrewtestTm.Restart();
                return FCResultType.NEXT;
            }
            return FCResultType.CASE1;
        }

        private FCResultType flowChart110_20_FlowRun(object sender, EventArgs e)
        {
            J_ThrowScrewtestTm.Restart();
            return FCResultType.NEXT;
        }
        private FCResultType flowChart10_FlowRun(object sender, EventArgs e)
        {
            I_ThrowScrewtestNgCount++;
            return FCResultType.NEXT;
        }

        private void btn_StartThrowScrewtest_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Start throw screw test?", "throw test", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            B_ThrowScrewtestFlow = true;
            flowChart110_1.TaskReset();
            Task.Factory.StartNew(() =>
            {
                while (B_ThrowScrewtestFlow)
                {
                    flowChart110_1.TaskRun();
                    Thread.Sleep(0);
                    Application.DoEvents();
                }
            });
        }

        private void btn_StopThrowScrewtest_Click(object sender, EventArgs e)
        {
            B_ThrowScrewtestFlow = false;
            StopRun();
        }

        private FCResultType flowChart9_FlowRun(object sender, EventArgs e)
        {
            return FCResultType.NEXT;
        }

        #endregion

        private FCResultType flowChart11_FlowRun(object sender, EventArgs e)
        {
            B_GantryResult = false;
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
            return FCResultType.NEXT;
        }

        private FCResultType flowChart12_FlowRun_1(object sender, EventArgs e)
        {
            JackingAxisPos AxisZ = GetJackingPointList("SafePos");
            bool bFinsh = MTR_Jacking.Goto(AxisZ.Position);
            if (bFinsh)
            {
                MiddleLayer.ConveyorF.myRFID2.ResultBool = false;
                MiddleLayer.SystemF.ShowStatus(2, SystemForm.WorkStatus.Fail);
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
                return FCResultType.NEXT;
            }
            if (J_AxisAutoTm.IsOn(GetSettingValue("PSet", "AutoAsixTimes")))
            {
                J_AxisAutoTm.Restart();
                MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart2_19.Text} overtime", false);
                GnatryAlarm("3014", flowChartMessage21);
                return FCResultType.CASE2;
            }
            return FCResultType.IDLE;
        }

        private FCResultType flowChart7_FlowRun_1(object sender, EventArgs e)
        {
            if (I_ScrewCount == 2)
            {
                B_ScrewVision = true;
            }
            J_AxisAutoTm.Restart();
            MiddleLayer.RecordF.LogShow(SysPara.UserName + " " + $"{this.Text} Module {flowChart5.Text} finish", true);
            return FCResultType.NEXT;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedRatio.Text = trackBar1.Value.ToString();
        }

        private void T_UpdateStatus_Tick(object sender, EventArgs e)
        {
            //MiddleLayer.SystemF.ShowStatus(2, SystemForm.WorkStatus.WaitProcess);
            T_UpdateStatus.Enabled = false;
        }
    }
}
