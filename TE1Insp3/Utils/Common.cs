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

namespace TE1
{
    //public enum EXECUTION_STATE : uint
    //{
    //    ES_AWAYMODE_REQUIRED = 0x00000040,
    //    ES_CONTINUOUS = 0x80000000,
    //    ES_DISPLAY_REQUIRED = 0x00000002,
    //}

    public static class Common
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        //public static void Prevent_Screensaver(bool enable)
        //{
        //    if (enable) SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        //    else SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        //}

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

        public static String GetImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.bmp; *.png; *.jpg; *.jpeg)|*.bmp; *.png; *.jpg; *.jpeg";
                if (openFileDialog.ShowDialog() != DialogResult.OK) return String.Empty;
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

        public static Boolean DirectoryExists(String path) => DirectoryExists(path, false);
        public static Boolean DirectoryExists(String path, Boolean Create)
        {
            try
            {
                if (Directory.Exists(path)) return true;
                if (Create) Directory.CreateDirectory(path);
                return Directory.Exists(path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "DirectoryExists");
                return false;
            }
        }

        public static Boolean Ping(String host)
        {
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions() { DontFragment = true };
            try
            {
                // Create a buffer of 32 bytes of data to be transmitted.
                PingReply reply = pingSender.Send(host, 1000, Encoding.ASCII.GetBytes("TEST"), options);
                Debug.WriteLine($"PingTest {host}[{reply.Status}]");
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
