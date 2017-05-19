using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_Profile_api_iKCoder_Profile_Get_SelectNodes : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null)
        {
            string account = string.Empty;
            XmlNode accountNode = REQUESTDOCUMENT.SelectSingleNode("/root/account");
            account = logined_user_name;
            if (accountNode == null)
            {
                accountNode = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "account", account);
                REQUESTDOCUMENT.SelectSingleNode("/root").AppendChild(accountNode);
            }
            else
                class_XmlHelper.SetNodeValue(accountNode, account);
            string requestAPI = "/Profile/api_AccountProfile_SelectNodesValues.aspx";
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(REQUESTDOCUMENT.OuterXml, URL, 1000 * 60, 100000);
            XmlDocument returnDoc = new XmlDocument();
            returnDoc.LoadXml(returnStrDoc);
            if (returnDoc.SelectSingleNode("/root/err")==null)
            {
                XmlNodeList msgNodes = returnDoc.SelectNodes("/root/msg");
                foreach (XmlNode msgNode in msgNodes)
                {
                    string value = class_XmlHelper.GetAttrValue(msgNode, "value");
                    string xpath = class_XmlHelper.GetAttrValue(msgNode, "xpath");
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("value", value);
                    attrs.Add("xpath", xpath);
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
                }
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);


            List<string> lstSelectXpath = new List<string>();
            XmlNodeList selectItems = REQUESTDOCUMENT.SelectNodes("/root/select/items");
            foreach(XmlNode activeItem in selectItems)
            {
                string selectXpath = class_XmlHelper.GetNodeValue(activeItem);
                lstSelectXpath.Add(selectXpath);
            }
            Dictionary<string, string> resultLst = new Dictionary<string, string>();
            resultLst = Object_ProfileDocs.GetDocNotesValues(logined_user_name, class_CommonDefined.enumProfileDoc.doc_basic, lstSelectXpath, true);

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);

    }
}