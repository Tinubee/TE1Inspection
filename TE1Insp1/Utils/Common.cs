﻿using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using DevExpress.Utils.Extensions;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TE1.Schemas
{
    public static class Common
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        public static Double UseMemory() => Process.GetCurrentProcess().WorkingSet64;

        public static Boolean GarbageCollection(Int32 MBytes)
        {
            Double usage = UseMemory() / 1000000;
            Debug.WriteLine(usage, "메모리 사용량");
            if (usage < MBytes) return false;
            GC.Collect();
            return true;
        }

        public static Double StandardDeviation(IEnumerable<Double> data)
        {
            Int32 count = data.Count();
            if (data == null || count < 1) return 0;
            Double mean = data.Average();
            Double sumOfSquaresOfDifferences = data.Select(val => (val - mean) * (val - mean)).Sum();
            Double variance = sumOfSquaresOfDifferences / count;
            return Math.Sqrt(variance);
        }

        public static Double StandardDeviation<T>(IEnumerable<T> data) where T : IConvertible
        {
            Int32 count = data.Count();
            if (data == null || count < 1) return 0;
            Double mean = data.Average(val => val.ToDouble(null));
            Double sumOfSquaresOfDifferences = data.Select(val => (val.ToDouble(null) - mean) * (val.ToDouble(null) - mean)).Sum();
            Double variance = sumOfSquaresOfDifferences / count;
            return Math.Sqrt(variance);
        }

        public static ICogImage8PixelMemory[] GetChannels(ICogImage image)
        {
            if (image == null) return null;
            if (image.GetType() == typeof(CogImage8Grey))
                return new ICogImage8PixelMemory[] { (image as CogImage8Grey).Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height) };
            if (image.GetType() == typeof(CogImage24PlanarColor))
            {
                ICogImage8PixelMemory[] c = new ICogImage8PixelMemory[3];
                (image as CogImage24PlanarColor).Get24PlanarColorPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height, out c[0], out c[1], out c[2]);
                return c;
            }
            return null;
        }

        public static Mat ToMat(ICogImage image)
        {
            //DateTime s = DateTime.Now;
            ICogImage8PixelMemory[] channels = GetChannels(image);
            if (channels == null || channels.Length < 1) return null;
            Mat mat = null;
            if (channels.Length == 1) mat = Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[0].Scan0);
            else
            {
                mat = new Mat(image.Height, image.Width, MatType.CV_8UC3);
                Mat[] mats = new Mat[] {
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[2].Scan0), // B
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[1].Scan0), // G
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[0].Scan0), // R
                };
                Cv2.Merge(mats, mat);
                mats.ForEach(m => m.Dispose());
            }
            channels.ForEach(c => c.Dispose());
            //Debug.WriteLine((DateTime.Now - s).TotalMilliseconds, "ToMat");
            return mat;

            //if (image == null) return null;
            //if (image.GetType() == typeof(CogImage8Grey))
            //{
            //    using (ICogImage8PixelMemory mem = (image as CogImage8Grey).Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height))
            //        return new Mat(image.Height, image.Width, MatType.CV_8UC1, mem.Scan0);
            //}
            //else if (image.GetType() == typeof(CogImage24PlanarColor))
            //{
            //    using (Bitmap bitmap = (image as CogImage24PlanarColor)?.ToBitmap())
            //        return BitmapConverter.ToMat(bitmap);
            //}
        }

        public static ICogImage ToCogImage(Mat mat)
        {
            if (mat == null) return null;
            if (mat.Type() == MatType.CV_8UC3)
                return new CogImage24PlanarColor(mat.ToBitmap());
            CogImage8Grey image = new CogImage8Grey();
            using (CogImage8Root cogImage8Root = new CogImage8Root())
            {
                cogImage8Root.Initialize(mat.Width, mat.Height, mat.Ptr(0), mat.Width, null);
                image.SetRoot(cogImage8Root);
            }
            return image;
        }

        public static Mat ConcatHorizontal(Mat image1, Mat image2, Int32 padding = 0)
        {
            if (image1 == null || image2 == null) return null;
            Mat image = new Mat(Math.Max(image1.Height, image2.Height), image1.Width + image2.Width, MatType.CV_8UC1);
            Cv2.HConcat(image1, image2, image);
            if (padding <= 0) return image;
            Mat padded = new Mat(image.Rows + padding * 2, image.Cols + padding * 2, MatType.CV_8UC1, Scalar.Black);
            Rect region = new Rect(padding, padding, image.Width, image.Height);
            padded[region] = image;
            image.Dispose();
            return padded;
        }

        public static Mat CopyRectangle(Mat image, Rect region, Color background) =>
            CopyRectangle(image, region, ToScalar(background));
        public static Mat CopyRectangle(Mat image, Rect region, Scalar background)
        {
            if (image == null) return null;
            Rect source = region.Intersect(new Rect(0, 0, image.Width, image.Height));
            Rect target = new Rect(0, 0, source.Width, source.Height);
            if (region.X < 0) target.X -= region.X;
            if (region.Y < 0) target.Y -= region.Y;
            //Debug.WriteLine($"region={region}, source={source}, target={target}");
            Mat output = new Mat(region.Size, image.Type(), background);
            image[source].CopyTo(output[target]);
            return output;
        }

        public static Mat Resize(Mat source, Double scale) => Resize(source, scale, scale);
        public static Mat Resize(Mat source, Double scaleX, Double scaleY) =>
            source.Resize(new OpenCvSharp.Size(source.Cols * scaleX, source.Rows * scaleY));

        public static void DrawRotatedRectangle(Mat image, Point2f[] points, Scalar color, Int32 thickness)
        {
            for (Int32 i = 0; i < 4; ++i)
                image.Line(points[i].ToPoint(), points[(i + 1) % 4].ToPoint(), color, thickness);
        }

        public static void ShowImage(Mat image, String name)
        {
            Cv2.NamedWindow(name, WindowFlags.AutoSize);
            Cv2.ImShow(name, image);
        }

        public static Double ToRadian(Double degree) => degree / 180.0 * Math.PI;
        public static Double ToDegree(Double radian) => radian / Math.PI * 180.0;
        public static Scalar ToScalar(Color color) => Scalar.FromRgb(color.R, color.G, color.B);

        public static String GetImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.bmp; *.png; *.jpg; *.jpeg)|*.bmp; *.png; *.jpg; *.jpeg";
                try { if (openFileDialog.ShowDialog() != DialogResult.OK) return String.Empty; }
                catch (Exception ex) { Debug.WriteLine(ex.Message); return String.Empty; }
                return openFileDialog.FileName;
            }
        }

        public static String[] GetImageFiles()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.bmp; *.png; *.jpg; *.jpeg)|*.bmp; *.png; *.jpg; *.jpeg";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return new string[] { };
                return openFileDialog.FileNames;
            }
        }

        public static String SaveImagePath(String fileName = "")
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                //saveFileDialog.Filter = "Bitmap File(*.bmp) | *.bmp |PNG File(*.png) | *.png |JPG File(*.jpg)| *.jpg";
                saveFileDialog.Filter = "PNG File(*.png) | *.png";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.FileName = fileName;
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return String.Empty;
                return saveFileDialog.FileName;
            }
        }

        public static CogImage8Grey LoadImage(IntPtr sufaceAddress, Int32 width, Int32 height)
        {
            using (CogImage8Root cogImage8Root = new CogImage8Root())
            {
                cogImage8Root.Initialize(width, height, sufaceAddress, width, null);
                CogImage8Grey image = new CogImage8Grey();
                image.SetRoot(cogImage8Root);
                return image;
            }
        }

        public static ICogImage LoadImage()
        {
            String path = GetImageFile();
            if (String.IsNullOrEmpty(path)) return null;
            return LoadImage(path);
        }

        public static ICogImage LoadImage(String path, Boolean Colored = false)
        {
            if (!File.Exists(path)) return null;
            try
            {
                using (CogImageFileTool imageTool = new CogImageFileTool())
                {
                    imageTool.Operator.Open(path, CogImageFileModeConstants.Read);
                    imageTool.Run();
                    return imageTool.OutputImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
                }
                // 대용량 이미지 로드 못함
                //using (Bitmap bitmap = new Bitmap(path))
                //{
                //    if (Colored) return new CogImage24PlanarColor(bitmap);
                //    return new CogImage8Grey(bitmap);
                //}
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "LoadImage"); }
            return null;
        }

        public static Boolean ImageSaveAs(Bitmap bitmap, out String error, Int32 compression = 3) => ImageSaveAs(bitmap, String.Empty, out error, compression);
        public static Boolean ImageSaveAs(Bitmap bitmap, String fileName, out String error, Int32 compression = 3)
        {
            error = String.Empty;
            if (bitmap == null) return false;
            String path = SaveImagePath(fileName);
            if (String.IsNullOrEmpty(path)) return false;
            using (Mat mat = BitmapConverter.ToMat(bitmap))
                return ImageSavePng(mat, path, out error, compression);
        }
        public static Boolean ImageSaveAs(CogImage8Grey image, String fileName, out String error, Int32 compression = 3)
        {
            error = String.Empty;
            if (image == null) return false;
            String path = SaveImagePath(fileName);
            if (String.IsNullOrEmpty(path)) return false;
            return ImageSavePng(image, path, out error, compression);
        }

        public static Boolean ImageSavePng(Mat image, String fileName, out String error, Int32 compression = 3)
        {
            error = String.Empty;
            if (image == null) return false;
            try
            {
                ImageEncodingParam[] @params = new ImageEncodingParam[] {
                    new ImageEncodingParam(ImwriteFlags.PngCompression, compression), // compression: 0에서 9까지의 값 중 선택
                };
                return image.SaveImage(fileName, @params);
            }
            catch (Exception ex) { error = ex.Message; }
            return false;
        }

        public static Boolean ImageSavePng(ICogImage image, String fileName, out String error, Int32 compression = 3)
        {
            error = String.Empty;
            if (image == null) return false;
            try
            {
                using (Mat mat = ToMat(image))
                    return ImageSavePng(mat, fileName, out error, compression);
            }
            catch (Exception ex) { error = ex.Message; }
            return false;
        }

        //ImwriteFlags.JpegQuality
        //  JPEG 이미지의 품질을 지정합니다.이 옵션을 사용하여 이미지의 압축 레벨을 설정할 수 있습니다.
        //  값의 범위는 0부터 100까지이며, 100은 최상의 품질을 나타냅니다. 값이 높을수록 이미지 품질이 높아지지만 파일 크기도 커집니다.
        //ImwriteFlags.JpegProgressive
        //  JPEG 이미지를 프로그레시브 형식으로 저장할지 여부를 결정합니다.
        //  프로그레시브 JPEG 이미지는 다운로드 및 표시 과정에서 점진적으로 더 높은 품질의 이미지를 표시할 수 있도록 합니다.
        //ImwriteFlags.JpegOptimize
        //  JPEG 이미지를 최적화하여 저장할지 여부를 결정합니다. 최적화된 JPEG 이미지는 파일 크기를 줄이고 품질을 유지합니다.
        //ImwriteFlags.JpegRstInterval
        //  JPEG 이미지의 재설정 (RST) 마커를 삽입할 간격을 설정합니다.이 값은 0부터 65535까지의 정수로 지정됩니다.
        //  RST 마커는 이미지를 더 작은 블록으로 나누는 데 사용되며, 재복구 및 디코딩에 도움이 됩니다.
        public static Boolean ImageSaveJpg(Mat image, String fileName, out String error, Int32 quality = 90)
        {
            error = String.Empty;
            if (image == null) return false;
            try
            {
                ImageEncodingParam[] @params = new ImageEncodingParam[] {
                    new ImageEncodingParam(ImwriteFlags.JpegQuality, quality),
                    new ImageEncodingParam(ImwriteFlags.JpegOptimize, 1),
                };
                return image.SaveImage(fileName, @params);
            }
            catch (Exception ex) { error = ex.Message; }
            return false;
        }
        public static Boolean ImageSaveJpeg(Mat image, String fileName, out String error, Int32 quality = 90) => ImageSaveJpg(image, fileName, out error, quality);

        public static Boolean ImageSaveJpg(ICogImage image, String fileName, out String error, Int32 quality = 90)
        {
            using (Mat mat = ToMat(image))
                return ImageSaveJpg(mat, fileName, out error, quality);

            // 저장 속도가 쓰레기임
            //try
            //{
            //    CogImageFileJPEG jpeg = new CogImageFileJPEG() { Quality = quality };
            //    jpeg.Open(fileName, CogImageFileModeConstants.Write);
            //    jpeg.Append(image);
            //    jpeg.Close();
            //    error = String.Empty;
            //    return true;
            //}
            //catch (Exception ex) { error = ex.Message; return false; }
        }

        public static Boolean ImageSaveBmp(Mat image, String fileName, out String error)
        {
            error = String.Empty;
            if (image == null) return false;
            try { return image.SaveImage(fileName); }
            catch (Exception ex) { error = ex.Message; }
            return false;
        }

        public static Boolean ImageSaveBmp(ICogImage image, String fileName, out String error)
        {
            error = String.Empty;
            if (image == null) return false;
            try
            {
                using (CogImageFileBMP BmpImage = new CogImageFileBMP())
                {
                    BmpImage.Open(fileName, CogImageFileModeConstants.Write);
                    BmpImage.Append(image);
                    BmpImage.Close();
                }
                return true;
            }
            catch (Exception ex) { error = ex.Message; }
            return false;
        }

        public static String CreateDirectory(List<String> dirs)
        {
            try
            {
                String path = String.Empty;
                foreach (String dir in dirs)
                {
                    if (String.IsNullOrEmpty(path)) path = dir;
                    else path = Path.Combine(path, dir);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                }
                return path;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return String.Empty;
        }

        public static bool DirectoryExists(string Path)
        {
            return DirectoryExists(Path, false);
        }

        public static bool DirectoryExists(string Path, bool Create)
        {
            try
            {
                if (Directory.Exists(Path))
                    return true;

                Directory.CreateDirectory(Path);
                return Directory.Exists(Path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "DirectoryExists");
                return false;
            }
        }

        public static Boolean Ping(String Host)
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions() { DontFragment = true };
            try
            {
                // Create a buffer of 32 bytes of data to be transmitted.
                PingReply reply = pingSender.Send(Host, 1000, Encoding.ASCII.GetBytes("TEST"), options);
                Debug.WriteLine($"PingTest {Host}[{reply.Status}]");
                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
    }

    public class MyWatch : Stopwatch
    {
        private string Title = String.Empty;
        public MyWatch(String title)
        {
            this.Title = title;
            this.Start();
        }

        public void Stop(String Tag, Boolean Restart = true)
        {
            this.Stop();
            Debug.WriteLine($"{this.Title}: {Tag} => {this.ElapsedMilliseconds}ms");
            if (Restart)
            {
                this.Reset();
                this.Start();
            }
        }

        public void Print(String contents)
        {
            Debug.WriteLine($"{this.Title}: {contents}");
        }
    }

    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
