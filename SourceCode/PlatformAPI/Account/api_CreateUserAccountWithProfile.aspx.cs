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
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        string profileProduct = class_XmlHelper.GetNodeValue("", productNode);   
        if (string.IsNullOrEmpty(username))
            username = GetQuerystringParam("username");
        if (string.IsNullOrEmpty(password))
            password = GetQuerystringParam("password");
        if(string.IsNullOrEmpty(profileProduct))
            profileProduct = GetQuerystringParam("product");
        if (string.IsNullOrEmpty(profileProduct))
            profileProduct = "iKCoder";
        class_Security_DES object_DES = new class_Security_DES(class_CommonLogic.Const_DESKey);
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
        StringBuilder strProDoc = new StringBuilder();
        strProDoc.Append("<root>");
        strProDoc.Append("<docbasic>");
        strProDoc.Append("<doc_id>");
        strProDoc.Append("</doc_id>");
        strProDoc.Append("<doc_symbol>");
        strProDoc.Append(profileName);
        strProDoc.Append("</doc_symbol>");
        strProDoc.Append("</docbasic>");
        strProDoc.Append("<usrbasic>");
        strProDoc.Append("<usr_name>");
        strProDoc.Append(username);
        strProDoc.Append("</usr_name>");
        strProDoc.Append("<usr_nickname>");
        strProDoc.Append("</usr_nickname>");
        strProDoc.Append("<coins>");
        strProDoc.Append("0");
        strProDoc.Append("</coins>");
        strProDoc.Append("<account_status>");
        strProDoc.Append("L0");
        strProDoc.Append("</account_status>");
        strProDoc.Append("<account_limited>");
        strProDoc.Append("</account_limited>");
        strProDoc.Append("<account_childs>");
        strProDoc.Append("</account_childs>");
        strProDoc.Append("<account_head>");
        strProDoc.Append("</account_head>");
        strProDoc.Append("</usrbasic>");
        strProDoc.Append("<lessons>");
        strProDoc.Append("<begin></begin>");
        strProDoc.Append("<intermediate></intermediate>");
        strProDoc.Append("<senior></senior>");
        strProDoc.Append("</lessons>");
        strProDoc.Append("<friends></friends>");
        strProDoc.Append("</root>");
        XmlDocument proDoc = new XmlDocument();
        proDoc.LoadXml(strProDoc.ToString());
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
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
                activeSPEntry.ModifyParameterValue("@profile_data", strProDoc);
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