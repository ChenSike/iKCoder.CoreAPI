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
    protected XmlDocument sourceDoc_profile = new XmlDocument();
    protected XmlDocument sourceDoc_sence = new XmlDocument();
    
    XmlDocument reportDoc = new XmlDocument();

    protected XmlNode node_report_root;

    protected bool Init_ProfileDoc()
    {
        sourceDoc_profile = class_Bus_ProfileDoc.GetProfileDocument(Server_API, Virtul_Folder_API, Object_NetRemote, logined_user_name);
        if (sourceDoc_profile == null && string.IsNullOrEmpty(sourceDoc_profile.OuterXml))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "profiledoc->empty", "");
            return false;
        }
        else
            return true;
    }

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        reportDoc.LoadXml("<root></root>");
        node_report_root = reportDoc.SelectSingleNode("/root");
        Init_ProfileDoc();
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

    }
}