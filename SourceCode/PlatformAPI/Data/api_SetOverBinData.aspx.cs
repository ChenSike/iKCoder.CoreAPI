using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_SetOverBinData : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        byte[] binBuffer = new byte[Request.InputStream.Length];
        Request.InputStream.Read(binBuffer, 0, (int)Request.InputStream.Length);
        string symbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(symbol))
        {

        }
        string type = GetQuerystringParam("type");
        if (binBuffer != null && Request.InputStream.Length > 0)
        {
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            string binData = System.Text.Encoding.Default.GetString(binBuffer);
            string binData64 = class_CommonUtil.Decoder_Base64(binData);
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
            string data = binData64;
            if (string.IsNullOrEmpty(type))
                type = "bin";                
            class_Data_SqlSPEntry activeSPEntry_binData = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
            activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
            DataTable binDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (binDataTable.Rows.Count == 0)
            {
                activeSPEntry.ModifyParameterValue("@symbol", symbol);
                activeSPEntry.ModifyParameterValue("@type", type);
                activeSPEntry.ModifyParameterValue("@data", data);
                activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
                activeSPEntry.ModifyParameterValue("@isBinary", "1");
                activeSPEntry.ModifyParameterValue("@isBase64", "1");
                activeSPEntry.ModifyParameterValue("@isDES", "0");
                activeSPEntry.ModifyParameterValue("@DESKey", "");
                object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
            }
            else
            {
                string id = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(binDataTable.Rows[0], "id", out id);
                activeSPEntry.ModifyParameterValue("@id", id);
                activeSPEntry.ModifyParameterValue("@data", data);
                object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");

            }
            object_CommonLogic.CloseDBConnection();
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty param->symbol", "");
        }
    }
}