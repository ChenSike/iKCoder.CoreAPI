using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetClassroomList : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        class_Bus_ClassRoom objectClassroom = new class_Bus_ClassRoom(Object_CommonData, logined_centersymbol);
        switchResponseMode(enumResponseMode.text);
        Dictionary<string, string> resultLst = objectClassroom.GetClassroomsList();
        foreach(string id in resultLst.Keys)
        {
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("id", id);
            attrs.Add("symbol", resultLst[id]);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
        }
    }
}