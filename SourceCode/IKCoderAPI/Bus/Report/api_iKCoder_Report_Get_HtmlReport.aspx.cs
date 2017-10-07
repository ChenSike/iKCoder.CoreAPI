using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Report_api_iKCoder_Workspace_Get_HtmlReport : class_WebBase_IKCoderAPI_UA
{
    protected XmlDocument sourceDoc_sence = new XmlDocument();
    Dictionary<string, DataRow> sourceRows_sence = new Dictionary<string, DataRow>();
    List<string> finishSymbol = new List<string>();

    XmlDocument reportDoc = new XmlDocument();

    protected XmlNode node_report_root;
    Dictionary<string, List<string>> typedSymbols = new Dictionary<string, List<string>>();
    List<string> symbolList = new List<string>();
    class_Bus_ProfileDocs Object_ProfileDocs;
    string strPrimerKey = "primer";
    string strPrimaryKey = "primary";
    string strMiddlekey = "middle";
    string strSeniorKey = "senior";
    string strAdvancedKey = "advanced";

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        Object_ProfileDocs = new class_Bus_ProfileDocs(ref Object_CommonData);
        reportDoc.LoadXml("<root></root>");
        node_report_root = reportDoc.SelectSingleNode("/root");
        init_sourceDoc_Sence();
        init_typedSymbols();
        Set_Report();
        Set_Overview();
        Set_Honor();
        Set_Codetimes();
        Set_Ability();
        Set_Level();
        RESPONSEDOCUMENT.LoadXml(reportDoc.OuterXml);
    }
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

        foreach (string strSymbol in symbolList)
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

    protected void Set_Report()
    {
        XmlNode reportNode = class_XmlHelper.CreateNode(reportDoc, "report", "");
        class_XmlHelper.SetAttribute(reportNode, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        node_report_root.AppendChild(reportNode);
    }

    protected void Set_Overview()
    {
        XmlNode overviewNode = class_XmlHelper.CreateNode(reportDoc, "overview", "");        
        node_report_root.AppendChild(overviewNode);
        class_XmlHelper.SetAttribute(overviewNode, "date", DateTime.Now.ToString("yyyy-MM-dd"));
        XmlDocument sourceDoc_studystatus = Object_ProfileDocs.GetProfileDocObject(logined_user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
        finishSymbol = Object_ProfileDocs.GetFinishedSymbols(logined_user_name);
        class_XmlHelper.SetAttribute(overviewNode, "finish", finishSymbol.Count.ToString());
        class_Bus_Exp objectExp = new class_Bus_Exp(Object_CommonData, finishSymbol,Object_ProfileDocs,logined_user_name);
        double loginedUserTotalExp = 0;
        double.TryParse(Object_ProfileDocs.GetTotalExp(logined_user_name), out loginedUserTotalExp);
        class_XmlHelper.SetAttribute(overviewNode, "exprate", ((int)((loginedUserTotalExp / objectExp.Get_TotalExp())*100)).ToString());
        List<string> usersInProfilePool = Object_ProfileDocs.GetUsersInProfilePool();
        List<double> usersExpLst = new List<double>();
        foreach (string activeUser in usersInProfilePool)
        {
            double tmpValue = 0;
            double.TryParse(Object_ProfileDocs.GetTotalExp(activeUser), out tmpValue);
            usersExpLst.Add(tmpValue);
        }
        usersExpLst.Sort();
        int position = 1;
        foreach(double activeExp in usersExpLst)
        {
            if (loginedUserTotalExp > activeExp)
                position++;
            else
                break;
        }
        class_XmlHelper.SetAttribute(overviewNode, "overrate", ((int)((position / usersInProfilePool.Count) * 100) == 100 ? 99 : (int)((position / usersInProfilePool.Count) * 100)).ToString());
        class_XmlHelper.SetAttribute(overviewNode, "usr_nickname", Session["logined_user_nickname"].ToString());
        class_Bus_Title objectTitle = new class_Bus_Title(Object_CommonData);
        class_XmlHelper.SetAttribute(overviewNode, "usr_title", objectTitle.GetTitle(finishSymbol));

    }

    public void Set_Honor()
    {
        class_Bus_Hornor objectHornor = new class_Bus_Hornor();
        objectHornor.init_SourceDoc(Object_CommonData);
        List<string> finishedSymbolList = Object_ProfileDocs.GetFinishedSymbols(logined_user_name);
        List<Dictionary<string, string>> honorResult = objectHornor.get_HornorConfig(finishedSymbolList);
        XmlNode honorNode = class_XmlHelper.CreateNode(reportDoc, "honor", "");
        node_report_root.AppendChild(honorNode);
        foreach (Dictionary<string, string> activeItem in honorResult)
        {
            XmlNode newItem = class_XmlHelper.CreateNode(reportDoc, "item", "");
            honorNode.AppendChild(newItem);
            foreach (string keyName in activeItem.Keys)
                class_XmlHelper.SetAttribute(newItem, keyName, activeItem[keyName]);
        }
    }

    protected void Set_Level()
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
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configDoc);
                    XmlDocument activeSourceDoc_sence = new XmlDocument();
                    activeSourceDoc_sence.LoadXml(class_CommonUtil.Decoder_Base64(configDoc));
                    XmlNode scoreNode = activeSourceDoc_sence.SelectSingleNode("/sence/score");
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
        foreach (string activeFinishSymbol in finishSymbol)
        {
            if (sourceRows_sence.ContainsKey(activeFinishSymbol))
            {
                DataRow activeDataRow = sourceRows_sence[activeFinishSymbol];
                string configDoc = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configDoc);
                XmlDocument tmpConfigDoc = new XmlDocument();
                tmpConfigDoc.LoadXml(class_CommonUtil.Decoder_Base64(configDoc));
                XmlNode scoreNode = tmpConfigDoc.SelectSingleNode("/sence/score");
                string strBasicScore = class_XmlHelper.GetAttrValue(scoreNode, "score");
                string strDiffScore = class_XmlHelper.GetAttrValue(scoreNode, "diff");
                double iBasicScore = 0;
                double.TryParse(strBasicScore, out iBasicScore);
                double iDiffScore = 0;
                double.TryParse(strDiffScore, out iDiffScore);
                if (activeFinishSymbol.StartsWith("a") || activeFinishSymbol.StartsWith("A"))
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
        XmlNode levelNode = class_XmlHelper.CreateNode(reportDoc, "level", "");
        class_XmlHelper.SetAttribute(levelNode, "total", (u_exvalue_primer + u_exvalue_primary + u_exvalue_middle + u_exvalue_senior + u_exvalue_advanced).ToString());
        node_report_root.AppendChild(levelNode);
        foreach (string typeKey in typedSymbols.Keys)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(reportDoc, "item", "");
            levelNode.AppendChild(itemNode);
            class_XmlHelper.SetAttribute(itemNode, "id", typeKey);
            if (typeKey == strPrimerKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Primer"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_primer > 0 ? ((u_exvalue_primer / total_exvalue_primer) * 100).ToString(".") : "0");
            }
            else if (typeKey == strPrimaryKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Primary"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_primary > 0 ? ((u_exvalue_primary / total_exvalue_primary) * 100).ToString(".") : "0");
            }
            else if (typeKey == strMiddlekey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Middle"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_middle > 0 ? ((u_exvalue_middle / total_exvalue_middle) * 100).ToString(".") : "0");
            }
            else if (typeKey == strSeniorKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Senior"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_senior > 0 ? ((u_exvalue_senior / total_exvalue_senior) * 100).ToString() : "0");
            }
            else if (typeKey == strAdvancedKey)
            {
                class_XmlHelper.SetAttribute(itemNode, "name", Object_LabelController.GetString("labels", "Center_Level_Advanced"));
                class_XmlHelper.SetAttribute(itemNode, "value", total_exvalue_advanced > 0 ? ((u_exvalue_advanced / total_exvalue_advanced) * 100).ToString() : "0");
            }
        }
    }


    protected void Set_Ability()
    {
        XmlNode node_ablity = class_XmlHelper.CreateNode(reportDoc, "ability", "");
        node_report_root.AppendChild(node_ablity);
        class_XmlHelper.SetAttribute(node_ablity, "des",Object_LabelController.GetString("lables", "report_ability_des"));
        XmlNode node_finishedcourse = class_XmlHelper.CreateNode(reportDoc, "finishedcourse", "");
        node_ablity.AppendChild(node_finishedcourse);
        foreach (string activeSymbol in finishSymbol)
        {
            XmlNode node_item = class_XmlHelper.CreateNode(reportDoc, "item", "");
            node_finishedcourse.AppendChild(node_item);
            class_XmlHelper.SetAttribute(node_item, "symbol", activeSymbol);
            class_XmlHelper.SetAttribute(node_item, "title", class_Bus_SenceDoc.GetSenceValue(Object_CommonData, activeSymbol, "/sence", "name"));
            class_XmlHelper.SetAttribute(node_item, "unit", class_Bus_SenceDoc.GetSenceValue(Object_CommonData, activeSymbol, "/sence", "unit"));
        }
        XmlNode node_stmel = class_XmlHelper.CreateNode(reportDoc, "steml", "");
        node_ablity.AppendChild(node_stmel);
        class_XmlHelper.SetAttribute(node_stmel, "s", "0");
        class_XmlHelper.SetAttribute(node_stmel, "t", "0");
        class_XmlHelper.SetAttribute(node_stmel, "e", "0");
        class_XmlHelper.SetAttribute(node_stmel, "m", "0");
        class_XmlHelper.SetAttribute(node_stmel, "l", "0");
    }

    protected void Set_Codetimes()
    {
        XmlNode codetimesNode = class_XmlHelper.CreateNode(reportDoc, "codetimes", "");
        node_report_root.AppendChild(codetimesNode);
        XmlDocument sourceDoc_CodeLine = Object_ProfileDocs.GetProfileDocObject(logined_user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
        List<XmlNode> codetimelineItemNodes = Object_ProfileDocs.GetCodetimeLineItems(logined_user_name, sourceDoc_CodeLine);
        int userTotalTime = Object_ProfileDocs.GetTotalTime(logined_user_name);
        class_XmlHelper.SetAttribute(codetimesNode, "totaltime", userTotalTime.ToString("0.00"));
        foreach (XmlNode activeItem in codetimelineItemNodes)
        {
            XmlNode item = class_XmlHelper.CreateNode(reportDoc, "item", "");
            string strDate = class_XmlHelper.GetAttrValue(activeItem, "date");
            string strValue = class_XmlHelper.GetAttrValue(activeItem, "value");
            if (!string.IsNullOrEmpty(strDate))
            {
                DateTime dtTmp = DateTime.Now;
                DateTime.TryParse(strDate, out dtTmp);
                if (dtTmp.Month == DateTime.Now.Month)
                {
                    if (string.IsNullOrEmpty(strValue))
                        strValue = "0";
                    class_XmlHelper.SetAttribute(item, "date", strDate);
                    double dHours = 0;
                    double dMintues = 0;
                    double.TryParse(strValue, out dMintues);
                    dHours = dMintues / 60;
                    class_XmlHelper.SetAttribute(item, "value", dHours.ToString("0.00"));
                    codetimesNode.AppendChild(item);
                }
            }
        }
    }

}