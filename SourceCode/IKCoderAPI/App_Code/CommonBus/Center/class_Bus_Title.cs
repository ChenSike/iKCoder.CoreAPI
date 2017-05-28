using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Title
/// </summary>
public class class_Bus_Title
{

    public const string symbol_title_config = "data_list_title";
    protected XmlDocument sourceDoc_title = new XmlDocument();

    public class_Bus_Title(class_CommonData Object_CommonData)
    {
        //
        // TODO: Add constructor logic here
        //
        string result = string.Empty;
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol_title_config);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            string data = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out data);
            data = class_CommonUtil.Decoder_Base64(data);
            sourceDoc_title.LoadXml(data);
        }
    }

    public string GetTitle(List<string> finishedSymbols)
    {
        int title_index = 0;
        string title = string.Empty;
        foreach (XmlNode activeTitle in sourceDoc_title.SelectNodes("/root/title"))
        {
            string str_condition = class_XmlHelper.GetAttrValue(activeTitle, "condition");
            string str_index = class_XmlHelper.GetAttrValue(activeTitle, "index");
            string str_title = class_XmlHelper.GetAttrValue(activeTitle, "value");
            int i_index = 0;
            int.TryParse(str_index, out i_index);
            if (string.IsNullOrEmpty(str_condition))
            {
                title = str_title;
                title_index = i_index;
            }
            else
            {
                string[] arrConditions = str_condition.Split('|');
                Dictionary<string, bool> verifyCondition = new Dictionary<string, bool>();
                foreach (string activeCondition in arrConditions)
                {
                    if (finishedSymbols.Contains(activeCondition))
                        if (!verifyCondition.ContainsKey(activeCondition))
                            verifyCondition.Add(activeCondition, true);
                        else
                             if (!verifyCondition.ContainsKey(activeCondition))
                            verifyCondition.Add(activeCondition, false);

                }
                bool verifyChangeTitle = true;
                foreach(string activeCondition in verifyCondition.Keys)
                {
                    if (verifyCondition[activeCondition] == false)
                    {
                        verifyChangeTitle = false;
                        break;
                    }
                }
                if(verifyChangeTitle && i_index > title_index)
                    title = str_title;
            }
        }
        return title;
    }

}