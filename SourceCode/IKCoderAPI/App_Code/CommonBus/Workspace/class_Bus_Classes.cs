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

    public string assigned
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
            string data_assigned = string.Empty;
            string data_id = string.Empty;
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                class_Bus_ClassesItem newItem = new class_Bus_ClassesItem();
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "scheduledoc", out data_schedule_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "basicinfodoc", out data_basicinfo_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "studentlistdoc", out data_studentlist_b64);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "assigned", out data_assigned);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out data_symbol);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out data_id);
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
                newItem.assigned = data_assigned;
                classesPool.Add(newItem.symbol, newItem);
            }
            
        }        
    }

    public void SetAssignedClassToTeacher(string symbol,string to)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        if(activeItem!=null)
        {
            activeItem.assigned = to;
        }
        SetUpdateClass(symbol);
    }

    public string GetAssignedClassToTeacher(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        if (activeItem != null)
        {
            return activeItem.assigned;
        }
        else
            return string.Empty;
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
            SetOrderScheduleItemsOrder(symbol);
            SetUpdateClass(symbol);
            return true;
        }
        else
            return false;
    }

    public bool SetRemoveScheduleItem(string symbol,string date,string starttime,string classroom, string lessonsymbol)
    {
        if (GetVerifyScheduleItem(date, starttime, classroom))
        {
            class_Bus_ClassesItem activeClassItem = GetExsitedItem(symbol);
            XmlNode activeItemNode = GetExistedScheduleIetm(symbol, date, starttime, classroom);
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

    public XmlNode GetExistedScheduleIetm(string symbol,string date, string starttime, string classroom)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        if (activeItem != null)
        {
            XmlDocument activeScheduleDoc = activeItem.doc_schedule;
            XmlNode itemNode = activeScheduleDoc.SelectSingleNode("/root/item[@date='" + date + "' and start='" + starttime + "' and classroom='" + classroom + "']");
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

    public void SetUpdateClass(string symbol)
    {
        class_Bus_ClassesItem activeItem = GetExsitedItem(symbol);
        activeSPEntry.ClearAllParamsValues();
        if (activeItem == null)
        {
            activeSPEntry.ModifyParameterValue("@symbol", activeItem.symbol);
            activeSPEntry.ModifyParameterValue("@centersymbol", activeItem.centersymbol);
            activeSPEntry.ModifyParameterValue("@scheduledoc", class_CommonUtil.Encoder_Base64(activeItem.doc_schedule.OuterXml));
            activeSPEntry.ModifyParameterValue("@basicinfodoc", class_CommonUtil.Encoder_Base64(activeItem.doc_basicinfo.OuterXml));
            activeSPEntry.ModifyParameterValue("@studentlistdoc", class_CommonUtil.Encoder_Base64(activeItem.doc_studentdoc.OuterXml));
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
            activeSPEntry.ModifyParameterValue("@assigned", activeItem.assigned);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
    }

}