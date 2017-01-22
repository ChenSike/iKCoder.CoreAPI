using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Profile_api_AccountProfile_Select : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string accountName = GetQuerystringParam("account");
        string produceName = GetQuerystringParam("produce");
        if(string.IsNullOrEmpty(accountName))
        {
            if(REQUESTDOCUMENT!=null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/account") != null)
                    accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/account"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for select profile.", "");
                    return;
                }

            }
        }
        if(string.IsNullOrEmpty(produceName))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/produce") != null)
                    accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/produce"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated produce name for select profile.", "");
                    return;
                }

            }
        }
        string profileSymbol = "profile_" + accountName;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
        activeSPEntry.ModifyParameterValue("@profile_product", produceName);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
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