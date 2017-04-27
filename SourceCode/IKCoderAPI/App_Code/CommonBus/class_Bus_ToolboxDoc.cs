using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_ToolboxDoc
/// </summary>
public class class_Bus_ToolboxDoc
{
    public class_Bus_ToolboxDoc()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetToolboxSymbol(string symbol,string currentStage)
    {
        return "lesson" + "_" + symbol + "_" + currentStage;
    }

    public static string GetToolboxOptionalSymbol(string headertype,string symbol,string curtrentStage)
    {
        return headertype + "_" + symbol + "_" + curtrentStage;
    }

    public static XmlDocument GetToolboxDocument(class_CommonData Object_CommonData,string symbol,string currentStage,out DataRow activeToolBoxDataRow)
    {
        XmlDocument sourceDoc_toolbox = new XmlDocument();
        symbol = GetToolboxSymbol(symbol, currentStage);
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_BLOCKLY);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            activeToolBoxDataRow = activeDataRow;
            string strBaseSenceDoc = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "config", out strBaseSenceDoc);
            if (!string.IsNullOrEmpty(strBaseSenceDoc))
            {
                sourceDoc_toolbox.LoadXml(class_CommonUtil.Decoder_Base64(strBaseSenceDoc));
                return sourceDoc_toolbox;
            }
            else
            {
                return null;
            }
        }
        else
        {
            activeToolBoxDataRow = null;
            return null;
        }
    }
}