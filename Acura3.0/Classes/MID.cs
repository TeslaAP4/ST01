using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Johnson@2019/9/18
/// </summary>
namespace AlphaRap.Classes
{
    public class MID
    {
        //经过测试OK 请不要改变里面MID命令字符串
        public const string M0001 = "00200001            "; //Application Communication start 必须不带版本号 不然发Keep Alive会没反馈然后就断线了
        //public const string M0001 = "002000010060    00";
        public static string M0002 = "00560002";             //Application Communication start acknowledge
        public const string M0003 = "00200003            "; //Application Communication stop  
        //public const string M0003 = "002000030010        \0";   //可能是这个？？
        public static string M0004 = "00260004";             //Application Communication negative acknowledge 代表命令执行错误 具体报警代码自己查pdf
        public static string M0005 = "00240005";             //Application Communication positive acknowledge 代码命令执行正常
        //public static string M0060 = "00200060            "; //Last tightening result data subscribe 版本号越高信息越多 用01最ok了


        public static string M0008 = "006800080010        0900001390 020010020NUL"; //Application data message subscription
        //public static string M0009 = ""; //Application Data Message unsubscribe
        public const string M0060 = "00200060002         "; //Last tightening result data subscribe 必须版本号2才有Rundown Angle越高信息越多
        //public const string M0060 = "002000600010    00  ";
        public static string M0061 = "02310061   ";          //Last tightening result data
        public const string M0062 = "00200062            "; //Last tightening result data acknowledge 收到0061的MID后 必须发送这条0062 不然就断线了
        public const string M0063 = "00200063            "; //Last tightening result data unsubscribe
        public const string M0064 = "00300064002         "; //Old tightening result upload request / Requesting tightening ID zero is the same as requesting the latest tightening performed.
        public const string M0065 = "02260065002";
        //public static string M9997 = "00259997001";        //Communication acknowledge
        //public static string M9998 = "00279998001";        //Communication acknowledge error
        public const string M9999 = "00209999            "; //Keep Alive 返回"00209999        0100"




        public enum TighteningValueStatus
        {
            LOW = 0,
            OK = 1,
            HIGH = 2
        }

        public struct LastTighteningResult
        {
            //Rev 1
            //public int CELL_ID;
            //public int CHANNEL_ID;
            //public string TORQUE_CONTROLLER_NAME;
            //public string VIN_NUMBER;
            //public int JOB_ID;
            //public int PARAMETER_SET_ID;
            //public int BATCH_SIZE;
            //public int BATCH_COUNTER;
            public bool TIGHTENING_STATUS; //NOK(0) or OK(1)
            public TighteningValueStatus TORQUE_STATUS; //Low(0) OK(1) High(2)
            public TighteningValueStatus ANGLE_STATUS; //Low(0) OK(1) High(2)
            public double TORQUE_MIN_LIMIT;
            public double TORQUE_MAX_LIMIT;
            public double TORQUE_FINAL_TARGET;
            public double TORQUE; //实际扭力
            public int ANGLE_MIN_LIMIT;
            public int ANGLE_MAX_LIMIT;
            public int ANGLE_FINAL_TARGET;
            public int ANGLE; //实际角度
            public DateTime TIMESTAMP;
            //public DateTime LAST_CHANGE_IN_PARAMETER_SET;
            //public TighteningValueStatus BATCH_STATUS; //NOK(0) OK(1) NotUsed(2)
            public int TIGHTENING_ID;

            //Rev 2
            //public Strategy STRATEGY;
            //public StrategyOptions STRATEGY_OPTIONS;
            public TighteningValueStatus RUNDOWN_ANGLE_STATUS;
            //public TighteningValueStatus CURRENT_MONITORING_STATUS;
            //public TighteningValueStatus SELFTAP_STATUS;
            //public TighteningValueStatus PREVAIL_TORQUE_MONITORING_STATUS;
            //public TighteningValueStatus PREVAIL_TORQUE_COMPENSATE_STATUS;
            public TighteningErrorStatus TIGHTENING_ERROR_STATUS;
            public int RUNDOWN_ANGLE_MIN;
            public int RUNDOWN_ANGLE_MAX;
            public int RUNDOWN_ANGLE;
            //public double CURRENT_MONITORING_MIN;
            //public double CURRENT_MONITORING_MAX;
            //public double CURRENT_MONITORING_VALUE;
            //public double SELFTAP_MIN;
            //public double SELFTAP_MAX;
            //public double SELFTAP_TORQUE;
            //public double PREVAIL_TORQUE_MONITORING_MIN;
            //public double PREVAIL_TORQUE_MONITORING_MAX;
            //public double PREVAIL_TORQUE;
            //public int JOB_SEQUENCE_NUMBER;
            public int SYNC_TIGHTENING_ID;
            //public string TOOL_SERIAL_NUMBER;
        }

