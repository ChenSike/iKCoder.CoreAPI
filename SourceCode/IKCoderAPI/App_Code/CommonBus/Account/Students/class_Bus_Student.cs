using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Student
/// </summary>
public class class_Bus_Student:class_BusBase
{
    class_Data_SqlSPEntry activeSPEntry;

    public class_Bus_Student(class_CommonData refObjectCommonData) : base(refObjectCommonData)
    {
        activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_POOL_STUDENTS);
        Object_CommonData.PrepareDataOperation();
    }

    public string GetStudentID(string name)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@name", name);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        string id = string.Empty;
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            class_Data_SqlDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out id);
        }
        return id;
    }

    public bool GetISStudentExisted(string name)
    {
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@name", name);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (activeDataTable != null && activeDataTable.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public XmlDocument GetReadyStudents(string centersymbol)
    {
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml("<root></root>");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
        activeSPEntry.ModifyParameterValue("@status", class_CommonDefined.enumStudentStatus.ready);
        DataTable activeDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        int index = 1;
        class_Bus_ProfileDocs profileObject = new class_Bus_ProfileDocs(ref Object_CommonData);
        foreach(DataRow activeDR in activeDataTable.Rows)
        {
            string data_symbol = string.Empty;
            string data_nickname = string.Empty;
            string data_sex = string.Empty;
            string data_grade = string.Empty;
            string data_created = string.Empty;
            string data_regdate = string.Empty;
            XmlNode newItemNode = class_XmlHelper.CreateNode(resultDoc, "item", "");
            class_XmlHelper.SetAttribute(newItemNode, "index", index.ToString());
            class_Data_SqlDataHelper.GetColumnData(activeDR, "symbol", out data_symbol);
            class_Data_SqlDataHelper.GetColumnData(activeDR, "regdate", out data_regdate);
            class_Data_SqlDataHelper.GetColumnData(activeDR, "grade", out data_grade);
            class_Data_SqlDataHelper.GetColumnData(activeDR, "created", out data_created);
            data_nickname = profileObject.GetDocNoteValue(data_symbol, class_CommonDefined.enumProfileDoc.doc_basic, "/usrbasic/usr_nickname");
            data_sex = profileObject.GetDocNoteValue(data_symbol, class_CommonDefined.enumProfileDoc.doc_basic, "/usrbasic/sex");
            class_XmlHelper.SetAttribute(newItemNode, "symbol", data_symbol);
            class_XmlHelper.SetAttribute(newItemNode, "nickname", data_nickname);
            class_XmlHelper.SetAttribute(newItemNode, "grade", data_grade);
            class_XmlHelper.SetAttribute(newItemNode, "sex", data_sex);
            class_XmlHelper.SetAttribute(newItemNode, "created", data_created);
            class_XmlHelper.SetAttribute(newItemNode, "regdate", data_regdate);
            resultDoc.SelectSingleNode("/root").AppendChild(newItemNode);
        }
        return resultDoc;
    }
    
    public void SwitchStudentStatus(string symbol,string centersymbol, class_CommonDefined.enumStudentStatus activeStudentStatus)
    {

    }



    public void SetUpdateStudent(string name, string password, string centersymbol)
    {
        if (!GetISStudentExisted(name))
        {
            class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
            string reg_date = DateTime.Now.ToString("yyyy-MM-dd");
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@name", name);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@status", "0");
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            activeSPEntry.ModifyParameterValue("@regdate", reg_date);
            Object_CommonData.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        else
        {
            string id = GetStudentID(name);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@password", password);
            activeSPEntry.ModifyParameterValue("@centersymbol", centersymbol);
            Object_CommonData.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        }
        class_Bus_Profile_VerifyStudentDocs objectStudentDocs = new class_Bus_Profile_VerifyStudentDocs(ref Object_CommonData);
        objectStudentDocs.VerifyAll(name);
    }

}