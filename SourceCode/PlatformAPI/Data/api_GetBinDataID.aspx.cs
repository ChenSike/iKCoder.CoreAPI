using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_GetBinDataID : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;                
        string symbol = GetQuerystringParam("symbol");        
        class_Data_SqlSPEntry activeSPEntry_binData = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
        DataTable binDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (binDataTable.Rows.Count > 0)
        {
            string returnID = "";
            class_Data_SqlDataHelper.GetColumnData(binDataTable.Rows[0], "id", out returnID);
            Dictionary<string,string> messageList = new Dictionary<string,string>();
            messageList.Add("id",returnID);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), messageList);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : symbol existed or guid existed.", "");
        }
        object_CommonLogic.CloseDBConnection();
    }
}