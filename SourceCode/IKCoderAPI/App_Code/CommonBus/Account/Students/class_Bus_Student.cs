using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Student
/// </summary>
public class class_Bus_Student:class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Student(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_STUDENTS);
        Object_CommonData.PrepareDataOperation();
    }

    public string GetStudentID(string name)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@name", name);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string id = string.Empty;
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out id);
        }
        return id;
    }

    public bool GetISStudentExisted(string name)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@name", name);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetUpdateStudent(string name, string password, string centersymbol)
    {
        if (!GetISStudentExisted(name))
        {
            class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
            Dictionary<string, string> result = objectLicences.SetFetchUnusedLicence("0");
            string c_licenceid = result["id"].ToString();
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@name", name);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@status", "0");
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            activeSPEntry.ModifyParameterValue("@regdate", c_licenceid);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            string id = GetStudentID(name);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        class_Bus_Profile_VerifyStudentDocs objectStudentDocs = new class_Bus_Profile_VerifyStudentDocs(ref Object_CommonData);
        objectStudentDocs.VerifyAll(name);
    }

}