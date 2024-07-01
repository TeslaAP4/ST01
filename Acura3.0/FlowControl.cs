using AcuraLibrary;
using AcuraLibrary.Forms;
using JabilSDK;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Acura3._0
{
    public class FlowControl
    {
        public bool bStopWork = false;
        Thread FlowControlThread = null;
        private static Thread FlowChartThread = null;
        private static Thread RefreshIOThread = null;
        private static Thread[] ModulesThread;

        public void StartThread()
        {
            bStopWork = false;
            FlowControlThread = new Thread(DoWork);
            FlowControlThread.Name = "FlowControlThread";
            FlowControlThread.Priority = ThreadPriority.Highest;
            FlowControlThread.Start();

            RefreshIOThread = new Thread(RefreshIO);
            RefreshIOThread.Name = "RefreshIOThread";
            RefreshIOThread.Priority = ThreadPriority.AboveNormal;
            RefreshIOThread.Start();

            // Execute Modules Run & Initialize Thread
            int j = 0;
            ModulesThread = new Thread[ModuleManager.ModuleList.Count];
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
            {
                ModulesThread[j] = new Thread(() => DoWorkFlow(Module));
                ModulesThread[j].Name = string.Format("Module{0}", j);
                ModulesThread[j].Priority = ThreadPriority.AboveNormal;
                ModulesThread[j].Start();
                j++;
            }
        }

        public void StopThread()
        {
            if (FlowControlThread != null)
            {
                bStopWork = true;
                FlowControlThread.Join();
            }

            if (FlowChartThread != null)
            {
                bStopWork = true;
                FlowChartThread.Join();
            }
        }

        public void DoWork()
        {
            int ScanTick = 0;
            Int64 LastSecond = 0;
            Int64 TempSecond = 0;
        GetTickCountEx tick = new GetTickCountEx();
            while (!bStopWork)
            {
                Execute();

                Thread.Sleep(2);
                ScanTick++;
                TempSecond = tick.Value;
                if ((TempSecond - LastSecond) >= 1000)
                {
                    LastSecond = TempSecond;
                    if (SysPara.SystemMode == RunMode.RUN)
                    {
                        SysPara.OperationSecond++;
                        if (SysPara.SystemRun)
                            SysPara.RunSecond++;
                        else
                            SysPara.StopSecond++;
                    }
                    SysPara.ScanTime = ScanTick;
                    ScanTick = 0;
                }
            }
        }

        public void DoWorkFlow(ModuleBaseForm Module)
        {
            while (!bStopWork)
            {
                if (SysPara.SystemRun)
                {
                    if (SysPara.SystemMode == RunMode.RUN)
                    {
                        try { ExecuteRun(Module); }
                        catch (Exception) { JSDK.Alarm.Show("2021"); }
                    }
                    else if (SysPara.SystemMode == RunMode.INITIAL)
                    {
                        try { ExecuteInitial(Module); }
                        catch (Exception) { JSDK.Alarm.Show("2022"); }
                    }
                }
                Thread.Sleep(1);
            }
        }

        private void Execute()
        {
          
            //try { JSDK.RefreshControls(); }
            //catch (Exception) { JSDK.Alarm.Show("2018"); }

            try { MiddleLayer.AlwaysRun(); }
            catch (Exception) { JSDK.Alarm.Show("2019"); }

            try { MiddleLayer.CheckMotorProtected(); }
            catch (Exception) { JSDK.Alarm.Show("2020"); }
        }


        private void RefreshIO()
        {
            while (!bStopWork)
            {
                try { JSDK.RefreshControls(); }
                catch (Exception) { JSDK.Alarm.Show("2018"); }
                Thread.Sleep(1);
            }
           
        }
        #region Initial
        private int iInitialTask = 0;
        public void InitialReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                Module.iInitialTask = 0;
            SysPara.SystemMode = RunMode.INITIAL;
            SysPara.SystemInitialOk = false;
        }

        private void ExecuteInitial(ModuleBaseForm Module)
        {
            switch (Module.iInitialTask)
            {
                case 0:
                    try
                    {
                        Module.InitialParameterReset();
                    }
                    catch (Exception ex)
                    {
                        JSDK.Alarm.Show("2013", "An unpredictable error occurred on ExecuteReset() ! ModuleName=\"" + Module.Name + "\"");
                        //MainF.AddAlarmToolTip("2013", ex.ToString());
                        MiddleLayer.FlowCtrl.InitialReset();
                        MiddleLayer.StopRun();
                    }
                    Module.iInitialTask++;
                    JSDK.Alarm.Show("2000");
                    break;
                case 1:
                    try
                    {
                        Module.InitialReset();
                    }
                    catch (Exception ex)
                    {
                        JSDK.Alarm.Show("2014", "An unpredictable error occurred on InitialReset() ! ModuleName=\"" + Module.Name + "\"");
                        //MainF.AddAlarmToolTip("2014", ex.ToString());
                        MiddleLayer.FlowCtrl.InitialReset();
                        MiddleLayer.StopRun();
                    }
                    Module.iInitialTask++;
                    break;
                case 2:
                    try
                    {
                        Module.ServoOn();
                    }
                    catch (Exception ex)
                    {
                        JSDK.Alarm.Show("2011", "An unpredictable error occurred on ServoOn() ! ModuleName=\"" + Module.Name + "\"");
                        //MainF.AddAlarmToolTip("2011", ex.ToString());
                        MiddleLayer.StopRun();
                    }
                    Module.iInitialTask++;
                    break;
                case 3:
                    Module.iInitialTask++;
                    break;
                case 4:
                    try
                    {
                        Module.Initial();
                    }
                    catch (Exception ex)
                    {
                        JSDK.Alarm.Show("2015", "An unpredictable error occurred on Initial() ! ModuleName=\"" + Module.Name + "\"");
                        //MainF.AddAlarmToolTip("2015", ex.ToString());
                        MiddleLayer.StopRun();
                    }
                    if (MiddleLayer.GetInitialOk())
                        Module.iInitialTask++;
                    break;
                case 5:
                    MiddleLayer.StopRun();
                    SysPara.SystemMode = RunMode.IDLE;
                    SysPara.SystemInitialOk = true;
                    Module.iInitialTask++;
                    JSDK.Alarm.Show("2003");
                    break;
                case 6:
                    break;
            }
        }

        #endregion

        #region Run
        private int iRunTask = 0;
        public void RunReset()
        {
            foreach (ModuleBaseForm Module in ModuleManager.ModuleList)
                Module.iRunTask = 0;
            SysPara.SystemMode = RunMode.RUN;
        }

        private void ExecuteRun(ModuleBaseForm Module)
        {
            if (SysPara.SystemInitialOk)
            {
                if (Module.GetModuleRun())
                {
                    switch (Module.iRunTask)
                    {
                        case 0:
                            Module.iRunTask++;
                            break;
                        case 1:
                            try
                            {
                                Module.RunReset();
                            }
                            catch (Exception ex)
                            {
                                JSDK.Alarm.Show("2016", "An unpredictable error occurred on RunReset() ! ModuleName=\"" + Module.Name + "\"");
                                //MainF.AddAlarmToolTip("2016", ex.ToString());
                                MiddleLayer.StopRun();
                            }
                            Module.iRunTask++;
                            break;
                        case 2:
                            try
                            {
                                Module.Run();
                            }
                            catch (Exception ex)
                            {
                                string text = ex.ToString();
                                JSDK.Alarm.Show("2017", "An unpredictable error occurred on Run() ! ModuleName=\"" + Module.Name + "\"");
                                //MainF.AddAlarmToolTip("2017", ex.ToString());
                                MiddleLayer.StopRun();
                            }
                            break;
                    }
                }
            }
        }

        
        #endregion
    }
}
