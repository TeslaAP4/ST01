using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
//using CsvHelper;

/// <summary>
/// Version 1.1 20191221 增加图片保存
/// Version 1.2 20191221 增加文件夹删除超期文件和文件夹功能
/// Version 1.3 20200810 增加保存路径+生产数据列首字字符串外部设置参数+流程Log
/// Version 1.4 20200810 增加设备运行状态Log，Error，Pause，Run，Stop
/// Version 1.5 20200817  保存生产数据/tar数据传入数据改为数组格式
/// Version 1.6 20200819  在生产数据文件里，查找某个特定列的特定数据，并反馈查找列数据+保存数据和Tar增加自定义保存基路径下文件夹名称
/// Version 1.7 20200819  增加Recipe和Setting参数改变保存修改数据
/// </summary>

namespace Acura3._0.Classes
{
    class SaveFile
    {
    
        /// <summary>
        /// TarFile上传路径
        /// </summary>
        public static string strTarPath = "C://tar";
        /// <summary>
        /// 本地文件数据保存路径
        /// </summary>
        public static string strFileRotePath = "D://DataSave";
        /// <summary>
        /// 保存生产数据列首字符串，不同列名称用英文逗号,间隔
        /// </summary>
        public static string strDataColumnName = "";//"SaveTime,Station,PosX,TimesY,ResultZ,XOffset,YOffset,AngOffset"
        /// <summary>
        /// 保存csv文件，分列字符串用英文逗号间隔
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fullPath"></param>
        public static void SaveCSV(string str, string fullPath)
        {
                FileInfo fi = new FileInfo(fullPath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    sw.WriteLine(str);
                    sw.Close();
                    fs.Close();
        }
        /// <summary>
        /// 保存csv文件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fullPath"></param>
        public static void SaveCSV(string[] str, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string strMid = "";
            for (int i = 0; i < str.Length; i++)
            {
                strMid += str[i] + ",";
            }
            sw.WriteLine(strMid);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 保存报错信息；保存文件的根目录如D：/aaa/11.txt
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sSaveData"></param>
        public static void SaveErr(string sSaveData)
        {
            //数据记录
            string sfileName = strFileRotePath+"/Err";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            SaveCSV(DateTime.Now.ToString("HH:mm:ss") + "," + sSaveData, strFileName);
        }
        /// <summary>
        /// 用户保存 
        /// </summary>
        /// <param name="sUser"></param>
        public static void SaveUser(string sUser)
        {
            //数据记录
            string sfileName = strFileRotePath+"/User";
            string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            SaveCSV(DateTime.Now.ToString("HH:mm:ss") + "," + sUser, strFileName);
        }     
        /// <summary>
        /// 保存tar文件 "C:\\tar\\aa.tar" 不同传入输入列为不同行数据
        /// </summary>
        /// <param name="sFullFilePath"></param>
        /// <param name="sSaveData"></param>
        /// <returns></returns>
        public static void SaveTar(string strTarName, string[] sSaveData)
        {
                //本地存储Tar文件夹
                string sfileName = strFileRotePath+"/Tar/" + DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(sfileName))
                {
                    Directory.CreateDirectory(sfileName);
                }
                //tar上传文件夹
            if (!Directory.Exists(strTarPath))
            {
                Directory.CreateDirectory(strTarPath);
            }

                string sFullFilePath;
                FileStream fs;
                StreamWriter sw;
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        sFullFilePath = sfileName + "/" + DateTime.Now.ToString("HHmmss") + "-" + strTarName;
                    }
                    else
                    {
                        sFullFilePath = strTarPath + "/" + strTarName;
                    }
                    fs = new FileStream(sFullFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    for (int i = 0; i < sSaveData.Length; i++)
                    {
                        sw.WriteLine(sSaveData[i]);
                    }
                    sw.Close();
                    fs.Close();
                }
        }
        /// <summary>
        /// 保存tar文件 "C:\\tar\\aa.tar" 不同传入输入列为不同行数据,本地保存路径文件夹名称
        /// </summary>
        /// <param name="sFullFilePath"></param>
        /// <param name="sSaveData"></param>
        /// <returns></returns>
        public static void SaveTar(string strLocalFileName,string strTarName, string[] sSaveData)
        {
            //本地存储Tar文件夹
            string sfileName = strLocalFileName + "//" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(sfileName))
            {
                Directory.CreateDirectory(sfileName);
            }
            //tar上传文件夹
            if (!Directory.Exists(strTarPath))
            {
                Directory.CreateDirectory(strTarPath);
            }

