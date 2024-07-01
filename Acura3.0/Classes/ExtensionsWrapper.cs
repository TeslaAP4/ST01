using JabilSDK;
using JabilSDK.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using static JabilSDK.MotorBaseInterface;

namespace Acura3._0.Classes
{


    public static class ExtensionsWrapper
    {
        // Storage for the properties.
        private static Dictionary<object, Dictionary<string, object>> PropertyValues = new Dictionary<object, Dictionary<string, object>>();


        /// <summary>Set value for the SafeToMove.
        /// </summary>
        /// <param name="value">A boolean value to indicate the motor is safe to move.</param>
        public static void SafeToMove(this Motor item, bool value)
        {
            // If we don't have a dictionary for this item yet,
            // make one.
            if (!PropertyValues.ContainsKey(item))
                PropertyValues[item] = new Dictionary<string, object>();

            // Set the value in the item's dictionary.
            PropertyValues[item][item.Name + "SafeToMove"] = value;
        }

        /// <summary>Return value for the SafeToMove.
        /// </summary>
        /// <returns>
        /// This method returns an boolean value to indicate the motor is safe to move.
        /// </returns>
        public static bool SafeToMove(this Motor item)
        {
            // If we don't have a dictionary for
            // this item yet, return the default value.
            if (!PropertyValues.ContainsKey(item)) return false;

            // If the value isn't in the dictionary,
            // return the default value.
            if (!PropertyValues[item].ContainsKey(item.Name + "SafeToMove"))
                return false;

            // Return the saved value.
            return (bool)PropertyValues[item][item.Name + "SafeToMove"];
        }

        /// <summary>Set value for the Home State.
        /// </summary>
        /// <param name="value">A int value to indicate the motor homing seq.</param>
        public static void HomeState(this Motor item, int value)
        {
            // If we don't have a dictionary for this item yet,
            // make one.
            if (!PropertyValues.ContainsKey(item))
                PropertyValues[item] = new Dictionary<string, object>();

            // Set the value in the item's dictionary.
            PropertyValues[item][item.Name + "HomeState"] = value;
        }

        /// <summary>Set value for the Home State.
        /// </summary>
        /// <returns>
        /// This method returns an int value to indicate the motor homing sequence.
        /// </returns>
        public static int HomeState(this Motor item)
        {
            // If we don't have a dictionary for
            // this item yet, return the default value.
            if (!PropertyValues.ContainsKey(item)) return 0;

            // If the value isn't in the dictionary,
            // return the default value.
            if (!PropertyValues[item].ContainsKey(item.Name + "HomeState"))
                return 0;

            // Return the saved value.
            return (int)PropertyValues[item][item.Name + "HomeState"];
        }

        /// <summary>Set value for the Ignore Alarm.
        /// </summary>
        /// <param name="value">A boolean value to indicate the motor is Ignore Alarm.</param>
        public static void IgnoreAlarm(this Motor item, bool value)
        {
            // If we don't have a dictionary for this item yet,
            // make one.
            if (!PropertyValues.ContainsKey(item))
                PropertyValues[item] = new Dictionary<string, object>();

            // Set the value in the item's dictionary.
            PropertyValues[item][item.Name + "IgnoreAlarm"] = value;
        }

        /// <summary>Return value for the Ignore Alarm.
        /// </summary>
        /// <returns>
        /// This method returns an boolean value to indicate the motor is Ignore Alarm.
        /// </returns>
        public static bool IgnoreAlarm(this Motor item)
        {
            // If we don't have a dictionary for
            // this item yet, return the default value.
            if (!PropertyValues.ContainsKey(item)) return false;

            // If the value isn't in the dictionary,
            // return the default value.
            if (!PropertyValues[item].ContainsKey(item.Name + "IgnoreAlarm"))
                return false;

            // Return the saved value.
            return (bool)PropertyValues[item][item.Name + "IgnoreAlarm"];
        }

        /// <summary>Set value for the JHomme Done.
        /// </summary>
        /// <param name="value">A boolean value to indicate the motor is JHomme Done.</param>
        public static void IsJHomeDone(this Motor item, bool value)
        {
            // If we don't have a dictionary for this item yet,
            // make one.
            if (!PropertyValues.ContainsKey(item))
                PropertyValues[item] = new Dictionary<string, object>();

            // Set the value in the item's dictionary.
            PropertyValues[item][item.Name + "IsJHomeDone"] = value;
        }

        /// <summary>Return value for the JHomme Done.
        /// </summary>
        /// <returns>
        /// This method returns an boolean value to indicate the motor is JHomme Done.
        /// </returns>
        public static bool IsJHomeDone(this Motor item)
        {
            // If we don't have a dictionary for
            // this item yet, return the default value.
            if (!PropertyValues.ContainsKey(item)) return false;

            // If the value isn't in the dictionary,
            // return the default value.
            if (!PropertyValues[item].ContainsKey(item.Name + "IsJHomeDone"))
                return false;

            // Return the saved value.
            return (bool)PropertyValues[item][item.Name + "IsJHomeDone"];
        }


