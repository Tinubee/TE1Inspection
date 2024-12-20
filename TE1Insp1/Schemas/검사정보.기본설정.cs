﻿using MvUtils;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TE1.Schemas
{
    public enum 비전파일구분
    {
        [Description("Dimension Left & Right")]
        Cam02 = 2,
        [Description("Dimension Top")]
        Cam03 = 3,
    }
    public enum 카메라구분
    {
        [ListBindable(false)]
        None = 0,
        [Description("Dimension Left")]
        Cam01 = 1,
        [Description("Dimension Right")]
        Cam02 = 2,
        [Description("Dimension Top")]
        Cam03 = 3,
        [Description("Surface Bottom")]
        Cam04 = 4,
        [Description("Surface Right")]
        Cam05 = 5,
        [Description("Surface Left")]
        Cam06 = 6,
    }

    // 카메라구분 과 번호 맞춤
    public enum 장치구분
    {
        [Description("None"), DeviceInfo(Hosts.None, false)]
        None = 0,
        [Description("Cam01"), DeviceInfo(Hosts.Measure, true)]
        Cam01 = 카메라구분.Cam01,
        [Description("Cam02"), DeviceInfo(Hosts.Measure, true)]
        Cam02 = 카메라구분.Cam02,
        [Description("Cam03"), DeviceInfo(Hosts.Measure, true)]
        Cam03 = 카메라구분.Cam03,
        [Description("Cam04"), DeviceInfo(Hosts.Surface, true)]
        Cam04 = 카메라구분.Cam04,
        [Description("Cam05"), DeviceInfo(Hosts.Surface, true)]
        Cam05 = 카메라구분.Cam05,
        [Description("Cam06"), DeviceInfo(Hosts.Surface, true)]
        Cam06 = 카메라구분.Cam06,
        [Description("Cameras"), DeviceInfo(Hosts.Surface, false)]
        Cameras = 10,
        [Description("Flatness"), DeviceInfo(Hosts.Surface, false)]
        Flatness = 11,
        [Description("Thickness"), DeviceInfo(Hosts.Server, false)]
        Thickness = 12,
        [Description("QrReader"), DeviceInfo(Hosts.Server, false)]
        QrReader = 13,
        [Description("QrMarker"), DeviceInfo(Hosts.Server, false)]
        QrMarker = 14,
    }

    public enum 검사그룹
    {
        [Description("None"), Translation("None", "없음")]
        없음 = 0,
        [Description("Dimension"), Translation("Dimension", "치수")]
        치수 = 1,
        [Description("Surface"), Translation("Surface", "외관")]
        표면 = 2,
    }

    public enum 단위구분
    {
        [Description("mm")]
        MM = 0,
        [Description("OK/NG")]
        ON = 1,
        [Description("EA")]
        EA = 2,
        [Description("Grade")]
        GA = 3,
    }

    public enum 큐알등급
    {
        [Description("-")]
        X = 0,
        [Description("A")]
        A = 1,
        [Description("B")]
        B = 2,
        [Description("C")]
        C = 3,
        [Description("D")]
        D = 4,
        [Description("E")]
        E = 5,
        [Description("F")]
        F = 6,
    }

    public enum 결과구분
    {
        [Description("Waiting"), Translation("Waiting", "대기중")]
        WA = 0,
        [Description("PS"), Translation("Pass", "통과")]
        PS = 2,
        [Description("ER"), Translation("Error", "오류")]
        ER = 3,
        [Description("NG"), Translation("NG", "불량")]
        NG = 5,
        [Description("OK"), Translation("OK", "양품")]
        OK = 7,
    }

    [Table("inspl")]
    public partial class 검사결과
    {
        [Column("ilwdt"), Required, Key, JsonProperty("ilwdt"), Translation("Time", "일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("ilmcd"), JsonProperty("ilmcd"), Translation("Model", "모델")]
        public 모델구분 모델구분 { get; set; } = 모델구분.None;
        [Column("ilnum"), JsonProperty("ilnum"), Translation("Index", "번호")]
        public Int32 검사번호 { get; set; } = 0;
        [Column("ilres"), JsonProperty("ilres"), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.WA;
        [Column("ilctq"), JsonProperty("ilctq"), Translation("Dimension", "치수결과")]
        public 결과구분 치수결과 { get; set; } = 결과구분.WA;
        [Column("ilsuf"), JsonProperty("ilapp"), Translation("Suface", "외관결과")]
        public 결과구분 외관결과 { get; set; } = 결과구분.WA;
        [Column("ilqrg"), JsonProperty("ilqrg"), Translation("QR Legibility", "QR등급")]
        public 큐알등급 큐알등급 { get; set; } = 큐알등급.X;
        [Column("ilqrs"), JsonProperty("ilqrs"), Translation("QR Code", "QR코드")]
        public String 큐알내용 { get; set; } = String.Empty;
        [Column("ilngs"), JsonProperty("ilngs"), Translation("NG Items", "불량정보")]
        public String 불량정보 { get; set; } = String.Empty;
        [NotMapped, JsonIgnore]
        public String[] 라벨내용 { get; set; }
        [NotMapped, JsonIgnore]
        public 결과구분 큐알결과 => 큐알등급 >= 큐알등급.A && 큐알등급 <= 큐알등급.C ? 결과구분.OK : 결과구분.NG;

        [NotMapped, JsonIgnore]
        public String 결과문구 => Localization.GetString(측정결과);
        [NotMapped, JsonIgnore]
        public String 품질문구 => Localization.GetString(치수결과);
        [NotMapped, JsonIgnore]
        public String 외관문구 => Localization.GetString(외관결과);

        [NotMapped, JsonProperty("inspd"), Description("Details")]
        public List<검사정보> 검사내역 { get; set; } = new List<검사정보>();
        [NotMapped, JsonProperty("ilreg"), Description("Surfaces")]
        public List<표면불량> 표면불량 { get; set; } = new List<표면불량>();

        [NotMapped, JsonIgnore]
        public List<카메라구분> 검사완료 = new List<카메라구분>();
        [NotMapped, JsonIgnore]
        public List<카메라구분> 그랩완료 = new List<카메라구분>();
        [NotMapped, JsonIgnore]
        public Boolean 검사완료여부 = false;

        public 검사결과()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
        }

        public 검사결과 Reset()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
            this.측정결과 = 결과구분.WA;
            this.치수결과 = 결과구분.WA;
            this.외관결과 = 결과구분.WA;
            this.큐알등급 = 큐알등급.X;
            this.큐알내용 = String.Empty;
            this.불량정보 = String.Empty;
            this.검사내역.Clear();
            this.표면불량.Clear();
            this.검사완료.Clear();
            this.그랩완료.Clear();

            검사설정 자료 = Global.모델자료.GetItem(this.모델구분)?.검사설정;
            if (자료 != null)
            {
                foreach (검사정보 정보 in 자료)
                {
                    if (!정보.검사여부) continue;
                    this.검사내역.Add(검사정보.Create(정보, this.검사일시));
                }
            }
            return this;
        }
        public 검사결과 Reset(카메라구분 카메라)
        {
            검사설정 자료 = Global.모델자료.GetItem(this.모델구분)?.검사설정;
            foreach (검사정보 정보 in 자료)
            {
                if ((Int32)정보.검사장치 != (Int32)카메라) continue;

                검사정보 수동 = new 검사정보();

                if (정보.검사항목 == 검사항목.None)
                    수동 = this.검사내역.Where(e => e.검사명칭 == 정보.검사명칭).FirstOrDefault();
                else
                    수동 = this.검사내역.Where(e => e.검사항목 == 정보.검사항목).FirstOrDefault();

                if (수동 == null) continue;
                수동.최소값 = 정보.최소값;
                수동.기준값 = 정보.기준값;
                수동.최대값 = 정보.최대값;
                수동.보정값 = 정보.보정값;
                수동.교정값 = 정보.교정값;
            }
            this.표면불량.RemoveAll(e => (Int32)e.장치구분 == (Int32)카메라);
            this.검사완료.RemoveAll(e => e == 카메라);
            this.그랩완료.RemoveAll(e => e == 카메라);
            return this;
        }
        public void AddRange(List<검사정보> 자료)
        {
            if (자료 == null || 자료.Count < 1) return;
            this.검사내역.AddRange(자료);
            this.검사내역.ForEach(e =>
            {
                e.Init();
                e.검사명칭 = Global.모델자료.GetItemName(this.모델구분, e.검사항목);
            });
            //List<String> 불량내역 = this.검사내역.Where(e => e.측정결과 != 결과구분.OK && e.측정결과 != 결과구분.PS).Select(e => e.검사명칭).ToList();
            //if (불량내역.Count > 0) this.불량정보 = String.Join(",", 불량내역);
        }
        public void AddRange(List<표면불량> 자료)
        {
            if (자료 == null || 자료.Count < 1) return;
            this.표면불량.AddRange(자료);
        }

        public 검사정보 GetItem(장치구분 장치, String name) => 검사내역.Where(e => e.검사장치 == 장치 && e.변수명칭 == name).FirstOrDefault();
        public 검사정보 GetItem(검사항목 항목) => 검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault();

        //public List<검사정보> GetItem(Int32 분류) => 검사내역.Where(e => e.검사분류 == 분류).ToList();

        private String[] AppearanceFields = new String[] { nameof(측정결과), nameof(치수결과), nameof(외관결과) };
        public void SetAppearance(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e == null || !AppearanceFields.Contains(e.Column.FieldName)) return;
            PropertyInfo p = typeof(검사결과).GetProperty(e.Column.FieldName);
            if (p == null || p.PropertyType != typeof(결과구분)) return;
            Object v = p.GetValue(this);
            if (v == null) return;
            e.Appearance.ForeColor = 환경설정.ResultColor((결과구분)v);
        }

        #region 결과적용
        private Decimal PixelToMeter(검사정보 검사, Double value)
        {
            Double result = 0;
            if (value == 0 || 검사.교정값 <= 0 || value == -99999) result = value;
            else if (검사.카메라여부) result = value * Decimal.ToDouble(검사.교정값) / 1000;
            else if (검사.검사명칭.Contains("Burr")) result = value * Decimal.ToDouble(검사.교정값) / 1000;
            else result = value;
            return (Decimal)Math.Round(result, Global.환경설정.결과자릿수);
        }
        private Double MeterToPixel(검사정보 검사, Decimal value)
        {
            if (검사.교정값 <= 0 || !검사.카메라여부) return Decimal.ToDouble(value);
            return Decimal.ToDouble(value) / Decimal.ToDouble(검사.교정값) * 1000;
        }
        public Boolean SetResultValue(검사정보 검사, Double value, out Decimal 결과값, out Decimal 측정값, Boolean 마진포함 = false)
        {
            Decimal result = PixelToMeter(검사, value);
            //Debug.WriteLine($"{검사.검사명칭} / {result}");
            if (검사.검사명칭.StartsWith("H") || 검사.검사명칭.StartsWith("R"))
            {
                if (검사.검사명칭.Contains("X"))
                    result = Math.Abs(result) - (Decimal)982.25;
                if (검사.검사명칭.Contains("Y"))
                    result = result - (Decimal)63;
            }
            else if (검사.검사명칭.StartsWith("T"))
            {
                if (검사.검사항목 != 검사항목.T044)
                {
                    if (검사.instype == InsType.Y)
                    {
                        result = result + (Decimal)63;
                    }
                    else if (검사.instype == InsType.X)
                    {
                        if (검사.검사항목 == 검사항목.T015 || 검사.검사항목 == 검사항목.T016 || 검사.검사항목 == 검사항목.T017 || 검사.검사항목 == 검사항목.T018 || 검사.검사항목 == 검사항목.T019)
                            result = result + (Decimal)982.25;
                        else
                        {
                            result = (Decimal)982.25 - Math.Abs(result);
                        }
                    }
                }
            }
            if (검사.검사명칭.StartsWith("T"))
            {
                if (검사.보정값 != 0)
                {
                    result = Math.Abs(result) - Math.Abs(검사.보정값);
                }
            }

            else if (검사.검사명칭.StartsWith("M"))
            {
                if (검사.검사항목 == 검사항목.MICA01각도 || 검사.검사항목 == 검사항목.MICA02각도)
                {

                }
                else
                {
                    InsType check = 검사.검사명칭.Contains("X") == true ? InsType.X : InsType.Y;
                    Int32 시트번호 = Convert.ToInt32(검사.검사명칭.Substring(1, 2));
                    Int32 시트위치번호 = Convert.ToInt32(검사.검사명칭.Substring(4, 1));

                    InsItem item = 검사.Attr.검사정보;

                    item.D = Convert.ToDouble(result);
                    if (check == InsType.X)
                    {
                        if (시트위치번호 == 1)
                            result = Math.Abs(검사.보정값) - Math.Abs(result);
                        else
                            result = Math.Abs(result) - Math.Abs(검사.보정값);
                    }
                    else
                    {
                        if (시트번호 < 16)
                        {
                            if (시트위치번호 == 2)
                                result = Math.Abs(result) - Math.Abs(검사.보정값);
                            else
                                result = Math.Abs(검사.보정값) - Math.Abs(result);
                        }
                        else if (시트번호 < 31)
                        {
                            if (시트위치번호 == 2)
                                result = Math.Abs(검사.보정값) - Math.Abs(result);
                            else
                                result = Math.Abs(result) - Math.Abs(검사.보정값);
                        }
                        else if (시트번호 < 46)
                        {
                            if (시트위치번호 == 2)
                                result = Math.Abs(result) - Math.Abs(검사.보정값);
                            else
                                result = Math.Abs(검사.보정값) - Math.Abs(result);
                        }
                        else
                        {
                            if (시트위치번호 == 2)
                                result = Math.Abs(검사.보정값) - Math.Abs(result);
                            else
                                result = Math.Abs(result) - Math.Abs(검사.보정값);
                        }
                    }
                }
            }
            else
                result += 검사.보정값;

            result *= 검사.결과부호;

            Boolean r = false;
            if (Global.장치상태.마스터모드)
            {
                Decimal 최소값 = 검사.마스터값 - 검사.마스터공차;
                Decimal 최대값 = 검사.마스터값 + 검사.마스터공차;

                r = result >= 최소값 && result <= 최대값;
            }
            else
                r = result >= 검사.최소값 && result <= 검사.최대값;

            if (Global.환경설정.VIP모드 && (검사.검사명칭.StartsWith("M") || 검사.검사명칭.StartsWith("B")))
            {
                if (r)
                {
                    결과값 = result;
                    측정값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
                }
                else
                {
                    Random random = new Random();
                    Int32 randomNumber = random.Next(1, 9);

                    Double offset = randomNumber * 0.001;

                    if (result < 검사.최소값)
                        결과값 = 검사.최소값 + Convert.ToDecimal(offset);
                    else
                        결과값 = 검사.최대값 - Convert.ToDecimal(offset);

                    측정값 = (Decimal)Math.Round(결과값, Global.환경설정.결과자릿수);
                    return true;
                }
            }
            else
            {
                결과값 = result;
                측정값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            }

            if (r) return true;
            if (검사.마진값 <= 0 || 마진포함) return false;

            Double val = MeterToPixel(검사, Math.Abs(result));
            Double mar = MeterToPixel(검사, Math.Abs(검사.마진값));
            Double min = MeterToPixel(검사, Math.Abs(검사.최소값 + 검사.보정값));
            Double max = MeterToPixel(검사, Math.Abs(검사.최대값 + 검사.보정값));
            Double mgn = Math.Min(min, max) - mar;
            Double mgx = Math.Max(min, max) + mar;
            //Debug.WriteLine($"{min}, {max}, {mar}, {mgn} <= {val} >= {mgx}, {val < mgn}, {val > mgx}", "픽셀");
            if (val < mgn || val > mgx) return false;
            Double rnd = Global.Random.Next(0, 10) / 1000;// Global.Random.NextDouble() / 100;
            min += rnd * 검사.결과부호;
            max -= rnd * 검사.결과부호;
            Double value2 = result < 검사.최소값 ? min : max;
            Boolean r2 = SetResultValue(검사, value2, out Decimal 결과값2, out Decimal 측정값2, true);
            if (r2)
            {
                결과값 = 결과값2;
                측정값 = 측정값2;
            }
            return r2;
        }

        public 검사정보 SetResult(검사정보 검사, Double value)
        {
            if (검사 == null) return null;
            if (Double.IsNaN(value)) { 검사.측정결과 = 결과구분.ER; return 검사; }

            if ((검사.검사명칭.StartsWith("H") || 검사.검사명칭.StartsWith("B")) && 검사.검사명칭.Contains("P"))
            {
                //위치도계산
                Boolean okL = 위치도계산(검사, value, out Decimal 결과값L, out Decimal 측정값L);

                검사.측정값 = Math.Round(측정값L, 3);
                검사.결과값 = Math.Round(결과값L, 3);
                검사.측정결과 = okL ? 결과구분.OK : 결과구분.NG;
                //Debug.WriteLine($"{검사.검사항목} : {검사.측정값} / {검사.최소값} / {검사.최대값} / {okL}");
                return 검사;
            }

            Boolean ok = SetResultValue(검사, value, out Decimal 결과값, out Decimal 측정값);
            검사.측정값 = 측정값;
            검사.결과값 = 결과값;

            검사.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            return 검사;
        }

        public Boolean 위치도계산(검사정보 검사, Double value, out Decimal 결과값, out Decimal 측정값, Boolean 마진포함 = false)
        {
            결과값 = 0;
            측정값 = 0;

            //Debug.WriteLine($"{검사.검사명칭.Substring(0, 6)}");
            String xStr = String.Empty;
            String yStr = String.Empty;
            if (검사.검사명칭.StartsWith("B"))
            {
                //BoltTop, BoltBottom
                xStr = 검사.검사명칭.Contains("Top") ? 검사.검사명칭.Substring(0, 7) + "X" : 검사.검사명칭.Substring(0, 10) + "X";
                yStr = 검사.검사명칭.Contains("Top") ? 검사.검사명칭.Substring(0, 7) + "Y" : 검사.검사명칭.Substring(0, 10) + "Y";
            }
            else
            {
                xStr = 검사.검사명칭.Substring(0, 3) + "X";
                yStr = 검사.검사명칭.Substring(0, 3) + "Y";
            }

            검사정보 정보X = 검사내역.Where(e => e.검사항목.ToString() == xStr).FirstOrDefault();
            검사정보 정보Y = 검사내역.Where(e => e.검사항목.ToString() == yStr).FirstOrDefault();

            Double 편차X = Convert.ToDouble(Math.Abs(정보X.결과값));
            Double 편차Y = Convert.ToDouble(Math.Abs(정보Y.결과값));

            결과값 = Convert.ToDecimal(Math.Sqrt(편차X * 편차X + 편차Y * 편차Y)) * 2;
            측정값 = 결과값;

            Boolean r = false;

            if (Global.장치상태.마스터모드)
            {
                Decimal 최소값 = 검사.마스터값 - 검사.마스터공차;
                Decimal 최대값 = 검사.마스터값 + 검사.마스터공차;
                r = 결과값 >= 최소값 && 결과값 <= 최대값;
            }
            else
                r = 결과값 >= 검사.최소값 && 결과값 <= 검사.최대값;

            return r;
        }

        public 검사정보 SetBurrResult(String name, Double value) => SetResult(검사내역.Where(e => e.검사명칭 == name).FirstOrDefault(), value);
        public 검사정보 SetResult(String name, Double value) => SetResult(검사내역.Where(e => e.검사항목.ToString() == name).FirstOrDefault(), value);
        public 검사정보 SetResult(검사항목 항목, Double value) => SetResult(검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault(), value);
        public void SetResults(카메라구분 카메라, String json)
        {
            try
            {
                List<Result> results = JsonConvert.DeserializeObject<List<Result>>(json);
                foreach (Result result in results)
                {
                    검사정보 결과 = SetResult(result.K, result.V);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "Inspection"); }
        }
        public void SetResults(Dictionary<Int32, Decimal> 내역)
        {
            if (내역 == null) return;
            foreach (Int32 index in 내역.Keys)
            {
                검사항목 항목 = (검사항목)index;
                SetResult(항목, Convert.ToDouble(내역[index]));
            }
        }
        #endregion

        public List<표면불량> 불량영역(카메라구분 카메라) => this.표면불량.Where(e => e.장치구분 == (장치구분)카메라).ToList();
        public void 불량영역제거(카메라구분 카메라)
        {
            List<표면불량> 불량 = 불량영역(카메라);
            불량.ForEach(e => this.표면불량.Remove(e));
        }

        private 결과구분 최종결과(List<결과구분> 결과목록)
        {
            if (결과목록.Contains(결과구분.ER)) return 결과구분.ER;
            if (결과목록.Contains(결과구분.NG)) return 결과구분.NG;
            return 결과구분.OK;
        }
        public 결과구분 결과계산()
        {
            this.SetResult(검사항목.QrLegibility, (Int32)큐알등급.A);
            this.SetResult(검사항목.Imprinted, 1);
            this.SetResult(검사항목.Surface, this.표면불량.Count);

            List<결과구분> 전체결과 = new List<결과구분>();
            List<결과구분> 치수결과 = new List<결과구분>();
            List<결과구분> 외관결과 = new List<결과구분>();

            foreach (검사정보 정보 in this.검사내역)
            {
                if (정보.측정결과 == 결과구분.PS) continue;
                if (정보.측정결과 < 결과구분.ER) 정보.측정결과 = 결과구분.ER; // 미 검사 항목 Error 처리

                if (!전체결과.Contains(정보.측정결과)) 전체결과.Add(정보.측정결과);
                if (정보.검사그룹 == 검사그룹.치수) { if (!치수결과.Contains(정보.측정결과)) 치수결과.Add(정보.측정결과); }
                else if (정보.검사그룹 == 검사그룹.표면) { if (!외관결과.Contains(정보.측정결과)) 외관결과.Add(정보.측정결과); }
            }

            this.측정결과 = 최종결과(전체결과);
            if (this.측정결과 == 결과구분.OK)
            {
                this.치수결과 = 결과구분.OK;
                this.외관결과 = 결과구분.OK;
            }
            else
            {
                this.치수결과 = 최종결과(치수결과);
                this.외관결과 = 최종결과(외관결과);
                String[] 오류 = this.검사내역.Where(e => e.측정결과 != 결과구분.OK).Select(e => e.오류문구).ToArray();
                String[] 라벨 = this.검사내역.Where(e => e.측정결과 != 결과구분.OK && Global.분류자료.GetItem(e.검사분류) != null && Global.분류자료.GetItem(e.검사분류).라벨 != String.Empty).Select(e => Global.분류자료.GetItem(e.검사분류).라벨).ToArray();
                this.불량정보 = String.Join(",", 오류);
                this.라벨내용 = 라벨.Distinct().ToArray();
            }
            return this.측정결과;
        }
    }

    [Table("inspd"), ProtoContract]
    public partial class 검사정보
    {
        [NotMapped, JsonProperty("idcat"), ProtoMember(1), Translation("Category", "검사분류"), BatchEdit(true)]
        public Int32 검사분류 { get; set; } = 0;
        [NotMapped, JsonProperty("idnam"), ProtoMember(2), Translation("Name", "명칭")]
        public String 검사명칭 { get; set; } = String.Empty;

        [Column("idwdt", Order = 0), Required, Key, JsonProperty("idwdt"), ProtoMember(3), Translation("Time", "검사일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("iditm", Order = 1), Required, Key, JsonProperty("iditm"), ProtoMember(4), Translation("Item", "검사항목")]
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        [Column("idgrp"), JsonProperty("idgrp"), ProtoMember(5), Translation("Group", "검사그룹")]
        public 검사그룹 검사그룹 { get; set; } = 검사그룹.없음;
        [Column("iddev"), JsonProperty("iddev"), ProtoMember(6), Translation("Device", "검사장치")]
        public 장치구분 검사장치 { get; set; } = 장치구분.None;
        [Column("iduni"), JsonProperty("iduni"), ProtoMember(7), Translation("Unit", "단위"), BatchEdit(true)]
        public 단위구분 측정단위 { get; set; } = 단위구분.MM;
        [Column("idmin"), JsonProperty("idmin"), ProtoMember(8), Translation("Min", "최소값"), BatchEdit(true)]
        public Decimal 최소값 { get; set; } = 0m;
        [Column("idstd"), JsonProperty("idstd"), ProtoMember(9), Translation("Norminal", "기준값"), BatchEdit(true)]
        public Decimal 기준값 { get; set; } = 0m;
        [Column("idmax"), JsonProperty("idmax"), ProtoMember(10), Translation("Max", "최대값"), BatchEdit(true)]
        public Decimal 최대값 { get; set; } = 0m;
        [Column("idoff"), JsonProperty("idoff"), ProtoMember(11), Translation("Offset", "보정값"), BatchEdit(true)]
        public Decimal 보정값 { get; set; } = 0m;
        [Column("idcal"), JsonProperty("idcal"), ProtoMember(12), Translation("Calib(µm)", "교정(µm)"), BatchEdit(true)]
        public Decimal 교정값 { get; set; } = 0m;
        [Column("idmes"), JsonProperty("idmes"), ProtoMember(13), Translation("Measure", "측정값")]
        public Decimal 측정값 { get; set; } = 0m;
        [Column("idval"), JsonProperty("idval"), ProtoMember(14), Translation("Value", "결과값")]
        public Decimal 결과값 { get; set; } = 0m;
        [NotMapped, JsonProperty("idrel"), ProtoMember(15), Translation("Real", "실측값")]
        public Decimal 실측값 { get; set; } = 0m;
        [NotMapped, JsonProperty("idmag"), ProtoMember(16), Translation("Margin"), BatchEdit(true)]
        public Decimal 마진값 { get; set; } = 0m;
        [Column("idres"), JsonProperty("idres"), ProtoMember(17), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.WA;
        [NotMapped, JsonProperty("iduse"), ProtoMember(18), Translation("Used", "검사"), BatchEdit(true)]
        public Boolean 검사여부 { get; set; } = true;
        [NotMapped, JsonProperty("idmaster"), Translation("Master", "마스터값"), BatchEdit(true)]
        public Decimal 마스터값 { get; set; } = 0m;
        [NotMapped, JsonProperty("idmastertol"), Translation("MasterTol", "마스터공차"), BatchEdit(true)]
        public Decimal 마스터공차 { get; set; } = 0m;
        [NotMapped, JsonProperty("isShow")]
        public Boolean isShow { get; set; } = true;
        [NotMapped, JsonProperty("imPoint")]
        public Boolean 중요검사포인트 { get; set; } = false;
        [NotMapped, JsonIgnore]
        public Double 검사시간 = 0;
        [NotMapped, JsonIgnore]
        public ResultAttribute Attr = null;
        [NotMapped, JsonIgnore]
        public Hosts 피씨구분 => Attr != null ? Attr.피씨구분 : Hosts.None;
        [NotMapped, JsonIgnore]
        public String 변수명칭 => Attr != null ? Attr.변수명칭 : String.Empty;
        [NotMapped, JsonIgnore]
        public Boolean 카메라여부 => Attr != null ? DeviceInfoAttribute.IsCamera(Attr.장치구분) : false;
        [NotMapped, JsonIgnore]
        public Int32 결과부호 => Attr != null ? Attr.결과부호 : 1;
        [NotMapped, JsonIgnore]
        public Double X { get; set; } //Attr != null ? Attr.검사정보.X : 0;
        [NotMapped, JsonIgnore]
        public Double Y { get; set; } //Attr != null ? Attr.검사정보.Y : 0;
        [NotMapped, JsonIgnore]
        public Double D => Attr != null ? Attr.검사정보.D : 0;
        [NotMapped, JsonIgnore]
        public Double H => Attr != null ? Attr.검사정보.H : 0;
        [NotMapped, JsonIgnore]
        public InsType instype => Attr != null ? Attr.검사정보.InsType : InsType.X;
        [NotMapped, JsonIgnore]
        public String 오류내용 = String.Empty;
        [NotMapped, JsonIgnore]
        public String 오류문구
        {
            get
            {
                if (this.측정결과 == 결과구분.OK) return String.Empty;
                if (String.IsNullOrEmpty(오류내용)) return this.검사명칭;
                return $"{this.검사명칭}={오류내용}";
            }
        }
        public static 검사정보 Create(검사정보 정보, DateTime 일시) => new 검사정보() { 검사항목 = 정보.검사항목 }.Set(정보, 일시);

        public static 검사정보 CreateName(검사정보 정보, DateTime 일시) => new 검사정보() { 검사명칭 = 정보.검사명칭, 교정값 = 정보.교정값 }.Set(정보, 일시);
        public void Init()
        {
            Attr = Utils.GetAttribute<ResultAttribute>(this.검사항목);
            if (String.IsNullOrEmpty(this.검사명칭)) this.검사명칭 = this.검사항목.ToString();


            this.X = InsItems.GetItem(this.검사명칭).X;
            this.Y = InsItems.GetItem(this.검사명칭).Y;

            //Debug.WriteLine($"{this.검사항목} => {this.검사분류} / {Global.분류자료.GetItem(this.검사분류).명칭}");

            //this.중요검사포인트 = Global.분류자료.GetItem(this.검사분류).중요포인트표시;

            //this.중요검사포인트 = false;

            //if (this.검사명칭.StartsWith("M"))
            //{
            //    this.교정값 = 29;
            //    Int32 type = Convert.ToInt32(this.검사명칭.Substring(this.검사명칭.Length - 1));
            //    if (type % 2 == 1)
            //    {
            //        this.보정값 = Convert.ToDecimal(InsItems.GetItem(this.검사명칭).X);
            //    }
            //    else
            //        this.보정값 = Convert.ToDecimal(InsItems.GetItem(this.검사명칭).Y);
            //}
        }

        public 검사정보 Set(검사정보 정보, DateTime 일시)
        {
            //Debug.WriteLine($"{정보.검사명칭} => {정보.isShow}");
            if (정보 != null)
            {
                foreach (PropertyInfo p in typeof(검사정보).GetProperties())
                {
                    if (!p.CanWrite) continue;
                    Object v = p.GetValue(정보);
                    if (v == null) continue;
                    p.SetValue(this, v);
                }
            }

            this.검사일시 = 일시;
            this.측정값 = 0;
            this.결과값 = 0;
            this.측정결과 = 결과구분.WA;
            //this.검사분류 = 정보.검사분류;
            this.Init();
            return this;
        }

        public Boolean 교정계산(검사정보 정보)
        {
            try
            {
                InsItem item = 정보.Attr.검사정보;

                if (item.InsType == InsType.X || item.InsType == InsType.Y || item.InsType == InsType.S)
                {
                    if (item.InsType == InsType.S)
                    {
                        InsType check = 정보.검사명칭.Contains("X") == true ? InsType.X : InsType.Y;
                        Int32 시트번호 = Convert.ToInt32(정보.검사명칭.Substring(1, 2));
                        Int32 시트위치번호 = Convert.ToInt32(정보.검사명칭.Substring(4, 1));
                        Decimal 적용값 = 0;

                        if (check == InsType.X)
                        {
                            적용값 = 시트위치번호 == 1 ? Convert.ToDecimal(Math.Abs(item.X)) - this.실측값 : Convert.ToDecimal(Math.Abs(item.X)) + this.실측값;
                        }
                        else
                        {
                            if (시트번호 < 16)
                            {
                                적용값 = 시트위치번호 == 2 ? Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값 : Convert.ToDecimal(Math.Abs(item.Y)) - this.실측값;
                            }
                            else if (시트번호 < 31)
                            {
                                적용값 = 시트위치번호 == 2 ? Convert.ToDecimal(Math.Abs(item.Y)) - this.실측값 : Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값;
                            }
                            else if (시트번호 < 46)
                            {
                                적용값 = 시트위치번호 == 2 ? Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값 : Convert.ToDecimal(Math.Abs(item.Y)) - this.실측값;
                            }
                            else
                            {
                                적용값 = 시트위치번호 == 2 ? Convert.ToDecimal(Math.Abs(item.Y)) - this.실측값 : Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값;
                            }
                        }
                        Debug.WriteLine($"{정보.검사명칭} {시트번호} {시트위치번호} - 적용값:{적용값}");

                        item.D = Convert.ToDouble(적용값);
                        this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(적용값 / this.측정값 * 1000, 9)));
                        this.보정값 = check == InsType.X ? Convert.ToDecimal(item.X) : Convert.ToDecimal(item.Y);
                    }
                    else
                    {
                        //if (정보.검사항목 == 검사항목.T044)
                        //{
                        //    Decimal 적용값 = (Convert.ToDecimal(Math.Abs(item.X)) + this.실측값);
                        //    this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(적용값 / this.측정값 * 1000, 9)));
                        //}
                        //else
                        //{
                        if (item.InsType == InsType.Y)
                        {
                            Decimal 적용값 = this.Y < 0 ? (Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값 - (Decimal)63) : (Convert.ToDecimal(Math.Abs(item.Y)) + this.실측값 + (Decimal)63);
                            this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(적용값 / this.측정값 * 1000, 9)));
                            this.보정값 = Convert.ToDecimal(item.Y);
                        }
                        if (item.InsType == InsType.X)
                        {
                            Decimal 적용값 = 0;
                            if (정보.검사항목 == 검사항목.T015 || 정보.검사항목 == 검사항목.T016 || 정보.검사항목 == 검사항목.T017 || 정보.검사항목 == 검사항목.T018 || 정보.검사항목 == 검사항목.T019)
                            {
                                적용값 = (Convert.ToDecimal(Math.Abs(item.X)) + this.실측값) - (Decimal)982.25;
                            }
                            else
                            {

                                적용값 = (Decimal)982.25 - (Convert.ToDecimal(Math.Abs(item.X)) + this.실측값);
                            }

                            this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(적용값 / this.측정값 * 1000, 9)));
                            this.보정값 = Convert.ToDecimal(item.X);
                        }
                        //}
                    }
                }
                else
                {
                    if ((item.InsType == InsType.H || item.InsType == InsType.R) && (정보.검사명칭.Contains("X") || 정보.검사명칭.Contains("Y")))
                    {
                        if (정보.검사명칭.Contains("X"))
                        {
                            if (this.X == -982.25)
                            {
                                Decimal 적용값 = this.실측값 - (Decimal)정보.X;

                                if (정보.검사항목 == 검사항목.H18X || 정보.검사항목 == 검사항목.H19X)
                                {
                                    Decimal 추가보정 = 결과값 - 적용값;
                                    Debug.WriteLine($"{정보.검사항목} => {적용값} / {this.결과값} / {this.측정값} / {추가보정}");
                                    this.보정값 = (Decimal)982.25 - 추가보정;
                                }
                            }
                            else
                            {
                                //if (정보.검사명칭.Contains("Bolt"))
                                //{
                                //    Debug.WriteLine($"{this.실측값}");
                                //}
                                //else
                                this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(-((Decimal)982.25 - Math.Abs(this.실측값)) / this.측정값 * 1000, 9)));
                            }

                        }
                        else if (정보.검사명칭.Contains("Y"))
                        {
                            this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round((this.실측값 + (Decimal)63) / this.측정값 * 1000, 9)));
                        }

                    }
                    else
                    {
                        if (정보.검사명칭.Contains("Burr")) this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round((Decimal)this.D / 2 / this.측정값 * 1000, 9)));
                        else
                            this.교정값 = Convert.ToDecimal(Math.Abs(Math.Round(this.실측값 / this.측정값 * 1000, 9)));
                    }
                }


                return true;
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.ToString());
                return false;
            }
        }

        public String DisplayText()
        {
            String display = DisplayText(this.결과값);
            if (String.IsNullOrEmpty(display)) display = Utils.FormatNumeric(this.결과값, Global.환경설정.결과표현);
            return display;
        }
        public String DisplayText(Decimal value)
        {
            if (this.검사항목 == 검사항목.QrLegibility) return Utils.GetDescription((큐알등급)Convert.ToInt32(value));
            if (this.측정단위 == 단위구분.EA) return Utils.FormatNumeric(value);
            if (this.측정단위 == 단위구분.ON) return value == 1 ? "OK" : "NG";
            return String.Empty;
        }
        private String[] AppearanceFields = new String[] { nameof(측정결과), nameof(최소값), nameof(최대값), nameof(기준값), nameof(결과값) };
        public void SetAppearance(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e == null || !AppearanceFields.Contains(e.Column.FieldName)) return;
            PropertyInfo p = typeof(검사정보).GetProperty(e.Column.FieldName);
            if (p.Name == nameof(결과값) || p.Name == nameof(측정결과))
                e.Appearance.ForeColor = 환경설정.ResultColor(this.측정결과);
            if (p.PropertyType != typeof(Decimal)) return;
            Object v = p.GetValue(this);
            if (v == null) return;
            String display = DisplayText((Decimal)v);
            if (!String.IsNullOrEmpty(display)) e.DisplayText = display;
        }
    }

    [Table("insuf")]
    public partial class 표면불량
    {
        [Column("iswdt"), Required, Key, JsonProperty("iswdt"), Translation("Time", "일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("isdev"), JsonProperty("isdev"), Translation("Model", "모델")]
        public 장치구분 장치구분 { get; set; } = 장치구분.None;
        [Column("iscof"), JsonProperty("iscof"), Translation("Confidence", "신뢰도")]
        public Single 신뢰점수 { get; set; } = 0;
        [Column("issiz"), JsonProperty("issiz"), Translation("Size", "크기")]
        public Single 검출크기 { get; set; } = 0;
        [Column("isctx"), JsonProperty("islef"), Translation("X", "X")]
        public Single 가로중심 { get; set; } = 0;
        [Column("iscty"), JsonProperty("istop"), Translation("Y", "Y")]
        public Single 세로중심 { get; set; } = 0;
        [Column("iswid"), JsonProperty("iswid"), Translation("Width", "Width")]
        public Single 가로길이 { get; set; } = 0;
        [Column("ishei"), JsonProperty("ishei"), Translation("Height", "Height")]
        public Single 세로길이 { get; set; } = 0;
        [Column("isang"), JsonProperty("isang"), Translation("Angle", "Angle")]
        public Single 회전각도 { get; set; } = 0;
        [Column("islbl"), JsonProperty("islbl"), Translation("Label", "불량유형")]
        public String 불량유형 { get; set; } = String.Empty;
    }

    #region Attributes 
    public class BaseAttribute : Attribute
    {
        public static T GetAttribute<T>(Enum @enum)
        {
            if (@enum == null) return default(T);
            try
            {
                Type type = @enum.GetType();
                return (T)type.GetField(type.GetEnumName(@enum)).GetCustomAttributes(typeof(T), true).FirstOrDefault();
            }
            catch (Exception ex) { Debug.WriteLine($"[{@enum.GetType()}] {ex.Message}", "GetAttribute"); return default(T); }
        }
    }

    public class DeviceInfoAttribute : BaseAttribute
    {
        public Hosts Host = Hosts.None;
        public Boolean IsCam = true;
        public DeviceInfoAttribute(Hosts host, Boolean cam) { Host = host; IsCam = cam; }

        public static Boolean IsCamera(장치구분 구분)
        {
            DeviceInfoAttribute a = GetAttribute<DeviceInfoAttribute>(구분);
            if (a == null) return false;
            return a.IsCam;
        }

        public static Hosts GetHost(장치구분 구분)
        {
            DeviceInfoAttribute a = GetAttribute<DeviceInfoAttribute>(구분);
            if (a == null) return Hosts.None;
            return a.Host;
        }
    }

    public class ResultAttribute : BaseAttribute
    {
        public 검사그룹 검사그룹 = 검사그룹.없음;
        public 장치구분 장치구분 = 장치구분.None;
        public String 변수명칭 = String.Empty;
        public Int32 결과부호 = 1;
        public InsItem 검사정보 = new InsItem();
        public Hosts 피씨구분 = Hosts.Server;
        public ResultAttribute() { }
        public ResultAttribute(검사그룹 그룹, 장치구분 장치) { 검사그룹 = 그룹; 장치구분 = 장치; }
        public ResultAttribute(검사그룹 그룹, 장치구분 장치, String 변수, Hosts pc = Hosts.Server) { 검사그룹 = 그룹; 장치구분 = 장치; 변수명칭 = 변수; 검사정보 = InsItems.GetItem(변수명칭); 피씨구분 = pc; }

        public ResultAttribute(검사그룹 그룹, 장치구분 장치, String 변수, Int32 부호, Hosts pc = Hosts.Server) { 검사그룹 = 그룹; 장치구분 = 장치; 변수명칭 = 변수; 검사정보 = InsItems.GetItem(변수명칭); 결과부호 = 부호; 피씨구분 = pc; }
        //public ResultAttribute(검사그룹 그룹, 장치구분 장치, Int32 부호) { 검사그룹 = 그룹; 장치구분 = 장치; 결과부호 = 부호; }
        //public ResultAttribute(검사그룹 그룹, 장치구분 장치, String 변수, Int32 부호) { 검사그룹 = 그룹; 장치구분 = 장치; 변수명칭 = 변수; 결과부호 = 부호; }

        public static String VarName(검사항목 항목)
        {
            ResultAttribute a = GetAttribute<ResultAttribute>(항목);
            if (a == null) return 항목.ToString();
            return a.변수명칭;
        }
        public static Int32 ValueFactor(검사항목 항목)
        {
            ResultAttribute a = GetAttribute<ResultAttribute>(항목);
            if (a == null) return 1;
            return a.결과부호;
        }

        public static Hosts GetPCType(검사항목 항목)
        {
            ResultAttribute a = GetAttribute<ResultAttribute>(항목);
            if (a == null) return Hosts.None;
            return a.피씨구분;
        }
    }
    #endregion
}
