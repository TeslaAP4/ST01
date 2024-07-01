using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Acura3._0.Classes
{
    public class MTCom
    {
        public const int SUMMARY_CHANNEL_1 = 13;
        public const int SUMMARY_CHANNEL_2 = 14;
        public const int TRACE_CHANNEL = 15;
        public enum Unit : int
        {
            MTU_mNm = 0,
            MTU_cNm,
            MTU_Nm,
            MTU_mN,
            MTU_N,
            MTU_kN,
            MTU_inlbf,
            MTU_lbf,
            MTU_inozf,
            MTU_gcm,
            MTU_kgm,
            MTU_ftlbf,
            MTU_ozf,
            MTU_kgf,
            MTU_gf,
            MTU_INVALID = -1,
        }
        public enum DeviceStatus : int
        {
            MT_DEVICE_NOT_FOUND = 0,
            MT_DEVICE_PRESENT, 
            MT_DEVICE_CONNECTED,
            MT_DEVICE_READY
        }
        public enum DeviceType : int
        {
            MT_DEVICE_UNKNOWN = 0,
            MT_DEVICE_MICROTEST_MC,
            MT_DEVICE_MT_G4,
            MT_DEVICE_ACTA_MT4,
            MT_DEVICE_MTF400_BASIC,
            MT_DEVICE_MTF400_ADVANCED
        }

        public enum ErrorCode : int
        {
            MT_OK = 0,
            MT_ERR_INVALID_VERSION = -1,
            MT_ERR_INVALID_PARAMETER = -2,
            MT_ERR_INVALID_DATA = -3,
            MT_ERR_INVALID_CHECKSUM = -4,
            MT_ERR_CONNECTION_ERROR = -5,
            MT_ERR_INVALID_MESSAGE_ID = -6,
            MT_ERR_MUTEX_TIMEOUT = -7,
            MT_ERR_PIPE_READ_FAILED = -8,
            MT_ERR_PIPE_WRITE_FAILED = -9,
            MT_ERR_USB_READ_FAILED = -10,
            MT_ERR_USB_WRITE_FAILED = -11,
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
        public struct TracePoint
        {
            [MarshalAs(UnmanagedType.R8)]
            public double torque;
            [MarshalAs(UnmanagedType.R8)]
            public double angle;
        }
        public readonly static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("MTCom64.dll", EntryPoint = "MT_Init")]
        public static extern bool Init();

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetDeviceList")]
        public static extern bool GetDeviceList([MarshalAs(UnmanagedType.LPStr)] StringBuilder devices, [MarshalAs(UnmanagedType.I4)] out int numDevices);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetDeviceInfo")]
        public static extern bool GetDeviceInfo([MarshalAs(UnmanagedType.LPStr)] string serial, [MarshalAs(UnmanagedType.I4)] out DeviceStatus deviceStatus, [MarshalAs(UnmanagedType.I4)] out DeviceType deviceType);

        [DllImport("MTCom64.dll", EntryPoint = "MT_Open")]
        public static extern IntPtr Open([MarshalAs(UnmanagedType.LPStr)] string serial, int reserved);

        [DllImport("MTCom64.dll", EntryPoint = "MT_Close")]
        public static extern void Close(ref IntPtr client);

        [DllImport("MTCom64.dll", EntryPoint = "MT_Clear")]
        public static extern bool Clear(IntPtr client, int channel);

        [DllImport("MTCom64.dll", EntryPoint = "MT_WriteSet")]
        public static extern bool WriteSet(IntPtr client, int channel, [MarshalAs(UnmanagedType.LPArray)] byte[] dataset, int noBytes);

        [DllImport("MTCom64.dll", EntryPoint = "MT_ReadSet")]
        public static extern bool ReadSet(IntPtr client, int channel, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] buffer,
         [MarshalAs(UnmanagedType.I4)] out int noBytes, int timeOut);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetSummary")]
        public static extern bool GetSummary(IntPtr client, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] summary, [MarshalAs(UnmanagedType.I4)] out int noBytes, [MarshalAs(UnmanagedType.I4)] out int sequenceNo);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetNoTraceChannels")]
        public static extern bool GetNoTraceChannels(IntPtr client, [MarshalAs(UnmanagedType.I4)] out int noTraceChannels);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetTraceInfo")]
        public static extern bool GetTraceInfo(IntPtr client, int traceChannel, [MarshalAs(UnmanagedType.I4)] out int noPoints, [MarshalAs(UnmanagedType.I4)] out int sampleRate, [MarshalAs(UnmanagedType.I4)] out int torqueUnit);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetTracePoints")]
        public static extern bool GetTracePoints(IntPtr client, int traceChannel, [MarshalAs(UnmanagedType.LPArray)] [Out] TracePoint[] points, int startPoint, int maxPoints, [MarshalAs(UnmanagedType.I4)] out int noPoints);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetOutput")]
        public static extern bool GetOutput(IntPtr client, [MarshalAs(UnmanagedType.I4)] out int output);

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetLastError")]
        public static extern ErrorCode GetLastError();

        [DllImport("MTCom64.dll", EntryPoint = "MT_GetDllVersion")]
        public static extern bool GetDllVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder version);

        [DllImport("MTCom64.dll", EntryPoint = "MT_Clear")]
        public static extern bool MT_Clear(IntPtr client, int channel);
    }
}