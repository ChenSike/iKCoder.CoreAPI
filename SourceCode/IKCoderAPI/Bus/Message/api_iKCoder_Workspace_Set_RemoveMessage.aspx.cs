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
        string messageid = GetQuerystringParam("id");
        string operationid = GetQuerystringParam("operationid");
        if (string.IsNullOrEmpty(operationid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "operationid->empty", enum_MessageType.Exception);
            return;
        }
        if (string.IsNullOrEmpty(messageid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "messageid->empty", enum_MessageType.Exception);
            return;
        }
        string URL = Server_API + Virtul_Folder_API + "/Message/api_Message_RemoveMessage.aspx?id=" + messageid;
        string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml(returnDoc);
        XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
        if (msgNode != null)
        {
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
            activeSPEntry_resourceMesssage.ClearAllParamsValues();
            activeSPEntry_resourceMesssage.ModifyParameterValue("@id", operationid);
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_resourceMesssage, class_CommonDefined.enumDataOperaqtionType.delete.ToString(), this.GetType());
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "", enum_MessageType.Exception);
        }
    }
}