using MvUtils;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace TE1.Schemas
{
    public enum 사진형식 { Jpg } // , Bmp, Png
    public class 사진저장
    {
        [JsonProperty("Camera"), Translation("Camera", "카메라")]
        public 카메라구분 카메라 { get; set; } = 카메라구분.None;
        //[JsonProperty("Origin"), Translation("Origin", "원본저장")]
        //public Boolean 원본저장 { get; set; } = false;
        [JsonProperty("Result"), Translation("Result", "결과저장")] // 결과저장 로직 구현
        public Boolean 결과저장 { get; set; } = false;
        [JsonProperty("Copy"), Translation("Copy", "사본저장")]
        public Boolean 사본저장 { get; set; } = true;
        [JsonProperty("Format"), Translation("Format", "사본유형")]
        public 사진형식 사본유형 { get; set; } = 사진형식.Jpg;
        [JsonProperty("Scale"), Translation("Scale", "사진비율")]
        public Int32 사진비율 { get; set; } = 100;
        [JsonProperty("Quality"), Translation("Quality", "사진품질")]
        public Int32 사진품질 { get; set; } = 70;

        public void Set(사진저장 정보)
        {
            if (정보 == null) return;
            foreach(PropertyInfo p in typeof(사진저장).GetProperties())
            {
                if (!p.CanWrite) continue;
                Object v = p.GetValue(정보);
                if (v == null) continue;
                p.SetValue(this, v);
            }
        }
    }

    public class 사진자료 : Dictionary<카메라구분, 사진저장>
    {
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Save Images", "사진저장");
        private String 저장파일 => Path.Combine(Global.환경설정.기본경로, "Images.json");

        public void Init() => Load();
        public void Close() { }
        public void Load()
        {
            foreach (카메라구분 구분 in typeof(카메라구분).GetEnumValues())
            {
                if (구분 == 카메라구분.None) continue;
                this.Add(구분, new 사진저장 { 카메라 = 구분 });
            }

            if (!File.Exists(저장파일)) return;
            try
            {
                String json = File.ReadAllText(저장파일);
                List<사진저장> 자료 = JsonConvert.DeserializeObject<List<사진저장>>(json);
                foreach (사진저장 정보 in 자료)
                {
                    if (!this.ContainsKey(정보.카메라)) continue;
                    this[정보.카메라].Set(정보);
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Load", ex.Message, true);
            }
        }
        public void Save() => File.WriteAllText(저장파일, JsonConvert.SerializeObject(this.Values.ToList()));
        public 사진저장 GetItem(카메라구분 카메라) => this.Values.Where(e => e.카메라 == 카메라).FirstOrDefault();

        #region 사진저장
        public void SaveImage(그랩장치 장치, 검사결과 결과) => SaveImage(장치.구분, 장치.MatImage(), 결과.검사일시, 결과.검사번호);

        public void SaveImage(그랩장치 장치, 검사결과 결과, Mat 합성이미지) => SaveImage(장치.구분, 합성이미지, 결과.검사일시, 결과.검사번호);
        public void SaveImage(카메라구분 카메라, Mat image, DateTime 시간, Int32 번호)
        {
            if (!this.ContainsKey(카메라)) return;
            사진저장 정보 = this[카메라];
            if (!정보.사본저장) return;

            new Thread(() => {
                String file = String.Empty;
                if (!정보.사본저장) return;
                file = CopyImageFile(시간, 번호, 카메라, 정보.사본유형);
                Double scale = Math.Max(0.1, Math.Min((Double)정보.사진비율 / 100, 1.0));
                //Debug.WriteLine($"Scale: {정보.사진비율} => {scale}", 카메라.ToString());
                if (scale == 1) this.SaveImage(정보, image, file);
                else
                {
                    using(Mat mat = Common.Resize(image, scale))
                        this.SaveImage(정보, mat, file);
                }
            }).Start();
        }
        public void SaveImage(사진저장 정보, Mat mat, String file)
        {
            if (정보 == null || mat == null || String.IsNullOrEmpty(file)) return;
            Boolean result = false;
            String error = String.Empty;
            if (정보.사본유형 == 사진형식.Jpg) result = Common.ImageSaveJpeg(mat, file, out error, 100); //정보.사진품질
            //else if (정보.사본유형 == 사진형식.Png) result = Common.ImageSavePng(mat, file, out error);
            //else if (정보.사본유형 == 사진형식.Bmp) result = Common.ImageSaveBmp(mat, file, out error);
            else return;
            if (!result) Global.오류로그(로그영역.GetString(), 정보.카메라.ToString(), error, false);
        }

        public String CopyImagePath(DateTime 시간, 카메라구분 카메라)
        {
            String path = Path.Combine(Global.환경설정.사진저장, Utils.FormatDate(시간, "{0:yyyy-MM-dd}"), 카메라.ToString());
            Common.DirectoryExists(path, true);
            return path;
        }
        public String CopyImageFile(DateTime 시간, Int32 번호, 카메라구분 카메라)
        {
            if (!this.ContainsKey(카메라)) return String.Empty;
            return CopyImageFile(시간, 번호, 카메라, this[카메라].사본유형);
        }
        public String CopyImageFile(DateTime 시간, Int32 번호, 카메라구분 카메라, 사진형식 형식)
        {
            String path = CopyImagePath(시간, 카메라);
            String file = SaveImageFileName(시간, 번호, 형식);
            if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(file)) return String.Empty;
            return Path.Combine(path, file);
        }
        public static String SaveImageFileName(DateTime 시간, Int32 번호, 사진형식 형식) =>
            $"{번호.ToString("d4")}_{Utils.FormatDate(시간, "{0:HHmmss}")}.{형식.ToString()}";
        #endregion
    }
}
