using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace DVLD
{
    public class clsCryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("65501427771183305900947904142954");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("2212163617609024");

        
        public static string AESEncrypt(string text)
        {
            // return the Encryption Text
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
                        return encryptedBase64;
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return null;
        }

        public static string AESDecrypt(string EncryptionText)
        {
            //return the Text
            try
            {
                
                
                byte[] encryptedBytes = Convert.FromBase64String(EncryptionText);
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
