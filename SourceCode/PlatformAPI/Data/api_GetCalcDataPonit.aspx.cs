using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;


public partial class Data_api_GetCalcDataPonit : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {
            Dictionary<string, XmlDocument> dataSource_Buffer = new Dictionary<string, XmlDocument>();
            Dictionary<string, double> dataPoint_Buffer = new Dictionary<string, double>();
            XmlNode sourceNode = REQUESTDOCUMENT.SelectSingleNode("/root/source");
            XmlNode defindNode = REQUESTDOCUMENT.SelectSingleNode("/root/defined");
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
            class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();
            foreach (XmlNode sourceItemNode in sourceNode.SelectNodes("item"))
            {
                string symbol = class_XmlHelper.GetAttrValue(sourceItemNode, "symbol");
                string id = class_XmlHelper.GetAttrValue(sourceItemNode, "id");
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@symbol", symbol);
                DataTable textDataTable = _objectSqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                if (textDataTable != null && textDataTable.Rows.Count > 0)
                {
                    string strDoc = "";
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(textDataTable.Rows[0], "data", out strDoc);
                    string calcEnBase64 = class_CommonUtil.Decoder_Base64(strDoc);
                    XmlDocument resultDoc = new XmlDocument();
                    resultDoc.LoadXml(strDoc);
                    dataSource_Buffer.Add(id, resultDoc);
                }
            }
            foreach (XmlNode definedItemNode in defindNode.SelectNodes("item"))
            {
                string source = class_XmlHelper.GetAttrValue(definedItemNode, "source");
                string id = class_XmlHelper.GetAttrValue(definedItemNode, "id");
                string value = class_XmlHelper.GetAttrValue(definedItemNode, "value");
                string xpath = class_XmlHelper.GetAttrValue(definedItemNode, "xpath");
                if (string.IsNullOrEmpty(value))
                {
                    if (dataSource_Buffer.ContainsKey(source))
                    {
                        XmlDocument activeSourceDocument = dataSource_Buffer[source];
                        XmlNode valueNode = activeSourceDocument.SelectSingleNode(xpath);
                        if (valueNode != null)
                        {
                            value = class_XmlHelper.GetNodeValue(valueNode);
                        }
                        else
                        {
                            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetCalcDataPonit", "Wrong Xpath.", "");
                            return;
                        }
                    }
                    else
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetCalcDataPonit", "Wrong Source.", "");
                        return;
                    }
                }
                double dValue = 0.0;
                double.TryParse(value, out dValue);
                dataPoint_Buffer.Add(id, dValue);
            }
            XmlNode calcNode = REQUESTDOCUMENT.SelectSingleNode("/root/calc");
            if (calcNode != null)
            {
                string calcExp = class_XmlHelper.GetNodeValue(calcNode);
                if (!string.IsNullOrEmpty(calcExp))
                {
                    foreach (string activeID in dataPoint_Buffer.Keys)
                    {
                        if(calcExp.Contains(activeID))
                        {
                            string value = dataPoint_Buffer[activeID].ToString();
                            calcExp = calcExp.Replace(activeID, value);
                        }
                    }
                    DataTable tmpDataTableObject = new DataTable();
                    object computeResult = tmpDataTableObject.Compute(calcExp, "");
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), computeResult.ToString(), "");
                }
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetCalcDataPonit", "No CalcExpr.", "");
                    return;
                }
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetCalcDataPonit", "No CalcNode.", "");
                return;
            }

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetMetaText", "No Input.", "");
    }
}