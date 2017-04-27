using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_SenceDoc
/// </summary>
public class class_Bus_SenceDoc
{
    public class_Bus_SenceDoc()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static XmlDocument GetSenceDocument(class_CommonData Object_CommonData,string symbol,out DataRow activeSenceDataRow)
    {
        XmlDocument sourceDoc_sence = new XmlDocument();
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            activeSenceDataRow = activeDataRow;
            string strBaseSenceDoc = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeSenceDataRow, "config", out strBaseSenceDoc);
            if (!string.IsNullOrEmpty(strBaseSenceDoc))
            {
                sourceDoc_sence.LoadXml(class_CommonUtil.Decoder_Base64(strBaseSenceDoc));
                return sourceDoc_sence;
            }
            else
            {
                return null;
            }
        }
        else
        {
            activeSenceDataRow = null;
            return null;
        }
    }

    public static List<DataRow> GetAllSences(class_CommonData Object_CommonData)
    {
        List<DataRow> resultLst = new List<DataRow>();
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
        activeSPEntry_configSence.ClearAllParamsValues();
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            foreach(DataRow activeRow in textDataTable.Rows)            
                resultLst.Add(activeRow);            
        }
        return resultLst;
    }



}