using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Account_Profile_SET_CurrentWorkSpace : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        string senceName = GetQuerystringParam("name");
        if(string.IsNullOrEmpty(senceName))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "sence name->empty", "", enum_MessageType.Exception);        
        }
        if(REQUESTDOCUMENT!=null)
        {
            string resourceSymbol = class_CommonDefined._ResourceWorkspaceFlagSymbol + logined_user_name + "_" + senceName;
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetVerifySymbolExisted.aspx?cid=" + cid + "&symbol=" + resourceSymbol;
            string strReturnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            string dataBase64 = class_CommonUtil.Encoder_Base64(REQUESTDOCUMENT.OuterXml);
            string inputDoc = "<root><symbol>" + resourceSymbol + "</symbol><data>" + dataBase64 + "</data></root>";
            if(strReturnDoc.Contains("true"))
            {
                URL = Server_API + Virtul_Folder_API + "/Data/api_UpdateMetaTextData.aspx?cid=" + cid;
                strReturnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc, URL, 1000 * 60, 100000);
                if(URL.Contains("true"))                
                    AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Msg_Workspace_Save"), "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Workspace_Save"), "");                  
            }
            else
            {
                URL = Server_API + Virtul_Folder_API + "/Data/api_SetMetaTextData.aspx?cid=" + cid;
                strReturnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc, URL, 1000 * 60, 100000);
                if (URL.Contains("true"))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Msg_Workspace_Save"), "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Workspace_Save"), "");   
            }           
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Workspace_Save"), "");   

        }
    }
}