using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Classes
/// </summary>
/// 

public class class_Bus_ClassesItem
{
    public string symbol
    {
        set;
        get;
    }

    public string centersymbol
    {
        set;
        get;
    }

    public string id
    {
        set;
        get;
    }

    public XmlDocument doc_schedule
    {
        set;
        get;
    }

    public XmlDocument doc_basicinfo
    {
        set;
        get;
    }

    public XmlDocument doc_studentdoc
    {
        set;
        get;
    }

    public string ongid
    {
        set;
        get;
    }

    public string typevalue
    {
        set;
        get;
    }

    public string status
    {
        set;
        get;
    }

}

public class class_Bus_Classes : class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;
    Dictionary<string, class_Bus_ClassesItem> classesPool;
    string _centersymbol = string.Empty;

    public class_Bus_Classes(class_CommonData refObjectCommonData, string centersymbol) : base(refObjectCommonData)
    {
        _centersymbol = centersymbol;
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_CLASSES);
        Object_CommonData.PrepareDataOperation();
        GetLoadClasses();
    }

    public string GetClassesID(string symbol)
    {
        if (classesPool.ContainsKey(symbol))
        {
            class_Bus_ClassesItem activeItem = classesPool[symbol];
            return activeItem.id;
        }
        else
            return string.Empty;
    }

    public bool GetIsClassesExisted(string symbol)
    {
        if (classesPool.ContainsKey(symbol))
        {
            return true;
        }
        else
            return false;
    }

    public class_Bus_ClassesItem GetExsitedItem(string symbol)
    {
        if (GetIsClassesExisted(symbol))
        {
            class_Bus_ClassesItem activeItem = classesPool[symbol];
            return activeItem;
        }
        else
            return null;
    }

    public void GetLoadClasses()
    {
        classesPool = new Dictionary<string, class_Bus_ClassesItem>();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@centersymbol", _centersymbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            string data_schedule_b64 = string.Empty;
            string data_basicinfo_b64 = string.Empty;
            string data_studentlist_b64 = string.Empty;
            string data_symbol = string.Empty;
            string data_onguid = string.Empty;
            string data_id = string.Empty;
            string data_typevalue = string.Empty;
            string data_status = string.Empty;
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                class_Bus_ClassesItem newItem = new class_Bus_ClassesItem();
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "scheduledoc", out data_schedule_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "basicinfodoc", out data_basicinfo_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "studentlistdoc", out data_studentlist_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "onguid", out data_onguid);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out data_symbol);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out data_id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "typevalue", out data_typevalue);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out data_status);
                if (!string.IsNullOrEmpty(data_schedule_b64))
                {
                    newItem.doc_schedule.LoadXml(class_CommonUtil.Decoder_Base64(data_schedule_b64));
                }
                else
                {
                    newItem.doc_schedule.LoadXml("<root></root>");
                }
                if (!string.IsNullOrEmpty(data_basicinfo_b64))
                {
                    newItem.doc_basicinfo.LoadXml(class_CommonUtil.Decoder_Base64(data_basicinfo_b64));
                }
                else
                {
                    newItem.doc_basicinfo.LoadXml("<root></root>");
                }
                if (!string.IsNullOrEmpty(data_studentlist_b64))
                {
                    newItem.doc_studentdoc.LoadXml(class_CommonUtil.Decoder_Base64(data_studentlist_b64));
                }
                else
                {
                    newItem.doc_studentdoc.LoadXml("<root></root>");
                }
                newItem.id = data_id;
                newItem.symbol = data_symbol;
                newItem.centersymbol = _centersymbol;
                newItem.ongid = data_onguid;
                newItem.typevalue = data_typevalue;
                newItem.status = data_status;
                classesPool.Add(newItem.symbol, newItem);
            }
            
        }        
    }

    public void SetAssignedClassToTeacher(string symbol,string guid, string to)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        if(activeItem!=null)
        {
            XmlNode activeScheduleItem = activeItem.doc_schedule.SelectSingleNode("/root/item[@guid='"+guid+"']");
            if(activeScheduleItem!=null)
            {
                class_XmlHelper.SetAttribute(activeScheduleItem, "assigned", to);
            }
        }
        SetUpdateClass(symbol);
    }

    public bool SetUpdateScheduleItem(string symbol,string date,string starttime,string classroom,string lessonsymbol)
    {
        if (GetVerifyScheduleItem(date, starttime, classroom))
        {
            class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
            XmlNode newItem = class_XmlHelper.CreateNode(activeClassItem.doc_schedule, "item", "");
            class_XmlHelper.SetAttribute(newItem, "date", date);
            class_XmlHelper.SetAttribute(newItem, "start", starttime);
            class_XmlHelper.SetAttribute(newItem, "classroom", classroom);
            class_XmlHelper.SetAttribute(newItem, "lesson", lessonsymbol);
            class_XmlHelper.SetAttribute(newItem, "guid", Guid.NewGuid().ToString());
            class_XmlHelper.SetAttribute(newItem, "status", "0");
            activeClassItem.doc_schedule.SelectSingleNode("/root").AppendChild(newItem);
            SetOrderScheduleItemsOrder(symbol);
            SetUpdateClass(symbol);
            return true;
        }
        else
            return false;
    }

    public bool SetStartLesson(string symbol,string guid)
    {
        class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
        if (activeClassItem != null)
        {
            XmlNode scheduleItemNode = GetExistedScheduleIetm(symbol, guid);
            if(scheduleItemNode!=null)
            {
                activeClassItem.ongid = guid;
            }
            SetUpdateClass(symbol);
            return true;
        }
        else
            return false;
    }

    public bool SetStopLesson(string symbol,string guid)
    {
        class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
        if (activeClassItem != null)
        {
            XmlNode scheduleItemNode = GetExistedScheduleIetm(symbol, guid);
            if (scheduleItemNode != null)
            {
                activeClassItem.ongid = string.Empty;
            }
            SetUpdateClass(symbol);
            return true;
        }
        else
            return false;
    }

    public List<XmlNode> GetClassesForTeacher(string teachersymbol)
    {
        List<XmlNode> result = new List<XmlNode>();
        foreach(class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            XmlNodeList itemNodes = activeItem.doc_schedule.SelectNodes("/root/item[@assigned='" + teachersymbol + "']");
            foreach (XmlNode activeItemNode in itemNodes)
            {
                result.Add(activeItemNode);
            }
        }
        return result;
    }

    public List<class_Bus_ClassesItem> GetClasses()
    {
        return classesPool.Values.ToList();
    }

    public XmlDocument GetClassesDocument()
    {
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml("<root></root>");
        foreach(class_Bus_ClassesItem activeClassItem in classesPool.Values)
        {
            XmlNode newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
            class_XmlHelper.SetAttribute(newItemNode, "id", activeClassItem.id);
            class_XmlHelper.SetAttribute(newItemNode, "symbol", activeClassItem.symbol);
            class_XmlHelper.SetAttribute(newItemNode, "status", activeClassItem.status);
            class_XmlHelper.SetAttribute(newItemNode, "type", activeClassItem.typevalue);
            XmlNode rootNode = activeClassItem.doc_basicinfo.SelectSingleNode("/root");
            class_XmlHelper.SetAttribute(newItemNode, "teacher", class_XmlHelper.GetNodeValue("@teacher", rootNode));
            class_XmlHelper.SetAttribute(newItemNode, "classroom", class_XmlHelper.GetNodeValue("@classrom", rootNode));
            class_XmlHelper.SetAttribute(newItemNode, "persons", class_XmlHelper.GetNodeValue("@person", rootNode));
            XmlNodeList activeStudentItems = activeClassItem.doc_studentdoc.SelectNodes("/root/item");
            class_XmlHelper.SetAttribute(newItemNode, "activepersons", activeStudentItems.Count.ToString());
            class_XmlHelper.SetAttribute(newItemNode, "start", class_XmlHelper.GetNodeValue("@start", rootNode));
            resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        }
        return resultDoc;
    }

    public void SetSwicthActiveClassToFinished(string symbol)
    {
        classesPool[symbol].status = "2";
        SetUpdateClass(symbol);
        GetLoadClasses();
    }

    

    public List<class_Bus_ClassesItem> GetClassesItemsListForTeacher(string teachersymbol)
    {
        List<class_Bus_ClassesItem> result = new List<class_Bus_ClassesItem>();
        foreach (class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            XmlNodeList itemNodes = activeItem.doc_schedule.SelectNodes("/root/item[@assigned='" + teachersymbol + "']");
            if(itemNodes!=null && itemNodes.Count>0)
            {
                result.Add(activeItem);
            }
        }
        return result;
    }

    public class_Bus_ClassesItem GetClass(string symbol)
    {
        if (classesPool.ContainsKey(symbol))
        {
            class_Bus_ClassesItem activeItem = classesPool[symbol];
            return activeItem;
        }
        else
            return null;
    }

    public int GetCountOfClasses()
    {
        return classesPool.Count;
    }

    public int GetCountOfClasses(string typeValue)
    {
        int result = 0;
        foreach(class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            if (activeItem.typevalue == typeValue)
                result++;
        }
        return result;
    }

    public void SetClassScheduleStatus(string symbol,string status)
    {
        if (classesPool.ContainsKey(symbol))
        {
            class_Bus_ClassesItem activeItem = classesPool[symbol];
                        
        }
    }

    public bool SetRemoveScheduleItem(string symbol,string date,string starttime,string classroom, string lessonsymbol)
    {
        if (GetVerifyScheduleItem(date, starttime, classroom))
        {
            class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
            XmlNode activeItemNode = GetExistedScheduleIetm(symbol, date);
            if (activeItemNode != null)
            {
                activeClassItem.doc_schedule.SelectSingleNode("/root").RemoveChild(activeItemNode);
                SetOrderScheduleItemsOrder(symbol);
                SetUpdateClass(symbol);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public void SetOrderScheduleItemsOrder(string symbol)
    {
        class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
        if(activeClassItem!=null)
        {
            XmlNodeList items = activeClassItem.doc_schedule.SelectNodes("/root/item");
            int index = 1;
            foreach(XmlNode activeItem in items)
            {
                class_XmlHelper.SetAttribute(activeItem, "index", index.ToString());
                index++;
            }
        }
    }

    public bool GetVerifyScheduleItem(string date,string starttime,string classroom)
    {
        bool result = true;
        foreach(class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            XmlDocument activeScheduleDoc = activeItem.doc_schedule;
            XmlNode itemNode = activeScheduleDoc.SelectSingleNode("/root/item[@date='" + date + "' and start='" + starttime + "' and classroom='" + classroom + "']");
            if (itemNode != null)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public List<XmlNode> GetExtstedScheduleItemsForCurrentDate(string date)
    {
        List<XmlNode> result = new List<XmlNode>();
        foreach(class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            XmlDocument activeScheduleDoc = activeItem.doc_schedule;
            XmlNodeList itemNodes = activeScheduleDoc.SelectNodes("/root/item[@date='" + date + "']");
            foreach (XmlNode activeNode in itemNodes)
            {
                result.Add(activeNode);
            }
        }
        return result;
    }

    public XmlNode GetExistedScheduleIetm(string symbol,string guid)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        if (activeItem != null)
        {
            XmlDocument activeScheduleDoc = activeItem.doc_schedule;
            XmlNode itemNode = activeScheduleDoc.SelectSingleNode("/root/item[@guid='" + guid + "']");
            if (itemNode != null)
            {
                return itemNode;
            }
            else
                return null;
        }
        else
            return null;
    }


    public void SetUpdateStudent(string symbol,string studentsymbol)
    {
        if(!GetVerifyStudentItem(symbol,studentsymbol))
        {
            class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
            XmlNode newItem = class_XmlHelper.CreateNode(activeClassItem.doc_schedule, "item", "");
            class_XmlHelper.SetAttribute(newItem, "symbol", studentsymbol);
            activeClassItem.doc_studentdoc.SelectSingleNode("/root").AppendChild(newItem);
            SetUpdateClass(symbol);
        }
    }

    public void SetRemoveStudentItem(string symbol,string studentsymbol)
    {
        if(GetVerifyStudentItem(symbol,studentsymbol))
        {
            class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
            XmlNode studentItemNode = GetExistedStudentItem(symbol, studentsymbol);
            if(studentsymbol!=null)
            {
                activeClassItem.doc_studentdoc.SelectSingleNode("/root").RemoveChild(studentItemNode);
                SetUpdateClass(symbol);
            }
        }
    }

    public bool GetVerifyStudentItem(string symbol,string studentsymbol)
    {
        class_Bus_ClassesItem newItem = GetExsitedItem(symbol);
        XmlNode itemNode = newItem.doc_studentdoc.SelectSingleNode("/root/item[@symbol='" + studentsymbol + "']");
        if (itemNode != null)
            return true;
        else
            return false;
    }

    public XmlNode GetExistedStudentItem(string symbol,string studentsymbol)
    {
        class_Bus_ClassesItem newItem = GetExsitedItem(symbol);
        XmlNode itemNode = newItem.doc_studentdoc.SelectSingleNode("/root/item[@symbol='" + studentsymbol + "']");
        if (itemNode != null)
            return itemNode;
        else
            return null;
    }

    public bool SetRemoveClass(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        activeSPEntry.ClearAllParamsValues();
        if (activeItem != null)
        {
            activeSPEntry.ModifyParameterValue("@id", activeItem.id);
            Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetUpdateAllClasses(string symbol)
    {
        foreach(class_Bus_ClassesItem activeItem in classesPool.Values)
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", activeItem.id);
            activeSPEntry.ModifyParameterValue("@symbol", activeItem.symbol);
            activeSPEntry.ModifyParameterValue("@centersymbol", activeItem.centersymbol);
            activeSPEntry.ModifyParameterValue("@status", activeItem.status);
            activeSPEntry.ModifyParameterValue("@scheduledoc", class_CommonUtil.Encoder_Base64(activeItem.doc_schedule.OuterXml));
            activeSPEntry.ModifyParameterValue("@basicinfodoc", class_CommonUtil.Encoder_Base64(activeItem.doc_basicinfo.OuterXml));
            activeSPEntry.ModifyParameterValue("@studentlistdoc", class_CommonUtil.Encoder_Base64(activeItem.doc_studentdoc.OuterXml));
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
    }

    public class_Bus_ClassesItem SetNewClass(string symbol)
    {
        class_Bus_ClassesItem newItem = new class_Bus_ClassesItem();
        newItem.symbol = symbol;
        newItem.centersymbol = _centersymbol;
        newItem.status = "1";
        newItem.doc_basicinfo = new XmlDocument();
        newItem.doc_basicinfo.LoadXml("<root></root>");
        newItem.doc_schedule = new XmlDocument();
        newItem.doc_schedule.LoadXml("<root></root>");
        newItem.doc_studentdoc = new XmlDocument();
        newItem.doc_studentdoc.LoadXml("<root></root>");
        return newItem;
    }

    public XmlDocument GetClassBasicDoc(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        return activeItem.doc_basicinfo;
    }

    public XmlDocument GetClassStudentDoc(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        return activeItem.doc_studentdoc;
    }

    public XmlDocument GetClassScheduleDoc(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        return activeItem.doc_schedule;
    }

    public void SetUpdateClass(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        activeSPEntry.ClearAllParamsValues();
        if (activeItem == null)
        {
            activeItem = SetNewClass(symbol);
            activeSPEntry.ModifyParameterValue("@symbol", activeItem.symbol);
            activeSPEntry.ModifyParameterValue("@centersymbol", activeItem.centersymbol);
            activeSPEntry.ModifyParameterValue("@scheduledoc", class_CommonUtil.Encoder_Base64(activeItem.doc_schedule.OuterXml));
            activeSPEntry.ModifyParameterValue("@basicinfodoc", class_CommonUtil.Encoder_Base64(activeItem.doc_basicinfo.OuterXml));
            activeSPEntry.ModifyParameterValue("@studentlistdoc", class_CommonUtil.Encoder_Base64(activeItem.doc_studentdoc.OuterXml));
            activeSPEntry.ModifyParameterValue("@status", activeItem.status);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            activeSPEntry.ModifyParameterValue("@id", activeItem.id);
            activeSPEntry.ModifyParameterValue("@symbol", activeItem.symbol);
            activeSPEntry.ModifyParameterValue("@centersymbol", activeItem.centersymbol);
            activeSPEntry.ModifyParameterValue("@scheduledoc", class_CommonUtil.Encoder_Base64(activeItem.doc_schedule.OuterXml));
            activeSPEntry.ModifyParameterValue("@basicinfodoc", class_CommonUtil.Encoder_Base64(activeItem.doc_basicinfo.OuterXml));
            activeSPEntry.ModifyParameterValue("@studentlistdoc", class_CommonUtil.Encoder_Base64(activeItem.doc_studentdoc.OuterXml));
            activeSPEntry.ModifyParameterValue("@onguid", activeItem.ongid);
            activeSPEntry.ModifyParameterValue("@status", activeItem.status);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        GetLoadClasses();
    }

}