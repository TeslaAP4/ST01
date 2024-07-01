using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Acura3._0.Classes
{
    public class VirtualKeyboard
    {
        //IMPORTANT - In order to use User32, current application must run as Administrator.
        #region User32
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmd);

        [DllImport("user32.dll", EntryPoint = "MoveWindow", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private class ShowWindowPara
        {
            public const uint SW_HIDE = 0;
            public const uint SW_SHOWNORMAL = 1;
            public const uint SW_NORMAL = 1;
            public const uint SW_SHOWMINIMIZED = 2;
            public const uint SW_SHOWMAXIMIZED = 3;
            public const uint SW_MAXIMIZE = 3;
            public const uint SW_SHOWNOACTIVATE = 4;
            public const uint SW_SHOW = 5;
            public const uint SW_MINIMIZE = 6;
            public const uint SW_SHOWMINNOACTIVE = 7;
            public const uint SW_SHOWNA = 8;
            public const uint SW_RESTORE = 9;
        }
        #endregion

        private static string _ApplicationName = null;
        public bool bIsEnabled = false;

        public VirtualKeyboard(string _appName)
        {
            _ApplicationName = _appName;
        }

        public void HideKeyboard()
        {
            if(!bIsEnabled) return;
            try
            {
                Process[] _prcsList = Process.GetProcesses();
                foreach (Process _prcs in _prcsList)
                {
                    if (_prcs.ProcessName == "osk")
                    {
                        _prcs.Kill();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ShowKeyboard(int posX,int posY)
        {
            if (!bIsEnabled) return;
            try
            {
                IntPtr _currHandler = IntPtr.Zero;
                Process[] _prcsList = Process.GetProcesses();
                foreach (Process _prcs in _prcsList)
                {
                    if (_prcs.ProcessName == "osk")
                    {
                        _currHandler = _prcs.MainWindowHandle;
                        ShowWindow(_currHandler, (int)ShowWindowPara.SW_SHOWNORMAL);
                        MoveWindow(_currHandler, posX, posY, 1000, 400, true);
                        return;
                    }
                }

                var path64 = Path.Combine(Directory.GetDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "winsxs"), "amd64_microsoft-windows-osk_*")[0], "osk.exe");
                var path32 = @"C:\\windows\\system32\\osk.exe";
                var path = Environment.Is64BitOperatingSystem ? path64 : path32; //IF current OS is 64-bit will execute osk in 64-bit else 32-bit

                if (!File.Exists(path32)) //Hardcoded as current app run in 32-bit only
                {
                    MessageBox.Show("Virtual keyboard does not installed in current Windows!", _ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                Process _process = Process.Start(path32);

                Task.Factory.StartNew(() => 
                {
                    _prcsList = Process.GetProcesses();
                    foreach (Process _prcs in _prcsList)
                    {
                        if (_prcs.ProcessName == "osk")
                        {
                            while (_currHandler == IntPtr.Zero)
                            {
                                _currHandler = _process.MainWindowHandle;
                                Thread.Sleep(10);
                            }

                            ShowWindow(_currHandler, (int)ShowWindowPara.SW_SHOWNORMAL);
                            MoveWindow(_currHandler, posX, posY, 1000, 400, true);
                            return;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _ApplicationName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
