using TE1.UI.Forms;
using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Cognex.VisionPro;
using OpenCvSharp;

namespace TE1.Schemas
{
    public partial class 검사결과
    {
        public Boolean 카메라검사보기(검사정보 정보)
        {
            try
            {
                if (this.검사번호 >= 9999 || this.검사번호 < 1 || 정보 == null || !DeviceInfoAttribute.IsCamera(정보.검사장치)) return false;
                카메라구분 카메라 = (카메라구분)정보.검사장치;
                String file = Global.사진자료.CopyImageFile(this.검사일시, this.검사번호, 카메라);
                if (String.IsNullOrEmpty(file) || !File.Exists(file))
                    return Utils.WarningMsg("The image file does not exist.");
                CogToolEdit cogToolEdit = new CogToolEdit() { 사진파일 = file };
                cogToolEdit.Init(Global.비전검사[카메라]);
                cogToolEdit.Show(Global.MainForm);
                return true;
            }
            catch (Exception ex) { Utils.ErrorMsg(ex.Message); }
            return false;
        }
    }

    public partial class 표면불량
    {
        public 표면불량() { }
        public 표면불량(DateTime 일시, 카메라구분 카메라, Single 신뢰, RotatedRect 영역, String 유형, Single 축소비율)
        {
            검사일시 = 일시;
            장치구분 = (장치구분)카메라;
            불량유형 = 유형;
            가로중심 = 영역.Center.X;
            세로중심 = 영역.Center.Y;
            가로길이 = 영역.Size.Width;
            세로길이 = 영역.Size.Height;
            회전각도 = 영역.Angle;
            신뢰점수 = 신뢰;
            검출크기 = (Single)(Math.Max(영역.Size.Width, 영역.Size.Height) * Global.그랩제어.교정Y(카메라) / 1000);
        }
        public CogRectangleAffine GetRectangleAffine() => new CogRectangleAffine() { CenterX = 가로중심, CenterY = 세로중심, SideXLength = 가로길이, SideYLength = 세로길이, Rotation = Common.ToRadian(회전각도) };
        public CogRectangleAffine GetRectangleAffine(CogColorConstants color) { var r = GetRectangleAffine(); r.Color = color; return r; }
        public CogRectangleAffine GetRectangleAffine(CogColorConstants color, Int32 lineWidth) { var r = GetRectangleAffine(color); r.LineWidthInScreenPixels = lineWidth; return r; }
    }
}
