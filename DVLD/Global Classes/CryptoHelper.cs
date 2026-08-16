using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace DVLD
{
    public class CryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("65501427771183305900947904142954");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("2212163617609024");

        
        public static bool EncryptAndSaveToFile(string text,string FilePath)
        {
            try
            {
                using (var aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;

                    byte[] textBytes = Encoding.UTF8.GetBytes(text);
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    {
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        string encryptedBase64 = Convert.ToBase64String(encryptedBytes);
                        File.WriteAllText(FilePath, encryptedBase64);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        public static string DecryptFromFile(string FilePath)
        {
            try
            {
                if(!File.Exists(FilePath))
                    return null;
                string SavedBase64 = File.ReadAllText(FilePath);
                byte[] encryptedBytes = Convert.FromBase64String(SavedBase64);
                using (var aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }
    }
}
