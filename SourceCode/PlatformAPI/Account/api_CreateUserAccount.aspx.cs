using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Account_api_CreateUserAccount : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        XmlNode productNode = REQUESTDOCUMENT.SelectSingleNode("/root/product");
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        string profileProduct = class_XmlHelper.GetNodeValue("", productNode);
        if (string.IsNullOrEmpty(username))
            username = GetQuerystringParam("username");
        if (string.IsNullOrEmpty(password))
            password = GetQuerystringParam("password");
        if (string.IsNullOrEmpty(profileProduct))
            profileProduct = GetQuerystringParam("product");
        if (string.IsNullOrEmpty(profileProduct))
            profileProduct = "ikcoder";
        string desPassword = string.Empty;
        object_CommonLogic.Object_DES.DESCoding(password, out desPassword);
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count == 1)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : insert -> username existed.", "");
            return;
        }
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        activeSPEntry.ModifyParameterValue("@password", desPassword);
        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, "insert", this.GetType());

    }
}