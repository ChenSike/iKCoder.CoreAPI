using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_Teacher_api_iKCoder_Teacher_GetClassScheduleContent : class_WebBase_IKCoderAPI_UA_Teacher
{
    protected override void ExtendedAction()
    {
        string classSymbol = GetQuerystringParam("symbol");
        switchResponseMode(enumResponseMode.text);
        class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        objectClasses.GetLoadClasses();
        class_Bus_ClassesItem activeItem = objectClasses.GetExsitedItem(classSymbol);
        RESPONSEDOCUMENT.LoadXml(activeItem.doc_schedule.OuterXml);
    }
}