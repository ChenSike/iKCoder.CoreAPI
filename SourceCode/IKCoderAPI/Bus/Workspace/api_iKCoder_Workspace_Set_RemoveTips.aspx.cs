using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_RemoveTips : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
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
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configBlockly, class_CommonDefined.enumDataOperaqtionType.deletecondition.ToString(), this.GetType());
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}