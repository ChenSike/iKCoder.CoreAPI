using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;


public partial class Profile_api_AccountProfile_SelectProfileBySymbol : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string symbol = GetQuerystringParam("symbol");
        if (string.IsNullOrEmpty(symbol))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/symbol") != null)
                    symbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/symbol"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated symbol for select profile.", "");
                    return;
                }

            }
        }     
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", symbol);        
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null)
        {
            if (activeAccountProfileDataTable.Rows.Count > 0)
            {
                string strDoc = "";
                class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out strDoc);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), strDoc, "");
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");

    }
}