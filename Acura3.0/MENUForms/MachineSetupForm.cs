using Acura3._0.Forms;
using AcuraLibrary.Forms;
using AcuraLibrary;
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
    public partial class MachineSetupForm : Form
    {
        public MachineSetupForm()
        {
            InitializeComponent();
        }

        private void MachineSetupForm_Load(object sender, EventArgs e)
        {
            MiddleLayer.SignalTowerF.TopLevel = false;
            MiddleLayer.SignalTowerF.Parent = plSignalTowerSetup;
            MiddleLayer.SignalTowerF.Show();
            plSignalTowerSetup.Height = MiddleLayer.SignalTowerF.Height;

            tcMotorSetup.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plMotionSetup.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plMotionSetup.Parent = tg;
                    tcMotorSetup.TabPages.Add(tg);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            label2.Focus();
            bool isSuccess = false;
            try { 
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].BeforeProductionSetting();

                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsSettingClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].WriteSettingData();
                    }

                if (isSuccess)
                    MessageBox.Show(new Form { TopMost = true }, "Saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(new Form { TopMost = true }, "ERROR: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SysPara.isSettingRefresh = false;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            label2.Focus();
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].ReadSettingData();
        }
    }
}
