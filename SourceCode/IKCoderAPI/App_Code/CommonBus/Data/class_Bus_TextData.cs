using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_TextData
/// </summary>
public class class_Bus_TextData
{
    public class_Bus_TextData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetTextData(string symbol,class_CommonData Object_CommonData)
    {
        string result = string.Empty;
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            string data = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out data);
            data = class_CommonUtil.Decoder_Base64(data);
            return data;
        }
        else
            return string.Empty;
    }

}