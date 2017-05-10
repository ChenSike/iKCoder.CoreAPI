using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Message_api_Message_GetMessageRouterList : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_router");
        DataTable activeDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            foreach(DataRow activeRow in activeDataTable.Rows)
            {
                string id = string.Empty;
                string produce = string.Empty;
                string pushapi = string.Empty;
                string getapi = string.Empty;
                string symbol = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeRow, "produce", out produce);
                class_Data_SqlDataHelper.GetColumnData(activeRow, "pushapi", out pushapi);
                class_Data_SqlDataHelper.GetColumnData(activeRow, "getapi", out getapi);
                class_Data_SqlDataHelper.GetColumnData(activeRow, "symbol", out symbol);
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("id", id);
                attrs.Add("produce", produce);
                attrs.Add("pushapi", pushapi);
                attrs.Add("getapi", getapi);
                attrs.Add("symbol", symbol);
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }
    }
}