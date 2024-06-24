using MvUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public partial class 검사결과
    {
        public void 큐알정보등록(큐알내용 큐알)
        {
            Debug.WriteLine($"큐알정보등록 : {this.큐알등급}");
            //this.큐알내용 = 큐알.큐알;
            if (this.큐알내용 != 큐알.큐알) this.큐알등급 = 큐알등급.X;
            else this.큐알등급 = 큐알.등급;
            this.SetResult(검사항목.QrLegibility, (Int32)this.큐알등급);
            this.SetResult(검사항목.Imprinted, 1);
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

        public String 라벨출력내용(String[] 라벨내용)
        {
            String 전송내용 = 라벨출력내용(this.검사일시, this.모델구분, this.검사번호, 라벨내용);
            return 전송내용;
        }
        private const Int32 레이아웃 = 1;
        private const Int32 출력장수 = 1;
        private const String 날짜포맷 = "{0:MMdd}";
        public static String 라벨출력내용(DateTime 날짜, 모델구분 모델, Int32 검사코드, String[] 라벨내용)
        {
            Char[] keys = { 'H', 'T', 'M', 'B', 'F', 'S', 'Q' };
            Char LF = Convert.ToChar(10);
            Char ETB = Convert.ToChar(23);
            //String 자료 = $"041C1E{레이아웃}Q{출력장수}{ETB}D{Utils.FormatDate(날짜, 날짜포맷)}{Utils.GetDescription(모델)}D{번호:D4}{LF}T1{LF} {LF} {LF} {LF} {LF} {LF} ??";

            //1: H , 2:T , 3:B , 4:M , 5:F , 6:S , 7:Q
            Dictionary<Char, String> grouped = 라벨내용
             .GroupBy(item => item[0]) // Group by the first letter
             .ToDictionary(g => g.Key, g => string.Concat(g)); // Convert to dictionary

            // Ensure all keys are present, use String.Empty if not
            String[] result = keys
                .Select(key => grouped.ContainsKey(key) ? grouped[key] : " ")
                .ToArray();

            Debug.WriteLine($"{String.Join(",", result)}");

            String 추가항목 = String.Join(LF.ToString(), result);

            //String 내용 = $"041C1E{레이아웃}Q{출력장수}{ETB}D{Utils.GetDescription(모델)}{Utils.FormatDate(날짜, 날짜포맷)}D{검사코드:D4}{LF}{추가항목}??";
            List<String> 인쇄항목 = new List<String>();
            인쇄항목.Add($"041C1E{레이아웃}Q{출력장수}{ETB}D{Utils.GetDescription(모델)}{Utils.FormatDate(날짜, 날짜포맷)}D{검사코드:D4}{LF}");
            인쇄항목.Add(추가항목);
            인쇄항목.Add("??");
            String 내용 = String.Join(String.Empty, 인쇄항목.ToArray());
            Debug.WriteLine($"라벨출력내용 => {내용}");
            return 내용;
        }
    }
}
