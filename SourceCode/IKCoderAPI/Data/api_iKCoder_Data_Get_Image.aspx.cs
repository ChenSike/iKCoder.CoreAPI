using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_iKCoder_Data_Get_Image : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            string symbol = GetQuerystringParam("symbol");
            if (!string.IsNullOrEmpty(symbol))
            {
                Object_CommonData.PrepareDataOperation();
                class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
                activeSPEntry_binData.ClearAllParamsValues();
                activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if (binDataTable != null && binDataTable.Rows.Count > 0)
                {
                    DataRow activeDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                    byte[] headerImageData = class_Data_SqlDataHelper.GetArrBytesColumnData(activeDataRow, "data");
                    string isbyte = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "isbyte", out isbyte);
                    if (isbyte == "0")
                    {
                        string strDataBuffer = string.Empty;
                        class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out strDataBuffer);
                        headerImageData = class_CommonUtil.Decoder_Base64ToByte(strDataBuffer);
                    }
                    if (headerImageData != null && headerImageData.Length > 0)
                    {
                        
                        switchResponseMode(enumResponseMode.bin);
                        RESPONSEBUFFER = headerImageData;
                    }
                    else
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "", enum_MessageType.Exception);
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }

    }
}