        public enum Strategy
        {
            TORQUE_CONTROL = 1,
            TORQUE_CONTROL_AND_ANGLE_MONITORING = 2,
            TORQUE_CONTROL_AND_ANGLE_CONTROL = 3,
            ANGLE_CONTROL_AND_TORQUE_MONITORING = 4,
            DS_CONTROL = 5,
            DS_CONTROL_TORQUE_MONITORING = 6,
            REVERSE_ANGLE = 7,
            REVERSE_TORQUE = 8,
            CLICK_WRENCH = 9,
            ROTATE_SPINDLE_FORWARD = 10,
            TORQUE_CONTROL_OR_ANGLE_CONTROL = 11,
            ROTATE_SPINDLE_REVERSE = 12,
            HOME_POSITION_FORWARD = 13,
            EP_MONITORING = 14,
            YIELD = 15,
            EP_FIXED = 16,
            EP_CONTROL = 17,
            EP_ANGLE_SHUTOFF = 18,
            YIELD_OR_TORQUE_CONTROL = 19,
            SNUG_GRADIENT = 20,
            RESIDUAL_TORQUE_AND_TIME = 21,
            RESIDUAL_ANGLE_AND_TIME = 22,
            BREAKAWAY_PEAK = 23,
            LOOSE_AND_TIGHTENING = 24,
            HOME_POSITION_REVERSE = 25,
            PVT_COMP_WITH_SNUG = 26,
            NO_STRATEGY = 99
        }

        public struct StrategyOptions
        {
            //Byte 0
            public bool Torque;
            public bool Angle;
            public bool Batch;
            public bool PvtMonitoring;
            public bool PvtCompensate;
            public bool Selftap;
            public bool Rundown;
            public bool CM;
            //Byte 1
            public bool DsControl;
            public bool ClickWrench;
            public bool RbwMonitoring;
        }

        public class TighteningErrorStatus
        {
            //Byte 0
            public bool RundownAngleMaxShutOff;
            public bool RundownAngleMinShutOff;
            public bool TorqueMaxShutOff;
            public bool AngleMaxShutOff;
            public bool SelftapTorqueMaxShutOff;
            public bool SelftapTorqueMinShutOff;
            public bool PrevailTorqueMaxShutOff;
            public bool PrevailTorqueMinShutOff;
            //Byte 1
            public bool PrevailTorqueCompensateOverflow;
            public bool CurrentMonitoringMaxShutOff;
            public bool PostViewTorqueMinTorqueShutOff;
            public bool PostViewTorqueMaxTorqueShutOff;
            public bool PostViewTorqueAngleTooSmall;
            public bool TriggerLost;
            public bool TorqueLessThanTarget;
            public bool ToolHot;
            //Byte 2
            public bool MultistageAbort;
            public bool Rehit;
            public bool DsMeasureFailed;
            public bool CurrentLimitReached;
            public bool EndTimeOutShutOff;
            public bool RemoveFastenerLimitExceeded;
            public bool DisableDrive;
            public bool TransducerLost;
            //Byte 3
            public bool TransducerShorted;
            public bool TransducerCorrupt;
            public bool SyncTimeout;
            public bool DynamicCurrentMonitoringMin;
            public bool DynamicCurrentMonitoringMax;
            public bool AngleMaxMonitor;
            public bool YieldNutOff;
            public bool YieldTooFewSamples;
        }

