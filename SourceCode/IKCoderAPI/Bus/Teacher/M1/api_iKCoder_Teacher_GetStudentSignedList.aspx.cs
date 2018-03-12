using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Teacher_api_iKCoder_Teacher_GetStudentSignedList : class_WebBase_IKCoderAPI_UA_Teacher
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string guid = GetQuerystringParam("guid");
        if (!string.IsNullOrEmpty(symbol))
        {
            RESPONSEDOCUMENT.LoadXml("<root></root>");
            class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            objectClasses.GetLoadClasses();
            class_Bus_ClassesItem activeClassItem = objectClasses.GetClass(symbol);
            class_Bus_Profile_Common objectProfileObject = new class_Bus_Profile_Common(ref Object_CommonData);
            XmlNodeList items = activeClassItem.doc_studentdoc.SelectNodes("/root/item");
            foreach(XmlNode activeItemNode in items)
            {
                XmlNode newItemNode = class_XmlHelper.CreateNode(RESPONSEDOCUMENT, "item", "");
                string studentsymbol = class_XmlHelper.GetAttrValue(activeItemNode, "symbol");
                class_XmlHelper.SetAttribute(newItemNode, "symbol", studentsymbol);
                XmlDocument doc = objectProfileObject.GetProfileDocObject(studentsymbol, class_CommonDefined.enumProfileDoc.doc_basic);
                XmlNode nameNode = doc.SelectSingleNode("/basic/name");
                string nameValue = class_XmlHelper.GetNodeValue(nameNode);
                XmlNode signedItemNode = activeItemNode.SelectSingleNode("signin/item[@guid=" + guid + "']");
                class_XmlHelper.SetAttribute(newItemNode, "name", nameValue);
                class_XmlHelper.SetAttribute(newItemNode, "checked", signedItemNode == null ? "0" : "1");
                RESPONSEDOCUMENT.SelectSingleNode("/root").AppendChild(newItemNode);
            }
            switchResponseMode(enumResponseMode.text);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->symbol", "");
        }
    }
}