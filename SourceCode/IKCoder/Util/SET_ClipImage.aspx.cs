using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Drawing;
using System.Text;

public partial class Util_SET_ClipImage : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        string tmpBinResourceSymbol = GetQuerystringParam("tmpsymbol");
        string clipedBinResourceSymbol = GetQuerystringParam("symbol");
        int startX = 0;
        int startY = 0;
        int width = 0;
        int height = 0;
        int.TryParse(GetQuerystringParam("startx"), out startX);
        int.TryParse(GetQuerystringParam("starty"), out startY);
        int.TryParse(GetQuerystringParam("width"), out width);
        int.TryParse(GetQuerystringParam("height"), out height);
        if(!string.IsNullOrEmpty(tmpBinResourceSymbol))
        {
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetBinDataWithBase64.aspx?symbol=" + tmpBinResourceSymbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(returnDoc);            
            XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
            if (msgNode != null)
            {
                string data = class_XmlHelper.GetAttrValue(msgNode, "data");
                string type = class_XmlHelper.GetAttrValue(msgNode, "type");
                if (!string.IsNullOrEmpty(data))
                {
                    ISBINRESPONSE = false;
                    ISRESPONSEDOC = true;
                    byte[] imageData = class_CommonUtil.Decoder_Base64ToByte(data);
                    Bitmap sourceBitMap = class_Util_Drawing.CreateImage(imageData);
                    Bitmap clippedBitMap = class_Util_Drawing.ClipImage(sourceBitMap, startX, startY, width, height);
                    string base64ImageData = class_Util_Drawing.GetBase64FromBitmap(clippedBitMap, type);                    
                    URL = Server_API + Virtul_Folder_API + "/Data/api_SetOverBinBase64Data.aspx?cid=" + cid;
                    StringBuilder inputStr = new StringBuilder();
                    inputStr.Append("<root>");
                    inputStr.Append("<symbol>");
                    inputStr.Append(clipedBinResourceSymbol);
                    inputStr.Append("</symbol>");
                    inputStr.Append("<type>");
                    inputStr.Append(type);
                    inputStr.Append("</type>");
                    inputStr.Append("<data>");
                    inputStr.Append(base64ImageData);
                    inputStr.Append("</data>");
                    inputStr.Append("</root>");
                    returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputStr.ToString(), URL, 1000 * 60, 1024 * 1024);
                    if (returnDoc.Contains("err"))
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : symbol existed or guid existed.", "");
                    }
                    else
                    {
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                    }                    
                }
            }
        }
    }
}