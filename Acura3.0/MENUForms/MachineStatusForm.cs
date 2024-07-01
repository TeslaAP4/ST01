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
    public partial class MachineStatusForm : Form
    {
        public MachineStatusForm()
        {
            InitializeComponent();
        }

        private void MachineStatusForm_Load(object sender, EventArgs e)
        {
            tcMachineStatus.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plMachineStatus.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    tg.Padding = new Padding(0);
                    Stage.plMachineStatus.Parent = tg;
                    tcMachineStatus.TabPages.Add(tg);
                }
            }
        }
    }
}
