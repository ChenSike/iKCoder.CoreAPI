using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;


public partial class Bus_Teacher_api_iKCoder_Teacher_GetClassesList : class_WebBase_IKCoderAPI_UA_Teacher
{
    protected override void ExtendedAction()
    {
        class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        objectClasses.GetLoadClasses();
        List<class_Bus_ClassesItem> list = objectClasses.GetClassesListForTeacher(logined_user_name);
        switchResponseMode(enumResponseMode.text);
        RESPONSEDOCUMENT.LoadXml("<root></root>");
        foreach(class_Bus_ClassesItem activeItem in list)
        {
            XmlNode newItem = class_XmlHelper.CreateNode(RESPONSEDOCUMENT, "item", "");
            class_XmlHelper.SetAttribute(newItem, "symbol", activeItem.symbol);
            RESPONSEDOCUMENT.SelectSingleNode("/root").AppendChild(newItem);
        }
    }
}