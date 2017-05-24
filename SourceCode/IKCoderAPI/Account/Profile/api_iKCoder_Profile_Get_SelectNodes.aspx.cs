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
 
            List<string> lstSelectXpath = new List<string>();
            XmlNodeList selectItems = REQUESTDOCUMENT.SelectNodes("/root/select/items");
            foreach(XmlNode activeItem in selectItems)
            {
                string selectXpath = class_XmlHelper.GetAttrValue(activeItem,"value");
                lstSelectXpath.Add(selectXpath);
            }
            Dictionary<string, string> resultLst = new Dictionary<string, string>();
            resultLst = Object_ProfileDocs.GetDocNotesValues(logined_user_name, class_CommonDefined.enumProfileDoc.doc_basic, lstSelectXpath, true);
            foreach (string resultXpath in resultLst.Keys)
            {
                string value = resultLst[resultXpath];
                string xpath = resultXpath;
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("value", value);
                attrs.Add("xpath", xpath);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);

    }
}