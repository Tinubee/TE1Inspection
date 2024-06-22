using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public partial class 검사결과
    {
        public void 큐알정보등록(큐알내용 큐알)
        {
            //this.큐알내용 = 큐알.큐알;
            if (this.큐알내용 != 큐알.큐알) this.큐알등급 = 큐알등급.X;
            else this.큐알등급 = 큐알.등급;
            this.SetResult(검사항목.QrLegibility, (Int32)this.큐알등급);
        }

        public String 큐알각인내용()
        {
            this.큐알내용 = 큐알각인내용(this.검사일시, this.모델구분, this.검사번호);
            return this.큐알내용;
        }

        public const String 큐알정보구분자 = ";";
        public static String 큐알각인내용(DateTime 날짜, 모델구분 모델, Int32 검사코드)
        {
            List<String> 인쇄항목 = new List<String>();
            인쇄항목.Add(Utils.FormatDate(날짜, "{0:yyMMdd}"));
            인쇄항목.Add(((Int32)모델).ToString("d2"));
            인쇄항목.Add(검사코드.ToString("d4"));
            인쇄항목.Add(Utils.GetDescription(모델));
            //인쇄항목.Add(Global.환경설정.회사코드);
            //인쇄항목.Add(Global.환경설정.라인번호);
            //인쇄항목.Add(Global.환경설정.TP_NTP);
            //인쇄항목.Add(Global.환경설정.RevisionNo);
            String 내용 = String.Join(큐알정보구분자, 인쇄항목.ToArray());
            Debug.WriteLine($"길이={내용.Length} => {내용}");
            return 내용;
            //if (내용.Length > 35) return 내용.Substring(0, 35);
            //return 내용;
        }
    }
}
