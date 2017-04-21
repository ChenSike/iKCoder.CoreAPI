using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;

public partial class Bus_Workspace_SET_WorkspaceStatus : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        string symbol = GetQuerystringParam("symbol");
        string stage = GetQuerystringParam("stage");
        if (!string.IsNullOrEmpty(symbol) && !string.IsNullOrEmpty(stage))
        {
            if (REQUESTDOCUMENT != null)
            {
                string base64data = class_CommonUtil.Encoder_Base64(REQUESTDOCUMENT.OuterXml);
                string workspaceStatusSymbol = class_WorkspaceProcess._PreWorkspace + symbol + "_" + "s" + stage + "_" + Session["logined_user_name"].ToString();
                StringBuilder inputStr = new StringBuilder();
                inputStr.Append("<root>");
                inputStr.Append("<symbol>");
                inputStr.Append(workspaceStatusSymbol);
                inputStr.Append("</symbol>");
                inputStr.Append("<type>");
                inputStr.Append("text");
                inputStr.Append("</type>");
                inputStr.Append("<data>");
                inputStr.Append(base64data);
                inputStr.Append("</data>");
                inputStr.Append("</root>");
                string URL = Server_API + Virtul_Folder_API + "/Data/api_SetOverMetaTextData.aspx?cid=" + ClientSymbol + "&symbol=" + workspaceStatusSymbol;
                string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputStr.ToString(), URL, 1000 * 60, 1024 * 1024);
                if(!returnDoc.Contains("err"))
                {
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
                }

            }
        }
        else
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "false", "");
    }
}