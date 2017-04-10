using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;


public partial class Account_SET_ResetPassword : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        if (REQUESTDOCUMENT != null)
        {
            string userSymbol = "";
            string userOldPassword = "";
            string codeName = "";
            string codeValue = "";
            string userNewPassword = "";
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode oldPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/oldpassword");
            XmlNode codeNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/codename");
            XmlNode codeValueNode = REQUESTDOCUMENT.SelectSingleNode("/root/codevalue");
            XmlNode newPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/newpassword");
            userSymbol = class_XmlHelper.GetNodeValue(userSymbolNode);
            if (string.IsNullOrEmpty(userSymbol))
            {
                userSymbol = GetQuerystringParam("symbol");
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

            codeValue = class_XmlHelper.GetNodeValue(codeValueNode);
            if (string.IsNullOrEmpty(codeValue))
            {
                codeValue = GetQuerystringParam("codevalue");
            }
            if (string.IsNullOrEmpty(codeValue))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Checkcode"), "");
                return;
            }

            codeName = class_XmlHelper.GetNodeValue(codeNameNode);
            if (string.IsNullOrEmpty(codeName))
            {
                codeName = GetQuerystringParam("codename");
            }
            if (string.IsNullOrEmpty(codeName))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_CodeName"), "");
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

            string codeValueFromServer = "";
            if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), codeName) != null)
                codeValueFromServer = (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), codeName)).ToString();
            if (codeValueFromServer != codeValue)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Equal_Param_Reg_Code"), "");
                return;
            }
            string requestAPI = "/Account/api_VerifyAccountExisted.aspx?cid=" + cid + "&username=" + userSymbol;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            if (returnDoc.Contains("true"))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_AccountExisted"), "");
                return;
            }
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
            requestAPI = "/Account/api_CreateUserAccountWithProfile.aspx?cid=" + cid;
            URL = Server_API + Virtul_Folder_API + requestAPI;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc.ToString(), URL, 1000 * 60, 100000);
            if (!returnDoc.Contains("<err>"))
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Changed_Password"), "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Changed_Password"), "");
        }
    }
}