using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for class_Bus_Lessons
/// </summary>
public class class_Bus_Lessons:class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;
    string Lessons_Symbol = "data_lis_lessons";
    XmlDocument Lessons_Doc = new XmlDocument();
    string Lessons_DatalistID = string.Empty;


    public class_Bus_Lessons(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        GetLoadLessonsNAVDoc();
    }

    public void GetLoadLessonsNAVDoc()
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", Lessons_Symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string dataContent = string.Empty;
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = activeDataTable.Rows[0];
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "data", out dataContent);
            string dataID = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out dataID);
            Lessons_DatalistID = dataID;
            Lessons_Doc.LoadXml(class_CommonUtil.Decoder_Base64(dataContent));
        }
        else
        {
            StringBuilder strLessonsDoc = new StringBuilder();
            strLessonsDoc.Append("<root>");
            strLessonsDoc.Append("</root>");
            activeSPEntry.ModifyParameterValue("@symbol", Lessons_Symbol);
            dataContent = class_CommonUtil.Encoder_Base64(strLessonsDoc.ToString());
            activeSPEntry.ModifyParameterValue("@data", dataContent);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
    }

    public int GetCountOfLessons(class_CommonDefined.enumLessonLevel activeLevel)
    {
        XmlNodeList itemNodes = Lessons_Doc.SelectNodes("/root/level[@value='" + activeLevel.ToString() + "']/lesson");
        return itemNodes.Count;
    }

    public int GetCountOfLessons(string activeLevel)
    {
        XmlNodeList itemNodes = Lessons_Doc.SelectNodes("/root/level[@value='" + activeLevel.ToString() + "']/lesson");
        return itemNodes.Count;
    }

    public XmlNodeList GetLessonsList(string activeLevel)
    {
        return Lessons_Doc.SelectNodes("/root/level[@value='" + activeLevel + "']/lesson");
    }

    public void GetLessonsList(string activeLevel,ref XmlDocument doc)
    {
        XmlNodeList itemNodes = Lessons_Doc.SelectNodes("/root/level[@value='" + activeLevel + "']/lesson");
        doc.LoadXml("<root></root>");
        XmlNode rootNode = doc.SelectSingleNode("/root");
        foreach(XmlNode activeItem in itemNodes)
        {
            rootNode.AppendChild(activeItem.CloneNode(true));
        }
    }

}