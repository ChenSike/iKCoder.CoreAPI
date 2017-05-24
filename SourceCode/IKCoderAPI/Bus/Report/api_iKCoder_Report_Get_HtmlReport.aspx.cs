using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Report_api_iKCoder_Workspace_Get_HtmlReport : class_WebBase_IKCoderAPI_UA
{
    protected XmlDocument sourceDoc_sence = new XmlDocument();
    
    XmlDocument reportDoc = new XmlDocument();

    protected XmlNode node_report_root;
        
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        reportDoc.LoadXml("<root></root>");
        node_report_root = reportDoc.SelectSingleNode("/root");
        Set_Report();
        Set_Overview();
        Set_Honor();
        set_Codetimes();
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
        List<string> finishSymbol = Object_ProfileDocs.GetFinishedSymbols(logined_user_name);
        class_XmlHelper.SetAttribute(overviewNode, "finish", finishSymbol.Count.ToString());
        class_Bus_Exp objectExp = new class_Bus_Exp(Object_CommonData, finishSymbol);
        double loginedUserTotalExp = 0;
        double.TryParse(Object_ProfileDocs.GetTotalExp(logined_user_name), out loginedUserTotalExp);
        class_XmlHelper.SetAttribute(overviewNode, "exprate", ((int)(loginedUserTotalExp / objectExp.Get_TotalExp())).ToString());
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
        class_XmlHelper.SetAttribute(overviewNode, "overrate", ((int)(position / usersInProfilePool.Count)).ToString());
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

    protected void set_Codetimes()
    {
        XmlNode codetimesNode = class_XmlHelper.CreateNode(reportDoc, "codetimes", "");
        node_report_root.AppendChild(codetimesNode);
        XmlDocument sourceDoc_CodeLine = Object_ProfileDocs.GetProfileDocObject(logined_user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
        List<XmlNode> codetimelineItemNodes = Object_ProfileDocs.GetCodetimeLineItems(logined_user_name, sourceDoc_CodeLine);
        foreach (XmlNode activeItem in codetimelineItemNodes)
        {
            XmlNode item = class_XmlHelper.CreateNode(reportDoc, "item", "");
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

}