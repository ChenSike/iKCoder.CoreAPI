using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Message_api_Message_Send : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);
        string messageid = GetQuerystringParam("messageid");
        string toproduce = GetQuerystringParam("produce");
        string to = GetQuerystringParam("to");
        if(string.IsNullOrEmpty(messageid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "messageid->empty", "");
            return;
        }
        if(string.IsNullOrEmpty(toproduce))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "produce->empty", "");
            return;
        }
        if(string.IsNullOrEmpty(to))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "to->empty", "");
            return;
        }
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_router");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@produce", toproduce);
        DataTable activeDTRouter = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        string pushapi = string.Empty;
        if (activeDTRouter!=null && activeDTRouter.Rows.Count >0 )
        {
            DataRow activeRow = null;
            class_Data_SqlDataHelper.GetActiveRow(activeDTRouter, 0, out activeRow);
            class_Data_SqlDataHelper.GetColumnData(activeRow, "pushapi", out pushapi);
        }
        if(string.IsNullOrEmpty(pushapi))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "router->empty", "");
            return;
        }
        string paramerters = "&messageid=" + messageid + "&to=" + to;
        string url = pushapi + paramerters;
        class_Net_RemoteRequest netObject = new class_Net_RemoteRequest();
        netObject.getRemoteRequestToStringWithCookieHeader("<root></root>", url, 60, 1024 * 1024);
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "sent","");
    }
}