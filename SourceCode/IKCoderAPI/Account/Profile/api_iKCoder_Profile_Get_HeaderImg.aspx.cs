using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Account_Profile_api_iKCoder_Profile_Get_HeaderImg : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = "img_header_" + logined_user_name;
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
        activeSPEntry_binData.ClearAllParamsValues();
        activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
        DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (binDataTable != null && binDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
            byte[] headerImageData = class_Data_SqlDataHelper.GetArrBytesColumnData(activeDataRow, "data");
            if (headerImageData != null && headerImageData.Length > 0)
            {
                switchResponseMode(enumResponseMode.bin);
                RESPONSEBUFFER = headerImageData;
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);
        }
        else
        {
            symbol = "img_header_tmp_" + logined_user_name;
            activeSPEntry_binData.ClearAllParamsValues();
            activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
            binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (binDataTable != null && binDataTable.Rows.Count > 0)
            {
                DataRow activeDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                byte[] headerImageData = class_Data_SqlDataHelper.GetArrBytesColumnData(activeDataRow, "data");
                if (headerImageData != null && headerImageData.Length > 0)
                {
                    switchResponseMode(enumResponseMode.bin);
                    RESPONSEBUFFER = headerImageData;
                }
            }
            else
            {
                symbol = "img_header_default";
                string URL = Server_API + Virtul_Folder_API + "/Data/api_GetBinDataWithBase64.aspx?symbol=" + symbol;
                string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
                XmlDocument resultDoc = new XmlDocument();
                resultDoc.LoadXml(returnDoc);
                XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
                if (msgNode != null)
                {
                    string data = class_XmlHelper.GetAttrValue(msgNode, "data");
                    if (!string.IsNullOrEmpty(data))
                    {
                        switchResponseMode(enumResponseMode.bin);
                        RESPONSEBUFFER = class_CommonUtil.Decoder_Base64ToByte(data);
                    }
                    else
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "", enum_MessageType.Exception);
                    }
                }
            }
        }

    }
}