using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MessageHelper
{
    public class MessageHelper
    {
        public static string ExecuteSucessful()
        {
            return "{ \"executed:\":\"true\"}";
        }

        public static string ExecuteSucessful(string MSGCODE,string MSG)
        {
            return "{ \"executed:\":\"true\",\"msgcode\":\"" + MSGCODE + "\"," + "\"msg\":" + MSG + "\"}";
        }

        public static string ExecuteFalse()
        {
            return "{ \"executed:\":\"false\"}";
        }

        public static string ExecuteFalse(string MSGCODE, string MSG)
        {
            return "{ \"executed:\":\"false\",\"msgcode\":\"" + MSGCODE + "\"," + "\"msg\":" + MSG + "\"}";
        }

        public static string AssetExecute(bool result)
        {
            if(result)
                return "{ \"executed:\":\"true\"}";
            else
                return "{ \"executed:\":\"false\"}";
        }

        

    }
}
