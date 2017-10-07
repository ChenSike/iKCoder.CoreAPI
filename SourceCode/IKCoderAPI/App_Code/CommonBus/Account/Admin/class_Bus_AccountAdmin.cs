using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_AccountAdmin
/// </summary>
public class class_Bus_AccountAdmin
{
    class_CommonData Object_CommonData;
    public class_Bus_AccountAdmin(ref class_CommonData refCommonDataObject)
    {
        Object_CommonData = refCommonDataObject;
    }

    public bool GetCheckedAccountAdmin(string symbol,string password)
    {
        if (string.IsNullOrEmpty(symbol))
            return false;
        else
        {
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_ADMINS);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@type", "1");
            DataTable activeDTData = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (activeDTData != null && activeDTData.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }

}