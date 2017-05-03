using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Message_api_Message_GetMessage : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);
        if (REQUESTDOCUMENT != null && !string.IsNullOrEmpty(REQUESTDOCUMENT.OuterXml))
        {
            string id = string.Empty;
            id = GetQuerystringParam("id");
            if (string.IsNullOrEmpty(id))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "id->empty", "");
                return;
            }
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_content");
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            DataTable activeDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (activeDataTable != null && activeDataTable.Rows.Count > 0)
            {
                foreach (DataRow activeRow in activeDataTable.Rows)
                {
                    string message = string.Empty;
                    string title = string.Empty;
                    string username = string.Empty;
                    string createtime = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "message", out message);
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "title", out title);
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "username", out username);
                    class_Data_SqlDataHelper.GetColumnData(activeRow, "createtime", out createtime);
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("message", message);
                    attrs.Add("title", title);
                    attrs.Add("username", username);
                    attrs.Add("createtime", createtime);
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
                }
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "no data", "");
                return;
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "request->empty", "");
            return;
        }
    }
}