using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_iKCoder_Data_Get_AggResourceText : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configBlockly = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_configBlockly, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            foreach (DataRow activeRow in textDataTable.Rows)
            {
                string symbol = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeRow, "symbol", out symbol);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), symbol, "");
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}