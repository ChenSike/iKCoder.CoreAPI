using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetTeachersList : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        class_Bus_Teacher objectTeacher = new class_Bus_Teacher(Object_CommonData);
        Dictionary<string, string> lstTeachers = objectTeacher.GetTeachersForCenter(logined_centersymbol);
        switchResponseMode(enumResponseMode.text);
        foreach(string id in lstTeachers.Keys)
        {
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("id", id);
            attrs.Add("symbol", lstTeachers[id]);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
        }
    }
}