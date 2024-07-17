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
using Cognex.VisionPro.ImageFile;
using System.Windows.Media.Media3D;

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
        internal Int32 mergeImageWidth = 0;
        [JsonIgnore]
        internal Int32 mergeImageHeight = 0;
        [JsonIgnore]
        internal Object BufferLock = new Object();
        [JsonIgnore]
        internal UInt32 BufferSize = 0;
        [JsonIgnore]
        internal IntPtr BufferAddress = IntPtr.Zero;
        [JsonIgnore]
        internal IntPtr mergeBufferAddress = IntPtr.Zero;
        [JsonIgnore]
        internal Queue<Mat> Images = new Queue<Mat>();
        [JsonIgnore]
        internal Mat Image => Images.LastOrDefault<Mat>();
        [JsonIgnore]
        internal Queue<Mat> 합성이미지들 = new Queue<Mat>();
        [JsonIgnore]
        internal Mat 합성이미지 => 합성이미지들.LastOrDefault<Mat>();
        [JsonIgnore]
        public const String 로그영역 = "Camera";

        public void Dispose()
        {
            while (this.Images.Count > 3)
                this.Dispose(this.Images.Dequeue());
        }
        public void MergedImageDispose()
        {
            while (this.합성이미지들.Count > 1)
                this.Dispose(this.합성이미지들.Dequeue());
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
            while (this.합성이미지들.Count > 0)
                this.Dispose(this.합성이미지들.Dequeue());
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
                this.MergedImageDispose();
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
        public ICogImage MergeCogImage()
        {
            return Common.ToCogImage(this.합성이미지);
            //try
            //{
            //    //if (this.Image != null) return Common.ToCogImage(this.Image);
            //    if (this.mergeBufferAddress == IntPtr.Zero) return null;
            //    using (CogImage8Root cogImage8Root = new CogImage8Root())
            //    {
            //        CogImage8Grey image = new CogImage8Grey();
            //        cogImage8Root.Initialize(mergeImageWidth, mergeImageHeight, mergeBufferAddress, mergeImageWidth, null);
            //        image.SetRoot(cogImage8Root);
            //        return image;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Global.오류로그(로그영역, "Acquisition", $"[{this.구분.ToString()}] {e.Message}", true);
            //}
            //return null;
        }
        public Mat MatImage()
        {
            if (this.Image != null) return this.Image;
            if (BufferAddress == IntPtr.Zero) return null;
            return new Mat(ImageHeight, ImageWidth, ImageType, BufferAddress);
        }

        public Mat MergeImage()
        {
            if (this.Image != null) return this.Image;
            if (BufferAddress == IntPtr.Zero) return null;
            return new Mat(mergeImageHeight, mergeImageWidth, ImageType, mergeBufferAddress);
        }

        public void MergeImages(Mat 좌측이미지, Mat 우측이미지, int LeftX, int RightX, int cropSize = 145)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Double leftAngle = 0.279972;
            Double rightAngle = 0.216211;

            Console.WriteLine("Processing left image:");
            Mat leftProcessed = ProcessImage(좌측이미지, leftAngle);

            Console.WriteLine("\nProcessing right image:");
            Mat rightProcessed = ProcessImage(우측이미지, rightAngle);
            //Stopwatch stopwatch = Stopwatch.StartNew();
            Mat mergedImage = 이미지합성(leftProcessed, rightProcessed, LeftX, RightX);
            this.합성이미지들.Enqueue(mergedImage);
            stopwatch.Stop();
            Console.WriteLine($"Total processing time: {stopwatch.ElapsedMilliseconds} ms");
        }
        public Mat 이미지합성(Mat leftImg, Mat rightImg, int leftX, int rightX)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Mat leftImgCropped = CropImage(leftImg, true);
            Mat rightImgCropped = CropImage(rightImg, false);

            int mergedWidth = leftX + (rightImgCropped.Cols - rightX);
            int mergedHeight = Math.Max(leftImgCropped.Rows, rightImgCropped.Rows);

            Mat mergedImg = new Mat(mergedHeight, mergedWidth, MatType.CV_8UC1, Scalar.Black);

            // Define the rows and columns for left image cropping
            int leftStartRow = 0;
            int leftEndRow = leftImgCropped.Rows;
            int leftStartCol = 0;
            int leftEndCol = leftX;

            // Define the rows and columns for merging left image into merged image
            int mergeLeftStartRow = 0;
            int mergeLeftEndRow = mergedImg.Rows;
            int mergeLeftStartCol = 0;
            int mergeLeftEndCol = leftX;

            // Copy left image to merged image
            leftImgCropped.SubMat(leftStartRow, leftEndRow, leftStartCol, leftEndCol)
                .CopyTo(mergedImg.SubMat(mergeLeftStartRow, mergeLeftEndRow, mergeLeftStartCol, mergeLeftEndCol));

            // Define the rows and columns for right image cropping
            int rightStartRow = 0;
            int rightEndRow = rightImgCropped.Rows;
            int rightStartCol = rightX;
            int rightEndCol = rightImgCropped.Cols;

            // Define the rows and columns for merging right image into merged image
            int mergeRightStartRow = 0;
            int mergeRightEndRow = mergedImg.Rows;
            int mergeRightStartCol = leftX;
            int mergeRightEndCol = mergedImg.Cols;

            // Copy right image to merged image
            rightImgCropped.SubMat(rightStartRow, rightEndRow, rightStartCol, rightEndCol)
                .CopyTo(mergedImg.SubMat(mergeRightStartRow, mergeRightEndRow, mergeRightStartCol, mergeRightEndCol));

            stopwatch.Stop();
            Console.WriteLine($"Merge time: {stopwatch.ElapsedMilliseconds} ms");

            return mergedImg;
        }
        public Mat CropImage(Mat image, bool isLeft)
        {
            int startRow = isLeft ? 42 : 0;
            int endRow = isLeft ? image.Rows : image.Rows - 42;
            int startCol = 0;
            int endCol = image.Cols;

            return image.SubMat(startRow, endRow, startCol, endCol);
            //return isLeft ? image[42.., ..] : image[..^42, ..];
        }
        public Mat ProcessImage(Mat Image, Double angle)
        {
            Double scaleFactor = 0.5;
            Mat resizedImg = ResizeImage(Image, scaleFactor);
            Mat rotatedImg = RotateImage(resizedImg, angle);
            return rotatedImg;
        }

        public Mat ResizeImage(Mat image, double scaleFactor)
        {
            var newSize = new OpenCvSharp.Size(image.Width * scaleFactor, image.Height * scaleFactor);
            return image.Resize(newSize, 0, 0, InterpolationFlags.Area);
        }
        public Mat RotateImage(Mat image, double angle)
        {
            Point2f center = new Point2f(image.Cols / 2f, image.Rows / 2f);
            Mat rotMatrix = Cv2.GetRotationMatrix2D(center, angle, 1.0);
            Mat rotated = new Mat();
            Cv2.WarpAffine(image, rotated, rotMatrix, image.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);
            return rotated;
        }
    }
    #endregion
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
