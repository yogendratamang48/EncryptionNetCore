using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
namespace EncryptionTest
{
    public  class EncryptionConfig
    {
        public  string passPhrase { get; set; }
        public  string saltValue { get; set; }
        public  string hashAlgorithm { get; set; }
        public  string passwordIterations { get; set; }
        public  string initVector { get; set; }
        public  string keySize { get; set; }
        public  string key { get; set; }
        
    }
}