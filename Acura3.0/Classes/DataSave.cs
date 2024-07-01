using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acura3._0.Classes
{
    public class FileDataControl
    {
        public void WriteCSV(string filePathName, string[] ls)
        {
            WriteCSV(filePathName, true, ls);
        }

        public void WriteCSV(string filePathName, bool append, string[] ls)
        {
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                StreamWriter streamWriter = new StreamWriter(filePathName, append, Encoding.Default);
                streamWriter.WriteLine(string.Join(",", ls));
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<string[]> ReadCSV(string filePathName)
        {
            List<string[]> list = new List<string[]>();
            string[] array = File.ReadAllLines(filePathName, Encoding.GetEncoding("GB2312"));
            for (int i = 0; i < array.Length; i++)
            {
                string[] item = array[i].Split(',');
                list.Add(item);
            }
            return list;
        }

        public void WriteExcelData(string FileName, string[] data)
        {
            FileInfo fileInfo = new FileInfo(FileName);
            DirectoryInfo directory = fileInfo.Directory;
            if (!directory.Exists)
            {
                directory.Create();
            }
            WriteCSV(FileName, data);
        }
    }
    public class DataFormat
    {
        private string gName;

        [Browsable(true)]
        [Description("")]
        [Category("Design")]
        [DefaultValue("")]
        public string Name
        {
            get
            {
                return gName;
            }
            set
            {
                gName = value;
            }
        }

        public DataFormat()
        {
            Name = "data";
        }
    }

    public class TypeCollection1 : Component
    {
        private FileDataControl FileDataControl = new FileDataControl();

        private string gText;

        private string gSavePath;

        private List<DataFormat> gitems = new List<DataFormat>();

        private IContainer components = null;

        [Browsable(true)]
        [Description("")]
        [Category("Design")]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                return gText;
            }
            set
            {
                gText = value;
            }
        }

        [Browsable(true)]
        [Description("")]
        [Category("Design")]
        [DefaultValue("")]
        public string SavePath
        {
            get
            {
                return gSavePath;
            }
            set
            {
                gSavePath = value;
            }
        }

        [TypeConverter(typeof(CollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("DataFormat")]
        [Description("图像文件集")]
        public List<DataFormat> Items
        {
            get
            {
                return gitems;
            }
            set
            {
                gitems = value;
            }
        }

        public TypeCollection1()
        {
            InitializeComponent();
        }

        public void SaveDataToFile(DateTime DataDate, string barcode, string[] data)
        {
            string[] obj = new string[8]
            {
                SavePath,
                "\\",
                null,
                null,
                null,
                null,
                null,
                null
            };
            DateTime now = DateTime.Now;
            obj[2] = now.ToString("yyyy");
            obj[3] = "\\";
            now = DateTime.Now;
            obj[4] = now.ToString("MM");
            obj[5] = "\\";
            obj[6] = DataDate.ToString("yyyy-MM-dd");
            obj[7] = ".csv";
            string text = string.Concat(obj);
            if (!File.Exists(text))
            {
                string[] array = new string[Items.Count];
                for (int i = 0; i < Items.Count; i++)
                {
                    string text2 = array[i] = Items[i].Name;
                }
                FileDataControl.WriteExcelData(text, array);
            }
            string[] array2 = new string[data.Count() + 2];
            array2[0] = DataDate.ToString("yyyy-MM-dd HH:mm:ss:fff");
            array2[1] = barcode;
            for (int j = 0; j < data.Count(); j++)
            {
                array2[j + 2] = data[j];
            }
            FileDataControl.WriteExcelData(text, array2);
        }

        public void SaveDataToFile(string[] data)
        {
            string[] obj = new string[8]
            {
                SavePath,
                "\\",
                null,
                null,
                null,
                null,
                null,
                null
            };
            DateTime now = DateTime.Now;
            obj[2] = now.ToString("yyyy");
            obj[3] = "\\";
            now = DateTime.Now;
            obj[4] = now.ToString("MM");
            obj[5] = "\\";
            now = DateTime.Now;
            obj[6] = now.ToString("yyyy-MM-dd");
            obj[7] = ".csv";
            string text = string.Concat(obj);
            if (!File.Exists(text))
            {
                string[] array = new string[Items.Count];
                for (int i = 0; i < Items.Count; i++)
                {
                    string text2 = array[i] = Items[i].Name;
                }
                FileDataControl.WriteExcelData(text, array);
            }
            string[] array2 = new string[data.Count() + 2];
            string[] array3 = array2;
            now = DateTime.Now;
            array3[0] = now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            for (int j = 0; j < data.Count(); j++)
            {
                array2[j + 1] = data[j];
            }
            FileDataControl.WriteExcelData(text, array2);
        }

        public List<string[]> ReadDataFromToFile(DateTime starTime1, DateTime endTime1)
        {
            List<string[]> list = new List<string[]>();
            try
            {
                string[] obj = new string[5]
                {
                    SavePath,
                    "\\",
                    null,
                    null,
                    null
                };
                DateTime now = DateTime.Now;
                obj[2] = now.ToString("yyyy");
                obj[3] = "\\";
                now = DateTime.Now;
                obj[4] = now.ToString("MM");
                string path = string.Concat(obj);
                if (!Directory.Exists(path))
                {
                    return list;
                }
                bool flag = false;
                string[] files = Directory.GetFiles(path, "*.csv");
                string[] array = files;
                string[] array2 = array;
                foreach (string path2 in array2)
                {
                    DateTime dateTime = default(DateTime);
                    DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
                    dateTimeFormatInfo.ShortDatePattern = "yyyy-MM-dd";
                    dateTime = Convert.ToDateTime(Path.GetFileNameWithoutExtension(path2), dateTimeFormatInfo);
                    int num2 = DateTime.Compare(dateTime.Date, starTime1.Date);
                    int num3 = DateTime.Compare(dateTime.Date, endTime1.Date);
                    int num4;
                    if ((num2 != 1 || num3 != -1) && num2 != 0)
                    {
                        num4 = ((num3 != 0) ? 1 : 0);
                        goto IL_0103;
                    }
                    num4 = 0;
                    goto IL_0103;
                IL_0103:
                    if (num4 == 0)
                    {
                        string[] array3 = File.ReadAllLines(path2, Encoding.GetEncoding("GB2312"));
                        for (int i = 0; i < array3.Length; i++)
                        {
                            if (i == 0)
                            {
                                if (!flag)
                                {
                                    string[] item = array3[i].Split(',');
                                    list.Add(item);
                                }
                            }
                            else
                            {
                                string[] item2 = array3[i].Split(',');
                                list.Add(item2);
                            }
                        }
                        flag = true;
                    }
                }
                return list;
            }
            catch
            {
                return list;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
        }
    }
}

