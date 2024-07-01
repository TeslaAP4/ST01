using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JAGConnectivty;
using JAGConnectivty.Enum;
using Acura3._0;

namespace Acura3._0.FunctionForms
{
    public enum BoardStatus
    {
        Null,
        OK,
        NG,
        AssemblyInfoTimeout,
        BoardStatusTimeout
    }

    public struct Result
    {
        public string Point;
        public string Torque;
        public string Angle;
    }

    public partial class MESForm : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MESFileHelper fileHelper;
        JAGConnectivty.Model.TarContent tarContent;
        TisHelper tisHelper;
        string AutoAssemblyPartNo = "";
        string AutoAssemblyRevNo = "";
        public bool bEnableTIS = false;
        public bool bEnableTAR = false;

        public MESForm()
        {
            InitializeComponent();
        }

        public dynamic GetRecipeValue(string TableName, string ColumnName, int RowIndex = 0)
        {
            dynamic Value = null;
            if (RecipeData.Tables[TableName].Columns.IndexOf(ColumnName) >= 0)
            {
                Value = RecipeData.Tables[TableName].Rows[RowIndex][ColumnName, DataRowVersion.Original];
                if (Value.ToString() == "")
                {
                    Value = GetDefaultValue(RecipeData.Tables[TableName].Columns[ColumnName].DataType);
                }
            }
            return Value;
        }

        private dynamic GetDefaultValue(Type tp)
        {
            dynamic dmc = null;
            if (tp == typeof(Boolean))
                dmc = false;
            else if (tp == typeof(Int16))
                dmc = 0;
            else if (tp == typeof(Int32))
                dmc = 0;
            else if (tp == typeof(Int64))
                dmc = 0;
            else if (tp == typeof(double))
                dmc = 0;
            else if (tp == typeof(byte))
                dmc = 0;
            else if (tp == typeof(string))
                dmc = "";
            return dmc;
        }

        public void WriteMesTisConfig(string RecipePath)
        {
            FileInfo fiTmp1 = new FileInfo(RecipePath);
            if (fiTmp1.Directory.Exists == false)
                fiTmp1.Directory.Create();
            RecipeData.AcceptChanges();

            DataSet ds = new DataSet();
            ds = RecipeData;
            ds.WriteXml(RecipePath);
        }

        public void LoadMesConfig(string RecipePath)
        {
            if (File.Exists(RecipePath))
            {
                DataSet ds = new DataSet();
                RecipeData.Clear();
                ds = RecipeData;
                ds.ReadXml(RecipePath);
            }
            else
            {
                WriteMesTisConfig(RecipePath);
            }
            RecipeData.AcceptChanges();
        }

