using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_iKCoder_Data_Set_RemoveBinData : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
            activeSPEntry_binData.ClearAllParamsValues();
            activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
            if( Object_CommonData.Object_SqlHelper.ExecuteDeleteConditionSP(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer))
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "true", "", enum_MessageType.Exception);
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "", enum_MessageType.Exception);
        }
    }
}