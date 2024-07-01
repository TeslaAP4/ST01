using Acura3._0.Classes;
using CFX_Adapter;
using Newtonsoft.Json;
using SASDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static SASDK.DBEngine.ucDBEquipmentState;

namespace Acura3._0
{
    public class SysPara
    {
        #region Standard Init
        //Calculate Parameter
        public static int WorkOK_Count = 0;
        public static int WorkNG_Count = 0;
        public static int UPH_Count = 0;
        public static CTRecoder CT_WorkFlow = new CTRecoder();
        public static Int64 CT_LastWorkFlow = 0;

        //System Parameter
        public static string MdbPath = "SystemData.mdb";
        public static string ProgramDir = System.IO.Directory.GetCurrentDirectory();
        public static bool Simulation;
        public static string ProjectName;
        public static string RecipeName;
        public static bool isRecipeRefresh;
        public static bool EnableJAGConnectivity;
        public static bool isBarcodeDoneToGet;
        public static bool isBtnStartPress;
        public static bool isBtnStartEnable;
        public static bool isSettingRefresh;
        //System Path
        public static string LogFilePath;
        public static string VisionFileDirectory;
        public static string AlarmTableDirectory;
        public static string SettingDataDirectory;
        public static string RecipeDataDirectory;
        public static string MESDirectory;
        public static string LanguageDataDirectory;
        public static string IOPortDirectory;
        public static string DispenserDirectory;

        //Login Info
        public static PermissionType UserPermission = PermissionType.None;//PermissionType.Administrator; 
        public static string UserName = "None";
        public static bool UseFingerprint = false;
        public static string FailedLoginCount = null;
        public static DateTime LastFailedLogin;
        public static string WebService = null;

        //Langurage
        public static LanguageType LanguageShow = LanguageType.English;
        public static List<ComponentTextInfo>[] ComponentLangurageList;

        //Flow Control
        public static bool IsMaintenanceMode = false;
        public static bool bSaftyReady = false;
        public static bool SystemRun = false;
        public static RunMode SystemMode = RunMode.IDLE;
        public static bool SystemInitialOk = false;
        public static bool IsDryRun = false;

        //Operation Time Record
        public static int ScanTime = 0;
        public static UInt64 OperationSecond = 0;
        public static UInt64 RunSecond = 0;
        public static UInt64 StopSecond = 0;
        public static DateTime StartWorkTM;
        public static DateTime EndWorkTM;
        #endregion

        #region Screw
        public static ReadDisplacement newReadCOM = new ReadDisplacement();
        #endregion

        //CFX 
        public static CFXHandler CFX = new CFXHandler();
        public static MachineState MState = new MachineState();      //Flow Control
        public static DateTime WorkStartTimeStamp = new DateTime();

