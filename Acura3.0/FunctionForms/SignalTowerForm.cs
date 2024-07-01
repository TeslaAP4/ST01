using Acura3._0.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0.Forms
{
    public partial class SignalTowerForm : Form
    {
        private List<SignalTowerData> TowerData = new List<SignalTowerData>();
        private SignalTowerStatusType SelectStatus = SignalTowerStatusType.MessageError;

        private RadioButton[] rbGroup_GreenLight_RunMode;
        private RadioButton[] rbGroup_YellowLight_RunMode;
        private RadioButton[] rbGroup_RedLight_RunMode;
        private RadioButton[] rbGroup_Buzz_RunMode;
        private RadioButton[] rbGroup_GreenLight_MaintenanceMode;
        private RadioButton[] rbGroup_YellowLight_MaintenanceMode;
        private RadioButton[] rbGroup_RedLight_MaintenanceMode;
        private RadioButton[] rbGroup_Buzz_MaintenanceMode;

        private SignalTowerStatusType PreviousStatus = SignalTowerStatusType.MachineIdle;
        public Status GreenLightStatus = 0;
        public Status YellowLightStatus = 0;
        public Status RedLightStatus = 0;
        public Status BuzzerStatus = 0;
        public bool bHaveChangeStatus = false;

        public enum SignalTowerStatusType
        {
            MachineIdle = 0,
            MachineInitialize,
            MachineRunning,
            MessageError,
            //MessageInformation, /*Hidden*/
            MessageWarning,
            MachineStarving, /*Add*/
            MachineBlocked, /*Add*/
            MachineLowMaterial, /*Add*/
            MachineRetryProcess,  /*Add*/
            MachineNoMaterial /*Add*/
        }

        public enum Status
        {
            Off = 0,
            On,
            Blink
        }

        public struct SignalTowerData
        {
            public SignalTowerStatusType SignalTowerStatus;
            public Status GreenLightStatus_R;
            public Status YellowLightStatus_R;
            public Status RedLightStatus_R;
            public Status BuzzStatus_R;
            public Status GreenLightStatus_M;
            public Status YellowLightStatus_M;
            public Status RedLightStatus_M;
            public Status BuzzerStatus_M;
        }

        public SignalTowerForm()
        {
            InitializeComponent();

            rbGroup_GreenLight_RunMode = new RadioButton[] { rbGreenOff_R, rbGreenOn_R, rbGreenBlink_R };
            rbGroup_YellowLight_RunMode = new RadioButton[] { rbYellowOff_R, rbYellowOn_R, rbYellowBlink_R };
            rbGroup_RedLight_RunMode = new RadioButton[] { rbRedOff_R, rbRedOn_R, rbRedBlink_R };
            rbGroup_Buzz_RunMode = new RadioButton[] { rbBuzzOff_R, rbBuzzOn_R, rbBuzzBlink_R };
            rbGroup_GreenLight_MaintenanceMode = new RadioButton[] { rbGreenOff_M, rbGreenOn_M, rbGreenBlink_M };
            rbGroup_YellowLight_MaintenanceMode = new RadioButton[] { rbYellowOff_M, rbYellowOn_M, rbYellowBlink_M };
            rbGroup_RedLight_MaintenanceMode = new RadioButton[] { rbRedOff_M, rbRedOn_M, rbRedBlink_M };
            rbGroup_Buzz_MaintenanceMode = new RadioButton[] { rbBuzzOff_M, rbBuzzOn_M, rbBuzzBlink_M };

            ReadAllSignalTowerData();
        }

        private void rbGroup_GreenLight_RunMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_GreenLight_RunMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_YellowLight_RunMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_YellowLight_RunMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_RedLight_RunMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_RedLight_RunMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_Buzzer_RunMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_Buzz_RunMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_GreenLight_MaintenanceMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_GreenLight_MaintenanceMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_YellowLight_MaintenanceMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_YellowLight_MaintenanceMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_RedLight_MaintenanceMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_RedLight_MaintenanceMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void rbGroup_Buzzer_MaintenanceMode_Click(object sender, MouseEventArgs e)
        {
            ChangeSelectItems(rbGroup_Buzz_MaintenanceMode, sender);
            RefreshSelectItemValue();
            WriteSignalTowerData(SelectStatus);
        }

        private void ChangeSelectItems(RadioButton[] Group, object SelectedItem)
        {
            RadioButton target = SelectedItem as RadioButton;
            foreach (RadioButton rb in Group)
                rb.Checked = false;
            target.Checked = true;
        }

        private void RefreshSelectItemValue()
        {
            try
            {
                SignalTowerData newSignalTowerData = new SignalTowerData();
                newSignalTowerData.SignalTowerStatus = SelectStatus;

                #region Get selected item value
                for (int i = 0; i < rbGroup_GreenLight_RunMode.Length; i++)
                    if (rbGroup_GreenLight_RunMode[i].Checked)
                    {
                        newSignalTowerData.GreenLightStatus_R = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_YellowLight_RunMode.Length; i++)
                    if (rbGroup_YellowLight_RunMode[i].Checked)
                    {
                        newSignalTowerData.YellowLightStatus_R = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_RedLight_RunMode.Length; i++)
                    if (rbGroup_RedLight_RunMode[i].Checked)
                    {
                        newSignalTowerData.RedLightStatus_R = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_Buzz_RunMode.Length; i++)
                    if (rbGroup_Buzz_RunMode[i].Checked)
                    {
                        newSignalTowerData.BuzzStatus_R = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_GreenLight_MaintenanceMode.Length; i++)
                    if (rbGroup_GreenLight_MaintenanceMode[i].Checked)
                    {
                        newSignalTowerData.GreenLightStatus_M = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_YellowLight_MaintenanceMode.Length; i++)
                    if (rbGroup_YellowLight_MaintenanceMode[i].Checked)
                    {
                        newSignalTowerData.YellowLightStatus_M = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_RedLight_MaintenanceMode.Length; i++)
                    if (rbGroup_RedLight_MaintenanceMode[i].Checked)
                    {
                        newSignalTowerData.RedLightStatus_M = (Status)i;
                        break;
                    }
                for (int i = 0; i < rbGroup_Buzz_MaintenanceMode.Length; i++)
                    if (rbGroup_Buzz_MaintenanceMode[i].Checked)
                    {
                        newSignalTowerData.BuzzerStatus_M = (Status)i;
                        break;
                    }
                #endregion

                TowerData[TowerData.FindIndex((SingalTowerData) => SingalTowerData.SignalTowerStatus == SelectStatus)] = newSignalTowerData;
            }
            catch (Exception) { }
        }

        private bool ReadAllSignalTowerData()
        {
            try
            {
                bool Success = false;
                DataTable ReadDate = DataBase.ReadAllData_Adapter("SignalTowerData", "SystemData.mdb", ref Success);
                if (Success)
                {
                    TowerData.Clear();
                    for (int i = 0; i < ReadDate.Rows.Count; i++)
                    {
                        SignalTowerData tmpSignalData = new SignalTowerData();
                        tmpSignalData.SignalTowerStatus = (SignalTowerStatusType)Enum.Parse(typeof(SignalTowerStatusType), ReadDate.Rows[i]["SignalTowerStatus"].ToString());
                        tmpSignalData.GreenLightStatus_R = (Status)Convert.ToInt32(ReadDate.Rows[i]["Green_R"]);
                        tmpSignalData.YellowLightStatus_R = (Status)Convert.ToInt32(ReadDate.Rows[i]["Yellow_R"]);
                        tmpSignalData.RedLightStatus_R = (Status)Convert.ToInt32(ReadDate.Rows[i]["Red_R"]);
                        tmpSignalData.BuzzStatus_R = (Status)Convert.ToInt32(ReadDate.Rows[i]["Buzzer_R"]);
                        tmpSignalData.GreenLightStatus_M = (Status)Convert.ToInt32(ReadDate.Rows[i]["Green_M"]);
                        tmpSignalData.YellowLightStatus_M = (Status)Convert.ToInt32(ReadDate.Rows[i]["Yellow_M"]);
                        tmpSignalData.RedLightStatus_M = (Status)Convert.ToInt32(ReadDate.Rows[i]["Red_M"]);
                        tmpSignalData.BuzzerStatus_M = (Status)Convert.ToInt32(ReadDate.Rows[i]["Buzzer_M"]);
                        TowerData.Add(tmpSignalData);
                    }
                }
                return Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool WriteAllSignalTowertData()
        {
            for (int row = 0; row < TowerData.Count; row++)
            {
                string strSQL = "update SignalTowerData set " +
                           "[Green_R]=" + (int)TowerData[row].GreenLightStatus_R +
                          ",[Yellow_R]=" + (int)TowerData[row].YellowLightStatus_R +
                          ",[Red_R]=" + (int)TowerData[row].RedLightStatus_R +
                          ",[Buzzer_R]=" + (int)TowerData[row].BuzzStatus_R +
                          ",[Green_M]=" + (int)TowerData[row].GreenLightStatus_M +
                          ",[Yellow_M]=" + (int)TowerData[row].YellowLightStatus_M +
                          ",[Red_M]=" + (int)TowerData[row].RedLightStatus_M +
                          ",[Buzzer_M]=" + (int)TowerData[row].BuzzerStatus_M +
                          " where [SignalTowerStatus]= \"" + TowerData[row].SignalTowerStatus.ToString() + "\"";

                int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
                if (result != 0)
                    return false;
            }
            return true;
        }

        private bool WriteSignalTowerData(SignalTowerStatusType Type)
        {
            int ListIndex = TowerData.FindIndex((SingalTowerData) => SingalTowerData.SignalTowerStatus == Type);
            string strSQL = "update SignalTowerData set " +
                       "[Green_R]=" + (int)TowerData[ListIndex].GreenLightStatus_R +
                      ",[Yellow_R]=" + (int)TowerData[ListIndex].YellowLightStatus_R +
                      ",[Red_R]=" + (int)TowerData[ListIndex].RedLightStatus_R +
                      ",[Buzzer_R]=" + (int)TowerData[ListIndex].BuzzStatus_R +
                      ",[Green_M]=" + (int)TowerData[ListIndex].GreenLightStatus_M +
                      ",[Yellow_M]=" + (int)TowerData[ListIndex].YellowLightStatus_M +
                      ",[Red_M]=" + (int)TowerData[ListIndex].RedLightStatus_M +
                      ",[Buzzer_M]=" + (int)TowerData[ListIndex].BuzzerStatus_M +
                      " where [SignalTowerStatus]= \"" + TowerData[ListIndex].SignalTowerStatus.ToString() + "\"";

            int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
            if (result != 0)
                return false;
            return true;
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            SelectStatus = (SignalTowerStatusType)Enum.Parse(typeof(SignalTowerStatusType), ((RadioButton)sender).Tag.ToString());
            int ListIndex = TowerData.FindIndex((SingalTowerData) => SingalTowerData.SignalTowerStatus == SelectStatus);
            if (ListIndex == -1) return;
            ChangeSelectItems(rbGroup_GreenLight_RunMode, rbGroup_GreenLight_RunMode[(int)TowerData[ListIndex].GreenLightStatus_R]);
            ChangeSelectItems(rbGroup_YellowLight_RunMode, rbGroup_YellowLight_RunMode[(int)TowerData[ListIndex].YellowLightStatus_R]);
            ChangeSelectItems(rbGroup_RedLight_RunMode, rbGroup_RedLight_RunMode[(int)TowerData[ListIndex].RedLightStatus_R]);
            ChangeSelectItems(rbGroup_Buzz_RunMode, rbGroup_Buzz_RunMode[(int)TowerData[ListIndex].BuzzStatus_R]);
            ChangeSelectItems(rbGroup_GreenLight_MaintenanceMode, rbGroup_GreenLight_MaintenanceMode[(int)TowerData[ListIndex].GreenLightStatus_M]);
            ChangeSelectItems(rbGroup_YellowLight_MaintenanceMode, rbGroup_YellowLight_MaintenanceMode[(int)TowerData[ListIndex].YellowLightStatus_M]);
            ChangeSelectItems(rbGroup_RedLight_MaintenanceMode, rbGroup_RedLight_MaintenanceMode[(int)TowerData[ListIndex].RedLightStatus_M]);
            ChangeSelectItems(rbGroup_Buzz_MaintenanceMode, rbGroup_Buzz_MaintenanceMode[(int)TowerData[ListIndex].BuzzerStatus_M]);
        }

        private void SignalTowerForm_Load(object sender, EventArgs e)
        {
            radioButton30.PerformClick();
        }

        public void SwitchSignalTowerStatus(SignalTowerStatusType SignalTowerStatus)
        {
            for (int i = 0; i < TowerData.Count; i++)
            {
                if (TowerData[i].SignalTowerStatus == SignalTowerStatus)
                {
                    if (SysPara.IsMaintenanceMode)
                    {
                        GreenLightStatus = TowerData[i].GreenLightStatus_M;
                        YellowLightStatus = TowerData[i].YellowLightStatus_M;
                        RedLightStatus = TowerData[i].RedLightStatus_M;
                        BuzzerStatus = TowerData[i].BuzzerStatus_M;
                    }
                    else
                    {
                        GreenLightStatus = TowerData[i].GreenLightStatus_R;
                        YellowLightStatus = TowerData[i].YellowLightStatus_R;
                        RedLightStatus = TowerData[i].RedLightStatus_R;
                        BuzzerStatus = TowerData[i].BuzzStatus_R;
                    }
                    break;
                }
            }
            if (PreviousStatus != SignalTowerStatus)
            {
                PreviousStatus = SignalTowerStatus;
                bHaveChangeStatus = true;
            }
        }
    }
}
