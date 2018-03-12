using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetLessonsLevel : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml("<root></root>");
        XmlNode newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
        class_XmlHelper.SetAttribute(newItemNode, "symbol", class_CommonDefined.enumLessonLevel.primer.ToString());
        class_XmlHelper.SetAttribute(newItemNode, "value", "5001");
        resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
        class_XmlHelper.SetAttribute(newItemNode, "symbol", class_CommonDefined.enumLessonLevel.middle.ToString());
        class_XmlHelper.SetAttribute(newItemNode, "value", "5002");
        resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
        class_XmlHelper.SetAttribute(newItemNode, "symbol", class_CommonDefined.enumLessonLevel.senior.ToString());
        class_XmlHelper.SetAttribute(newItemNode, "value", "5003");
        resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
        class_XmlHelper.SetAttribute(newItemNode, "symbol", class_CommonDefined.enumLessonLevel.advanced.ToString());
        class_XmlHelper.SetAttribute(newItemNode, "value", "5004");
        resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        RESPONSEDOCUMENT.LoadXml(resultDoc.OuterXml);
    }
}