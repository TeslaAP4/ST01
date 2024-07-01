using AcuraLibrary;
using AcuraLibrary.Forms;
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
    public partial class ProductionSettingForm : Form
    {
        public ProductionSettingForm()
        {
            InitializeComponent();
        }

        private void ProductionSettingForm_Load(object sender, EventArgs e)
        {
            tcProductionSetting.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plProductionSetting.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plProductionSetting.Parent = tg;
                    tcProductionSetting.TabPages.Add(tg);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            bool isSuccess = false;
            try
            {
                SysPara.isSettingRefresh = true;
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].BeforeProductionSetting();

                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsSettingClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].WriteSettingData();
                    }

                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    if (ModuleManager.ModuleList[i].bIsSettingClean)
                    {
                        isSuccess = true;
                        ModuleManager.ModuleList[i].AfterProductionSetting();
                    }

                //MiddleLayer.SaveTeachPoint();
                if (isSuccess)
                    MessageBox.Show(new Form { TopMost = true },"Saved successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            panel1.Focus();
            SysPara.isSettingRefresh = true;
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                ModuleManager.ModuleList[i].ReadSettingData();

            MiddleLayer.StartUp();
            //MiddleLayer.ReadTeachPoint();
            SysPara.isSettingRefresh = false;
        }

        private void ProductionSettingForm_ParentChanged(object sender, EventArgs e)
        {
            SysPara.isSettingRefresh = true;
            if (this.Parent == null)
            {
                MiddleLayer.StopManualRun();
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                {
                    ModuleManager.ModuleList[i].ExitProductionSettingPage();
                    ModuleManager.ModuleList[i].ReadSettingData();
                }
                //MiddleLayer.ReadTeachPoint();
            }
            else
            {
                for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                    ModuleManager.ModuleList[i].IntoProductionSettingPage();
            }
            SysPara.isSettingRefresh = false;
        }
    }
}
