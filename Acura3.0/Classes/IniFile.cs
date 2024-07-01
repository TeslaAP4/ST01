using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
/************************************************/
//This class is VCL Like TINIFile
//Copy from http://www.dotblogs.com.tw/atowngit/archive/2009/09/01/10358.aspx
//Copy at 2012/09/12 
//Author: atowngit 
//Modify: Shunepi
//Ver 1.0.0.0 2012/09/12
//Ver 1.0.0.1 2012/09/27
//Ver 1.0.0.2 2012/10/11
/************************************************/
namespace Acura3._0.Classes
{
    public class IniFile : IDisposable
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private bool bDisposed = false;
        private string _FilePath = string.Empty;
        //-------------------------------------------------------------------------------------------
        public string FilePath
        {
            get
            {
                if (_FilePath == null)
                    return string.Empty;
                else
                    return _FilePath;
            }
            set
            {
                if (_FilePath != value)
                    _FilePath = value;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="path">檔案路徑</param>      
        public IniFile(string path)
        {
            _FilePath = path;
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 解構子
        /// </summary>
        ~IniFile()
        {
            Dispose(false);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 釋放資源(程式設計師呼叫)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 釋放資源(給系統呼叫的)
        /// </summary>        
        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
            {
                return;
            }
            if (IsDisposing)
            {
                //補充：

                //這裡釋放具有實做 IDisposable 的物件(資源關閉或是 Dispose 等..)
                //ex: DataSet DS = new DataSet();
                //可在這邊 使用 DS.Dispose();
                //或是 DS = null;
                //或是釋放 自訂的物件
                //因為我沒有這類的物件，故意留下這段 code ;若繼承這個類別，
                //可覆寫這個函式
            }

            bDisposed = true;
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 設定 KeyValue 以 String 型態寫入值
        /// </summary>
        /// <param name="IN_Section">Section</param>
        /// <param name="IN_Key">Key</param>
        /// <param name="IN_Value">Value</param>
        public void WriteString(string IN_Section, string IN_Key, string IN_Value)
        {
            WritePrivateProfileString(IN_Section, IN_Key, IN_Value, this._FilePath);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 設定 KeyValue 以 Integer 型態寫入值
        /// </summary>
        /// <param name="IN_Section">Section</param>
        /// <param name="IN_Key">Key</param>
        /// <param name="IN_Value">Value</param>
        public void WriteInteger(string IN_Section, string IN_Key, int IN_Value)
        {
            WritePrivateProfileString(IN_Section, IN_Key, IN_Value.ToString(), this._FilePath);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 設定 KeyValue 以 Boolen 型態寫入值
        /// </summary>
        /// <param name="IN_Section">Section</param>
        /// <param name="IN_Key">Key</param>
        /// <param name="IN_Value">Value</param>
        public void WriteBoolen(string IN_Section, string IN_Key, bool IN_Value)
        {
            WritePrivateProfileString(IN_Section, IN_Key, IN_Value.ToString(), this._FilePath);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 設定 KeyValue 以 Double 型態寫入值
        /// </summary>
        /// <param name="IN_Section">Section</param>
        /// <param name="IN_Key">Key</param>
        /// <param name="IN_Value">Value</param>
        public void WriteDouble(string IN_Section, string IN_Key, double IN_Value)
        {
            WritePrivateProfileString(IN_Section, IN_Key, IN_Value.ToString(), this._FilePath);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 取得 Key 相對的 Value 值，若沒有則使用預設值(DefaultValue)；以 String 型態傳回
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">Key</param>
        /// <param name="DefaultValue">DefaultValue</param>        
        public string ReadString(string Section, string Key, string DefaultValue)
        {
            StringBuilder sbResult = null;
            try
            {
                sbResult = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, "", sbResult, 255, this._FilePath);
                return (sbResult.Length > 0) ? sbResult.ToString() : DefaultValue;
            }
            catch
            {
                return DefaultValue;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 取得 Key 相對的 Value 值，若沒有則使用預設值(DefaultValue)；以 Integer 型態傳回
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">Key</param>
        /// <param name="DefaultValue">DefaultValue</param>        
        public int ReadInteger(string Section, string Key, int DefaultValue)
        {
            StringBuilder sbResult = null;
            try
            {
                sbResult = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, "", sbResult, 255, this._FilePath);
                return (sbResult.Length > 0) ? int.Parse(sbResult.ToString()) : DefaultValue;
            }
            catch
            {
                return DefaultValue;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 取得 Key 相對的 Value 值，若沒有則使用預設值(DefaultValue)；以 Boolen 型態傳回
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">Key</param>
        /// <param name="DefaultValue">DefaultValue</param>        
        public bool ReadBoolen(string Section, string Key, bool DefaultValue)
        {
            StringBuilder sbResult = null;
            try
            {
                sbResult = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, "", sbResult, 255, this._FilePath);
                return (sbResult.Length > 0) ? bool.Parse(sbResult.ToString()) : DefaultValue;
            }
            catch
            {
                return DefaultValue;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 取得 Key 相對的 Value 值，若沒有則使用預設值(DefaultValue)；以 Double 型態傳回
        /// </summary>
        /// <param name="Section">Section</param>
        /// <param name="Key">Key</param>
        /// <param name="DefaultValue">DefaultValue</param>        
        public double ReadDouble(string Section, string Key, double DefaultValue)
        {
            StringBuilder sbResult = null;
            try
            {
                sbResult = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, "", sbResult, 255, this._FilePath);
                return (sbResult.Length > 0) ? double.Parse(sbResult.ToString()) : DefaultValue;
            }
            catch
            {
                return DefaultValue;
            }
        }
    }
}
