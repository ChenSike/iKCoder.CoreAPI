using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_Profile_api_iKCoder_Profile_Set_Nodes : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null)
        {
            string account = string.Empty;
            account = logined_user_name;
            XmlNode accountNode = REQUESTDOCUMENT.SelectSingleNode("/root/account");            
            if (accountNode == null)
            {
                accountNode = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "account", account);
                REQUESTDOCUMENT.SelectSingleNode("/root").AppendChild(accountNode);
            }
            else
                class_XmlHelper.SetNodeValue(accountNode, account);
            string produce = Produce_Name;
            XmlNode produceNode = REQUESTDOCUMENT.SelectSingleNode("/root/produce");
            if (produceNode == null)
            {
                produceNode = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "produce", produce);
                REQUESTDOCUMENT.SelectSingleNode("/root").AppendChild(produceNode);
            }
            else
                class_XmlHelper.SetNodeValue(produceNode, produce);
            string requestAPI = "/Profile/api_AccountProfile_SetNodes.aspx";
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(REQUESTDOCUMENT.OuterXml, URL, 1000 * 60, 1024 * 1024);
            if (!returnDoc.Contains("err"))
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Changed_Profiles"), "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Changed_Profiles"), "");

            
            string parentXpath = string.Empty;
            XmlNode parentNode = REQUESTDOCUMENT.SelectSingleNode("/root/parent");
            if(parentNode!=null)
            {
                parentXpath = class_XmlHelper.GetNodeValue(parentNode);
            }
            XmlNodeList newNodes_items = REQUESTDOCUMENT.SelectNodes("/root/newnodes/item");
            Dictionary<string, string> xpathSet = new Dictionary<string, string>();
            foreach(XmlNode activeItemNode in newNodes_items)
            {
                string newnodename = class_XmlHelper.GetAttrValue(activeItemNode, "name");
                string value = class_XmlHelper.GetAttrValue(activeItemNode, "value");
                string xpath = parentXpath + "/" + newnodename;
                if (xpath.StartsWith("/root"))
                    xpath = xpath.Replace("/root", "");
                if (!xpathSet.ContainsKey(xpath))
                    xpathSet.Add(xpath, value);
            }
            Object_ProfileDocs.SetNodesValues(logined_user_name, xpathSet, class_CommonDefined.enumProfileDoc.doc_basic);
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty request document", "");

    }
}