        public struct MID0004Error
        {
            public int Mid;
            public MID0004ErrorCode Error;
        }
        /// <summary>
        /// MID0004的Error代码
        /// </summary>
        public enum MID0004ErrorCode
        {
            NO_ERROR = 00,
            INVALID_DATA = 01,
            PARAMETER_SET_ID_NOT_PRESENT = 02,
            PARAMETER_SET_CANNOT_BE_SET = 03,
            PARAMETER_SET_NOT_RUNNING = 04,
            VIN_UPLOAD_SUBSCRIPTION_ALREADY_EXISTS = 06,
            VIN_UPLOAD_SUBSCRIPTION_DOESNT_EXISTS = 07,
            VIN_INPUT_SOURCE_NOT_GRANTED = 08,
            LAST_TIGHTENING_RESULT_SUBSCRIPTION_ALREADY_EXISTS = 09,
            LAST_TIGHTENING_RESULT_SUBSCRIPTION_DOESNT_EXISTS = 10,
            ALARM_SUBSCRIPTION_ALREADY_EXISTS = 11,
            ALARM_SUBSCRIPTION_DOESNT_EXISTS = 12,
            PARAMETER_SET_SELECTION_SUBSCRIPTION_ALREADY_EXISTS = 13,
            PARAMETER_SET_SELECTION_SUBSCRIPTION_DOESNT_EXISTS = 14,
            TIGHTENING_ID_REQUESTED_NOT_FOUND = 15,
            CONNECTION_REJECTED_PROTOCOL_BUSY = 16,
            JOB_ID_NOT_PRESENT = 17,
            JOB_INFO_SUBSCRIPTION_ALREADY_EXISTS = 18,
            JOB_INFO_SUBSCRIPTION_DOESNT_EXISTS = 19,
            JOB_CANNOT_BE_SET = 20,
            JOB_NOT_RUNNING = 21,
            NOT_POSSIBLE_TO_EXECUTE_DYNAMIC_JOB_REQUEST = 22,
            JOB_BATCH_DECREMENT_FAILED = 23,
            NOT_POSSIBLE_TO_CREATE_PSET = 24,
            PROGRAMMING_CONTROL_NOT_GRANTED = 25,
            CONTROLLER_IS_NOT_A_SYNC_MASTER_OR_STATION_CONTROLLER = 30,
            MULTI_SPINDLE_STATUS_SUBSCRIPTION_ALREADY_EXISTS = 31,
            MULTI_SPINDLE_STATUS_SUBSCRIPTION_DOESNT_EXISTS = 32,
            MULTI_SPINDLE_RESULT_SUBSCRIPTION_ALREADY_EXISTS = 33,
            MULTI_SPINDLE_RESULT_SUBSCRIPTION_DOESNT_EXISTS = 34,
            JOB_LINE_CONTROL_INFO_SUBSCRIPTION_ALREADY_EXISTS = 40,
            JOB_LINE_CONTROL_INFO_SUBSCRIPTION_DOESNT_EXISTS = 41,
            IDENTIFIER_INPUT_SOURCE_NOT_GRANTED = 42,
            MULTIPLE_IDENTIFIERS_WORK_ORDER_SUBSCRIPTION_ALREADY_EXISTS = 43,
            MULTIPLE_IDENTIFIERS_WORK_ORDER_SUBSCRIPTION_DOESNT_EXISTS = 44,
            STATUS_EXTERNAL_MONITORED_INPUTS_SUBSCRIPTION_ALREADY_EXISTS = 50,
            STATUS_EXTERNAL_MONITORED_INPUTS_SUBSCRIPTION_DOESNT_EXISTS = 51,
            IO_DEVICE_NOT_CONNECTED = 52,
            FAULTY_IO_DEVICE_ID = 53,
            TOOL_TAG_ID_UNKNOWN = 54,
            TOOL_TAG_ID_SUBSCRIPTION_ALREADY_EXISTS = 55,
            TOOL_TAG_ID_SUBSCRIPTION_DOESNT_EXISTS = 56,
            TOOL_MOTOR_TUNING_FAILED = 57,
            NO_ALARM_PRESENT = 58,
            TOOL_CURRENTLY_IN_USE = 59,
            NO_HISTOGRAM_AVAILABLE = 60,
            CALIBRATION_FAILED = 70,
            SUBSCRIPTION_ALREADY_EXISTS = 71,
            SUBSCRIPTION_DOESNT_EXISTS = 72,
            COMMAND_FAILED = 79,
            AUTOMATIC_MANUAL_MODE_SUBSCRIBE_ALREADY_EXISTS = 82,
            AUTOMATIC_MANUAL_MODE_SUBSCRIBE_DOESNT_EXISTS = 83,
            RELAY_FUNCTION_SUBSCRIPTION_ALREADY_EXISTS = 84,
            RELAY_FUNCTION_SUBSCRIPTION_DOESNT_EXISTS = 85,
            SELECTOR_SOCKET_INFO_SUBSCRIPTION_ALREADY_EXISTS = 86,
            SELECTOR_SOCKET_INFO_SUBSCRIPTION_DOESNT_EXISTS = 87,
            DIGIN_INFO_SUBSCRIPTION_ALREADY_EXISTS = 88,
            DIGIN_INFO_SUBSCRIPTION_DOESNT_EXISTS = 89,
            LOCK_AT_BATCH_DONE_SUBSCRIPTION_ALREADY_EXISTS = 90,
            LOCK_AT_BATCH_DONE_SUBSCRIPTION_DOESNT_EXISTS = 91,
            OPEN_PROTOCOL_COMMANDS_DISABLED = 92,
            OPEN_PROTOCOL_COMMANDS_DISABLED_SUBSCRIPTION_ALREADY_EXISTS = 93,
            OPEN_PROTOCOL_COMMANDS_DISABLED_SUBSCRIPTION_DOESNT_EXISTS = 94,
            REJECT_REQUEST_POWER_MACS_IS_IN_MANUAL_MODE = 95,
            CLIENT_ALREADY_CONNECTED = 96,
            MID_REVISION_UNSUPPORTED = 97,
            CONTROLLER_INTERNAL_REQUEST_TIMEOUT = 98,
            UNKNOWN_MID = 99
        }

