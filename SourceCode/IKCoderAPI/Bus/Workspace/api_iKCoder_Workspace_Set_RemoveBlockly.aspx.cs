using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_RemoveBlockly : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string senceSymbol = GetQuerystringParam("symbol");
        string stage = GetQuerystringParam("stage");
        string optionHeader = GetQuerystringParam("header");
        if (string.IsNullOrEmpty(senceSymbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
            return;
        }
        if (string.IsNullOrEmpty(stage))
            stage = "1";
        string symbol = string.Empty;
        if (string.IsNullOrEmpty(optionHeader))
            symbol = class_Bus_ToolboxDoc.GetToolboxSymbol(symbol, stage);
        else
            symbol = class_Bus_ToolboxDoc.GetToolboxOptionalSymbol(optionHeader, symbol, stage);
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_configBlockly = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_BLOCKLY);
        activeSPEntry_configBlockly.ClearAllParamsValues();
        activeSPEntry_configBlockly.ModifyParameterValue("@symbol", symbol);
        Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configBlockly, class_CommonDefined.enumDataOperaqtionType.delete.ToString(), this.GetType());
    }
}