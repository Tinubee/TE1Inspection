using Cognex.VisionPro;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using TE1.Multicam;
using OpenCvSharp.Extensions;

namespace TE1.Schemas
{
    public class 그랩장치 : IDisposable
    {
        [JsonProperty("Camera"), Translation("Camera", "카메라")]
        public virtual 카메라구분 구분 { get; set; } = 카메라구분.None;
        [JsonIgnore, Translation("Index", "번호")]
        public virtual Int32 번호 { get; set; } = 0;
        [JsonProperty("Serial"), Translation("Serial", "Serial")]
        public virtual String 코드 { get; set; } = String.Empty;
        [JsonIgnore, Translation("Name", "명칭")]
        public virtual String 명칭 { get; set; } = String.Empty;
        [JsonProperty("Description"), Translation("Description", "설명")]
        public virtual String 설명 { get; set; } = String.Empty;
        [JsonProperty("IpAddress"), Translation("IP", "IP")]
        public virtual String 주소 { get; set; } = String.Empty;
        [JsonProperty("Width"), Description("Width"), Translation("Width", "가로")]
        public virtual Int32 가로 { get; set; } = 0;
        [JsonProperty("Height"), Description("Height"), Translation("Height", "세로")]
        public virtual Int32 세로 { get; set; } = 0;
        [JsonProperty("CalibX"), Description("CalibX(µm)"), Translation("CalibX(µm)", "CalibX(µm)")]
        public virtual Double 교정X { get; set; } = 0;
        [JsonProperty("CalibY"), Description("CalibY(µm)"), Translation("CalibY(µm)", "CalibY(µm)")]
        public virtual Double 교정Y { get; set; } = 0;
        [JsonIgnore, Description("카메라 초기화 상태"), Translation("Live", "상태")]
        public virtual Boolean 상태 { get; set; } = false;
        [JsonIgnore]
        internal virtual MatType ImageType => MatType.CV_8UC1;
        [JsonIgnore]
        internal virtual Boolean UseMemoryCopy => false;
        [JsonIgnore]
        internal Int32 ImageWidth = 0;
        [JsonIgnore]
        internal Int32 ImageHeight = 0;
        [JsonIgnore]
        internal Object BufferLock = new Object();
        [JsonIgnore]
        internal UInt32 BufferSize = 0;
        [JsonIgnore]
        internal IntPtr BufferAddress = IntPtr.Zero;
        [JsonIgnore]
        internal Queue<Mat> Images = new Queue<Mat>();
        [JsonIgnore]
        internal Mat Image => Images.LastOrDefault<Mat>();
        [JsonIgnore]
        public const String 로그영역 = "Camera";

        public void Dispose()
        {
            while (this.Images.Count > 3)
                this.Dispose(this.Images.Dequeue());
        }
        internal void Dispose(Mat image)
        {
            if (image == null || image.IsDisposed) return;
            image.Dispose();
        }

        public virtual void Set(그랩장치 장치)
        {
            if (장치 == null) return;
            this.코드 = 장치.코드;
            this.설명 = 장치.설명;
            this.교정X = 장치.교정X;
            this.교정Y = 장치.교정Y;
        }
        public virtual Boolean Init() => false;
        public virtual Boolean Active() => false;
        public virtual Boolean Stop() => false;
        public virtual Boolean Close()
        {
            while (this.Images.Count > 0)
                this.Dispose(this.Images.Dequeue());
            return true;
        }
        public virtual void TurnOn() => Global.조명제어.TurnOn(this.구분);
        public virtual void TurnOff() => Global.조명제어.TurnOff(this.구분);

