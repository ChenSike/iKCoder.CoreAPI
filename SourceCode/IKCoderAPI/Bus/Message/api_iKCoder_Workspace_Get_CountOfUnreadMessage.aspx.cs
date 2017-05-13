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
    protected XmlDocument sourceDoc_profile = new XmlDocument();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        sourceDoc_profile = class_Bus_ProfileDoc.GetProfileDocument(Server_API, Virtul_Folder_API, Object_NetRemote, logined_user_name);
        class_Bus_Message messageLogicObject = new class_Bus_Message(sourceDoc_profile);
        messageLogicObject.checkMessageStatus();
        class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", "all");
        DataTable activeDTResourceMessage = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        int countOfmessage = 0;
        if (activeDTResourceMessage != null)
        {
            foreach (DataRow activeDataRow in activeDTResourceMessage.Rows)
            {
                string operationID = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out operationID);
                if (!messageLogicObject.isRead(operationID))
                    countOfmessage++;
            }
        }
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", logined_user_name);
        activeSPEntry_resourceMesssage.ModifyParameterValue("@isread", "0");
        activeDTResourceMessage = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDTResourceMessage != null)
            countOfmessage = countOfmessage + activeDTResourceMessage.Rows.Count;
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), countOfmessage.ToString(), "");
    }
}