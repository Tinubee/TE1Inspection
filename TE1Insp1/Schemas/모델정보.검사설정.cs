using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TE1.Schemas
{
    public class 검사설정 : List<검사정보>
    {
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Inspection Settings", "검사설정");
        private String 저장파일 => Path.Combine(Global.환경설정.기본경로, $"Model.{모델번호.ToString("d2")}.json");
        private 모델정보 모델정보;
        public 모델구분 모델구분 => 모델정보.모델구분;
        public Int32 모델번호 => 모델정보.모델번호;
        public 검사설정(모델정보 모델) { this.모델정보 = 모델; }

        public static Boolean Bindable(검사항목 항목)
        {
            ListBindableAttribute b = Utils.GetAttribute<ListBindableAttribute>(항목);
            return b == null || b.ListBindable;
        }

        public void Init() => Load();
        public void Load()
        {
            this.Clear();
            foreach (검사항목 항목 in typeof(검사항목).GetEnumValues())
            {
                if (항목 == 검사항목.None) continue;
                if (!Bindable(항목)) continue;
                ResultAttribute a = Utils.GetAttribute<ResultAttribute>(항목);
                if (a == null || DeviceInfoAttribute.GetHost(a.장치구분) != 환경설정.HostType) continue;
                this.Add(new 검사정보() { 검사항목 = 항목, 검사명칭 = 항목.ToString(), 검사그룹 = a.검사그룹, 검사장치 = a.장치구분 });
            }

            if (!File.Exists(저장파일))
            {
                Global.정보로그(로그영역.GetString(), "Load", $"[{Utils.GetDescription(모델구분)}] 검사설정 파일이 없습니다.", false);
                this.Save();
                return;
            }

            try
            {
                List<검사정보> 자료 = JsonConvert.DeserializeObject<List<검사정보>>(File.ReadAllText(저장파일));
                if (자료 == null)
                {
                    Global.정보로그(로그영역.GetString(), "Load", "저장 된 설정자료가 올바르지 않습니다.", false);
                    return;
                }
                this.Load(자료);
            }
            catch (Exception ex) { Global.오류로그(로그영역.GetString(), "Load", ex.Message, false); }
        }

        public void Load(List<검사정보> 자료)
        {
            try
            {
                if (자료 == null || 자료.Count < 1)
                {
                    Global.정보로그(로그영역.GetString(), "Load", "수신 된 설정자료가 올바르지 않습니다.", false);
                    return;
                }

                this.ForEach(설정 => {
                    검사정보 정보 = 자료.Where(e => e.검사항목 == 설정.검사항목).FirstOrDefault();
                    if (정보 != null) 설정.Set(정보, DateTime.Now);
                });
            }
            catch (Exception ex) { Global.오류로그(로그영역.GetString(), "Load", ex.Message, false); }
        }

        public Boolean Save()
        {
            try
            {
                if (File.Exists(저장파일))
                {
                    String path = Path.Combine(Global.환경설정.기본경로, "backup");
                    if (Common.DirectoryExists(path, true))
                        File.Copy(저장파일, Path.Combine(path, $"검사설정.{모델번호.ToString("d2")}.{Utils.FormatDate(DateTime.Now, "{0:yyMMddhhmmss}")}.json"));
                }
                File.WriteAllText(저장파일, JsonConvert.SerializeObject(this, Formatting.Indented));

                Global.피씨통신.검사설정전송(this);
                return true;
            }
            catch (Exception ex) { Global.오류로그(로그영역.GetString(), "Save", ex.Message, false); return false; }
        }

        public 검사정보 GetItem(검사항목 항목) => this.Where(e => e.검사항목 == 항목).FirstOrDefault();
    }
}
