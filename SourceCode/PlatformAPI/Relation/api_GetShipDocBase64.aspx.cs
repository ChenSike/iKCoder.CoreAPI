using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Relation_api_GetShipDocBase64 : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string symbol = GetQuerystringParam("symbol");
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_relationship");
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            Dictionary<string, string> attrMap = new Dictionary<string, string>();
            string tmpValue = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out tmpValue);
            attrMap.Add("id", tmpValue);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "symbol", out tmpValue);
            attrMap.Add("symbol", tmpValue);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "produce", out tmpValue);
            attrMap.Add("produce", tmpValue);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "relationdoc", out tmpValue);
            string strBase64 = class_CommonUtil.Encoder_Base64(tmpValue);
            attrMap.Add("relationdoc", strBase64);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrMap);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
    }
}