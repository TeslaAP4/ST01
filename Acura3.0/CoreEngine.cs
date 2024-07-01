using JabilSDK;
using JabilSDK.Controls;
using SASDK;
using SASDK.Alarm;
using SASDK.Core;
using SASDK.DBEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0
{
    public class CoreEngine
    {
        private bool bStopWork = false;
        private Thread CoreEngineThread = null;
        private ToolTip ttDuration = new ToolTip();
        private Dictionary<string, FlowChartModel> FlowDuration = new Dictionary<string, FlowChartModel>();
        public Classes.VirtualKeyboard _vKeyboard = new Classes.VirtualKeyboard(Application.ProductName);// Zax 7/19/21
        public event EventHandler<OperationLogArgs> OperationLogRaise;
        public static event EventHandler<FlowLogArgs> FlowChartEnter;
        public static event EventHandler<FlowLogArgs> FlowChartLeave;
        public event EventHandler CFXStationOnlineEvent;

        private void LogEvents_LogRaise(object sender, LogArgs e)
        {
            //Trigger Log here
        }

        public CoreEngine()
        {
            AlarmEvent.AlarmRaise += AlarmEvent_AlarmRaise;
            LogEvents.LogRaise += LogEvents_LogRaise;
        }

        public void StartThread()
        {
            Task.Factory.StartNew(() =>
             {
                 bStopWork = false;
                 CoreEngineThread = new Thread(DoWork);
                 CoreEngineThread.Name = "CoreEngine";
                 CoreEngineThread.Priority = ThreadPriority.BelowNormal;

                 MasterCore.Initialize();
                 CoreEngineThread.Start();
                 CFXStationOnlineEvent?.Invoke(null, null);
             });
        }

        private void AlarmEvent_AlarmRaise(object sender, AlarmArgs e)
        {
            JSDK.Alarm.Show(e.mCode, e.content);
        }

        public void StopThread()
        {
            try
            {
                for (int x = 0; x < MasterCore.dBEngineCore.DBEngineList.Count; x++)
                {
                    if (MasterCore.dBEngineCore.DBEngineList[x] is ucDBEquipmentState)
                    {
                        var ret = MasterCore.dBEngineCore.DBEngineList[x] as ucDBEquipmentState;
                        ret.AcuraShutDown();
                    }
                }
                if (CoreEngineThread != null)
                {
                    bStopWork = true;
                    CoreEngineThread.Join();
                }
            }
            catch (Exception ex)
            {
                LogEvents.NotifyLogRaise(null, new LogArgs
                {
                    Module = "CoreEngine",
                    SubModule = "StopThread",
                    Message = ex.Message,
                    StactTrace = ex.StackTrace,
                    Type = LogEvents.LogType.Exception
                });
            }

        }

        public void DisposeCtrl()
        {
            MasterCore.Dispose();
        }

        public void DoWork()
        {
            while (!bStopWork)
            {
                Execute();
                Thread.Sleep(100);//5
            }
        }

        private void Execute()
        {
            try
            {
                SetDBEngineMode();
                MasterCore.Refresh();
            }
            catch (Exception ex)
            {
                LogEvents.NotifyLogRaise(null, new LogArgs
                {
                    Module = "CoreEngine",
                    SubModule = "Execute",
                    Message = ex.Message,
                    StactTrace = ex.StackTrace,
                    Type = LogEvents.LogType.Exception
                });
            }
        }


        private void SetDBEngineMode()
        {
            for (int x = 0; x < MasterCore.dBEngineCore.DBEngineList.Count; x++)
            {
                MasterCore.dBEngineCore.DBEngineList[x].isAlarm = JSDK.Alarm.IsError;
                MasterCore.dBEngineCore.DBEngineList[x].isWarning = JSDK.Alarm.IsWarning;
                MasterCore.dBEngineCore.DBEngineList[x].Username = SysPara.UserName;
                MasterCore.dBEngineCore.DBEngineList[x].IsMaintenanceMode = SysPara.IsMaintenanceMode;
                MasterCore.dBEngineCore.DBEngineList[x].RecipeName = SysPara.RecipeName;
                MasterCore.dBEngineCore.DBEngineList[x].isStimulation = SysPara.Simulation;
                #region System Mode
                switch (SysPara.SystemMode)
                {
                    case RunMode.IDLE:
                    case RunMode.RUN:
                        if (!SysPara.SystemRun && SysPara.SystemMode == RunMode.IDLE && !SysPara.SystemInitialOk)
                        {
                            MasterCore.dBEngineCore.DBEngineList[x].runMode = ucDBEngineBase.RunMode.STOP;
                            break;
                        }
                        else if (!SysPara.SystemRun)
                        {
                            MasterCore.dBEngineCore.DBEngineList[x].runMode = ucDBEngineBase.RunMode.PAUSE;
                            break;
                        }
                        else if (SysPara.SystemRun)
                        {
                            MasterCore.dBEngineCore.DBEngineList[x].runMode = ucDBEngineBase.RunMode.START;
                            break;
                        }

                        if (SysPara.SystemMode == RunMode.IDLE && SysPara.SystemInitialOk)
                        {
                            MasterCore.dBEngineCore.DBEngineList[x].runMode = ucDBEngineBase.RunMode.INITCOMPLETE;
                        }
                        break;
                    case RunMode.INITIAL:
                        // inprogress
                        break;

                }
                #endregion

                if (MasterCore.dBEngineCore.DBEngineList[x] is ucDBEquipmentState)
                {
                    var EquipmentStateControl = (ucDBEquipmentState)MasterCore.dBEngineCore.DBEngineList[x];
                    if (JSDK.Alarm.IsError)
                    {
                        EquipmentStateControl.EquipmentState = ucDBEquipmentState.StateModel.DOWN;
                    }
                    else if (!JSDK.Alarm.IsError && SysPara.SystemRun && SysPara.SystemInitialOk)
                    {
                        EquipmentStateControl.EquipmentState = ucDBEquipmentState.StateModel.READY;
                    }
                    else if (EquipmentStateControl.IsMaintenanceMode || !SysPara.SystemInitialOk)
                    {
                        EquipmentStateControl.EquipmentState = ucDBEquipmentState.StateModel.SETUP;
                    }
                }

                if (MasterCore.dBEngineCore.DBEngineList[x] is ucDBEngineTowerLight)
                {
                    var twControl = (ucDBEngineTowerLight)MasterCore.dBEngineCore.DBEngineList[x];
                    twControl.TowerLighColour.Green = (MiddleLayer.SignalTowerF.GreenLightStatus == Forms.SignalTowerForm.Status.On) ? true : false;
                    twControl.TowerLighColour.GreenBlink = (MiddleLayer.SignalTowerF.GreenLightStatus == Forms.SignalTowerForm.Status.Blink) ? true : false;
                    twControl.TowerLighColour.Yellow = (MiddleLayer.SignalTowerF.YellowLightStatus == Forms.SignalTowerForm.Status.On) ? true : false;
                    twControl.TowerLighColour.YellowBlink = (MiddleLayer.SignalTowerF.YellowLightStatus == Forms.SignalTowerForm.Status.Blink) ? true : false;
                    twControl.TowerLighColour.Red = (MiddleLayer.SignalTowerF.RedLightStatus == Forms.SignalTowerForm.Status.On) ? true : false;
                    twControl.TowerLighColour.RedBlink = (MiddleLayer.SignalTowerF.RedLightStatus == Forms.SignalTowerForm.Status.Blink) ? true : false;
                }
            }

        }

        #region Control Registration
        public void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);

                if (control.GetType() == typeof(Panel))
                    GetAllControl(control, list);
                else if (control.GetType() == typeof(TableLayoutPanel))
                    GetAllControl(control, list);
                else if (control.GetType() == typeof(GroupBox))
                    GetAllControl(control, list);
                else if (control.GetType() == typeof(FlowLayoutPanel))
                    GetAllControl(control, list);
                else if (control.GetType() == typeof(TabControl))
                    GetAllControl(control, list);
                else if (control.GetType() == typeof(TabPage))
                    GetAllControl(control, list);

            }
        }

        public void SubAllControl(Form form)
        {
            List<Control> list = new List<Control>();

            GetAllControl(form, list);

            foreach (Control control in list)
            {

                if (control is CheckBox)
                {
                    (control as CheckBox).Click += CheckBox_Checked;
                }
                else if (control is Button)
                {
                    control.Click += Btn_Click;
                }
                else if (control is PictureBox)
                {
                    control.Click += Btn_Click;
                }
                else if (control is ToolStrip)
                {
                    foreach (ToolStripButton tsBtn in (control as ToolStrip).Items)
                    {
                        tsBtn.Click += ToolStripButton_Click;
                    }
                }
                else if (control is RadioButton)
                {
                    (control as RadioButton).Click += RadioBox_Checked;

                }
                else if (control is ComboBox)
                {
                    (control as ComboBox).Click += Btn_Click;

                }
                else if (control.GetType() == typeof(JabilSDK.UserControlLib.FlowChart))
                {
                    var cFlowChart = control as JabilSDK.UserControlLib.FlowChart;
                    cFlowChart.BackColorChanged += CFlowChart_BackColorChanged;
                    cFlowChart.MouseLeave += CFlowChart_MouseLeave;
                    cFlowChart.Click += CFlowChart_Click;
                    JabilSDK.UserControlLib.FlowChart cFlow = cFlowChart;
                    string key = cFlow.Name + "_" + cFlow.Text + "_" + cFlow.Location;

                    if (!FlowDuration.ContainsKey(key))
                    {
                        FlowDuration.Add(key, new FlowChartModel { TopParentName = form.Text });
                    }
                    else
                    {
                        FlowDuration[key].TopParentName = form.Text;
                    }
                }
                else if (control.GetType() == typeof(Output))
                {
                    var cvc = control as Output;

                    if (control.Controls.Count > 0)
                    {
                        var picBox = control.Controls[0];
                        picBox.Click += OutputBtn_Click;
                    }
                }
                else if (control.GetType() == typeof(Cylinder))
                {

                    if (control.Controls.Count > 0)
                    {
                        var tableLayout = control.Controls[0] as TableLayoutPanel;
                        if (tableLayout.Controls.Count > 1)
                        {
                            var button1 = tableLayout.Controls[0];
                            var button2 = tableLayout.Controls[1];
                            button1.Click += CylinderBtn_Click;
                            button2.Click += CylinderBtn_Click;
                        }

                    }
                }
                else if (control.GetType() == typeof(MotorJog))
                {
                    var cvc = control as MotorJog;
                    cvc.Click += Btn_Click;
                }
                else if (control.GetType() == typeof(TextBox))
                {
                    var cvc = control as TextBox;
                    cvc.Enter += TextBox_Enter;
                    cvc.GotFocus += TextBox_GotFocus;
                    cvc.LostFocus += TextBox_LostFocus;
                }
                else if (control.GetType() == typeof(NumericUpDown))
                {
                    var NUD = control as NumericUpDown;
                    NUD.Enter += NumericUpDown_Enter;
                    NUD.GotFocus += NumericUpDown_GotFocus;
                    NUD.LostFocus += NumericUpDown_LostFocus;
                    //cvc.GotFocus += TextBox_GotFocus;
                    //cvc.LostFocus += TextBox_LostFocus;
                }
            }
        }

        #region Flow Chart
        private void CFlowChart_MouseLeave(object sender, EventArgs e)
        {
            JabilSDK.UserControlLib.FlowChart cFlow = (JabilSDK.UserControlLib.FlowChart)sender;
            if (string.IsNullOrEmpty(ttDuration.GetToolTip(cFlow)))
            {
                ttDuration.RemoveAll();
            }
        }

        private void CFlowChart_BackColorChanged(object sender, EventArgs e)
        {
            JabilSDK.UserControlLib.FlowChart cFlow = (JabilSDK.UserControlLib.FlowChart)sender;
            string key = cFlow.Name + "_" + cFlow.Text + "_" + cFlow.Location;
            if (cFlow.BackColor == Color.FromArgb(0, 180, 230))
            {
                //FlowChart Enter
                if (!FlowDuration.ContainsKey(key))
                {
                    FlowDuration.Add(key, new FlowChartModel { StartTime = DateTime.Now, EndTime = DateTime.Now });
                    FlowChartEnter?.Invoke(sender, new FlowLogArgs
                    {
                        Module = FlowDuration[key].TopParentName,
                        StartTime = FlowDuration[key].StartTime.ToString("hh:mm:ss:fff"),
                        EndTime = FlowDuration[key].EndTime.ToString("hh:mm:ss:fff"),
                        Duration = Math.Round(FlowDuration[key].TotalTime.TotalMilliseconds, 2).ToString(),
                        Name = cFlow.Text
                    });
                }
                else
                {
                    FlowDuration[key].StartTime = DateTime.Now;
                    FlowDuration[key].EndTime = DateTime.Now;
                    FlowChartEnter?.Invoke(sender, new FlowLogArgs
                    {
                        Module = FlowDuration[key].TopParentName,
                        StartTime = FlowDuration[key].StartTime.ToString("hh:mm:ss:fff"),
                        EndTime = FlowDuration[key].EndTime.ToString("hh:mm:ss:fff"),
                        Duration = Math.Round(FlowDuration[key].TotalTime.TotalMilliseconds, 2).ToString(),
                        Name = cFlow.Text
                    });
                }

            }
            else
            {
                //FlowChart leave
                if (FlowDuration.ContainsKey(key))
                {
                    FlowDuration[key].EndTime = DateTime.Now;
                    FlowChartLeave?.Invoke(sender, new FlowLogArgs
                    {
                        Module = FlowDuration[key].TopParentName,
                        StartTime = FlowDuration[key].StartTime.ToString("hh:mm:ss:fff"),
                        EndTime = FlowDuration[key].EndTime.ToString("hh:mm:ss:fff"),
                        Duration = Math.Round(FlowDuration[key].TotalTime.TotalMilliseconds, 2).ToString(),
                        Name = cFlow.Text
                    });
                }
            }

        }

        private void CFlowChart_Click(object sender, EventArgs e)
        {
            JabilSDK.UserControlLib.FlowChart cFlow = (JabilSDK.UserControlLib.FlowChart)sender;
            string key = cFlow.Name + "_" + cFlow.Text + "_" + cFlow.Location;
            if (FlowDuration.ContainsKey(key) && ttDuration.Active)
            {

                int durationMilliseconds = 5000;
                ttDuration.Show("Duration : " + Math.Round(FlowDuration[key].TotalTime.TotalMilliseconds, 2) + " ms", cFlow, durationMilliseconds);
            }

        } 
        #endregion

        public void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has click<{((Control)sender).Name}>{((Control)sender).Text}",
                    Type = LogEvents.LogType.UserLog
                });
                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Click<{((Control)sender).Name}>{((Control)sender).Text}"
                });
            }
            catch { }

        }

        public void ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has click<{((ToolStripButton)sender).Name}>{((ToolStripButton)sender).Text}",
                    Type = LogEvents.LogType.UserLog
                });
                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Click<{((ToolStripButton)sender).Name}>{((ToolStripButton)sender).Text}"
                });
            }
            catch { }

        }

        public void CheckBox_Checked(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has change CheckBox state to <{((Control)sender).Name}><{((Control)sender).Text}><{((CheckBox)sender).Checked}>",
                    Type = LogEvents.LogType.UserLog
                });
                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Change CheckBox state to <{((Control)sender).Name}><{((Control)sender).Text}><{((CheckBox)sender).Checked}>"
                });
            }
            catch { }
        }

        public void RadioBox_Checked(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has change RadioButton state to <{((RadioButton)sender).Name}><{((RadioButton)sender).Text}><{((RadioButton)sender).Checked}>",
                    Type = LogEvents.LogType.UserLog
                });

                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Change RadioButton state to <{((RadioButton)sender).Name}><{((RadioButton)sender).Text}><{((RadioButton)sender).Checked}>"
                });
            }
            catch { }
        }

        public void OutputBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has click Output <{((Control)sender).Parent.Name}>{((Control)sender).Parent.Text}",
                    Type = LogEvents.LogType.UserLog
                });
                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Click Output <{((Control)sender).Parent.Name}>{((Control)sender).Parent.Text}"
                });
            }
            catch { }

        }

        public void CylinderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has click<{((Control)sender).Parent.Parent.Name}>{((Control)sender).Text}",
                    Type = LogEvents.LogType.UserLog
                });

                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Click<{((Control)sender).Parent.Parent.Name}>{((Control)sender).Text}"
                });
            }
            catch { }

        }

        #region TextBox
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Tag = ((TextBox)sender).Text;
            // ((TextBox)sender).TxtGetFocus();
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (((TextBox)sender).Tag.ToString() == ((TextBox)sender).Text) return;
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has Change TextBox<{((TextBox)sender).Name}> Old:{((TextBox)sender).Tag } New:{((TextBox)sender).Text}",
                    Type = LogEvents.LogType.UserLog
                });

                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Change TextBox<{((TextBox)sender).Name}> Old:{((TextBox)sender).Tag} New:{((TextBox)sender).Text}"
                });
            }
            catch { }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            //Zax 7/19/21
            ((TextBox)sender).Tag = ((TextBox)sender).Text;
            Point p5 = ((TextBox)sender).FindForm().PointToClient(((TextBox)sender).Parent.PointToScreen(((TextBox)sender).Location));
            int posX = p5.X + 45;
            int posY = p5.Y + ((TextBox)sender).Size.Height + 45;
            _vKeyboard.ShowKeyboard(posX, posY);
        }
        #endregion

        #region NumericUpDown
        private void NumericUpDown_GotFocus(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Tag = ((NumericUpDown)sender).Value;
        }

        private void NumericUpDown_LostFocus(object sender, EventArgs e)
        {
            if (((NumericUpDown)sender).Tag.ToString() == ((NumericUpDown)sender).Value.ToString()) return;
            try
            {
                LogEvents.NotifyLogRaise(sender, new LogArgs
                {
                    Module = "UserEvent",
                    Message = $"{SysPara.UserName} has Change NumericUpDown<{((NumericUpDown)sender).Name}> Old:{((NumericUpDown)sender).Tag } New:{((NumericUpDown)sender).Value}",
                    Type = LogEvents.LogType.UserLog
                });

                OperationLogRaise?.Invoke(sender, new OperationLogArgs
                {
                    Module = "UserEvent",
                    Username = SysPara.UserName,
                    Event = $"Change NumericUpDown<{((NumericUpDown)sender).Name}> Old:{((NumericUpDown)sender).Tag} New:{((NumericUpDown)sender).Value}"
                });
            }
            catch { }
        }

        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            //Zax 7/19/21
            ((NumericUpDown)sender).Tag = ((NumericUpDown)sender).Value;
            Point p5 = ((NumericUpDown)sender).FindForm().PointToClient(((NumericUpDown)sender).Parent.PointToScreen(((NumericUpDown)sender).Location));
            int posX = p5.X + 45;
            int posY = p5.Y + ((NumericUpDown)sender).Size.Height + 45;
            _vKeyboard.ShowKeyboard(posX, posY);
        } 
        #endregion

        #endregion
    }

    public class FlowChartModel
    {
        public DateTime StartTime;
        public DateTime EndTime;
        private DateTime _TotalTime;
        public TimeSpan TotalTime { get { return EndTime - StartTime; } }
        public string TopParentName;
    }

    public class OperationLogArgs : EventArgs
    {
        public string Module { get; set; }
        public string Username { get; set; }
        public string Event { get; set; }

    }

    public class FlowLogArgs : EventArgs
    {
        public string Module { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            if (Name == "")
            {
                Name = "NULL";
            }
            return $"{Module},{StartTime},{EndTime},{Duration},{Name}";
        }
    }

    public static class FuncExtern
    {
        private static Dictionary<string, string> TextBoxDirtyValue = new Dictionary<string, string>();
        public static void TxtGetFocus(this TextBox txt)
        {
            string key = txt.Name;
            if (txt.Parent != null)
            {
                key = txt.Name + txt.Parent.Name;
            }
            if (!TextBoxDirtyValue.ContainsKey(key))
                TextBoxDirtyValue.Add(key, txt.Text);
            else
            { TextBoxDirtyValue[key] = txt.Text; }
        }
        public static string OldValue(this TextBox txt)
        {
            string key = txt.Name;
            if (txt.Parent != null)
            {
                key = txt.Name + txt.Parent.Name;
            }
            if (TextBoxDirtyValue.ContainsKey(key))
                return TextBoxDirtyValue[key];
            else
            { return null; }
        }

    }

}
