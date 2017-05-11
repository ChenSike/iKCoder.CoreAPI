using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Message_api_iKCoder_Workspace_Get_MessageList : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", logined_user_name);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        int index = 1;        
        foreach (DataRow activeDataRow in activeDataTable.Rows.Cast<DataRow>().OrderBy(r => DateTime.Parse(r["crreated"].ToString())))
        {
            string messageid = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "messageid",out messageid);
            string operationid = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out operationid);
            string messagetype = string.Empty;
            string isread = string.Empty;
            string istop = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "issys", out messagetype);
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "isread", out isread);
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "istop", out istop);
            string requestAPI = "/Message/api_Message_GetMessage.aspx&id = " + messageid;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024*1024);
            XmlDocument returnDoc = new XmlDocument();
            returnDoc.LoadXml(returnStrDoc);
            XmlNode msgNode = returnDoc.SelectSingleNode("/root/msg");
            string messageContent = class_XmlHelper.GetAttrValue(msgNode, "message");
            string messageUsername = class_XmlHelper.GetAttrValue(msgNode, "username");
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("message", messageContent);
            attrs.Add("index", index.ToString());
            attrs.Add("username", messageUsername);
            attrs.Add("isread", isread);
            attrs.Add("operationid", operationid);
            attrs.Add("messageid", messageid);
            attrs.Add("istop", istop);
            
            activeSPEntry_resourceMesssage.ClearAllParamsValues();
            activeSPEntry_resourceMesssage.ModifyParameterValue("@isread", "1");
            activeSPEntry_resourceMesssage.ModifyParameterValue("@messageid", operationid);
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_resourceMesssage, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
            index++;
        }
    }
}