        #region 이미지그랩
        internal void InitBuffers(Int32 width, Int32 height)
        {
            if (width == 0 || height == 0) return;
            Int32 channels = ImageType == MatType.CV_8UC3 ? 3 : 1;
            Int32 imageSize = width * height * channels;
            if (BufferAddress != IntPtr.Zero && imageSize == BufferSize) return;
            this.ImageWidth = width; this.ImageHeight = height;
            if (BufferAddress != IntPtr.Zero)
            {
                Marshal.Release(BufferAddress);
                BufferAddress = IntPtr.Zero;
                BufferSize = 0;
            }

            BufferAddress = Marshal.AllocHGlobal(imageSize);
            if (BufferAddress == IntPtr.Zero) return;
            BufferSize = (UInt32)imageSize;
            Debug.WriteLine(this.구분.ToString(), "InitBuffers");
        }

        internal void CopyMemory(IntPtr surfaceAddr, Int32 width, Int32 height)
        {
            // 메모리 복사
            lock (this.BufferLock)
            {
                try
                {
                    this.InitBuffers(width, height);
                    Common.CopyMemory(BufferAddress, surfaceAddr, BufferSize);
                }
                catch (Exception e)
                {
                    Global.오류로그(로그영역, "Acquisition", $"[{this.구분.ToString()}] {e.Message}", true);
                }
            }
        }

        internal void AcquisitionFinished(IntPtr surfaceAddr, Int32 width, Int32 height)
        {
            if (surfaceAddr == IntPtr.Zero) { AcquisitionFinished("Failed."); return; }
            try
            {
                if (this.UseMemoryCopy) this.CopyMemory(surfaceAddr, width, height);
                else
                {
                    this.BufferAddress = surfaceAddr;
                    this.ImageWidth = width;
                    this.ImageHeight = height;
                }

                Global.그랩제어.그랩완료(this);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Acquisition", $"[{this.구분}] {ex.Message}", true);
            }
        }

        internal void AcquisitionFinished(String error) =>
            Global.오류로그(로그영역, "Acquisition", $"[{this.구분.ToString()}] {error}", true);
        internal void AcquisitionFinished(Mat image)
        {
            if (image == null) { AcquisitionFinished("Failed."); return; }
            this.Images.Enqueue(image);
            this.Dispose();
            Global.그랩제어.그랩완료(this);
        }

        public ICogImage CogImage()
        {
            try
            {
                if (this.Image != null) return Common.ToCogImage(this.Image);
                if (this.BufferAddress == IntPtr.Zero) return null;
                using (CogImage8Root cogImage8Root = new CogImage8Root())
                {
                    CogImage8Grey image = new CogImage8Grey();
                    cogImage8Root.Initialize(ImageWidth, ImageHeight, BufferAddress, ImageWidth, null);
                    image.SetRoot(cogImage8Root);
                    return image;
                }
            }
            catch (Exception e)
            {
                Global.오류로그(로그영역, "Acquisition", $"[{this.구분.ToString()}] {e.Message}", true);
            }
            return null;
        }
        public Mat MatImage()
        {
            if (this.Image != null) return this.Image;
            if (BufferAddress == IntPtr.Zero) return null;
            return new Mat(ImageHeight, ImageWidth, ImageType, BufferAddress);
        }

