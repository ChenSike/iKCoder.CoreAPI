using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Data_api_EncodeText : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {             
            string data = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/data");
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api, class_CommonDefined.enumExecutedCode.executed.ToString(), data, "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to execute api : api_OperationMetaTextData.", "");
        }
    }    
}