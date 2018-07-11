using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace iKCoderComps
{
    public static class FileStore
    {
		static FileStore()
		{
			if (!Directory.Exists("storepool"))
			{
				Directory.CreateDirectory("storepool");
			}		
		}

		public static string GetUerStoreName(string userid)
		{
			return "u" + "_" + userid;
		}

		public static void VerifyUserStorItem(string userid)
		{
			if (!Directory.Exists("storepool\\" + GetUerStoreName(userid)))
				Directory.CreateDirectory("\\storepool\\" + GetUerStoreName(userid));
			if (!Directory.Exists("storepool\\" + GetUerStoreName(userid) + "\\images"))
				Directory.CreateDirectory("storepool\\" + GetUerStoreName(userid) + "\\images");
			if (!Directory.Exists("storepool\\" + GetUerStoreName(userid) + "\\data"))
				Directory.CreateDirectory("storepool\\" + GetUerStoreName(userid) + "\\data");
		}

		public static string GetImageStore(string userid)
		{
			return "storepool\\" + GetUerStoreName(userid) + "\\images";
		}

		public static string GetDataStore(string userid)
		{
			return "storepool\\" + GetUerStoreName(userid) + "\\data";
		}

    }
}
