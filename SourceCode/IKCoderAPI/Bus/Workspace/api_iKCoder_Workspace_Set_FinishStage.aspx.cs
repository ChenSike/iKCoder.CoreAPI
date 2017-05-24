using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_FinishStage : class_WebBase_IKCoderAPI_UA
{
    XmlDocument sourceDoc_profile = new XmlDocument();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            XmlDocument resourceDoc_studyStatus = Object_ProfileDocs.GetProfileDocObject(logined_user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
            string currentStage = Object_ProfileDocs.GetCurrentStage(logined_user_name, symbol);
            Object_ProfileDocs.SetFinishItemStage(logined_user_name, symbol, currentStage, resourceDoc_studyStatus);
            Object_ProfileDocs.SetCodetimeLineHours(logined_user_name, symbol);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
        }
    }
}