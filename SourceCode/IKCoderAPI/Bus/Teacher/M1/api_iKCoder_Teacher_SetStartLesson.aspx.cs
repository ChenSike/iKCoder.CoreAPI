using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_Teacher_api_iKCoder_Teacher_SetStartLesson : class_WebBase_IKCoderAPI_UA_Teacher
{
    protected override void ExtendedAction()
    {
        string classsymbol = GetQuerystringParam("classsymbol");
        
    }
}