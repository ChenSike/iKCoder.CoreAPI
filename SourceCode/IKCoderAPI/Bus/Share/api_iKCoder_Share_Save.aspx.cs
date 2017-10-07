using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;
using System.IO;

public partial class Bus_Share_api_iKCoder_Share_Save : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        if (REQUESTDOCUMENT != null && !string.IsNullOrEmpty(REQUESTDOCUMENT.OuterXml))
        {
            XmlNode node_sencesymbol = REQUESTDOCUMENT.SelectSingleNode("/root/sencesymbol");
            string sencesymbol = "default";
            if(node_sencesymbol!=null)
                sencesymbol = class_XmlHelper.GetNodeValue(node_sencesymbol);
            XmlNode node_configdoc = REQUESTDOCUMENT.SelectSingleNode("/root/config");
            if (node_configdoc == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "config->empty", "");
                return;
            }
            string configdoc = node_configdoc.InnerXml;
            string hostName = HttpContext.Current.Request.Url.Host;
            string serverPath = hostName + "/ikcoder/";
            XmlNode node_serverPathNode = REQUESTDOCUMENT.SelectSingleNode("/root/serverpath");
            if (node_serverPathNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "serverpath->empty", "");
                return;
            }
            serverPath = class_XmlHelper.GetNodeValue(node_serverPathNode);
            string symbol = Guid.NewGuid().ToString();
            string shareLink = serverPath + "share.html?symbol=" + symbol;
            List<string> QRImgSymbolList = new List<string>();
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TMPSENCES);
            activeSPEntry.ClearAllParamsValues();
            DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (textDataTable != null)
            {
                foreach (DataRow activeDataRow in textDataTable.Rows)
                {
                    string str_createdtime = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "createdtime", out str_createdtime);
                    DateTime dt_createdtime = DateTime.Parse(str_createdtime);
                    string id = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                    string sharesymbol = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out sharesymbol);
                    if ((DateTime.Now- dt_createdtime).Days > 7)
                    {
                        QRImgSymbolList.Add("QR_TMP_" + sharesymbol);
                        activeSPEntry.ClearAllParamsValues();
                        activeSPEntry.ModifyParameterValue("@id", id);
                        Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                    }
                }
            }          
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@createdtime", DateTime.Now.ToString());
            activeSPEntry.ModifyParameterValue("@config", configdoc);
            activeSPEntry.ModifyParameterValue("@sencesymbol", sencesymbol);            
            activeSPEntry.ModifyParameterValue("@sharedlink", shareLink);
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            Util_QRCoder objectQRCoder = new Util_QRCoder();
            byte[] linkQRData = objectQRCoder.CreateQRToByteArr(shareLink, System.Drawing.Imaging.ImageFormat.Png);
            string linkQRDataBase64 = class_CommonUtil.Encoder_Base64(linkQRData);
            string symbolQRData = "QR_TMP_" + symbol;
            class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
            if (QRImgSymbolList.Count > 0)
            {
                foreach (string tmpSymbol in QRImgSymbolList)
                {                    
                    activeSPEntry_binData.ClearAllParamsValues();
                    activeSPEntry_binData.ModifyParameterValue("@symbol", tmpSymbol);
                    DataTable dt_QRData = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                    string QRBinDataID = string.Empty;
                    if (dt_QRData != null && dt_QRData.Rows.Count > 0)
                    {
                        DataRow activeDataRow = null;
                        class_Data_SqlDataHelper.GetActiveRow(dt_QRData, 0, out activeDataRow);
                        class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out QRBinDataID);
                        activeSPEntry_binData.ClearAllParamsValues();
                        activeSPEntry_binData.ModifyParameterValue("@id", QRBinDataID);
                        Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                    }
                }
            }
            activeSPEntry_binData.ClearAllParamsValues();
            activeSPEntry_binData.ModifyParameterValue("@symbol", symbolQRData);
            activeSPEntry_binData.ModifyParameterValue("@type", "png");
            activeSPEntry_binData.ModifyParameterValue("@owner", "system");
            activeSPEntry_binData.ModifyParameterValue("@data", linkQRDataBase64);
            activeSPEntry_binData.ModifyParameterValue("@isbyte", "0");
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            Dictionary<string, string> resultMap = new Dictionary<string, string>();
            resultMap.Add("link", shareLink);
            resultMap.Add("qrsymbol", symbolQRData);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), resultMap);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "request document->empty", "");
            return;

        }
    }
}