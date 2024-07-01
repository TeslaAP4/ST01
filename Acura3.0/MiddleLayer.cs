using Acura3._0.Classes;
using Acura3._0.Forms;
using Acura3._0.FunctionForms;
using Acura3._0.MENUForms;
using Acura3._0.ModuleForms;
using AcuraLibrary;
using AcuraLibrary.Forms;
using JabilSDK;
using JabilSDK.Controls;
using JabilSDK.UserControlLib;
using SASDK.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static JabilSDK.MotorBaseInterface;
using VppComp;

namespace Acura3._0
{
    class MiddleLayer
    {

        //===================Module Form===================
        public static SystemForm SystemF;
        public static ConveyorForm ConveyorF;
        //public static PCBAModuleForm PCBAModuleF;
        //public static BackplateModuleForm BackplateModuleF;
        //public static RobotScrewModuleForm ScrewFastenModuleF;
        public static MCU_PCBA_Module1 MCU_PCBA_Module1F;
        public static MyForm MyFormF;
        //public static MCU_PCBA_Module2 MCU_PCBA_Module2F;
        public static AP_PCBA_Vision AP_PCBA_V;
        public static CoverAssembly CoverF;
        public static RecordForm RecordF;
        //===================Function Form=================
        public static LoadingMarqueeForm LoadingMarqueeF;
        public static SignalTowerForm SignalTowerF;
        public static MotorJogForm MotorJogF;
        public static LogForm LogF;
        public static Vpp vpp = null;
        //===================MENU Form==================
        public static MainForm MainF;
        public static MachineStatusForm MachineStatusF;
        public static MachineSetupForm MachineSetupF;
        public static RecipeEditorForm RecipeEditorF;
        public static ProductionSettingForm ProductionSettingF;
        public static MaintenanceForm MaintenanceF;
        public static FlowChartForm FlowChartF;
        public static UserSettingForm UserSettingF;
        public static FingerprintCaptureForm FingerprintCaptureF;

        //=================================================
        public static List<dynamic> lstForm = new List<dynamic>();
        public static FlowControl FlowCtrl;
        public static CoreEngine coreEngine; //teong
        public static CsvFile csvFile = new CsvFile();

        //private static List<Task> RunTask = new List<Task>(); //Zax 9/1/21- Module Multi Thread

        public static void InitialProject()
        {
            SysPara.CFX.OpenEndpoint();
            SysPara.CFX.StationOnline();
            SysPara.CFX.SystemTimeSync();

            #region Load Ini File
            LoadingMarqueeF.SetCaption("IniFile");
            IniFile iniFile = new IniFile(".\\MachineSetup.ini");
            SysPara.ProjectName = iniFile.ReadString("MachineSetup", "ProjectName", "Acura3.0");
            SysPara.RecipeName = iniFile.ReadString("MachineSetup", "RecipeName", "Recipe");
            SysPara.Simulation = iniFile.ReadBoolen("LogicSetup", "Simulation", false);
            SysPara.EnableJAGConnectivity = iniFile.ReadBoolen("LogicSetup", "EnableJAGConnectivity", true);
            SysPara.LogFilePath = iniFile.ReadString("PathSetup", "LogFileDirectory", ".\\LogFile");
            SysPara.VisionFileDirectory = iniFile.ReadString("PathSetup", "VisionDataDirectory", ".\\VisionData");
            SysPara.AlarmTableDirectory = iniFile.ReadString("PathSetup", "AlarmTableDirectory", ".\\AlarmTable");
            SysPara.SettingDataDirectory = iniFile.ReadString("PathSetup", "SettingDataDirectory", ".\\ModuleData\\SettingData");
            SysPara.RecipeDataDirectory = iniFile.ReadString("PathSetup", "RecipeDirectory", ".\\ModuleData\\RecipeData");
            SysPara.MESDirectory = iniFile.ReadString("PathSetup", "MESDirectory", ".\\ModuleData\\MESData");
            SysPara.IOPortDirectory = iniFile.ReadString("PathSetup", "IOPortDirectory", ".\\ModuleData");
            SysPara.LanguageDataDirectory = iniFile.ReadString("PathSetup", "LanguageDirectory", ".\\LanguageData");
            SysPara.DispenserDirectory = iniFile.ReadString("PathSetup", "DispenserDirectory", ".\\ModuleData\\DispenserData");
            SysPara.WebService = iniFile.ReadString("WebService", "Address", "http://jpetewebapp/jtesw_ws/jtesw_webservice.asmx"); //Zax 7/27/21
            //lstModule = ModuleBaseParameter.lstModule;
            ModuleManager.SettingDataDirectory = SysPara.SettingDataDirectory;

            LoadingMarqueeF.SetCaption("Dispenser Setting");

            #endregion
            #region Load Alarm Table
            LoadingMarqueeF.SetCaption("AlarmTable");
            if (!JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, SysPara.LanguageShow.ToString())))
            {
                MessageBox.Show("Error reading alarm list." + Environment.NewLine + "Application Will Close");
                Environment.Exit(0);

            }
            #endregion

