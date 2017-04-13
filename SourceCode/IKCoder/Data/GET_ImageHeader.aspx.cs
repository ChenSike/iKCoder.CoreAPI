using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Text;
using System.Xml;

public partial class Data_GET_ImageHeader : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        string symbol = "img_header_" + Session["logined_user_name"].ToString();
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Param : symbol -> Empty", "", enum_MessageType.Exception);
        }
        else
        {
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetVerifySymbolExisted.aspx?symbol=" + symbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            if (returnDoc.Contains("false"))
            {
                symbol = "img_header_default";
            }
            URL = Server_API + Virtul_Folder_API + "/Data/api_GetBinDataWithBase64.aspx?symbol=" + symbol;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(returnDoc);
            XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
            if (msgNode != null)
            {
                string data = class_XmlHelper.GetAttrValue(msgNode, "data");
                if (!string.IsNullOrEmpty(data))
                {
                    ISBINRESPONSE = true;
                    ISRESPONSEDOC = false;
                    RESPONSEBUFFER = class_CommonUtil.Decoder_Base64ToByte(data);
                }
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Data From Result Document.", "", enum_MessageType.Exception);

                }
            }

        }
    }
}