        public static class BitHelper
        {
            public static bool GetBit(byte b, int bitNumber) => (b & (1 << bitNumber - 1)) != 0;

            //public static byte SetByte(bool[] values)
            //{
            //    byte result = 0;
            //    int index = 8 - values.Length;
            //    foreach (bool b in values)
            //    {
            //        if (b)
            //            result |= (byte)(1 << (7 - index));
            //        index++;
            //    }
            //    return result;
            //}
        }

        public static TighteningErrorStatus TighteningErrorStatusConvertFromBytes(string value)
        {
            Func<int, bool> func = new Func<int, bool>((i) => { return Convert.ToInt16(value.Substring(i - 1, 1)) == 1 ? true : false; });

            return new TighteningErrorStatus
            {
                RundownAngleMaxShutOff = func(32),
                RundownAngleMinShutOff = func(31),
                TorqueMaxShutOff = func(30),
                AngleMaxShutOff = func(29),
                SelftapTorqueMaxShutOff = func(28),
                SelftapTorqueMinShutOff = func(27),
                PrevailTorqueMaxShutOff = func(26),
                PrevailTorqueMinShutOff = func(25),

                PrevailTorqueCompensateOverflow = func(24),
                CurrentMonitoringMaxShutOff = func(23),
                PostViewTorqueMinTorqueShutOff = func(22),
                PostViewTorqueMaxTorqueShutOff = func(21),
                PostViewTorqueAngleTooSmall = func(20),
                TriggerLost = func(19),
                TorqueLessThanTarget = func(18),
                ToolHot = func(17),

                MultistageAbort = func(16),
                Rehit = func(15),
                DsMeasureFailed = func(14),
                CurrentLimitReached = func(13),
                EndTimeOutShutOff = func(12),
                RemoveFastenerLimitExceeded = func(11),
                DisableDrive = func(10),
                TransducerLost = func(9),

                TransducerShorted = func(8),
                TransducerCorrupt = func(7),
                SyncTimeout = func(6),
                DynamicCurrentMonitoringMin = func(5),
                DynamicCurrentMonitoringMax = func(4),
                AngleMaxMonitor = func(3),
                YieldNutOff = func(2),
                YieldTooFewSamples = func(1),
            };
        }

