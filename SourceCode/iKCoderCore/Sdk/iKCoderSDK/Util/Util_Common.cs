using System;
using System.Collections.Generic;
using System.Text;

namespace iKCoderSDK
{
    public class Util_Common
    {
        public static string Encoder_Base64(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            else
            {
                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    string str = Convert.ToBase64String(bytes);
                    return str;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
