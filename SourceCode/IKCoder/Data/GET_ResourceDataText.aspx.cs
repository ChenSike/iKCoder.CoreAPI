using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Data_GET_ResourceDataText : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        string symbol = GetQuerystringParam("symbol");
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Param : symbol -> Empty", "", enum_MessageType.Exception);
        }
        else
        {
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?symbol=" + symbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(returnDoc);
            XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
            if (msgNode != null)
            {
                string data = class_XmlHelper.GetAttrValue(msgNode, "msg");
                if (!string.IsNullOrEmpty(data))
                {
                    string strDoc = class_CommonUtil.Decoder_Base64(data);
                    RESPONSEDOCUMENT.LoadXml(strDoc);
                }
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Data From Result Document.", "", enum_MessageType.Exception);

                }
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Data.", "", enum_MessageType.Exception);
            }
        }
    }
}