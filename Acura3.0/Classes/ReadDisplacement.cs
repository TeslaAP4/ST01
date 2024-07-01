using Acura3._0.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0
{
    public class ReadDisplacement
    {
        public NPSerialProt SPCom = new NPSerialProt();
        public bool ConnectCom(string Com)
        {
            return SPCom.ConnectComPressL(Com, "9600,8,0,1");
        }


        public void DisconnectCom3()
        {
            if (SPCom.ConnectStates())
                SPCom.Dispose();
        }

        /// <summary>
        /// 读取COM8内容
        /// </summary>
        /// <returns></returns>
        public string ReadContent_COM(string str)
        {
            string strContent = "";
            try
            {
                //if (!SPCom.ConnectStates())
                //    ConnectCom();
                bool b1 = SPCom.WriteHEX(str);
                Thread.Sleep(100);
                string strdata = SPCom.readChar(0);
                if (strdata.Length > 0)
                {
                    // 按空格分割字符串
                    string[] splitStr = strdata.Split(' ');
                    //十六进制转10进制  整数位
                    double d1 = Convert.ToInt32((splitStr[3] + splitStr[4]),16);
                    //十六进制转10进制  小数位
                    double d2 = Convert.ToInt32((splitStr[5] + splitStr[6]),16);
                    //最终结果  整数位+小数位
                    strContent = (d1 + d2/65535).ToString("F3");
                }
                return strContent;
            }
            catch (Exception)
            {
                return strContent;
            }
        }
    }
}
