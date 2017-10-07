using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Profile_VerifyBase
/// </summary>
public class class_Bus_Profile_Base : class_BusBase
{

    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Profile_Base(class_CommonData refCommonDataObject) : base(refCommonDataObject)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
    }

    protected void VerifyProfileItem(string username)
    {
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable.Rows.Count == 0)
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        else if (textDataTable.Rows.Count > 1)
        {
            for (int rowIndex = 1; rowIndex < textDataTable.Rows.Count; rowIndex++)
            {
                DataRow tmpDataRow = null;
                class_Data_SqlDataHelper.GetActiveRow(textDataTable, rowIndex, out tmpDataRow);
                string removedID = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(tmpDataRow, "id", out removedID);
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@id", removedID);
                Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            }
        }
    }

    protected bool VerifyProfileDocExisted(string username, class_CommonDefined.enumProfileDoc ProfileDocType)
    {
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string strDoc = string.Empty;
        if (textDataTable != null || textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            switch (ProfileDocType)
            {
                case class_CommonDefined.enumProfileDoc.doc_basic:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_basic", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_commnunication:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_commnunication", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_datastore:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_datastore", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_payment:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_payment", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_studystatus:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_studystatus", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_recored:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_recored", out strDoc);
                    break;
                case class_CommonDefined.enumProfileDoc.doc_message:
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "doc_message", out strDoc);
                    break;
            }
            if (string.IsNullOrEmpty(strDoc))
                return false;
            else
                return true;
        }
        else
            return false;
    }

    public bool VerifyProfileDocExisted(string username)
    {
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null || textDataTable.Rows.Count > 0)
            return true;
        else
            return false;
    }

    protected DataRow GetProfileItem(string username)
    {
        DataRow resultRow = null;
        if (string.IsNullOrEmpty(username))
            return resultRow;
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null || textDataTable.Rows.Count > 0)
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out resultRow);
        return resultRow;
    }

    protected string GetProfileDoc(string username, class_CommonDefined.enumProfileDoc profileDocType)
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
            return strDoc;
        }
        else
            return string.Empty;
    }

    protected string GetProfileItemID(string username)
    {
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@username", username);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null || textDataTable.Rows.Count > 0)
        {
            DataRow tmpDataRow;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out tmpDataRow);
            string id = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(tmpDataRow, "id", out id);
            return id;
        }
        else
            return string.Empty;

    }

    protected void SetUpdateProfileItem(string username, class_CommonDefined.enumProfileDoc ProfileDocType, string profileDoc,string profileType)
    {
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();
        profileDoc = class_CommonUtil.Encoder_Base64(profileDoc);
        switch (ProfileDocType)
        {
            case class_CommonDefined.enumProfileDoc.doc_basic:
                activeSPEntry.ModifyParameterValue("@doc_basic", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_commnunication:
                activeSPEntry.ModifyParameterValue("@doc_commnunication", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_datastore:
                activeSPEntry.ModifyParameterValue("@doc_datastore", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_payment:
                activeSPEntry.ModifyParameterValue("@doc_payment", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_recored:
                activeSPEntry.ModifyParameterValue("@doc_recored", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_studystatus:
                activeSPEntry.ModifyParameterValue("@doc_studystatus", profileDoc);
                break;
            case class_CommonDefined.enumProfileDoc.doc_message:
                activeSPEntry.ModifyParameterValue("@doc_message", profileDoc);
                break;
        }
        activeSPEntry.ModifyParameterValue("@type", profileType);
        if (VerifyProfileDocExisted(username))
        {
            activeSPEntry.ModifyParameterValue("@id", GetProfileItemID(username));
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            activeSPEntry.ModifyParameterValue("@username", username);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
    }

    protected XmlDocument GetProfileDocObject(string username, class_CommonDefined.enumProfileDoc profileDocType)
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