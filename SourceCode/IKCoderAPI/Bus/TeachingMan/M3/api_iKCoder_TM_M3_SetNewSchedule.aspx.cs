using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_SetNewSchedule_aspx : class_WebBase_IKCoderAPI_UA_Tm
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
            if(classsymbolNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->classsymbol", "");
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
            data_classsymbol = class_XmlHelper.GetAttrValue(classsymbolNode, "value");
            data_classroom = class_XmlHelper.GetAttrValue(classroomNode, "value");
            data_startdate = class_XmlHelper.GetAttrValue(startdateNode, "value");
            data_starttime = class_XmlHelper.GetAttrValue(starttimeNode, "value");
            data_teacher = class_XmlHelper.GetAttrValue(teacherNode, "value");
            DateTime dtForStartDate = DateTime.Parse(data_startdate);
            class_Bus_Lessons objectLessons = new class_Bus_Lessons(Object_CommonData);
            int count = objectLessons.GetCountOfLessons(data_classsymbol);
            class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            string typeSymbol = objectClass.GetClassLevel(data_classsymbol);
            XmlNodeList lessonsItemNodes = objectLessons.GetLessonsList(typeSymbol);
            Queue<XmlNode> queueLessonsIetmNodes = new Queue<XmlNode>();
            foreach(XmlNode activeNode in lessonsItemNodes)
                queueLessonsIetmNodes.Enqueue(activeNode);
            for (int i=0;i<count;i++)
            {
                foreach(XmlNode activeFreqItemNode in freqItemNodes)
                {
                    string value = class_XmlHelper.GetAttrValue(activeFreqItemNode, "value");
                    if (dtForStartDate.DayOfWeek.ToString() == value)
                    {
                        if (queueLessonsIetmNodes.Count > 0)
                        {
                            XmlNode lessonItem = queueLessonsIetmNodes.Dequeue();
                            string data_lesson_symbol = class_XmlHelper.GetAttrValue(lessonItem, "symbol");
                            XmlNode newItemNode = class_XmlHelper.CreateNode(RESPONSEDOCUMENT, "item", "");
                            class_XmlHelper.SetAttribute(newItemNode, "index", i.ToString());
                            string newGuid = Guid.NewGuid().ToString();
                            class_XmlHelper.SetAttribute(newItemNode, "guid", newGuid);
                            class_XmlHelper.SetAttribute(newItemNode, "lesson", data_lesson_symbol);
                            class_XmlHelper.SetAttribute(newItemNode, "classroom", data_classroom);
                            class_XmlHelper.SetAttribute(newItemNode, "date", dtForStartDate.ToString("yyyy-MM-dd"));
                            class_XmlHelper.SetAttribute(newItemNode, "start", data_starttime);
                            class_XmlHelper.SetAttribute(newItemNode, "status", "0");
                            class_XmlHelper.SetAttribute(newItemNode, "assigned", data_teacher);
                            class_XmlHelper.SetAttribute(newItemNode, "finished", "");
                            rootNode.AppendChild(newItemNode);
                            objectClass.SetUpdateScheduleItem(data_classsymbol, dtForStartDate.ToString("yyyy-MM-dd"), data_starttime, data_classroom, data_lesson_symbol);
                        }
                    }
                }
                dtForStartDate = dtForStartDate.AddDays(1);
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No request document.", "");
        }
    }
}