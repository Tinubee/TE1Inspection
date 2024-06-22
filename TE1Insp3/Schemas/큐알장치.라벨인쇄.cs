﻿using MvUtils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public class 라벨인쇄 : 큐알장치
    {
        public enum 제어명령
        {
            [Description("None")]
            명령없음,
            [Description("041")]
            자료전송,
            [Description("041C1")]
            자료삭제,
            [Description("002")]
            장치리셋,
            [Description("000")]
            장치상태,
        }

        public override String 로그영역 => "Label Printer";
        public override String Host { get => Global.환경설정.라벨인쇄주소; set => Global.환경설정.라벨인쇄주소 = value; }
        public override Int32  Port { get => Global.환경설정.라벨인쇄포트; set => Global.환경설정.라벨인쇄포트 = value; }
        public override String STX => Convert.ToChar(2).ToString();
        public override Char ETX => Convert.ToChar(13);
        private Char LF => Convert.ToChar(10);
        private Char ETB => Convert.ToChar(23);
        private const Int32 레이아웃 = 1;
        private const Int32 출력장수 = 1;
        private const String 날짜포맷 = "{0:MMdd}";

        public delegate void Communication(통신구분 통신, 제어명령 명령, String mesg);
        public event Communication 송신수신알림;

        private Boolean 명령전송(제어명령 명령, String command)
        {
            Debug.WriteLine($"{Convert.ToInt32(this.STX[0])}, {Convert.ToInt32(this.ETX)}, {Convert.ToInt32(this.ETB)}, {Convert.ToInt32(this.LF)}");
            송신수신알림?.Invoke(통신구분.TX, 명령, command.Trim());
            String mesg = this.SendCommand(command, 1000);
            송신수신알림?.Invoke(통신구분.RX, 명령, mesg);
            return true;
        }

        public Boolean 자료전송(DateTime 날짜, 모델구분 모델, Int32 번호)
        {
            //String 자료 = $"041E{레이아웃}Q{출력장수}{ETB}D{Utils.FormatDate(날짜, 날짜포맷)}{LF}{Utils.GetDescription(모델)}{LF}{((Int32)모델).ToString("d2")}{번호.ToString("d4")}??";
            String 자료 = $"041C1E{레이아웃}Q{출력장수}{ETB}D{Utils.FormatDate(날짜, 날짜포맷)}{Utils.GetDescription(모델)}D{번호:D4}{LF}T1{LF} {LF} {LF} {LF} {LF} {LF} ??";

            return this.명령전송(제어명령.자료전송, 자료);
        }

        public Boolean 라벨출력(검사결과 검사) => 자료전송(검사.검사일시, 검사.모델구분, 검사.검사번호);
        public Boolean 라벨부착() { return false; }
        public Boolean 장치리셋() => this.명령전송(제어명령.장치리셋, "002??");
        public Boolean 장치상태() => this.명령전송(제어명령.장치상태, "000??");
    }
}