            //=======================Module Create===========================
            LoadingMarqueeF.SetCaption("Create Module");
            SystemF = CreateForm(SystemF, "SystemForm");
            ConveyorF = CreateForm(ConveyorF, "ConveyorForm");
            //PCBAModuleF = CreateForm(PCBAModuleF, "PCBAModuleForm");
            //BackplateModuleF = CreateForm(BackplateModuleF, "BackplateModuleForm");
            //ScrewFastenModuleF = CreateForm(ScrewFastenModuleF, "ScrewFastenModuleForm");
            MCU_PCBA_Module1F = CreateForm(MCU_PCBA_Module1F, "MCU_PCBA_Module1");
            MyFormF = CreateForm(MyFormF, "MyForm");
            //MCU_PCBA_Module2F = CreateForm(MCU_PCBA_Module2F, "MCU_PCBA_Module2");

            AP_PCBA_V = CreateForm(AP_PCBA_V, "AP_PCBA_Vision");
            CoverF = CreateForm(CoverF, "CoverAssembly");
            RecordF = CreateForm(RecordF, "RecordForm");
            //========================Form Create============================
            MainF = CreateForm(MainF, "MainForm");
            SignalTowerF = CreateForm(SignalTowerF, "SignalTowerForm");
            MotorJogF = CreateForm(MotorJogF, "MotorJogForm");
            LogF = CreateForm(LogF, "LogForm");

            //=====================Complex Form Create=======================
            MachineStatusF = CreateForm(MachineStatusF, "MachineStatusForm");
            MachineSetupF = CreateForm(MachineSetupF, "MachineSetupForm");
            RecipeEditorF = CreateForm(RecipeEditorF, "RecipeEditorForm");
            ProductionSettingF = CreateForm(ProductionSettingF, "ProductionSettingForm");
            MaintenanceF = CreateForm(MaintenanceF, "MaintenanceForm");
            FlowChartF = CreateForm(FlowChartF, "FlowChartForm");
            UserSettingF = CreateForm(UserSettingF, "UserSettingForm");

            vpp = Vpp.GetInstance();
            vpp.LoadRecipe("123", null);
            vpp.LoadCalibration();
            vpp.EnableControl();
            if (SysPara.UseFingerprint)
                FingerprintCaptureF = new FingerprintCaptureForm();
            //===============================================================
            //InitialLanguageData();
            //SwitchLanguage(SysPara.LanguageShow);
            SwitchPermission(SysPara.UserPermission);
            SwitchPermission(PermissionType.Administrator);
            //InitialIOPortData();
            JSDK.SetSimulation(SysPara.Simulation);
            JSDK.InitialControls();

            //Zax 9/1/21- Module Multi Thread
            //for (int x = 0; x < ModuleManager.ModuleList.Count; x++)
            //{
            //    Task _Task = null;
            //    RunTask.Add(_Task);
            //}

            FlowCtrl = new FlowControl();
            FlowCtrl.StartThread();

            LoadingMarqueeF.SetCaption("Recipe");
            OpenRecipe(string.Format("{0}\\{1}.xml", SysPara.RecipeDataDirectory, SysPara.RecipeName));
            ServoOn();
            StartUp();
            SysPara.isBarcodeDoneToGet = true;

