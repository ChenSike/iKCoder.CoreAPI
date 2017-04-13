using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_GET_Workspace : class_WebBase_UA
{
    private XmlDocument sourceDoc_configsItem;
    private Dictionary<string, string> sourceDic_accountProfile;
        
    private XmlDocument workspaceDoc;
    private string symbol;
    private string user_name;

    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        symbol = GetQuerystringParam("symbol");        
        if(!string.IsNullOrEmpty(symbol))
        {
            user_name = Session["logined_user_name"].ToString();
            string configsItemSymbol = "workspace_configsitem_common";
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?symbol=" + configsItemSymbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            string decoderDoc = class_CommonUtil.Decoder_Base64(returnDoc);
            sourceDoc_configsItem = new XmlDocument();
            sourceDoc_configsItem.LoadXml(returnDoc);
            
            StringBuilder requestInput = new StringBuilder();
            requestInput.Append("<root>");
            requestInput.Append("<account>");
            requestInput.Append(user_name);
            requestInput.Append("</account>");
            requestInput.Append("<select>");
            requestInput.Append("<item>");
            requestInput.Append("/root/studystatus/currentsence/currentstage");
            requestInput.Append("/root/studystatus/currentsence/symbol");
            requestInput.Append("</item>");
            requestInput.Append("</select>");
            string requestAPI = "/Profile/api_AccountProfile_SelectNodesValues.aspx?cid=" + cid;
            URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(requestInput.ToString(), URL, 1000 * 60, 100000);
            if (!returnStrDoc.Contains("err"))
            {
                XmlDocument tmpData = new XmlDocument();
                XmlNodeList msgNodes = tmpData.SelectNodes("/root/msg");
                foreach (XmlNode msgNode in msgNodes)
                {
                    string value = class_XmlHelper.GetAttrValue(msgNode, "value");
                    string xpath = class_XmlHelper.GetAttrValue(msgNode, "xpath");
                    sourceDic_accountProfile.Add(xpath, value);
                }
            }
            
            workspaceDoc = new XmlDocument();
            workspaceDoc.LoadXml("<root></root>");

        }
        //else
    }

    protected void BuilldNode_basic(XmlNode rootNode)
    {
        //build basic node
        XmlNode basicNode = class_XmlHelper.CreateNode(workspaceDoc, "basic", "");
        rootNode.AppendChild(basicNode);
        XmlNode usrNode = class_XmlHelper.CreateNode(workspaceDoc, "usr", "");
        basicNode.AppendChild(usrNode);
        string user_id = Session["logined_user_id"].ToString();
        string user_nickname = Session["logined_user_nickname"].ToString();
        string user_header = "/Data/GET_ImageHeader.aspx?cid=" + ClientSymbol + "&symbol=" + symbol;
        class_XmlHelper.SetAttribute(usrNode, "id", user_id);
        class_XmlHelper.SetAttribute(usrNode, "nickname", user_nickname);
        class_XmlHelper.SetAttribute(usrNode, "header", user_header);
        //build sence node
        XmlNode senceNode = class_XmlHelper.CreateNode(workspaceDoc, "sence", "");
        rootNode.AppendChild(senceNode);
        string stageSymbol = string.Empty;
        if (sourceDic_accountProfile.ContainsKey("/root/studystatus/currentsence/symbol"))
            stageSymbol = sourceDic_accountProfile["/root/studystatus/currentsence/symbol"];
        XmlNode source_senceNode = workspaceDoc.SelectSingleNode("/root/sence[@symbol='" + stageSymbol + "']");
        int stageCount = workspaceDoc.SelectNodes("/root/stages/stage").Count;
        string stageName = class_XmlHelper.GetAttrValue(source_senceNode, "name");        
        string id = class_XmlHelper.GetAttrValue(source_senceNode, "id");
        string currentstage = string.Empty;        
        if(sourceDic_accountProfile.ContainsKey("/root/studystatus/currentsence/currentstage"))
            currentstage = sourceDic_accountProfile["/root/studystatus/currentsence/currentstage"];
        class_XmlHelper.SetAttribute(senceNode, "name", stageName);
        class_XmlHelper.SetAttribute(senceNode, "symbol", stageSymbol);
        class_XmlHelper.SetAttribute(senceNode, "id", id);
        class_XmlHelper.SetAttribute(senceNode, "totalstage", stageCount.ToString());
        class_XmlHelper.SetAttribute(senceNode, "currentstage", currentstage);
    }

    protected void BuildNode_sence(XmlNode rootNode)
    {
       

        
    }

}