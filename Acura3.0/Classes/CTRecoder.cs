using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acura3._0.Classes
{
    public class CTRecoder
    {
        public List<RecordData> Record = new List<RecordData>();
        public struct RecordData
        {
            public string ActionName;
            public Int64 ActionTime;
        }

        private GetTickCountEx tick = new GetTickCountEx();
        private Int64 BaseTM;
        private bool IsPause = false;
        private Int64 ToatalPauseTM;
        private Int64 TotalStartTM;

        public CTRecoder()
        {
            Reset();
        }

        public void Reset()
        {
            ToatalPauseTM = 0;
            TotalStartTM = 0;
            BaseTM = tick.Value;
            Record.Clear();
            IsPause = true;
        }

        public void Pause()
        {
            if (!IsPause)
            {
                long Now = tick.Value;
                TotalStartTM += (Now - BaseTM);
                BaseTM = Now;
                IsPause = true;
            }
        }

        public void Start()
        {
            if (IsPause)
            {
                long Now = tick.Value;
                ToatalPauseTM += (Now - BaseTM);
                BaseTM = Now;
                IsPause = false;
            }
        }

        public Int64 GetCurrentTime()
        {
            if (IsPause)
                return TotalStartTM;
            else
                return TotalStartTM + (tick.Value - BaseTM);
        }

        public void AddRecode(string ActionName)
        {
            RecordData rd = new RecordData();
            rd.ActionName = ActionName;
            rd.ActionTime = GetCurrentTime();
            Record.Add(rd);
        }

        public string[] GetRecord()
        { 
            string[] RecordArray = new string[Record.Count];
            for (int i = 0; i < Record.Count; i++)
            {
                Int64 ActionGapTime = (i == 0) ? Record[i].ActionTime : Record[i].ActionTime - Record[i - 1].ActionTime;
                RecordArray[i] = string.Format("{0},{1},{2}", Record[i].ActionName, ((double)ActionGapTime / 1000).ToString(), ((double)Record[i].ActionTime / 1000).ToString());
            }
            return RecordArray;
        }
    }
}
