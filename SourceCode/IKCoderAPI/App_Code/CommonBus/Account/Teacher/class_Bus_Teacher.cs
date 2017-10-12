using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Teacher
/// </summary>
public class class_Bus_Teacher : class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Teacher(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_TEACHERS);
        Object_CommonData.PrepareDataOperation();
    }

    public void SetUpdateTeacher(string symbol,string password,string centersymbol)
    {
        if (!GetISTeacherExisted(symbol))
        {
            class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
            Dictionary<string, string> result = objectLicences.SetFetchUnusedLicence("0");
            string c_licenceid = result["id"].ToString();
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@status", "0");
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            activeSPEntry.ModifyParameterValue("@licenceid", c_licenceid);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            string id = GetTeacherID(symbol);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@password", password);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        class_Bus_Profile_VerifyTeacherDocs objectVerifyTeacherDocs = new class_Bus_Profile_VerifyTeacherDocs(ref Object_CommonData);
        objectVerifyTeacherDocs.VerifyDoc_Basic(symbol);
    }

    public string GetTeacherID(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string id = string.Empty;
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out id);            
        }
        return id;
    }



    public bool GetCheckedAccountTeacher(string symbol,string password,string licence)
    {
        if(string.IsNullOrEmpty(symbol) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(licence))
        {
            return false;
        }
        else
        {
            if(GetISTeacherExisted(symbol))
            {
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@symbol", symbol);
                activeSPEntry.ModifyParameterValue("@password", password);
                DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if (activeDataTable != null && activeDataTable.Rows.Count > 0)
                {
                    class_Bus_Licences objectLicence = new class_Bus_Licences(Object_CommonData);
                    string licenceid = objectLicence.GetLicenceID(licence);
                    if (objectLicence.GetCheckLicence(licenceid, "1", licence))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public bool GetISTeacherExisted(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
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

    public string GetCenterSymbol(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string centersymbol = string.Empty;
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "centersymbol", out centersymbol);
        }
        return centersymbol;
    }

}