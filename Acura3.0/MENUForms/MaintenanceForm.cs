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
    public partial class MaintenanceForm : Form
    {
        public MaintenanceForm()
        {
            InitializeComponent();
        }

        private void MaintenanceForm_Load(object sender, EventArgs e)
        {
            tcMaintenance.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plMaintenance.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plMaintenance.Parent = tg;
                    tcMaintenance.TabPages.Add(tg);
                }
            }
        }
    }
}
