using System;
using System.Collections.Generic;
using System.Text;
using iKCoderSDK;
using System.Xml;

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

        public string DESKey
        {
            set;
            get;
        }

        public override void Input()
        {
            Console.Write("M_Config@application configdoc file:");
            this.AppConfigFile = Console.ReadLine();
            Console.Write("M_Config@application DES Key(Enter for default):");
            this.DESKey = Console.ReadLine();
        }

        public bool init()
        {
            return configDoc.DoOpen(this.AppConfigFile);
        }

        public void DESMode()
        {
            if (!string.IsNullOrEmpty(this.DESKey))
            {
                configDoc.SwitchToDESModeON(this.DESKey);
            }
            else
            {
                configDoc.SwitchToDESModeON();
            }
        }

        public void NormalMode()
        {
            configDoc.SwitchToDESModeOFF();
        }

        public void Save()
        {
            configDoc.DoSave();
        }

        public void NewSession(string sessionname,string sessionvalue)
        {
            configDoc.CreateNewSession(sessionname, sessionvalue);
            configDoc.DoSave();
        }

        public void RemoveSession(string sessionname)
        {
            configDoc.RemoveSession(sessionname);
            configDoc.DoSave();
        }

        public string GetSessionValue(string sessionname)
        {
            return configDoc.GetSessionValue(sessionname);
        }

        public void SetSessionAttr(string sessionname,string attrname,string attrvalue)
        {
            configDoc.SetSessionAttr(sessionname, attrname, attrvalue);
            configDoc.DoSave();
        }

        public string GetSessionAttrValue(string sessionname,string attrname)
        {
            XmlNode sessionNode = configDoc.GetSessionNode(sessionname);
            return configDoc.GetAttrValue(sessionNode, attrname);
        }

        public void NewSessionSubItem(string sessionname,string itemname,string itemvalue)
        {
            XmlNode sessionNode = configDoc.GetSessionNode(sessionname);
            configDoc.CreateItem(sessionNode, itemname, itemvalue);
            configDoc.DoSave();
        }

        public string GetSessionItemValue(string sessionname,string itemname)
        {
            return configDoc.GetItemValue(sessionname, itemname);
        }

    }
}
