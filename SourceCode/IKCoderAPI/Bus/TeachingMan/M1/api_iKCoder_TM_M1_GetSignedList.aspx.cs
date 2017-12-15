using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetSignedList : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        RESPONSEDOCUMENT.LoadXml("<root></root>");
        XmlNode rootNode = RESPONSEDOCUMENT.SelectSingleNode("/root");
        string symbol = GetQuerystringParam("symbol");
        Object_CommonData.PrepareDataOperation();
        class_Bus_ProfileDocs ObjectProfileDocs = new class_Bus_ProfileDocs(ref Object_CommonData);
        class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
        XmlDocument studentDoc = objectClass.GetClassStudentDoc(symbol);
        XmlNodeList items = studentDoc.SelectNodes("/root/item");
        int index = 1;
        foreach (XmlNode activeItem in items)
        {
            string studentSymbol = class_XmlHelper.GetAttrValue(activeItem, "symbol");
            XmlNode newItem = class_XmlHelper.CreateNode(RESPONSEDOCUMENT, "item", "");
            XmlNode basicDoc = ObjectProfileDocs.GetProfileDocObject(studentSymbol, class_CommonDefined.enumProfileDoc.doc_basic);
            string username = class_XmlHelper.GetNodeValue(basicDoc.SelectSingleNode("/usrbasic/usr_nickname"));
            class_XmlHelper.SetAttribute(newItem, "index", index.ToString());
            class_XmlHelper.SetAttribute(newItem, "name", username);
            class_XmlHelper.SetAttribute(newItem, "symbol", studentSymbol);
            XmlNodeList signin_items = activeItem.SelectNodes("signin/item");
            int signed = 0;
            int unsigned = 0;
            int leaved = 0;
            foreach(XmlNode activesignitem in signin_items)
            {
                string signstatus = class_XmlHelper.GetAttrValue(activesignitem, "status");
                switch(signstatus)
                {
                    case "1":
                        signed++;
                        break;
                    case "0":
                        unsigned++;
                        break;
                    case "2":
                        leaved++;
                        break;
                }
            }
            class_XmlHelper.SetAttribute(newItem, "signed", signed.ToString());
            class_XmlHelper.SetAttribute(newItem, "unsigned", unsigned.ToString());
            class_XmlHelper.SetAttribute(newItem, "leaved", leaved.ToString());
            XmlDocument doc_schedule = objectClass.GetClassScheduleDoc(symbol);
            XmlNodeList scheduleItems = doc_schedule.SelectNodes("/root/item");
            XmlNodeList scheduleFinishedItems = doc_schedule.SelectNodes("/root/item[@finished='1']");
            class_XmlHelper.SetAttribute(newItem, "finished", scheduleFinishedItems.Count.ToString());
            class_XmlHelper.SetAttribute(newItem, "total", scheduleItems.Count.ToString());
            rootNode.AppendChild(newItem);
        }
    }
    
}