        public Mat MergeImages(Mat 좌측이미지, Mat 우측이미지, int x1, int x2, int cropSize = 145)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // 이미지 로드
            using (Bitmap leftImg = 좌측이미지.ToBitmap())
            using (Bitmap rightImg = 우측이미지.ToBitmap())
            {
                // 이미지 크기 확인
                if (leftImg.Height != rightImg.Height)
                    throw new ArgumentException("두 이미지의 높이가 같아야 합니다.");

                // 이미지 자르기
                Rectangle leftRect = new Rectangle(0, cropSize, leftImg.Width, leftImg.Height - cropSize);
                Rectangle rightRect = new Rectangle(0, 0, rightImg.Width, rightImg.Height - cropSize);

                using (Bitmap croppedLeftImg = leftImg.Clone(leftRect, leftImg.PixelFormat))
                using (Bitmap croppedRightImg = rightImg.Clone(rightRect, rightImg.PixelFormat))
                {
                    // 합성할 오른쪽 이미지의 폭 계산
                    int rightWidth = croppedRightImg.Width - x2;

                    // 새로운 이미지 크기 계산
                    int newWidth = Math.Max(croppedLeftImg.Width, x1 + rightWidth);
                    int height = croppedLeftImg.Height;

                    // 합성 이미지 생성
                    using (Bitmap mergedImg = new Bitmap(newWidth, height, PixelFormat.Format8bppIndexed))
                    {
                        // 팔레트 설정 (그레이스케일)
                        ColorPalette palette = mergedImg.Palette;
                        for (int i = 0; i < 256; i++)
                            palette.Entries[i] = Color.FromArgb(i, i, i);
                        mergedImg.Palette = palette;

                        // 이미지 데이터 복사
                        BitmapData mergedData = mergedImg.LockBits(new Rectangle(0, 0, mergedImg.Width, mergedImg.Height), ImageLockMode.WriteOnly, mergedImg.PixelFormat);
                        BitmapData leftData = croppedLeftImg.LockBits(new Rectangle(0, 0, croppedLeftImg.Width, croppedLeftImg.Height), ImageLockMode.ReadOnly, croppedLeftImg.PixelFormat);
                        BitmapData rightData = croppedRightImg.LockBits(new Rectangle(x2, 0, rightWidth, croppedRightImg.Height), ImageLockMode.ReadOnly, croppedRightImg.PixelFormat);

                        unsafe
                        {
                            byte* mergedPtr = (byte*)mergedData.Scan0;
                            byte* leftPtr = (byte*)leftData.Scan0;
                            byte* rightPtr = (byte*)rightData.Scan0;

                            // 왼쪽 이미지 복사
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < croppedLeftImg.Width; x++)
                                {
                                    mergedPtr[x + y * mergedData.Stride] = leftPtr[x + y * leftData.Stride];
                                }
                            }

                            // 오른쪽 이미지 부분 복사 및 합성
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < rightWidth; x++)
                                {
                                    mergedPtr[(x1 + x) + y * mergedData.Stride] = rightPtr[x + y * rightData.Stride];
                                }
                            }
                        }

                        croppedLeftImg.UnlockBits(leftData);
                        croppedRightImg.UnlockBits(rightData);
                        mergedImg.UnlockBits(mergedData);

                        // 결과 이미지 저장
                        //mergedImg.Save(outputPath, ImageFormat.Png);
                        stopwatch.Stop();
                        Debug.WriteLine($"Final image dimensions: {mergedImg.Width}x{mergedImg.Height}");
                        Debug.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");

                        Bitmap bit = new Bitmap(mergedImg);
                        return bit.ToMat();
                    }
                }
            }
        }
        #endregion
    }

    public class EuresysLink : 그랩장치
    {
        [JsonIgnore]
        private CamControl Device = null;

        public EuresysLink(CamControl cam)
        {
            this.구분 = cam.Camera;
            this.Device = cam;
        }

        public override Boolean Init()
        {
            this.Device.Init();
            this.Device.Start();
            this.Device.AcquisitionFinishedEvent += AcquisitionFinishedEvent;
            Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);
            this.상태 = true;
            return this.상태;
        }

        private void AcquisitionFinishedEvent(IntPtr surfaceAddr, Int32 width, Int32 height, String error)
        {
            if (surfaceAddr == IntPtr.Zero) this.AcquisitionFinished(error);
            else this.AcquisitionFinished(surfaceAddr, width, height);
            //this.Stop();
        }

        public override Boolean Stop() => true;
        public override Boolean Close() { base.Close(); this.Device.Free(); return true; }
        public override Boolean Active() { this.Device.Active(); return true; }
    }

    public class ImageDevice : 그랩장치
    {
        public override Boolean Init() => true;
        public override Boolean Active() => true;
        public override Boolean Stop() => true;
        public override void TurnOn() { }
        public override void TurnOff() { }
    }
}
