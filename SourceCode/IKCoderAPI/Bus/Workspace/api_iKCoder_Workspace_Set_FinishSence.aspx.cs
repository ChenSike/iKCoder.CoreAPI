using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_FinishSence : class_WebBase_IKCoderAPI_UA
{
    XmlDocument sourceDoc_profile = new XmlDocument();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            XmlDocument resourceDoc_studystatus = Object_ProfileDocs.GetProfileDocObject(logined_user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
            Object_ProfileDocs.SetFinishSence(logined_user_name, symbol, resourceDoc_studystatus);
            Object_ProfileDocs.SetSenceTimeLine(logined_user_name, symbol, resourceDoc_studystatus);
            List<string> finishList = Object_ProfileDocs.GetFinishedSymbols(logined_user_name);
            class_Bus_Title objectTitle = new class_Bus_Title(Object_CommonData);
            string title = objectTitle.GetTitle(finishList);
            Object_ProfileDocs.SetFlushBasicTitle(logined_user_name, title);
            AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
        }
    }
}