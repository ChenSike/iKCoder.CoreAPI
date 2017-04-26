using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Workspace_api_iKCoder_Data_Set_WorkspaceStatus : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null)
        {
            string senceSymbol = GetQuerystringParam("symbol");
            string stage = GetQuerystringParam("stage");
            if (string.IsNullOrEmpty(senceSymbol))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                return;
            }
            if (string.IsNullOrEmpty(stage))
                stage = "1";
            string symbol = senceSymbol + "_" + "s" + stage + "_" + logined_user_name;
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_WORKSPACESTATUS);
            activeSPEntry_configSence.ClearAllParamsValues();
            activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (textDataTable != null && textDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                string id = string.Empty;
                activeSPEntry_configSence.ClearAllParamsValues();
                activeSPEntry_configSence.ModifyParameterValue("@id", id);
                activeSPEntry_configSence.ModifyParameterValue("@config", REQUESTDOCUMENT.OuterXml);
                Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
            }
            else
            {
                activeSPEntry_configSence.ClearAllParams();
                activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
                activeSPEntry_configSence.ModifyParameterValue("@config", REQUESTDOCUMENT.OuterXml);
                Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
            }
        }
    }
}