            string sFullFilePath;
            FileStream fs;
            StreamWriter sw;
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    sFullFilePath = sfileName + "/" + DateTime.Now.ToString("HHmmss") + "-" + strTarName;
                }
                else
                {
                    sFullFilePath = strTarPath + "/" + strTarName;
                }
                fs = new FileStream(sFullFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                for (int i = 0; i < sSaveData.Length; i++)
                {
                    sw.WriteLine(sSaveData[i]);
                }
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>  
        /// 保存CogDisplay控件的内容到指定文件路径+图片名称
        /// </summary>  
        /// <param name="displayControl">控件对象</param>  
        /// <param name="filePath">文件路径</param>  
        //public static bool SaveImage(CogDisplay displayControl, string fileName)//"Picture.png"  
        //{
        //    try
        //    {
        //        string strFileName = strFileRotePath+"/Picture/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + fileName;
        //        if (displayControl.Image == null)
        //            return true;
        //        string directoryName = Path.GetDirectoryName(strFileName);
        //        if (!Directory.Exists(directoryName))
        //        {
        //            Directory.CreateDirectory(directoryName);
        //        }
        //        displayControl.CreateContentBitmap(Cognex.VisionPro.Display.CogDisplayContentBitmapConstants.Image).Save(strFileName);
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}
        /// <summary>
        /// 删除设定文件夹内的文件夹和文件（创建时间超过设定天数）
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="iSaveDays"></param>
        public static void DeleteFile(string strFile, int iSaveDays)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(strFile);//字符串转路径
                FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();//获取路径下的所有文件信息

