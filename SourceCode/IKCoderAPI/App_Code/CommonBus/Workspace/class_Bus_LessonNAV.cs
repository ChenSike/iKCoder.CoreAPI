using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_LessonNAV
/// </summary>
public class class_Bus_LessonNAV : class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;
    string LessonNAV_Symbol = "data_lis_lessonNAV";
    XmlDocument LessonNAV_Doc = new XmlDocument();
    string LessonNAV_DatalistID;

    public class_Bus_LessonNAV(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        Object_CommonData.PrepareDataOperation();      
    }

    public bool GetLoadLessonNAVDoc()
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", LessonNAV_Symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = activeDataTable.Rows[0];
            string dataContent = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "data", out dataContent);
            string dataID = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out dataID);
            LessonNAV_DatalistID = dataID;
            LessonNAV_Doc.LoadXml(class_CommonUtil.Decoder_Base64(dataContent));
            return true;
        }
        else
            return false;
    }

    public int GetCountOfPages()
    {
        XmlNodeList pageNodesList = LessonNAV_Doc.SelectNodes("/root/page");
        if (pageNodesList != null)
            return pageNodesList.Count;
        else
            return 0;
    }

    public string GetResourceSymbol(string lessonSymbol,string pageIndex)
    {
        XmlNode pageNode = LessonNAV_Doc.SelectSingleNode("/root/lesson[@symbol='" + lessonSymbol + "']/page[@index='" + pageIndex + "']");
        if (pageNode == null)
            return string.Empty;
        else
        {
            string symbol = class_XmlHelper.GetAttrValue(pageNode, "symbol");
            return symbol;
        }
    }

    public XmlDocument GetLessonCurrentNAVDoc(string lessonSymbol)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<root></root>");
        XmlNode lessonNode = LessonNAV_Doc.SelectSingleNode("/root/lesson[@symbol='" + lessonSymbol + "']");
        if(lessonNode!=null)
        {
            doc.SelectSingleNode("/root").AppendChild(lessonNode.CloneNode(true));
        }
        return doc;
    }


    public void SetSaveLessonNAVDoc()
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", LessonNAV_DatalistID);
        string dataContent = class_CommonUtil.Encoder_Base64(LessonNAV_Doc.OuterXml);
        activeSPEntry.ModifyParameterValue("@data", dataContent);
        Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
    }


}