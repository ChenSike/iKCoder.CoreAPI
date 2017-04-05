using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Data_api_GetMetaText : class_WebClass_WA
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
        DataTable textDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if(textDataTable!=null && textDataTable.Rows.Count>0)
        {
            string result = "";            
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(textDataTable.Rows[0], "data", out result);
            result = class_CommonUtil.Decoder_Base64(result);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), result, "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetMetaText", "No data.", "");
        }
    }
}