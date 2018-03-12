using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_GetLessonsList : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        string classSymbol = GetQuerystringParam("classsymbol");
        if(string.IsNullOrEmpty(classSymbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->classsymbol", "");
            return;
        }
        class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        string typeSymbol = objectClasses.GetClassLevel(classSymbol);
        class_Bus_Lessons objectLessons = new class_Bus_Lessons(Object_CommonData);
        objectLessons.GetLessonsList(typeSymbol,ref RESPONSEDOCUMENT);
        switchResponseMode(enumResponseMode.text);
    }
}