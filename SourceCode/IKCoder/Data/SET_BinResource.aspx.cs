using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;
using System.Xml;

public partial class Data_SET_BinResource : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;        
        string symbol = GetQuerystringParam("symbol");
        string filetype = GetQuerystringParam("filetype");
        if (!string.IsNullOrEmpty(symbol))
        {
            HttpFileCollection requestFiles = Request.Files;
            HttpPostedFile postFile = requestFiles[0];
            if (postFile != null)
            {
                byte[] binBuffer = new byte[postFile.InputStream.Length];
                postFile.InputStream.Read(binBuffer, 0, (int)postFile.InputStream.Length);
                string binData64 = class_CommonUtil.Encoder_Base64(binBuffer);
                string URL = Server_API + Virtul_Folder_API + "/Data/api_SetOverBinBase64Data.aspx?cid=" + cid;
                StringBuilder inputStr = new StringBuilder();
                inputStr.Append("<root>");
                inputStr.Append("<symbol>");
                inputStr.Append(symbol);
                inputStr.Append("</symbol>");
                inputStr.Append("<type>");
                inputStr.Append(filetype);
                inputStr.Append("</type>");
                inputStr.Append("<data>");
                inputStr.Append(binData64);
                inputStr.Append("</data>");
                inputStr.Append("</root>");
                string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputStr.ToString(), URL, 1000 * 60, 1024 * 1024);
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