using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/***********************************************************************/
// Class name : VCL like TStringList For C#
// Author     : Shunepi
// Theory     : Using List<string>
// Version    : 1.0.0.2
// Date       : 2013/11/2
// 11/2  修改 AppendToFile的方式
/***********************************************************************/
namespace Acura3._0.Classes
{
    public class StringList
    {
        private List<string> slStrings;
        private string sDelimitedtext;
        private string sDelimiter;
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 建構子
        /// </summary>
        public StringList()
        {
            slStrings = new List<string>();
            Delimiter = ",";
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 解構子
        /// </summary>
        ~StringList()
        {
            slStrings.Clear();
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 清除List中所有 String 內容
        /// </summary>
        public void Clear()
        {
            slStrings.Clear();
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 傳回 List 中所有 String 數量
        /// </summary>
        public int Count
        {
            get
            {
                return slStrings.Count;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 回傳 List 中符合第一個特定字串的索引值(由0開始)
        /// </summary>
        public int Find(string s)
        {
            return slStrings.IndexOf(s);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 對 List 進行排序
        /// </summary>
        public void Sort()
        {
            slStrings.Sort();
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 刪除特定索引的 String
        /// </summary>
        public void Delete(int index)
        {
            if (index > slStrings.Count || index < 0) throw new ArgumentOutOfRangeException();
            slStrings.RemoveAt(index);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 對 String 以 Delimiter 進行分隔，並覆寫於 List 中
        /// </summary>
        public string Delimitedtext
        {
            set
            {
                slStrings.Clear();
                sDelimitedtext = value;
                slStrings = sDelimitedtext.Split(char.Parse(sDelimiter)).ToList<string>();
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 設定 List 用於進行 Delimited 的分隔符號
        /// </summary>
        public string Delimiter
        {
            set
            {
                sDelimiter = value;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將 List 中特定索引的兩個 String 進行交換
        /// </summary>
        public void Exchange(int index1, int index2)
        {
            if (index1 >= slStrings.Count || index1 < 0) throw new ArgumentOutOfRangeException();
            if (index2 >= slStrings.Count || index2 < 0) throw new ArgumentOutOfRangeException();
            string sTmp = slStrings[index1];
            slStrings[index2] = slStrings[index1];
            slStrings[index1] = sTmp;
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將一個 String 寫入 List 中
        /// </summary>
        public void Add(string s)
        {
            if (s.EndsWith(Environment.NewLine))
                slStrings.Add(s);
            else slStrings.Add(s + Environment.NewLine);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 回傳該索引的 String
        /// </summary>
        public string this[int index]
        {
            get
            {
                if (index >= slStrings.Count || index < 0) throw new ArgumentOutOfRangeException();
                else return slStrings[index];
            }
            set
            {
                slStrings[index] = value;
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 傳回 List 中所有陣列並合併為 String 型態的 Text 傳回
        /// </summary>
        public string Text
        {
            set
            {
                slStrings.Clear();
                slStrings.Add(Text);
            }
            get
            {
                System.Text.StringBuilder s = new System.Text.StringBuilder(slStrings.Count);
                for (int i = 0; i < slStrings.Count; i++)
                {
                    s.Append(slStrings[i]);
                }
                return s.ToString();
            }
        }//-------------------------------------------------------------------------------------------
         /// <summary>
         /// 將 List 附加於特定檔案之後，並使用系統預設編碼；檔案不存在則會新建一個檔案
         /// </summary>
        public void AppendToFile(string fileName)
        {
            if (!File.Exists(fileName)) File.CreateText(fileName);
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string data = sr.ReadToEnd();
                sr.Close();
                StreamWriter sw = new StreamWriter(fileName);
                if (data.EndsWith(Environment.NewLine))
                    data = data.Substring(0, data.LastIndexOf(Environment.NewLine));
                sw.WriteLine(data + this.Text);
                sw.Close();
                //File.AppendAllText (fileName, this.Text, System.Text.ASCIIEncoding.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine("%s,%s", e.Message, fileName);
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將 List 儲存於特定檔案，並設定編碼
        /// </summary>
        public void SaveToFile(string fileName, Encoding encoding)
        {
            if (!File.Exists(fileName)) File.CreateText(fileName);
            try
            {
                File.WriteAllText(fileName, this.Text, encoding);
            }
            catch (Exception e)
            {
                Console.WriteLine("%s,%s", e.Message, fileName);
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將 List 儲存於特定檔案，使用系統預設編碼
        /// </summary>
        public void SaveToFile(string fileName)
        {
            try
            {
                File.AppendAllText(fileName, this.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine("%s,%s", e.Message, fileName);
            }
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 讀取一個檔案至 List，並使用系統預設編碼
        /// </summary>
        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return;
            this.Clear();
            System.IO.StreamReader sr2 = new System.IO.StreamReader(fileName, System.Text.ASCIIEncoding.Default);
            while (sr2.Peek() >= 0)
            {
                this.Add(sr2.ReadLine());
            }
            sr2.Close();
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 讀取一個檔案至 List，並使用編碼
        /// </summary>
        public void LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            if (!File.Exists(fileName)) return;
            this.Clear();
            System.IO.StreamReader sr2 = new System.IO.StreamReader(fileName, encoding);
            while (sr2.Peek() >= 0)
            {
                this.Add(sr2.ReadLine());
            }
            sr2.Close();
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 尋找 List 中，開頭為特定字串的第一個 index；由 0 開始，找不到時回傳 -1
        /// </summary>
        public int FindStartWith(string s)
        {
            for (int i = 0; i < slStrings.Count; i++)
            {
                if (slStrings[i].StartsWith(s)) return i;
            }
            return -1;
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 尋找 List 中，字尾為特定字串的第一個 index；由 0 開始，找不到時回傳 -1
        /// </summary>
        public int FindEndsWith(string s)
        {
            for (int i = 0; i < slStrings.Count; i++)
            {
                if (slStrings[i].EndsWith(s)) return i;
            }
            return -1;
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將一個 List 寫入此 List 中
        /// </summary>
        /// <param name="sList"></param>
        public void AddRange(List<string> sList)
        {
            foreach (string s in sList)
                Add(s);
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 將一個 TStringList 寫入此 List 中
        /// </summary>
        /// <param name="sList"></param>
        public void AddRange(StringList sList)
        {
            for (int i = 0; i < sList.Count; i++)
                Add(sList[i]);
        }
        //-------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------
    }
}
    
