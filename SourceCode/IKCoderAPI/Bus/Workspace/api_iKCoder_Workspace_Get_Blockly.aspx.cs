using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Workspace_api_iKCoder_Workspace_Get_Blockly : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            switchResponseMode(enumResponseMode.text);
            string senceSymbol = GetQuerystringParam("symbol");
            string stage = GetQuerystringParam("stage");
            string optionHeader = GetQuerystringParam("header");
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
                if (string.IsNullOrEmpty(optionHeader))
                    symbol = class_Bus_ToolboxDoc.GetToolboxSymbol(senceSymbol, stage);
                else
                    symbol = class_Bus_ToolboxDoc.GetToolboxOptionalSymbol(optionHeader, senceSymbol, stage);
            }
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_configBlockly = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_BLOCKLY);
            activeSPEntry_configBlockly.ClearAllParamsValues();
            activeSPEntry_configBlockly.ModifyParameterValue("@symbol", symbol);
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configBlockly, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            foreach (DataRow activeRow in textDataTable.Rows)
            {
                string config = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeRow, "config", out config);
                config = class_CommonUtil.Decoder_Base64(config);
                RESPONSEDOCUMENT = new XmlDocument();
                RESPONSEDOCUMENT.LoadXml(config);
            }

        }
    }
}