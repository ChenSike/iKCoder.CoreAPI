using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_SetRemoveScheduleItem : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string classsymbol = GetQuerystringParam("classsymbol");
        if(string.IsNullOrEmpty(classsymbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->classsymbol", "");
            return;
        }
        string guid = GetQuerystringParam("guid");
        if (string.IsNullOrEmpty(guid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->guid", "");
            return;
        }
        class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
    }
}