        public static StrategyOptions StrategyOptionsConvertFromBytes(byte[] value)
        {

            return new StrategyOptions()
            {
                //Byte 0
                Torque = BitHelper.GetBit(value[0], 1),
                Angle = BitHelper.GetBit(value[0], 2),
                Batch = BitHelper.GetBit(value[0], 3),
                PvtMonitoring = BitHelper.GetBit(value[0], 4),
                PvtCompensate = BitHelper.GetBit(value[0], 5),
                Selftap = BitHelper.GetBit(value[0], 6),
                Rundown = BitHelper.GetBit(value[0], 7),
                CM = BitHelper.GetBit(value[0], 8),
                //Byte 1
                DsControl = BitHelper.GetBit(value[1], 1),
                ClickWrench = BitHelper.GetBit(value[1], 2),
                RbwMonitoring = BitHelper.GetBit(value[1], 3)
            };
        }

        public static int HeaderParser(byte[] Data)
        {
            return int.Parse(Encoding.ASCII.GetString(Data, 4, 4));
        }

        public static MID0004Error MID0004Parser(byte[] Data)
        {
            return new MID0004Error()
            {
                Mid = int.Parse(Encoding.ASCII.GetString(Data, 20, 4)),
                Error = (MID0004ErrorCode)int.Parse(Encoding.ASCII.GetString(Data, 24, 2))
            };
        }

        private static string GetParameter(byte[] Bytes, int Index, int Count)
        {
            //Index + 2 (segment id is 2 byte length)
            return Encoding.ASCII.GetString(Bytes, Index + 2, Count);
        }

        private static byte[] GetSubBytes(byte[] Bytes, int Index, int Count)
        {
            byte[] bytes = new byte[Count];
            for (int i = 0; i < Count; i++)
            {
                bytes[i] = Bytes[Index + 2 + i];
            }
            return bytes;
        }

        private static string GetBinStringFromBytes(byte[] Bytes, int Index, int Count)
        {
            return Convert.ToString(Convert.ToInt64(GetParameter(Bytes, Index, Count)), 2).PadLeft(32, '0');
        }



