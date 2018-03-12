using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;


/// <summary>
/// Summary description for class_Bus_HomeWork
/// </summary>
/// 

public class class_Bus_HomeWorkItem
{
    public string id
    {
        set;
        get;
    }

    public string symbol
    {
        set;
        get;
    }

    public XmlDocument homework
    {
        set;
        get;
    }

    public string centersymbol
    {
        set;
        get;
    }

    public string owner
    {
        set;
        get;
    }

    public XmlDocument signed
    {
        set;
        get;
    }

    public string classsymbol
    {
        set;
        get;
    }

    public string createddate
    {
        set;
        get;
    }

}

public class class_Bus_HomeWork: class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;
    string _centersymbol = string.Empty;
    Dictionary<string, class_Bus_HomeWorkItem> pool_homeworks = new Dictionary<string, class_Bus_HomeWorkItem>();

    public class_Bus_HomeWork(class_CommonData refObjectCommonData, string centersymbol) : base(refObjectCommonData)
    {
        _centersymbol = centersymbol;
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_HOMEWORK);
        Object_CommonData.PrepareDataOperation();
        LoadItems();
    }

    public void LoadItems()
    {
        pool_homeworks = new Dictionary<string, class_Bus_HomeWorkItem>();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@centersymbol", _centersymbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            foreach(DataRow activeDataRow in activeDataTable.Rows)
            {
                string data_id = string.Empty;
                string data_symbol = string.Empty;
                string data_homework = string.Empty;
                string data_owner = string.Empty;
                string data_signed = string.Empty;
                string data_classsymbol = string.Empty;
                string data_date = string.Empty;
                class_Bus_HomeWorkItem newItem = new class_Bus_HomeWorkItem();
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out data_id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out data_symbol);
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_homework", out data_homework);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "owner", out data_owner);
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_signed", out data_signed);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "classsymbol", out data_classsymbol);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "date", out data_date);
                newItem.id = data_id;
                newItem.symbol = data_symbol;
                newItem.homework = new XmlDocument();
                newItem.homework.LoadXml(class_CommonUtil.Decoder_Base64(data_homework));
                newItem.owner = data_owner;
                newItem.signed = new XmlDocument();
                newItem.signed.LoadXml(class_CommonUtil.Decoder_Base64(data_signed));
                newItem.classsymbol = data_classsymbol;
                newItem.createddate = data_date;
                pool_homeworks.Add(newItem.symbol, newItem);
            }
        }
    }

    public XmlDocument GetStatusForAllStudentsWithClass(string classsymbol)
    {
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml("<root></root>");
        XmlNode rootNode = resultDoc.SelectSingleNode("/root");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@centersymbol", _centersymbol);
        activeSPEntry.ModifyParameterValue("@classsymbol", classsymbol);
        Dictionary<string, class_Bus_StudentStatusItem> Pool_HomeWorkStudents = new Dictionary<string, class_Bus_StudentStatusItem>();
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            int totalQuestions = 0;
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                string data_id = string.Empty;
                string data_symbol = string.Empty;
                string data_homework = string.Empty;
                string data_owner = string.Empty;
                string data_signed = string.Empty;
                string data_date = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out data_id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out data_symbol);
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_homework", out data_homework);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "owner", out data_owner);
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_signed", out data_signed);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "date", out data_date);
                XmlDocument doc_signed = new XmlDocument();
                doc_signed.LoadXml(class_CommonUtil.Decoder_Base64(data_signed));
                XmlDocument doc_homework = new XmlDocument();          
                doc_homework.LoadXml(class_CommonUtil.Decoder_Base64(data_homework));
                XmlNodeList questionItems = doc_homework.SelectNodes("/root/item");
                totalQuestions = totalQuestions + questionItems.Count;
                XmlNodeList students_nodes = doc_signed.SelectNodes("/root/student");
                foreach (XmlNode activeStudentNode in students_nodes)
                {
                    string symbol = class_XmlHelper.GetAttrValue(activeStudentNode, "symbol");
                    class_Bus_StudentStatusItem tmpNewItem = null;
                    if (Pool_HomeWorkStudents.ContainsKey(symbol))
                    {
                        tmpNewItem = Pool_HomeWorkStudents[symbol];
                    }
                    else
                    {
                        tmpNewItem = new class_Bus_StudentStatusItem();
                        tmpNewItem.symbol = symbol;
                    }
                    XmlNodeList recordItems = activeStudentNode.SelectNodes("item[@isright='1']");
                    tmpNewItem.rightcount = tmpNewItem.rightcount + recordItems.Count;
                }
            }
            int index = 1;
            foreach (class_Bus_StudentStatusItem activeItem in Pool_HomeWorkStudents.Values)
            {
                XmlNode newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
                class_XmlHelper.SetAttribute(newItemNode, "index", index.ToString());
                class_XmlHelper.SetAttribute(newItemNode, "name", activeItem.symbol);
                class_XmlHelper.SetAttribute(newItemNode, "finsihed", activeItem.finished.ToString());
                class_XmlHelper.SetAttribute(newItemNode, "unfinished", activeItem.unfinished.ToString());
                class_XmlHelper.SetAttribute(newItemNode, "name", (activeItem.rightcount/totalQuestions).ToString());
                class_XmlHelper.SetAttribute(newItemNode, "name", totalQuestions.ToString());
                rootNode.AppendChild(newItemNode);
            }
        }
        return resultDoc;
    }

}