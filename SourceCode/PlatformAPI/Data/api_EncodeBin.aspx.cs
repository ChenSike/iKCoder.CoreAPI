using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iKCoder_Platform_SDK_Kit;

public partial class Data_api_EncodeBin : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        byte[] dataBuffer = new byte[Request.InputStream.Length];
        try
        {
            Request.InputStream.Read(dataBuffer,0,dataBuffer.Length);
            string result = System.Text.Encoding.Default.GetString(dataBuffer);
            string resultBase64 = class_CommonUtil.Decoder_Base64(result);
            Dictionary<string, string> attrList = new Dictionary<string, string>();
            attrList.Add("data", resultBase64);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrList);
        }
        catch
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : can not read data from request stream.", "");
        }
    }    
}