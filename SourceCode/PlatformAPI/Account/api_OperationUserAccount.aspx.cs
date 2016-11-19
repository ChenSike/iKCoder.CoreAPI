using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Account_api_OperationUserAccount : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode operationNode = REQUESTDOCUMENT.SelectSingleNode("/root/operation");
        if(operationNode==null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No operation node.", "");
            return;
        }
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        if (usernameNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No username node.", "");
            return;
        }
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        if (passwordNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No password node.", "");
            return;
        }
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string operation = class_XmlHelper.GetNodeValue("", operationNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        PlatformAPICodeBehind.Account.class_Account _objectAccount = new PlatformAPICodeBehind.Account.class_Account(object_CommonLogic.Object_SqlConnectionHelper.Get_ActiveConnection(class_CommonLogic.Const_PlatformDBSymbol), object_CommonLogic.storeProceduresList[class_CommonLogic.Const_PlatformDBSymbol]);
        _objectAccount.Action_DoOperation(operation, username, password);
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "api_OperationUserAccount", class_CommonDefined.enumExecutedCode.executed.ToString(), "Executed Api", "");
        object_CommonLogic.CloseDBConnection();
    }    
}