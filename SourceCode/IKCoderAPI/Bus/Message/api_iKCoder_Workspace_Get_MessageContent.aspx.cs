using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Message_api_iKCoder_Workspace_Get_MessageContent : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        string messageid = GetQuerystringParam("id");
        string URL = Server_API + Virtul_Folder_API + "/Message/api_Message_GetMessage.aspx?id=" + messageid;
        string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml(returnDoc);
        XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
        if (msgNode != null)
        {
            string message = class_XmlHelper.GetAttrValue(msgNode, "message");
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), message,"");
        }
    }
}