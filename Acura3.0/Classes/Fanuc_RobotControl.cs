using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPFanucRobotDLL;
using JabilSDK;
using System.Threading;
using JabilSDK.Controls;
using System.Windows.Forms;

namespace Acura3._0.Classes
{
    public enum eRobotState
    {
        NoTask,
        Done,
        Stop,
        Alarm,
        Disconnected
    }

    public enum eRobotSafeStatus
    {
        NoSafeToMove,
        SafeToMove,
        Error
    }


    public  class Fanuc_RobotControl
    {
        //机器人实例化   // New Robot 
        public FANUC Robot = new FANUC();
        //机器人IP      // IP Address
        private string robotIP="192.168.10.1";
        //机器人数值寄存器值   // Robot Num Register 
        public double[] d_ReadRobotR =new double[101];
        //机器人位置寄存器值    // Robot Position Register
        private double[,] d_ReadRobotPR=new double[101,6];

        private object O_RobotControl = new object();

        public string RobotIP
        {
            get { return robotIP; }
            set { robotIP = value; }
        }

        public double[,] D_ReadRobotPR
        {
            get { return d_ReadRobotPR; }
            set { d_ReadRobotPR = value; }
        }


        public double[] D_ReadRobotR
        {
            get { return d_ReadRobotR; }
            set { d_ReadRobotR = value; }
        }

        public bool DelayMs(int delayMilliseconds)
        {
            DateTime now = DateTime.Now;
            Double s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.TotalMilliseconds + spand.Seconds * 1000;
                Application.DoEvents();
            }
            while (s < delayMilliseconds);
            return true;
        }

        #region //机器人连接读取写入参数     Robot Function
        /// <summary>
        /// 连接机器人  Connect Robot
        /// </summary>
        /// <returns></returns>
        public bool ConnectToRobot()
        {
            try
            {
                Robot.Disconnect();
                bool bConncet = Robot.Connect(robotIP);
                if (bConncet == false)
                {
                    return false;
                    
                }
                return bConncet;
            }
            catch (Exception e)
            {
                string a = e.ToString();
                return false;
            }
        }

        /// <summary>
        /// 刷新R值 Reflash Robot Register
        /// </summary>
        /// <returns></returns>
        public bool ReflashRobotR()
        {
            bool B_Return = false;
            if (ReflashR())
            {
                B_Return = true;
            }
            else
            {
                if (ConnectToRobot())
                {
                    if (ReflashR())
                    {
                        B_Return = true;
                    }
                }
            }
            if (B_Return == false)
            {
                B_Return = false;
               
            }
            return B_Return;
        }

