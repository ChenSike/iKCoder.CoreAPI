using System;
using System.Collections.Generic;
using System.Text;
using iKCoderSDK;

namespace CoreInit
{
    public class M_Config:M_Base
    {
        private Basic_Config configDoc = new Basic_Config();

        public string AppConfigFile
        {
            set;
            get;
        }

        public override void Input()
        {
            Console.Write("M_Config@application configdoc file:");
            this.AppConfigFile = Console.ReadLine();
        }

        public bool init()
        {
            return configDoc.DoOpen(this.AppConfigFile);
        }

        public void DESMode()
        {
            configDoc.SwitchToDESModeON();
        }

        public void NormalMode()
        {
            configDoc.SwitchToDESModeOFF();
        }

        public bool newSession(string sessionname,string sessionvalue)
        {
            configDoc.CreateNewSession()
        }
        

    }
}
