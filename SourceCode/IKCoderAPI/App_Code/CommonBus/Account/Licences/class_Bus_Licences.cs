using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Licences
/// </summary>
public class class_Bus_Licences:class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Licences(class_CommonData refObject):base(refObject)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_LICENCES);
    }

    public bool SetCreateNewLicence(string type)
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_LICENCES);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@type", type);
        string licence_1 = Guid.NewGuid().ToString();
        string licence_2 = Guid.NewGuid().ToString();
        string licence = licence_1 + licence_2;
        activeSPEntry.ModifyParameterValue("@licence", licence);
        activeSPEntry.ModifyParameterValue("@status", "0");
        if (Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Dictionary<string,Dictionary<string,string>> GetAllList()
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_LICENCES);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        if (activeDataTable != null)
        {
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                string id = string.Empty;
                string typefromdb = string.Empty;
                string licence = string.Empty;
                string status = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out typefromdb);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out status);
                attrs.Add("id", id);
                attrs.Add("type", typefromdb);
                attrs.Add("licence", licence);
                attrs.Add("status", status);
                result.Add(id, attrs);
            }
        }
        return result;
    }

    /// <summary>
    /// status = 1
    /// </summary>
    /// <param name="type">
    /// 1 for nomal licences
    /// 2 for internal licences
    /// 3 for optional licences
    /// </param>
    /// <returns></returns>
    public Dictionary<string,Dictionary<string,string>> GetUsedList(string type)
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_LICENCES);
        activeSPEntry.ModifyParameterValue("@status", "1");
        activeSPEntry.ModifyParameterValue("@type", type);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        if (activeDataTable != null)
        {
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                string id = string.Empty;
                string typefromdb = string.Empty;
                string licence = string.Empty;
                string status = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out typefromdb);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out status);
                attrs.Add("id", id);
                attrs.Add("type", typefromdb);
                attrs.Add("licence", licence);
                attrs.Add("status", status);
                result.Add(id, attrs);                
            }
        }
        return result;
    }

    /// <summary>
    /// status = 0
    /// </summary>
    /// <param name="type">
    /// 1 for nomal licences
    /// 2 for internal licences
    /// 3 for optional licences
    /// </param>
    /// <returns></returns>
    public Dictionary<string, Dictionary<string, string>> GetUnusedList(string type)
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_LICENCES);
        activeSPEntry.ModifyParameterValue("@status", "0");
        activeSPEntry.ModifyParameterValue("@type", type);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        if (activeDataTable != null)
        {
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                string id = string.Empty;
                string typefromdb = string.Empty;
                string licence = string.Empty;
                string status = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out typefromdb);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out status);
                attrs.Add("id", id);
                attrs.Add("type", typefromdb);
                attrs.Add("licence", licence);
                attrs.Add("status", status);
                result.Add(id, attrs);
            }
        }
        return result;
    }

    public string GetLicenceID(string licence)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@licence", licence);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(activeDataTable, 0, out activeDataRow);
            string id = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
            return id;
        }
        else
            return string.Empty;
    }

    public string GetLicenceContent(string licenceid)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", licenceid);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(activeDataTable, 0, out activeDataRow);
            string licence = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
            return licence;
        }
        else
            return string.Empty;
    }

    public bool GetCheckLicence(string id,string type,string licence)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", id);
        activeSPEntry.ModifyParameterValue("@type", type);
        activeSPEntry.ModifyParameterValue("@licence", licence);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// status = 3
    /// </summary>
    /// <param name="type">
    /// 1 for nomal licences
    /// 2 for internal licences
    /// 3 for optional licences
    /// </param>
    /// <returns></returns>
    public Dictionary<string, Dictionary<string, string>> GetLockedList(string type)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@status", "3");
        activeSPEntry.ModifyParameterValue("@type", type);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        if (activeDataTable != null)
        {
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                string id = string.Empty;
                string typefromdb = string.Empty;
                string licence = string.Empty;
                string status = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out typefromdb);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out status);
                attrs.Add("id", id);
                attrs.Add("type", typefromdb);
                attrs.Add("licence", licence);
                attrs.Add("status", status);
                result.Add(id, attrs);
            }
        }
        return result;
    }

    /// <summary>
    /// status = 3
    /// </summary>
    /// <param name="type">
    /// 1 for nomal licences
    /// 2 for internal licences
    /// 3 for optional licences
    /// </param>
    /// <returns></returns>
    public Dictionary<string, Dictionary<string, string>> GetRemovedList(string type)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@status", "4");
        activeSPEntry.ModifyParameterValue("@type", type);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        if (activeDataTable != null)
        {
            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                string id = string.Empty;
                string typefromdb = string.Empty;
                string licence = string.Empty;
                string status = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type", out typefromdb);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "licence", out licence);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "status", out status);
                attrs.Add("id", id);
                attrs.Add("type", typefromdb);
                attrs.Add("licence", licence);
                attrs.Add("status", status);
                result.Add(id, attrs);
            }
        }
        return result;
    }

    public Dictionary<string,string> SetFetchUnusedLicence(string type)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@status", "0");
        activeSPEntry.ModifyParameterValue("@type", type);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {

            string id = string.Empty;
            string typefromdb = string.Empty;
            string licence = string.Empty;
            string status = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out id);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "type", out typefromdb);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "licence", out licence);
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "status", out status);
            attrs.Add("id", id);
            attrs.Add("type", typefromdb);
            attrs.Add("licence", licence);
            attrs.Add("status", status);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@status", "1");
            activeSPEntry.ModifyParameterValue("@id", id);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        return attrs;
    }

    public bool SetLockLicence(string id)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", id);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@status", "3");
            activeSPEntry.ModifyParameterValue("@id", id);
            if (Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }

    public bool SetRemovedLicence(string id)
    {
        Object_CommonData.PrepareDataOperation();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", id);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@status", "4");
            activeSPEntry.ModifyParameterValue("@id", id);
            if (Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }

}