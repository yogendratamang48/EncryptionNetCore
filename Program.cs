using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
namespace EncryptionTest
{
        class Program
    {
        static void Main(string[] args)
        {
            // var config=new EncryptionHelper();
            // Console.WriteLine($"Key:{config.secretKey}");
            // Console.WriteLine($"IV:{config.initVector}");
            MainEncryptionTest();
         
        }
       
        public static void MainEncryptionTest()
        {
             try
            {
               Console.WriteLine("Enter your code: ");
               var content=Console.ReadLine();
               EncryptionHelper helper=new EncryptionHelper();
               var encrypted=helper.EncryptString(content);
               Console.WriteLine("Encrypted Text: {0}", encrypted);
               var decrypted=helper.DecryptString(encrypted);
               Console.WriteLine("Decrypted Message: {0}", decrypted);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        
       
        }
}
