using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_SetUpdateScheduleItem_aspx : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        if (REQUESTDOCUMENT != null)
        {
            XmlNodeList freqItemNodes = REQUESTDOCUMENT.SelectNodes("/root/freq/item");
            XmlNode classroomNode = REQUESTDOCUMENT.SelectSingleNode("/root/classroom");
            XmlNode startdateNode = REQUESTDOCUMENT.SelectSingleNode("/root/startdate");
            XmlNode starttimeNode = REQUESTDOCUMENT.SelectSingleNode("/root/starttime");
            XmlNode teacherNode = REQUESTDOCUMENT.SelectSingleNode("/root/teacher");
            XmlNode classsymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/classsymbol");
            XmlNode lessonNode = REQUESTDOCUMENT.SelectSingleNode("/root/lesson");
            if (freqItemNodes == null || freqItemNodes.Count <= 0)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->freq", "");
                return;
            }
            if (classroomNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->classroom", "");
                return;
            }
            if (startdateNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->startdate", "");
                return;
            }
            if (starttimeNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->starttime", "");
                return;
            }
            if (classsymbolNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->classsymbol", "");
                return;
            }
            if (lessonNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->lesson", "");
                return;
            }
            switchResponseMode(enumResponseMode.text);
            RESPONSEDOCUMENT.LoadXml("<root></root>");
            XmlNode rootNode = RESPONSEDOCUMENT.SelectSingleNode("/root");
            string data_classroom = string.Empty;
            string data_startdate = string.Empty;
            string data_starttime = string.Empty;
            string data_teacher = string.Empty;
            string data_classsymbol = string.Empty;
            string data_lessonsymbol = string.Empty;
            data_classsymbol = class_XmlHelper.GetAttrValue(classsymbolNode, "value");
            data_classroom = class_XmlHelper.GetAttrValue(classroomNode, "value");
            data_startdate = class_XmlHelper.GetAttrValue(startdateNode, "value");
            data_starttime = class_XmlHelper.GetAttrValue(starttimeNode, "value");
            data_teacher = class_XmlHelper.GetAttrValue(teacherNode, "value");
            data_lessonsymbol = class_XmlHelper.GetAttrValue(lessonNode, "value");
            class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            objectClass.SetUpdateScheduleItem(data_classsymbol, data_startdate, data_starttime, data_classroom, data_lessonsymbol);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No request document.", "");
        }
    }
}