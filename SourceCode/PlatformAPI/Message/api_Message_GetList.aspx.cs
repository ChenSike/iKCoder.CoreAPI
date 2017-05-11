using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Message_api_Message_GetList : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);        
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_content");
        activeSPEntry.ClearAllParamsValues();        
        DataTable activeDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if(activeDataTable!=null && activeDataTable.Rows.Count>0)
        {
            foreach(DataRow activeRow in activeDataTable.Rows)
            {
                string id = string.Empty;
                string title = string.Empty;
                string message = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeRow, "title", out title);
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeRow, "message",out message);
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("id", id);
                attrs.Add("title", title);
                attrs.Add("message", message);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "request->empty", "");
            return;
        }
    }
}