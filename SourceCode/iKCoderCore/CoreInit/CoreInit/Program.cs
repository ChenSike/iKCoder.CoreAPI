using System;
using iKCoderSDK;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CoreInit
{
		
	class Program
    {
				
		static void Main(string[] args)
        {

			
			Basic_Config basic_Config = new Basic_Config();
			basic_Config.CreateNewConfigDocument("test.xml");
			basic_Config.CreateNewSession("test","");
			basic_Config.SwitchToAESModeON();
			basic_Config.SetSessionAttr("test", "attr", "12345678");
			basic_Config.DoSave();
			/*
            ConsoleMessage objectConsoleMessage = new ConsoleMessage();
            string command = string.Empty;
            Console.WriteLine("Corerun 1.0 2018 copyright by iKCoder.LTD.ShenZhen.");
            while(true)
            {
                Console.Write("C:");
                command = Console.ReadLine();
                if (command == "quit")
                {
                    Console.WriteLine("Bye,bye.");
                    Environment.Exit(0);
                    break;
                }
                else
                {
                    R_Base rounterRef = null;
                    if (command == "M_DB")
                        rounterRef = new R_DB();
                    else if (command == "M_Config")
                        rounterRef = new R_Config();
                    if (rounterRef != null)
                    {
                        string subCommand = string.Empty;
                        while (true)
                        {
                            Console.Write("SC:");
                            subCommand = Console.ReadLine();
                            if (subCommand == "break")
                                break;
                            rounterRef.Process(subCommand);
                        }
                    }
                    else
                    {
                        Console.WriteLine("R:@Can not load the module.");
                    }
                }
                
            }*/

		}
    }
}
