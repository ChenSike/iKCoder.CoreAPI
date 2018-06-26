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

        public string AESKey
        {
            set;
            get;
        }

        public override void Input()
        {
            Console.Write("M_Config@application configdoc file:");
            this.AppConfigFile = Console.ReadLine();            
            Console.Write("M_Config@application DES Key(Enter for default):");
            this.AESKey = Console.ReadLine();
        }

        public bool init()
        {            
            return configDoc.DoOpen(this.AppConfigFile);
        }

        public void AESMode()
        {
            if (!string.IsNullOrEmpty(this.AESKey))
            {
                configDoc.SwitchToAESModeON(this.AESKey);
            }
            else
            {
                configDoc.SwitchToAESModeON();
            }
        }

        public void NormalMode()
        {
            configDoc.SwitchToAESModeOFF();
        }

        public bool Save()
        {
            return configDoc.DoSave();
        }

        public bool NewDocument()
        {
            return configDoc.CreateNewConfigDocument(this.AppConfigFile);
        }

        public bool NewSession(string sessionname,string sessionvalue)
        {
            configDoc.CreateNewSession(sessionname, sessionvalue);
            return configDoc.DoSave();
        }

        public bool RemoveSession(string sessionname)
        {
            configDoc.RemoveSession(sessionname);
            return configDoc.DoSave();
        }

        public string GetSessionValue(string sessionname)
        {
            return configDoc.GetSessionValue(sessionname);
        }

        public bool SetSessionAttr(string sessionname,string attrname,string attrvalue)
        {
            configDoc.SetSessionAttr(sessionname, attrname, attrvalue);
            return configDoc.DoSave();
        }

        public string GetSessionAttrValue(string sessionname,string attrname)
        {
            XmlNode sessionNode = configDoc.GetSessionNode(sessionname);
            return configDoc.GetAttrValue(sessionNode, attrname);
        }

        public bool NewSessionSubItem(string sessionname,string itemname,string itemvalue)
        {
            XmlNode sessionNode = configDoc.GetSessionNode(sessionname);
            configDoc.CreateItem(sessionNode, itemname, itemvalue);
            return configDoc.DoSave();
        }

        public string GetSessionItemValue(string sessionname,string itemname)
        {
            return configDoc.GetItemValue(sessionname, itemname);
        }

		public string GetFullSession(string sessionname)
		{
			Dictionary<string, string> activeSessionMap = new Dictionary<string, string>();
			List<XmlAttribute> sessionAttrs = configDoc.GetSessionAttrs(sessionname);
			XmlNode sessionNode = configDoc.GetSessionNode(sessionname);
			foreach (XmlAttribute activeAttr in sessionAttrs)
			{
				if (activeAttr.Name != "name")
					activeSessionMap.Add(activeAttr.Name, configDoc.GetAttrValue(sessionNode, activeAttr.Name));
			}
			StringBuilder returnStr = new StringBuilder();
			returnStr.AppendLine("Session Name : " + sessionname);
			foreach(string tmpAttr in activeSessionMap.Keys)
			{				
				returnStr.AppendLine("Attr : " + tmpAttr + " | Value : " + activeSessionMap[tmpAttr]);
			}
			return returnStr.ToString();
		}

    }
}
