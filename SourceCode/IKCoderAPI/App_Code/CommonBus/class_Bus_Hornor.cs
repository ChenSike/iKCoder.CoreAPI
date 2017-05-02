using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Hornor
/// </summary>
public class class_Bus_Hornor
{

    public const string symbol_hornor_config = "data_list_hornor";

    protected XmlDocument sourceDoc_hornor = new XmlDocument();
    protected XmlNodeList nodesList_centerHornor;

    public void init_SourceDoc(class_CommonData Object_CommonData)
    {
        string result = string.Empty;
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        activeSPEntry_configSence.ClearAllParamsValues();
        activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol_hornor_config);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            string data = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "data", out data);
            data = class_CommonUtil.Decoder_Base64(data);
            sourceDoc_hornor.LoadXml(data);
            nodesList_centerHornor = sourceDoc_hornor.SelectNodes("/root/centerhornor/item");
        }
    }

    public List<Dictionary<string, string>> get_HornorConfig(List<string> finishedSymbol)
    {
        List<Dictionary<string, string>> returnList = new List<Dictionary<string, string>>();
        if (nodesList_centerHornor != null)
        {
            foreach (XmlNode item in nodesList_centerHornor)
            {
                Dictionary<string, string> hornorItem = new Dictionary<string, string>();
                bool isActive = false;
                string condition = class_XmlHelper.GetAttrValue(item, "condition");
                foreach (string tmpSymbol in finishedSymbol)
                {
                    if (condition.Contains(tmpSymbol))
                    {
                        isActive = true;
                        break;
                    }
                }
                string name = class_XmlHelper.GetAttrValue(item, "name");
                string active = class_XmlHelper.GetAttrValue(item, "active");
                string inactive = class_XmlHelper.GetAttrValue(item, "inactive");
                hornorItem.Add("name", name);
                hornorItem.Add("image", isActive ? active : inactive);
                returnList.Add(hornorItem);
            }           

        }
        return returnList;
    }

    public class_Bus_Hornor()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

