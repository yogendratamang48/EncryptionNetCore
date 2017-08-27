using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace EncryptionTest
{
    public class EncryptionHelper
    {
        private EncryptionConfig config=null;
        public  EncryptionHelper(string jsonFile="encryptConfig.json", string section="EncryptionConfig")
        {
            config=new EncryptionConfigLoad().LoadEncryptionConfigFromJson(jsonFile, section);
       
        }
         
        public  string EncryptString(string plainText)
        {
            var Key = Encoding.ASCII.GetBytes(config.key);
            var IV = Encoding.ASCII.GetBytes(config.initVector);
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
        public  string DecryptString(string encrypted)
        {
            var Key = Encoding.ASCII.GetBytes(config.key);
            var IV = Encoding.ASCII.GetBytes(config.initVector);
            var cipherText = Convert.FromBase64String(encrypted);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public  string HashString(string password)
        {
          
            var saltConverted = Encoding.UTF8.GetBytes(config.saltValue);
            var iterations = Convert.ToInt32(config.passwordIterations);
          
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltConverted,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: iterations,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
