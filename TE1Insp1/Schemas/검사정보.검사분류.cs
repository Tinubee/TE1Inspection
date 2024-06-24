using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TE1.Schemas
{
    public class 분류정보
    {
        [JsonProperty("Code"), Translation("Code", "코드")]
        public Int32 코드 { get; set; } = 1;
        [JsonProperty("Index"), Translation("Index", "순번")]
        public Int32 순번 { get; set; } = 1;
        [JsonProperty("Name"), Translation("Name", "명칭")]
        public String 명칭 { get; set; } = String.Empty;
        [JsonProperty("Label"), Translation("Label", "라벨")]
        public String 라벨 { get; set; } = String.Empty;
        [JsonProperty("Group"), Translation("Group", "그룹")]
        public 검사그룹 그룹 { get; set; } = 검사그룹.없음;

        public void Set(분류정보 정보)
        {
            순번 = 정보.순번;
            명칭 = 정보.명칭;
            그룹 = 정보.그룹;
            라벨 = 정보.라벨;
        }
    }

    public class 분류자료 : BindingList<분류정보>
    {
        [JsonIgnore]
        private const String 로그영역 = "Categorys";
        [JsonIgnore]
        private string 저장파일 => Path.Combine(Global.환경설정.기본경로, "Categorys.json");

        public void Init() => Load();
        public void Close() { }

        public void Load()
        {
            this.Add(new 분류정보 { 코드 = 11, 순번 = 1, 그룹 = 검사그룹.치수, 명칭 = "Hole1", 라벨 = "H1" });
            this.Add(new 분류정보 { 코드 = 12, 순번 = 2, 그룹 = 검사그룹.치수, 명칭 = "Hole2", 라벨 = "H2" });
            this.Add(new 분류정보 { 코드 = 13, 순번 = 3, 그룹 = 검사그룹.치수, 명칭 = "Hole3", 라벨 = "H3" });
            this.Add(new 분류정보 { 코드 = 14, 순번 = 4, 그룹 = 검사그룹.치수, 명칭 = "Hole4", 라벨 = "H4" });
            this.Add(new 분류정보 { 코드 = 21, 순번 = 5, 그룹 = 검사그룹.치수, 명칭 = "Trim1", 라벨 = "T1" });
            this.Add(new 분류정보 { 코드 = 22, 순번 = 6, 그룹 = 검사그룹.치수, 명칭 = "Trim2", 라벨 = "T2" });
            this.Add(new 분류정보 { 코드 = 23, 순번 = 7, 그룹 = 검사그룹.치수, 명칭 = "Trim3", 라벨 = "T3" });
            this.Add(new 분류정보 { 코드 = 24, 순번 = 8, 그룹 = 검사그룹.치수, 명칭 = "Trim4", 라벨 = "T4" });
            this.Add(new 분류정보 { 코드 = 25, 순번 = 9, 그룹 = 검사그룹.치수, 명칭 = "Trim5", 라벨 = "T5" });
            this.Add(new 분류정보 { 코드 = 26, 순번 = 10, 그룹 = 검사그룹.치수, 명칭 = "Trim6", 라벨 = "T6" });
            this.Add(new 분류정보 { 코드 = 27, 순번 = 11, 그룹 = 검사그룹.치수, 명칭 = "Trim7", 라벨 = "T7" });
            this.Add(new 분류정보 { 코드 = 28, 순번 = 12, 그룹 = 검사그룹.치수, 명칭 = "Trim8", 라벨 = "T8 " });
            this.Add(new 분류정보 { 코드 = 31, 순번 = 13, 그룹 = 검사그룹.치수, 명칭 = "Bolt", 라벨 = "B1" });
            this.Add(new 분류정보 { 코드 = 41, 순번 = 14, 그룹 = 검사그룹.치수, 명칭 = "Mica1", 라벨 = "M1" });
            this.Add(new 분류정보 { 코드 = 42, 순번 = 15, 그룹 = 검사그룹.치수, 명칭 = "Mica2", 라벨 = "M2" });
            this.Add(new 분류정보 { 코드 = 51, 순번 = 16, 그룹 = 검사그룹.치수, 명칭 = "Flatness", 라벨 = "F1" });
            this.Add(new 분류정보 { 코드 = 61, 순번 = 17, 그룹 = 검사그룹.치수, 명칭 = "Thickness", 라벨 = String.Empty });
            this.Add(new 분류정보 { 코드 = 81, 순번 = 18, 그룹 = 검사그룹.표면, 명칭 = "Surface1", 라벨 = "S1" });
            this.Add(new 분류정보 { 코드 = 82, 순번 = 19, 그룹 = 검사그룹.표면, 명칭 = "Surface2", 라벨 = "S2" });
            this.Add(new 분류정보 { 코드 = 83, 순번 = 20, 그룹 = 검사그룹.표면, 명칭 = "Surface3", 라벨 = "S3" });
            this.Add(new 분류정보 { 코드 = 84, 순번 = 21, 그룹 = 검사그룹.표면, 명칭 = "Surface4", 라벨 = "S4" });
            this.Add(new 분류정보 { 코드 = 85, 순번 = 22, 그룹 = 검사그룹.표면, 명칭 = "Surface5", 라벨 = "S5" });
            this.Add(new 분류정보 { 코드 = 86, 순번 = 23, 그룹 = 검사그룹.표면, 명칭 = "Surface6", 라벨 = "S6" });
            this.Add(new 분류정보 { 코드 = 91, 순번 = 24, 그룹 = 검사그룹.표면, 명칭 = "QR Code", 라벨 = "Q1" });
            this.Add(new 분류정보 { 코드 = 92, 순번 = 25, 그룹 = 검사그룹.표면, 명칭 = "Imprinted", 라벨 = "Q2" });

            if (!File.Exists(this.저장파일)) return;
            try
            {
                List<분류정보> 자료 = JsonConvert.DeserializeObject<List<분류정보>>(File.ReadAllText(this.저장파일), Utils.JsonSetting());
                자료.Sort((a, b) => a.순번.CompareTo(b.순번));
                foreach (분류정보 정보 in 자료)
                    this.GetItem(정보.코드)?.Set(정보);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Load", ex.Message, false);
            }
        }

        public void Save()
        {
            if (!Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, Utils.JsonSetting())))
                Global.오류로그(로그영역, "Save", "Saving failed.", true);
            else Global.정보로그(로그영역, "Save", "Saved.", true);
        }

        public 분류정보 GetItem(Int32 코드) => this.Where(e => e.코드 == 코드).FirstOrDefault();
        //public 분류정보 GetName(Int32 코드) => this.Where(e => e.코드 == 코드);
    }
}