                foreach (FileSystemInfo info in fileInfo)//遍历文件
                {
                    TimeSpan spand = DateTime.Now - info.CreationTime.Date;//文件距离当前时间差
                    int s = spand.Days;
                    if (s > iSaveDays)//文件创建时间是否超过设定天数
                    {
                        if (info is DirectoryInfo)//判断是否是文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(info.FullName);
                            subdir.Delete(true);//删除文件
                        }
                        else//文件删除文件
                        {
                            File.Delete(info.FullName);
                        }
                        MiddleLayer.RecordF.LogShow(SysPara.UserName + "  " + "超过文件保存时间，自动删除文件", true);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        private static string[] strOlderLog = new string[255];
        private static object lockobject = new object();
        /// <summary>
        /// 保存流程Log，如果想分列存储请将保存字符串列用英文逗号,间隔，最多255个
        /// </summary>
        /// <param name="sSaveLog"></param>      
        public static void SaveLog(int iFlowNo ,string sSaveLog)
        {
            Task.Factory.StartNew(() =>
            {
                lock (lockobject)
                {
                    if (strOlderLog[iFlowNo] != sSaveLog)
                    {
                        strOlderLog[iFlowNo] = sSaveLog;
                        //数据记录
                        string sfileName = strFileRotePath + "/FlowLog";
                        string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

                        if (!Directory.Exists(sfileName))
                        {
                            Directory.CreateDirectory(sfileName);
                        }
                        if (!File.Exists(strFileName))
                        {
                            SaveCSV(strDataColumnName, strFileName);//列首名称
                        }
                        SaveCSV(DateTime.Now.ToString("HH:mm:ss:fff") + "," + sSaveLog, strFileName);
                    }
                }
            });
        }

        /// <summary>
        /// 保存设备状态信息，Error，Pause，Stop，Run
        /// </summary>
    

        /// <summary>
        /// 保存参数修改数据，用户，参数名，原来数据内容，最新的数据内容跟
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="ParaName"></param>
        /// <param name="strOldValue"></param>
        /// <param name="strNewValue"></param>
        public static void SaveSettingChange(string strUser,string ParaName,string strOldValue,string strNewValue)
        {
            Task.Factory.StartNew(() =>
            {
                lock (lockobject)
                {
                        //数据记录
                        string sfileName = strFileRotePath + "/SettingChangeLog";
                        string strFileName = sfileName + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                        if (!Directory.Exists(sfileName))
                        {
                            Directory.CreateDirectory(sfileName);
                        }
                        if (!File.Exists(strFileName))
                        {
                            SaveCSV(strDataColumnName, strFileName);//列首名称
                        }
                        SaveCSV(DateTime.Now.ToString("HH:mm:ss:fff") + "," + strUser+","+ ParaName+",Old:"+ strOldValue+ ",New:"+strNewValue, strFileName);
                }
            });
        }

        /// <summary>
        /// 在生产数据文件里查询指定天数csv文件，并查找指定列的内容，并返回特定列的信息
        /// </summary>
        public  static   List<string[]> listTotalData = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
        public static string strSearchFile=  strFileRotePath+"/ProductData";//默认保存路径
        /// <summary>
        /// 到指定路径下面查找iDays天前的数据并存入list Total DATa列表内
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="iDays"></param>
        /// <returns></returns>
        public static void readCsv( int iDays)
        {
            listTotalData.Clear();
            DateTime dtNow = DateTime.Now;
            for (int i = 0; i <= iDays; i++)
            {
                string strFileMid = strSearchFile + "//" + dtNow.AddDays(-1 * i).ToString("yyyy-MM-dd") + ".csv";
                //没有对应日期文件
                if (!File.Exists(strFileMid))
                {
                    continue;
                }
                StreamReader reader = new StreamReader(strFileMid);
                string line = "";
                List<string[]> listStrArr = new List<string[]>();//数组List，相当于可以无限扩大的二维数组。
                line = reader.ReadLine();//读取一行数据
                while (line != null)
                {
                    if(line!="")
                        {
                        listStrArr.Add(line.Split(','));//将文件内容分割成数组
                    }
                    line = reader.ReadLine();
                }
                reader.Dispose();
                // listTotalData = listA.Concat(listB).ToList<int>();Concat          //保存重复项
                listTotalData = listStrArr.Union(listTotalData).ToList<string[]>();          //剔除重复项
            }
        }
        /// <summary>
        /// 在所有数据里面查找列iCodeColumn编号内容等于strBarcode，返回对应行所有数据
        /// </summary>
        /// <param name="iCodeColumn"></param>
        /// <param name="strBarcode"></param>
        /// <param name="iWeightColumn"></param>
        /// <returns></returns>
        public static string SearchCode(int iCodeColumn, string strBarcode,int iWeightColumn)
        {
            if (listTotalData.Count == 0)
            {
                return "Err donot find the barcode no data";
            }

            List<string[]> listSearch = listTotalData;
            listSearch.Reverse();
            string sMid;
            int iMid;

            //bool bFindIT=false;
            //for (int i = 0; i < listSearch.Count; i++)
            //{
            //    iMid = i;
            //    sMid = listSearch[i][iCodeColumn];
            //    sMid = sMid.Trim();
            //    if (sMid== strBarcode.Trim())
            //    {
            //        bFindIT = true;
            //        break;
            //    }
            //}
            //iMid = 0;
             iMid = listSearch.FindIndex(x => x[iCodeColumn].Trim() == strBarcode.Trim());//返回列编号
            if (iMid == -1)
            {
                return "Err donot find the barcode";
            }
            else
            {
                sMid = listSearch[iMid][iWeightColumn];
                return sMid;
            }
           
        }

        /// <summary>
        /// 增加查询数据，数组形式
        /// </summary>
        /// <param name="strData"></param>
        public static void ListAddData(string[] strData)
        {
            listTotalData.Add(strData);
        }

        /// <summary>
        /// Sava Parameter Change
        /// </summary>
        /// <param name="ModifyData"></param>
        public static void SaveRecipeChange(string ModifyData)
        {
            string strFileName = strFileRotePath + "//ParameterChange" + "//" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            FileInfo fi = new FileInfo(strFileName);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            if (!File.Exists(strFileName))
            {
                File.AppendAllText(strFileName, "DateTime" + "," + "User" + "," + "Recipe/Setting" + "," + "Parameter Name" + "," + "Original Value" + "," + "Update Value" + "," + "\r\n");
            }
            SaveCSV(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ","+SysPara.UserName+"," + ModifyData, strFileName);
        }

        /// <summary>
        /// Log保存
        /// </summary>
        /// <param name="Textshow">需要保存的文字</param>
        public static void SaveLog(string Textshow)
        {
            //string sFileNamePerson =MiddleLayer.SystemF.GetSettingValue("PSet", "LogSavePath") + "//" +
            //        DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
            //        DateTime.Now.Day.ToString("00");
            //string strFileNamePerson = sFileNamePerson + "\\" + DateTime.Now.ToString("D") + ".txt";
            string sFileName = "C://DataSave/LogSave";
            string strFileName= sFileName + "\\" + DateTime.Now.ToString("D") + ".txt";
            //if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
            //{
            //    Directory.CreateDirectory(sFileNamePerson);
            //}
            if (!Directory.Exists(sFileName))//创建本地数据保存文件夹
            {
                Directory.CreateDirectory(sFileName);
            }
            string sAppend = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff") + " " + Textshow + Environment.NewLine;
            //File.AppendAllText(strFileNamePerson, sAppend);
            File.AppendAllText(strFileName, sAppend);
        }

        ///// <summary>
        ///// LINK数据
        ///// </summary>
        ///// <param name="path">保存地址</param>
        ///// <param name="scan">载具码</param>
        ///// <param name="pcbscan">PCB码</param>
        ///// <param name="startTime">扫码时间</param>
        ///// <param name="endTime">组装完成时间</param>
        ///// <param name="tp">OKNG</param>
        //public static void SaveLink(string path, string scan, string pcbscan, string startTime, string endTime, bool tp)
        //{
        //    string sFileName = path + ":" + "//" + MiddleLayer.ConveyorF.GetSettingValue("PSet", "FilenameSave") + "//" +
        //            DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + "//" +
        //            DateTime.Now.Day.ToString("00") + "//" + DateTime.Now.Hour.ToString("00") + "//" + "tarslink";
        //    string SavePos1 = sFileName + "\\" + scan + ".txt";
        //    string sFileNamePerson = path + "://DataSave/LinkSave";
        //    string SavePos2 = sFileNamePerson + "\\" + scan + ".txt";
        //    string CM = "C" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Cm");
        //    string PCN = "N" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Pcn");
        //    string LINK = "N" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Link");
        //    string LN = "L" + MiddleLayer.GantryF.GetRecipeValue("RSet", "LineN");
        //    string MN = "M" + MiddleLayer.GantryF.GetRecipeValue("RSet", "M");
        //    if (!Directory.Exists(sFileName))//创建上传MES文件夹
        //    {
        //        Directory.CreateDirectory(sFileName);
        //    }
        //    if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
        //    {
        //        Directory.CreateDirectory(sFileNamePerson);
        //    }
        //    string TPFP = tp ? "TP" : "TF";

        //    string date = null;

        //    date = "S" + scan + Environment.NewLine + CM + Environment.NewLine + PCN + Environment.NewLine + "P" + LINK + Environment.NewLine + LN + Environment.NewLine +
        //        TPFP + Environment.NewLine + "[" + startTime + Environment.NewLine + "]" + endTime + Environment.NewLine + MN + Environment.NewLine + "d" + pcbscan;
        //    File.AppendAllText(SavePos1, date);
        //    File.AppendAllText(SavePos2, date);
        //}

        //public static int Count1 = 1;
        //public static int Count2 = 1;
        //public static string Scan1 = "";
        //public static string Scan2 = "";
        //public static string OtherOK1;
        //public static string OtherNG1;
        //public static string OtherOK2;
        //public static string OtherNG2;
        //public static bool TPFP1;
        //public static bool TPFP2;
        //public static string StartTime1;
        //public static string StartTime2;

        ///// <summary>
        ///// MES1数据
        ///// </summary>
        ///// <param name="path">保存地址</param>
        ///// <param name="scan">SN码</param>
        ///// <param name="startTime">扫码时间</param>
        ///// <param name="endTime">最后一颗螺丝时间</param>
        ///// <param name="kind">机台工位</param>
        ///// <param name="torque">扭力</param>
        ///// <param name="angle">角度</param>
        ///// <param name="tp">螺丝结果</param>
        ///// <param name="isEndSave">是否最后一颗螺丝</param>
        //public static void SaveMES1(string scan, string startTime, string endTime, int kind, double torque, double angle, string height,bool tp, bool isEndSave)
        //{
        //    if (Count1 == 1)
        //    {
        //        //Scan1 = scan;
        //        StartTime1 = startTime;
        //    }
        //    string sFileName = MiddleLayer.GantryF.GetSettingValue("PSet", "BackTars") + "//" + "Tars" + "//" +
        //            DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
        //            DateTime.Now.Day.ToString("00");
        //    string SavePos1 = sFileName + "\\" + scan + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        //    string savePos1 = SavePos1 + ".txt";
        //    string sFileNamePerson ="DataSave/MesData";
        //    string SavePos2 = sFileNamePerson + "\\" + scan;
        //    string savePos2 = SavePos2 + ".txt";
        //    string sTars = MiddleLayer.GantryF.GetSettingValue("PSet", "Tars");
        //    string SavePos3 = sTars + "\\" + scan;
        //    string savePos3 = SavePos3 + ".txt";
        //    string CM = "C" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Cm");
        //    string PCN = "N" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Pcn");
        //    string PS = "P" + MiddleLayer.GantryF.GetRecipeValue("RSet", "PS");
        //    string LN = "L" + MiddleLayer.GantryF.GetRecipeValue("RSet", "LineN");

        //    string strRs = (tp) ? ("dPass") : ("dFail");
        //    if (!Directory.Exists(sFileName))//创建上传MES文件夹
        //    {
        //        Directory.CreateDirectory(sFileName);
        //    }
        //    if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
        //    {
        //        Directory.CreateDirectory(sFileNamePerson);
        //    }
        //    if (!Directory.Exists(sTars))//创建本地数据保存文件夹
        //    {
        //        Directory.CreateDirectory(sTars);
        //    }
        //    if (Count1 == 1)
        //    {
        //        OtherOK1 = null;
        //        OtherNG1 = null;
        //        TPFP1 = false;
        //        if (tp)
        //        {
        //            OtherOK1 += $"M{kind}-{SysPara.Sdate1} T" + Environment.NewLine +
        //                $"d{torque}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} A" + Environment.NewLine +
        //                $"d{angle}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} F" + Environment.NewLine +
        //                $"d{height}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} R" + Environment.NewLine +
        //                strRs + Environment.NewLine;
        //        }
        //        else
        //        {
        //            TPFP1 = true;
        //            OtherOK1 += $"M{kind}-{SysPara.Sdate1} TF" + Environment.NewLine +
        //                $"d{torque}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} AF" + Environment.NewLine +
        //                $"d{angle}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} FF" + Environment.NewLine +
        //                $"d{height}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} RF" + Environment.NewLine +
        //                strRs + Environment.NewLine;
        //        }
        //        Count1++;
        //    }
        //    else
        //    {
        //        if(Count1>7)
        //        {
        //            if (tp)
        //            {
        //                OtherOK1 += $"M{kind}-{SysPara.Sdate2} T" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} A" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} F" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} R" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //            else
        //            {
        //                TPFP1 = true;
        //                OtherOK1 += $"M{kind}-{SysPara.Sdate2} TF" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} AF" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} FF" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} RF" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //        }
        //        else
        //        {
        //            if (tp)
        //            {
        //                OtherOK1 += $"M{kind}-{SysPara.Sdate1} T" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} A" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} F" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} R" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //            else
        //            {
        //                TPFP1 = true;
        //                OtherOK1 += $"M{kind}-{SysPara.Sdate1} TF" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} AF" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} FF" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} RF" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //        }
        //        if (isEndSave)
        //        {
        //            //string tf = TPFP1 ? "TF" : "TP";
        //            string date = null;
        //            //if (tf == "TP")
        //            {
        //                date = "S" + scan + Environment.NewLine + CM + Environment.NewLine + PCN + Environment.NewLine + PS + Environment.NewLine + LN + Environment.NewLine + "TP"
        //                + Environment.NewLine + "[" + StartTime1 + Environment.NewLine + "]" + endTime + Environment.NewLine + OtherOK1;
        //            }
        //            //else if (tf == "TF")
        //            //{
        //            //    date = "S" + scan + Environment.NewLine + CM + Environment.NewLine + PCN + Environment.NewLine + PS + Environment.NewLine + LN + Environment.NewLine + tf
        //            //    + Environment.NewLine + "[" + StartTime1 + Environment.NewLine + "]" + endTime + Environment.NewLine + OtherOK1 +
        //            //    "Mmanual screw" + Environment.NewLine + OtherNG1;
        //            //}
        //            File.AppendAllText(savePos1, date);
        //            File.AppendAllText(savePos2, date);
        //            File.AppendAllText(savePos3, date);
        //            Count1 = 0;
        //        }
        //        Count1++;
        //    }
        //}

        ///// <summary>
        ///// MES2数据
        ///// </summary>
        ///// <param name="path">保存地址</param>
        ///// <param name="scan">SN码</param>
        ///// <param name="startTime">扫码时间</param>
        ///// <param name="endTime">最后一颗螺丝时间</param>
        ///// <param name="kind">螺丝种类</param>
        ///// <param name="torque">扭力</param>
        ///// <param name="angle">角度</param>
        ///// <param name="tp">螺丝结果</param>
        ///// <param name="isEndSave">是否最后一颗螺丝</param>
        //public static void SaveMES2(string scan, string startTime, string endTime, int kind, double torque, double angle,string height ,bool tp, bool isEndSave)
        //{
        //    if (Count2 == 1)
        //    {
        //        //Scan2 = scan;
        //        StartTime2 = startTime;
        //    }
        //    string sFileName = MiddleLayer.GantryF.GetSettingValue("PSet", "BackTars") + "//" + "Tars" + "//" +
        //            DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
        //            DateTime.Now.Day.ToString("00");
        //    string SavePos1 = sFileName + "\\" + scan + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        //    string savePos1 = SavePos1 + ".txt";
        //    string sFileNamePerson = "DataSave/MesData";
        //    string SavePos2 = sFileNamePerson + "\\" + scan;
        //    string savePos2 = SavePos2 + ".txt";
        //    string sTars = MiddleLayer.GantryF.GetSettingValue("PSet", "Tars");
        //    string SavePos3 = sTars + "\\" + scan ;
        //    string savePos3 = SavePos3 + ".txt";
        //    string CM = "C" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Cm");
        //    string PCN = "N" + MiddleLayer.GantryF.GetRecipeValue("RSet", "Pcn");
        //    string PS = "P" + MiddleLayer.GantryF.GetRecipeValue("RSet", "PS");
        //    string LN = "L" + MiddleLayer.GantryF.GetRecipeValue("RSet", "LineN");

        //    string strRs = (tp) ? ("dPass") : ("dFail");
        //    if (!Directory.Exists(sFileName))//创建上传MES文件夹
        //    {
        //        Directory.CreateDirectory(sFileName);
        //    }
        //    if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
        //    {
        //        Directory.CreateDirectory(sFileNamePerson);
        //    }
        //    if (!Directory.Exists(sTars))//创建本地数据保存文件夹
        //    {
        //        Directory.CreateDirectory(sTars);
        //    }
        //    if (Count2 == 1)
        //    {
        //        OtherOK2 = null;
        //        OtherNG2 = null;
        //        TPFP2 = false;
        //        if (tp)
        //        {
        //            OtherOK2 += $"M{kind}-{SysPara.Sdate1} T" + Environment.NewLine +
        //                $"d{torque}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} A" + Environment.NewLine +
        //                $"d{angle}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} F" + Environment.NewLine +
        //                $"d{height}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} R" + Environment.NewLine +
        //                strRs + Environment.NewLine;
        //        }
        //        else
        //        {
        //            TPFP2 = true;
        //            OtherOK2 += $"M{kind}-{SysPara.Sdate1} TF" + Environment.NewLine +
        //                $"d{torque}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} AF" + Environment.NewLine +
        //                $"d{angle}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} FF" + Environment.NewLine +
        //                $"d{height}" + Environment.NewLine +
        //                $"M{kind}-{SysPara.Sdate1} RF" + Environment.NewLine +
        //                strRs + Environment.NewLine;
        //        }
        //        Count2++;
        //    }
        //    else
        //    {
        //        if(Count2>7)
        //        {
        //            if (tp)
        //            {
        //                OtherOK2 += $"M{kind}-{SysPara.Sdate2} T" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} A" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} F" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} R" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //            else
        //            {
        //                TPFP2 = true;
        //                OtherOK2 += $"M{kind}-{SysPara.Sdate2} TF" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} AF" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} FF" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate2} RF" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //        }
        //        else
        //        {
        //            if (tp)
        //            {
        //                OtherOK2 += $"M{kind}-{SysPara.Sdate1} T" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} A" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} F" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} R" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //            else
        //            {
        //                TPFP2 = true;
        //                OtherOK2 += $"M{kind}-{SysPara.Sdate1} TF" + Environment.NewLine +
        //                    $"d{torque}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} AF" + Environment.NewLine +
        //                    $"d{angle}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} FF" + Environment.NewLine +
        //                    $"d{height}" + Environment.NewLine +
        //                    $"M{kind}-{SysPara.Sdate1} RF" + Environment.NewLine +
        //                    strRs + Environment.NewLine;
        //            }
        //        }
        //        if (isEndSave)
        //        {
        //            //string tf = TPFP2 ? "TF" : "TP";
        //            string date = null;
        //            //if (tf == "TP")
        //            {
        //                date = "S" + scan + Environment.NewLine + CM + Environment.NewLine + PCN + Environment.NewLine + PS + Environment.NewLine + LN + Environment.NewLine + "TP"
        //                + Environment.NewLine + "[" + StartTime2 + Environment.NewLine + "]" + endTime + Environment.NewLine + OtherOK2;
        //            }
        //            //else if (tf == "TF")
        //            //{
        //            //    date = "S" + scan + Environment.NewLine + CM + Environment.NewLine + PCN + Environment.NewLine + PS + Environment.NewLine + LN + Environment.NewLine + tf
        //            //    + Environment.NewLine + "[" + StartTime2 + Environment.NewLine + "]" + endTime + Environment.NewLine + OtherOK2 +
        //            //    "Mmanual screw" + Environment.NewLine + OtherNG2;
        //            //}
        //            File.AppendAllText(savePos1, date);
        //            File.AppendAllText(savePos2, date);
        //            File.AppendAllText(savePos3, date);
        //            Count2 = 0;
        //        }
        //        Count2++;
        //    }
        //}

        ///// <summary>
        ///// 保存图像
        ///// </summary>
        ///// <param name="imageResult">需要保存的图像</param>
        ///// <param name="strSN">二维码</param>
        ///// <param name="work_mark">视觉任务</param>
        ///// <param name="imageokng">拍照结果</param>
        ///// <param name="js">Mark拍照位</param>
        //public static void SaveImage1(CogDisplay displayControl, string strSN, VppComp.FlowControl work_mark, string js, bool imageokng)
        //{
        //    string DirectoryPath = MiddleLayer.GantryF.GetSettingValue("PSet", "BackTars") + "//" + "Image" + "//" +
        //                        DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
        //                        DateTime.Now.Day.ToString("00");
        //    string DirectoryPath2 = "";
        //    string DirectoryPath3 = "";
        //    string DirectoryPath4 = "";
        //    if (imageokng)
        //    {
        //        DirectoryPath2 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "OK";
        //        DirectoryPath3 = DirectoryPath2 + DateTime.Now.ToString("yyyy-MM-dd-HH");
        //        DirectoryPath4 = DirectoryPath3 + ".txt";
        //    }
        //    else
        //    {
        //        DirectoryPath2 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "NG";
        //        DirectoryPath3 = DirectoryPath2 + DateTime.Now.ToString("yyyy-MM-dd-HH");
        //        DirectoryPath4 = DirectoryPath3 + ".txt";
        //        //DirectoryPath3 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "原图";
        //    }
        //    string pathInit = "";
        //    string pathInit2 = "";
        //    string pathResult = "";
        //    if (!Directory.Exists(DirectoryPath2))
        //    {
        //        Directory.CreateDirectory(DirectoryPath2);
        //    }
        //    try
        //    {
        //        if (imageokng)
        //        {
        //            pathResult = DirectoryPath2 + "\\" + "工位1 Mark" + js + ".png";
        //            displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Display).Save(pathResult);
        //            File.AppendAllText(DirectoryPath4, SysPara.S_ccd1offset);
        //        }
        //        else
        //        {
        //            //work_mark.SaveImage(1, DirectoryPath2, "原图", true);
        //            pathInit = DirectoryPath2 + "\\" + "工位1 Mark" + js + ".png";
        //            displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Display).Save(pathInit);
        //            File.AppendAllText(DirectoryPath4, SysPara.S_ccd1offset);
        //            //pathInit2 = DirectoryPath3 + "\\" + "工位1 Mark" + js + ".bmp";
        //            //displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Image).Save(pathInit2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("图像保存失败！");
        //    }
        //}

        ///// <summary>
        ///// 保存图像
        ///// </summary>
        ///// <param name="imageResult">需要保存的图像</param>
        ///// <param name="strSN">二维码</param>
        ///// <param name="work_mark">视觉任务</param>
        ///// <param name="imageokng">拍照结果</param>
        ///// <param name="js">Mark拍照位</param>
        //public static void SaveImage2(CogDisplay displayControl, string strSN, VppComp.FlowControl work_mark, string js, bool imageokng)
        //{
        //    string DirectoryPath = MiddleLayer.GantryF.GetSettingValue("PSet", "BackTars") + "//" + "Image" + "//" +
        //                        DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
        //                        DateTime.Now.Day.ToString("00");
        //    string DirectoryPath2 = "";
        //    string DirectoryPath3 = "";
        //    string DirectoryPath4 = "";
        //    if (imageokng)
        //    {
        //        DirectoryPath2 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "OK";
        //        DirectoryPath3 = DirectoryPath2 + DateTime.Now.ToString("yyyy-MM-dd-HH");
        //        DirectoryPath4 = DirectoryPath3 + ".txt";
        //    }
        //    else
        //    {
        //        DirectoryPath2 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "NG";
        //        //DirectoryPath3 = DirectoryPath + "//" + strSN + " " + DateTime.Now.ToString("yyyy-MM-dd-HH") + "//" + "原图";
        //        DirectoryPath3 = DirectoryPath2 + DateTime.Now.ToString("yyyy-MM-dd-HH");
        //        DirectoryPath4 = DirectoryPath3 + ".txt";
        //    }
        //    string pathInit = "";
        //    string pathInit2 = "";
        //    string pathResult = "";
        //    if (!Directory.Exists(DirectoryPath2))
        //    {
        //        Directory.CreateDirectory(DirectoryPath2);
        //    }
        //    try
        //    {
        //        if (imageokng)
        //        {
        //            pathResult = DirectoryPath2 + "\\" + "工位2 Mark" + js + ".png";
        //            displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Display).Save(pathResult);
        //            File.AppendAllText(DirectoryPath4, SysPara.S_ccd2offset);
        //        }
        //        else
        //        {
        //            //work_mark.SaveImage(1, DirectoryPath2, "原图", true);
        //            pathInit = DirectoryPath2 + "\\" + "工位2 Mark" + js + ".png";
        //            displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Display).Save(pathInit);
        //            File.AppendAllText(DirectoryPath4, SysPara.S_ccd2offset);
        //            //pathInit2 = DirectoryPath3 + "\\" + "工位1 Mark" + js + ".bmp";
        //            //displayControl.CreateContentBitmap(CogDisplayContentBitmapConstants.Image).Save(pathInit2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("图像保存失败！");
        //    }
        //}

        /// <summary>
        /// Alarm保存
        /// </summary>
        /// <param name="Textshow">需要保存的报警</param>
        public static void SaveAlarm(string Textshow)
        {
            string sFileNamePerson = MiddleLayer.SystemF.GetSettingValue("PSet", "BackTars") + "//" + "Alarm" + "//" +
                                    DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
                                    DateTime.Now.Day.ToString("00");
            string strFileNamePerson = sFileNamePerson + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
            if (!Directory.Exists(sFileNamePerson))//创建本地数据保存文件夹
            {
                Directory.CreateDirectory(sFileNamePerson);
            }
            string sAppend = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss ffff") + " " + Textshow + Environment.NewLine;
            File.AppendAllText(strFileNamePerson, sAppend);
        }

    }

}

