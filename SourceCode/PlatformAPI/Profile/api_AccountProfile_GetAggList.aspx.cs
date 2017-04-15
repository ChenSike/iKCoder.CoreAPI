using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Profile_api_AccountProfile_GetAggList : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string produceName = GetQuerystringParam("produce");       
        if (string.IsNullOrEmpty(produceName))
        {
            produceName = "iKCoder";
        }        
        object_CommonLogic.ConnectToDatabase();        
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        if (!string.IsNullOrEmpty(produceName))
            activeSPEntry.ModifyParameterValue("@profile_product", produceName);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null)
        {
            if (activeAccountProfileDataTable.Rows.Count > 0)
            {
                foreach (DataRow activeRow in activeAccountProfileDataTable.Rows)
                {
                    string symbol = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "profile_name", out symbol);
                    string id = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "id", out id);
                    string account = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "account_name", out account);
                    Dictionary<string, string> resultAttrs = new Dictionary<string, string>();
                    resultAttrs.Add("symbol", symbol);
                    resultAttrs.Add("account", account);
                    resultAttrs.Add("id", id);
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), resultAttrs);
                }
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
    }
}