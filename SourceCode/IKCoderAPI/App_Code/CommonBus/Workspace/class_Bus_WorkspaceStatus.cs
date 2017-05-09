using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_WorkspaceStatus
/// </summary>
public class class_Bus_WorkspaceStatus
{
    public class_Bus_WorkspaceStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetWorkspaceStatusSymbol(string symbol,string currentstage,string username)
    {
        return symbol + "_s" + currentstage + "_" + username;
    }

    public static string GetWorkspaceStatusDefaultSymbol(string symbol, string currentstage)
    {
        return symbol + "_s" + currentstage;
    }

    public static XmlDocument GetWorkspaceStatusDocument(class_CommonData Object_CommonData, string symbol, string currentStage, string username,out DataRow activeWorkspaceStatusDataRow)
    {
        XmlDocument sourceDoc_worskspacestatus = new XmlDocument();
        symbol = GetWorkspaceStatusSymbol(symbol, currentStage, username);        
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_WORKSPACESTATUS);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            activeWorkspaceStatusDataRow = activeDataRow;
            string strBaseSenceDoc = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out strBaseSenceDoc);
            if (!string.IsNullOrEmpty(strBaseSenceDoc))
            {
                sourceDoc_worskspacestatus.LoadXml(class_CommonUtil.Decoder_Base64(strBaseSenceDoc));
                return sourceDoc_worskspacestatus;
            }
            else
            {
                return null;
            }
        }
        else
        {
            string defaultSymbol = GetWorkspaceStatusDefaultSymbol(symbol, currentStage);
            activeSPEntry_configSence.ClearAllParamsValues();
            activeSPEntry_configSence.ModifyParameterValue("@symbol", defaultSymbol);
            textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (textDataTable != null && textDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                activeWorkspaceStatusDataRow = activeDataRow;
                string strBaseSenceDoc = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "config", out strBaseSenceDoc);
                if (!string.IsNullOrEmpty(strBaseSenceDoc))
                {
                    sourceDoc_worskspacestatus.LoadXml(class_CommonUtil.Decoder_Base64(strBaseSenceDoc));
                    return sourceDoc_worskspacestatus;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                activeWorkspaceStatusDataRow = null;
                return null;
            }
        }
    }   
}