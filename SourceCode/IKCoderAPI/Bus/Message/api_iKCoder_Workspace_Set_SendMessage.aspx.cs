using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_Message_api_iKCoder_Workspace_Set_SendMessage : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            string messageID;
            string username;
            string isread = "0";
            string issys = "0";
            string istop = "0";
            messageID = GetQuerystringParam("messageid");
            username = GetQuerystringParam("to");
            issys = GetQuerystringParam("type");
            istop = GetQuerystringParam("istop");
            if(string.IsNullOrEmpty(messageID))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "MessageID->Empty", "");
                return;
            }
            if(string.IsNullOrEmpty(username))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "to->Empty", "");
                return;
            }
            if(username=="*")
                issys = "1";
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
            activeSPEntry_resourceMesssage.ClearAllParamsValues();
            activeSPEntry_resourceMesssage.ModifyParameterValue("@messageid", messageID);
            activeSPEntry_resourceMesssage.ModifyParameterValue("@username", username);
            activeSPEntry_resourceMesssage.ModifyParameterValue("@isread", isread);
            activeSPEntry_resourceMesssage.ModifyParameterValue("@issys", issys);
            activeSPEntry_resourceMesssage.ModifyParameterValue("@istop", istop);
            activeSPEntry_resourceMesssage.ModifyParameterValue("@created", DateTime.Now.ToString());
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_resourceMesssage, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());                
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Access To Operation", "");
        }
    }
}