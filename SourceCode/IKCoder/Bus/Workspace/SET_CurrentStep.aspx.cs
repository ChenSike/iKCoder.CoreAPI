using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;

public partial class Bus_Workspace_SET_CurrentStep : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        string currentstage = GetQuerystringParam("stage");
        string account = Session["logined_user_name"].ToString();
        if (!string.IsNullOrEmpty(currentstage))
        {
            string requestAPI = "/Profile/api_AccountProfile_SetNodes.aspx?cid=" + cid;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            StringBuilder strRequestDoc = new StringBuilder();
            strRequestDoc.Append("<root>");
            strRequestDoc.Append("<account>");
            strRequestDoc.Append(account);
            strRequestDoc.Append("</account>");
            strRequestDoc.Append("<produce>");
            strRequestDoc.Append(Produce_Name);
            strRequestDoc.Append("</produce>");
            strRequestDoc.Append("<parent>");
            strRequestDoc.Append("/root/studystatus/currentsence");
            strRequestDoc.Append("</parent>");
            strRequestDoc.Append("<newnodes>");
            strRequestDoc.Append("<item name=\"currentstage\" value=\"" + currentstage + "\" >");
            strRequestDoc.Append("</item>");
            strRequestDoc.Append("</newnodes>");
            strRequestDoc.Append("</root>");
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strRequestDoc.ToString(), URL, 1000 * 60, 100000);
            if (!returnDoc.Contains("err"))
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty stage in querystring", "");

    }
}