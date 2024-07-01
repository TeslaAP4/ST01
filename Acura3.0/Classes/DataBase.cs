using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ADOX;

namespace Acura3._0.Classes
{
    class DataBase
    {
        #region Declarations
        private string _mdbPath;
        #endregion
        #region Public Proporties
        public string mdbPath
        {
            get { return _mdbPath; }
            set { _mdbPath = value; }
        }
        #endregion
        #region Public Function
        // 读取mdb数据 
        public static DataTable ReadAllData(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;
         
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName + " order by 编号 asc";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        Application.DoEvents();
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                        
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static double ReadSingleData(string mdbPath,string tableName,string ColumnName)
        {
            double Result = 0;
            try
            {                
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select [" + ColumnName + "] from [" + tableName + "]";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                if(odrReader.Read())
                {
                    Result = double.Parse(odrReader[ColumnName].ToString());
                }

                //关闭连接 
                odrReader.Close();
                odcConnection.Close();

                return Result;

            }
            catch
            {
                return Result;
            }
        }

        public static DataTable ReadSampleInterval(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        Application.DoEvents();
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();

                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadAllData_Adapter(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                string strSQL = "select * from " + tableName;// +" order by No asc";
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);
                //关闭连接 
                oleDa.Dispose();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch(Exception)
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadAllData_Adapter(string tableName, string mdbPath,string SQL, ref bool success)
        {
            DataTable dt = new DataTable();
            int intRowCount = 0;
            int intColumnCount = 0;
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                string strSQL = "select " + SQL + " from " + tableName + " order by 测试时间 asc";
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);

                intRowCount = dt.Rows.Count;
                intColumnCount = dt.Columns.Count;

                object[] arrColumnName = new object[intColumnCount];
                for (int i = 0; i < intColumnCount; i++)
                {
                    arrColumnName[i] = dt.Columns[i].ColumnName;
                }

                //object[,] arr2DData = new object[intRowCount, intColumnCount];

                    //关闭连接 
                oleDa.Dispose();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadData_Adapter(string mdbPath, string SQL, ref bool Successful)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbDataAdapter oleDa = new OleDbDataAdapter(SQL, odcConnection);
                oleDa.Fill(dt);

                //关闭连接 
                oleDa.Dispose();
                odcConnection.Close();
                Successful = true;
                return dt;
            }
            catch(Exception)
            {
                Successful = false;
                return dt;
            }
        }

        public static DataTable ReadONOFFData(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName + " where 开停状态='ON' or 开停状态='OFF'";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                        Application.DoEvents();
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static bool ReadTCOffSetData(string tableName, string mdbPath, ref double[] OffSetData)
        {
            try
            {
                int intArrIndex = 0;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName + " order by Num asc";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();

                while (odrReader.Read())
                {

                    OffSetData[intArrIndex] = double.Parse(odrReader[odrReader.GetName(1)].ToString());

                    intArrIndex++;
                    if (intArrIndex >= 60) break;
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                if (intArrIndex > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("找不到用于保存热电偶修正值的文件，请联系供应商！","系统提示");
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool UpDataTCOffSetData(string tableName, string mdbPath, double[] OffSetData)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //3、输入查询语句 
                string strSQL = "select * from " + tableName + " order by Num asc";
                //建立读取 
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);
                if (dt.Rows.Count >= 60)
                {
                    for (int i = 0; i < 60; i++)
                    {
                        dt.Rows[i][1] = OffSetData[i];
                    }
                }

                OleDbCommandBuilder sqlcb = new OleDbCommandBuilder(oleDa);
                oleDa.Update(dt);

                oleDa.Dispose();
                odcConnection.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static DataTable ReadONOFFData_Adapter(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //3、输入查询语句 
                string strSQL = "select * from " + tableName + " where 开停状态='ON' or 开停状态='OFF'" + " order by 测试时间 asc";
                //建立读取 
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);
                
                oleDa.Dispose();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadONOFFData_Adapter(string tableName, string mdbPath,string SQL ,ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //3、输入查询语句 
                string strSQL = "select " + SQL + " from " + tableName + " where 开停状态='ON' or 开停状态='OFF'" + " order by 测试时间 asc";
                //建立读取 
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);

                oleDa.Dispose();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadNoteData(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName + " order by 编号 asc";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 1; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 1; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
                        Application.DoEvents();
                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable ReadNoteData_Adapter(string tableName, string mdbPath, ref bool success)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //3、输入查询语句 
                string strSQL = "select * from " + tableName + " order by 编号 asc";
                //建立读取 
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                //查询并显示数据 
                oleDa.Fill(dt);
                //关闭连接 
                oleDa.Dispose();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static double[,] ReadGwData(string tableName, string mdbPath, double Xin, ref ArrayList arraylistIncrement, ref bool success)
        {
            DataTable dt = new DataTable();
            int intColumnCount = 68;
            int intRowCount = 0;
            double douXinTemp = 0;
            double[,] douErrorData=new double[,]{{0,0},{0,0}};

            arraylistIncrement.Clear();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 2; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 2; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();

                    }
                    dt.Rows.Add(dr);
                }

                intColumnCount = dt.Columns.Count;
                intRowCount = dt.Rows.Count;
                DataRow row;
                double[,] dou2Ddata = new double[intRowCount, intColumnCount];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = dt.Rows[i];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dou2Ddata[i, j] = double.Parse(row[j].ToString());
                    }

                    arraylistIncrement.Add(douXinTemp);
                    douXinTemp += Xin;
                }
                    //关闭连接 

                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dou2Ddata;
            }
            catch(Exception)
            {
                success = false;
                return douErrorData;
            }
        }

        public static int ReadGwCurveDataCount(string tableName, string mdbPath, ref bool success)
        {
            int intDataCount = 0;

            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select count(*) from " + tableName;
                //建立读取 
                intDataCount = (int)odCommand.ExecuteScalar();

                success = true;
                return intDataCount;
            }
            catch (Exception)
            {
                success = false;
                return 0;
            }
        }

