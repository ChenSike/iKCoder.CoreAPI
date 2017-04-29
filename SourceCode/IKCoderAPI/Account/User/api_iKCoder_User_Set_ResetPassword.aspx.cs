using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Account_User_api_iKCoder_User_Set_ResetPassword : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null)
        {
            string userSymbol = "";
            string userOldPassword = "";
            string userNewPassword = "";
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode oldPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/oldpassword");
            XmlNode newPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/newpassword");
            userSymbol = class_XmlHelper.GetNodeValue(userSymbolNode);
            if (string.IsNullOrEmpty(userSymbol))
            {
                userSymbol = GetQuerystringParam("symbol");
            }
            if (string.IsNullOrEmpty(userSymbol))
            {
                userSymbol = logined_user_name;
            }
            if (string.IsNullOrEmpty(userSymbol))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Symbol"), "");
                return;
            }
            userOldPassword = class_XmlHelper.GetNodeValue(oldPasswordNode);
            if (string.IsNullOrEmpty(userOldPassword))
            {
                userOldPassword = GetQuerystringParam("oldpassword");
            }
            if (string.IsNullOrEmpty(userOldPassword))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Password"), "");
                return;
            }
            userNewPassword = class_XmlHelper.GetNodeValue(newPasswordNode);
            if (string.IsNullOrEmpty(userNewPassword))
            {
                userNewPassword = GetQuerystringParam("newpassword");
            }
            if (string.IsNullOrEmpty(userNewPassword))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_NickName"), "");
                return;
            }
            string requestAPI = "/Account/api_VerifyAccountExisted.aspx?username=" + userSymbol;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            if (returnDoc.Contains("true"))
            {
                StringBuilder inputDoc = new StringBuilder();
                inputDoc.Append("<root>");
                inputDoc.Append("<username>");
                inputDoc.Append(userSymbol);
                inputDoc.Append("</username>");
                inputDoc.Append("<oldpassword>");
                inputDoc.Append(userOldPassword);
                inputDoc.Append("</oldpassword>");
                inputDoc.Append("<newpassword>");
                inputDoc.Append(userNewPassword);
                inputDoc.Append("</newpassword>");
                inputDoc.Append("</root>");
                requestAPI = "/Account/api_ChangePassword.aspx";
                URL = Server_API + Virtul_Folder_API + requestAPI;
                returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc.ToString(), URL, 1000 * 60, 100000);
                if (!returnDoc.Contains("err"))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Changed_Password"), "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Changed_Password"), "");
            }
        }
    }
}