﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_Tips : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            if (REQUESTDOCUMENT != null)
            {
                string senceSymbol = GetQuerystringParam("symbol");
                if (string.IsNullOrEmpty(senceSymbol))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                    return;
                }                
                Object_CommonData.PrepareDataOperation();
                class_Data_SqlSPEntry activeSPEntry_configBlockly = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TIPS);
                activeSPEntry_configBlockly.ClearAllParamsValues();
                activeSPEntry_configBlockly.ModifyParameterValue("@symbol", senceSymbol);
                DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configBlockly, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                string base64Doc = class_CommonUtil.Encoder_Base64(REQUESTDOCUMENT.OuterXml);
                if (textDataTable != null && textDataTable.Rows.Count > 0)
                {
                    DataRow activeDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                    string id = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                    activeSPEntry_configBlockly.ClearAllParamsValues();
                    activeSPEntry_configBlockly.ModifyParameterValue("@id", id);
                    activeSPEntry_configBlockly.ModifyParameterValue("@config", base64Doc);
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configBlockly, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
                else
                {
                    activeSPEntry_configBlockly.ClearAllParamsValues();
                    activeSPEntry_configBlockly.ModifyParameterValue("@symbol", senceSymbol);
                    activeSPEntry_configBlockly.ModifyParameterValue("@config", base64Doc);
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configBlockly, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}