        public static double ReadGwCurveData(string tableName, string mdbPath, double Xin, ref double[] Xdata,ref double[] realtimeXdata, ref double[,] YMulData,ref double[,]realtimeYdata, ref bool success)
        {
            double douXinTemp = 0;
            int intRow = 0;
            int intColunm = 0;

            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName + " order by 测试时间 asc";
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();

                //查询并显示数据 
                int size = odrReader.FieldCount;

                while (odrReader.Read())
                {
                    for (intColunm = 2; intColunm < size; intColunm++)
                    {
                        YMulData[intRow, intColunm - 2] = double.Parse(odrReader[odrReader.GetName(intColunm)].ToString());
                        realtimeYdata[intRow, intColunm - 2] = YMulData[intRow, intColunm - 2];
                    }
                    douXinTemp += Xin;
                    Xdata[intRow] = douXinTemp;
                    realtimeXdata[intRow] = douXinTemp;
                    intRow++;
                }
                //关闭连接 

                odrReader.Close();
                odcConnection.Close();
                success = true;
                return douXinTemp;
            }
            catch (Exception)
            {
                success = false;
                return 0.0;
            }
        }

        public static double ReadGwCurveData(string tableName, string mdbPath, double Xin, ref List<double> Xdata, ref List<double>[] YMulData, ref bool success)
        {
            double douXinTemp = 0;
            int intRow = 0;
            int intColunm = 0;

            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();

                //查询并显示数据 
                int size = odrReader.FieldCount;

                while (odrReader.Read())
                {
                    for (intColunm = 2; intColunm < size; intColunm++)
                    {
                        YMulData[intColunm - 2].Add(double.Parse(odrReader[odrReader.GetName(intColunm)].ToString()));

                    }
                    douXinTemp += Xin;
                    Xdata.Add(douXinTemp);
                    intRow++;
                }
                //关闭连接 

                odrReader.Close();
                odcConnection.Close();
                success = true;
                return douXinTemp;
            }
            catch (Exception)
            {
                success = false;
                return 0.0;
            }
        }

        public static void DeleteGWData(string tableName, string mdbPath, ref bool success)
        {
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source="  + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "delete * from " + tableName;
                odCommand.ExecuteNonQuery();

                odCommand.Dispose();
                odcConnection.Dispose();
                success = true;
            }
            catch
            {
                success = false;
            }
        }
        // 娄据库查询
        public static DataTable DataBaseQuery_Delete(string tableName, string mdbPath, ref bool success,string strSQL)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = strSQL;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                odrReader.Close();

                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();

                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }

        public static DataTable DataBaseQuery_Condition(string tableName, string mdbPath, ref bool success, string strSQL)
        {
            DataTable dt = new DataTable();
            try
            {
                DataRow dr;

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = strSQL;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();;
                //查询并显示数据 
                int size = odrReader.FieldCount;
                for (int i = 0; i < size; i++)
                {
                    DataColumn dc;
                    dc = new DataColumn(odrReader.GetName(i));
                    dt.Columns.Add(dc);
                }
                while (odrReader.Read())
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < size; i++)
                    {
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();

                    }
                    dt.Rows.Add(dr);
                }
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return dt;
            }
            catch
            {
                success = false;
                return dt;
            }
        }
        // 读取测试曲线数据 
        public static void ReadTestData(string tableName, string mdbPath, ref bool success,ArrayList XYrange,ArrayList Xdata,ArrayList Ydata)
        {

            XYrange.Clear();
            Xdata.Clear();
            Ydata.Clear();

            try
            {

                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //读取行数
                if (odrReader.HasRows)
                {
                    while (odrReader.Read())
                    {
                        XYrange.Add(odrReader[odrReader.GetName(0)]);
                        Xdata.Add(odrReader[odrReader.GetName(1)]);
                        Ydata.Add(odrReader[odrReader.GetName(2)]);

                    }
                }

                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
            }
            catch
            {
                success = false;
            }
        }

        public static int ReadRowData(string tableName, string mdbPath,int RowIndex, ref bool success,ref object[] values)
        {
            int Column_int=0;
            bool temp_bool=false ;
            int intRowCount = 0;
            try
            {
          
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                OleDbCommand odCommand = odcConnection.CreateCommand();
                //3、输入查询语句 
                odCommand.CommandText = "select * from " + tableName;
                //建立读取 
                OleDbDataReader odrReader = odCommand.ExecuteReader();
                //查询并显示数据 
                int size = odrReader.FieldCount;
                //读取行数
                if (odrReader.HasRows)
                {
                    do
                    {
                        temp_bool = odrReader.Read();
                        intRowCount++;
                    } while (temp_bool);
                    
                }
                intRowCount = intRowCount - 1;

                if (RowIndex > intRowCount)
                {
                    success = false;
                    return Column_int;
                }

                odrReader.Close();
                odrReader = odCommand.ExecuteReader();

                for (int i = 0; i < RowIndex; i++)
                {
                    temp_bool = odrReader.Read(); 
                }
                      
                Column_int = odrReader.GetValues(values);
                //关闭连接 
                odrReader.Close();
                odcConnection.Close();
                success = true;
                return Column_int;
            }
            catch
            {
                success = false;
                return Column_int;
            }
        }

        //动态创建表格
        public static void CreateTable(string mdbPath,string TableName)
        {
            Catalog myCatalog = new Catalog();
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
            ADODB.Connection cn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();
            cn.Open(strConn, null, null, -1);
            myCatalog.ActiveConnection = cn;
            ADOX.Table table = new ADOX.Table();
            table.Name = TableName;
            //ADOX.Column column = new ADOX.Column();
            //column.ParentCatalog = myCatalog;
            //column.Name = "ID";
            //column.Type = DataTypeEnum.adInteger;
            //column.DefinedSize = 9;
            //column.Properties["AutoIncrement"].Value = true;
            //table.Columns.Append(column, DataTypeEnum.adInteger  ,9);
            //table.Keys.Append("FirstTablePrimaryKey", KeyTypeEnum.adKeyPrimary, column, null, null);
            //table.Columns.Append("姓名", DataTypeEnum.adVarWChar, 255);
            //table.Columns.Append("度汞X轴Y轴量程", DataTypeEnum.adSingle,9);
            //table.Columns.Append("度汞数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("度汞电压数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样一X轴Y轴量程", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样一数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样一电压数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样二X轴Y轴量程", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样二数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样二电压数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样三X轴Y轴量程", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样三数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样三电压数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样四X轴Y轴量程", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样四数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样四电压数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样五X轴Y轴量程", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样五数据", DataTypeEnum.adSingle, 9);
            //table.Columns.Append("标样五电压数据", DataTypeEnum.adSingle, 9);
            table.Columns.Append("血样X轴Y轴量程", DataTypeEnum.adDouble, 9);
            table.Columns.Append("血样电压数据", DataTypeEnum.adDouble, 9);
            table.Columns.Append("血样测试数据", DataTypeEnum.adDouble, 9);
            myCatalog.Tables.Append(table);

            cn.Close();
        }

        //动态创建表格
        public static void CreateTable(string mdbPath, string TableName,List<CheckBox> ColumnsName)
        {
            try
            {
                if (ColumnsName.Count < 1) return;
                Catalog myCatalog = new Catalog();
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                ADODB.Connection cn = new ADODB.Connection();
                ADODB.Recordset rs = new ADODB.Recordset();
                cn.Open(strConn, null, null, -1);
                myCatalog.ActiveConnection = cn;
                ADOX.Table table = new ADOX.Table();
                //判断表是否存在,如果存在则删除
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    DataTable ExistTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    if (ExistTable != null)
                    {
                        for (int i = 0; i < ExistTable.Rows.Count; i++)
                        {
                            if (ExistTable.Rows[i]["TABLE_NAME"].ToString() == TableName)
                            {
                                myCatalog.Tables.Delete(TableName);
                                break;
                            }
                        }
                    }

                }

                table.Name = TableName;
                table.Columns.Append("测试时间", DataTypeEnum.adDate, 0);
                table.Columns.Append("开停状态", DataTypeEnum.adWChar, 50);
                for (int i = 0; i < ColumnsName.Count; i++)
                {
                    table.Columns.Append(ColumnsName[i].Text, DataTypeEnum.adDouble, 9);
                }
                myCatalog.Tables.Append(table);
                
                cn.Close();
            }
            catch(Exception ec)
            {
                MessageBox.Show(ec.Message);
            }

        }

        public static void CreateTable(string mdbPath, string TableName, List<CheckBox> ColumnsName,bool IsCreateNew)
        {
            try
            {
                if (ColumnsName.Count < 1) return;
                Catalog myCatalog = new Catalog();
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                ADODB.Connection cn = new ADODB.Connection();
                ADODB.Recordset rs = new ADODB.Recordset();
                cn.Open(strConn, null, null, -1);
                myCatalog.ActiveConnection = cn;
                ADOX.Table table = new ADOX.Table();
                if (IsCreateNew)
                {
                    //判断表是否存在,如果存在则删除
                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {
                        conn.Open();
                        DataTable ExistTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        if (ExistTable != null)
                        {
                            for (int i = 0; i < ExistTable.Rows.Count; i++)
                            {
                                if (ExistTable.Rows[i]["TABLE_NAME"].ToString() == TableName)
                                {
                                    myCatalog.Tables.Delete(TableName);
                                    break;
                                }
                            }
                        }

                    }

                    table.Name = TableName;
                    table.Columns.Append("测试时间", DataTypeEnum.adDate, 0);
                    table.Columns.Append("开停状态", DataTypeEnum.adWChar, 50);
                    for (int i = 0; i < ColumnsName.Count; i++)
                    {
                        table.Columns.Append(ColumnsName[i].Text, DataTypeEnum.adDouble, 9);
                    }
                    myCatalog.Tables.Append(table);
                }
                else
                {
                    int intTableColumnsCount = myCatalog.Tables[TableName].Columns.Count;
                    if (ColumnsName.Count < (intTableColumnsCount - 2))
                    {
                        for (int i = 2; i < myCatalog.Tables[TableName].Columns.Count; i++)
                        {
                            if (myCatalog.Tables[TableName].Columns[i].Name != ColumnsName[i - 2].Text)
                            {
                                myCatalog.Tables[TableName].Columns.Delete(i);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                cn.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }

        }

        public static bool IsTableExist(string mdbPath, string TableName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                DataTable ExistTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (ExistTable != null)
                {
                    for (int i = 0; i < ExistTable.Rows.Count; i++)
                    {
                        if (ExistTable.Rows[i]["TABLE_NAME"].ToString() == TableName)
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        public static bool CreateTable(string mdbPath, string TableName, List<string> ColumnsName)
        {
            try
            {
                if (ColumnsName.Count < 1) return false;
                Catalog myCatalog = new Catalog();
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
                ADODB.Connection cn = new ADODB.Connection();
                ADODB.Recordset rs = new ADODB.Recordset();
                cn.Open(strConn, null, null, -1);
                myCatalog.ActiveConnection = cn;
                ADOX.Table table = new ADOX.Table();

                table.Name = TableName;
                for (int i = 0; i < ColumnsName.Count; i++)
                {
                    table.Columns.Append(ColumnsName[i], DataTypeEnum.adDouble, 9);
                }
                myCatalog.Tables.Append(table);

                cn.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //动态删除表格
        public static void DeleteTable(string mdbPath, string TableName,ref bool success,ref string strmse)
        {
            try
            {
                Catalog myCatalog = new Catalog();
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
                ADODB.Connection cn = new ADODB.Connection();
                ADODB.Recordset rs = new ADODB.Recordset();
                cn.Open(strConn, null, null, -1);
                myCatalog.ActiveConnection = cn;
                myCatalog.Tables.Delete(TableName);
                cn.Close();
                success = true;
            }
            catch
            {
                success = false;
                strmse = "操作不成功，请检查表格名称是否正确！！";
            }

        }
        //病人数据
        public static void WritePatientInfoData(string mdbPath, string tableName, ArrayList arrlistPatientInfoData,bool write_Or_updata)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                if (write_Or_updata)
                {
                   // 输入查询语句
                    strSQL = "update " + tableName + " set 标题='" + (string)arrlistPatientInfoData[0] + "',检测项目='" + (string)arrlistPatientInfoData[1] + "',序号='" + (string)arrlistPatientInfoData[2] + "'," +
                        "姓名='" + (string)arrlistPatientInfoData[3] + "',病历号码='" + (string)arrlistPatientInfoData[4] + "',性别='" + (string)arrlistPatientInfoData[5] + "'," +
                        "年龄='" + (string)arrlistPatientInfoData[6] + "',病人类别='" + (string)arrlistPatientInfoData[7] + "',化验号码='" + (string)arrlistPatientInfoData[8] + "'," +
                        "送检物='" + (string)arrlistPatientInfoData[9] + "',送检科室='" + (string)arrlistPatientInfoData[10] + "',送检人='" + (string)arrlistPatientInfoData[11] + "'," +
                        "送检性质='" + (string)arrlistPatientInfoData[12] + "',操作者='" + (string)arrlistPatientInfoData[13] + "',收费价格='" + (string)arrlistPatientInfoData[14] + "'," +
                        "受检日期='" + (string)arrlistPatientInfoData[15] + "'," +
                            "检测结果='" + (string)arrlistPatientInfoData[16] + "'" + "";

                }
                else
                {
                   // string tt = null;
                   // 输入查询语句
                    strSQL = "insert into " + tableName + " values ('" + (string)arrlistPatientInfoData[0] + "','" + (string)arrlistPatientInfoData[1] + "','" + (string)arrlistPatientInfoData[2] + "'," +
                        "'" + (string)arrlistPatientInfoData[3] + "','" + (string)arrlistPatientInfoData[4] + "','" + (string)arrlistPatientInfoData[5] + "','" + (string)arrlistPatientInfoData[6] +
                        "','" + (string)arrlistPatientInfoData[7] + "','" + (string)arrlistPatientInfoData[8] + "','" + (string)arrlistPatientInfoData[9] +
                        "','" + (string)arrlistPatientInfoData[10] + "','" + (string)arrlistPatientInfoData[11] + "','" + (string)arrlistPatientInfoData[12] +
                        "','" + (string)arrlistPatientInfoData[13] + "','" + (string)arrlistPatientInfoData[14] + "','" + (string)arrlistPatientInfoData[15] + "')";

                }
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

  

        }

        //public static void InsertTestData(string mdbPath, string tableName, ArrayList arrlistPatientInfoData)
        //{
        //    string strSQL;
        //    //1、建立连接 
        //    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;

        //    OleDbConnection odcConnection = new OleDbConnection(strConn);
        //    //2、打开连接 
        //    odcConnection.Open();
        //    //建立SQL查询 
        //    OleDbCommand odCommand = odcConnection.CreateCommand();
        //    try
        //    {
        //        // string tt = null;
        //        // 输入查询语句
        //        //strSQL = "insert into " + tableName + " values ('" + (DateTime)arrlistPatientInfoData[0] + "','" + (string)arrlistPatientInfoData[1] + "','" + (double)arrlistPatientInfoData[2] + "'," +
        //        //    "'" + (double)arrlistPatientInfoData[3] + "','" + (double)arrlistPatientInfoData[4] + "','" + (double)arrlistPatientInfoData[5] + "','" + (double)arrlistPatientInfoData[6] +
        //        //    "','" + (double)arrlistPatientInfoData[7] + "','" + (double)arrlistPatientInfoData[8] + "','" + (double)arrlistPatientInfoData[9] +
        //        //    "','" + (double)arrlistPatientInfoData[10] + "','" + (double)arrlistPatientInfoData[11] + "','" + (double)arrlistPatientInfoData[12] +
        //        //    "','" + (double)arrlistPatientInfoData[13] + "','" + (double)arrlistPatientInfoData[14] + "','" + (double)arrlistPatientInfoData[15] +
        //        //    "','" + (double)arrlistPatientInfoData[16] + "','" + (double)arrlistPatientInfoData[17] + "','" + (double)arrlistPatientInfoData[18] +
        //        //    "','" + (double)arrlistPatientInfoData[19] + "','" + (double)arrlistPatientInfoData[20] + "','" + (double)arrlistPatientInfoData[21] +
        //        //    "','" + (double)arrlistPatientInfoData[22] + "','" + (double)arrlistPatientInfoData[23] + "','" + (double)arrlistPatientInfoData[24] +
        //        //    "','" + (double)arrlistPatientInfoData[25] + "','" + (double)arrlistPatientInfoData[26] + "','" + (double)arrlistPatientInfoData[27] +
        //        //    "','" + (double)arrlistPatientInfoData[28] + "','" + (double)arrlistPatientInfoData[29] + "','" + (double)arrlistPatientInfoData[30] +
        //        //    "','" + (double)arrlistPatientInfoData[31] + "','" + (double)arrlistPatientInfoData[32] + "','" + (double)arrlistPatientInfoData[33] +
        //        //    "','" + (double)arrlistPatientInfoData[34] + "','" + (double)arrlistPatientInfoData[35] + "','" + (double)arrlistPatientInfoData[36] +
        //        //    "','" + (double)arrlistPatientInfoData[37] + "','" + (double)arrlistPatientInfoData[38] + "','" + (double)arrlistPatientInfoData[39] +
        //        //    "','" + (double)arrlistPatientInfoData[40] + "','" + (double)arrlistPatientInfoData[41] + "','" + (double)arrlistPatientInfoData[42] +
        //        //    "','" + (double)arrlistPatientInfoData[43] + "','" + (double)arrlistPatientInfoData[44] + "','" + (double)arrlistPatientInfoData[45] +
        //        //    "','" + (double)arrlistPatientInfoData[46] + "','" + (double)arrlistPatientInfoData[47] + "','" + (double)arrlistPatientInfoData[48] +
        //        //    "','" + (double)arrlistPatientInfoData[49] + "','" + (double)arrlistPatientInfoData[50] + "','" + (double)arrlistPatientInfoData[51] +
        //        //    "','" + (double)arrlistPatientInfoData[52] + "','" + (double)arrlistPatientInfoData[53] + "','" + (double)arrlistPatientInfoData[54] +
        //        //    "','" + (double)arrlistPatientInfoData[55] + "','" + (double)arrlistPatientInfoData[56] + "','" + (double)arrlistPatientInfoData[57] +
        //        //    "','" + (double)arrlistPatientInfoData[58] + "','" + (double)arrlistPatientInfoData[59] + "','" + (double)arrlistPatientInfoData[60] +
        //        //    "','" + (double)arrlistPatientInfoData[61] + "','" + (double)arrlistPatientInfoData[62] + "','" + (double)arrlistPatientInfoData[63] +
        //        //    "','" + (double)arrlistPatientInfoData[64] + "','" + (double)arrlistPatientInfoData[65] + "','" + (double)arrlistPatientInfoData[66] +
        //        //    "','" + (double)arrlistPatientInfoData[67] + "','" + (double)arrlistPatientInfoData[68] + "','" + (double)arrlistPatientInfoData[69] + "')";
        //        strSQL = "insert into " + tableName + " values ('" + arrlistPatientInfoData[0] + "','" + arrlistPatientInfoData[1] + "','" + arrlistPatientInfoData[2] + "'," +
        // "'" + arrlistPatientInfoData[3] + "','" +arrlistPatientInfoData[4] + "','" + arrlistPatientInfoData[5] + "','" + arrlistPatientInfoData[6] +
        // "','" + arrlistPatientInfoData[7] + "','" + arrlistPatientInfoData[8] + "','" + arrlistPatientInfoData[9] +
        // "','" + arrlistPatientInfoData[10] + "','" + arrlistPatientInfoData[11] + "','" + arrlistPatientInfoData[12] +
        // "','" + arrlistPatientInfoData[13] + "','" + arrlistPatientInfoData[14] + "','" + arrlistPatientInfoData[15] +
        // "','" + arrlistPatientInfoData[16] + "','" + arrlistPatientInfoData[17] + "','" + arrlistPatientInfoData[18] +
        // "','" + arrlistPatientInfoData[19] + "','" + arrlistPatientInfoData[20] + "','" + arrlistPatientInfoData[21] +
        // "','" + arrlistPatientInfoData[22] + "','" + arrlistPatientInfoData[23] + "','" + arrlistPatientInfoData[24] +
        // "','" +  arrlistPatientInfoData[25] + "','" + arrlistPatientInfoData[26] + "','" + arrlistPatientInfoData[27] +
        // "','" + arrlistPatientInfoData[28] + "','" + arrlistPatientInfoData[29] + "','" + arrlistPatientInfoData[30] +
        // "','" + arrlistPatientInfoData[31] + "','" + arrlistPatientInfoData[32] + "','" + arrlistPatientInfoData[33] +
        // "','" + arrlistPatientInfoData[34] + "','" + arrlistPatientInfoData[35] + "','" + arrlistPatientInfoData[36] +
        // "','" + arrlistPatientInfoData[37] + "','" + arrlistPatientInfoData[38] + "','" + arrlistPatientInfoData[39] +
        // "','" + arrlistPatientInfoData[40] + "','" + arrlistPatientInfoData[41] + "','" + arrlistPatientInfoData[42] +
        // "','" + arrlistPatientInfoData[43] + "','" + arrlistPatientInfoData[44] + "','" + arrlistPatientInfoData[45] +
        // "','" + arrlistPatientInfoData[46] + "','" + arrlistPatientInfoData[47] + "','" + arrlistPatientInfoData[48] +
        // "','" + arrlistPatientInfoData[49] + "','" + arrlistPatientInfoData[50] + "','" + arrlistPatientInfoData[51] +
        // "','" + arrlistPatientInfoData[52] + "','" + arrlistPatientInfoData[53] + "','" + arrlistPatientInfoData[54] +
        // "','" + arrlistPatientInfoData[55] + "','" + arrlistPatientInfoData[56] + "','" + arrlistPatientInfoData[57] +
        // "','" + arrlistPatientInfoData[58] + "','" + arrlistPatientInfoData[59] + "','" + arrlistPatientInfoData[60] +
        // "','" + arrlistPatientInfoData[61] + "','" + arrlistPatientInfoData[62] + "','" + arrlistPatientInfoData[63] +
        // "','" + arrlistPatientInfoData[64] + "','" + arrlistPatientInfoData[65] + "','" + arrlistPatientInfoData[66] +
        // "','" + arrlistPatientInfoData[67] + "','" + arrlistPatientInfoData[68] + "','" + arrlistPatientInfoData[69] + "')";
  
        //        odCommand.CommandText = strSQL;
        //        odCommand.ExecuteNonQuery();

        //    }
        //    finally
        //    {
        //        odCommand.Dispose();
        //        odcConnection.Dispose();
        //    }



        //}

        public static void InsertTestData(string mdbPath, string tableName, ArrayList arrlistPatientInfoData)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;

            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                strSQL = "insert into " + tableName + " values ('";
                for (int i = 0; i < arrlistPatientInfoData.Count; i++)
                {
                    strSQL = strSQL + arrlistPatientInfoData[i] + "','";
                }

                strSQL = strSQL.Substring(0, strSQL.Length - 3) + "')";

                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void InsertTestData(string mdbPath, string tableName, double[] DataArray)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;

            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                strSQL = "insert into " + tableName + " values ('";
                for (int i = 0; i < DataArray.Length; i++)
                {
                    strSQL = strSQL + DataArray[i] + "','";
                }

                strSQL = strSQL.Substring(0, strSQL.Length - 3) + "')";

                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataDataBase(string mdbPath, string tableName, CheckBox[] arrCheck,ComboBox[] arrCombox1,ComboBox[] arrCombox2)
        {
            int intRowCount;

            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                intRowCount = arrCheck.Length;
                for (int i = 49; i < (intRowCount + 49); i++)
                {
                    // 输入查询语句
                    strSQL = "update " + tableName + " set 选择状态='" + Convert.ToInt32(arrCheck[i - 49].Checked) + "',自定义名称='" + (string)arrCombox1[i - 49].Text + "',选择工位='" + (string)arrCombox2[i - 49].Text + "' where 编号= " + i.ToString();
                    //strSQL = "update PlotSta0 set 选择状态='1 ',自定义名称='温度103 ' where 编号= 9";
                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();
                }


            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataMpData(string mdbPath, string tableName, string[] ReportHead, string[] Data)
        {
            int intRowCount;

            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                intRowCount = ReportHead.Length;
                for (int i = 0; i < intRowCount; i++)
                {
                    // 输入查询语句
                    strSQL = "update " + tableName + " set [Item]='" + ReportHead[i] + "',[Data]='" + Data[i] + "' where [No]= " + (i + 1).ToString();
                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static int DataBaseExecute(string mdbPath, string ExecuteSQL)
        {
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                odCommand.CommandText = ExecuteSQL;
                odCommand.ExecuteNonQuery();
                return 0;
            }
            catch(Exception)
            {
                return -1;
                
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

        }

        public static void UpdataDataBase(string mdbPath, string tableName, CheckBox[] arrCheck)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                for (int i = 49; i < arrCheck.Length; i++)
                {
                    // 输入查询语句
                    strSQL = "update " + tableName + " set 选择状态='" + Convert.ToInt32(arrCheck[i].Checked) + "',自定义名称='" + (string)arrCheck[i].Text + "' where 编号= " + i.ToString();
                    //strSQL = "update PlotSta0 set 选择状态='1 ',自定义名称='温度103 ' where 编号= 9";
                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();
                }


            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataCustom(string mdbPath, string tableName, string SQL,ref bool success)
        {
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                odCommand.CommandText = SQL;
                odCommand.ExecuteNonQuery();
                success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }
        }

        public static void DeleteTableColumn(string mdbPath, string tableName, string columnName)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // 输入查询语句
                strSQL = "alter table " + tableName + " drop column " + columnName;
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void Insert2Ddata(string mdbPath, string tableName, string[,] Data)
        {
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // 删除原来数据
                odCommand.CommandText = "delete * from " + tableName;
                odCommand.ExecuteNonQuery();

                for (int i = 0; i < Data.GetLength(0); i++)
                {
                    odCommand.CommandText = "insert into " + tableName + " values ('" + Data[i, 0] + "','"
                                                                                      + Data[i, 1] + "','"
                                                                                      + Data[i, 2] + "','"
                                                                                      + Data[i, 3] + "','"
                                                                                      + Data[i, 4] + "','"
                                                                                      + Data[i, 5] + "','"
                                                                                      + Data[i, 6] + "','"
                                                                                      + Data[i, 7] + "','"
                                                                                      + Data[i, 8] + "','"
                                                                                      + Data[i, 9] + "','"
                                                                                      + Data[i, 10] + "')";

                    odCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void AddTableColumn(string mdbPath, string tableName, string columnName)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // 输入查询语句
                strSQL = "alter table " + tableName + " add " + columnName + " double default 0";
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataColor(string mdbPath, string tableName,int intColor, int RowPos)
        {

            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // 输入查询语句
                strSQL = "update " + tableName + " set 颜色='" + intColor + "' where 编号= " + RowPos.ToString();
                //strSQL = "update PlotSta0 set 选择状态='1 ',自定义名称='温度103 ' where 编号= 9";
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

        }

        public static void UpdataCurveVisible(string mdbPath, string tableName, bool visible, int RowPos)
        {

            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // 输入查询语句
                strSQL = "update " + tableName + " set 隐现状态='" + Convert.ToInt32(visible) + "' where 编号= " + RowPos.ToString();
                //strSQL = "update PlotSta0 set 选择状态='1 ',自定义名称='温度103 ' where 编号= 9";
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

        }

        public static void UpdataInterval(string mdbPath, string tableName, int Data)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {
                // string tt = null;
                // 输入查询语句
                //strSQL = "insert into " + tableName + " values ('" + Data + "')";

                strSQL = "update " + tableName + " set SampleInterval='" + Data + "'";

                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdateParameters(string mdbPath, string tableName, List<string> Name,List<double> Value)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {

                for (int i = 0; i < Name.Count;i++ )
                {
                    strSQL = "update [" + tableName + "] set [" + Name[i] + "]=" + Value[i];

                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void InsertParameters(string mdbPath, string tableName, List<double> Value)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {

                strSQL = "insert into [" + tableName + "] values ('";
                for (int i = 0; i < Value.Count; i++)
                {
                    strSQL = strSQL + Value[i] + "','";
                }

                strSQL = strSQL.Substring(0, strSQL.Length - 3) + "')";

                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataNoteData_All(string mdbPath, string tableName,string[] NotaData)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {

                for (int i = 0; i < NotaData.Length; i++)
                {
                    strSQL = "update " + tableName + " set 名称='" + NotaData[i] + "' where 编号= " + i;
                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();
                }
                    
            }
            catch
            {

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

        }

        public static void UpdataNoteData_One(string mdbPath, string tableName, string NotaData,int index)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {

                strSQL = "update " + tableName + " set 名称='" + NotaData + "' where 编号= " + index;
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }

        public static void UpdataNoteData_Label(string mdbPath, string tableName, string NotaData, int index)
        {
            string strSQL;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();
            try
            {

                strSQL = "update " + tableName + " set 标签='" + NotaData + "' where 编号= " + index;
                odCommand.CommandText = strSQL;
                odCommand.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }



        }
        //保存曲线数据
        public static void SavePatientData(string mdbPath, string tableName, ArrayList arrlistXYrange, ArrayList arrlistXdata, ArrayList arrlistYdata)
        {
            string strSQL;
            double douXYrange;
            double douXdata;
            double douYdata;
            //1、建立连接 
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);
            //2、打开连接 
            odcConnection.Open();
            //建立SQL查询 
            OleDbCommand odCommand = odcConnection.CreateCommand();

            strSQL = "delete * from " + tableName;
            odCommand.CommandText = strSQL;
            odCommand.ExecuteNonQuery();
            try
            {
                for (int i = 0; i < arrlistYdata.Count; i++)
                {
                    douXdata = (double)arrlistXdata[i];
                    douYdata = (double)arrlistYdata[i];
                    try
                    {
                        douXYrange = (double)arrlistXYrange[i];
                    }
                    catch
                    {
                        douXYrange = 0;
                    }

                    strSQL = "insert into " + tableName + " values ('" + douXYrange + "','" + douXdata + "','" + douYdata + "')";
                    odCommand.CommandText = strSQL;
                    odCommand.ExecuteNonQuery();

                }
            }
            finally
            {
                odCommand.Dispose();
                odcConnection.Dispose();
            }

        }

        public static void SaveAsData(string fileName)
        {
            ArrayList dataMsg = new ArrayList();
            ArrayList Xarray = new ArrayList();
            ArrayList Yarray = new ArrayList();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader Br = new BinaryReader(fs);
            FileStream fs1 = new FileStream(System.IO.Path.ChangeExtension(fileName, "txt"), FileMode.Create, FileAccess.ReadWrite);

            StreamWriter sw = new StreamWriter(fs1);
            dataMsg.Add(Br.ReadInt32());
            dataMsg.Add(Br.ReadDouble());
            dataMsg.Add(Br.ReadDouble());
            dataMsg.Add(Br.ReadDouble());
            dataMsg.Add(Br.ReadDouble());
            int count = Br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                //Xarray.Add(Br.ReadDouble);
                //Yarray.Add(Br.ReadDouble);
                sw.Write(Br.ReadDouble().ToString ("0.0000"));
                sw.Write(" , ");
                sw.Write(Br.ReadDouble().ToString ("0.0000"));
                sw.Write(Environment.NewLine);
            }
         
           
            sw.Flush();
            sw.Close();
            Br.Close();
            fs.Close();
            fs1.Close();

        }

        public static void SaveMesFile(string fileName,string content)
        {
            try
            {
                FileStream fs = new FileStream(System.IO.Path.ChangeExtension(fileName, "txt"), FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);

                sw.Flush();
                sw.Close();
                fs.Close();
                //fs1.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public static void SaveAsData(string fileName,ArrayList dataArray)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(dataArray.Count);
            for (int i = 0; i < dataArray.Count; i++)
            {
                bw.Write((double)dataArray[i]);
            }
            bw.Flush();
            bw.Close();
            fs.Close();

        }

        public static void OpenData(string fileName,ref ArrayList dataArray)
        {
            dataArray.Clear();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader Br = new BinaryReader(fs);
            int count = Br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                dataArray.Add(Br.ReadDouble());
            }
            Br.Close();
            fs.Close();

        }


        public static void OpenDataToNote(string fileName)
        {

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader Br = new BinaryReader(fs);
            FileStream fs1 = new FileStream("temp.txt", FileMode.Create, FileAccess.ReadWrite);

            StreamWriter sw = new StreamWriter(fs1);
            int mode = Br.ReadInt32();
            double XAxisMin = Br.ReadDouble();
            double XAxisMax = Br.ReadDouble();
            double YAxisMin = Br.ReadDouble();
            double YAxisMax = Br.ReadDouble();
            int count = Br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                sw.Write(Br.ReadDouble().ToString("0.0000"));
                sw.Write(" , ");
                sw.Write(Br.ReadDouble().ToString("0.0000"));
                sw.Write(Environment.NewLine);

            }
            System.Diagnostics.Process.Start("notepad.exe ", "temp.txt ");
            sw.Flush();
            sw.Close();
            Br.Close();
            fs1.Close();
            fs.Close();
        }

        public static void OpenDataToNote(ArrayList XdataArray,ArrayList YdataArray)
        {
            FileStream fs1 = new FileStream("temp.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs1);
            int count = XdataArray.Count;
            for (int i = 0; i < count; i++)
            {
                sw.Write(string.Format("{0:0.0000}", (double)XdataArray[i]));
                sw.Write(" , ");
                sw.Write(string.Format("{0:0.0000}", (double)YdataArray[i]));
                sw.Write(Environment.NewLine);

            }
            System.Diagnostics.Process.Start("notepad.exe ", "temp.txt ");
            sw.Flush();
            sw.Close();
            fs1.Close();
      
        }

        //加载Excel 
        public static DataSet LoadDataFromExcel(string filePath)
        {
            try
            {
                string strConn;
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
                OleDbConnection OleConn = new OleDbConnection(strConn);
                OleConn.Open();
                String sql = "SELECT * FROM  [Sheet1$]";//可是更改Sheet名称，比如sheet2，等等 

                OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                DataSet OleDsExcle = new DataSet();
                OleDaExcel.Fill(OleDsExcle, "Sheet1");
                OleConn.Close();
                return OleDsExcle;
            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        //public static bool SaveDataTableToExcel(System.Data.DataTable excelTable, string filePath)
        //{
        //    Microsoft.Office.Interop.Excel.Application app =
        //        new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    try
        //    {
        //        app.Visible = false;
        //        Microsoft.Office.Interop.Excel.Workbook wBook = app.Workbooks.Add(true);
        //        Microsoft.Office.Interop.Excel.Worksheet wSheet = wBook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        //        if (excelTable.Rows.Count > 0)
        //        {
        //            int row = 0;
        //            row = excelTable.Rows.Count;
        //            int col = excelTable.Columns.Count;
        //            for (int i = 0; i < row; i++)
        //            {
        //                for (int j = 0; j < col; j++)
        //                {
        //                    string str = excelTable.Rows[i][j].ToString();
        //                    wSheet.Cells[i + 2, j + 1] = str;
        //                }
        //            }
        //        }

        //        int size = excelTable.Columns.Count;
        //        for (int i = 0; i < size; i++)
        //        {
        //            wSheet.Cells[1, 1 + i] = excelTable.Columns[i].ColumnName;
        //        }
        //        //设置禁止弹出保存和覆盖的询问提示框 
        //        app.DisplayAlerts = false;
        //        app.AlertBeforeOverwriting = false;
        //        //保存工作簿 
        //        wBook.Save();
        //        //保存excel文件 
        //        app.Save(filePath);
        //        app.SaveWorkspace(filePath);
        //        app.Quit();
        //        app = null;
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show("导出Excel出错！错误原因：" + err.Message, "提示信息",
        //            MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //    finally
        //    {
        //    }
        //}

        //public static void OpenExcel(double[] Xdata, double[] Ydata)
        //{
        //    int intRowCount = Xdata.Length;
        //    int intColumsCount = 2;
        //    Microsoft.Office.Interop.Excel.Application MyExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    Microsoft.Office.Interop.Excel.Workbook wBook = MyExcel.Workbooks.Add(true);
        //    Microsoft.Office.Interop.Excel.Worksheet wSheet = wBook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        //    double[,] DataArray = new double[intRowCount, intColumsCount];
        //    for (int i = 0; i < Xdata.Length; i++)
        //    {
        //        DataArray[i, 0] = Xdata[i];
        //        DataArray[i, 1] = Ydata[i];
        //    }
        //    wSheet.get_Range("A1", wSheet.Cells[intRowCount, intColumsCount]).Value2 = DataArray;
        //    MyExcel.Visible = true;
        //    MyExcel = null;
        //}

        //public static void OpenDataBaseToExcel(string mdbPath)
        //{
        //    try
        //    {
        //        Microsoft.Office.Interop.Excel.Application MyExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //        Microsoft.Office.Interop.Excel.Workbook wBook = MyExcel.Workbooks.OpenDatabase(mdbPath, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
        //        MyExcel.Visible = true;
        //        MyExcel = null;
        //    }
        //    catch
        //    {
        //    }


        //}

        //public static void OpenDataToExcel(string ExcelPath,string strReportHead,object[] FildName,object[,] DataArray,object[,] DataArray1)
        //{
        //    Microsoft.Office.Interop.Excel.Application MyExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    Microsoft.Office.Interop.Excel.Workbook wBook = MyExcel.Workbooks.Open(ExcelPath, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
        //    Microsoft.Office.Interop.Excel.Worksheet wSheet0 = wBook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        //    Microsoft.Office.Interop.Excel.Worksheet wSheet1 = wBook.Worksheets[2] as Microsoft.Office.Interop.Excel.Worksheet;
        //    wSheet0.Cells[2, 0] = strReportHead;
        //    wSheet1.Cells[2, 0] = strReportHead;
        //    wSheet0.get_Range("A3", wSheet0.Cells[3, FildName.Length]).Value2 = FildName;
        //    wSheet1.get_Range("A3", wSheet1.Cells[3, FildName.Length]).Value2 = FildName;
        //    wSheet0.get_Range("A4", wSheet0.Cells[DataArray.GetLength(0) + 4, DataArray.GetLength(1)]).Value2 = DataArray;
        //    wSheet1.get_Range("A4", wSheet1.Cells[DataArray1.GetLength(0) + 4, DataArray1.GetLength(1)]).Value2 = DataArray1;
        //    MyExcel.Save(ExcelPath);
        //    MyExcel.Visible = true;
        //    MyExcel = null;

        //}

        public static bool SetPreDataSQL(string strTableName,string strFileName,ref string strPreSQL)
        {
            DataTable dt = new DataTable();
            try
            {
                //1、建立连接 
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strFileName;
                OleDbConnection odcConnection = new OleDbConnection(strConn);
                //2、打开连接 
                odcConnection.Open();
                //建立SQL查询 
                string strSQL = "select * from " + strTableName;
                OleDbDataAdapter oleDa = new OleDbDataAdapter(strSQL, odcConnection);
                oleDa.Fill(dt);

                strPreSQL = "测试时间,开停状态,";

                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (k < 8)
                    {
                        strPreSQL = strPreSQL + dt.Rows[k][1].ToString() + ",";
                    }
                    else
                    {
                        if ((bool)dt.Rows[k][3] && dt.Rows[k][1].ToString() == dt.Rows[k][5].ToString())
                        {
                            strPreSQL = strPreSQL + dt.Rows[k][1].ToString() + ",";
                        }
                        else if ((bool)dt.Rows[k][3] && dt.Rows[k][1].ToString() != dt.Rows[k][5].ToString())
                        {
                            strPreSQL = strPreSQL + dt.Rows[k][1].ToString() + " as " + dt.Rows[k][5].ToString() + ",";
                        }

                    }

                }

                strPreSQL = strPreSQL.Substring(0, strPreSQL.Length - 1);
                //关闭连接 
                oleDa.Dispose();
                odcConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static void OpenDataToExcel(string ExcelPath, string strReportHead, DataTable dtProcessData, DataTable dtOnOffData)
        //{
        //    Microsoft.Office.Interop.Excel.Application MyExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    try
        //    {
        //        Microsoft.Office.Interop.Excel.Workbook wBook = MyExcel.Workbooks.Open(ExcelPath, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
        //        Microsoft.Office.Interop.Excel.Worksheet wSheet0 = wBook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        //        Microsoft.Office.Interop.Excel.Worksheet wSheet1 = wBook.Worksheets[2] as Microsoft.Office.Interop.Excel.Worksheet;
        //        wSheet0.Cells[2, 1] = strReportHead;
        //        wSheet1.Cells[2, 1] = strReportHead;
        //        for (int i = 0; i < dtProcessData.Columns.Count; i++)
        //        {
        //            wSheet0.Cells[3, i + 1] = dtProcessData.Columns[i].ColumnName;
        //            wSheet1.Cells[3, i + 1] = dtProcessData.Columns[i].ColumnName;
        //        }
        //        object[,] objProessData = new object[dtProcessData.Rows.Count, dtProcessData.Columns.Count];
        //        for (int i = 0; i < dtProcessData.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < dtProcessData.Columns.Count; k++)
        //            {
        //                objProessData[i, k] = dtProcessData.Rows[i][k];
        //            }
        //        }

        //        object[,] objOnOffData = new object[dtOnOffData.Rows.Count, dtOnOffData.Columns.Count];
        //        for (int i = 0; i < dtOnOffData.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < dtOnOffData.Columns.Count; k++)
        //            {
        //                objOnOffData[i, k] = dtOnOffData.Rows[i][k];
        //            }
        //        }

        //        wSheet0.get_Range("A4", wSheet0.Cells[dtProcessData.Rows.Count + 3, dtProcessData.Columns.Count]).Value2 = objProessData;
        //        wSheet1.get_Range("A4", wSheet1.Cells[dtOnOffData.Rows.Count + 3, dtOnOffData.Columns.Count]).Value2 = objOnOffData;

        //        wBook.Save();
        //        MyExcel.Visible = true;
        //        wBook = null;
        //        MyExcel = null;
               
             
        //        //KillExcel(MyExcel);

        //    }
        //    catch
        //    {
        //        MyExcel = null;
        //        MessageBox.Show("数据导出Excel表格失败，请联系供应商！");
        //    }
           

        //}

        ///*******************************Excel report******************************/
        //public static void SaveDataReportToExcel(string strFileName, string[] NameplateHead,string[] NamePlateData,ConditionSetup conditionSetup,string NozzleData,string[,] MainReportData,string[,] AppendReportData)
        //{
        //    Microsoft.Office.Interop.Excel.Application MyExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    try
        //    {
        //        Microsoft.Office.Interop.Excel.Workbook wBook = MyExcel.Workbooks.Open(strFileName, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
        //        Microsoft.Office.Interop.Excel.Worksheet wSheet0 = wBook.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
        //        Microsoft.Office.Interop.Excel.Worksheet wSheet1 = wBook.Worksheets[2] as Microsoft.Office.Interop.Excel.Worksheet;
        //        //铭牌
        //        for (int i = 3; i < 8; i++)
        //        {
        //            wSheet0.Cells[i, 1] = NameplateHead[i - 3] + NamePlateData[i - 3];
        //            wSheet1.Cells[i, 1] = NameplateHead[i - 3] + NamePlateData[i - 3];
        //        }

        //        for (int i = 3; i < 8; i++)
        //        {
        //            wSheet0.Cells[i, 3] = NameplateHead[i + 2] + NamePlateData[i + 2];
        //            wSheet1.Cells[i, 3] = NameplateHead[i + 2] + NamePlateData[i + 2];
        //        }

        //        for (int i = 3; i < 7; i++)
        //        {
        //            wSheet0.Cells[i, 7] = NameplateHead[i + 7] + NamePlateData[i + 7];
        //            wSheet1.Cells[i, 7] = NameplateHead[i + 7] + NamePlateData[i + 7];
        //        }
        //        //工况
        //        wSheet0.Cells[4, 11]=conditionSetup.OutdoorDB;
        //        wSheet0.Cells[5, 11]=conditionSetup.OutdoorWB;
        //        wSheet0.Cells[6, 11]=conditionSetup.IndoorDB;
        //        wSheet0.Cells[7, 11]=conditionSetup.IndoorWB;
        //        wSheet1.Cells[4, 11]=conditionSetup.OutdoorDB;
        //        wSheet1.Cells[5, 11]=conditionSetup.OutdoorWB;
        //        wSheet1.Cells[6, 11]=conditionSetup.IndoorDB;
        //        wSheet1.Cells[7, 11]=conditionSetup.IndoorWB;

        //        wSheet0.Cells[55, 1]=NozzleData;
        //        wSheet1.Cells[52, 1]=NozzleData;

        //        wSheet0.get_Range("A10", "K54").Value2 = MainReportData;
        //        wSheet1.get_Range("A4", "K49").Value2 = AppendReportData;

        //        wBook.Save();
        //        MyExcel.Visible = true;
        //        wBook = null;
        //        MyExcel = null;


        //        //KillExcel(MyExcel);

        //    }
        //    catch
        //    {
        //        MyExcel = null;
        //        MessageBox.Show("数据导出Excel表格失败，请联系供应商！","System message",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //    }
        //}
        /************************************************************8************/
        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        //public static void KillExcel(Microsoft.Office.Interop.Excel.Application excel)
        //{
        //    IntPtr t = new IntPtr(excel.Hwnd); //得到这个句柄，具体作用是得到这块内存入口

        //    int k = 0;
        //    GetWindowThreadProcessId(t, out k); //得到本进程唯一标志k
        //    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k); //得到对进程k的引用
        //    p.Kill(); //关闭进程k
        //}

        public void AccessGuideJoinExcel(string Access, string AccTable, string Excel)
        {
            try
            {
                string tem_sql = "";//定义字符串
                string connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Access + ";Persist Security Info=True";//记录连接Access的语句
                OleDbConnection tem_conn = new OleDbConnection(connstr);//连接Access数据库
                OleDbCommand tem_comm;//定义OleDbCommand类
                tem_conn.Open();//打开连接的Access数据库
                tem_sql = "select Count(*) From " + AccTable;//设置SQL语句，获取记录个数
                tem_comm = new OleDbCommand(tem_sql, tem_conn);//实例化OleDbCommand类
                int RecordCount = (int)tem_comm.ExecuteScalar();//执行SQL语句，并返回结果
                //每个Sheet只能最多保存65536条记录。
                tem_sql = @"select top 65535 * into [Excel 8.0;database=" + Excel + @".xls].[Sheet1] from " + AccTable;//  from后面一定要有空格，记录连接Excel的语句
                tem_comm = new OleDbCommand(tem_sql, tem_conn);//实例化OleDbCommand类
                tem_comm.ExecuteNonQuery();//执行SQL语句，将数据表的内容导入到Excel中
                tem_conn.Close();//关闭连接
                tem_conn.Dispose();//释放资源
                tem_conn = null;
                MessageBox.Show("导出完成");
            }
            catch
            {
                MessageBox.Show("当前已有Sheet1页");
            }
        }
        #endregion
    }
}
