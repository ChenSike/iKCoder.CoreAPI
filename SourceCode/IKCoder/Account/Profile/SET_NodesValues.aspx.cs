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
        if (REQUESTDOCUMENT != null)
        {
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