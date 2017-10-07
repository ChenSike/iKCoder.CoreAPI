using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Bus_Message_api_iKCoder_Workspace_Get_SysMessageList : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        class_Bus_ProfileDocs Object_ProfileDocs = new class_Bus_ProfileDocs(ref Object_CommonData);
        class_Data_SqlSPEntry activeSPEntry_resourceMesssage = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        int index = 1;
        activeSPEntry_resourceMesssage.ClearAllParamsValues();
        activeSPEntry_resourceMesssage.ModifyParameterValue("@username", "all");
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry_resourceMesssage, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        foreach (DataRow activeDataRow in activeDataTable.Rows.Cast<DataRow>().OrderBy(r => DateTime.Parse(r["created"].ToString())))
        {
            string messageid = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "messageid", out messageid);
            string operationid = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out operationid);
            string messagetype = string.Empty;
            string isread = string.Empty;
            string time = string.Empty;
            string istop = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "issys", out messagetype);
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "created", out time);
            isread = Object_ProfileDocs.GetMessageIsRead(logined_user_name,operationid) ? "1" : "0";
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "istop", out istop);
            string requestAPI = "/Message/api_Message_GetMessage.aspx?id=" + messageid;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument returnDoc = new XmlDocument();
            returnDoc.LoadXml(returnStrDoc);
            XmlNode msgNode = returnDoc.SelectSingleNode("/root/msg");
            if (msgNode != null)
            {
                string messageContent = class_XmlHelper.GetAttrValue(msgNode, "message");
                string messageUsername = class_XmlHelper.GetAttrValue(msgNode, "username");
                if (!Object_ProfileDocs.GetMessageIsRMV(logined_user_name,operationid))
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("message", messageContent);
                    attrs.Add("index", index.ToString());
                    attrs.Add("username", messageUsername);
                    attrs.Add("isread", isread);
                    attrs.Add("operationid", operationid);
                    attrs.Add("messageid", messageid);
                    attrs.Add("istop", istop);
                    attrs.Add("messagetype", messagetype);
                    attrs.Add("datetime", time);
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
                    index++;
                    Object_ProfileDocs.SetMessageToRead(logined_user_name, operationid);
                }
            }
        }
    }
}