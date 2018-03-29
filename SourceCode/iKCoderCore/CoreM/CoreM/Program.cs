using System;
using iKCoderSDK;
using System.Collections.Generic;

namespace CoreM
{
    class Program
    {

        static void Main(string[] args)
        {
            iKCoderSDK.Basic_Config coreMconfig = new Basic_Config();
            coreMconfig.DoOpen("coreMconfig.xml");
            Dictionary<string, Dictionary<string, string>> Map_ApiConfigs = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<String, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();
            class_Data_SqlConnectionHelper db_objectConnectionHelper = new class_Data_SqlConnectionHelper();
            Data_dbSqlHelper db_objectSqlHelper = new Data_dbSqlHelper();
            ConsoleMessage obj_message = new ConsoleMessage();
            obj_message.start_process();
            Console.WriteLine("CoreM 1.0 2018 copyright by iKCoder.LTD.ShenZhen.");
            Console.WriteLine("Start services...");
            Console.WriteLine("[CoreM will keep running until this process closed.]");

        }
    }
}
