using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_Profile_GET_CurrentWorkSpace : class_WebBase_UA
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        string senceName = GetQuerystringParam("name");
        if(string.IsNullOrEmpty(senceName))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "sence name->empty", "", enum_MessageType.Exception);        
        }
        if(REQUESTDOCUMENT!=null)
        {
            string resourceSymbol = class_CommonDefined._ResourceWorkspaceFlagSymbol + logined_user_name + "_" + senceName;
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetBinDataWithBase64.aspx?cid=" + cid + "&symbol=" + resourceSymbol;
            string strReturnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            XmlDocument dataDoc = new XmlDocument();
            dataDoc.LoadXml(strReturnDoc);
            XmlNode msgNode = dataDoc.SelectSingleNode("/root/msg");
            if(msgNode!=null)
            {
                string data = class_XmlHelper.GetAttrValue(msgNode, "data");
                string xmlData = class_CommonUtil.Decoder_Base64(data);                
                RESPONSEDOCUMENT.LoadXml(xmlData);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Workspace_Load"), "");   
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Workspace_Load"), "");   
        }
    }
}