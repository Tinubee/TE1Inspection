using ProtoBuf;
using System;
using System.Collections.Generic;

namespace TE1.Schemas
{
    public enum Datums { F01 = 3, F02 = 1, F03 = 2, F04 = 4, F05 = 5, F06 = 7, F07 = 8, F08 = 6, }
    [ProtoContract]
    public class 센서자료 : List<검사정보>
    {
        public Boolean 결과추가(검사결과 검사, String data)
        {
            if (검사 == null || String.IsNullOrEmpty(data) || !data.StartsWith("M0")) return false;
            //Debug.WriteLine(data);
            String[] cols = data.Split(',');
            if (cols.Length != 9) return false;

            foreach (Datums datum in typeof(Datums).GetEnumValues())
            {
                Int32 idx = (Int32)datum;
                검사항목 item = GetItem(datum);
                Double value = 수치변환(cols[idx]);
                if (item == 검사항목.None) continue;
                검사정보 정보 = 검사.GetItem(item);
                if (정보 == null) continue;
                검사.SetResult(정보, value);
                this.Add(정보);
            }
            return true;
        }
        private Double 수치변환(String str)
        {
            if (str.StartsWith("+")) return Convert.ToDouble(str.Substring(1)) / 1000;
            else return Convert.ToDouble(str) / 1000;
        }

        public static 검사항목 GetItem(Datums datum)
        {
            String name = datum.ToString();
            if (Enum.TryParse<검사항목>(name, true, out 검사항목 항목)) return 항목;
            return 검사항목.None;
        }
    }
}
