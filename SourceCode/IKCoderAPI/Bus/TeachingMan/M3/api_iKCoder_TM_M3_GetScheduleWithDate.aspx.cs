using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_GetScheduleWithDate : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string startDate = GetQuerystringParam("startdate");
        string enddate = GetQuerystringParam("enddate");
        if(string.IsNullOrEmpty(startDate))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->startdate", "");
            return;
        }
        if(string.IsNullOrEmpty(enddate))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->enddate", "");
            return;
        }
        DateTime dtStartDate;
        DateTime dtEndDate;
        DateTime.TryParse(startDate, out dtStartDate);
        DateTime.TryParse(enddate, out dtEndDate);
        class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        XmlDocument resultDoc = objectClass.GetScheduleWithDateForClassroom(dtStartDate, dtEndDate);
        switchResponseMode(enumResponseMode.text);
        RESPONSEDOCUMENT.LoadXml(resultDoc.OuterXml);
    }
}