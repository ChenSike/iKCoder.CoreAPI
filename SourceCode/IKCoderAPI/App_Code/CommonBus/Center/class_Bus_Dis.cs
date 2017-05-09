using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Dis
/// </summary>
public class class_Bus_Dis
{
    public class_Bus_Dis()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public const string symbol_dis_config = "data_list_dis";

    protected XmlDocument sourceDoc_dis = new XmlDocument();
    protected XmlNodeList nodesList_centerDis;

    public void init_SourceDoc(class_CommonData Object_CommonData)
    {
        string result = string.Empty;
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol_dis_config);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            string data = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out data);
            data = class_CommonUtil.Decoder_Base64(data);
            sourceDoc_dis.LoadXml(data);
            nodesList_centerDis = sourceDoc_dis.SelectNodes("/root/item");
        }
    }

    public Dictionary<string,Dictionary<string,string>> get_disSymbolsList(List<string> finishedSymbol)
    {
        Dictionary<string, Dictionary<string, string>> returnList = new Dictionary<string, Dictionary<string, string>>();
        if (nodesList_centerDis != null)
        {
            foreach (XmlNode item in nodesList_centerDis)
            {
                string condition = class_XmlHelper.GetAttrValue(item, "condition");
                string type = class_XmlHelper.GetAttrValue(item, "type");
                string chinese = class_XmlHelper.GetAttrValue(item, "chinese");                
                Dictionary<string, string> mapList = new Dictionary<string, string>();
                returnList.Add(type, mapList);
                mapList.Add("title", chinese);
                string symbolList = string.Empty;
                foreach (string tmpSymbol in finishedSymbol)
                {
                    if (condition.Contains(tmpSymbol))
                    {
                        if (string.IsNullOrEmpty(symbolList))
                            symbolList = tmpSymbol;
                        else
                            symbolList = symbolList + "|" + tmpSymbol;
                    }
                }
                mapList.Add("symbols", symbolList);
            }
        }
        return returnList;
    }

    public void set_CreateDisItems(ref XmlNode rootNode,Dictionary<string,DataRow> refSenceDataRows, List<string> finishedSymbol)
    {
        if(finishedSymbol!=null && finishedSymbol.Count>0)
        {
            Dictionary<string, Dictionary<string, string>> mapSymbols = get_disSymbolsList(finishedSymbol);
            foreach(string activeType in mapSymbols.Keys)
            {
                List<string> lstSymbols = mapSymbols[activeType]["symbols"].Split('|').ToList<string>();
                XmlNode itemNode = class_XmlHelper.CreateNode(rootNode.OwnerDocument, "item", "");
                class_XmlHelper.SetAttribute(itemNode, "id", activeType);
                class_XmlHelper.SetAttribute(itemNode, "name", mapSymbols[activeType]["title"]);
                int totalScore = 0;
                foreach (string activeFinishedSymbol in lstSymbols)
                {
                    if (refSenceDataRows.ContainsKey(activeFinishedSymbol))
                    {
                        DataRow activeSenceDataRow = refSenceDataRows[activeFinishedSymbol];
                        string doc = string.Empty;
                        class_Data_SqlDataHelper.GetColumnData(activeSenceDataRow, "config", out doc);
                        XmlDocument tmpConfigSenceDoc = new XmlDocument();
                        tmpConfigSenceDoc.LoadXml(class_CommonUtil.Decoder_Base64(doc));
                        XmlNode scoreNode = tmpConfigSenceDoc.SelectSingleNode("/root/score");
                        if(scoreNode!=null)
                        {
                            string scoreValue = class_XmlHelper.GetAttrValue(scoreNode, "score");
                            int iscoreValue = 0;
                            int.TryParse(scoreValue, out iscoreValue);
                            totalScore = totalScore + iscoreValue;
                        }
                    }
                    else
                        continue;
                }
                class_XmlHelper.SetAttribute(itemNode, "value", totalScore.ToString());
                rootNode.AppendChild(itemNode);

            }
        }
    }

}