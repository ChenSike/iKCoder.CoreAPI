using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace iKCoderSDK
{
    public class Basic_DES
    {
        public int bufferLength = 4096;
        private const string iv = "1234";
        private string key = "ASDFGHJK";
        private Encoding MyEncoding = new UnicodeEncoding();        
        private SymmetricAlgorithm provider = SymmetricAlgorithm.Create("TripleDES");

        public string DesEncrypt(string input)
        {

            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            tripleDES.Key = byteKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DesDecrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            tripleDES.Key = byteKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        public Basic_DES(string OptionalKey)
        {
            try
            {
                this.key = OptionalKey;
                this.provider.Key = this.MyEncoding.GetBytes(this.key);
                this.provider.IV = this.MyEncoding.GetBytes("1234");
            }
            catch
            {
                this.key = "ASDFGHJK";
                this.provider.Key = this.MyEncoding.GetBytes(this.key);
                this.provider.IV = this.MyEncoding.GetBytes("1234");
            }
        }

        public Basic_DES()
        {
            try
            {
                this.provider.Key = this.MyEncoding.GetBytes(this.key);
                this.provider.IV = this.MyEncoding.GetBytes("1234");
            }
            catch
            {
                this.key = "ASDFGHJK";
                this.provider.Key = this.MyEncoding.GetBytes(this.key);
                this.provider.IV = this.MyEncoding.GetBytes("1234");
            }
        }
    }
}
