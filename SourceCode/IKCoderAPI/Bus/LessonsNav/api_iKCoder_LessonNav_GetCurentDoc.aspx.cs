using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_LessonsNav_api_iKCoder_LessonNav_GetCurentDoc : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string lessonSymbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(lessonSymbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No Symbol", "");
        }
        class_Bus_LessonNAV objectLessonNAV = new class_Bus_LessonNAV(Object_CommonData);
        RESPONSEDOCUMENT.LoadXml(objectLessonNAV.GetLessonCurrentNAVDoc(lessonSymbol).OuterXml);
    }
}