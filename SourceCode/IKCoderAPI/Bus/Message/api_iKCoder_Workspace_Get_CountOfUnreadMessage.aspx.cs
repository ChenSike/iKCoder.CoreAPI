using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Message_api_iKCoder_Workspace_Get_CountOfUnreadMessage : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", "*");
        activeSPEntry_resourceMesssage.ModifyParameterValue("@isread", "0");
        DataTable activeDTResourceMessage = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        int countOfmessage = 0;
        if (activeDTResourceMessage!=null)
            countOfmessage = activeDTResourceMessage.Rows.Count;
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", User);
        activeSPEntry_resourceMesssage.ModifyParameterValue("@isread", "0");
        activeDTResourceMessage = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDTResourceMessage != null)
            countOfmessage = countOfmessage + activeDTResourceMessage.Rows.Count;
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), countOfmessage.ToString(), "");
    }
}