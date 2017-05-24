using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Message_api_iKCoder_Workspace_Set_RemoveMessage : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string messageid = GetQuerystringParam("id");
        string operationid = GetQuerystringParam("operationid");
        Object_CommonData.PrepareDataOperation();
        if (string.IsNullOrEmpty(operationid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "messageid->empty", enum_MessageType.Exception);
            return;
        }
        Object_ProfileDocs.SetMessageToRMV(logined_user_name, operationid);
        string requestAPI = "/Message/api_Message_GetMessage.aspx?id=" + messageid;
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument returnDoc = new XmlDocument();
        returnDoc.LoadXml(returnStrDoc);
        XmlNode msgNode = returnDoc.SelectSingleNode("/root/msg");
        class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        if (msgNode == null)
        {
            activeSPEntry_resourceMesssage.ClearAllParamsValues();
            activeSPEntry_resourceMesssage.ModifyParameterValue("@id", operationid);
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_resourceMesssage, class_CommonDefined.enumDataOperaqtionType.delete.ToString(), this.GetType());           
        }
        Object_ProfileDocs.SetMessageToRMV(logined_user_name, operationid);
    }
}