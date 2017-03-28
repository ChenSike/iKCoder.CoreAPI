using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Text;

public class testdata
{
    public string data;
}

public partial class Account_SET_Reg : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
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
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_CodeName"), "");
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
            inputDoc.Append("<password>");
            inputDoc.Append(userPassword);
            inputDoc.Append("</password>");
            inputDoc.Append("<product>");
            inputDoc.Append(Produce_Name);
            inputDoc.Append("</product>");
            inputDoc.Append("<template>");
            inputDoc.Append("profile_template_ikcoder");
            inputDoc.Append("</template>");
            inputDoc.Append("</root>");
            requestAPI = "/Account/api_CreateUserAccountWithProfile.aspx?cid=" + cid;
            URL = Server_API + Virtul_Folder_API + requestAPI;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc.ToString(), URL, 1000 * 60, 100000);
            if (!returnDoc.Contains("<err>"))
            {                
                    requestAPI = "/Profile/api_AccountProfile_SetNodes.aspx?cid=" + cid;
                    URL = Server_API + Virtul_Folder_API + requestAPI;
                    StringBuilder strRequestDoc = new StringBuilder();
                    strRequestDoc.Append("<root>");
                    strRequestDoc.Append("<account>");
                    strRequestDoc.Append(userSymbol);
                    strRequestDoc.Append("</account>");
                    strRequestDoc.Append("<produce>");
                    strRequestDoc.Append(Produce_Name);
                    strRequestDoc.Append("</produce>");
                    strRequestDoc.Append("<parent>");
                    strRequestDoc.Append("/root/usrbasic");
                    strRequestDoc.Append("</parent>");
                    strRequestDoc.Append("<newnodes>");
                    strRequestDoc.Append("<item name=\"usr_nickname\" value=\"" + nickname + "\" >");
                    strRequestDoc.Append("</item>");
                    strRequestDoc.Append("</newnodes>");
                    strRequestDoc.Append("</root>");
                    returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strRequestDoc.ToString(), URL, 1000 * 60, 100000);
                    if (!returnDoc.Contains("<err>"))
                        AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Reg_Account"), "");
                    else
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_Account"), "");                    
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_Account"), "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Input Document", "", enum_MessageType.Exception);

    }
}