using Acura3._0.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Acura3._0.Classes
{
    public class ReadCOM
    {
        System.Timers.Timer Timer_Com = new System.Timers.Timer();
        public string Port;
        public bool ConnectionState;

        //压力传感器时间实例化
        public JTimer J_PressureTm = new JTimer();
        //压力发送指令
       public  string Readcomdata = "01 03 00 1E 00 02 A4 0D";

        public static DataTable dtTable_Com = new DataTable();

        public NPSerialProt SPCom = new NPSerialProt();

        public static PreasureData preasureData = new PreasureData();
        public struct PreasureData
        {
            public string Time;
            public string Code;
            public int PreasureDirver;
            public double Preasure;
        }

        #region COM
        public bool ConnectCom(string Port)
        {
            if (ConnectionState)
            {
                return true;
            }
            if (SPCom.ConnectComPressL(Port, "9600,8,0,1"))
            {
                 return   ConnectionState = true;
            }
            return false;
        }
        public void DisconnectCom()
        {
            if (SPCom.ConnectStates())
            {
                SPCom.Dispose();
                ConnectionState = false;
            }
        }


        /// <summary>
        /// 读取COM内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_Work(string string1)
        {
            string strContent = "";
            try
            {
                bool b1 = SPCom.WriteHEX(string1);//:001RDGROSS=   获取称重值
                Thread.Sleep(100);
                string strMid = SPCom.readHEX();
                if (strMid=="-999")
                {
                    return strContent="Error";
                }
                if (strMid.Length > 0)
                {
                    // 按空格分割字符串
                    string[] splitStr = strMid.Split(' ');
                    //十六进制转10进制  整数位
                    double d1 = Convert.ToInt32((splitStr[3] + splitStr[4]), 16);
                    //十六进制转10进制  小数位
                    double d2 = Convert.ToInt32((splitStr[5] + splitStr[6]), 16);

                    //d1 = d1 > 6000 ? 0 : d1;
                    //d2 = d2 > 6000 ? 0 : d2;
                    //最终结果  整数位+小数位
                    strContent = (d1 + d2 / 10).ToString();
                }
                return strContent;
            }
            catch (Exception ex)
            {
                return strContent;
            }
        }
        /// <summary>
        /// 读取COM内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_COM()
        {
            string strContent = "";
            bool b1 = SPCom.Write(":001RDGROSS=0" + "\r\n");//:001RDGROSS="READ[OD][OA]"   获取称重值
            Thread.Sleep(100);
            string strMid = SPCom.readSting();
            if (strMid == null)
            {
                return "Null";
                //JSDK.Alarm.Show("9999");
            }
            if (strMid.Length > 0)
            {
                strMid = strMid.Trim().Replace("\r\n", "").Trim();
                string r1 = strMid;
                r1 = r1.Replace("+", "-");
                string[] strArray = r1.Split('-');
                if (strArray.Length > 0)
                {
                    r1 = strArray[strArray.Length - 1];
                    r1 = r1.Replace("?", "");
                }
                strContent = r1.Trim('.');
                if (strContent.Contains(','))
                {
                    strContent = strContent.Split(',')[1];
                }
            }
            return strContent;
        }

        public void ClearZero()
        {
            bool b1 = SPCom.Write(":001CLSZERO=0" + "\r\n");
        }
        /// <summary>
        /// COM1当前读取到的压力值
        /// </summary>
        public double _CurrentCom_Pressure = 0;
        /// <summary>
        /// X2轴运行的状态
        /// </summary>
        public bool _AxisX2_RunComplete = true;
        /// <summary>
        /// Com1监测是否结束
        /// </summary>
        public bool _Com1Read_Complete = false;


        /// <summary>
        /// RefreshUI
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }

        /// <summary>
        ///  COM pressure values are read continuously
        /// </summary>
        /// <param name="c_Pressure">Chart</param>
        /// <param name="dgvData_AutoCom_Pressure">DataGridView</param>
        /// <param name="sn">SN</param>
        /// <param name="path">Path</param>
        public void ContinuedRead_COM(Chart c_Pressure, DataGridView dgvData_AutoCom_Pressure,string sn,string path)
        {
            ChartClear_Pressure_RubberSheet(c_Pressure, dgvData_AutoCom_Pressure);
            dtTable_Com = new DataTable();
            dtTable_Com.Columns.Add("Index", typeof(int));
            dtTable_Com.Columns.Add("Pressure", typeof(double));
            dtTable_Com.Columns.Add("Times", typeof(int));

            J_PressureTm.Restart();
            ClearZero();
            Task.Factory.StartNew(() =>
            {
                #region MyRegion
                _Com1Read_Complete = false;
                while (!_Com1Read_Complete)
                {
                    string strPressure = ReadContent_Work(Readcomdata);
                    if (strPressure.Trim().Length > 0)
                    {
                        double _Pressure;
                        if (double.TryParse(strPressure, out _Pressure))
                        {
                            _CurrentCom_Pressure = _Pressure;
                            double _PressureN = _Pressure /*/ 100 * 9.8*/;
                            int _time = Convert.ToInt32((dtTable_Com.Rows.Count + 1) * Timer_Com.Interval);
                            DataRow dtRow = dtTable_Com.NewRow();
                            dtRow["Index"] = dtTable_Com.Rows.Count + 1;
                            dtRow["Pressure"] = Math.Round(_PressureN, 3);
                            dtRow["Times"] = _time;
                            dtTable_Com.Rows.Add(dtRow);
                            ChartAdd_Pressure_RubberSheet(c_Pressure, dgvData_AutoCom_Pressure,dtTable_Com.Rows.Count + 1,_PressureN,sn,path);
                            Thread.Sleep(100);
                            strPressure = ReadContent_Work(Readcomdata);
                            if (strPressure.Trim().Length > 0)
                            {
                                if (double.TryParse(strPressure, out _Pressure))
                                {
                                    _PressureN = _Pressure;
                                    _time = Convert.ToInt32((dtTable_Com.Rows.Count + 1) * Timer_Com.Interval);
                                    dtRow = dtTable_Com.NewRow();
                                    dtRow["Index"] = dtTable_Com.Rows.Count + 1;
                                    dtRow["Pressure"] = Math.Round(_PressureN, 3);
                                    dtRow["Times"] = _time;
                                    dtTable_Com.Rows.Add(dtRow);
                                    ChartAdd_Pressure_RubberSheet(c_Pressure, dgvData_AutoCom_Pressure,dtTable_Com.Rows.Count + 1, _PressureN, sn, path, "Over");
                                }
                            }
                            ClearZero();
                        }
                    }
                }
                #endregion
            });
        }

        #region//压力曲线
        object objchart1 = new object();
        public void ChartAdd_Pressure_RubberSheet(Chart c_Pressure,DataGridView dgvData_AutoCom_Pressure, int _index, double _Pressure, string sn, string path,string _Type = "")
        {
            //Task.Factory.StartNew(() => {

            lock (objchart1)
            {
                RefreshDifferentThreadUI(c_Pressure, () =>
                {
                    if (c_Pressure.ChartAreas[0].AxisX.Maximum < _index)
                    {
                        c_Pressure.ChartAreas[0].AxisX.Maximum = _index;
                    }
                    c_Pressure.Series[0].Points.AddXY(_index, _Pressure);
                });
                if (_Type == "Over")
                {
                    RefreshDifferentThreadUI(c_Pressure, () =>
                    {
                        c_Pressure.Series[0].LegendText = "Min: 0 \r\nMax: " + _Pressure;
                    });
                    RefreshDifferentThreadUI(dgvData_AutoCom_Pressure, () =>
                    {
                        dgvData_AutoCom_Pressure.DataSource = ReadCOM.dtTable_Com;
                    });
                    preasureData.Time = DateTime.Now.ToString("hh:mm:ss");
                    preasureData.Code = sn;
                    preasureData.PreasureDirver = 1;
                    preasureData.Preasure = c_Pressure.Series[0].Points.FindMaxByValue().YValues[0]; //曲线最大值
                    WritePreasureData(preasureData, dgvData_AutoCom_Pressure,path);
                }
            }
            //this.chart1.Series[0].Points.Add(_Pressure,_time);
            //});
        }

        public void ChartClear_Pressure_RubberSheet(Chart c_Pressure, DataGridView dgvData_AutoCom_Pressure)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (c_Pressure != null)
                        if (c_Pressure.Series.Count > 0)
                            if (c_Pressure.Series[0] != null)
                                if (c_Pressure.Series[0].Points.Count > 0)
                                {
                                    RefreshDifferentThreadUI(c_Pressure, () => { c_Pressure.Series[0].Points.Clear(); });
                                }
                }
                catch (Exception ex)
                { }
            });
        }

        #endregion

        #region//压力表格
        public void WritePreasureData(PreasureData preasureData, DataGridView dgvData_AutoCom_Pressure,string path)
        {
            string CsvHeader = "";
            RefreshDifferentThreadUI(dgvData_AutoCom_Pressure, () =>
            {
                dgvData_AutoCom_Pressure.DataSource = null;
                int index = dgvData_AutoCom_Pressure.Rows.Add();
                dgvData_AutoCom_Pressure.ClearSelection();
                dgvData_AutoCom_Pressure.Rows[index].Cells[0].Value = preasureData.Time.ToString();
                dgvData_AutoCom_Pressure.Rows[index].Cells[1].Value = preasureData.Code.ToString();
                dgvData_AutoCom_Pressure.Rows[index].Cells[2].Value = preasureData.PreasureDirver.ToString();
                dgvData_AutoCom_Pressure.Rows[index].Cells[3].Value = preasureData.Preasure.ToString("f3");
                dgvData_AutoCom_Pressure.Rows[index].Selected = true;
                dgvData_AutoCom_Pressure.FirstDisplayedScrollingRowIndex = index;
            });
            for (int i = 0; i < dgvData_AutoCom_Pressure.Columns.Count; i++)
            {
                CsvHeader += dgvData_AutoCom_Pressure.Columns[i].HeaderText + ",";
            }
            SaveCSV(path, preasureData, CsvHeader);
        }
        #endregion

        #region//压力数据保存
        public void SaveCSV(string filePath, PreasureData preasureData, string header)
        {
            try
            {
                filePath += "//" + "PressureData" + "//" +
                                    DateTime.Now.Year.ToString("0000") + "//" + DateTime.Now.Month.ToString("00") + "//" +
                                    DateTime.Now.Day.ToString("00");
                string folderPath = filePath + "\\" + DateTime.Now.ToString("yyyy_MM_dd") + ".csv";
                string str = null;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (!File.Exists(folderPath))
                {
                    if (header != "")
                    {
                        str = header + "\r\n";
                    }
                }
                str += preasureData.Time.ToString() + "," + preasureData.Code.ToString() + ","  +
                    preasureData.PreasureDirver.ToString() + "," +
                    preasureData.Preasure.ToString() ;
                FileStream fs = new FileStream(folderPath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(str);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #endregion
    }
}
