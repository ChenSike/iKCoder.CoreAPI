using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_ProfileDocs
/// </summary>
/// 




public class class_Bus_ProfileDocs
{
    class_CommonData Object_CommonData;

    public class_Bus_ProfileDocs(ref class_CommonData refCommonDataObject)
    {
        Object_CommonData = refCommonDataObject;
        Object_CommonData.PrepareDataOperation();
        
    }

    public DataRow GetProfileItem(string username)
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

    public List<string> GetUsersInProfilePool()
    {
        List<string> result = new List<string>();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
        activeSPEntry.ClearAllParamsValues();        
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null || textDataTable.Rows.Count > 0)
        {
            foreach(DataRow activeDataRow in textDataTable.Rows)
            {
                string username = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "username", out username);
                result.Add(username);
            }
        }
        return result;
    }

    public string GetCurrentStage(string username,string symbol)
    {
        XmlDocument doc = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode currentsenceNode = doc.SelectSingleNode("/studystatus/currentsence[symbol='" + symbol + "']");
        string currentStage = string.Empty;
        if (currentsenceNode != null)
        {            
            XmlNode currentStageNode = currentsenceNode.SelectSingleNode("currentstage");
            currentStage = class_XmlHelper.GetNodeValue(currentStageNode);
        }
        else
            currentStage = "1";
        return currentStage;
    }

    public string GetProfileDoc(string username,class_CommonDefined.enumProfileDoc profileDocType)
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

    public string GetProfileItemID(string username)
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

    public bool GetISProfileItemExisted(string username)
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

    public bool SetDeleteProfileItem(string username)
    {
        if (GetISProfileItemExisted(username))
        {
            string ID = GetProfileItemID(username);
            class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_PROFILEDOCS);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", ID);
            Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            return true;
        }
        else
            return false;
    }

    public bool GetISProfileDocExisted(string username,class_CommonDefined.enumProfileDoc ProfileDocType)
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

    public void SetCodetimeLineHours(string username, string symbol)
    {
        XmlDocument resourceDoc_Profile = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode codetimelineNode = resourceDoc_Profile.SelectSingleNode("/studystatus/codetimeline");
        if (codetimelineNode == null)
        {
            codetimelineNode = class_XmlHelper.CreateNode(resourceDoc_Profile, "codetimeline", "");
            class_XmlHelper.SetAttribute(codetimelineNode, "recordtime", DateTime.Now.ToString());
            resourceDoc_Profile.SelectSingleNode("/studystatus").AppendChild(codetimelineNode);
        }
        else
            class_XmlHelper.SetAttribute(codetimelineNode, "recordtime", DateTime.Now.ToString());
        XmlNodeList itemNodes = codetimelineNode.SelectNodes("item");
        if (itemNodes.Count >= 10)
        {
            XmlNode lastNode = codetimelineNode.SelectSingleNode("item[@index='10']");
            if (lastNode != null)
                codetimelineNode.RemoveChild(lastNode);
        }
        XmlNode activeItemNode = codetimelineNode.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if (activeItemNode == null)
        {
            activeItemNode = class_XmlHelper.CreateNode(resourceDoc_Profile, "item", "");
            class_XmlHelper.SetAttribute(activeItemNode, "index", (itemNodes.Count + 1).ToString());
            class_XmlHelper.SetAttribute(activeItemNode, "date", DateTime.Now.ToString("yyyy-MM-dd"));
            codetimelineNode.AppendChild(activeItemNode);
        }
        string strRecordTime = class_XmlHelper.GetAttrValue(codetimelineNode, "recordtime");
        DateTime dtRecordTime = DateTime.Parse(strRecordTime);
        string strValue = class_XmlHelper.GetAttrValue(activeItemNode, "value");
        int iValue = 0;
        int.TryParse(strValue, out iValue);
        int iTotalMinutesDate = iValue + (DateTime.Now - dtRecordTime).Minutes;
        class_XmlHelper.SetAttribute(activeItemNode, "value", iTotalMinutesDate.ToString());
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus,resourceDoc_Profile.OuterXml);
    }

    public void SetUpdateProfileItem(string username,class_CommonDefined.enumProfileDoc ProfileDocType,string profileDoc)
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
        if (GetISProfileItemExisted(username))
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

    public List<string> GetFinishedSymbols(string username)
    {
        List<string> returnList = new List<string>();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_studystatus));
        XmlNodeList finishItems = doc.SelectNodes("/studystatus/finished/item[@finished='1']");
        foreach(XmlNode finishItem in finishItems)
        {
            string symbol = class_XmlHelper.GetAttrValue(finishItem, "symbol");
            returnList.Add(symbol);
        }
        return returnList;
    }

    public int GetCountOfFinishedStages(string username,string symbol)
    {
        List<string> returnList = new List<string>();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_studystatus));
        XmlNode finishItem = doc.SelectSingleNode("/studystatus/finished/item[@symbol='"+symbol+"']");
        if (finishItem != null)
        {
            XmlNodeList finishStagesNodes = finishItem.SelectNodes("stages/stage[@finish='1']");
            return finishStagesNodes.Count;
        }
        else
            return 0;
    }

    public void VerifyDoc_TimeLineInStatus(string username,ref XmlDocument ref_doc_studystatus)
    {
        XmlDocument doc_studystatus = ref_doc_studystatus;
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_codetimeline = node_studystatus.SelectSingleNode("codetimeline");
        if (node_codetimeline == null)
        {
            node_codetimeline = class_XmlHelper.CreateNode(doc_studystatus, "codetimeline", "");
            node_studystatus.AppendChild(node_codetimeline);
        }
        XmlNode node_year = node_codetimeline.SelectSingleNode("year[@value='" + DateTime.Now.Year + "']");
        if (node_year == null)
        {
            node_year = class_XmlHelper.CreateNode(doc_studystatus, "year", "");
            class_XmlHelper.SetAttribute(node_year, "value", DateTime.Now.Year.ToString());
            node_codetimeline.AppendChild(node_year);
        }
        XmlNode node_month = node_year.SelectSingleNode("month[@value='" + DateTime.Now.Month + "']");
        if (node_month == null)
        {
            node_month = class_XmlHelper.CreateNode(doc_studystatus, "month", "");
            class_XmlHelper.SetAttribute(node_month, "value", DateTime.Now.Month.ToString());
            node_year.AppendChild(node_month);
        }
        XmlNode node_sence = node_month.SelectSingleNode("sences");
        if (node_sence == null)
        {
            node_sence = class_XmlHelper.CreateNode(doc_studystatus, "sences", "");
            node_month.AppendChild(node_sence);
        }
    } 

    public void SetAppStudioTimeLine(string username,string symbol,XmlDocument refdocStudystatus)
    {
        XmlDocument doc_studystatus = refdocStudystatus;
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_codetimeline = node_studystatus.SelectSingleNode("codetimeline");
        XmlNode node_year = node_codetimeline.SelectSingleNode("year[@value='" + DateTime.Now.Year + "']");
        XmlNode node_month = node_year.SelectSingleNode("month[@value='" + DateTime.Now.Month + "']");
        XmlNode node_appstudio = node_month.SelectSingleNode("appstudio");
        XmlNode node_item = node_appstudio.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if (node_item == null)
        {
            node_item = class_XmlHelper.CreateNode(doc_studystatus, "item", "");
            node_appstudio.AppendChild(node_item);
            class_XmlHelper.SetAttribute(node_item, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        string attr_last = class_XmlHelper.GetAttrValue(node_item, "last");
        if (string.IsNullOrEmpty(attr_last))
        {
            attr_last = DateTime.Now.ToString();
            class_XmlHelper.SetAttribute(node_item, "last", attr_last);
        }
        DateTime dt_attr_last = DateTime.Parse(attr_last);
        string attr_opentimes = class_XmlHelper.GetAttrValue(node_item, "opentimes");
        if (string.IsNullOrEmpty(attr_opentimes))
        {
            attr_opentimes = "1";
            class_XmlHelper.SetAttribute(node_item, "opentimes", attr_opentimes);
        }
        int i_attr_opentimes = 0;
        int.TryParse(attr_opentimes, out i_attr_opentimes);
        class_XmlHelper.SetAttribute(node_item, "opentimes", (i_attr_opentimes++).ToString());
        class_XmlHelper.SetAttribute(node_item, "spendtime", (DateTime.Now - dt_attr_last).Minutes.ToString());
        class_XmlHelper.SetAttribute(node_item, "last", DateTime.Now.ToString());
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc_studystatus.OuterXml);
    }

    public void SetSenceTimeLine(string username,string symbol, XmlDocument refdocStudystatus)
    {
        XmlDocument doc_studystatus = refdocStudystatus;
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_codetimeline = node_studystatus.SelectSingleNode("codetimeline");
        XmlNode node_year = node_codetimeline.SelectSingleNode("year[@value='" + DateTime.Now.Year + "']");
        XmlNode node_month = node_year.SelectSingleNode("month[@value='" + DateTime.Now.Month + "']");
        XmlNode node_sence = node_month.SelectSingleNode("sences");
        XmlNode node_item = node_sence.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if(node_item==null)
        {
            node_item = class_XmlHelper.CreateNode(doc_studystatus, "item", "");
            node_sence.AppendChild(node_item);
            class_XmlHelper.SetAttribute(node_item, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        XmlNode node_activeItem = node_item.SelectSingleNode("activeitem[@symbol='" + symbol + "']");
        if(node_activeItem==null)
        {
            node_activeItem = class_XmlHelper.CreateNode(doc_studystatus, "activeitem", "");
            class_XmlHelper.SetAttribute(node_activeItem, "symbol", symbol);
            node_item.AppendChild(node_activeItem);
        }
        string attr_last = class_XmlHelper.GetAttrValue(node_activeItem, "last");
        if (string.IsNullOrEmpty(attr_last))
        {
            attr_last = DateTime.Now.ToString();
            class_XmlHelper.SetAttribute(node_activeItem, "last", attr_last);
        }            
        DateTime dt_attr_last = DateTime.Parse(attr_last);
        string attr_opentimes = class_XmlHelper.GetAttrValue(node_activeItem, "opentimes");
        if (string.IsNullOrEmpty(attr_opentimes))
        {
            attr_opentimes = "1";
            class_XmlHelper.SetAttribute(node_activeItem, "opentimes", attr_opentimes);
        }
        int i_attr_opentimes = 0;
        int.TryParse(attr_opentimes, out i_attr_opentimes);
        class_XmlHelper.SetAttribute(node_activeItem, "opentimes", (i_attr_opentimes++).ToString());
        class_XmlHelper.SetAttribute(node_activeItem, "spendtime", (DateTime.Now - dt_attr_last).Minutes.ToString());
        class_XmlHelper.SetAttribute(node_activeItem, "last", DateTime.Now.ToString());
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc_studystatus.OuterXml);
    }

    public void SetFinishSence(string username,string symbol, XmlDocument refdocStudystatus)
    {
        VerifyDoc_FinishItemNodes(username, symbol, ref refdocStudystatus);
        XmlNode node_item = refdocStudystatus.SelectSingleNode("/studystatus/finished/item[@symbol='" + symbol + "']");
        class_XmlHelper.SetAttribute(node_item, "finish", "1");
        XmlNode node_stages = node_item.SelectSingleNode("stages");
        foreach (XmlNode activeNodeStage in node_stages.SelectNodes("stage"))
            class_XmlHelper.SetAttribute(activeNodeStage, "finish", "1");
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, refdocStudystatus.OuterXml);
    }

    public void SetFinishItemStage(string username,string symbol,string currentStage, XmlDocument refdocStudystatus)
    {
        VerifyDoc_FinishItemNodes(username, symbol,ref refdocStudystatus);
        XmlNode node_item = refdocStudystatus.SelectSingleNode("/studystatus/finished/item[@symbol='" + symbol + "']");
        XmlNode node_stages = node_item.SelectSingleNode("stages");
        XmlNode node_stage = node_stages.SelectSingleNode("stage[@step='" + currentStage + "']");
        class_XmlHelper.SetAttribute(node_stage, "finish", "1");
        bool isSenceFinished = true;
        foreach(XmlNode activeNodeStage in node_stages.SelectNodes("stage"))
        {
            if (class_XmlHelper.GetAttrValue(activeNodeStage, "finish") == "0")
            {
                isSenceFinished = false;
                break;
            }
        }
        if (isSenceFinished)
            class_XmlHelper.SetAttribute(node_item, "finish", "1");
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, refdocStudystatus.OuterXml);
    }

    public void SetNodesValues(string username,Dictionary<string,string> newValuesMap, class_CommonDefined.enumProfileDoc profileType,bool filterRootNode)
    {        
        string strDoc = GetProfileDoc(username, profileType);
        XmlDocument doc_basic = new XmlDocument();
        doc_basic.LoadXml(strDoc);
        bool isUpdated = false;
        foreach(string xpath in newValuesMap.Keys)
        {
            string tmpXpath = xpath;
            if (filterRootNode)
                if (tmpXpath.StartsWith("/root"))
                    tmpXpath = tmpXpath.Replace("/root", "");
            XmlNode itemNode = doc_basic.SelectSingleNode(tmpXpath);
            if (itemNode != null)
            {                
                class_XmlHelper.SetNodeValue(itemNode, newValuesMap[xpath]);
            }
            else
            {
                itemNode = class_XmlHelper.CreateNodeByXpath(doc_basic, tmpXpath, newValuesMap[xpath]);

            }
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml);
    }
        
    public void VerifyDoc_FinishItemNodes(string username,string symbol,ref XmlDocument refdocStudystatus)
    {
        XmlNode node_item = refdocStudystatus.SelectSingleNode("/studystatus/finished/item[@symbol='" + symbol + "']");
        if(node_item==null)
        {
            node_item = class_XmlHelper.CreateNode(refdocStudystatus, "item", "");
            class_XmlHelper.SetAttribute(node_item, "symbol", symbol);
            class_XmlHelper.SetAttribute(node_item, "finish", "0");
            XmlNode finishedNode = refdocStudystatus.SelectSingleNode("/studystatus/finished");
            finishedNode.AppendChild(node_item);
        }
        int countOfStages = class_Bus_SenceDoc.GetCountOfStages(Object_CommonData, symbol);
        XmlNode node_stages = node_item.SelectSingleNode("stages");
        if(node_stages==null)
        {
            node_stages = class_XmlHelper.CreateNode(refdocStudystatus, "stages", "");
            node_item.AppendChild(node_stages);
        }        
        for(int i=1;i<=countOfStages;i++)
        {
            XmlNode node_stage = node_stages.SelectSingleNode("stage[@step='" + i + "']");
            if (node_stage == null)
            {
                node_stage = class_XmlHelper.CreateNode(refdocStudystatus, "stage", "");
                class_XmlHelper.SetAttribute(node_stage, "finish", "0");
                class_XmlHelper.SetAttribute(node_stage, "step", i.ToString());
                node_stages.AppendChild(node_stage);
            }
        }
    }
        
    public void SetUpdateAppStudioLastTime(string username,string symbol, XmlDocument refdocStudystatus)
    {
        XmlDocument doc_studystatus = refdocStudystatus;
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_codetimeline = node_studystatus.SelectSingleNode("codetimeline");
        XmlNode node_year = node_codetimeline.SelectSingleNode("year[@value='" + DateTime.Now.Year + "']");
        XmlNode node_month = node_year.SelectSingleNode("month[@value='" + DateTime.Now.Month + "']");
        XmlNode node_appstudio = node_month.SelectSingleNode("appstudio");
        XmlNode node_item = node_appstudio.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if (node_item == null)
        {
            node_item = class_XmlHelper.CreateNode(doc_studystatus, "item", "");
            node_appstudio.AppendChild(node_item);
            class_XmlHelper.SetAttribute(node_item, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        }     
        class_XmlHelper.SetAttribute(node_item, "last", DateTime.Now.ToString());
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc_studystatus.OuterXml);
    }

    public void SetUpdateSencesLastTime(string username,string symbol, XmlDocument refdocStudystatus)
    {
        XmlDocument doc_studystatus = refdocStudystatus;
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_codetimeline = node_studystatus.SelectSingleNode("codetimeline");
        XmlNode node_year = node_codetimeline.SelectSingleNode("year[@value='" + DateTime.Now.Year + "']");
        XmlNode node_month = node_year.SelectSingleNode("month[@value='" + DateTime.Now.Month + "']");
        XmlNode node_sence = node_month.SelectSingleNode("sences");
        XmlNode node_item = node_sence.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if (node_item == null)
        {
            node_item = class_XmlHelper.CreateNode(doc_studystatus, "item", "");
            node_sence.AppendChild(node_item);
            class_XmlHelper.SetAttribute(node_item, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        }
        XmlNode node_activeItem = node_item.SelectSingleNode("activeitem[@symbol='" + symbol + "']");
        if (node_activeItem == null)
        {
            node_activeItem = class_XmlHelper.CreateNode(doc_studystatus, "activeitem", "");
            class_XmlHelper.SetAttribute(node_activeItem, "symbol", symbol);
            node_item.AppendChild(node_activeItem);
        }
        class_XmlHelper.SetAttribute(node_activeItem, "last", DateTime.Now.ToString());
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc_studystatus.OuterXml);
    }

    public void SetCurrentStage(string username,string symbol,string currentstage)
    {
        VerifyDoc_Currentsence(username, symbol);
        XmlDocument doc = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode currentsenceNode = doc.SelectSingleNode("/studystatus/currentsence[symbol[text()='" + symbol + "']]");
        if (currentsenceNode != null)
        {
            string currentStage = string.Empty;
            XmlNode currentStageNode = currentsenceNode.SelectSingleNode("currentstage");
            class_XmlHelper.SetNodeValue(currentStageNode, currentstage);
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc.OuterXml);
        }
    }

    public bool GetMessageIsRMV(string username,string operationID)
    {
        XmlDocument resourceDoc = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlNode messagestatusNode = resourceDoc.SelectSingleNode("/messagestatus");
        XmlNode rmv_msg_node = messagestatusNode.SelectSingleNode("rmv_msg");
        XmlNode item = rmv_msg_node.SelectSingleNode("item[@id='" + operationID + "']");
        if (item != null)
            return true;
        else
            return false;
    }

    public List<XmlNode> GetCodetimeLineItems(string username,XmlDocument sourceDoc_StudyStatus)
    {
        List<XmlNode> resultList = new List<XmlNode>();
        XmlNodeList codetimelineItemNodes = sourceDoc_StudyStatus.SelectNodes("/studystatus/codetimeline/item");
        foreach (XmlNode activeItem in codetimelineItemNodes)
            resultList.Add(activeItem);
        return resultList;
    }

    public bool GetMessageIsRead(string username,string operationID)
    {
        XmlDocument resourceDoc = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlNode messagestatusNode = resourceDoc.SelectSingleNode("/messagestatus");
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        XmlNode item = read_sys_Node.SelectSingleNode("item[@id='" + operationID + "']");
        if (item != null)
            return true;
        else
            return false;
    }

    public int GetCountOfUnreadMessage(string username)
    {
        XmlDocument resourceDoc = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlNode messagestatusNode = resourceDoc.SelectSingleNode("/message");
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        XmlNodeList items = read_sys_Node.SelectNodes("item");
        if (items != null)
            return items.Count;
        else
            return 0;
    }

    public void VerifyDoc_Currentsence(string username,string symbol)
    {
        XmlDocument sourceDoc_studyStatus = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode node_studystatus = sourceDoc_studyStatus.SelectSingleNode("/studystatus");
        XmlNode node_currentsence = node_studystatus.SelectSingleNode("currentsence[symbol='" + symbol + "']");
        if(node_currentsence==null)
        {
            node_currentsence = class_XmlHelper.CreateNode(sourceDoc_studyStatus, "currentsence", "");
            node_studystatus.AppendChild(node_currentsence);
            XmlNode node_symbol = class_XmlHelper.CreateNode(sourceDoc_studyStatus, "symbol", symbol);
            node_currentsence.AppendChild(node_symbol);
            XmlNode node_fiststarttime = class_XmlHelper.CreateNode(sourceDoc_studyStatus, "fiststarttime", DateTime.Now.ToString());
            node_currentsence.AppendChild(node_fiststarttime);
            XmlNode node_currentstage = class_XmlHelper.CreateNode(sourceDoc_studyStatus, "currentstage", "1");
            node_currentsence.AppendChild(node_currentstage);
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, sourceDoc_studyStatus.OuterXml);
        }
    }

    

    public void SetMessageToRead(string username,string operationID)
    {
        string strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlDocument doc_message = new XmlDocument();
        doc_message.LoadXml(strDoc);
        if(!VerifyReadedMessage(username,operationID))
        {
            XmlNode node_readsys = doc_message.SelectSingleNode("/messagestatus/read_sys");
            XmlNode node_newitem = class_XmlHelper.CreateNode(doc_message, "item", "");
            class_XmlHelper.SetAttribute(node_newitem, "id", operationID);
            node_readsys.AppendChild(node_newitem);
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_message, doc_message.OuterXml);
        }
    }

    public void SetTotalExp(string username,double totalExp)
    {
        XmlDocument sourceDoc_studystatus = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode node_studystatus = sourceDoc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_exp = node_studystatus.SelectSingleNode("exp");
        class_XmlHelper.SetAttribute(node_exp, "total", totalExp.ToString());
    }

    public string GetTotalExp(string username)
    {
        XmlDocument sourceDoc_studystatus = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        XmlNode node_studystatus = sourceDoc_studystatus.SelectSingleNode("/studystatus");
        XmlNode node_exp = node_studystatus.SelectSingleNode("exp");
        return class_XmlHelper.GetAttrValue(node_exp, "total");
    }

    public bool VerifyReadedMessage(string username,string operationID)
    {
        string strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlDocument doc_message = new XmlDocument();
        doc_message.LoadXml(strDoc);
        XmlNode read_item = doc_message.SelectSingleNode("/messagestatus/read_sys/item[@id='" + operationID + "']");
        if (read_item == null)
            return false;
        else
            return true;
    }

    public void SetMessageToRMV(string username,string operationID)
    {
        string strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlDocument doc_message = new XmlDocument();
        doc_message.LoadXml(strDoc);
        if (!VerifyReadedMessage(username, operationID))
        {
            XmlNode node_readsys = doc_message.SelectSingleNode("/messagestatus/rmv_sys");
            XmlNode node_newitem = class_XmlHelper.CreateNode(doc_message, "item", "");
            class_XmlHelper.SetAttribute(node_newitem, "id", operationID);
            node_readsys.AppendChild(node_newitem);
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_message, doc_message.OuterXml);
        }
    }

    public bool VerifyRMVMessage(string username,string operationID)
    {
        string strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        XmlDocument doc_message = new XmlDocument();
        doc_message.LoadXml(strDoc);
        XmlNode read_item = doc_message.SelectSingleNode("/messagestatus/rmv_sys/item[@id='" + operationID + "']");
        if (read_item == null)
            return false;
        else
            return true;
    }

    public string GetDocNoteValue(string username,class_CommonDefined.enumProfileDoc profileDocType,string xpath)
    {
        XmlDocument doc = new XmlDocument();
        if (GetISProfileDocExisted(username, profileDocType))
        {
            doc.LoadXml(GetProfileDoc(username, profileDocType));
            XmlNode activeNode = doc.SelectSingleNode(xpath);
            if (activeNode != null)
            {
                string result = string.Empty;
                result = class_XmlHelper.GetNodeValue(activeNode);
                return result;
            }
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    public Dictionary<string,string> GetDocNotesValues(string username, class_CommonDefined.enumProfileDoc profileDocType, List<string> xpathSet,bool filterRoorNode)
    {
        XmlDocument doc = new XmlDocument();
        Dictionary<string, string> resultLst = new Dictionary<string, string>();
        if (GetISProfileDocExisted(username, profileDocType))
        {
            doc.LoadXml(GetProfileDoc(username, profileDocType));
            foreach (string selectXpath in xpathSet)
            {
                string tmpXpath = selectXpath;
                if (filterRoorNode && tmpXpath.StartsWith("/root"))
                    tmpXpath = tmpXpath.Replace("/root", "");
                XmlNode activeNode = doc.SelectSingleNode(tmpXpath);
                if (activeNode != null)
                {
                    string result = string.Empty;
                    result = class_XmlHelper.GetNodeValue(activeNode);
                    if (!resultLst.ContainsKey(selectXpath))
                        resultLst.Add(selectXpath, result);
                }
            }
            return resultLst;
        }
        else
            return resultLst;
    }

    public string GetDocAttrValue(string username, class_CommonDefined.enumProfileDoc profileDocType, string xpath,string attrName)
    {
        XmlDocument doc = new XmlDocument();
        if (GetISProfileDocExisted(username, profileDocType))
        {
            doc.LoadXml(GetProfileDoc(username, profileDocType));
            XmlNode activeNode = doc.SelectSingleNode(xpath);
            if (activeNode != null)
            {
                string result = string.Empty;
                result = class_XmlHelper.GetAttrValue(activeNode, attrName);
                return result;
            }
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    public void VerifyProfileItem(string username)
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

    public void VerifyDoc_Basic(string username)
    {
        XmlDocument doc_basic = new XmlDocument();
        string strDoc = string.Empty;
        bool isUpdated = false;
        if (GetISProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_basic))
            strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_basic);
        else
        {
            strDoc = "<usrbasic></usrbasic>";
            isUpdated = true;
        }
        doc_basic.LoadXml(strDoc);
        XmlNode node_usrbasic = doc_basic.SelectSingleNode("/usrbasic");
        if (node_usrbasic == null)
        {
            strDoc = "<usrbasic></usrbasic>";
            doc_basic.LoadXml(strDoc);
            node_usrbasic = doc_basic.SelectSingleNode("/usrbasic");
            isUpdated = true;
        }
        XmlNode node_usrname = node_usrbasic.SelectSingleNode("usr_name");
        if (node_usrname == null)
        {
            node_usrname = class_XmlHelper.CreateNode(doc_basic, "usr_name", username);
            node_usrbasic.AppendChild(node_usrname);
            isUpdated = true;
        }
        XmlNode node_usrnickname = node_usrbasic.SelectSingleNode("usr_nickname");
        if (node_usrnickname == null)
        {
            node_usrnickname = class_XmlHelper.CreateNode(doc_basic, "usr_nickname", username);
            node_usrbasic.AppendChild(node_usrnickname);
            isUpdated = true;
        }
        XmlNode node_sex = node_usrbasic.SelectSingleNode("sex");
        if (node_sex == null)
        {
            node_sex = class_XmlHelper.CreateNode(doc_basic, "sex", "1");
            node_usrbasic.AppendChild(node_sex);
            isUpdated = true;
        }
        XmlNode node_birthday = node_usrbasic.SelectSingleNode("birthday");
        if (node_birthday == null)
        {
            node_birthday = class_XmlHelper.CreateNode(doc_basic, "birthday", "2000-01-01");
            node_usrbasic.AppendChild(node_birthday);
            isUpdated = true;
        }
        XmlNode node_state = node_usrbasic.SelectSingleNode("state");
        if (node_state == null)
        {
            node_state = class_XmlHelper.CreateNode(doc_basic, "state", "");
            node_usrbasic.AppendChild(node_state);
            isUpdated = true;
        }
        XmlNode node_city = node_usrbasic.SelectSingleNode("city");
        if (node_city == null)
        {
            node_city = class_XmlHelper.CreateNode(doc_basic, "city", "");
            node_usrbasic.AppendChild(node_city);
            isUpdated = true;
        }
        XmlNode node_school = node_usrbasic.SelectSingleNode("school");
        if (node_school == null)
        {
            node_school = class_XmlHelper.CreateNode(doc_basic, "school", "");
            node_usrbasic.AppendChild(node_school);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml);
    }

    public void VerifyDoc_Finished(string username,ref XmlDocument refdocStudystatus)
    {
        XmlNode node_studystatus = refdocStudystatus.SelectSingleNode("/studystatus");
        XmlNode node_finished = node_studystatus.SelectSingleNode("/finished");
        if (node_finished == null)
        {
            node_finished = class_XmlHelper.CreateNode(refdocStudystatus, "finished","");
            node_studystatus.AppendChild(node_finished);
        }        
    }

    public void VerifyDoc_Studystatus(string username)
    {
        XmlDocument doc_studystatus = new XmlDocument();
        string strDoc = string.Empty;
        if (GetISProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_studystatus))
            strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_studystatus);
        else
            strDoc = "<studystatus></studystatus>";
        doc_studystatus.LoadXml(strDoc);
        XmlNode node_studystatus = doc_studystatus.SelectSingleNode("/studystatus");
        if(node_studystatus==null)
        {
            strDoc = "<studystatus></studystatus>";
            doc_studystatus.LoadXml(strDoc);
        }
        XmlNode node_exp = node_studystatus.SelectSingleNode("exp");
        if(node_exp==null)
        {
            node_exp = class_XmlHelper.CreateNode(doc_studystatus, "exp", "");
            class_XmlHelper.SetAttribute(node_exp, "total", "0");
            node_studystatus.AppendChild(node_exp);
        }
        VerifyDoc_TimeLineInStatus(username,ref doc_studystatus);
        VerifyDoc_Finished(username, ref doc_studystatus);
        SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_studystatus, doc_studystatus.OuterXml);
    }

    public void VerifyDoc_Message(string username)
    {
        XmlDocument doc_message = new XmlDocument();
        bool isUpdated = false;
        string strDoc = string.Empty;
        if (GetISProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_message))
            strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        else
        {
            strDoc = "<message></message>";
            isUpdated = true;
        }
        doc_message.LoadXml(strDoc);
        XmlNode node_messsage = doc_message.SelectSingleNode("/message");
        XmlNode node_readsys = node_messsage.SelectSingleNode("read_sys");
        if (node_readsys == null)
        {
            node_readsys = class_XmlHelper.CreateNode(doc_message, "read_sys", "");
            node_messsage.AppendChild(node_readsys);
            isUpdated = true;
        }
        XmlNode node_rmvsys = node_messsage.SelectSingleNode("rmv_sys");
        if(node_rmvsys==null)
        {
            node_rmvsys = class_XmlHelper.CreateNode(doc_message, "rmv_sys","");
            node_messsage.AppendChild(node_rmvsys);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_message, doc_message.OuterXml);
    }   

    public void VerifyDataStore(string username)
    {
        XmlDocument doc_datastore = new XmlDocument();
        bool isUpdated = false;
        string strDoc = string.Empty;
        if (GetISProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_datastore))
            doc_datastore = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_datastore);
        else
        {
            strDoc = "<datastore></datastore>";
            isUpdated = true;
            doc_datastore.LoadXml(strDoc);
        }
        
    }

    public void VerifyAll(string username)
    {
        VerifyProfileItem(username);
        VerifyDoc_Basic(username);
        VerifyDoc_Studystatus(username);
        VerifyDoc_Message(username);        
    }

}