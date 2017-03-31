using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;
using System.Text;

public partial class Account_api_CreateUserAccountWithProfile : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();    
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        XmlNode productNode = REQUESTDOCUMENT.SelectSingleNode("/root/product");
        XmlNode templateNode = REQUESTDOCUMENT.SelectSingleNode("/root/template");
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        string profileProduct = class_XmlHelper.GetNodeValue("", productNode);
        string template = class_XmlHelper.GetNodeValue("", templateNode); 
        if (string.IsNullOrEmpty(username))
            username = GetQuerystringParam("username");
        if (string.IsNullOrEmpty(password))
            password = GetQuerystringParam("password");
        if(string.IsNullOrEmpty(profileProduct))
            profileProduct = GetQuerystringParam("product");
        if (string.IsNullOrEmpty(profileProduct))
            profileProduct = "iKCoder";
        if (string.IsNullOrEmpty(template))
            template = GetQuerystringParam("template");
        if (string.IsNullOrEmpty(template))
            template = "profile_ikcoder_template";
        string desPassword = string.Empty;
        object_CommonLogic.Object_DES.DESCoding(password, out desPassword);
        if (!string.IsNullOrEmpty(desPassword))
            password = desPassword;
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count == 1)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : insert -> username existed.", "");
            object_CommonLogic.CloseDBConnection();
            return;
        }
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        activeSPEntry.ModifyParameterValue("@password", password);
        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, "insert", this.GetType());
        string profileName = "profile_" + username;
        class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();
        activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", template);
        DataTable textDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        string templateresult = "";
        XmlDocument templateDocument = new XmlDocument();
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(textDataTable.Rows[0], "data", out templateresult);
            templateDocument.LoadXml(templateresult);
            XmlNode activeNode = templateDocument.SelectSingleNode("/root/docbasic/doc_symbol");
            class_XmlHelper.SetNodeValue(activeNode, profileName);
            activeNode = templateDocument.SelectSingleNode("/root/usrbasic/usr_name");
            class_XmlHelper.SetNodeValue(activeNode, username);
        }
        activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", profileName);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null)
        {
            if (activeAccountProfileDataTable.Rows.Count > 0)
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : account profile existed.", "");
            else
            {
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@profile_name", profileName);
                activeSPEntry.ModifyParameterValue("@profile_product", profileProduct);
                activeSPEntry.ModifyParameterValue("@account_name", username);
                activeSPEntry.ModifyParameterValue("@profile_data", templateDocument.OuterXml);
                if (object_CommonLogic.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "created profile", "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : faild to create account profile.", "");
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : faild to select account profile.", "");
        object_CommonLogic.CloseDBConnection();

    }
}