using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_iKCoder_Data_Set_BinaryData : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            string symbol = GetQuerystringParam("symbol");
            string filetype = GetQuerystringParam("filetype");
            if (!string.IsNullOrEmpty(symbol))
            {
                string strBase64Data = string.Empty;
                if (!string.IsNullOrEmpty(REQUESTCONTENT))
                {

                    byte[] byteData = class_CommonUtil.Decoder_Base64ToByte(strBase64Data);
                    Object_CommonData.PrepareDataOperation();
                    class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
                    activeSPEntry_binData.ClearAllParamsValues();
                    activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                    DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                    if (binDataTable.Rows.Count == 0)
                    {
                        activeSPEntry_binData.ClearAllParams();
                        activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                        activeSPEntry_binData.ModifyParameterValue("@type", filetype);
                        activeSPEntry_binData.ModifyParameterValue("@owner", "sys");
                        activeSPEntry_binData.ModifyParameterValue("@data", byteData);
                        Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                    }
                    else
                    {
                        DataRow activeDataRow = null;
                        class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                        string id = string.Empty;
                        class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                        if (!string.IsNullOrEmpty(id))
                        {
                            activeSPEntry_binData.ClearAllParams();
                            activeSPEntry_binData.ModifyParameterValue("@id", id);
                            activeSPEntry_binData.ModifyParameterValue("@data", byteData);
                            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                        }
                        else
                            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
                    }
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");

            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}