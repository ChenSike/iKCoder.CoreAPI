using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Text;
using System.Data;

public partial class Buss_api_AccountProfile_Create : class_WebClass_WLA
{
    protected override void AfterExtenedFunction()
    {        
        ISRESPONSEDOC = true;
        string profileName = "profile_" + activeUserName;
        string profileProduct = string.Empty;
        profileProduct = GetQuerystringParam("product");
        if (string.IsNullOrEmpty(profileProduct))
            profileProduct = "iKCoder";
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
        strProDoc.Append(activeUserName);
        strProDoc.Append("</usr_name>");
        strProDoc.Append("<usr_nickname>");
        strProDoc.Append("</usr_nickname>");
        strProDoc.Append("<coins>");
        strProDoc.Append("0");
        strProDoc.Append("</coins>");
        strProDoc.Append("<account_status>");
        strProDoc.Append("0");
        strProDoc.Append("</account_status>");
        strProDoc.Append("<account_limited>");
        strProDoc.Append("</account_limited>");
        strProDoc.Append("<account_head>");
        strProDoc.Append("</account_head>");
        strProDoc.Append("<account_childs>");
        strProDoc.Append("</account_childs>");
        strProDoc.Append("<account_sms>");
        strProDoc.Append("</account_sms>");
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
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", profileName);
        activeSPEntry.ModifyParameterValue("@profile_product", profileProduct);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if(activeAccountProfileDataTable!=null)
        {
            if(activeAccountProfileDataTable.Rows.Count>0)            
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : account profile existed.", "");
            else
            {
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@profile_name", profileName);
                activeSPEntry.ModifyParameterValue("@profile_product", profileProduct);
                activeSPEntry.ModifyParameterValue("@account_name", activeUserName);
                activeSPEntry.ModifyParameterValue("@profile_data", strProDoc);
                if(object_CommonLogic.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "created profile", "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : faild to create account profile.", "");
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : faild to select account profile.", "");

    }
}