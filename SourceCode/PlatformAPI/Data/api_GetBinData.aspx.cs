﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Data_api_GetBinData : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        ISRESPONSEDOC = true;
        string dataId = "";
        dataId = GetQuerystringParam("id");
        if (!string.IsNullOrEmpty(dataId))
        {
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Data_Basic");
            class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();            
            activeSPEntry.ModifyParameterValue("@id", dataId);
            DataTable binDataTable = _objectSqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            string resultXML = class_Data_SqlDataHelper.ActionConvertDTtoXMLString(binDataTable);
            XmlDocument resultDoc = new XmlDocument();
            resultDoc.LoadXml(resultXML);            
            XmlNode rowNode = resultDoc.SelectSingleNode("/root/row");
            if(rowNode!=null)
            {                
                string resultBase64 =class_XmlHelper.GetAttrValue(rowNode,"data");                
                byte[] resultBin = class_CommonUtil.Decoder_Base64ToByte(resultBase64);
                ISRESPONSEDOC=false;
                Response.OutputStream.Write(resultBin, 0, resultBin.Length);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetBinData", "No data.", "");
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Data_api_GetBinData", "Empty data id", "");
        object_CommonLogic.CloseDBConnection();

    }
}