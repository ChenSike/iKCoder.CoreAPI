using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_LessonSchedule
/// </summary>
public class class_Bus_LessonSchedule:class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;
    string LessonNAV_Symbol = "data_lis_lessonSchedule";
    XmlDocument LessonNAV_Doc = new XmlDocument();

    public class_Bus_LessonSchedule(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        Object_CommonData.PrepareDataOperation();
    }

    
}