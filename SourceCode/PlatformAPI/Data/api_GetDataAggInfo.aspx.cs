using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Data_api_GetDataAggInfo : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        ISRESPONSEDOC = true;
        string produceName = "";
        produceName = GetQuerystringParam("produce");        
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();
        DataTable activeDataTable = new DataTable();
        if (!string.IsNullOrEmpty(produceName))
        {
            activeSPEntry.ModifyParameterValue("@produce", produceName);
            activeDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        }
        else
            activeDataTable = _objectSqlHelper.ExecuteSelectSPForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        foreach(DataRow activeDataRow in activeDataTable.Rows)
        {
            Dictionary<string,string> attrs = new Dictionary<string,string>();
            string id = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"id",out id);
            string symbol = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"symbol",out symbol);
            string type = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"type",out type);
            string produce = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"produce",out produce);
            string isBin = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"isBinary",out isBin);
            string isBase64 = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"isBase64",out isBase64);
            string isDES = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"isDES",out isDES);
            string DESKey = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow,"DESKey",out DESKey);
            attrs.Add("id", id);
            attrs.Add("symbol", symbol);
            attrs.Add("type", type);
            attrs.Add("produce", produce);
            attrs.Add("isBinary", isBin);
            attrs.Add("isBase64", isBase64);
            attrs.Add("isDES", isDES);
            attrs.Add("DESKey", DESKey);
            AddResponseMessageToResponseDOC("DataAggInfo", "126", attrs);
        }
    }
}