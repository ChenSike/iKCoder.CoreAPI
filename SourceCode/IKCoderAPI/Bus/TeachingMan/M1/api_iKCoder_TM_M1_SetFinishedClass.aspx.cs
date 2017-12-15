using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_SetFinishedClass : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty->symbol", "");
        }
        else
        {
            class_Bus_Classes classObject = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            classObject.SetSwicthActiveClassToFinished(symbol);
        }
    }
}