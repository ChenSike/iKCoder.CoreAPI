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
                if (!xpathSet.ContainsKey(xpath))
                    xpathSet.Add(xpath, value);
            }
            Object_ProfileDocs.SetNodesValues(logined_user_name, xpathSet, class_CommonDefined.enumProfileDoc.doc_basic,true); 
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Changed_Profiles"), "");

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty request document", "");

    }
}