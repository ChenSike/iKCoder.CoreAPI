using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Text;

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
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");            
            XmlNode userPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            XmlNode codeNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/codename");
            XmlNode codeValueNode = REQUESTDOCUMENT.SelectSingleNode("/root/codevalue");
            if (userSymbolNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName,Object_LabelController.GetString("message","Empty_Param_Reg_Symbol"), "");
                return;
            }
            else
            {
                userSymbol = class_XmlHelper.GetNodeValue(userSymbolNode);
                if(string.IsNullOrEmpty( userSymbol))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Symbol"), "");
                    return;
                }
            }
            if(userPasswordNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Password"), "");
                return;
            }
            else
            {
                userPassword = class_XmlHelper.GetNodeValue(userPasswordNode);
                if (string.IsNullOrEmpty(userPassword))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Password"), "");
                    return;
                }
            }
            if(codeValueNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Checkcode"), "");
                return;
            }
            else
            {
                codeValue = class_XmlHelper.GetNodeValue(codeValueNode);
                if (string.IsNullOrEmpty(codeValue))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Checkcode"), "");
                    return;
                }
            }
            if (codeNameNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_CodeName"), "");
                return;
            }
            else
            {
                codeName = class_XmlHelper.GetNodeValue(codeNameNode);
                if (string.IsNullOrEmpty(codeName))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_CodeName"), "");
                    return;
                }
            }
            string codeValueFromServer = (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name), codeName)).ToString();
            if(codeValueFromServer != codeValue)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Equal_Param_Reg_Code"), "");
                return;
            }
            StringBuilder inputDoc = new StringBuilder();
            inputDoc.Append("<root>");
            inputDoc.Append("<operation>insert</operation>");
            inputDoc.Append("<username>");
            inputDoc.Append(userSymbol);
            inputDoc.Append("</username>");
            inputDoc.Append("<password");
            inputDoc.Append(userPassword);
            inputDoc.Append("</password>");
            inputDoc.Append("</root>");
            string requestAPI = "Account/api_OperationUserAccount.aspx";
            string URL = Server_API+Virtul_Folder_API+requestAPI;
            Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc.ToString(), URL, 1000 * 60, 100000);
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Input Document", "",enum_MessageType.Exception);
        
    }
}