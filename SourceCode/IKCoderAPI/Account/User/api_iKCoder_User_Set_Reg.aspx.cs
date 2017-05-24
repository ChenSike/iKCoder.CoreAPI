using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;
using System.Xml;

public partial class Account_User_api_iKCoder_User_Set_Reg : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null)
        {
            string userSymbol = "";
            string userPassword = "";
            string codeName = "";
            string codeValue = "";
            string nickname = "";
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode userPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            XmlNode codeNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/codename");
            XmlNode codeValueNode = REQUESTDOCUMENT.SelectSingleNode("/root/codevalue");
            XmlNode nickNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/nickname");
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
            userPassword = class_XmlHelper.GetNodeValue(userPasswordNode);
            if (string.IsNullOrEmpty(userPassword))
            {
                userPassword = GetQuerystringParam("password");
            }
            if (string.IsNullOrEmpty(userPassword))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Password"), "");
                return;
            }
            if(userPassword.Length<0 || userPassword.Length>12)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_PasswordLength"), "");
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

            nickname = class_XmlHelper.GetNodeValue(nickNameNode);
            if (string.IsNullOrEmpty(nickname))
            {
                nickname = GetQuerystringParam("nickname");
            }
            if (string.IsNullOrEmpty(nickname))
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
            string requestAPI = "/Account/api_VerifyAccountExisted.aspx?username=" + userSymbol;
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
            inputDoc.Append("<password>");
            inputDoc.Append(userPassword);
            inputDoc.Append("</password>");
            inputDoc.Append("<product>");
            inputDoc.Append(Produce_Name);
            inputDoc.Append("</product>");
            inputDoc.Append("</root>");
            requestAPI = "/Account/api_CreateUserAccount.aspx";
            URL = Server_API + Virtul_Folder_API + requestAPI;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc.ToString(), URL, 1000 * 60, 1024 * 1024);
            if (!returnDoc.Contains("<err>"))
            {
                Object_ProfileDocs.VerifyAll(userSymbol);
                Dictionary<string, string> newValuesMap = new Dictionary<string, string>();
                newValuesMap.Add("/root/usrbasic/usr_nickname", nickname);
                Object_ProfileDocs.SetNodesValues(userSymbol, newValuesMap, class_CommonDefined.enumProfileDoc.doc_basic, true);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Reg_Account"), "");
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_Account"), "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Input Document", "", enum_MessageType.Exception);

    }
}