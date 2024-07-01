using AcuraLibrary;
using JabilSDK.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    public partial class MotorJogForm : Form
    {
        public List<MotorJog> MotorJogList = new List<MotorJog>();

        public MotorJogForm()
        {
            InitializeComponent();
            AddMotorContorlPanel();
            AddMotorJogToList();
            AdjustFlowlayoutPanel(flowLayoutPanel1);
        }

        public void AddMotorContorlPanel()
        {
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
            {
                if (ModuleManager.ModuleList[i].plMotorControl.Enabled)
                {
                    flowLayoutPanel1.Controls.Add(CreateLabel(ModuleManager.ModuleList[i].Text));
                    ModuleManager.ModuleList[i].plMotorControl.Size = new Size(0, 0);
                    ModuleManager.ModuleList[i].plMotorControl.AutoSize = true;
                    flowLayoutPanel1.Controls.Add(ModuleManager.ModuleList[i].plMotorControl);
                }
            }
        }

        private Control CreateLabel(string Caption)
        {
            Label label = new Label();
            label.ForeColor = Color.White;
            label.AutoSize = false;
            label.Text = Caption;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Height = 40;
            label.Margin = new Padding(0);
            label.BackColor = AcuraLibrary.AcuraColors.Background;
            return label;
        }

        private void AdjustFlowlayoutPanel(FlowLayoutPanel flowLayoutPanel)
        {
            int MaximumWidth = panel1.Width;
            int TotalHeight = panel1.Height + 8;
            for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
            {
                if (flowLayoutPanel.Controls[i].Width >= MaximumWidth)
                    MaximumWidth = flowLayoutPanel.Controls[i].Width;
                TotalHeight += flowLayoutPanel.Controls[i].Height;
            }

            this.Width = MaximumWidth + 16;
            this.Height = TotalHeight + 16 + flowLayoutPanel.Controls.Count * 8;
            for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                flowLayoutPanel.Controls[i].Width = MaximumWidth;
        }

        private void AddMotorJogToList()
        {
            for (int i = 0; i < ModuleManager.ModuleList.Count; i++)
                AddMotorJogToListCallBack(ModuleManager.ModuleList[i].plMotorControl);
        }

        private void AddMotorJogToListCallBack(Control cl)
        {
            foreach (Control control in cl.Controls)
            {
                Type ControlType = control.GetType();
                if (ControlType == typeof(MotorJog))
                    MotorJogList.Add((MotorJog)control);
                if (control.HasChildren)
                    AddMotorJogToListCallBack(control);
            }
        }

        private void MotorJogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MiddleLayer.AbbF.ManualArmChange = false; //Zax 8/6/21
            this.Visible = false;
            e.Cancel = true;
        }

        private void MotorJogForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetupMotorSpeedRatio();
                SetupMotorMoveMode();
                SetupMotorRelativeDistance();
                //MiddleLayer.AbbF.cbZSafe.Checked = true;
                //MiddleLayer.AbbF.cbJobArm.SelectedItem = MiddleLayer.AbbF.ABB_Robot.CurrentArm == ABB_RobotControl.Arm.R ? "Right" : "Left"; //Zax 8/5/21
                //MiddleLayer.AbbF.jogArm = MiddleLayer.AbbF.ABB_Robot.CurrentArm;
                //MiddleLayer.AbbF.cbChangeHead.SelectedItem = MiddleLayer.AbbF.getCurrentHead() == -1 ? "H1" : $"H{MiddleLayer.AbbF.getCurrentHead()}"; //Zax 8/5/21
                //MiddleLayer.AbbF.ManualArmChange = true; //Zax 8/6/21
            }
        }

        public void SetupMotorSpeedRatio()
        {
            for (int i = 0; i < MotorJogList.Count; i++)
                MotorJogList[i].SpeedRatio = trackBar1.Value;
            //MiddleLayer.AbbF.ABB_Robot.SpeedRatio = trackBar1.Value;
        }

        public void SetupMotorMoveMode()
        {
            for (int i = 0; i < MotorJogList.Count; i++)
            {
                if (radioButton1.Checked)
                    MotorJogList[i].MoveMode = MotorJog.MoveModeType.Jog;
                else
                    MotorJogList[i].MoveMode = MotorJog.MoveModeType.Relative;
            }
        }

        public void SetupMotorRelativeDistance()
        {
            if (textBox1.Text == string.Empty)
                textBox1.Text = "0";
            for (int i = 0; i < MotorJogList.Count; i++)
                MotorJogList[i].RelativeDistance = Convert.ToDouble(textBox1.Text);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            SetupMotorSpeedRatio();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetupMotorMoveMode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MotorJogList.Count; i++)
                MotorJogList[i].Stop();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SetupMotorRelativeDistance();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
            {
                e.Handled = true;
            }
            else if (e.KeyChar != 45 && e.KeyChar != 8)
            {
                if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                    e.Handled = true;
                if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46)
                    e.Handled = true;
            }
        }
    }
}
