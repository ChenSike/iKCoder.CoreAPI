using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Teacher_api_iKCoder_Teacher_GetLessonListForCurrentDate : class_WebBase_IKCoderAPI_UA_Teacher
{
    protected override void ExtendedAction()
    {

        class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        List<XmlNode> currrentClasses = objectClasses.GetExtstedScheduleItemsForCurrentDate(DateTime.Now.ToString("yyyy-MM-dd"));
        if (currrentClasses.Count > 0)
        {
            switchResponseMode(enumResponseMode.text);
            RESPONSEDOCUMENT.LoadXml("<root></root>");
            foreach (XmlNode activeItem in currrentClasses)
            {

                XmlNode newItem = class_XmlHelper.CreateNode(REQUESTDOCUMENT, "item", "");
                class_XmlHelper.SetAttribute(newItem, "symbol", class_XmlHelper.GetAttrValue(activeItem, "symbol"));
                class_XmlHelper.SetAttribute(newItem, "time", class_XmlHelper.GetAttrValue(activeItem, "start"));
                class_XmlHelper.SetAttribute(newItem, "date", class_XmlHelper.GetAttrValue(activeItem, "date"));
                class_XmlHelper.SetAttribute(newItem, "classroom", class_XmlHelper.GetAttrValue(activeItem, "classrrom"));
                class_XmlHelper.SetAttribute(newItem, "guid", class_XmlHelper.GetAttrValue(activeItem, "guid"));
                class_XmlHelper.SetAttribute(newItem, "status", class_XmlHelper.GetAttrValue(activeItem, "status"));
                class_XmlHelper.SetAttribute(newItem, "assigned", class_XmlHelper.GetAttrValue(activeItem, "assigned"));
                RESPONSEDOCUMENT.SelectSingleNode("/root").AppendChild(newItem);
            }
        }
    }
}