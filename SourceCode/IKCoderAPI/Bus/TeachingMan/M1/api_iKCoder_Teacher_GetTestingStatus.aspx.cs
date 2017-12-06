using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetTestingStatus : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("classsymbol");
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->symbol", "");
        }
        else
        {
            switchResponseMode(enumResponseMode.text);
            class_Bus_Testing objectHomework = new class_Bus_Testing(Object_CommonData, logined_centersymbol);
            RESPONSEDOCUMENT.LoadXml(objectHomework.GetStatusForAllStudentsWithClass(symbol).OuterXml);
        }
    }
}