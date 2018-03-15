using System;

namespace CoreInit
{
    class Program
    {
        static void Main(string[] args)
        {
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
                
            }
            
        }
    }
}
