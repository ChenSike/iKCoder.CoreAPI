using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace iKCoderSDK
{

       

    public class Basic_AES
    {

        private string key = "ikcoder-api-keys";

        public string optionalKey
        {
            set
            {
                key = value;
            }
            get
            {
                return key;
            }
        }

        public string AesEncrypt(string content)
        {

            string key_base64 = Util_Common.Encoder_Base64(key);
            byte[] keyArray = Convert.FromBase64String(key_base64);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(content);

            SymmetricAlgorithm des = Aes.Create();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = des.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }

        public string AesDecrypt(string content)
        {
            string key_base64 = Util_Common.Encoder_Base64(key);
            byte[] keyArray = Convert.FromBase64String(key);
            byte[] toEncryptArray = Convert.FromBase64String(content);

            SymmetricAlgorithm des = Aes.Create();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = des.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }

    }
}