            //========================SASDK Create============================
            coreEngine = new CoreEngine();
            coreEngine.StartThread();
            LoadingMarqueeF.SetCaption("Register User Input Tracker");
            coreEngine.SubAllControl(MachineStatusF);
            coreEngine.SubAllControl(MachineSetupF);
            coreEngine.SubAllControl(RecipeEditorF);
            coreEngine.SubAllControl(ProductionSettingF);
            coreEngine.SubAllControl(MaintenanceF);
            coreEngine.SubAllControl(FlowChartF);
            coreEngine.SubAllControl(UserSettingF);
            coreEngine.SubAllControl(MotorJogF); //Teong CFX 
            coreEngine.SubAllControl(SystemF.MesF);//Teong CFX 
            coreEngine.SubAllControl(MainF);

            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                coreEngine.SubAllControl(Module);
            }
            coreEngine.OperationLogRaise += CoreEngine_OperationLogRaise; //Teong CFX 
            coreEngine.CFXStationOnlineEvent += CoreEngine_CFXStationOnlineEvent;//Teong CFX 

        }

        private static void CoreEngine_CFXStationOnlineEvent(object sender, EventArgs e)
        {
            //SystemF.DBEngineCFX.StationOnline();//Teong CFX 
            //SystemF.DBEngineCFX.StationStateChanged(SASDK.DBEngine.ucDBEngineCFX.StateChange.Standby, DateTime.Now);
            //SystemF.DBEngineCFX.RecipeActivated(SysPara.RecipeName);

        }

        private static void CoreEngine_OperationLogRaise(object sender, OperationLogArgs e)
        {
            //Teong CFX 
            LogF.AddLog(LogForm.LogType.Operation, $"{e.Module},{e.Username},{e.Event}");
        }

        public static void DisposeProject()
        {
            //SystemF.DBEngineCFX.StationOffline();//Teong CFX 
            ServoOff();
            FlowCtrl.StopThread();
            FlowCtrl = null;

            coreEngine.StopThread();
            coreEngine.DisposeCtrl();
            coreEngine = null;

            LogF.DisposeLog();
            foreach (ControlBaseInterface control in JSDK.ControlList)
            {
                if (control is Output)
                {
                    ((Output)control).Off();
                }
            }

            JSDK.DisposeControls();
            //CogFrameGrabbers CCD_Graber = new Cognex.VisionPro.CogFrameGrabbers();
            //for (int i = 0; i < CCD_Graber.Count; i++)
            //    CCD_Graber[i].Disconnect(false);

        }

        private static void InitialLanguageCallback(Control cl, string FormName, ref List<ComponentTextInfo> ComponentLangurageList, ref List<ComponentTextInfo> XmlDataList)
        {
            foreach (Control control in cl.Controls)
            {
                Type ControlType = control.GetType();
                bool bNeedAdded = false;
                bNeedAdded |= (ControlType == typeof(Form));
                bNeedAdded |= (ControlType == typeof(Label));
                bNeedAdded |= (ControlType == typeof(Motor));
                bNeedAdded |= (ControlType == typeof(Input));
                bNeedAdded |= (ControlType == typeof(Output));
                bNeedAdded |= (ControlType == typeof(FlowChart));
                bNeedAdded |= (ControlType == typeof(GroupBox));
                bNeedAdded |= (ControlType == typeof(CheckBox));
                bNeedAdded |= (ControlType == typeof(CheckedListBox));
                bNeedAdded |= (ControlType == typeof(Button));
                if (control.Name != "" && bNeedAdded)
                {
                    ComponentTextInfo AddComLan = new ComponentTextInfo();
                    int Index = XmlDataList.FindIndex(ComLan => (ComLan.FormName == FormName && ComLan.ComponentName == control.Name));
                    if (Index >= 0)
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = XmlDataList[Index].ComponentName;
                        AddComLan.ComponentText = XmlDataList[Index].ComponentText;
                        AddComLan.Component = control;
                    }
                    else
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = control.Name;
                        AddComLan.ComponentText = control.Text;
                        AddComLan.Component = control;
                    }
                    ComponentLangurageList.Add(AddComLan);
                }
                if (control.HasChildren)
                    InitialLanguageCallback(control, FormName, ref ComponentLangurageList, ref XmlDataList);
            }
        }

        public static void SwitchLanguage(LanguageType LanType)
        {
            for (int i = 0; i < SysPara.ComponentLangurageList[(int)LanType].Count; i++)
                SysPara.ComponentLangurageList[(int)LanType][i].Component.Text = SysPara.ComponentLangurageList[(int)LanType][i].ComponentText;
            JSDK.Alarm.Initial(string.Format("{0}\\{1}.xml", SysPara.AlarmTableDirectory, SysPara.LanguageShow.ToString()));
            JSDK.Alarm.Clear();
            if (MainF != null)
            {
                //MainF.SwitchLanguage(LanType);
                //MainF.cbLanguage.SelectedIndex = (int)LanType;
            }
        }

        public static void SwitchPermission(PermissionType Permission)
        {
            SysPara.UserPermission = Permission;
            MainF.SwitchPermission(Permission);

            //MainF.SwitchLanguage(SysPara.LanguageShow);
        }

        private static void InitialIOPortCallback(Control cl, string FormName, ref List<IOPortInfo> ComponentIOPortList, ref List<IOPortInfo> XmlDataList)
        {
            foreach (dynamic control in cl.Controls)
            {
                Type ControlType = control.GetType();
                bool bNeedAdded = false;
                bNeedAdded |= (ControlType == typeof(Motor));
                bNeedAdded |= (ControlType == typeof(Input));
                bNeedAdded |= (ControlType == typeof(Output));
                if (control.Name != "" && bNeedAdded)
                {
                    IOPortInfo AddComLan = new IOPortInfo();
                    int Index = XmlDataList.FindIndex(ComLan => (ComLan.FormName == FormName && ComLan.ComponentName == control.Name));
                    if (Index >= 0)
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = XmlDataList[Index].ComponentName;
                        AddComLan.Port = XmlDataList[Index].Port;
                        control.Port = XmlDataList[Index].Port;
                    }
                    else
                    {
                        AddComLan.FormName = FormName;
                        AddComLan.ComponentName = control.Name;
                        AddComLan.Port = control.Port;
                    }
                    ComponentIOPortList.Add(AddComLan);
                }
                if (control.HasChildren)
                    InitialIOPortCallback(control, FormName, ref ComponentIOPortList, ref XmlDataList);
            }
        }

        private static dynamic CreateForm(dynamic FormAddress, string FormName)
        {
            LoadingMarqueeF.SetCaption(FormName);
            dynamic dmic = (FormAddress == null) ? null : FormAddress;
            Assembly Assembly = Assembly.GetExecutingAssembly();
            foreach (Type ObjType in Assembly.GetTypes())
                if (ObjType.Name == FormName)
                {
                    if (dmic == null)
                        dmic = Activator.CreateInstance(ObjType, null);
                    lstForm.Add(dmic);
                    if (ObjType.IsSubclassOf(typeof(ModuleBaseForm)))
                        dmic.ModuleInitialize(dmic.Text);
                    break;
                }
            return dmic;
        }

        public static void StartRun()
        {
            if (!SysPara.SystemRun)
                if ((SysPara.SystemMode == RunMode.RUN) || (SysPara.SystemMode == RunMode.INITIAL))
                {
                    JSDK.Alarm.Clear();
                    MiddleLayer.MotorAlarmReset();
                    SysPara.MState.ResetAll();
                    SysPara.CFX.FaultCleared(SysPara.UserName);
                    if (SysPara.SystemMode != RunMode.INITIAL)
                        MiddleLayer.SetSpeed(10/*MiddleLayer.SystemF.GetSettingValue("PSet", "MachineSpeedRatio")*/);

                    SysPara.IsDryRun = MiddleLayer.SystemF.GetSettingValue("PSet", "Dryrun");

                    //SysPara.CFX.FaultCleared(SysPara.UserName);

                    //if (SysPara.SystemMode == RunMode.RUN)
                    //    SysPara.CFX.StationStateChanged(CFX.Structures.ResourceState.PRD, DateTime.Now); //Auto
                    foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                        Module.StartRun();
                    SysPara.SystemRun = true;
                    SetModulesRun();
                }
        }

        public static void StopRun()
        {
            SysPara.SystemRun = false;
            SetModulesStop();
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                Module.StopRun();
            SysPara.MState.ResetAll();
            StopAllMotor();
            StopManualRun();
        }

        public static void StopAllMotor()
        {
            foreach (ControlBaseInterface control in JSDK.ControlList)
                if (control is Motor)
                    ((Motor)control).Stop();
        }

        public static void StopManualRun()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                try
                {
                    Module.StopManualTask.Cancel();
                }
                catch { }

        }

        public static bool IsManualRun()
        {
            bool IsManualRun = false;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                IsManualRun |= Module.IsManualRun();
            return IsManualRun;
        }

        public static void AlwaysRun()
        {
            if (JSDK.Alarm.DoStop)
            {
                if (SysPara.SystemRun || IsManualRun())
                {
                    //MiddleLayer.LogF.AddMachineStatusEvent(LogForm.MachineStatusType.MachineDown);

                    JSDK.Alarm.DoStop = false;
                    StopRun();
                }
            }

            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.AlwaysRun();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2010", "An unpredictable error occurred on AlwaysRun() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2010", ex.ToString());
                    StopRun();
                }
            }
        }

        public static void CheckMotorProtected()
        {
            if (SysPara.Simulation)
                return;

            AxisIOStatus IOState;
            foreach (ControlBaseInterface control in JSDK.ControlList)
            {
                //if (control is Motor)
                //{

                //    IOState = ((Motor)control).GetAxisIOStatus();
                //    if (((Motor)control).IgnoreAlarm()) return;
                //    if (IOState.ALM)
                //    {
                //        if (control.Name == "MTR_Y_Feeder1")
                //        {
                //            sFeederType = "Feeder1";
                //            SysPara.isMotorAlarmFeeder1 = true;
                //        }
                //        else if (control.Name == "MTR_Y_Feeder2")
                //        {
                //            sFeederType = "Feeder2";
                //            SysPara.isMotorAlarmFeeder2 = true;
                //        }
                //        if (SysPara.SystemRun)
                //        {
                //            ((Motor)control).Stop();
                //            JSDK.Alarm.Show("3030", "Motor Y " + sFeederType + " alarm!");
                //        }
                //        else
                //            JSDK.Alarm.Show("3000", "Motor Y " + sFeederType + " alarm!");
                //    }
                //    if (IOState.SLN)
                //    {
                //        if (control.Name == "MTR_Y_Feeder1")
                //        {
                //            sFeederType = "Feeder1";
                //            SysPara.isMotorAlarmFeeder1 = true;
                //        }
                //        else if (control.Name == "MTR_Y_Feeder2")
                //        {
                //            sFeederType = "Feeder2";
                //            SysPara.isMotorAlarmFeeder2 = true;
                //        }
                //        if (SysPara.SystemRun )
                //        {
                //            ((Motor)control).Stop();
                //            JSDK.Alarm.Show("3031", "Motor Y " +
                //                sFeederType + " soft negative limit alarm!");
                //        }
                //        else
                //            JSDK.Alarm.Show("3023", "Motor Y " +
                //                 sFeederType + " soft negative limit alarm!");
                //    }
                //    if (IOState.SLP)
                //    {
                //        if (control.Name == "MTR_Y_Feeder1")
                //        {
                //            sFeederType = "Feeder1";
                //            SysPara.isMotorAlarmFeeder1 = true;
                //        }
                //        else if (control.Name == "MTR_Y_Feeder2")
                //        {
                //            sFeederType = "Feeder2";
                //            SysPara.isMotorAlarmFeeder2 = true;
                //        }
                //        if (SysPara.SystemRun)
                //        {
                //            ((Motor)control).Stop();
                //            JSDK.Alarm.Show("3032", "Motor Y " +
                //                sFeederType + " soft positive limit alarm!");
                //        }
                //        else
                //            JSDK.Alarm.Show("3002", "Motor Y " +
                //                sFeederType + " soft positive limit alarm!");
                //    }
                //    if (IOState.HLN && !((Motor)control).IsHoming())
                //    {
                //        if (control.Name == "MTR_Y_Feeder1")
                //        {
                //            sFeederType = "Feeder1";
                //            SysPara.isMotorAlarmFeeder1 = true;
                //        }
                //        else if (control.Name == "MTR_Y_Feeder2")
                //        {
                //            sFeederType = "Feeder2";
                //            SysPara.isMotorAlarmFeeder2 = true;
                //        }
                //        if (SysPara.SystemRun)
                //        {
                //            ((Motor)control).Stop();
                //            JSDK.Alarm.Show("3033", "Motor Y " +
                //              sFeederType + " Motor Y hardware negative limit alarm!");
                //        }
                //        else
                //            JSDK.Alarm.Show("3022", "Motor Y " +
                //               sFeederType + " Motor Y hardware negative limit alarm!");
                //    }
                //    if (IOState.HLP && !((Motor)control).IsHoming())
                //    {
                //        if (control.Name == "MTR_Y_Feeder1")
                //        {
                //            sFeederType = "Feeder1";
                //            SysPara.isMotorAlarmFeeder1 = true;
                //        }
                //        else if (control.Name == "MTR_Y_Feeder2")
                //        {
                //            sFeederType = "Feeder2";
                //            SysPara.isMotorAlarmFeeder2 = true;
                //        }
                //        if (SysPara.SystemRun)
                //        {
                //            ((Motor)control).Stop();
                //            JSDK.Alarm.Show("3034", "Motor Y " +
                //             sFeederType + " Motor Y hardware positive limit alarm!");
                //        }
                //        else
                //            JSDK.Alarm.Show("3001", "Motor Y " +
                //              sFeederType + " Motor Y hardware positive limit alarm!");

                //        //JSDK.Alarm.Show("3001", "Motor Y " +
                //        //   sFeederType + " Motor Y hardware positive limit alarm!");
                //    }
                //    //    JSDK.Alarm.Show("3000", "The axis alarm! Motor Name=\"" + control.Name + "\"");
                //    //if (IOState.SLN)
                //    //    JSDK.Alarm.Show("3023", "The axis software negative limit! Motor Name=\"" + control.Name + "\"");
                //    //if (IOState.SLP)
                //    //    JSDK.Alarm.Show("3002", "The axis software positive limit! Motor Name=\"" + control.Name + "\"");
                //    //if (IOState.HLN)
                //    //    if (!((Motor)control).IsHoming())
                //    //        JSDK.Alarm.Show("3022", "The axis hardware negative limit! Motor Name=\"" + control.Name + "\"");
                //    //if (IOState.HLP)
                //    //    if (!((Motor)control).IsHoming())
                //    //        JSDK.Alarm.Show("3001", "The axis hardware positive limit! Motor Name=\"" + control.Name + "\"");
                if (control is Motor)
                {

                    IOState = ((Motor)control).GetAxisIOStatus();
                    if (((Motor)control).IgnoreAlarm()) return;
                    if (IOState.ALM)
                    {
                        if (SysPara.SystemRun)
                        {
                            ((Motor)control).Stop();
                            JSDK.Alarm.Show("3130", control.FindForm().Name + " Module ：" + control.Name + " alarm!");
                        }
                        //JSDK.Alarm.Show("3000", control.FindForm().Name + " Module ：" + control.Name + " alarm!");
                    }
                    if (IOState.SLN)
                    {
                        if (SysPara.SystemRun)
                        {
                            ((Motor)control).Stop();
                            JSDK.Alarm.Show("3131", control.FindForm().Name + " Module ：" + control.Name + " soft negative limit alarm!");
                        }
                        //JSDK.Alarm.Show("3023", control.FindForm().Name + " Module ：" + control.Name + " soft negative limit alarm!");
                    }
                    if (IOState.SLP)
                    {
                        if (SysPara.SystemRun)
                        {
                            ((Motor)control).Stop();
                            JSDK.Alarm.Show("3132", control.FindForm().Name + " Module ：" + control.Name + " soft positive limit alarm!");
                        }
                        //JSDK.Alarm.Show("3002", control.FindForm().Name + " Module ：" + control.Name + " soft positive limit alarm!");
                    }
                    if (IOState.HLN && !((Motor)control).IsHoming())
                    {
                        if (SysPara.SystemRun)
                        {
                            ((Motor)control).Stop();
                            JSDK.Alarm.Show("3133", control.FindForm().Name + " Module ：" + control.Name + "  hardware negative limit alarm!");
                        }
                        //JSDK.Alarm.Show("3022", control.FindForm().Name + " Module ：" + control.Name + "  hardware negative limit alarm!");
                    }
                    if (IOState.HLP && !((Motor)control).IsHoming())
                    {
                        if (SysPara.SystemRun)
                        {
                            ((Motor)control).Stop();
                            JSDK.Alarm.Show("3134", control.FindForm().Name + " Module ：" + control.Name + "  hardware negative limit alarm!");
                        }
                        //JSDK.Alarm.Show("3001", control.FindForm().Name + " Module ：" + control.Name + "  hardware negative limit alarm!");
                    }

                }
            }
        }

        public static void InitialParameterReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.InitialParameterReset();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2013", "An unpredictable error occurred on ExecuteReset() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2013", ex.ToString());
                    MiddleLayer.FlowCtrl.InitialReset();
                    StopRun();
                }
            }
        }

        public static void InitialReset()
        {
            SysPara.CFX.WorkTerminate();
            SysPara.CFX.SystemTimeSync();
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.InitialReset();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2014", "An unpredictable error occurred on InitialReset() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2014", ex.ToString());
                    MiddleLayer.FlowCtrl.InitialReset();
                    StopRun();
                }
            }
        }

        public static void ServoOn()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.ServoOn();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2011", "An unpredictable error occurred on ServoOn() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2011", ex.ToString());
                    StopRun();
                }
            }
        }

        public static void StartUp()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.StartUp();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2011", "An unpredictable error occurred on StartUp() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2011", ex.ToString());
                    StopRun();
                }
            }
        }

        public static void ServoOff()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.ServoOff();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2012", "An unpredictable error occurred on ServoOff() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2012", ex.ToString());
                    StopRun();
                }
            }
        }

        public static void SetSpeed(int SpeedRate)
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.SetSpeed(SpeedRate);
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2031", "An unpredictable error occurred on SetSpeed() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2031", ex.ToString());
                    StopRun();
                }
            }
        }

        public static bool Initial()
        {
            bool bInitailOk = true;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.Initial();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2015", "An unpredictable error occurred on Initial() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2015", ex.ToString());
                    StopRun();
                }
            }
            return bInitailOk;
        }

        public static bool GetInitialOk()
        {
            bool bInitialOk = true;
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                bInitialOk &= Module.GetInitialOk();
            return bInitialOk;
        }

        public static void RunReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.RunReset();
                }
                catch (Exception ex)
                {
                    JSDK.Alarm.Show("2016", "An unpredictable error occurred on RunReset() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2016", ex.ToString());
                    StopRun();
                }
            }
        }

        public static void Run()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    Module.Run();
                }
                catch (Exception ex)
                {
                    string text = ex.ToString();
                    JSDK.Alarm.Show("2017", "An unpredictable error occurred on Run() ! ModuleName=\"" + Module.Name + "\"");
                    MainF.AddAlarmToolTip("2017", ex.ToString());
                    StopRun();
                }
            }
        }
        public static void SetModulesRun()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    if (!Module.GetModuleRun())
                    {
                        Module.SetModuleRun(true);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void SetModulesStop()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                try
                {
                    if (!Module.GetModuleRun())
                    {
                        Module.SetModuleRun(false);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        //Zax 9/1/21- Module Multi Thread
        //public static void Run()
        //{
        //    for (int x = 0; x < ModuleManager.ModuleList.Count; x++)
        //    {
        //        ModuleBaseForm Module = ModuleManager.ModuleList[x] as ModuleBaseForm;
        //        try
        //        {
        //            if (RunTask[x] != null &&
        //               (RunTask[x].Status == TaskStatus.RanToCompletion || RunTask[x].Status == TaskStatus.Faulted))
        //            {
        //                RunTask[x].Dispose();
        //                RunTask[x] = null;
        //            }
        //            if (RunTask[x] == null)
        //            {
        //                RunTask[x] = Task.Factory.StartNew(() =>
        //                {
        //                    Module.Run();
        //                });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string text = ex.ToString();
        //            JSDK.Alarm.Show("2017", "An unpredictable error occurred on Run() ! ModuleName=\"" + Module.Name + "\"");
        //            MainF.AddAlarmToolTip("2017", ex.ToString());
        //            StopRun();
        //        }
        //    }
        //}

        public static void MotorAlarmReset()
        {
            foreach (ControlBaseInterface control in JSDK.ControlList)
                if (control is Motor)
                    if (((Motor)control).GetAxisIOStatus().ALM)
                        ((Motor)control).AlarmReset();

            //For ABB Robot control
            //AbbF.ABB_Robot.EmoReset();
            //if(AbbF.ABB_Robot.ALM)
            //{
            //    AbbF.ABB_Robot.ServoOn();
            //    AbbF.ABB_Robot.ResetPointer();
            //    AbbF.ABB_Robot.StartRapid();
            //}
        }

        public static bool OpenRecipe(string RecipePath)
        {
            SysPara.isRecipeRefresh = true;
            bool bOpenSuccess = false;
            try
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ReadRecipeData(RecipePath);
                SysPara.RecipeDataDirectory = Path.GetDirectoryName(RecipePath);
                SysPara.RecipeName = Path.GetFileNameWithoutExtension(RecipePath);
                SysPara.CFX.RecipeActivated(SysPara.RecipeName);
                // VisionF.LoadConfig(string.Format("{0}\\{1}\\{2}.xml", SysPara.RecipeDataDirectory, "Vision", SysPara.RecipeName + "_Vision"));
                SystemF.MesF.LoadMesConfig(SysPara.MESDirectory + "\\" + SysPara.RecipeName);
                //  OpenVision();
                //  coreEngine.OpenRVision();//teong
                IniFile IniFile = new IniFile(".\\MachineSetup.ini");
                IniFile.WriteString("MachineSetup", "RecipeName", SysPara.RecipeName);
                IniFile.WriteString("PathSetup", "RecipeDirectory", SysPara.RecipeDataDirectory.Replace(System.IO.Directory.GetCurrentDirectory() + "\\", ".\\"));
                bOpenSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SysPara.RecipeDataDirectory = ".\\ModuleData\\RecipeData";
                SysPara.RecipeName = "Recipe";
                bOpenSuccess = false;
            }

            //if (!SystemF.MesF.LoadMesTisConfig(SysPara.MESDirectory + "\\" + SysPara.RecipeName))
            //{
            //    JSDK.Alarm.Show("3006");
            //}


            SysPara.isRecipeRefresh = false;
            return bOpenSuccess;
        }

        public static bool OpenSmallRecipe(string RecipePath)
        {
            SysPara.isRecipeRefresh = true;
            bool bOpenSuccess = false;
            try
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].ReadRecipeData(RecipePath);
                SysPara.RecipeDataDirectory = Path.GetDirectoryName(RecipePath);
                SysPara.RecipeName = Path.GetFileNameWithoutExtension(RecipePath);

                bOpenSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SysPara.RecipeDataDirectory = ".\\ModuleData\\RecipeData";
                SysPara.RecipeName = "Recipe";
                bOpenSuccess = false;
            }

            //if (!SystemF.MesF.LoadMesTisConfig(SysPara.MESDirectory + "\\" + SysPara.RecipeName))
            //{
            //    JSDK.Alarm.Show("3006");
            //}


            SysPara.isRecipeRefresh = false;
            return bOpenSuccess;
        }
    }
}
