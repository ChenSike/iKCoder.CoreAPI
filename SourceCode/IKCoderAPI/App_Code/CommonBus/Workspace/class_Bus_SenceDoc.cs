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

    public static string GetSenceStrDocument(class_CommonData Object_CommonData, string symbol, out DataRow activeSenceDataRow)
    {
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
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeSenceDataRow, "config", out strBaseSenceDoc);
            if (!string.IsNullOrEmpty(strBaseSenceDoc))
            {
                return class_CommonUtil.Decoder_Base64(strBaseSenceDoc);
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
            foreach (DataRow activeRow in textDataTable.Rows)
                resultLst.Add(activeRow);
        }
        return resultLst;
    }

    public int GetCountOfSences(class_CommonData Object_CommonData)
    {
        List<DataRow> result = GetAllSences(Object_CommonData);
        return result.Count;
    }

    public static class_CommonDefined.enumSenceType GetSenceType(string symbol)
    {
        if (symbol.StartsWith("a") || symbol.StartsWith("A"))
            return class_CommonDefined.enumSenceType.primer;
        else if (symbol.StartsWith("b") || symbol.StartsWith("B"))
            return class_CommonDefined.enumSenceType.primary;
        else if (symbol.StartsWith("c") || symbol.StartsWith("C"))
            return class_CommonDefined.enumSenceType.middle;
        else if (symbol.StartsWith("d") || symbol.StartsWith("D"))
            return class_CommonDefined.enumSenceType.senior;
        else if (symbol.StartsWith("e") || symbol.StartsWith("E"))
            return class_CommonDefined.enumSenceType.advanced;
        return class_CommonDefined.enumSenceType.primer;
    }

    public static int GetCountOfStages(class_CommonData Object_CommonData,string symbol)
    {
        DataRow activeDataRow;
        string strDoc = GetSenceStrDocument(Object_CommonData, symbol, out activeDataRow);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(strDoc);
        XmlNodeList stagesNodes = doc.SelectNodes("/sence/stages/stage");
        return stagesNodes.Count;
    }

    public static string GetSenceTypeString(string symbol)
    {
        if (symbol.StartsWith("a") || symbol.StartsWith("A"))
            return class_CommonDefined.enumSenceType.primer.ToString();
        else if (symbol.StartsWith("b") || symbol.StartsWith("B"))
            return class_CommonDefined.enumSenceType.primary.ToString();
        else if (symbol.StartsWith("c") || symbol.StartsWith("C"))
            return class_CommonDefined.enumSenceType.middle.ToString();
        else if (symbol.StartsWith("d") || symbol.StartsWith("D"))
            return class_CommonDefined.enumSenceType.senior.ToString();
        else if (symbol.StartsWith("e") || symbol.StartsWith("E"))
            return class_CommonDefined.enumSenceType.advanced.ToString();
        return class_CommonDefined.enumSenceType.primer.ToString();
    }

    public static string GetSenceValue(class_CommonData Object_CommonData,string symbol,string xpath,string attrName)
    {
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);            
            string strBaseSenceDoc = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out strBaseSenceDoc);
            if (!string.IsNullOrEmpty(strBaseSenceDoc))
            {
                XmlDocument dataDoc = new XmlDocument();
                dataDoc.LoadXml(class_CommonUtil.Decoder_Base64(strBaseSenceDoc));
                XmlNode dataNode = dataDoc.SelectSingleNode(xpath);
                if (dataNode != null)
                {
                    if (string.IsNullOrEmpty(attrName))
                        return class_XmlHelper.GetNodeValue(dataNode);
                    else
                        return class_XmlHelper.GetAttrValue(dataNode, attrName);
                }
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }



}