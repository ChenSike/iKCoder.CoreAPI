using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Advisor
/// </summary>
public class class_Bus_Advisor:class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Advisor(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_ADVISOR);
        Object_CommonData.PrepareDataOperation();
    }

    public void SetUpdateAdvisor(string symbol, string password,string centersymbol)
    {
        if (!GetISAdvisorExisted(symbol))
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            string id = GetAdvisorID(symbol);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@password", password);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        class_Bus_Profile_VerifyAdvisor objectVerifyAdvisor = new class_Bus_Profile_VerifyAdvisor(ref Object_CommonData);
        objectVerifyAdvisor.VerifyDoc_Basic(symbol);
    }


    public string GetAdvisorID(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
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


    public bool GetISAdvisorExisted(string symbol)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@symbol", symbol);
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

    public bool GetCheckedAccountAdvisor(string symbol, string password)
    {
        if (string.IsNullOrEmpty(symbol) || string.IsNullOrEmpty(password))
        {
            return false;
        }
        else
        {
            if (GetISAdvisorExisted(symbol))
            {
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@symbol", symbol);
                activeSPEntry.ModifyParameterValue("@password", password);
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
            else
            {
                return false;
            }
        }
    }

}