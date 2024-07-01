using Acura3._0.Classes;
using Acura3._0.FunctionForms;
using AcuraLibrary;
using CFX.Structures;
using JabilSDK;
using SASDK.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Acura3._0
{
    public partial class MainForm : Form
    {
        private MENU_PageType MENU_SelectPage = MENU_PageType.MachineStatus;
        private TableLayoutPanel[] MENU_Tablayout;
        private bool IsOpenMENU = false;
        private bool isTpActive = false; //Teong 15022021

        public enum MENU_PageType
        {
            MachineStatus = 0,
            MachineSetup,
            RecipeEditor,
            ProductionSetting,
            Maintenance,
            FlowChart,
            UserSetting,
            Exit,
        }

        public MainForm()
        {
            InitializeComponent();
            MENU_Tablayout = new TableLayoutPanel[] { tlMENU_MachineStatus, tlMENU_MachineSetup, tlMENU_RecipeEditor, tlMENU_ProductionSetting, tlMENU_Maintenance, tlMENU_FlowChart, tlMENU_UserSetting, tlMENU_Exit };
            OpenMENU(false);

            lbModifyDate.Text = "Modify Date : " + Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy/MM/dd");
            lbProgramVer.Text = "Software Version : " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            SwitchMainPage(MENU_SelectPage);
            MiddleLayer.OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.AcuraExecution);
            if (SysPara.EnableJAGConnectivity)
            {
                MiddleLayer.SystemF.MesF.SendMachineStatus(MachineStatusType.AcuraExecution);
            }
            SysPara.CFX.OnFaultClear += OnFaultClear;
           
        }
        public void OnFaultClear()
        {
            JSDK.Alarm.Clear();
            SysPara.MState.ResetAll();
            SysPara.CFX.FaultCleared(SysPara.UserName);
        }
        private void MENU_Click(object sender, EventArgs e)
        {
            string ItemName = Convert.ToString(((Control)sender).Tag);
            SwitchMainPage((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName));
            OpenMENU(false);
        }

        private void SwitchUser_Click(object sender, EventArgs e)
        {
            string OrgUser = SysPara.UserName;
            UserLoginForm UserLoginF = new UserLoginForm();
            UserLoginF.ShowDialog();
            if (OrgUser != SysPara.UserName)
                if (!MENU_Tablayout[(int)MENU_SelectPage].Enabled)
                    SwitchMainPage(MENU_PageType.MachineStatus);
                else
                    RefreshMenuBackcolor();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            SysPara.CFX.OperatorDeactivated(SysPara.UserName);
            SysPara.UserName = "None";
            MiddleLayer.SwitchPermission(PermissionType.None);
            if (!MENU_Tablayout[(int)MENU_SelectPage].Enabled)
                SwitchMainPage(MENU_PageType.MachineStatus);
            else
                RefreshMenuBackcolor();
        }

        public void RefreshMenuBackcolor()
        {
            for (int i = 0; i < MENU_Tablayout.Length; i++)
                if (i == (int)MENU_SelectPage)
                    MENU_Tablayout[i].BackColor = AcuraColors.Selected;
                else
                {
                    if (MENU_Tablayout[i].Enabled)
                        MENU_Tablayout[i].BackColor = AcuraColors.Background;
                    else
                        MENU_Tablayout[i].BackColor = Color.SkyBlue;
                }
        }

        private void SwitchMainPage(MENU_PageType PageType)
        {
            MENU_SelectPage = PageType;
            RefreshMenuBackcolor();

            switch (MENU_SelectPage)
            {
                case MENU_PageType.MachineStatus:
                    if (!isTpActive)//Teong 15022021
                    {
                        //ShowhMainPage(MiddleLayer.UserSettingF);
                        //ShowhMainPage(MiddleLayer.MaintenanceF);
                        //ShowhMainPage(MiddleLayer.ProductionSettingF);
                        //ShowhMainPage(MiddleLayer.RecipeEditorF);
                        //ShowhMainPage(MiddleLayer.MachineSetupF);
                        ShowhMainPage(MiddleLayer.FlowChartF);
                        ShowhMainPage(MiddleLayer.MachineStatusF);
                    }
                    else
                    {
                        ShowhMainPage(MiddleLayer.MachineStatusF);
                    }
                    break;
                case MENU_PageType.MachineSetup:
                    MiddleLayer.StopRun();
                    ShowhMainPage(MiddleLayer.MachineSetupF);

                    break;
                case MENU_PageType.RecipeEditor:
                    MiddleLayer.StopRun();
                    ShowhMainPage(MiddleLayer.RecipeEditorF);

                    break;
                case MENU_PageType.ProductionSetting:
                    MiddleLayer.StopRun();
                    ShowhMainPage(MiddleLayer.ProductionSettingF);
                    break;
                case MENU_PageType.Maintenance:
                    MiddleLayer.StopRun();
                    ShowhMainPage(MiddleLayer.MaintenanceF);

                    break;
                case MENU_PageType.FlowChart:
                    ShowhMainPage(MiddleLayer.FlowChartF);

                    break;
                case MENU_PageType.UserSetting:
                    MiddleLayer.StopRun();
                    ShowhMainPage(MiddleLayer.UserSettingF);

                    break;
                case MENU_PageType.Exit:
                    Close();
                    MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.AcuraShutdown);

                    break;
            }
        }

        private void MENU_MouseEnter(object sender, EventArgs e)
        {
            Control Parent = ((Control)sender).Parent;
            if (Parent.Enabled)
                Parent.BackColor = AcuraColors.MouseOver;
        }

        private void MENU_MouseLeave(object sender, EventArgs e)
        {
            Control Parent = ((Control)sender).Parent;
            if (Parent.Enabled)
            {
                string ItemName = Convert.ToString(((Control)sender).Tag);
                Parent.BackColor = AcuraColors.Background;
                if (Parent.Name.IndexOf("MENU") >= 0)
                    if ((MENU_PageType)Enum.Parse(typeof(MENU_PageType), ItemName) == MENU_SelectPage)
                        Parent.BackColor = AcuraColors.Selected;
            }
        }
      
        private void ShowhMainPage(dynamic ShowPage)
        {
            foreach (Control control in plMainShow.Controls)
            {
               // control.Parent = null; //Teong 15022021
                control.Visible = false;
            }

            if (ShowPage.GetType().IsSubclassOf(typeof(Form)))
            {
                ShowPage.TopLevel = false;
                ShowPage.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                ShowPage.WindowState = FormWindowState.Maximized;
                ShowPage.Dock = DockStyle.Fill;
            }
            else
                ShowPage.Dock = DockStyle.Fill;
            ShowPage.Parent = plMainShow;
            ShowPage.Show();
          
            //Teong 15022021
            if ((ShowPage.Name == "FlowChartForm" || ShowPage.Name == "MachineStatusForm") && !isTpActive)
            {
                isTpActive = true;
                SASDK.Class.GFunc.ActiveTabPage(ShowPage);
            }
        }



        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        private void OpenMENU_Click(object sender, EventArgs e)
        {
            OpenMENU(!IsOpenMENU);
        }

        private void OpenMENU(bool bOpen)
        {
            if (bOpen)
            {
                plMENU.Width = 280;
                IsOpenMENU = true;
            }
            else
            {
                plMENU.Width = 86;
                IsOpenMENU = false;
            }
            this.Refresh();
        }

        private bool StatusChange_plLogin = false;
        private bool StatusChange_plLogout = false;
        private bool StatusChange_plMotorControl = false;
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            lbUserName.Text = SysPara.UserName;
            lbRecipeName.Text = SysPara.RecipeName;
            lbDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

            bool IsCanRunPage = (MENU_SelectPage == MENU_PageType.MachineStatus) || (MENU_SelectPage == MENU_PageType.FlowChart);
            bool IsLogin = (SysPara.UserPermission != PermissionType.None);

            btnInitial.Enabled = IsLogin & IsCanRunPage && !MiddleLayer.IsManualRun() && !SysPara.IsMaintenanceMode;
            btnInitial.BackColor = btnInitial.Enabled ? AcuraColors.Background : AcuraColors.Disable;

            btnStart.Enabled = IsLogin & IsCanRunPage & SysPara.SystemInitialOk & !SysPara.SystemRun && !MiddleLayer.IsManualRun() && !SysPara.IsMaintenanceMode; //) || SysPara.isBtnStartEnable;
            btnStart.BackColor = btnStart.Enabled ? AcuraColors.Background : AcuraColors.Disable;

            btnPause.Enabled = (IsLogin & IsCanRunPage & SysPara.SystemRun) || MiddleLayer.IsManualRun();
            btnPause.BackColor = btnPause.Enabled ? AcuraColors.Background : AcuraColors.Disable;

            btnStop.Enabled = IsLogin & IsCanRunPage & (SysPara.SystemMode != RunMode.IDLE) && !SysPara.IsMaintenanceMode;
            btnStop.BackColor = btnStop.Enabled ? AcuraColors.Background : AcuraColors.Disable;

            #region Login/Logout/MotorControl UI control
            plMotorControl.Enabled = IsLogin & (SysPara.SystemMode == RunMode.IDLE);
            if (plMotorControl.Enabled)
            {
                if (StatusChange_plMotorControl)
                {
                    plMotorControl.BackColor = AcuraColors.Background;
                    StatusChange_plMotorControl = false;
                }
            }
            else
            {
                if (!StatusChange_plMotorControl)
                {
                    plMotorControl.BackColor = AcuraColors.Disable;
                    StatusChange_plMotorControl = true;
                }
            }

            plLogin.Enabled = !SysPara.SystemRun;
            if (plLogin.Enabled)
            {
                if (StatusChange_plLogin)
                {
                    plLogin.BackColor = AcuraColors.Background;
                    StatusChange_plLogin = false;
                }
            }
            else
            {
                if (!StatusChange_plLogin)
                {
                    plLogin.BackColor = AcuraColors.Disable;
                    StatusChange_plLogin = true;
                }
            }

            plLogout.Enabled = IsLogin & !SysPara.SystemRun;
            if (plLogout.Enabled)
            {
                if (StatusChange_plLogout)
                {
                    plLogout.BackColor = AcuraColors.Background;
                    StatusChange_plLogout = false;
                }
            }
            else
            {
                if (!StatusChange_plLogout)
                {
                    plLogout.BackColor = AcuraColors.Disable;
                    StatusChange_plLogout = true;
                }
            }
            #endregion

            #region Progress Bar
            if (SysPara.SystemRun)
            {
                lbProgress.Visible = true;
                lbProgress.Left += 4;
                if (lbProgress.Left > plProgress.Width)
                    lbProgress.Left = -lbProgress.Width;
                if (SysPara.SystemMode == RunMode.INITIAL)
                    lbProgress.Text = "Initial";
                else if (SysPara.SystemMode == RunMode.RUN)
                    lbProgress.Text = "Running";

            }
            else
            {
                lbProgress.Visible = false;
            }
            #endregion

            #region Alarm Message
            if (JSDK.Alarm.DoRefresh)
            {
                JSDK.Alarm.DoRefresh = false;
                lvMessage.BeginUpdate();
                lvMessage.Items.Clear();
                for (int i = 0; i < JSDK.Alarm.AlarmList.Count; i++)
                {
                    AlarmClass.AlarmDataClass AlarmData = JSDK.Alarm.AlarmList[i];
                    ListViewItem Alarm = new ListViewItem(AlarmData.DateTime);
                    Alarm.SubItems.Add(AlarmData.Type);
                    Alarm.SubItems.Add(AlarmData.Code);
                    Alarm.SubItems.Add(AlarmData.Content);
                    Alarm.ToolTipText = GetAlarmToolTip(AlarmData.Code);
                    switch (AlarmData.Type)
                    {
                        case "E":
                            Alarm.SubItems[0].BackColor = Color.Red;
                            SysPara.CFX.FaultOccurred(new Fault { FaultCode = AlarmData.Code, Description = AlarmData.Content, Severity = FaultSeverity.Error });
                            break;
                        case "W":
                            Alarm.SubItems[0].BackColor = Color.Yellow;
                            SysPara.CFX.FaultOccurred(new Fault { FaultCode = AlarmData.Code, Description = AlarmData.Content, Severity = FaultSeverity.Warning });
                            break;
                        case "K":
                            Alarm.SubItems[0].BackColor = Color.DarkSalmon;
                            SysPara.CFX.FaultOccurred(new Fault { FaultCode = AlarmData.Code, Description = AlarmData.Content, Severity = FaultSeverity.Error });
                            break;
                        case "I":
                            SysPara.CFX.FaultOccurred(new Fault { FaultCode = AlarmData.Code, Description = AlarmData.Content, Severity = FaultSeverity.Information });
                            break;
                    }

                    lvMessage.Items.Insert(0, Alarm);
                    MiddleLayer.LogF.AddLog(LogForm.LogType.Alarm, AlarmData.Type + "," + AlarmData.Code + "," + AlarmData.Content);
                    if (SysPara.EnableJAGConnectivity)
                    {
                        MiddleLayer.SystemF.MesF.SendMachineError(AlarmData.Type, AlarmData.Code);
                    }
                }
                lvMessage.EndUpdate();


            }
            #endregion
        }
        Dictionary<string, string> AlarmExpDic = new Dictionary<string, string>();
        private string GetAlarmToolTip(string AlarmCode)
        {
            string ret = "";
            if (AlarmExpDic.ContainsKey(AlarmCode))
            {
                ret = AlarmExpDic[AlarmCode];
                AlarmExpDic.Remove(AlarmCode);
            }
            return ret;
        }

        public void AddAlarmToolTip(string AlarmCode, string Content)
        {
            if (!AlarmExpDic.ContainsKey(AlarmCode))
                AlarmExpDic.Add(AlarmCode, Content);
        }

        public void SwitchPermission(PermissionType Permission)
        {
            string strSQL = "select * from PermissionSetup where Permission ='" + Permission.ToString() + "'";
            bool Successful = false;

            DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            if (Successful)
            {
                if (readData.Rows.Count > 0)
                {
                    tlMENU_MachineSetup.Enabled = Convert.ToBoolean(readData.Rows[0]["MachineSetup"]);
                    tlMENU_RecipeEditor.Enabled = Convert.ToBoolean(readData.Rows[0]["RecipeEditor"]);
                    tlMENU_ProductionSetting.Enabled = Convert.ToBoolean(readData.Rows[0]["ProductionSetting"]);
                    tlMENU_Maintenance.Enabled = Convert.ToBoolean(readData.Rows[0]["Maintenance"]);
                    tlMENU_FlowChart.Enabled = Convert.ToBoolean(readData.Rows[0]["FlowChart"]);
                    tlMENU_UserSetting.Enabled = (Permission == PermissionType.Administrator);
                    tlMENU_Exit.Enabled = Convert.ToBoolean(readData.Rows[0]["Exit"]);
                }
            }
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            switch (SysPara.SystemMode)
            {
                case RunMode.IDLE:
                case RunMode.RUN:
                    if (SysPara.SystemInitialOk)
                    {
                        DialogResult result = MessageBox.Show("Automatic production now, Are you sure to exit automatic production and execute initialize?", "Initialize", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Cancel)
                            break;
                    }
                    MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineInitialization);
                    if (SysPara.EnableJAGConnectivity)
                    {
                        MiddleLayer.SystemF.MesF.SendMachineStatus(MachineStatusType.MachineInitialization);
                    }
                    MiddleLayer.FlowCtrl.InitialReset();
                    MiddleLayer.StartRun();
                    break;
                case RunMode.INITIAL:
                    MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineInitialization);
                    if (SysPara.EnableJAGConnectivity)
                    {
                        MiddleLayer.SystemF.MesF.SendMachineStatus(MachineStatusType.MachineInitialization);
                    }
                    MiddleLayer.StartRun();
                    return;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MiddleLayer.MCU_PCBA_Module1F.J_AxisAutoTm.Restart();
            MiddleLayer.MCU_PCBA_Module1F.J_ScrewAutoTm.Restart();
            //MiddleLayer.MCU_PCBA_Module2F.J_AxisAutoTm.Restart();
            //MiddleLayer.MCU_PCBA_Module2F.J_ScrewAutoTm.Restart();
            SysPara.isBtnStartPress = true;
            switch (SysPara.SystemMode)
            {
                case RunMode.IDLE:
                    SysPara.RunSecond = 0;
                    SysPara.StopSecond = 0;
                    SysPara.StartWorkTM = DateTime.Now;

                    MiddleLayer.FlowCtrl.RunReset();
                    MiddleLayer.StartRun();
                    break;
                case RunMode.RUN:
                    MiddleLayer.StartRun();
                    return;
            }
            SysPara.isBtnStartEnable = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {           
            MiddleLayer.StopRun();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
           
            if (SysPara.SystemMode == RunMode.RUN || SysPara.SystemMode == RunMode.INITIAL)
            {
                MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineIdle);
                if (SysPara.EnableJAGConnectivity)
                {
                    MiddleLayer.ConveyorF.MesF.SendMachineStatus(MachineStatusType.MachineIdle);
                }
                MiddleLayer.StopRun();
                SysPara.SystemMode = RunMode.IDLE;
                SysPara.SystemInitialOk = false;
            }
        }

        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            MiddleLayer.AP_PCBA_V.OB_Robot_Reset.Off();
            MiddleLayer.CoverF.OB_Robot_Reset.Off();
            MiddleLayer.SystemF.DelayMs(500);
            MiddleLayer.AP_PCBA_V.OB_Robot_Reset.On();
            MiddleLayer.CoverF.OB_Robot_Reset.On();
            MiddleLayer.LogF.AddLog(LogForm.LogType.Operation, $"{SysPara.UserName} click alarm reset");
            MiddleLayer.MotorAlarmReset();
            JSDK.Alarm.Clear();
            SysPara.MState.ResetAll();
            SysPara.CFX.FaultCleared(SysPara.UserName);
        }

        private void btnBuzzerOff_Click(object sender, EventArgs e)
        {
            MiddleLayer.SystemF.BuzzOff();
            SysPara.CFX.FaultAcknowledged(SysPara.UserName);
        }

        private void pbMotorControl_Click(object sender, EventArgs e)
        {
            MiddleLayer.MotorJogF.Show();
        }

        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
          
        }

        private void pbLog_Click(object sender, EventArgs e)
        {
            if (!MiddleLayer.LogF.Visible)
                MiddleLayer.LogF.Show();
            else
                MiddleLayer.LogF.BringToFront();
        }

        private void pbLogo_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SysPara.CFX.StationOffline();
            SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.NST_ShutdownAndStartup);
            System.Threading.Thread.Sleep(1000);
            SysPara.CFX.Endpoint.Close();
        }
    }
}