        /// <summary>
        /// 刷新R值 Reflash Robot Register
        /// </summary>
        /// <returns></returns>
        private bool ReflashR()
        {
            lock (O_RobotControl)
            {
                try
                {
                    return Robot.RefreshR(ref d_ReadRobotR);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 写R值到机器人寄存器   Write to R
        /// </summary>
        /// <param name="RAddress">R地址</param>
        /// <param name="PRValue">写进去的R值</param>
        /// <returns></returns>
        public bool WriteToRobotR(int RAddress, double RValue)
        {
            bool B_Return = false;
            if (WriteR(RAddress, RValue))
            {
                if (!ReflashR())
                {
                    B_Return = false;
                }
                if (D_ReadRobotR[RAddress] == RValue)
                {
                    B_Return = true;
                }
            }
            else
            {
                if (ConnectToRobot())
                {
                    B_Return = WriteR(RAddress, RValue);
                }
                else
                {
                    B_Return = false;
                }
            }

            return B_Return;
        }

        /// <summary>
        /// 写R值到机器人寄存器
        /// </summary>
        /// <param name="RAddress">R地址</param>
        /// <param name="PRValue">写进去的R值</param>
        /// <returns></returns>
        public bool WriteR(int RAddress, double RValue)
        {
            try
            {
              
                return Robot.WriteR(RAddress, RValue);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 写PR值到机器人寄存器  Write to PR
        /// </summary>
        /// <param name="RAddress">PR地址</param>
        /// <param name="PRValue">写进去的PR值</param>
        /// <returns></returns>
        public bool WriteToRobotPR(int RAddress, double[] PRValue)
        {
            bool bReturn = false;
            if (WritePR(RAddress, PRValue))
            {
                bReturn = true;
            }
            else
            {
                if (ConnectToRobot())
                {
                    bReturn = WritePR(RAddress, PRValue);
                }
            }

            return bReturn;
        }

        /// <summary>
        /// 写PR值到机器人寄存器 Write to PR
        /// </summary>
        /// <param name="RAddress">PR地址</param>
        /// <param name="PRValue">写进去的PR值</param>
        /// <returns></returns>
        public bool WritePR(int RAddress, double[] PRValue)
        {
            try
            {
                //if (ConnectToRobot())
                //{
                //    Robot.WritePR(RAddress, PRValue);
                //}
                return Robot.WritePR(RAddress, PRValue); ;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 读取PR值
        /// </summary>
        /// <param name="RAddress">PR值地址</param>
        /// <returns></returns>
        public  bool ReadFromRobotPR(int RAddress)
        {
            bool bReturn = false;
            if (ReadPR(RAddress))
            {
                bReturn = true;
            }
            else
            {
                if (ConnectToRobot())
                {
                    bReturn = ReadPR(RAddress);
                }
            }

            return bReturn;
        }

        /// <summary>
        /// 读取PR值
        /// </summary>
        /// <param name="RAddress">PR地址</param>
        /// <returns></returns>
        public  bool ReadPR(int RAddress)
        {
            try
            {
                return Robot.ReadPR(RAddress, ref d_ReadRobotPR);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region  Acura write to Register/Robot read from Register
        /// <summary>
        /// SetRobotTask:Command to execute specific task 
        /// </summary>
        /// <param name="Task">RobotTask Num</param>
        /// <returns></returns>
        public bool SetRobotTask(int Task)
        {
            if (WriteToRobotR(31, 2))
            {
                if (WriteToRobotR(1, Task))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// SetTaskIndex:Command to execute specific task index(eg. screw index, product index)
        /// </summary>
        /// <param name="Index">task index</param>
        /// <returns></returns>
        public bool SetTaskIndex(int Index)
        {
                if (WriteToRobotR(2, Index))
                {
                    return true;
                } 

            return false;
        }

        /// <summary>
        /// SafeMove:Acknowledge robot safe to move
        /// </summary>
        /// <param name="SafeMove"></param>
        /// <returns></returns>
        public bool SetSafeMove(int SafeMove)
        {
            if (WriteToRobotR(3, SafeMove))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetRecipe:Set recipe based on product
        /// </summary>
        /// <param name="Recipe">Recipe</param>
        /// <returns></returns>
        public bool SetRecipe(int Recipe)
        {
            if (WriteToRobotR(4, Recipe))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetSpeed:Set robot speed (%)
        /// </summary>
        /// <param name="Speed">Speed</param>
        /// <returns></returns>
        public bool SetSpeed(int Speed)
        {
            if (WriteToRobotR(5, Speed))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetRobotMode:Set robot run mode
        /// </summary>
        /// <param name="RobotMode"></param>
        /// <returns></returns>
        public bool SetRobotMode(int RobotMode)
        {
            if (WriteToRobotR(6, RobotMode))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetDryRun:Set robot run in dry run
        /// </summary>
        /// <param name="DryRun "></param>
        /// <returns></returns>
        public bool SetDryRun(int DryRun)
        {
            if (WriteToRobotR(7, DryRun))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetVisionResult:Acknowledge vision result
        /// </summary>
        /// <param name="DryRun"></param>
        /// <returns></returns>
        public bool SetVisionResult(int VisionResult)
        {
            if (WriteToRobotR(8, VisionResult))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetVisionResultX:
        /// </summary>
        /// <param name="VisionResultX"></param>
        /// <returns></returns>
        public bool SetVisionResultX(double VisionResultX)
        {
            if (WriteToRobotR(9, VisionResultX))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetVisionResultY:
        /// </summary>
        /// <param name="VisionResultY"></param>
        /// <returns></returns>
        public bool SetVisionResultY(double VisionResultY)
        {
            if (WriteToRobotR(10, VisionResultY))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// SetVisionResultA:
        /// </summary>
        /// <param name="VisionResultA"></param>
        /// <returns></returns>
        public bool SetVisionResultA(double VisionResultA)
        {
            if (WriteToRobotR(11, VisionResultA))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Acura write from Register/ Robot write to Register
        /// <summary>
        /// Current execute task state
        /// </summary>
        /// <returns></returns>
        public eRobotState GetCurrentTaskState()
        {
            eRobotState result;
            if (ReflashRobotR())
            {
                result = (eRobotState)d_ReadRobotR[31];
                
            }
            else
                result = eRobotState.Disconnected;
            return result;
        }

        /// <summary>
        /// Current execute task ID
        /// </summary>
        /// <returns></returns>
        public double GetCurrentExecuteTaskID()
        {
            double result;
            if (ReflashRobotR())
            {
                result = d_ReadRobotR[32];
            }
            else
                result = -1;
            return result;
        }


        /// <summary>
        /// Acknowledge machine safe to move
        /// </summary>
        /// <returns></returns>
        public eRobotSafeStatus GetSafeMove()
        {
            eRobotSafeStatus result;
            //string result;
            if (ReflashRobotR())
            {
                result = (eRobotSafeStatus)d_ReadRobotR[33];
            }
            else
                result = eRobotSafeStatus.Error;
            return result;
        }

        public double GetAlarmID()
        {
            double result;
            if (ReflashRobotR())
            {
                result = d_ReadRobotR[34];
            }
            else
                result = -1;
            return result;
        }
        #endregion

        #endregion


        #region //机器人运行参数   Robot Event

        /// <summary>
        /// 机器人启动方法  Robot Start Method
        /// </summary>
        /// <param name="OB_Robot_Maintain">Robot Hold </param>   
        /// <param name="OB_Robot_Procedure1">Robot Program </param>
        /// <param name="OB_Robot_Stop">Robot Stop</param>
        /// <param name="OB_Robot_Start">Robot Start</param>
        /// <param name="OB_Robot_Enabled">Robot Enable</param>
        /// <param name="OB_Robot_ReSet">Robot Reset</param>
        /// <returns></returns>
        public bool RobotStart(Output OB_Robot_Maintain, Output OB_Robot_Procedure1, Output OB_Robot_Stop, Output OB_Robot_Start,
            Output OB_Robot_Enabled, Output OB_Robot_ReSet)
        {
            OB_Robot_Maintain.Off();
            OB_Robot_Procedure1.Off();
            DelayMs(1000);
            OB_Robot_Stop.On();
            DelayMs(1000);
            OB_Robot_Stop.Off();
            OB_Robot_Start.Off();
            OB_Robot_Maintain.On();
            OB_Robot_Enabled.On();
            OB_Robot_ReSet.On();
            DelayMs(1000);
            OB_Robot_ReSet.Off();
            DelayMs(1000);
            OB_Robot_Start.On();
            DelayMs(1000);
            OB_Robot_Start.Off();
            OB_Robot_Procedure1.On();
            return true;
        }

        /// <summary>
        /// 机器人暂停 Robot Hold
        /// </summary>
        /// <param name="OB_Robot_Maintain">Robot Hold</param>
        /// <returns></returns>
        public bool RobotPause(Output OB_Robot_Maintain)
        {
            //机器人关闭保持  Robot Pause
            OB_Robot_Maintain.Off();
            return true;
        }

        /// <summary>
        /// 机器人暂停后继续启动   Robot continue
        /// </summary>
        /// <param name="OB_Robot_Maintain">机器人Hold信号 Ronot Hold</param>
        /// <param name="OB_Robot_Start">Robot Start</param>
        /// <returns></returns>
        public bool RobotContinue(Output OB_Robot_Maintain, Output OB_Robot_Start)
        {
            OB_Robot_Maintain.On();
            OB_Robot_Start.On();
            DelayMs(1000);
            OB_Robot_Start.Off();
            return true;
        }

        /// <summary>
        /// 机器人复位报警 Robot ResetAlarm
        /// </summary>
        /// <param name="OB_Robot_Reset">机器人复位信号 Robot Reset</param>
        /// <returns></returns>
        public bool RobotAlarmReset(Output OB_Robot_Reset)
        {
            OB_Robot_Reset.On();
            DelayMs(500);
            OB_Robot_Reset.Off();
            return true;
        }

        #endregion
    }
}
