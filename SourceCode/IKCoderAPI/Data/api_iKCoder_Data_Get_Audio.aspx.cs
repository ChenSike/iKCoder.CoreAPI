using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Data_api_iKCoder_Data_Get_Audio : class_WebBase_IKCoderAPI_NUA
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
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->Empty", "", enum_MessageType.Exception);
            }
            else
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
                    byte[] dataBuffer = class_Data_SqlDataHelper.GetArrBytesColumnData(activeDataRow, "data");
                    string type = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out type);
                    string isbyte = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "isbyte", out isbyte);
                    if (isbyte == "0")
                    {
                        string strDataBuffer = string.Empty;
                        class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out strDataBuffer);
                        dataBuffer = class_CommonUtil.Decoder_Base64ToByte(strDataBuffer);
                    }
                    switchResponseMode(enumResponseMode.bin);
                    Response.ContentType = "Audio/mpeg";
                    RESPONSEBUFFER = dataBuffer;
                }
                else
                {

                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Data.", "", enum_MessageType.Exception);
                }
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}