using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_GetSchedule_aspx : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->symbol", "");
        }
        else
        {
            class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            XmlDocument doc_schedule = objectClass.GetClassScheduleDoc(symbol);
            switchResponseMode(enumResponseMode.text);
            RESPONSEDOCUMENT.LoadXml(doc_schedule.OuterXml);
        }
    }
}