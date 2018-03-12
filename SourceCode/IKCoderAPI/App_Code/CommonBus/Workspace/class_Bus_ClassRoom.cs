using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;


/// <summary>
/// Summary description for class_Bus_ClassRoom
/// </summary>
public class class_Bus_ClassRoom : class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;
    string _centersymbol = string.Empty;
    public class_Bus_ClassRoom(class_CommonData refObjectCommonData, string centersymbol) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_CLASSROOM);
        Object_CommonData.PrepareDataOperation();
        _centersymbol = centersymbol;
    }

    public Dictionary<string, string> GetClassroomsList()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@centersymbol", _centersymbol);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {

            foreach (DataRow activeDataRow in activeDataTable.Rows)
            {
                string data_id = string.Empty;
                string data_symbol = string.Empty;
                class_Bus_ClassesItem newItem = new class_Bus_ClassesItem();
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out data_id);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out data_symbol);
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                if(!attrs.ContainsKey(data_id))
                {
                    attrs.Add(data_id, data_symbol);
                }
            }
        }
        return result;
    }

}