        public static LastTighteningResult MID0065Parser(byte[] Data)
        {
            //fill deafult value
            LastTighteningResult result = new LastTighteningResult();

            result.TIGHTENING_ID = Convert.ToInt32(GetParameter(Data, 20, 10));
            //result.VIN_NUMBER = GetParameter(Data, 32, 25);
            //result.JOB_ID = Convert.ToInt32(GetParameter(Data, 59, 4));
            //result.PARAMETER_SET_ID = Convert.ToInt32(GetParameter(Data, 65, 3));
            //result.STRATEGY = (Strategy)Convert.ToInt32(GetParameter(Data, 70, 2));
            //result.STRATEGY_OPTIONS = StrategyOptionsConvertFromBytes(GetSubBytes(Data, 74, 5));
            //result.BATCH_SIZE = Convert.ToInt32(GetParameter(Data, 81, 4));
            //result.BATCH_COUNTER = Convert.ToInt32(GetParameter(Data, 87, 4));
            result.TIGHTENING_STATUS = GetParameter(Data, 93, 1) == "1" ? true : false;
            //result.BATCH_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 96, 1));
            result.TORQUE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 99, 1));
            result.ANGLE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 102, 1));
            result.RUNDOWN_ANGLE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 105, 1));
            //result.CURRENT_MONITORING_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 108, 1));
            //result.SELFTAP_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 111, 1));
            //result.PREVAIL_TORQUE_MONITORING_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 114, 1));
            //result.PREVAIL_TORQUE_COMPENSATE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 117, 1));
            result.TIGHTENING_ERROR_STATUS = TighteningErrorStatusConvertFromBytes(GetBinStringFromBytes(Data, 120, 10));
            result.TORQUE = Convert.ToDouble(GetParameter(Data, 132, 6));
            result.ANGLE = Convert.ToInt32(GetParameter(Data, 140, 5));
            result.RUNDOWN_ANGLE = Convert.ToInt32(GetParameter(Data, 147, 5));
            //result.CURRENT_MONITORING_VALUE = Convert.ToDouble(GetParameter(Data, 154, 3));
            //result.SELFTAP_TORQUE = Convert.ToDouble(GetParameter(Data, 159, 6));
            //result.PREVAIL_TORQUE = Convert.ToDouble(GetParameter(Data, 167, 6));
            //result.JOB_SEQUENCE_NUMBER = Convert.ToInt32(GetParameter(Data, 175, 5));
            result.SYNC_TIGHTENING_ID = Convert.ToInt32(GetParameter(Data, 182, 5));
            //result.TOOL_SERIAL_NUMBER = GetParameter(Data, 189, 14);
            result.TIMESTAMP = DateTime.Parse($"{GetParameter(Data, 205, 10)} {GetParameter(Data, 216, 8)}");

            return result;
        }


        public static LastTighteningResult MID0061Parser(byte[] Data)
        {
            LastTighteningResult result;

            //Revision1
            //result.CELL_ID = Convert.ToInt32(GetParameter(Data, 20, 4));
            //result.CHANNEL_ID = Convert.ToInt32(GetParameter(Data, 26, 2));
            //result.TORQUE_CONTROLLER_NAME = GetParameter(Data, 30, 25);
            //result.VIN_NUMBER = GetParameter(Data, 57, 25);
            //result.JOB_ID = Convert.ToInt32(GetParameter(Data, 84, 2));
            //result.PARAMETER_SET_ID = Convert.ToInt32(GetParameter(Data, 88, 3));
            //result.BATCH_SIZE = int.Parse(GetParameter(Data, 93, 4));
            //result.BATCH_COUNTER = int.Parse(GetParameter(Data, 99, 4));
            //result.TIGHTENING_STATUS = GetParameter(Data, 105, 1) == "1" ? true : false;
            //result.TORQUE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 108, 1));
            //result.ANGLE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 111, 1));
            //result.TORQUE_MIN_LIMIT = int.Parse(GetParameter(Data, 114, 6));
            //result.TORQUE_MAX_LIMIT = int.Parse(GetParameter(Data, 122, 6));
            //result.TORQUE_FINAL_TARGET = int.Parse(GetParameter(Data, 130, 6));
            //result.TORQUE = Double.Parse(GetParameter(Data, 138, 6)) / 100;
            //result.ANGLE_MIN_LIMIT = Convert.ToInt32(GetParameter(Data, 146, 5));
            //result.ANGLE_MAX_LIMIT = Convert.ToInt32(GetParameter(Data, 153, 5));
            //result.ANGLE_FINAL_TARGET = Convert.ToInt32(GetParameter(Data, 160, 5));
            //result.ANGLE = int.Parse(GetParameter(Data, 167, 5));
            //result.TIMESTAMP = DateTime.Parse($"{GetParameter(Data, 174, 10)} {GetParameter(Data, 185, 8)}");
            //result.LAST_CHANGE_IN_PARAMETER_SET = DateTime.Parse($"{GetParameter(Data, 195, 10)} {GetParameter(Data, 206, 8)}");
            //result.BATCH_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 216, 1));
            //result.TIGHTENING_ID = int.Parse(GetParameter(Data, 219, 10));

            //Revision2
            //result.CELL_ID = Convert.ToInt32(GetParameter(Data, 20, 4));
            //result.CHANNEL_ID = Convert.ToInt32(GetParameter(Data, 26, 2));
            //result.TORQUE_CONTROLLER_NAME = GetParameter(Data, 30, 25);
            //result.VIN_NUMBER = GetParameter(Data, 57, 25);
            //result.JOB_ID = Convert.ToInt32(GetParameter(Data, 84, 4));
            //result.PARAMETER_SET_ID = Convert.ToInt32(GetParameter(Data, 90, 3));
            //result.STRATEGY = (Strategy)Convert.ToInt32(GetParameter(Data, 95, 2));
            //result.STRATEGY_OPTIONS = StrategyOptionsConvertFromBytes(GetSubBytes(Data, 99, 5));
            //result.BATCH_SIZE = Convert.ToInt32(GetParameter(Data, 106, 4));
            //result.BATCH_COUNTER = Convert.ToInt32(GetParameter(Data, 112, 4));
            result.TIGHTENING_STATUS = GetParameter(Data, 118, 1) == "1" ? true : false;
            //result.BATCH_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 121, 1));
            result.TORQUE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 124, 1));
            result.ANGLE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 127, 1));
            result.RUNDOWN_ANGLE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 130, 1));
            //result.CURRENT_MONITORING_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 133, 1));
            //result.SELFTAP_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 136, 1));
            //result.PREVAIL_TORQUE_MONITORING_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 139, 1));
            //result.PREVAIL_TORQUE_COMPENSATE_STATUS = (TighteningValueStatus)Convert.ToInt32(GetParameter(Data, 142, 1));
            result.TIGHTENING_ERROR_STATUS = TighteningErrorStatusConvertFromBytes(GetBinStringFromBytes(Data, 145, 10));
            result.TORQUE_MIN_LIMIT = Convert.ToDouble(GetParameter(Data, 157, 6)) / 100.0;
            result.TORQUE_MAX_LIMIT = Convert.ToDouble(GetParameter(Data, 165, 6)) / 100.0;
            result.TORQUE_FINAL_TARGET = Convert.ToDouble(GetParameter(Data, 173, 6)) / 100.0;
            result.TORQUE = Convert.ToDouble(GetParameter(Data, 181, 6)) / 100.0;
            result.ANGLE_MIN_LIMIT = Convert.ToInt32(GetParameter(Data, 189, 5));
            result.ANGLE_MAX_LIMIT = Convert.ToInt32(GetParameter(Data, 196, 5));
            result.ANGLE_FINAL_TARGET = Convert.ToInt32(GetParameter(Data, 203, 5));
            result.ANGLE = Convert.ToInt32(GetParameter(Data, 210, 5));
            result.RUNDOWN_ANGLE_MIN = Convert.ToInt32(GetParameter(Data, 217, 5));
            result.RUNDOWN_ANGLE_MAX = Convert.ToInt32(GetParameter(Data, 224, 5));
            result.RUNDOWN_ANGLE = Convert.ToInt32(GetParameter(Data, 231, 5));
            //result.CURRENT_MONITORING_MIN = Convert.ToDouble(GetParameter(Data, 238, 3)) / 100.0;
            //result.CURRENT_MONITORING_MAX = Convert.ToDouble(GetParameter(Data, 243, 3)) / 100.0;
            //result.CURRENT_MONITORING_VALUE = Convert.ToDouble(GetParameter(Data, 248, 3)) / 100.0;
            //result.SELFTAP_MIN = Convert.ToDouble(GetParameter(Data, 253, 6)) / 100.0;
            //result.SELFTAP_MAX = Convert.ToDouble(GetParameter(Data, 261, 6)) / 100.0;
            //result.SELFTAP_TORQUE = Convert.ToDouble(GetParameter(Data, 269, 6)) / 100.0;
            //result.PREVAIL_TORQUE_MONITORING_MIN = Convert.ToDouble(GetParameter(Data, 277, 6)) / 100.0;
            //result.PREVAIL_TORQUE_MONITORING_MAX = Convert.ToDouble(GetParameter(Data, 285, 6)) / 100.0;
            //result.PREVAIL_TORQUE = Convert.ToDouble(GetParameter(Data, 293, 6)) / 100.0;
            result.TIGHTENING_ID = Convert.ToInt32(GetParameter(Data, 301, 10));
            //result.JOB_SEQUENCE_NUMBER = Convert.ToInt32(GetParameter(Data, 313, 5));
            result.SYNC_TIGHTENING_ID = Convert.ToInt32(GetParameter(Data, 320, 5));
            //result.TOOL_SERIAL_NUMBER = GetParameter(Data, 327, 14);
            result.TIMESTAMP = DateTime.Parse($"{GetParameter(Data, 343, 10)} {GetParameter(Data, 354, 8)}");
            //result.LAST_CHANGE_IN_PARAMETER_SET = DateTime.Parse($"{GetParameter(Data, 364, 10)} {GetParameter(Data, 375, 8)}");

            return result;
        }


    }
}
