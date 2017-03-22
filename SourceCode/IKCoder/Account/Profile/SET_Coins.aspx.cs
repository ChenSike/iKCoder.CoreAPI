using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;

public partial class Account_Profile_Set_Coins : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        string nickName = GetQuerystringParam("nickname");
        string coins = GetQuerystringParam("coins");
        string account = GetQuerystringParam("account"); 
        if (!string.IsNullOrEmpty(nickName))
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
            strRequestDoc.Append("/root/usrbasic");
            strRequestDoc.Append("</parent>");
            strRequestDoc.Append("<newnodes>");
            strRequestDoc.Append("<item name=\"usr_nickname\" value=\"" + nickName + "\" >");
            strRequestDoc.Append("</item>");
            strRequestDoc.Append("</newnodes>");
            strRequestDoc.Append("</root>");
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strRequestDoc.ToString(), URL, 1000 * 60, 100000);
            if (!returnDoc.Contains("<err>"))
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Reg_Account"), "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_Account"), "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty nickname in querystring", "");

    }
}