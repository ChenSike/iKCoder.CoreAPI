using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iKCoder_Platform_SDK_Kit;

namespace CoreRun
{
    class Program
    {

        static void Main(string[] args)
        {

            ConsoleMessage objectConsoleMessage = new ConsoleMessage();

            string command = string.Empty;
            Console.WriteLine("Corerun 1.0 2018 copyright by iKCoder.LTD.ShenZhen.");
            string str_server = string.Empty;
            string str_uid = string.Empty;
            string str_db = string.Empty;
            string str_password = string.Empty;
            Console.Write("DB Server:");
            str_server = Console.ReadLine();
            Console.Write("DB Uid:");
            str_uid = Console.ReadLine();
            Console.Write("DB Pwd:");
            str_password = Console.ReadLine();
            Console.Write("DB:");
            str_db = Console.ReadLine();
            class_Data_SqlConnectionHelper objectConnectionHelper = new class_Data_SqlConnectionHelper();
            objectConnectionHelper.Set_NewConnectionItem("newconnection", str_server, str_uid, str_password, str_db, enum_DatabaseType.MySql);
            class_Data_SqlHelper objectSqlHelper = new class_Data_SqlHelper();
            objectSqlHelper.ActionAutoCreateSPS(objectConnectionHelper.Get_ActiveConnection("newconnection"));
            Console.WriteLine("Finish job");
            /*Console.WriteLine("Init Services:");
            objectConsoleMessage.start_process();
            while (true)
            {
                Console.Write(">");
                command = Console.ReadLine();            
            }
            */
        }
    }
}
