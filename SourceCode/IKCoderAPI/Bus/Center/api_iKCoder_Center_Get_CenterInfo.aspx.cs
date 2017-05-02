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
            XmlNodeList newItemNodes = sourceDoc_profile.SelectNodes("item");
            
        }
    }

    protected void set_Course()
    {
        XmlNode courseNode = class_XmlHelper.CreateNode(centerDocument, "course", "");
        rootNode.AppendChild(courseNode);
       
    }
  
    protected override void ExtendedAction()
    {
        Object_CommonData.PrepareDataOperation();
        init_sourceDoc_Sence();
        init_sourceDoc_Profile();
        init_Honor();
        init_Course();
        
        centerDocument.LoadXml("<root></root>");
        rootNode = centerDocument.SelectSingleNode("/root");
        set_Honor();
    }
}

