using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Message_api_Message_RegRouter : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string symbol = string.Empty;
        symbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
            return;
        }
        string pushedapi = string.Empty;
        pushedapi = GetQuerystringParam("pushedapi");
        string getapi = string.Empty;
        getapi = GetQuerystringParam("getapi");
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_router");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            string id = string.Empty;
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(activeDataTable, 0, out activeDataRow);
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@pushedapi", pushedapi);
            activeSPEntry.ModifyParameterValue("@getapi", getapi);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
        }
        else
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
            activeSPEntry.ModifyParameterValue("@pushedapi", pushedapi);
            activeSPEntry.ModifyParameterValue("@getapi", getapi);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());

        }

    }
}