using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Util_SET_ClipImage : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        string tmpBinResourceSymbol = GetQuerystringParam("tmpsymbol");
        string clipedBinResourceSymbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty( tmpBinResourceSymbol))
        {
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetBinDataWithBase64.aspx?symbol=" + tmpBinResourceSymbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
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
                    byte[] imageData = class_CommonUtil.Decoder_Base64ToByte(data);
                    
                }
            }
        }
    }
}