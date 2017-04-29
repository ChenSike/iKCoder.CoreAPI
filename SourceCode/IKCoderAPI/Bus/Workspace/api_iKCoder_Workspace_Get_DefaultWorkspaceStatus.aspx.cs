using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Bus_Workspace_api_iKCoder_Workspace_Get_WorkspaceStatus : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            string senceSymbol = GetQuerystringParam("symbol");
            string stage = GetQuerystringParam("stage");
            string fullySymbol = GetQuerystringParam("fullysymbol");
            if (string.IsNullOrEmpty(senceSymbol))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                return;
            }
            string symbol = string.Empty;
            if (!string.IsNullOrEmpty(fullySymbol))
            {
                symbol = fullySymbol;
            }
            else
            {
                if (string.IsNullOrEmpty(stage))
                    stage = "1";
                symbol = class_Bus_WorkspaceStatus.GetWorkspaceStatusDefaultSymbol(symbol, stage);
            }
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configSP = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_WORKSPACESTATUS);
            activeSPEntry_configSP.ClearAllParamsValues();
            activeSPEntry_configSP.ModifyParameterValue("@symbol", symbol);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSP, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            foreach (DataRow activeRow in textDataTable.Rows)
            {
                string config = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeRow, "config", out config);
                config = class_CommonUtil.Decoder_Base64(config);
                RESPONSEDOCUMENT = new XmlDocument();
                RESPONSEDOCUMENT.LoadXml(config);
            }
        }
    }
}