        //Flow Control
        public static WIP WIP1 = new WIP(1, true, Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location));
        public static WIP WIP2 = new WIP(2, true, Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location));
        public static WIP WIP3 = new WIP(3, true, Directory.GetLastWriteTime(Assembly.GetExecutingAssembly().Location));
        public static PalletEntity Pallet = new PalletEntity(); //Zax
        public static List<Queue<PalletEntity>> PalletList = new List<Queue<PalletEntity>>() {
            new Queue<PalletEntity>(), new Queue<PalletEntity>(), new Queue<PalletEntity>(), };
        public static Queue<PalletEntity> PalletList_M2M = new Queue<PalletEntity>();
        public static List<ProductEntity> Tray1List = new List<ProductEntity>() {
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity()};
        public static List<ProductEntity> Tray2List = new List<ProductEntity>() {
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity()};
        //public static List<Queue<ProductEntity>> Tray2List = new List<Queue<ProductEntity>>() {
        //    new Queue<ProductEntity>(), new Queue<ProductEntity>(), new Queue<ProductEntity>(),new Queue<ProductEntity>(),
        //    new Queue<ProductEntity>(), new Queue<ProductEntity>(), new Queue<ProductEntity>(),new Queue<ProductEntity>(),
        //    new Queue<ProductEntity>(), new Queue<ProductEntity>(), new Queue<ProductEntity>(),new Queue<ProductEntity>()};
        public static List<ProductEntity> EF_PartList = new List<ProductEntity>() {
            new ProductEntity(), new ProductEntity(), new ProductEntity(),new ProductEntity(), };
        public static bool EndLotMode = false;
        public static object RobotLock = new object();
        public static bool isMotorAlarmFeeder1 = false;
        public static bool isMotorAlarmFeeder2 = false;
        public static int IO_OverTime = 50000;
        public static int Robot_Overtime = 50000;

        public static SystemPara Station1Para = new SystemPara();
        public static SystemPara Station2Para = new SystemPara();
        public static SystemPara Station3Para = new SystemPara();
        public static SystemPara Station4Para = new SystemPara();
    }
    public class MachineState
    {
        public bool IsMaintenanceMode = false;
        public bool IsMachineBlocked = false;
        public bool IsMachineStarving = false;
        public bool IsMachineLowMaterial = false;
        public bool IsMachineNoMaterial = false;
        public bool IsMachineRetryProcess = false;

        public void ResetAll()
        {
            IsMachineBlocked = false;
            IsMachineStarving = false;
            IsMachineLowMaterial = false;
            IsMachineNoMaterial = false;
            IsMachineRetryProcess = false;
        }
    }

    public class SystemPara
    {
        public  String LastCT;
        public  String LastTaktTime;
        public  String CurrCT;
        public  String CurrTaktTime;
    }
    public struct ComponentTextInfo
    {
        public string FormName;
        public string ComponentName;
        public string ComponentText;
        public Control Component;
    }

    public struct IOPortInfo
    {
        public string FormName;
        public string ComponentName;
        public string Port;
    }

    public struct Pos3D
    {
        public double x;
        public double y;
        public double z;
        public double w;
    }

    public struct CalibrationData
    {
        public double PixelX;
        public double PixelY;
        public double MotorPosX;
        public double MotorPosY;
    }

    public enum PermissionType
    {
        Operator = 0,
        Maintenance,
        Administrator,
        None,
    }

    public enum LanguageType
    {
        Chinese = 0,
        English,
    }

    public enum RunMode
    {
        IDLE = 0,
        RUN,
        INITIAL,
    }

    public enum ResultCode
    {
        OK = 0,
        NG,
        Overtime,
        VisonNG,
        FastenNG
    }

    public enum MachineStatusType
    {
        AcuraExecution,
        AcuraShutdown,
        MachineIdle,
        MachineInitialization,
        MachineRun,
        MachineDown,
        WaitingForProducts,
    }

    public enum ToolConfig
    {
        None,
        Vusion_1_6,
        Vusion_2_2,
        Vusion_2_6,

    }

    [Serializable]
    public class PalletEntity //Zax
    {
        [JsonProperty(Order = 1)]
        public string PalletID { get; set; } = "Unknown_Pallet";
        [JsonProperty(Order = 2)]
        public bool Status { get; set; } = true; //True = PASS as default
        [JsonProperty(Order = 3)]
        public List<ProductEntity> Products { get; set; } = new List<ProductEntity> {
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity(),
            new ProductEntity(), new ProductEntity(), new ProductEntity(), new ProductEntity()
        };
        [JsonProperty(Order = 4)]
        public DateTime StartTime { get; set; } = DateTime.Now;
        [JsonProperty(Order = 5)]
        public DateTime EndTime { get; set; } = DateTime.Now;
        [JsonProperty(Order = 6)]
        public string FailMsg = ""; //Zax 8/12/21 - Required for SES II ST04
    }

    [Serializable]
    public class ProductEntity //Zax
    {
        [JsonProperty(Order = 1)]
        public bool Status { get; set; } = true; //TRUE = PASS as default
        [JsonProperty(Order = 2)]
        public string SerialNumber { get; set; } = "Unknown_Product";
        [JsonProperty(Order = 3)]
        public DateTime StartTime { get; set; } = DateTime.Now;
        [JsonProperty(Order = 4)]
        public DateTime EndTime { get; set; } = DateTime.Now;
        [JsonProperty(Order = 5)]
        public string FailMsg = ""; //Zax 8/12/21 - Required for SES II ST04
    }

    public enum eFeederType
    {
        None,
        Left,
        Right
    }

    public struct VisionResult
    {
        public bool ScanPass;
        public bool IsEmpty;
        public bool CorrectOrientation;
        public double X;
        public double Y;
        public double W;
        public bool BarcodePass;
        public string Barcode;
        public bool Broken;
    }

    public class TurretHead
    {
        public List<TurretHeadModel> turretHeads = new List<TurretHeadModel>();
        private int _activeHead;

        public TurretHead()
        {
            for (int x = 0; x < 4; x++)
            {
                turretHeads.Add(new TurretHeadModel());
            }

        }

        public void SetPartPreset(int index, bool state)
        {
            turretHeads[index].isPartPresent = state;
        }

        public void SetRejectEmpty(int index, bool state)
        {
            turretHeads[index].isPartReject = state;
        }

        public double IndexTHead(double Head1, double Target)
        {
            return Head1 + Target;
        }

        public double GetPresentHeadLocation(double Head1Location, double Head2Offset, double Head3Offset, double Head4Offset)
        {
            double target = -1;
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    SetActiveHead(x);
                    switch (x)
                    {
                        case 0:
                            target = Head1Location;
                            break;
                        case 1:
                            target = IndexTHead(Head1Location, Head2Offset);
                            break;
                        case 2:
                            target = IndexTHead(Head1Location, Head3Offset);
                            break;
                        case 3:
                            target = IndexTHead(Head1Location, Head4Offset);
                            break;

                    }
                    break;
                }
            }
            return target;
        }

        public int GetPresentHeadIndex()
        {
            int target = -1;
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    return x;
                }
            }
            return target;
        }

        public double GetEmptyHeadLocation(double Head1Location,
                                           double Head2Offset,
                                           double Head3Offset,
                                           double Head4Offset)
        {
            double target = double.MaxValue;
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (!turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    SetActiveHead(x);
                    switch (x)
                    {
                        case 0:
                            target = Head1Location;
                            break;
                        case 1:
                            target = IndexTHead(Head1Location, Head2Offset);
                            break;
                        case 2:
                            target = IndexTHead(Head1Location, Head3Offset);
                            break;
                        case 3:
                            target = IndexTHead(Head1Location, Head4Offset);
                            break;

                    }
                    break;
                }
            }
            return target;
        }

        public int GetEmptyHeadIndex()
        {
            int target = -1;
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (!turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    return x;
                }
            }
            return target;
        }

        public bool isReady()
        {
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (!turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    return false;
                }
            }
            return true;
        }

        public bool AtLeast1Alvaliable()
        {
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (turretHeads[x].isPartPresent && !turretHeads[x].isBypassing)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AtLeast1Reject()
        {
            for (int x = 0; x < turretHeads.Count; x++)
            {
                if (turretHeads[x].isPartReject && !turretHeads[x].isBypassing)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetActiveHead()
        {
            return _activeHead;
        }

        private void SetActiveHead(int _activeHead)
        {
            this._activeHead = _activeHead;
        }

        public string GetBarcode()
        {
            return turretHeads[_activeHead].Barcode;
        }

        public void SetResultToTurretHead(VisionResult visionResult)
        {
            turretHeads[_activeHead].Barcode = visionResult.Barcode;
        }

        public string GetBarcode(int index)
        {
            if (turretHeads[index].isBypassing)
                return "";

            return turretHeads[index].Barcode;
        }

        public class TurretHeadModel
        {
            public double offset = 0.0;
            public bool isPartPresent;
            public ToolConfig _ToolConfig = ToolConfig.None;
            public bool isBypassing = false;
            public bool isPartReject;
            public string Barcode = "";
        }

        public static class Handshake
        {
            public static _Handshake LeftFeedertoRobot = new _Handshake();
            public static _Handshake RightFeedertoRobot = new _Handshake();
            public static _FeederHandshake FeedertoRobot = new _FeederHandshake();
            public static _Handshake ConvToRobot = new _Handshake();
            public static _Handshake RobotToConv = new _Handshake();
            public static _Handshake Conv2ToConv1 = new _Handshake();
            public static _Handshake Conv3ToConv2 = new _Handshake();
            public static _Handshake TaskPick = new _Handshake();
            public static _Handshake TaskPlace = new _Handshake();
            public static _Handshake TaskVision = new _Handshake();
            public static _Handshake TaskPrecisor = new _Handshake();
            public static _Handshake TaskPurge = new _Handshake();
            public static _Handshake TaskChangeTools = new _Handshake();
            public static _Handshake RobotToCov_Init = new _Handshake();
            public static _Handshake[] Feeder = new _Handshake[2];
            public static _Handshake[] Vision = new _Handshake[2];
            public static _ConveyorHandshake[] Conveyor = new _ConveyorHandshake[3] { new _ConveyorHandshake(), new _ConveyorHandshake(), new _ConveyorHandshake() }; //Zax
        }

        public class _ConveyorHandshake //Zax
        {
            public CTRecoder CT_WorkFlow = new CTRecoder();
            public Int64 CT_LastLoadWorkFlow = 0;
            public Int64 CT_LastWorkFlow = 0;
            public _Handshake TaskUnload = new _Handshake();
            public _Handshake TaskLoad = new _Handshake();
        }

        public class _FeederHandshake
        {
            public bool CheckAvailable(eFeederType feederType)
            {
                lock (SysPara.RobotLock)
                {
                    bool ret = false;
                    if (feederType == eFeederType.Left)
                        ret = !Handshake.RightFeedertoRobot.Ready;
                    else
                        ret = !Handshake.LeftFeedertoRobot.Ready;
                    return ret;
                }
            }

            public bool Ready(eFeederType feederType)
            {
                bool ret = false;
                if (feederType == eFeederType.Left)
                    ret = Handshake.LeftFeedertoRobot.Ready;
                else
                    ret = Handshake.RightFeedertoRobot.Ready;
                return ret;
            }

            public void Ready(eFeederType feederType, bool trigger)
            {
                if (feederType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.Ready = trigger;
                else
                    Handshake.RightFeedertoRobot.Ready = trigger;
            }

            public bool ReadyPick(eFeederType feederType)
            {
                bool ret = false;
                if (feederType == eFeederType.Left)
                    ret = Handshake.LeftFeedertoRobot.ReadyPick;
                else
                    ret = Handshake.RightFeedertoRobot.ReadyPick;
                return ret;
            }

            public void ReadyPick(eFeederType feederType, bool trigger)
            {
                if (feederType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.ReadyPick = trigger;
                else
                    Handshake.RightFeedertoRobot.ReadyPick = trigger;
            }

            public bool Complete(eFeederType feederType)
            {
                bool ret = false;
                if (feederType == eFeederType.Left)
                    ret = Handshake.LeftFeedertoRobot.Complete;
                else
                    ret = Handshake.RightFeedertoRobot.Complete;
                return ret;
            }

            public void Complete(eFeederType feederType, bool trigger)
            {
                if (feederType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.Complete = trigger;
                else
                    Handshake.RightFeedertoRobot.Complete = trigger;
            }
            public bool Busy(eFeederType feederType)
            {
                bool ret = false;
                if (feederType == eFeederType.Left)
                    ret = Handshake.LeftFeedertoRobot.Busy;
                else
                    ret = Handshake.RightFeedertoRobot.Busy;
                return ret;
            }

            public void Busy(eFeederType loaderType, bool trigger)
            {
                if (loaderType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.Busy = trigger;
                else
                    Handshake.RightFeedertoRobot.Busy = trigger;
            }
            public bool Trigger(eFeederType loaderType)
            {
                bool ret = false;
                if (loaderType == eFeederType.Left)
                    ret = Handshake.LeftFeedertoRobot.Trigger;
                else
                    ret = Handshake.RightFeedertoRobot.Trigger;
                return ret;
            }

            public void Trigger(eFeederType loaderType, bool trigger)
            {
                if (loaderType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.Trigger = trigger;
                else
                    Handshake.RightFeedertoRobot.Trigger = trigger;
            }

            public void Reset(eFeederType loaderType)
            {
                if (loaderType == eFeederType.Left)
                    Handshake.LeftFeedertoRobot.Reset();
                else
                    Handshake.RightFeedertoRobot.Reset();
            }
        }

        public class _Handshake
        {
            private bool _Ready;
            private bool _ReadyPick;
            private bool _Complete;
            private bool _Busy;
            private bool _Trigger;

            public DateTime ReadyTime { get; set; } = DateTime.Now;
            public DateTime ReadyPickTime { get; set; } = DateTime.Now;
            public DateTime CompleteTime { get; set; } = DateTime.Now;
            public DateTime BusyTime { get; set; } = DateTime.Now;
            public DateTime TrigTime { get; set; } = DateTime.Now;
            public bool Ready
            {
                get { return _Ready; }
                set
                {
                    _Ready = value;
                    ReadyTime = DateTime.Now;
                }
            }

            public bool ReadyPick
            {
                get { return _ReadyPick; }
                set
                {
                    _ReadyPick = value;
                    ReadyPickTime = DateTime.Now;
                }
            }

            public bool Complete
            {
                get { return _Complete; }
                set
                {
                    _Complete = value;
                    CompleteTime = DateTime.Now;
                }
            }

            public bool Busy
            {
                get { return _Busy; }
                set
                {
                    _Busy = value;
                    BusyTime = DateTime.Now;
                }
            }

            public bool Trigger
            {
                get { return _Trigger; }
                set
                {
                    _Trigger = value;
                    TrigTime = DateTime.Now;
                }
            }

            public void Reset()
            {
                Ready = false;
                Complete = false;
                Busy = false;
                Trigger = false;

            }


        }

        public class TaktTime
        {
            private CTRecoder _CT_WorkFlow = new CTRecoder();
            public CTRecoder CT_WorkFlow
            {
                get { return _CT_WorkFlow; }
                set { _CT_WorkFlow = value; }
            }

            public Int64 CT_LastWorkFlow { get; set; } = 0;

            public void Trigger()
            {
                CT_LastWorkFlow = _CT_WorkFlow.GetCurrentTime();
                _CT_WorkFlow.Reset();
                _CT_WorkFlow.Start();
            }

            public void Reset()
            {
                CT_LastWorkFlow = 0;
                _CT_WorkFlow.Reset();
            }
        }
    }
}
