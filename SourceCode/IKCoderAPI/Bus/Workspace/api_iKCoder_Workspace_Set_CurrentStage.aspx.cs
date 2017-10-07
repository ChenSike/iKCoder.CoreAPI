using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_CurrentStage : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        string futurestage = GetQuerystringParam("stage");
        string symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(futurestage))
        {
            Object_CommonData.PrepareDataOperation();
            class_Bus_ProfileDocs Object_ProfileDocs = new class_Bus_ProfileDocs(ref Object_CommonData);
            Object_ProfileDocs.SetCurrentStage(logined_user_name, symbol, futurestage);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty stage in querystring", "");

    }
}