        public bool WriteTAR(string SerialNumber, DateTime StartTime, DateTime EndTime, bool Result, List<Result> Data)
        {
            bool ret = false;
            try
            {               
                string LocalPath = string.Format(@"{0}\{1}\{2}.tar", Environment.CurrentDirectory, "TAR_File", SerialNumber.Replace(':','_'));
                string MESPath = string.Format(@"{0}\{1}.tar", GetRecipeValue("RSet", "str_MesNetworkPath"), SerialNumber.Replace(':', '_'));
                if (!Directory.Exists(Path.GetDirectoryName(LocalPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(LocalPath));
                bool bFileExists = File.Exists(LocalPath);
                if (!bFileExists)
                {
                    StreamWriter sw = new StreamWriter(LocalPath, false);
                    sw.Close();
                }
                string strResultData = "";
                foreach (Result item in Data)
                {
                    strResultData = strResultData + string.Format("d{0}T {1}\r\nd{2}A {3}\r\n", item.Point, item.Torque, item.Point, item.Angle);
                }
                string TARContent = string.Format("S{0}\r\nC{1}\r\nN{2}\r\nP{3}\r\nD{4}\r\nR{5}\r\nT{6}\r\nL{7}\r\n[{8}\r\n]{9}\r\nMScrew Torque & Angle\r\n{10}",
                SerialNumber, GetRecipeValue("RSet", "str_Customer"), GetRecipeValue("RSet", "str_MachineName"), GetRecipeValue("RSet", "str_ProcessStep"), "AcuRA3.0", System.Reflection.Assembly.GetEntryAssembly().GetName().Version, Result?"P":"F", GetRecipeValue("RSet", "str_LineName"), StartTime.ToString("dd/MM/yyyy HH:mm:ss"), EndTime.ToString("dd/MM/yyyy HH:mm:ss"), strResultData);
                File.WriteAllText(LocalPath, TARContent, Encoding.UTF8);
                if (GetRecipeValue("RSet", "b_EnableTarGenerate"))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(MESPath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(MESPath));
                    bool bNetworkFileExists = File.Exists(MESPath);
                    if (!bNetworkFileExists)
                    {
                        StreamWriter sw = new StreamWriter(MESPath, false);
                        sw.Close();
                    }
                    File.WriteAllText(MESPath, TARContent, Encoding.UTF8);
                    //string backup = string.Format(@"{0}\backup\{1}.tar", GetRecipeValue("RSet", "str_MesNetworkPath"), SerialNumber.Replace(':', '_'));
                    //if (!Directory.Exists(Path.GetDirectoryName(backup)))
                    //    Directory.CreateDirectory(Path.GetDirectoryName(backup));
                    //bool bExists = File.Exists(backup);
                    //if (!bExists)
                    //{
                    //    StreamWriter sw = new StreamWriter(backup, false);
                    //    sw.Close();
                    //}
                    //File.WriteAllText(backup, TARContent, Encoding.UTF8);
                }
                ret = true;
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }


        public bool LoadMesTisConfig(string RecipePath)
        {
            bool ret = false;
            bool bInitMesOK = false;
            bool bInitTisOK = false;
            if (File.Exists(RecipePath))
            {
                DataSet ds = new DataSet();
                RecipeData.Clear();
                ds = RecipeData;
                ds.ReadXml(RecipePath);
            }
            else
            {
                WriteMesTisConfig(RecipePath);
            }
            RecipeData.AcceptChanges();
            bEnableTIS = GetRecipeValue("RSet", "b_EnableTis");
            bEnableTAR = GetRecipeValue("RSet", "b_EnableTarGenerate");
            if (bEnableTAR)
            {
                bInitMesOK = InitializeMES();
                bInitTisOK = InitializeTIS();
            }
            if(bEnableTIS)
            {
                
            }
            ret = (bInitMesOK || !bEnableTAR) & (bInitTisOK || !bEnableTIS);
            return ret ;
        }
        
        public bool InitializeMES()
        {
            bool ret = false;
            try
            {
                string Addr = GetRecipeValue("RSet", "str_LdapAddress");
                fileHelper = new MESFileHelper(Addr,10);
                tarContent = new JAGConnectivty.Model.TarContent();
                ret = true;
            }
            catch(Exception e) {  }
            return ret;
        }

        public bool CreateTarFile(string SerialNum, DateTime StartTime, DateTime EndTime, string User, string RecipeName, bool Result , string LocalFilePath)
        {
            bool ret = false;
            string ErrMesg = "";
            if (GetRecipeValue("RSet", "b_EnableTarGenerate"))
            {
                try
                {
                    string encyptString = GetRecipeValue("RSet", "str_Credential");
                    string NetworkAddr = GetRecipeValue("RSet", "str_MesNetworkPath");
                    //var uri = new Uri(NetworkAddr);
                    //string path = uri.AbsoluteUri;
                    tarContent.Customer = GetRecipeValue("RSet", "str_Customer");
                    tarContent.Division = GetRecipeValue("RSet", "str_Division");
                    tarContent.MachineName = GetRecipeValue("RSet", "str_MachineName");
                    tarContent.ProcessStep = GetRecipeValue("RSet", "str_ProcessStep"); ;
                    tarContent.AssemblyPartNumber = GetRecipeValue("RSet", "str_AssemblyPartNoMode") == "Auto" ? AutoAssemblyPartNo : GetRecipeValue("RSet", "str_AssemblyPartNo");
                    tarContent.AssemblyRevision = GetRecipeValue("RSet", "str_AssemblyRevNoMode") == "Auto" ? AutoAssemblyRevNo : GetRecipeValue("RSet", "str_AssemblyRevNo");
                    tarContent.RecipeName = RecipeName;
                    tarContent.SerialNumber = SerialNum;
                    tarContent.OperatorId = User;
                    tarContent.StartTime = StartTime;
                    tarContent.EndTime = EndTime;
                    tarContent.ProcessStatus = Result ? ProcessStatus.PASS : ProcessStatus.FAIL;
                    tarContent.FailureLabel = null;
                    tarContent.FailureMessage = null;
                    //fileHelper.SaveFileToNetworkDirectory(tarContent, encyptString, @"\\jpetequake\\quake\\AutoTar");
                    fileHelper.SaveFileToNetworkDirectory(tarContent, encyptString, NetworkAddr);
                    fileHelper.SaveFileToLocalDirectory(tarContent, LocalFilePath);                     
                    fileHelper.WriteTarToDB();
                    ret = true;
                }
                catch(Exception ex) { }
            }
            else
            {
                ret = true;
            }
            return ret;          
        }

        public bool InitializeTIS()
        {
            
            bool ret = false;
            if (GetRecipeValue("RSet", "b_EnableTis") || GetRecipeValue("RSet", "str_AssemblyPartNoMode") =="Auto" || GetRecipeValue("RSet", "str_AssemblyRevNoMode") == "Auto")
            {
                try
                {
                    tisHelper = new TisHelper(GetRecipeValue("RSet", "str_TisServerAdd"), GetRecipeValue("RSet", "str_TisNamespace"));
                    ret = true;
                }
                catch { }
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        public string GetAssemblyInfo(string SerialNumber)
        {
            string ret = "NaN";
            string Customer = GetRecipeValue("RSet", "str_Customer");
            string Divison = GetRecipeValue("RSet", "str_Division");
            if (GetRecipeValue("RSet", "str_AssemblyPartNoMode") == "Auto")
            {
                Task<JAGConnectivty.Model.AssemblyBoardInfo> assemblyBoardInfo = Task.Run(() => tisHelper.GetBoardAssemblyInfo(Customer, Divison, SerialNumber));
                assemblyBoardInfo.Wait();
                AutoAssemblyPartNo = assemblyBoardInfo.Result.AssemblyNumber;
                AutoAssemblyRevNo = assemblyBoardInfo.Result.Revision;
                ret = AutoAssemblyPartNo + "," + AutoAssemblyRevNo;
            }
            else
            {
                ret = "Fixed";
            }
            return ret;
        }

        public BoardStatus GetBoardStatus(string SerialNumber, out string ExpMesg)
        {
            BoardStatus ret = BoardStatus.Null;
            string ErrMesg = "";
            bool GetAsemblyInfoComplete = false;
            bEnableTIS = GetRecipeValue("RSet", "b_EnableTis");
            
            try
            {
                string Customer = GetRecipeValue("RSet", "str_Customer");
                string Divison = GetRecipeValue("RSet", "str_Division");
                string MachineName = GetRecipeValue("RSet", "str_MachineName");
                string ProcessStep = GetRecipeValue("RSet", "str_ProcessStep");
                AutoAssemblyPartNo = "";
                AutoAssemblyRevNo = "";
                if (GetRecipeValue("RSet", "str_AssemblyPartNoMode") == "Auto")
                {
                    Task<JAGConnectivty.Model.AssemblyChassisInfo> assemblyBoardInfo = Task.Run(() => tisHelper.GetChassisAssemblyInfo(Customer, Divison, SerialNumber));
                    assemblyBoardInfo.Wait(60000);
                    GetAsemblyInfoComplete = assemblyBoardInfo.IsCompleted;
                    if(GetAsemblyInfoComplete)
                    {
                        AutoAssemblyPartNo = assemblyBoardInfo.Result.AssemblyNumber;
                        AutoAssemblyRevNo = assemblyBoardInfo.Result.Revision;
                    }
                    else
                    {
                        ret = BoardStatus.AssemblyInfoTimeout;
                    }                
                }
                if (!bEnableTIS)
                {
                    ExpMesg = ErrMesg;
                    return BoardStatus.OK;
                }
                if (bEnableTIS && GetAsemblyInfoComplete || GetRecipeValue("RSet", "str_AssemblyPartNoMode") == "Fixed")
                {
                    string AssemblyPartNo = GetRecipeValue("RSet", "str_AssemblyPartNoMode") == "Auto" ? AutoAssemblyPartNo : GetRecipeValue("RSet", "str_AssemblyPartNo");
                    Task<JAGConnectivty.Model.TisChassisStatus> tisBoardStatus = Task.Run(() => tisHelper.GetChassisTisStatus(Customer, Divison, SerialNumber, AssemblyPartNo, MachineName, ProcessStep));
                    tisBoardStatus.Wait(60000);
                    if (tisBoardStatus.IsCompleted && tisBoardStatus.Result.IsOkToRun)
                    {
                        ret = BoardStatus.OK;
                    }
                    else if(tisBoardStatus.IsCompleted && !tisBoardStatus.Result.IsOkToRun)
                    {
                        ret = BoardStatus.NG;
                    }
                    else
                    {
                        ret = BoardStatus.BoardStatusTimeout;
                    }
                }
            }
            catch (Exception e){ ErrMesg = e.ToString(); }
            ExpMesg = ErrMesg;
            return ret;
        }

        public void SendMachineStatus(MachineStatusType machineStatus)
        {
            using (ErrorStatusLogger logger = new ErrorStatusLogger())
            {
                JAGConnectivty.Model.MachineStatusModel machineStatusModel = new JAGConnectivty.Model.MachineStatusModel();
                machineStatusModel.MachineID = GetRecipeValue("RSet", "int_MachineID");
                machineStatusModel.MachineStatus = machineStatus.ToString();
                machineStatusModel.StatusDateTime = DateTime.Now;
                logger.UpdateMachineStatus(machineStatusModel);
            }
        }

        public void SendMachineError(string ErrorType, string ErrorCode)
        {
            using (ErrorStatusLogger logger = new ErrorStatusLogger())
            {
                JAGConnectivty.Model.MachineErrorModel machineErrorModel = new JAGConnectivty.Model.MachineErrorModel();
                machineErrorModel.MachineID = GetRecipeValue("RSet", "int_MachineID");
                machineErrorModel.ErrorCode = ErrorCode;
                machineErrorModel.ErrorType = ErrorType;
                machineErrorModel.ErrorDateTime = DateTime.Now;
                logger.WriteErrorLog(machineErrorModel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool IsBoardDataAvail = false;
            List<Result> resultsData = new List<Result>();
            if (MiddleLayer.ConveyorF.Board_1.SerialNumber != "" && MiddleLayer.ConveyorF.Board_1.SerialNumber != null)
            {
                IsBoardDataAvail = true;
                foreach (FastenData data in MiddleLayer.ConveyorF.Board_1.Data)
                {
                    resultsData.Add(new Result { Point = data.Point, Torque = data.Torque, Angle = data.Angle });
                }
            }
            bool ret = WriteTAR(IsBoardDataAvail?MiddleLayer.ConveyorF.Board_1.SerialNumber:"TEST-MES:TAR", IsBoardDataAvail? MiddleLayer.ConveyorF.Board_1.StartTime:DateTime.Now.AddMinutes(-10), IsBoardDataAvail?MiddleLayer.ConveyorF.Board_1.EndTime:DateTime.Now, IsBoardDataAvail?!MiddleLayer.ConveyorF.Board_1.bResultNG:true, IsBoardDataAvail?resultsData:new List<Result> { new Result {Point = "P_1", Torque = "0.1", Angle = "0.2"}, new Result { Point = "P_2", Torque = "0.3", Angle = "0.4" } , new Result { Point = "P_3", Torque = "0.5", Angle = "0.6" } });
            if(!ret)
            {
                MessageBox.Show("Write MES TAR Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void chkbEnableTis_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbEnableTis.Checked)
            {
                Tis_Server.ReadOnly = false;
                Tis_Namespace.ReadOnly = false;
            }
            else
            {
                Tis_Server.ReadOnly = true;
                Tis_Namespace.ReadOnly = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if(checkBox1.Checked)
            //{
            //    button1.Enabled = true;
            //}
            //else
            //{
            //    button1.Enabled = false;
            //}
        }

        private void chkbEnableWip_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbEnableWip.Checked)
            {
                btnSaveWip.Enabled = true;
            }
            else
            {
                btnSaveWip.Enabled = false;
            }
        }

        private void comboBox25_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox25.SelectedItem == "Fixed")
            {
                textBox25.ReadOnly = false;
            }
            else
            {
                textBox25.ReadOnly = true;
            }
        }

        private void comboBox24_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox24.SelectedItem == "Fixed")
            {
                textBox24.ReadOnly = false;
            }
            else
            {
                textBox24.ReadOnly = true;
            }
        }
    }
}
