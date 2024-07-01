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

namespace Acura3._0.MENUForms
{
    public partial class UserSettingForm : Form
    {
        public UserSettingForm()
        {
            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(PermissionType)))
            {
                if (item.ToString() != "None")
                {
                    cbSelectedPermission.Items.Add(item);
                    cbAddUserPermission.Items.Add(item);
                }
            }
            cbSelectedPermission.SelectedIndex = 0;
            cbAddUserPermission.SelectedIndex = 0;
            ReadAllUserData();
            ReadPermission();
        }

        private void ReadAllUserData()
        {
            try
            {
                string strSQL = "select UserName,Permission from UserData";
                bool Successful = false;

                DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
                DataColumn FillCol = new DataColumn();
                readData.Columns.Add(FillCol);
                if (Successful)
                {
                    dgvUserList.DataSource = readData;
                    dgvUserList.Columns[0].Width = 250;
                    dgvUserList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvUserList.Columns[2].Width = 0;
                    dgvUserList.Columns[2].HeaderText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ReadPermission()
        {
            string strSQL = "select * from PermissionSetup";
            bool Successful = false;
            DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            if (Successful)
            {
                if (readData.Rows.Count > 0)
                {
                    dgvRightsConfig.DataSource = readData;
                    dgvRightsConfig.ClearSelection();
                    dgvRightsConfig.Columns[0].ReadOnly = true;
                    for (int i = 0; i < readData.Columns.Count; i++)
                    {
                        dgvRightsConfig.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRightsConfig.Columns[i].Width = 150;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool WritePermission()
        {
            bool IsUserExist = false;
            for (int row = 0; row < dgvRightsConfig.Rows.Count; row++)
            {
                string strSQL = "update PermissionSetup set ";
                for (int column = 1; column < dgvRightsConfig.Columns.Count; column++)
                    strSQL += String.Format("[{0}]={1}{2}", dgvRightsConfig.Columns[column].Name, dgvRightsConfig[column, row].Value, (column == (dgvRightsConfig.Columns.Count - 1)) ? "" : ",");
                strSQL += " where [Permission]= '" + dgvRightsConfig[0, row].Value.ToString() + "'";
                int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
                if (result != 0)
                {
                    IsUserExist = false;
                    break;
                }
                else
                {
                    IsUserExist = true;
                }
            }
            return IsUserExist;
        }

        private void dgvUserList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvUserList.CurrentCell != null)
            {
                if (dgvUserList.CurrentCell.RowIndex >= 0)
                {
                    int tmpIndex = Array.IndexOf(Enum.GetNames(typeof(PermissionType)), dgvUserList[1, dgvUserList.CurrentCell.RowIndex].Value.ToString());
                    cbSelectedPermission.SelectedIndex = tmpIndex;
                    textSelectedUserName.Text = dgvUserList[0, dgvUserList.CurrentCell.RowIndex].Value.ToString();
                    btnDeleteUser.Enabled = true;
                    btnModifyPermission.Enabled = true;
                    return;
                }
            }
            cbSelectedPermission.SelectedIndex = -1;
            textSelectedUserName.Text = "";
            btnDeleteUser.Enabled = false;
            btnModifyPermission.Enabled = false;
        }

        private void btnUserModify_Click(object sender, EventArgs e)
        {
            ModifyUserPermission(textSelectedUserName.Text, cbSelectedPermission.SelectedItem.ToString());
            ReadAllUserData();
        }

        public bool ModifyUserPermission(string UserName, string UserPermission)
        {
            try
            {
                string strSQL = "update [UserData] set [Permission]='" + UserPermission + "' where [UserName] ='" + UserName + "'";
                int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception) { }
            return false;
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            DeleteUser(textSelectedUserName.Text);
            ReadAllUserData();
        }

        private bool DeleteUser(string UserName)
        {
            try
            {
                string strSQL = "delete * from UserData where UserName ='" + UserName + "'";
                int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception) { }
            return false;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (CheckUserWhetherExist(textAddUserName.Text))
            {
                MessageBox.Show("The user name already exist!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textAddUserName.Focus();
                return;
            }

            if (textPassword.Text != textPasswordConfirm.Text)
            {
                MessageBox.Show("Password not match!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textPassword.Focus();
                return;
            }

            if (SysPara.UseFingerprint)
                if (MessageBox.Show("Are you want to setup finger", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MiddleLayer.FingerprintCaptureF.RegisterName = textAddUserName.Text;
                    MiddleLayer.FingerprintCaptureF.ShowDialog();
                    if (MiddleLayer.FingerprintCaptureF.Enroller.TemplateStatus != DPFP.Processing.Enrollment.Status.Ready)
                    {
                        MessageBox.Show("Fingerprint registration failed!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            int TmpIndex = dgvUserList.Rows.Count;
            if (AddUser(textAddUserName.Text, textPassword.Text, cbAddUserPermission.SelectedItem.ToString()))
            {
                ReadAllUserData();
                textAddUserName.Text = string.Empty;
                textPassword.Text = string.Empty;
                textPasswordConfirm.Text = string.Empty;
                dgvUserList[0, TmpIndex].Selected = true;
            }
        }

        private bool CheckUserWhetherExist(string UserName)
        {
            string strSQL = "select * from UserData where UserName ='" + UserName + "'";
            bool Successful = false;

            DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            if (Successful)
                if (readData.Rows.Count > 0)
                    return true;
            return false;
        }

        private bool AddUser(string UserName, string UserPassword, string UserLevel)
        {
            try
            {
                string strSQL = "insert into UserData values ('" + UserName + "','" + UserPassword + "','" + UserLevel + "')";
                int result = DataBase.DataBaseExecute(SysPara.MdbPath, strSQL);
                if (result == 0)
                    return true;
            }
            catch (Exception) { }
            return false;
        }

        private void AddUser_TextChanged(object sender, EventArgs e)
        {
            if (textAddUserName.Text != string.Empty && textPassword.Text != string.Empty && textPasswordConfirm.Text != string.Empty)
                btnAddUser.Enabled = true;
            else
                btnAddUser.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel2.Focus();
            WritePermission();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ReadPermission();
        }
    }
}
