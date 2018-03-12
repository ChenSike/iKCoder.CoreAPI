using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Profile_Student
/// </summary>
public class class_Bus_Profile_Common
{
    class_CommonData Object_CommonData;
    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Profile_Common(ref class_CommonData refCommonDataObject)
    {
        Object_CommonData = refCommonDataObject;
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
    }

    public DataRow GetProfileItem(string username)
    {
        DataRow resultRow = null;
        if (string.IsNullOrEmpty(username))
            return resultRow;
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null || textDataTable.Rows.Count > 0)
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out resultRow);
        return resultRow;
    }

    public XmlDocument GetProfileDocObject(string username, class_CommonDefined.enumProfileDoc profileDocType)
    {
        DataRow activeProfileItem = GetProfileItem(username);
        if (activeProfileItem != null)
        {
            string strDoc = string.Empty;
            switch (profileDocType)
            {
                case class_CommonDefined.enumProfileDoc.doc_basic:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_basic", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_commnunication:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_commnunication", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_datastore:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_datastore", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_payment:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_payment", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_studystatus:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_studystatus", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_recored:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_recored", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_message:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeProfileItem, "doc_message", out strDoc);
                    break;
            }
            strDoc = class_CommonUtil.Decoder_Base64(strDoc);
            XmlDocument result = new XmlDocument();
            result.LoadXml(strDoc);
            return result;
        }
        else
            return null;
    }


}