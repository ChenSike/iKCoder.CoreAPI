using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Account_api_VerifyAccountExisted : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        if (string.IsNullOrEmpty(username))
            username = GetQuerystringParam("username");
        class_Security_DES object_DES = new class_Security_DES(class_CommonLogic.Const_DESKey);        
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count == 1)
           AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        else
           AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");        

    }
}