using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace iKCoderSDK
{

	public class Basic_AES
    {

		

        private string key = "ikcoderA";

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

		public  string AesEncrypt(string input)
		{

			//byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
			//byte[] passwordBytes = Encoding.UTF8.GetBytes(key);

			//passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			//byte[] bytesEncrypted = AESEncryptBytes(bytesToBeEncrypted, passwordBytes);

			//string result = Convert.ToBase64String(bytesEncrypted);

			//return result;
			return "";

		}

		public string AesDecrypt(string content)
        {
            string key_base64 = Util_Common.Encoder_Base64(key);
            byte[] keyArray = Convert.FromBase64String(key_base64);
            byte[] toEncryptArray = Convert.FromBase64String(content);

            SymmetricAlgorithm des = Aes.Create();
            des.Key = keyArray;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = des.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
		

    }
}
