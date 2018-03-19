using System;
using System.Collections.Generic;
using System.Text;

namespace CoreInit
{
    public abstract class R_Base
    {

        public void assetResult(bool bResult)
        {
            if (bResult)
                Console.WriteLine("R:@True");
            else
                Console.WriteLine("R:@False");
        }

        public void invalidateCommand()
        {
            Console.WriteLine("I:@Invalidated Command.");
        }

        public string[] splitCommand(string command)
        {
            string[] result = command.Split("$");
            return result;
        }

        public void displayResult(string result)
        {
            Console.WriteLine("R:@" + result);
        }

        public abstract void Process(string commnad);
        
    }
}
