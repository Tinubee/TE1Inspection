using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace TE1
{
    public enum Language
    {
        [Description("English")]
        EN = 0,
        [Description("한국어")]
        KO = 1,
    }

    public static class Localization
    {
        public static Language CurrentLanguage => (Language)Properties.Settings.Default.Language;
        public static TranslationAttribute 제목 = new TranslationAttribute("TE1 Vision Inspection System");
        public static TranslationAttribute 취소 = new TranslationAttribute("Cancel", "취소");
        public static TranslationAttribute 닫기 = new TranslationAttribute("Close", "닫기");
        public static TranslationAttribute 저장 = new TranslationAttribute("Save", "저장");
        public static TranslationAttribute 삭제 = new TranslationAttribute("Delete", "삭제");
        public static TranslationAttribute 확인 = new TranslationAttribute("Confirm", "확인");
        public static TranslationAttribute 정보 = new TranslationAttribute("Infomation", "정보");
        public static TranslationAttribute 경고 = new TranslationAttribute("Warning", "경고");
        public static TranslationAttribute 오류 = new TranslationAttribute("Error", "오류");
        public static TranslationAttribute 조회 = new TranslationAttribute("Search", "조회");
        public static TranslationAttribute 일자 = new TranslationAttribute("Day", "일자");
        public static TranslationAttribute 시간 = new TranslationAttribute("Time", "시간");
        public static TranslationAttribute 저장완료 = new TranslationAttribute("It's saved.", "저장되었습니다.");
        public static TranslationAttribute 저장확인 = new TranslationAttribute("Want to save it?", "정보를 저장하시겠습니까?");
        public static TranslationAttribute 선택삭제 = new TranslationAttribute("Delete this selected item?", "선택 항목을 삭제하시겠습니까?");

        public static void SetCulture()
        {
            if (CurrentLanguage == Language.KO)
            {
                MvUtils.Localization.CurrentLanguage = MvUtils.Localization.Language.KO;
                CultureInfo.CurrentCulture = new CultureInfo("ko-KR", false);
            }
            else
            {
                MvUtils.Localization.CurrentLanguage = MvUtils.Localization.Language.EN;
                CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            }
        }

        public static String GetString(PropertyInfo prop) { return GetString(prop, CurrentLanguage); }
        public static String GetString(PropertyInfo prop, Language lang)
        {
            TranslationAttribute a = MvUtils.Utils.GetAttribute<TranslationAttribute>(prop);
            if (a == null) return prop.Name;
            return a.GetString(lang);
        }
        public static String GetString(Enum num) { return GetString(num, CurrentLanguage); }
        public static String GetString(Enum num, Language lang)
        {
            TranslationAttribute a = MvUtils.Utils.GetAttribute<TranslationAttribute>(num);
            if (a == null) return num.ToString();
            return a.GetString(lang);
        }

        public static void SetColumnCaption(GridView view, Type source)
        {
            foreach (GridColumn col in view.Columns)
            {
                try
                {
                    PropertyInfo p = source.GetProperty(col.FieldName);
                    if (p == null) continue;
                    col.Caption = GetString(p);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[{source.Name}, {col.FieldName}] {ex.Message}", "SetColumnCaption");
                }
            }
        }

        public static void SetColumnCaption(LookUpEdit edit, Type source)
        {
            foreach (LookUpColumnInfo col in edit.Properties.Columns)
            {
                try
                {
                    PropertyInfo p = source.GetProperty(col.FieldName);
                    if (p == null) continue;
                    col.Caption = GetString(p);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[{source.Name}, {col.FieldName}] {ex.Message}", "SetLookupColumnCaption");
                }
            }
        }
    }

    public class TranslationAttribute : Attribute
    {
        public String KO = String.Empty;
        public String EN = String.Empty;

        public TranslationAttribute() { }
        public TranslationAttribute(String en) : this(en, en) { }
        public TranslationAttribute(String en, String ko) { EN = en; KO = ko; }
        public String GetString(Language lang)
        {
            if (lang == Language.EN) return this.EN;
            if (lang == Language.KO) return this.KO;
            return this.EN;
        }
        public String GetString() => GetString(Localization.CurrentLanguage);
        public override String ToString() => GetString(Localization.CurrentLanguage);
    }
}
