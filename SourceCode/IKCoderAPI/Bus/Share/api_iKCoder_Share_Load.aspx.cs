using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_Share_api_iKCoder_Share_Load : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
            return;
        }       
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TMPSENCES);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(activeDataTable, 0, out activeDataRow);
            if (activeDataRow != null)
            {
                string configData = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configData);
                if (!string.IsNullOrEmpty(configData))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), configData, "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "configdoc->empty", "");
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Share_Link_Timeout"), "");
            return;
        }
    }
}