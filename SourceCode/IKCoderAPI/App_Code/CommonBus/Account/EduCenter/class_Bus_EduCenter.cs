using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_EduCenter
/// </summary>
public class class_Bus_EduCenter : class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry; 

    public class_Bus_EduCenter(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_EDUCENTERS);
        Object_CommonData.PrepareDataOperation();
    }

    public void SetUpdateCenter(string symbol, string password)
    {
        if (!GetISCenterExisted(symbol))
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@c_symbol", symbol);
            activeSPEntry.ModifyParameterValue("@c_password", password);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            string id = GetCenterID(symbol);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@c_symbol", symbol);
            activeSPEntry.ModifyParameterValue("@c_password", password);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        class_Bus_Profile_VerifyEduCenterDocs objectVerifyEduCenterDocs = new class_Bus_Profile_VerifyEduCenterDocs(ref Object_CommonData);
        objectVerifyEduCenterDocs.VerifyDoc_Basic(symbol);
    }

    public bool GetISCenterExisted(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@c_symbol", symbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if(activeDataTable!=null && activeDataTable.Rows.Count>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetCheckedAccountEduCenter(string symbol,string password)
    {
        if (string.IsNullOrEmpty(symbol))
            return false;
        else
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@c_symbol", symbol);            
            DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (activeDataTable != null && activeDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = activeDataTable.Rows[0];
                string cpassword = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "c_password", out cpassword);
                if (cpassword == password)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }

    public string GetCenterID(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@c_symbol", symbol);
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

    public string GetLicence(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@c_symbol", symbol);
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
    

}