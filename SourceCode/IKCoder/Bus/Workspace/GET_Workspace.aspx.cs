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
    private XmlDocument sourceDoc_configsItem = new XmlDocument();
    private XmlDocument sourceDoc_configsTips = new XmlDocument();
    private XmlDocument sourceDoc_workspaceStatus = new XmlDocument();
    private XmlDocument sourceDoc_toolBox = new XmlDocument();
    private XmlDocument sourceDoc_wordList = new XmlDocument();
    private Dictionary<string, string> sourceDic_accountProfile = new Dictionary<string, string>();

    private class_WorkspaceProcess objectWorkspaceProcess;
    private XmlDocument workspaceDoc;
    private XmlNode senceNode;
    private XmlNode stageNode;
    private string symbol;
    private string user_name;
    private string symbol_tips = "";
    private string symbol_toolbox = "";
    private string symbol_toolbox_src = "";
    private string symbol_step = "";
    private string symbol_workspacestatus = "";
    private string symbol_message_err = "";
    private string symbol_message_suc = "";


    protected override void ExtendedAction()
    {
                
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            user_name = Session["logined_user_name"].ToString();
            objectWorkspaceProcess = new class_WorkspaceProcess(Server_API + Virtul_Folder_API, ClientSymbol, Object_NetRemote, user_name);

            StringBuilder requestInput = new StringBuilder();
            requestInput.Append("<root>");
            requestInput.Append("<account>");
            requestInput.Append(user_name);
            requestInput.Append("</account>");
            requestInput.Append("<select>");
            requestInput.Append("<items value='/root/studystatus/currentsence/currentstage'>");            
            requestInput.Append("</items>");
            requestInput.Append("<items value='/root/studystatus/currentsence/symbol'>");            
            requestInput.Append("</items>");
            requestInput.Append("</select>");
            requestInput.Append("</root>");
            string requestAPI = "/Profile/api_AccountProfile_SelectNodesValues.aspx?cid=" + cid;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(requestInput.ToString(), URL, 1000 * 60, 100000);
            if (!returnStrDoc.Contains("err"))
            {
                XmlDocument tmpData = new XmlDocument();
                tmpData.LoadXml(returnStrDoc);
                XmlNodeList msgNodes = tmpData.SelectNodes("/root/msg");
                foreach (XmlNode msgNode in msgNodes)
                {
                    string value = class_XmlHelper.GetAttrValue(msgNode, "value");
                    string xpath = class_XmlHelper.GetAttrValue(msgNode, "xpath");
                    sourceDic_accountProfile.Add(xpath, value);
                }
            }
            string currentstage = string.Empty;
            if (sourceDic_accountProfile.ContainsKey("/root/studystatus/currentsence/currentstage"))
                currentstage = sourceDic_accountProfile["/root/studystatus/currentsence/currentstage"];
            if (string.IsNullOrEmpty(currentstage))
            {
                currentstage = "1";
                requestAPI = "/Profile/api_AccountProfile_SetNodes.aspx?cid=" + cid;
                URL = Server_API + Virtul_Folder_API + requestAPI;
                StringBuilder strRequestDoc = new StringBuilder();
                strRequestDoc.Append("<root>");
                strRequestDoc.Append("<account>");
                strRequestDoc.Append(user_name);
                strRequestDoc.Append("</account>");
                strRequestDoc.Append("<produce>");
                strRequestDoc.Append(Produce_Name);
                strRequestDoc.Append("</produce>");
                strRequestDoc.Append("<parent>");
                strRequestDoc.Append("/root/studystatus/currentsence");
                strRequestDoc.Append("</parent>");
                strRequestDoc.Append("<newnodes>");
                strRequestDoc.Append("<item name=\"currentstage\" value=\"" + currentstage + "\" >");
                strRequestDoc.Append("</item>");
                strRequestDoc.Append("</newnodes>");
                strRequestDoc.Append("</root>");
                Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strRequestDoc.ToString(), URL, 1000 * 60, 100000);
            }
            symbol_step = currentstage;     
            string configsItemSymbol = "workspace_configsitem_common";
            URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + ClientSymbol + "&symbol=" + configsItemSymbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument tmpDoc = new XmlDocument();
            tmpDoc.LoadXml(returnDoc);
            string strMsg = class_XmlHelper.GetAttrValue(tmpDoc.SelectSingleNode("/root/msg"), "msg");
            string decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);
            sourceDoc_configsItem = new XmlDocument();
            sourceDoc_configsItem.LoadXml(decoderDoc);
            senceNode = sourceDoc_configsItem.SelectSingleNode("/root/sence[@symbol='"+symbol+"']");
            stageNode = senceNode.SelectSingleNode("stages/stage[@step='" + currentstage + "']");
            symbol_tips = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("tips"), "symbol");
            symbol_toolbox = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("toolbox"), "symbol");
            symbol_toolbox_src = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("toolbox"), "src");
            symbol_workspacestatus = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("workspacestatus"), "symbol");
            XmlNode messageNode = stageNode.SelectSingleNode("message");
            symbol_message_err = class_XmlHelper.GetAttrValue(messageNode.SelectSingleNode("err"), "code");
            symbol_message_suc = class_XmlHelper.GetAttrValue(messageNode.SelectSingleNode("suc"), "code");

            string tmp_sourceDoc_workspaceStatus;            
            tmp_sourceDoc_workspaceStatus = objectWorkspaceProcess.GET_Doc_WorkspaceStatus(symbol,currentstage,symbol_workspacestatus);
            if(tmp_sourceDoc_workspaceStatus!="")
                sourceDoc_workspaceStatus.LoadXml(tmp_sourceDoc_workspaceStatus);
            
            string configTipsSymbol = "config_tips_workspace";
            URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + ClientSymbol + "&symbol=" + configTipsSymbol;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            tmpDoc = new XmlDocument();
            tmpDoc.LoadXml(returnDoc);
            strMsg = class_XmlHelper.GetAttrValue(tmpDoc.SelectSingleNode("/root/msg"), "msg");
            decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);
            sourceDoc_configsTips = new XmlDocument();
            sourceDoc_configsTips.LoadXml(decoderDoc);

            string wordListSymbol = "workspace_word_list";
            URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + ClientSymbol + "&symbol=" + wordListSymbol;
            returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            tmpDoc = new XmlDocument();
            tmpDoc.LoadXml(returnDoc);
            strMsg = class_XmlHelper.GetAttrValue(tmpDoc.SelectSingleNode("/root/msg"), "msg");
            decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);            
            sourceDoc_wordList.LoadXml(decoderDoc);

            workspaceDoc = new XmlDocument();
            workspaceDoc.LoadXml("<root></root>");
            XmlNode rootNode = workspaceDoc.SelectSingleNode("/root");
            BuildNode_Basic(rootNode);
            BuildNode_WorkspaceStatus(rootNode);
            BuildNode_Toolbox(rootNode);
            BuildNode_Game(rootNode);
            BuildNode_Word(rootNode);
            BuildNode_Message(rootNode);
            RESPONSEDOCUMENT.LoadXml(workspaceDoc.OuterXml);

        }
        //else
    }

    protected void BuildNode_Basic(XmlNode rootNode)
    {
        //build basic node
        XmlNode basicNode = class_XmlHelper.CreateNode(workspaceDoc, "basic", "");
        rootNode.AppendChild(basicNode);
        XmlNode usrNode = class_XmlHelper.CreateNode(workspaceDoc, "usr", "");
        basicNode.AppendChild(usrNode);
        string user_id = Session["logined_user_id"].ToString();
        string user_nickname = Session["logined_user_nickname"].ToString();
        string user_header = "/Data/GET_ImageHeader.aspx?cid=" + ClientSymbol ;
        class_XmlHelper.SetAttribute(usrNode, "id", user_id);
        class_XmlHelper.SetAttribute(usrNode, "nickname", user_nickname);
        class_XmlHelper.SetAttribute(usrNode, "header", user_header);
        //build sence node
        XmlNode senceNode = class_XmlHelper.CreateNode(workspaceDoc, "sence", "");
        rootNode.AppendChild(senceNode);  
        XmlNode source_senceNode =  sourceDoc_configsItem.SelectSingleNode("/root/sence[@symbol='" + symbol + "']");
        int stageCount = (source_senceNode.SelectNodes("stages/stage")).Count;
        string stageName = class_XmlHelper.GetAttrValue(source_senceNode, "name");        
        string id = class_XmlHelper.GetAttrValue(source_senceNode, "id");
        string currentstage = string.Empty;        
        if(sourceDic_accountProfile.ContainsKey("/root/studystatus/currentsence/currentstage"))
            currentstage = sourceDic_accountProfile["/root/studystatus/currentsence/currentstage"];
        class_XmlHelper.SetAttribute(senceNode, "name", stageName);
        class_XmlHelper.SetAttribute(senceNode, "symbol", symbol);
        class_XmlHelper.SetAttribute(senceNode, "id", id);
        class_XmlHelper.SetAttribute(senceNode, "totalstage", stageCount.ToString());
        class_XmlHelper.SetAttribute(senceNode, "currentstage", symbol_step);

        XmlNode tipsNode = class_XmlHelper.CreateNode(workspaceDoc,"tips","");
        rootNode.AppendChild(tipsNode);
        XmlNodeList tipItems = sourceDoc_configsTips.SelectNodes("/root/tip[@symbol='" + symbol_tips + "']/step[@value='" + currentstage + "']/item");
        int index = 1;
        foreach(XmlNode tipItem in tipItems)
        {
            XmlNode newItemNode = class_XmlHelper.CreateNode(workspaceDoc, "item", "");
            class_XmlHelper.SetAttribute(newItemNode, "index", index.ToString());
            foreach(XmlNode contentNode in tipItem.SelectNodes("content"))
            {
                XmlNode newContentNode = class_XmlHelper.CreateNode(workspaceDoc, "content", "");
                string chinese = string.Empty;
                string english = string.Empty;
                string blocktype = string.Empty;
                chinese = class_XmlHelper.GetAttrValue(contentNode, "chinese");
                english = class_XmlHelper.GetAttrValue(contentNode, "english");
                blocktype = class_XmlHelper.GetAttrValue(contentNode, "blocktype");
                class_XmlHelper.SetAttribute(newContentNode, "chinese", chinese);
                class_XmlHelper.SetAttribute(newContentNode, "english", english);
                if (string.IsNullOrEmpty(blocktype))
                    class_XmlHelper.SetAttribute(newContentNode, "blocktype", blocktype);
                newItemNode.AppendChild(newContentNode);
            }
            tipsNode.AppendChild(newItemNode);
            index++;
        }

    }

    protected void BuildNode_WorkspaceStatus(XmlNode rootnode)
    {
        if (sourceDoc_workspaceStatus != null && sourceDoc_workspaceStatus.OuterXml != "")
        {
            XmlNode workstatusNode = class_XmlHelper.CreateNode(workspaceDoc, "workspacestatus", "");
            XmlNode activeWorkspaceNode = workspaceDoc.ImportNode(sourceDoc_workspaceStatus.DocumentElement, true);
            workstatusNode.AppendChild(activeWorkspaceNode);
            rootnode.AppendChild(workstatusNode);
        }
    }

    protected void BuildNode_Toolbox(XmlNode rootnode)
    {        
        string tmp_sourceDoc_workspaceToolbox;
        tmp_sourceDoc_workspaceToolbox = objectWorkspaceProcess.GET_Doc_WorkspaceToolbox(symbol, symbol_step);
        if (!string.IsNullOrEmpty(tmp_sourceDoc_workspaceToolbox))
        {
            sourceDoc_toolBox.LoadXml(tmp_sourceDoc_workspaceToolbox);
            XmlNode toolbox = class_XmlHelper.CreateNode(workspaceDoc, "toolbox", "");
            rootnode.AppendChild(toolbox);
            class_XmlHelper.SetAttribute(toolbox, "src", symbol_toolbox_src);
            XmlNode toolBoxNode = workspaceDoc.ImportNode(sourceDoc_toolBox.DocumentElement, true);
            toolbox.AppendChild(toolBoxNode);
        }
    }

    protected void BuildNode_Game(XmlNode rootnode)
    {
        XmlNode gameNode = class_XmlHelper.CreateNode(workspaceDoc,"game","");
        rootnode.AppendChild(gameNode);
        foreach(XmlNode activeScriptNode in stageNode.SelectNodes("game/script"))
        {
            XmlNode scriptNode = class_XmlHelper.CreateNode(workspaceDoc,"script","");
            class_XmlHelper.SetAttribute(scriptNode,"src",class_XmlHelper.GetAttrValue(activeScriptNode,"src"));
            gameNode.AppendChild(scriptNode);
        }
    }

    protected void BuildNode_Word(XmlNode rootnode)
    {
        string currentStage = sourceDic_accountProfile["/root/studystatus/currentsence/currentstage"];
        XmlNode wordsNode = class_XmlHelper.CreateNode(workspaceDoc, "words", "");
        rootnode.AppendChild(wordsNode);
        XmlNodeList words = sourceDoc_wordList.SelectNodes("/root/list[@symbol='" + symbol + "']/stage[@value='" + symbol_step + "']");
        foreach(XmlNode word in words)
        {
            XmlNode activeWord = workspaceDoc.ImportNode(word, true);
            wordsNode.AppendChild(activeWord);
        }
    }

    protected void BuildNode_Message(XmlNode rootnode)
    {
        XmlNode messageNode = class_XmlHelper.CreateNode(workspaceDoc, "message", "");
        rootnode.AppendChild(messageNode);
        XmlNode errNode = class_XmlHelper.CreateNode(workspaceDoc, "faild", "");
        messageNode.AppendChild(errNode);
        class_XmlHelper.SetAttribute(errNode, "msg", Object_LabelController.GetString("message", "ERR_Msg_Workspce_Coding"));
        XmlNode sucNode = class_XmlHelper.CreateNode(workspaceDoc, "suc", "");
        messageNode.AppendChild(sucNode);
        class_XmlHelper.SetAttribute(errNode, "msg", Object_LabelController.GetString("message", "SC_Param_Workspace_Run"));
    }

}