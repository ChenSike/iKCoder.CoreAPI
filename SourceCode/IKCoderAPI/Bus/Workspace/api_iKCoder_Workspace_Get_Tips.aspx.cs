using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_Workspace_api_iKCoder_Workspace_Get_Tips : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            string senceSymbol = GetQuerystringParam("symbol");
            if (string.IsNullOrEmpty(senceSymbol))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                return;
            }
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configBlockly = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
            activeSPEntry_configBlockly.ClearAllParamsValues();
            activeSPEntry_configBlockly.ModifyParameterValue("@symbol", senceSymbol);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configBlockly, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (textDataTable != null && textDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                string config = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "config", out config);
                config = class_CommonUtil.Decoder_Base64(config);
                RESPONSEDOCUMENT = new System.Xml.XmlDocument();
                RESPONSEDOCUMENT.LoadXml(config);
            }

        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}