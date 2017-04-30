using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_iKCoder_Data_Set_TextData : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            string symbol = GetQuerystringParam("symbol");
            if (string.IsNullOrEmpty(symbol))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                return;
            }
            string strBase64Data = string.Empty;
            if (REQUESTDOCUMENT != null)
                strBase64Data = class_CommonUtil.Encoder_Base64(REQUESTDOCUMENT.OuterXml);
            else if(!string.IsNullOrEmpty(REQUESTCONTENT))
                strBase64Data = class_CommonUtil.Encoder_Base64(REQUESTCONTENT);
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
            activeSPEntry_configSence.ClearAllParamsValues();
            activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);

            if (textDataTable != null && textDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                string id = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                activeSPEntry_configSence.ClearAllParamsValues();
                activeSPEntry_configSence.ModifyParameterValue("@id", id);
                activeSPEntry_configSence.ModifyParameterValue("@data", strBase64Data);
                Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());

            }
            else
            {
                activeSPEntry_configSence.ClearAllParams();
                activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
                activeSPEntry_configSence.ModifyParameterValue("@data", strBase64Data);
                activeSPEntry_configSence.ModifyParameterValue("@owner", "sys");
                Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}