using Sygole.HFReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acura3._0.Classes
{
   public class SyGoleRFID
    {
        HFReader reader = new HFReader();
        Sygole.HFReader.EventHandler<CommEventArgs> _CommReceiveHandler = null;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool connect ;

        public void ReceiveHandler()
        {
            reader.CommEvent = new CommEventCS();
            _CommReceiveHandler = new Sygole.HFReader.EventHandler<CommEventArgs>(CommEvent_CommReceiveHandler);
            //订阅事件
            reader.CommEvent.CommReceiveHandler += _CommReceiveHandler;
        }

        void CommEvent_CommReceiveHandler(object sender, CommEventArgs Args)
        {
            SetResult(ByteToHexString(Args.CommDatas, 0, Args.CommDatasLen, " "));
        }

        public string SetResult(string result)
        {
            if (result.Substring(15, 2) == "00")
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool RFID_Connect(string ip,ushort port,string id)
        {
            if (connect)
            {
                return true;
            }
            if (reader.Connect(ip, port, byte.Parse(id))) //尝试用用户选定的波特率进行连接)
            {
                return connect = true;
            }
            return false;
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void RFID_DisConnect()
        {
            connect = false;
            reader.DisConnect();
        }

        /// <summary>
        /// 获取UID
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public string RFID_ReadUID(string id)
        {
            Status_enum status = Status_enum.FAILURE;
            byte[] uid = new byte[8];
            byte len = 0;
            status = reader.Inventory(byte.Parse(id), ref uid, ref len);
            if (status == Status_enum.SUCCESS)
            {
                return ByteToHexString(uid, 0, len, " ");
            }
            else
            {
                return "Fail";
            }
        }


        /// <summary>
        /// 读取数据转成文字
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="space">默认“ ”</param>
        /// <param name="pos">起始地址（默认0）</param>
        /// <param name="len">读取的字节数（默认16）</param>
        /// <returns></returns>
        public string RFID_ReadDataTostring(string id,string space=" ", string pos="0", string len="16")
        {
            Status_enum status = Status_enum.FAILURE;
            byte[] datas_r = new byte[16];
            status = reader.ReadBytes(byte.Parse(id), ushort.Parse(pos), 16, ref datas_r);
            if (status == Status_enum.SUCCESS)
            {
                return HexToString(ByteToHexString(datas_r, int.Parse(pos), int.Parse(len), space));
            }
            else
            {
                return "Fail";
            }
        }


        /// <summary>
        /// 读取数据转成文字
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="space">默认“ ”</param>
        /// <param name="pos">起始地址（默认0）</param>
        /// <param name="len">读取的字节数（默认16）</param>
        /// <returns></returns>
        public string RFID_ReadDataString(string id, string space = " ", string pos = "0", string len = "16")
        {
            Status_enum status = Status_enum.FAILURE;
            String datas_r = "";
            status = reader.ReadBytes(byte.Parse(id), ushort.Parse(pos), byte.Parse(len), ref datas_r);
            if (status == Status_enum.SUCCESS)
            {
                return datas_r;
            }
            else
            {
                return "Fail";
            }
        }

        /// <summary>
        ///  写入RFID
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="data">字符</param>
        /// <param name="pos">起始地址（默认0）</param>
        /// <param name="len">读取字节数（默认16）</param>
        /// <returns></returns>

        public bool RFID_Write(string id, string data, string pos="0", string len="16")
        {
            Status_enum status = Status_enum.FAILURE;
            status = reader.WriteBytes(byte.Parse(id), ushort.Parse( pos), byte.Parse(len), data);
            if (status == Status_enum.SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RFID_Clear(string id)
        {
            Status_enum status = Status_enum.FAILURE;
            byte[] datas_w = new byte[250];
            int write_cnt = 0;
            for (int i = 0; i < datas_w.Length; i++)
            {
                datas_w[i] = (byte)(write_cnt);
            }
            status = reader.WriteBytes(byte.Parse(id), 0, 250, datas_w);
            if (status == Status_enum.SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 字符串转16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string StringToHex(string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data);
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)
            {
                result += Convert.ToString(b[i], 16).ToUpper();
                
            }
            return result;
        }

        /// <summary>
        /// 16进制转字符串
        /// </summary>
        /// <param name="Hexdata"></param>
        /// <returns></returns>
        public string HexToString(string Hexdata)
        {
            string result = string.Empty;
            string hex= Hexdata.Substring(0, 2);
            hex += Hexdata.Substring(3, 2);
            byte[] arrByte = new byte[hex.Length/2];
            int index = 0;
            for (int i = 0; i < hex.Length; i+=2)
            {
                arrByte[index++] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            result = System.Text.Encoding.UTF8.GetString(arrByte);
            return result;
        }



        #region 类型转换
        public static string ByteToHexString(byte[] data, int pos, int length, string space)
        {
            string outString = "";

            for (int i = pos; i < pos + length; i++)
            {
                outString += ByteToHexString(data[i]);
                if (i != pos + length - 1)
                {
                    outString += space;
                }
            }

            return outString;
        }

        public static string ByteToHexString(byte data)
        {
            string outString = "";

            if (data < 16)
            {
                outString += "0";
            }
            outString += data.ToString("X");

            return outString;
        }
        #endregion

    }
}
