using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Center_api_iKCoder_Center_Get_CenterInfo : class_WebBase_IKCoderAPI_UA
{
    XmlDocument centerDocument = new XmlDocument();
    XmlDocument sourceDoc_sence = new XmlDocument();
    XmlDocument sourceDoc_profile = new XmlDocument();
    Dictionary<string, DataRow> sourceRows_sence = new Dictionary<string, DataRow>();

    List<string> symbolList = new List<string>();
    List<string> finishedSymbolList = new List<string>();
    List<Dictionary<string, string>> honorResult = new List<Dictionary<string, string>>();
    Dictionary<string, List<string>> typedSymbols = new Dictionary<string, List<string>>();
    XmlNode rootNode;
    int count_primerSence = 0;
    int count_primarySence = 0;
    int count_middleSence = 0;
    int count_seniorSence = 0;
    int count_advanceSence = 0;
    int count_primerSence_Complete = 0;
    int count_primarySence_Complete = 0;
    int count_middleSence_Complete = 0;
    int count_seniorSence_Complete = 0;
    int count_advanceSence_Complete = 0;
    string strPrimerKey = "primer";
    string strPrimaryKey = "primary";
    string strMiddlekey = "middle";
    string strSeniorKey = "senior";
    string strAdvancedKey = "advanced";

    protected void init_sourceDoc_Sence()
    {        
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        foreach (DataRow activeRow in textDataTable.Rows)
        {
            string symbol = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeRow, "symbol", out symbol);
            symbolList.Add(symbol);
            sourceRows_sence.Add(symbol, activeRow);
        }
    }

    protected void init_sourceDoc_Profile()
    {
        string profileSymbol = "profile_" + "ikcoder_" + logined_user_name;
        string requestAPI = "/Profile/api_AccountProfile_SelectProfileBySymbol.aspx?symbol=" + profileSymbol;
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
        XmlDocument tmpData = new XmlDocument();
        tmpData.LoadXml(returnStrDoc);
        if (tmpData.SelectSingleNode("/root/err") == null)
        {
            XmlNode msgNode = tmpData.SelectSingleNode("/root/msg");
            sourceDoc_profile.LoadXml(class_XmlHelper.GetAttrValue(msgNode, "msg"));
        }
    }

    protected void init_typedSymbols()
    {
        List<string> primerList = new List<string>();
        List<string> primaryList = new List<string>();
        List<string> middleList = new List<string>();
        List<string> seniorList = new List<string>();
        List<string> advancedList = new List<string>();
        typedSymbols.Add(strPrimerKey, primerList);
        typedSymbols.Add(strPrimaryKey, primaryList);
        typedSymbols.Add(strMiddlekey, middleList);
        typedSymbols.Add(strSeniorKey, seniorList);
        typedSymbols.Add(strAdvancedKey, advancedList);

        foreach(string strSymbol in symbolList)
        {
            if (strSymbol.StartsWith("a") || strSymbol.StartsWith("A"))
                primerList.Add(strSymbol);
            else if (strSymbol.StartsWith("b") || strSymbol.StartsWith("B"))
                primaryList.Add(strSymbol);
            else if (strSymbol.StartsWith("c") || strSymbol.StartsWith("C"))
                middleList.Add(strSymbol);
            else if (strSymbol.StartsWith("d") || strSymbol.StartsWith("D"))
                seniorList.Add(strSymbol);
            else
                advancedList.Add(strSymbol);
        }                    
    }

    protected void init_finishedSymbols()
    {
        XmlNodeList finishItems = sourceDoc_profile.SelectNodes("/root/studystatus/finished/item");
        foreach (XmlNode item in finishItems)
        {
            string symbol = string.Empty;
            XmlNode symbolNode = item.SelectSingleNode("symbol");
            symbol = class_XmlHelper.GetNodeValue(symbolNode);
            finishedSymbolList.Add(symbol);
        }
    }

    protected void init_Honor()
    {
        class_Bus_Hornor objectHornor = new class_Bus_Hornor();
        objectHornor.init_SourceDoc(Object_CommonData);
        honorResult = objectHornor.get_HornorConfig(symbolList);
    }   

    protected void init_Course()
    {     
        foreach (string activeSymbol in symbolList)
        {
            if (activeSymbol.StartsWith("a") || activeSymbol.StartsWith("A"))
                count_primerSence++;
            else if (activeSymbol.StartsWith("b") || activeSymbol.StartsWith("B"))
                count_primarySence++;
            else if (activeSymbol.StartsWith("c") || activeSymbol.StartsWith("C"))
                count_middleSence++;
            else if (activeSymbol.StartsWith("d") || activeSymbol.StartsWith("D"))
                count_seniorSence++;
            else
                count_advanceSence++;
        }
        XmlNode finishedSenceNode = sourceDoc_profile.SelectSingleNode("/root/studystatus/finished");
        if (finishedSenceNode != null)
        {
            XmlNodeList itemNodes = sourceDoc_profile.SelectNodes("item");
            foreach(XmlNode item in itemNodes)
            {
                XmlNode symbolNode = item.SelectSingleNode("symbol");
                if (symbolNode != null)
                {
                    string activeSymbol = class_XmlHelper.GetNodeValue(symbolNode);
                    if (activeSymbol.StartsWith("a") && activeSymbol.StartsWith("A"))
                        count_primerSence_Complete++;
                    else if (activeSymbol.StartsWith("b") && activeSymbol.StartsWith("B"))
                        count_primarySence_Complete++;
                    else if (activeSymbol.StartsWith("c") && activeSymbol.StartsWith("C"))
                        count_middleSence_Complete++;
                    else if (activeSymbol.StartsWith("d") && activeSymbol.StartsWith("D"))
                        count_seniorSence_Complete++;
                    else
                        count_advanceSence_Complete++;
                }
            }
        }
    }
        
    protected void set_Course()
    {
        XmlNode courseNode = class_XmlHelper.CreateNode(centerDocument, "course", "");
        rootNode.AppendChild(courseNode);
        XmlNode itemNode = null;
        for (int index = 1; index <= 5; index++)
        {
            string key = string.Empty;
            switch(index)
            {
                case 1:
                    key = strPrimerKey;
                    break;
                case 2:
                    key = strPrimaryKey;
                    break;
                case 3:
                    key = strMiddlekey;
                    break;
                case 4:
                    key = strSeniorKey;
                    break;
                case 5:
                    key = strAdvancedKey;
                    break;
            }
            itemNode = class_XmlHelper.CreateNode(centerDocument, "item", "");
            courseNode.AppendChild(itemNode);
            class_XmlHelper.SetAttribute(itemNode, "id", key);
            class_XmlHelper.SetAttribute(itemNode, "title", "跟着博士学Scratch编程(启蒙)");
            class_XmlHelper.SetAttribute(itemNode, "total", count_primerSence.ToString());
            class_XmlHelper.SetAttribute(itemNode, "complete", count_primerSence_Complete.ToString());
            XmlNode symbolLstNode = class_XmlHelper.CreateNode(centerDocument, "symbollst", "");
            itemNode.AppendChild(symbolLstNode);
            foreach (string activeSymbol in typedSymbols[key])
            {
                XmlNode lessonNode = class_XmlHelper.CreateNode(centerDocument, "lesson", "");
                symbolLstNode.AppendChild(lessonNode);
                class_XmlHelper.SetAttribute(lessonNode, "symbol", activeSymbol);
                class_XmlHelper.SetAttribute(lessonNode, "title", class_Bus_SenceDoc.GetSenceValue(Object_CommonData, activeSymbol, "/sence", "name"));
            }
        }

    }

    protected void set_Honor()
    {
        XmlNode honorNode = class_XmlHelper.CreateNode(centerDocument, "honor", "");
        rootNode.AppendChild(honorNode);
        foreach (Dictionary<string, string> activeItem in honorResult)
        {
            XmlNode newItem = class_XmlHelper.CreateNode(centerDocument, "item", "");
            honorNode.AppendChild(newItem);
            foreach (string keyName in activeItem.Keys)
                class_XmlHelper.SetAttribute(newItem, keyName, activeItem[keyName]);
        }
    }

    protected void set_Distributio()
    {
        XmlNode distributioNode = class_XmlHelper.CreateNode(centerDocument, "distributio", "");
        rootNode.AppendChild(distributioNode);
        class_Bus_Dis disObject = new class_Bus_Dis();
        disObject.init_SourceDoc(Object_CommonData);
        disObject.set_CreateDisItems(ref distributioNode, sourceRows_sence, finishedSymbolList);
    }

    protected void set_Level()
    {
        double total_exvalue_primer = 0;
        double total_exvalue_primary = 0;
        double total_exvalue_middle = 0;
        double total_exvalue_senior = 0;
        double total_exvalue_advanced = 0;
        double u_exvalue_primer = 0;
        double u_exvalue_primary = 0;
        double u_exvalue_middle = 0;
        double u_exvalue_senior = 0;
        double u_exvalue_advanced = 0;
        foreach (string typeKey in typedSymbols.Keys)
        {
            List<string> tmpList = typedSymbols[typeKey];
            foreach (string activeTypedSymbol in tmpList)
            {
                if (sourceRows_sence.ContainsKey(activeTypedSymbol))
                {
                    DataRow activeDataRow = sourceRows_sence[activeTypedSymbol];
                    string configDoc = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "config", out configDoc);
                    XmlNode scoreNode = sourceDoc_sence.SelectSingleNode("/root/score");
                    string strBasicScore = class_XmlHelper.GetAttrValue(scoreNode, "score");
                    string strDiffScore = class_XmlHelper.GetAttrValue(scoreNode, "diff");
                    double iBasicScore = 0;
                    double.TryParse(strBasicScore, out iBasicScore);
                    double iDiffScore = 0;
                    double.TryParse(strDiffScore, out iDiffScore);
                    if (typeKey.Contains("primer"))
                        total_exvalue_primer = total_exvalue_primer + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("primary"))
                        total_exvalue_primary = total_exvalue_primary + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("middle"))
                        total_exvalue_middle = total_exvalue_middle + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("senior"))
                        total_exvalue_senior = total_exvalue_senior + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("advanced"))
                        total_exvalue_advanced = total_exvalue_advanced + (iBasicScore * (1 + iDiffScore));
                }
            }
        }
        foreach (string activeFinishSymbol in finishedSymbolList)
        {
            if (sourceRows_sence.ContainsKey(activeFinishSymbol))
            {
                DataRow activeDataRow = sourceRows_sence[activeFinishSymbol];
                string configDoc = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configDoc);
                XmlDocument tmpConfigDoc = new XmlDocument();
                tmpConfigDoc.LoadXml(class_CommonUtil.Decoder_Base64(configDoc));
                XmlNode scoreNode = tmpConfigDoc.SelectSingleNode("/root/score");
                string strBasicScore = class_XmlHelper.GetAttrValue(scoreNode, "score");
                string strDiffScore = class_XmlHelper.GetAttrValue(scoreNode, "diff");
                double iBasicScore = 0;
                double.TryParse(strBasicScore, out iBasicScore);
                double iDiffScore = 0;
                double.TryParse(strDiffScore, out iDiffScore);
                if (activeFinishSymbol.StartsWith("a")|| activeFinishSymbol.StartsWith("A"))
                    u_exvalue_primer = u_exvalue_primer + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("b") || activeFinishSymbol.StartsWith("B"))
                    u_exvalue_primary = u_exvalue_primary + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("c") || activeFinishSymbol.StartsWith("C"))
                    u_exvalue_middle = u_exvalue_middle + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("d") || activeFinishSymbol.StartsWith("D"))
                    u_exvalue_senior = u_exvalue_senior + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("e") || activeFinishSymbol.StartsWith("E"))
                    u_exvalue_advanced = u_exvalue_advanced + (iBasicScore * (1 + iDiffScore));
            }
        }
        XmlNode levelNode = class_XmlHelper.CreateNode(centerDocument, "level", "");
        rootNode.AppendChild(levelNode);
        foreach (string typeKey in typedSymbols.Keys)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(centerDocument, "item", "");
            levelNode.AppendChild(itemNode);
            class_XmlHelper.SetAttribute(itemNode, "id", typeKey);
            if (typeKey == strPrimerKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Primer"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_primer > 0 ? (u_exvalue_primer / total_exvalue_primer).ToString() : "0");
            }
            else if(typeKey==strPrimaryKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Primary"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_primary > 0 ? (u_exvalue_primary / total_exvalue_primary).ToString() : "0");
            }
            else if (typeKey == strMiddlekey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Middle"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_middle > 0 ? (u_exvalue_middle / total_exvalue_middle).ToString() : "0");
            }
            else if (typeKey == strSeniorKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Senior"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_senior > 0 ? (u_exvalue_senior / total_exvalue_senior).ToString() : "0");
            }
            else if (typeKey == strAdvancedKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Advanced"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_advanced > 0 ? (u_exvalue_advanced / total_exvalue_advanced).ToString() : "0");
            }
        }
        
        
    }

    protected void set_Codetimes()
    {
        XmlNode codetimesNode = class_XmlHelper.CreateNode(centerDocument, "codetimes", "");
        rootNode.AppendChild(codetimesNode);
        class_XmlHelper.SetAttribute(codetimesNode, "over", "95");
        List<XmlNode> codetimelineItemNodes = class_Bus_ProfileDoc.GetCodetimeLineItems(sourceDoc_profile);
        foreach(XmlNode activeItem in codetimelineItemNodes)
        {
            XmlNode item = class_XmlHelper.CreateNode(centerDocument, "item", "");
            string strDate = class_XmlHelper.GetAttrValue(activeItem, "date");
            string strValue = class_XmlHelper.GetAttrValue(activeItem, "value");
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";
            class_XmlHelper.SetAttribute(item, "date", strDate);
            double dHours = 0;
            double dMintues = 0;
            double.TryParse(strValue, out dMintues);
            dHours = dMintues / 60;
            class_XmlHelper.SetAttribute(item, "value", dHours.ToString(".##"));
            codetimesNode.AppendChild(item);
        }
    }

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        init_sourceDoc_Sence();
        init_sourceDoc_Profile();
        init_finishedSymbols();
        init_typedSymbols();
        init_Honor();
        init_Course();
        
        centerDocument.LoadXml("<root></root>");
        rootNode = centerDocument.SelectSingleNode("/root");
        set_Honor();
        set_Course();
        set_Distributio();
        set_Level();
        set_Codetimes();
        RESPONSEDOCUMENT.LoadXml(centerDocument.OuterXml);
    }
}

