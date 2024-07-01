using Acura3._0.Classes;
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
    public partial class FlowChartForm : Form
    {
        public FlowChartForm()
        {
            InitializeComponent();
        }

        private void FlowChartForm_Load(object sender, EventArgs e)
        {
            tcFlowInitial.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plFlowInitial.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plFlowInitial.Parent = tg;
                    tcFlowInitial.TabPages.Add(tg);
                  
                }
            }

            tcFlowAuto.TabPages.Clear();
            foreach (ModuleBaseForm Stage in ModuleManager.ModuleList)
            {
                if (Stage.plFlowAuto.Enabled)
                {
                    TabPage tg = new TabPage(Stage.Text);
                    Stage.plFlowAuto.Parent = tg;
                    tcFlowAuto.TabPages.Add(tg);
                    
                }
            }
          
        }

        private void FlowChartForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                if (tabControl1.Width != 0)
                    tabControl1.ItemSize = new System.Drawing.Size(tabControl1.Width / 2 - 12, 50);
        }
    }
}
