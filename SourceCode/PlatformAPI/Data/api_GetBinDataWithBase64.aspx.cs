using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Data_api_GetBinDataWithBase64 : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        ISRESPONSEDOC = true;
        string dataId = "";
        dataId = GetQuerystringParam("id");
        string symbol = "";
        symbol = GetQuerystringParam("symbol");
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();
        if (!string.IsNullOrEmpty(dataId))
            activeSPEntry.ModifyParameterValue("@id", dataId);
        else if (!string.IsNullOrEmpty(symbol))
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            string resultBase64 = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataTable.Rows[0], "data", out resultBase64);
            Dictionary<string, string> messageList = new Dictionary<string, string>();
            messageList.Add("data", resultBase64);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), messageList);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetBinData", "No data.", "");
        }
        object_CommonLogic.CloseDBConnection();

    }
}