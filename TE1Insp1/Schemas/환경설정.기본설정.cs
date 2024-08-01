using MvUtils;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TE1.Schemas
{
    public enum Hosts
    {
        None = 0,
        Server = 1, //3번
        Measure = 2, //1번
        Surface = 3, //2번
    }

    public enum 동작구분
    {
        Live = 0,
        LocalTest = 2
    }

    public partial class 환경설정
    {
        public delegate void 모델변경(모델구분 모델코드);
        public event 모델변경 모델변경알림;
        public event Global.BaseEvent VIP모드변경알림;

        [JsonIgnore]
        public const String 프로젝트번호 = "23-0449-003";
        [JsonIgnore]
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Preferences", "환경설정");
        [JsonIgnore]
        private String 저장파일 => Path.Combine(this.기본경로, "Config.json");

        [Description("프로그램 동작구분"), JsonProperty("RunType")]
        public 동작구분 동작구분 { get; set; } = 동작구분.Live;

        [Translation("Config Path", "설정 저장 경로"), JsonProperty("ConfigSavePath")]
        public String 기본경로 { get; set; } = $@"{DefaultPath}\Config";
        [Translation("Document Save Path", "문서 저장 경로"), JsonProperty("DocumentSavePath")]
        public String 문서저장 { get; set; } = $@"{DefaultPath}\SaveData";
        [Translation("Copy Image Save Path", "사본 저장 경로"), JsonProperty("ImageSavePath")]
        public String 사진저장 { get; set; } = $@"{DefaultPath}\SaveImage";

        [Translation("Decimals", "검사 결과 자릿수"), JsonProperty("Decimals")]
        public Int32 결과자릿수 { get; set; } = 3;
        [Translation("Results Storage Days", "검사 결과 보관일"), JsonProperty("DaysToKeepResults")]
        public Int32 결과보관 { get; set; } = 900;
        [Translation("Logs Storage Days", "로그 보관일"), JsonProperty("DaysToKeepLogs")]
        public Int32 로그보관 { get; set; } = 60;
        [Translation("Enable Surface Insp.", "표면검사"), JsonProperty("EnableSurface")]
        public Boolean 표면검사 { get; set; } = false;

        [Description("비전 Tools"), JsonIgnore]
        public String 도구경로 => Path.Combine(기본경로, "Tools");
        [Description("마스터 이미지"), JsonIgnore]
        public String 마스터사진 => Path.Combine(기본경로, "Masters");

        [JsonProperty("CurrentModel")]
        public 모델구분 선택모델 { get; set; } = 모델구분.UPR3P24S;
        [JsonIgnore]
        public Boolean VIP모드 { get; set; } = false;
        [JsonIgnore]
        public Boolean Cam0102개별이미지저장 { get; set; } = false;
        [JsonIgnore]
        public String Format => "#,0." + String.Empty.PadLeft(this.결과자릿수, '0');
        [JsonIgnore]
        public String 결과표현 => "{0:" + Format + "}";

        [JsonIgnore, Description("사용자명")]
        public String 사용자명 { get; set; } = String.Empty;
        [JsonIgnore, Description("권한구분")]
        public 유저권한구분 사용권한 { get; set; } = 유저권한구분.없음;
        public Boolean 권한여부(유저권한구분 요구권한) => (Int32)사용권한 >= (Int32)요구권한;
        [JsonIgnore, Description("슈퍼유저")]
        public const String 시스템관리자 = "ivmadmin";
        public 유저권한구분 시스템관리자인증(string 사용자명, string 비밀번호)
        {
            if (사용자명 != 시스템관리자) return 유저권한구분.없음;
            String pass = $"{시스템관리자}";// {Utils.FormatDate(DateTime.Now, "{0:ddHH}")}";
            if (비밀번호 == pass)
            {
                this.시스템관리자로그인();
                return 유저권한구분.시스템;
            }
            return 유저권한구분.없음;
        }
        public void 시스템관리자로그인()
        {
            this.사용자명 = 시스템관리자;
            this.사용권한 = 유저권한구분.시스템;
        }
        [JsonIgnore]
        public Boolean 슈퍼유저 => 사용권한 == 유저권한구분.시스템 && 사용자명 == 시스템관리자;

        [JsonIgnore, Description("이미지 저장 디스크 사용율")]
        public Int32 저장비율 => 100 - this.SaveImageDriveFreeSpace();
        [JsonIgnore]
        public static String LocalApplicationData => SpecialFolder(Environment.SpecialFolder.LocalApplicationData);
        [JsonIgnore]
        public static String ExecutablePath => Path.GetDirectoryName(Application.ExecutablePath);
        public static String SpecialFolder(Environment.SpecialFolder folder) => Environment.GetFolderPath(folder);

        public static NpgsqlConnection CreateDbConnection()
        {
            NpgsqlConnectionStringBuilder b = new NpgsqlConnectionStringBuilder() { Host = Global.환경설정.서버주소, Port = 5432, Username = "postgres", Password = "ivmadmin", Database = "te1vision" };
            return new NpgsqlConnection(b.ConnectionString);
        }

        public Boolean CanDbConnect()
        {
            Boolean can = false;
            try
            {
                NpgsqlConnection conn = CreateDbConnection();
                conn.Open();
                can = conn.ProcessID > 0;
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Database server connection failure.", ex.Message, true);
            }
            return can;
        }

        public Boolean Init() => this.Load();
        public void Close() { } // => this.Save();

        public Boolean Load()
        {
            if (File.Exists(저장파일))
            {
                try
                {
                    환경설정 설정 = JsonConvert.DeserializeObject<환경설정>(File.ReadAllText(저장파일, Encoding.UTF8));
                    foreach (PropertyInfo p in 설정.GetType().GetProperties())
                    {
                        if (!p.CanWrite) continue;
                        Object v = p.GetValue(설정);
                        if (v == null) continue;
                        p.SetValue(this, v);
                    }
                }
                catch (Exception ex)
                {
                    Global.오류로그(로그영역.GetString(), "Failed to initialize preferences.", ex.Message, true);
                }
            }
            else
            {
                this.Save();
                Global.정보로그(로그영역.GetString(), "Initialize", "No files are saved.", false);
            }

            Debug.WriteLine(this.동작구분, "동작구분");
            if (this.동작구분 == 동작구분.LocalTest) this.서버주소 = "localhost";

            if (!CanDbConnect())
            {
                Global.오류로그(로그영역.GetString(), "Database", "Database server connection failure.", true);
                return false;
            }
            Common.DirectoryExists(Path.Combine(Application.StartupPath, @"Views"), true);
            if (!Common.DirectoryExists(기본경로, true))
            {
                Global.오류로그(로그영역.GetString(), "Initialize", "Can't create a preferences folder.", true);
                return false;
            }
            if (!Common.DirectoryExists(사진저장, true))
                Global.오류로그(로그영역.GetString(), "Initialize", "Can't create a folder to store images.", true);
            if (!Common.DirectoryExists(문서저장, true))
                Global.오류로그(로그영역.GetString(), "Initialize", "Can't create a folder to store documents.", true);
            if (!Common.DirectoryExists(도구경로, true))
                Global.오류로그(로그영역.GetString(), "Initialize", "Can't create a vision tools folder.", true);
            if (!Common.DirectoryExists(마스터사진, true))
                Global.오류로그(로그영역.GetString(), "Initialize", "Can't create a master image folder.", true);
            return true;
        }

        public void Save()
        {
            if (!Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, Utils.JsonSetting())))
                Global.오류로그(로그영역.GetString(), "Save", "Saving preferences failed.", true);
        }

        public void 모델변경요청(Int32 모델번호) => this.모델변경요청((모델구분)모델번호);
        public void 모델변경요청(모델구분 모델구분)
        {
            if (this.선택모델 == 모델구분) return;
            this.선택모델 = 모델구분;
            this.모델변경알림?.Invoke(this.선택모델);
        }

        public void VIP모드변경요청()
        {
            this.VIP모드 = !this.VIP모드;
            this.VIP모드변경알림?.Invoke();
        }

        [Description("결과별 표현색상")]
        public static Color ResultColor(결과구분 구분)
        {
            if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color HoleResultColor(결과구분 구분)
        {
            //if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.Lime;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color TrimResultColor(결과구분 구분)
        {
            //if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.RoyalBlue;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color MicaResultColor(결과구분 구분)
        {
           // if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.Yellow;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color FlatnessResultColor(결과구분 구분)
        {
            //if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.Aqua;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color BoltResultColor(결과구분 구분)
        {
            //if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.Gold;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static Color ThicknessResultColor(결과구분 구분)
        {
            //if (구분 == 결과구분.WA) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK || 구분 == 결과구분.WA) return Color.Olive;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }
        #region 드라이브 용량계산
        private DriveInfo ImageSaveDrive = null;
        private DriveInfo GetSaveImageDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (this.사진저장.StartsWith(drive.Name))
                {
                    //Debug.WriteLine(drive.Name, drive.VolumeLabel);
                    this.ImageSaveDrive = drive;
                    return this.ImageSaveDrive;
                }
            }
            if (drives.Length > 0)
                this.ImageSaveDrive = drives[0];

            return this.ImageSaveDrive;
        }

        public Int32 SaveImageDriveFreeSpace()
        {
            DriveInfo drive = this.GetSaveImageDrive();
            double FreeSpace = drive.AvailableFreeSpace / (double)drive.TotalSize * 100;
            return Convert.ToInt32(FreeSpace);
        }
        #endregion
    }
}
