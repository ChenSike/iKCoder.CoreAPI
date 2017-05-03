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

    List<string> symbolList = new List<string>();
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

    protected void init_Honor()
    {
        class_Bus_Hornor objectHornor = new class_Bus_Hornor();
        honorResult = objectHornor.get_HornorConfig(symbolList);
    }

    protected void set_Honor()
    {
        XmlNode honorNode = class_XmlHelper.CreateNode(centerDocument, "honor", "");
        rootNode.AppendChild(honorNode);
        foreach(Dictionary<string,string> activeItem in honorResult)
        {
            XmlNode newItem = class_XmlHelper.CreateNode(centerDocument, "item", "");
            honorNode.AppendChild(newItem);
            foreach (string keyName in activeItem.Keys)
                class_XmlHelper.SetAttribute(newItem, keyName, activeItem[keyName]);
        }
    }

    protected void init_Course()
    {     
        foreach (string activeSymbol in symbolList)
        {
            if (activeSymbol.StartsWith("a") && activeSymbol.StartsWith("A"))
                count_primerSence++;
            else if (activeSymbol.StartsWith("b") && activeSymbol.StartsWith("B"))
                count_primarySence++;
            else if (activeSymbol.StartsWith("c") && activeSymbol.StartsWith("C"))
                count_middleSence++;
            else if (activeSymbol.StartsWith("d") && activeSymbol.StartsWith("D"))
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
            class_XmlHelper.SetAttribute(itemNode, "id", "primer");
            class_XmlHelper.SetAttribute(itemNode, "title", "跟着博士学Scratch编程(启蒙)");
            class_XmlHelper.SetAttribute(itemNode, "total", count_primerSence.ToString());
            class_XmlHelper.SetAttribute(itemNode, "complete", count_primerSence_Complete.ToString());
            XmlNode symbolLstNode = class_XmlHelper.CreateNode(centerDocument, "symbollst", "");
            itemNode.AppendChild(symbolLstNode);
            foreach (string activeSymbol in typedSymbols[key])
            {
                XmlNode symbolItemNode = class_XmlHelper.CreateNode(centerDocument, "item", "");
                symbolLstNode.AppendChild(symbolLstNode);
                class_XmlHelper.SetAttribute(symbolLstNode, "symbol", activeSymbol);
                class_XmlHelper.SetAttribute(symbolLstNode, "title", class_Bus_SenceDoc.GetSenceValue(Object_CommonData, activeSymbol, "/sence", "name"));
            }
        }

    }
  
    protected override void ExtendedAction()
    {
        Object_CommonData.PrepareDataOperation();
        init_sourceDoc_Sence();
        init_sourceDoc_Profile();
        init_typedSymbols();
        init_Honor();
        init_Course();
        
        centerDocument.LoadXml("<root></root>");
        rootNode = centerDocument.SelectSingleNode("/root");
        set_Honor();
        set_Course();
    }
}

