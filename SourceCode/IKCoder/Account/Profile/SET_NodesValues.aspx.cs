using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_Profile_SET_NodesValues : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {
            string account = string.Empty;
            XmlNode accountNode = REQUESTDOCUMENT.SelectSingleNode("/root/account");
            account = Session["logined_user_name"].ToString();
            if (accountNode == null)
            {
                accountNode = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "account", account);
                REQUESTDOCUMENT.SelectSingleNode("/root").AppendChild(accountNode);
            }
            else
                class_XmlHelper.SetNodeValue(accountNode, account);
            string produce = Produce_Name;
            XmlNode produceNode = REQUESTDOCUMENT.SelectSingleNode("/root/produce");
            if(produceNode==null)
            {
                produceNode = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "produce", produce);
                REQUESTDOCUMENT.SelectSingleNode("/root").AppendChild(produceNode);
            }
            else
                class_XmlHelper.SetNodeValue(produceNode, produce);            
            string requestAPI = "/Profile/api_AccountProfile_SetNodes.aspx?cid=" + cid;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(REQUESTDOCUMENT.OuterXml, URL, 1000 * 60, 100000);
            if (!returnDoc.Contains("err"))
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Reg_Account"), "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_Account"), "");

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty request document", "");
    }
}