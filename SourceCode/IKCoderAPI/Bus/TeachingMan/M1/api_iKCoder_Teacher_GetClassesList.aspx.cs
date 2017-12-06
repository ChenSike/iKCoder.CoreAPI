using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetClassesList : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        XmlDocument resultDoc = objectClass.GetClassesDocument();
        switchResponseMode(enumResponseMode.text);
        RESPONSEDOCUMENT.LoadXml(resultDoc.OuterXml);
    }
}