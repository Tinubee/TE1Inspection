﻿using TE1.Schemas;
using MvUtils;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TE1
{
    public static class Global
    {
        public const String SkinName = "The Bezier";
        public const String BlackPalette = "Office Black";
        public const String ColorPalette = "Office Colorful";
        public static MainForm MainForm = null;
        public static Random Random = new Random();
        public static Int32 RandomSign => Random.NextDouble() >= 0.5 ? 1 : -1;
        public delegate void BaseEvent();
        public static event EventHandler<Boolean> Initialized;

        private const String 로그영역 = "Global";
        public static 환경설정 환경설정;
        public static 로그자료 로그자료;
        public static 유저자료 유저자료;
        public static 모델자료 모델자료;
        public static 장치통신 장치통신;
        public static 피씨통신 피씨통신;
        public static 큐알각인 큐알각인;
        public static 큐알리더 큐알리더;
        public static 라벨인쇄 라벨인쇄;
        public static 분류자료 분류자료;
        public static 검사자료 검사자료;
        public static 상태정보 상태정보;

        public static class 장치상태
        {
            public static Boolean 정상여부 => 컴퓨터1 && 컴퓨터2 && 큐알각인 && 큐알리더;
            public static Boolean 컴퓨터1  => Global.피씨통신.MeasureConnected;
            public static Boolean 컴퓨터2  => Global.피씨통신.SurfaceConnected;
            public static Boolean 큐알각인 => Global.큐알각인.연결여부;
            public static Boolean 큐알리더 => Global.큐알리더.연결여부;
            public static Boolean 라벨인쇄 => Global.라벨인쇄.연결여부;
            public static Boolean 자동수동 => Global.장치통신.자동수동;
            public static Boolean 시작정지 => Global.장치통신.시작정지;
        }

        public static Boolean Init()
        {
            알림화면 = new AlertControl() { AutoHeight = true, FormSize = new System.Drawing.Size(400, 200), PopupLocation = AlertControl.PopupLocations.CenterForm };
            로그화면 = new AlertControl() { PopupLocation = AlertControl.PopupLocations.BottomRight };
            질문화면 = new FlyoutDialog(FlyoutDialog.DialogType.Confirm);
            try
            {
                로그자료 = new 로그자료();
                환경설정 = new 환경설정();
                유저자료 = new 유저자료();
                장치통신 = new 장치통신();
                피씨통신 = new 피씨통신();
                모델자료 = new 모델자료();
                큐알각인 = new 큐알각인();
                큐알리더 = new 큐알리더();
                라벨인쇄 = new 라벨인쇄();
                분류자료 = new 분류자료();
                검사자료 = new 검사자료();
                상태정보 = new 상태정보();

                if (!환경설정.Init()) return false;
                로그자료.Init();
                유저자료.Init();
                장치통신.Init();
                피씨통신.Init();
                모델자료.Init();
                분류자료.Init();
                검사자료.Init();
                큐알각인.Init();
                큐알리더.Init();
                라벨인쇄.Init();
                if (!장치통신.Open()) new Exception("Unable to connect to PLC.");
                if (!피씨통신.Open()) new Exception("Unable to connect to server.");

                상태정보.Init();
                Global.정보로그(로그영역, "Initialize", "Initialize the system.", false);
                Initialized?.Invoke(null, true);
                return true;
            }
            catch (Exception ex)
            {
                Utils.DebugException(ex, 3);
                Global.오류로그(로그영역, "Initialize", "System initialization failed.\n" + ex.Message, true);
            }
            Initialized.Invoke(null, false);
            return false;
        }

        public static Boolean Close()
        {
            Global.정보로그(로그영역, "Termination", "Shut down the system.", false);
            Boolean r = false;
            try
            {
                분류자료.Close();
                검사자료.Close();
                if (환경설정.동작구분 == 동작구분.Live)
                {
                    큐알각인.Close();
                    큐알리더.Close();
                    라벨인쇄.Close();
                }

                피씨통신.Close();
                장치통신.Close();
                유저자료.Close();
                환경설정.Close();
                모델자료.Close();
                로그자료.Close();
                
                Properties.Settings.Default.Save();
                Debug.WriteLine("시스템 종료");
                r = true;
            }
            catch (Exception ex) { r = Utils.ErrorMsg("An error occurred during system shutdown.\n" + ex.Message); }
            finally { Environment.Exit(0); }
            return r;
        }

        public static void Start()
        {
            장치통신.Start();
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            큐알각인.Start();
            라벨인쇄.Start();
        }

        public static void DxLocalization()
        {
            if (Localization.CurrentLanguage == Language.KO)
            {
                MvUtils.Localization.CurrentLanguage = MvUtils.Localization.Language.KO;
                MvUtils.DxDataGridLocalizer.Enable();
                MvUtils.DxEditorsLocalizer.Enable();
                MvUtils.DxDataFilteringLocalizer.Enable();
                MvUtils.DxLayoutLocalizer.Enable();
                MvUtils.DxBarLocalizer.Enable();
            }
            else MvUtils.Localization.CurrentLanguage = MvUtils.Localization.Language.EN;
        }

        public static String GetGuid()
        {
            Assembly assembly = typeof(Program).Assembly;
            GuidAttribute attribute = assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0] as GuidAttribute;
            return attribute.Value;
        }

        #region Log / Notify / Confirm
        private static AlertControl 알림화면;
        private static AlertControl 로그화면;
        private static FlyoutDialog 질문화면;
        public static void ShowLog(Form owner, 로그정보 로그)
        {
            if (owner == null || owner.IsDisposed) return;
            if (owner.InvokeRequired) { owner.BeginInvoke(new Action(() => ShowLog(owner, 로그))); return; }
            if (로그.구분 == 로그구분.오류)
                로그화면.Show(AlertControl.AlertTypes.Invalid, 로그.제목, 로그.내용, owner);
            else if (로그.구분 == 로그구분.경고)
                로그화면.Show(AlertControl.AlertTypes.Warning, 로그.제목, 로그.내용, owner);
            else if (로그.구분 == 로그구분.정보)
                로그화면.Show(AlertControl.AlertTypes.Information, 로그.제목, 로그.내용, owner);
        }
        public static void ShowLog(로그정보 로그) => ShowLog(MainForm, 로그);

        public static 로그정보 로그기록(String 영역, 로그구분 구분, String 제목, String 내용, Form parent)
        {
            try
            {
                로그정보 로그 = 로그자료.Add(영역, 구분, 제목, 내용);
                #if DEBUG
                Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss}")}\t{구분.ToString()}\t{영역}\t{제목}\t{내용}");
                #endif
                ShowLog(parent, 로그);
                return 로그;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "로그기록 오류"); }
            return null;
        }
        public static 로그정보 오류로그(String 영역, String 제목, String 내용, bool show) => 오류로그(영역, 제목, 내용, show ? MainForm : null);
        public static 로그정보 오류로그(String 영역, String 제목, String 내용, Form parent) => 로그기록(영역, 로그구분.오류, 제목, 내용, parent);
        public static 로그정보 경고로그(String 영역, String 제목, String 내용, bool show) => 경고로그(영역, 제목, 내용, show ? MainForm : null);
        public static 로그정보 경고로그(String 영역, String 제목, String 내용, Form parent) => 로그기록(영역, 로그구분.경고, 제목, 내용, parent);
        public static 로그정보 정보로그(String 영역, String 제목, String 내용, bool show) => 정보로그(영역, 제목, 내용, show ? MainForm : null);
        public static 로그정보 정보로그(String 영역, String 제목, String 내용, Form parent) => 로그기록(영역, 로그구분.정보, 제목, 내용, parent);

        public static Boolean Notify(String 내용, String 제목, AlertControl.AlertTypes 구분 = AlertControl.AlertTypes.Information) => Notify(MainForm, 내용, 제목, 구분);
        public static Boolean Notify(Form owner, String 내용, String 제목, AlertControl.AlertTypes 구분 = AlertControl.AlertTypes.Information)
        {
            알림화면.Show(구분, 제목, 내용, owner);
            return 구분 == AlertControl.AlertTypes.Information;
        }

        public static Boolean Confirm(String 내용, String 제목) => Confirm(MainForm, 내용, 제목);
        public static Boolean Confirm(String 내용) => Confirm(MainForm, 내용, Localization.확인.GetString());
        public static Boolean Confirm(Control control, String 내용) => Confirm(control?.FindForm(), 내용, Localization.확인.GetString());
        public static Boolean Confirm(Form parent, String 내용, String 제목) => 질문화면.ShowConfirm(parent, 제목, 내용);
        #endregion
    }
}