        /// <summary>Return value for the JHomingTimer.
        /// </summary>
        /// <returns>
        /// This method returns JTimer for motor homing.
        /// </returns>
        public static JTimer JHomingTimer(this Motor item)
        {
            if (!PropertyValues.ContainsKey(item))
                PropertyValues[item] = new Dictionary<string, object>();

            // If the value isn't in the dictionary,
            // return the default value.
            if (!PropertyValues[item].ContainsKey(item.Name + "HommingTimer"))
                PropertyValues[item][item.Name + "HommingTimer"] = new JTimer();

            // Return the saved value.
            return (JTimer)PropertyValues[item][item.Name + "HommingTimer"];
        }


        /// <summary>
        /// indicate the target was in range
        /// </summary>
        /// <param name="pos">Position from motor encoder</param>
        /// <param name="tolerance">Tolerance Given.Default is 0.1</param>
        /// <returns></returns>
        public static bool GetRange(this Motor element, double pos, double tolerance = 10)
        {
            double a = element.GetEncoderPosition();
            return element.GetEncoderPosition() >= pos - tolerance && element.GetEncoderPosition() <= pos + tolerance;
        }

        /// <summary>Get Gantry Position  
        /// </summary>
        /// <param name="TableName">A string value to indicate the table in Data Set</param>
        /// <param name="PointName">A string value to indicate the Point Name Row</param>
        /// <param name="GantryName">A string value to indicate the Gantry Name column</param>
        /// <returns>
        /// This method returns an double value to indicate the specific teach point.
        /// </returns>
        public static double GetPost(this DataSet ds, string TableName, string PointName, string GantryName)
        {
            if (ds == null)
            {
                return 0;
            }
            DataRow dr = ds.Tables[TableName].AsEnumerable().SingleOrDefault(r => r.Field<string>("PointName") == PointName);
            if (dr == null)
            {
                JSDK.Alarm.Show("2030", $"Point Not Found at {TableName} , {PointName}");
                return 0;
            }

            return Convert.ToDouble(dr[GantryName]);
        }

        /// <summary>
        /// Set control to double buffer to prevent flickering
        /// </summary>
        /// <param name="cont"></param>
        /// 
        public static void SetDoubleBuffer(this Control cont)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, cont, new object[] { true });
        }

        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public static bool RHomeReset(this Motor Mtr)
        {
            bool isDone = false;
            Mtr.HomeReset();
            Mtr.HomeState(0);
            Mtr.IgnoreAlarm(false);
            Mtr.IsJHomeDone(false);
            isDone = true;

            return isDone;
        }

        #region Backup RHome
        #endregion
        
        public static bool RHome(this Motor Mtr, bool ReverseJog, int JogDelay = 3000)
        {
            AxisIOStatus IOState = Mtr.GetAxisIOStatus();
            switch (Mtr.HomeState())
            {
                case 0:
                    Mtr.IgnoreAlarm(true);
                    Mtr.HomeState(1);
                    Mtr.WorkSpeed = Mtr.HomeHighSpeed;
                    Mtr.SpeedRatio = 100;
                    break;
                case 1:
                    if (ReverseJog)
                    {
                        if (!IOState.ORG && !IOState.HLN) // Edited: Loong
                        {
                            Mtr.WorkSpeed = Mtr.HomeHighSpeed;
                            Mtr.JogP();
                        }
                    }
                    else
                    {
                        if (!IOState.ORG && !IOState.HLN) // Edited: Loong
                        {
                            Mtr.WorkSpeed = Mtr.HomeHighSpeed;
                            Mtr.JogN();
                        } 
                    }
                    if (IOState.HLN || IOState.HLP)
                    {
                        Mtr.Stop();
                        Mtr.HomeState(5);
                    }
                    else if (IOState.ORG)
                    {
                        Mtr.Stop();
                        Mtr.HomeState(5);    // Edited: Loong
                    }
                    break;
                case 5:
                    if (IOState.ALM)
                    {
                        Mtr.AlarmReset();
                    }

                    if (!IOState.ALM && !Mtr.IsBusy())
                    {
                        Mtr.HomeState(10);
                    }
                    break;
                case 10:
                    if (IOState.ORG)
                    {
                        Mtr.Stop();
                        Mtr.HomeState(12);    // Edited: Loong
                    }
                    else
                    { 
                        if (ReverseJog)
                        {
                            Mtr.WorkSpeed = Mtr.HomeHighSpeed;
                            Mtr.JogN();
                        }
                        else
                        {
                            Mtr.WorkSpeed = Mtr.HomeHighSpeed;
                            Mtr.JogP();
                        }
                    }
                    break;
                case 12: //Edited: Loong
                    if (!Mtr.IsBusy())
                    {
                        Mtr.HomeState(15);
                    }
                    break;
                case 15:
                    bool r1 = Mtr.Home();
                    if (r1 && !Mtr.IsBusy())
                        Mtr.HomeState(20);
                    break;
                case 20:
                    Mtr.IsJHomeDone(true);
                    Mtr.IgnoreAlarm(false);
                    Mtr.HomeState(999);
                    break;
            }
            return Mtr.IsJHomeDone();
        }
    }
}
