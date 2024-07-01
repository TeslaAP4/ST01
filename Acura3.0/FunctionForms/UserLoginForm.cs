using Acura3._0.Classes;
using JTESW.BLL.Base;
using JTESW.JTESW_WS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
//using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    public partial class UserLoginForm : Form
    {
        //private Task CheckCaptureTask;
        private CancellationTokenSource StopTask = new CancellationTokenSource();
        private const int maxLoginAttempt = 3;
        private const int accountLockDuration = 15;
        private string checkUserLogin = null;

        public UserLoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PermissionType UserPermission = PermissionType.None;
            #region Old Local Access - Deprecated
            //if (ReadUserData(textUserName.Text, textPassword.Text, ref UserPermission))
            //{
            //    SysPara.UserName = textUserName.Text;
            //    MiddleLayer.SwitchPermission(UserPermission);

            //    //MiddleLayer.LogF.AddLog(LogType.Operation, string.Format("User Login. UserType:{0} UserName:{1}", SysPara.LoginLevel.ToString(), SysPara.LoginUserName));
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Login fail, Please check username and password.");
            //    textUserName.Focus();
            //}
            #endregion

            //Zax 7/27/21
            #region NTLogin Access
            if (checkUserLogin != textUserName.Text.Trim())
            {
                SysPara.FailedLoginCount = null;
                checkUserLogin = textUserName.Text.Trim();
            }

            if (string.IsNullOrEmpty(textUserName.Text.Trim()) || string.IsNullOrEmpty(textPassword.Text.Trim()))
            {
                MessageBox.Show("Login fail, Please check username and password.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textUserName.Focus();
                return;
            }
            //else if (IsLDAPLock(textUserName.Text.Trim()))
            //{
            //    textUserName.Focus();
            //    return;
            //}
            else if (IsAccountLock(textUserName.Text.Trim()))
            {
                textUserName.Focus();
                return;
            }

            if (isAuthenticate(textUserName.Text.Trim(), textPassword.Text.Trim()))
            {
                if (ReadUserData(textUserName.Text.Trim(), ref UserPermission))
                {
                    SysPara.UserName = textUserName.Text.Trim();
                    MiddleLayer.SwitchPermission(UserPermission);

                    SysPara.FailedLoginCount = null;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login fail, Username not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textUserName.Focus();
                }
            }
            else if (ReadUserData(textUserName.Text.Trim(), textPassword.Text.Trim(), ref UserPermission))
            {
                SysPara.UserName = textUserName.Text.Trim();
                MiddleLayer.SwitchPermission(UserPermission);

                SysPara.FailedLoginCount = null;
                this.Close();
            }
            else
            {
                //if (IsLDAPLock(textUserName.Text.Trim()))
                //{
                //    textUserName.Focus();
                //    return;
                //}
                ValidateAccountLock();
                textUserName.Focus();
            }
            #endregion
        }

        #region NTLogin Methods

        private bool isAuthenticate(string UserName, string Password)
        {
            bool _authenticated = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(SysPara.WebService))
                {
                    SoapClientBLL soapClientBLL = new SoapClientBLL(SysPara.WebService);
                    _authenticated = soapClientBLL.isAuthenticate(UserName, Password);
                }
            }
            catch { }
            return _authenticated;
        }

        private void ValidateAccountLock()
        {
            int failedLoginCount = 0;
            if (SysPara.FailedLoginCount == null)
            {
                SysPara.FailedLoginCount = "0";
            }

            int.TryParse(SysPara.FailedLoginCount.ToString(), out failedLoginCount);
            failedLoginCount++;
            SysPara.FailedLoginCount = failedLoginCount.ToString();
            SysPara.LastFailedLogin = DateTime.Now;

            if (failedLoginCount >= maxLoginAttempt)
            {
                MessageBox.Show("Access has been locked. \nPlease try again in " + accountLockDuration + " minutes.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Invalid user name or password. \nYou have " + (maxLoginAttempt - failedLoginCount) + " attempt" + ((maxLoginAttempt - failedLoginCount) <= 1 ? "" : "s") + " left.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsAccountLock(string username)
        {
            int failedLoginCount = 0;
            DateTime LastFailedLogin;
            if (SysPara.FailedLoginCount == null)
            {
                return false;
            }

            int.TryParse(SysPara.FailedLoginCount.ToString(), out failedLoginCount);
            LastFailedLogin = Convert.ToDateTime(SysPara.LastFailedLogin);

            if (failedLoginCount >= maxLoginAttempt)
            {
                if (LastFailedLogin.AddMinutes(accountLockDuration) >= DateTime.Now)
                {
                    MessageBox.Show("Access has been locked. \nPlease try again in " + accountLockDuration + " minutes.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                else
                {
                    SysPara.FailedLoginCount = null;
                    SysPara.LastFailedLogin = DateTime.Now;
                }
            }

            return false;
        }

        private bool IsLDAPLock(string username)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                {
                    string ldapName = username;
                    if (!username.ToLower().StartsWith(@"jabil\"))
                    {
                        ldapName = @"jabil\" + username;
                    }

                    using (UserPrincipal up = UserPrincipal.FindByIdentity(ctx, ldapName))
                    {

                        if (up.AccountLockoutTime != null)
                        {
                            if (up.AccountLockoutTime.Value.AddMinutes(accountLockDuration) > DateTime.UtcNow)
                            {
                                MessageBox.Show("Your NTLogin had been locked. /nPlease try again in " + accountLockDuration + " minutes or contact IT to unlock your NTLogin", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return true;
                            }
                        }

                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private bool ReadUserData(string UserName, string Password, ref PermissionType UserPermission)
        {
            try
            {
                string strSQL = "select * from UserData where UserName = '" + UserName + "'";
                bool Successful = false;

                DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
                if (Successful)
                    if (readData.Rows.Count > 0)
                        if (readData.Rows[0]["Password"].ToString() == Password)
                        {
                            UserPermission = (PermissionType)Enum.Parse(typeof(PermissionType), readData.Rows[0]["Permission"].ToString());
                            return true;
                        }
            }
            catch (Exception) { }
            return false;
        }

        private bool ReadUserData(string UserName, ref PermissionType UserPermission)
        {
            try
            {
                string strSQL = "select * from UserData where UserName = '" + UserName + "'";
                bool Successful = false;

                DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
                if (Successful)
                    if (readData.Rows.Count > 0)
                    {
                        UserPermission = (PermissionType)Enum.Parse(typeof(PermissionType), readData.Rows[0]["Permission"].ToString());
                        return true;
                    }
            }
            catch (Exception) { }
            return false;
        }

        private void textPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin.PerformClick();
        }

        //For Debug use only
        private void pBTitleUserLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                SysPara.UserName = "Administrator";
                MiddleLayer.SwitchPermission(PermissionType.Administrator);
                Close();
            }
        }

        private void UserLoginForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (SysPara.UseFingerprint)
                {
                    ResetFingerprintDB();
                    MiddleLayer.FingerprintCaptureF.StartVerification();
                    //CheckHaveNewCapture();
                }
            }
            else
            {
                if (SysPara.UseFingerprint)
                {
                    MiddleLayer.FingerprintCaptureF.StopVerification();
                    //StopTask.Cancel();
                }
            }
        }

        public void ResetFingerprintDB()
        {
            MiddleLayer.FingerprintCaptureF.ClearTemplate();
            string strSQL = "select * from UserData";
            bool Successful = false;
            DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            if (Successful)
                for (int i = 0; i < readData.Rows.Count; i++)
                {
                    string Name = readData.Rows[i]["UserName"].ToString();
                    //MiddleLayer.FingerprintCaptureF.AddTemplate(i, readData.Rows[i]["UserName"].ToString());
                }
        }

        private void OnTemplate(int UserIndex)
        {
            string strSQL = "select * from UserData";
            bool Successful = false;
            DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            if (Successful)
            {
                RefreshDifferentThreadUI(textUserName, () =>
                {
                    textUserName.Text = readData.Rows[UserIndex]["UserName"].ToString();
                    textPassword.Text = readData.Rows[UserIndex]["Password"].ToString();
                    btnLogin.PerformClick();
                });
            }
        }

        public void CheckHaveNewCapture()
        {
            //StopTask.Dispose();
            //StopTask = new CancellationTokenSource();
            //CheckCaptureTask = Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(3000);
            //    while (!StopTask.IsCancellationRequested)
            //    {
            //        if (MiddleLayer.FingerprintF.bHaveNewCapture)
            //        {
            //            MiddleLayer.FingerprintF.bHaveNewCapture = false;
            //            if (MiddleLayer.FingerprintF.IdentificationIndex != -1)
            //            {
            //                string strSQL = "select * from UserData";
            //                bool Successful = false;
            //                DataTable readData = DataBase.ReadData_Adapter(SysPara.MdbPath, strSQL, ref Successful);
            //                if (Successful)
            //                {
            //                    RefreshDifferentThreadUI(textUserName, () =>
            //                    {
            //                        textUserName.Text = readData.Rows[MiddleLayer.FingerprintF.IdentificationIndex]["UserName"].ToString();
            //                        textPassword.Text = readData.Rows[MiddleLayer.FingerprintF.IdentificationIndex]["Password"].ToString();
            //                        btnLogin.PerformClick();
            //                    });
            //                }
            //                break;
            //            }
            //            Thread.Sleep(1);
            //        }
            //    }
            //});
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

        private void UserLoginForm_Load(object sender, EventArgs e)
        {
            if (SysPara.UseFingerprint)
                MiddleLayer.FingerprintCaptureF.OnTemplate += this.OnTemplate;
        }
    }
}
