using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Get_Workspace : class_WebBase_IKCoderAPI_UA
{
    protected XmlDocument sourceDoc_profile = new XmlDocument();
    protected XmlDocument sourceDoc_sence = new XmlDocument();
    protected XmlDocument sourceDoc_toolBox = new XmlDocument();

    protected DataRow activeSenceDataRow = null;
    protected DataRow activeToolboxDataRow = null;
    protected DataRow activeWorkspaceStatusRow = null;
    protected string symbol = string.Empty;
    protected string currentStage = string.Empty;
    protected XmlNodeList stageNodes = null;
    protected XmlDocument workspaceDoc = null;
    protected XmlNode workspaceDoc_rootNode = null;
    private string finishstage = "";

    protected bool Init_ProfileDoc()
    {
        sourceDoc_profile = class_Bus_ProfileDoc.GetProfileDocument(Server_API, Virtul_Folder_API, Object_NetRemote, logined_user_name);
        if (sourceDoc_profile == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "profiledoc->empty", "");
            return false;
        }
        else
            return true;    
    }

    protected bool Init_SenceDoc()
    {
        sourceDoc_sence = class_Bus_SenceDoc.GetSenceDocument(Object_CommonData, symbol, out activeSenceDataRow);
        if (sourceDoc_sence == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "sencedoc->empty", "");
            return false;
        }
        else
        {
            return true;
        }
    }

    protected bool Init_ToolBoxDoc()
    {
        sourceDoc_toolBox = class_Bus_ToolboxDoc.GetToolboxDocument(Object_CommonData, symbol, currentStage, out activeToolboxDataRow);
        if (sourceDoc_toolBox == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "sencedoc->empty", "");
            return false;
        }
        else
            return true;        
    }

    protected void Get_CurrentStage()
    {
        XmlNode currentsenceNode = sourceDoc_profile.SelectSingleNode("/root/studystatus/currentsence[symbol[text()='" + symbol + "']]");
        if (currentsenceNode == null)
        {
            currentStage = "1";
            currentsenceNode = class_XmlHelper.CreateNode(sourceDoc_profile, "currentsence", "");
            XmlNode studyStatusNode = sourceDoc_profile.SelectSingleNode("/root/studystatus");
            studyStatusNode.AppendChild(currentsenceNode);
            XmlNode symbolNode = class_XmlHelper.CreateNode(sourceDoc_profile, "symbol", symbol);
            currentsenceNode.AppendChild(symbolNode);
            XmlNode currentStageNode = class_XmlHelper.CreateNode(sourceDoc_profile, "currentstage", currentStage);
            currentsenceNode.AppendChild(currentStageNode);
            XmlNode finishStageNode = class_XmlHelper.CreateNode(sourceDoc_profile, "finishstage", "");
            currentsenceNode.AppendChild(finishStageNode);
            XmlNodeList stageNodes = sourceDoc_sence.SelectNodes("/sence/stages/stage");
            foreach (XmlNode activeStage in stageNodes)
            {
                string step = class_XmlHelper.GetAttrValue(activeStage, "step");
                XmlNode itemNode = class_XmlHelper.CreateNode(sourceDoc_profile, "item", "");
                class_XmlHelper.SetAttribute(itemNode, "step", step);
                class_XmlHelper.SetAttribute(itemNode, "finish", "0");
                finishStageNode.AppendChild(itemNode);
            }
            class_Bus_ProfileDoc.SetUserProfile(Server_API, Virtul_Folder_API, Object_NetRemote, logined_user_name, sourceDoc_profile.OuterXml);            
        }
        else
        {
            XmlNode symbolNode = currentsenceNode.SelectSingleNode("symbol");
            XmlNode currentStageNode = currentsenceNode.SelectSingleNode("currentstage");
            XmlNode finishStageNode = currentsenceNode.SelectSingleNode("finishstage");
            currentStage = class_XmlHelper.GetNodeValue(currentStageNode);
            if (string.IsNullOrEmpty(currentStage))
                currentStage = "1";
        }
        XmlNodeList senceNodes = sourceDoc_profile.SelectNodes("/root/studystatus/currentsence/finishstage/item[@finish='1']");
        finishstage = senceNodes.Count.ToString();
    }

    protected void Set_Basic()
    {
        XmlNode basicNode = class_XmlHelper.CreateNode(workspaceDoc, "basic", "");
        workspaceDoc_rootNode.AppendChild(basicNode);
        XmlNode usrNode = class_XmlHelper.CreateNode(workspaceDoc, "usr", "");
        basicNode.AppendChild(usrNode);
        string user_id = Session["logined_user_id"].ToString();
        string user_nickname = Session["logined_user_nickname"].ToString();
        string user_header = "/Account/Profile/api_iKCoder_Profile_Get_HeaderImg.aspx";
        class_XmlHelper.SetAttribute(usrNode, "id", user_id);
        class_XmlHelper.SetAttribute(usrNode, "nickname", user_nickname);
        class_XmlHelper.SetAttribute(usrNode, "header", user_header);
        XmlNode senceNode = class_XmlHelper.CreateNode(workspaceDoc, "sence", "");
        workspaceDoc_rootNode.AppendChild(senceNode);
        XmlNode source_senceNode = sourceDoc_sence.SelectSingleNode("/sence");
        int stageCount = (source_senceNode.SelectNodes("stages/stage")).Count;
        string stageName = class_XmlHelper.GetAttrValue(source_senceNode, "name");
        string id = class_XmlHelper.GetAttrValue(source_senceNode, "id");
        class_XmlHelper.SetAttribute(senceNode, "name", stageName);
        class_XmlHelper.SetAttribute(senceNode, "symbol", symbol);
        class_XmlHelper.SetAttribute(senceNode, "id", id);
        class_XmlHelper.SetAttribute(senceNode, "totalstage", stageCount.ToString());
        class_XmlHelper.SetAttribute(senceNode, "currentstage", currentStage);
        class_XmlHelper.SetAttribute(senceNode, "finishstage", finishstage);
    }

    protected void Set_Tip()
    {
        XmlNode stageNode = sourceDoc_sence.SelectSingleNode("/sence/stages/stage[@step='" + currentStage + "']");
        string symbol_tips = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("tips"), "symbol");
        class_Data_SqlSPEntry activeSPEntry_configTips = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TIPS);
        activeSPEntry_configTips.ClearAllParamsValues();
        activeSPEntry_configTips.ModifyParameterValue("@symbol", symbol_tips);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configTips, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null && textDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
            string strConfigDoc = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "config", out strConfigDoc);
            strConfigDoc = class_CommonUtil.Decoder_Base64(strConfigDoc);
            XmlDocument sourceDoc_configsTips = new XmlDocument();
            sourceDoc_configsTips.LoadXml(strConfigDoc);
            XmlNode tipsNode = class_XmlHelper.CreateNode(workspaceDoc, "tips", "");
            workspaceDoc_rootNode.AppendChild(tipsNode);
            XmlNodeList tipItems = sourceDoc_configsTips.SelectNodes("/tip/step[@value='" + currentStage + "']/item");
            int index = 1;
            foreach (XmlNode tipItem in tipItems)
            {
                XmlNode newItemNode = class_XmlHelper.CreateNode(workspaceDoc, "item", "");
                class_XmlHelper.SetAttribute(newItemNode, "index", index.ToString());
                foreach (XmlNode contentNode in tipItem.SelectNodes("content"))
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
    }

    public void Set_ToolBox()
    {
        XmlNode toolbox = class_XmlHelper.CreateNode(workspaceDoc, "toolbox", "");
        workspaceDoc_rootNode.AppendChild(toolbox);
        class_XmlHelper.SetAttribute(toolbox, "src", class_Bus_ToolboxDoc.GetToolboxSymbol(symbol, currentStage));
        XmlNode toolBoxNode = workspaceDoc.ImportNode(sourceDoc_toolBox.DocumentElement, true);
        toolbox.AppendChild(toolBoxNode);
    }

    public void Set_WorkspaceStatus()
    {
        XmlDocument sourceDoc_workspaceStatus = class_Bus_WorkspaceStatus.GetWorkspaceStatusDocument(Object_CommonData, symbol, currentStage, logined_user_name, out activeWorkspaceStatusRow);
        if (sourceDoc_workspaceStatus != null)
        {
            XmlNode workstatusNode = class_XmlHelper.CreateNode(workspaceDoc, "workspacestatus", "");
            XmlNode activeWorkspaceNode = workspaceDoc.ImportNode(sourceDoc_workspaceStatus.DocumentElement, true);
            workstatusNode.AppendChild(activeWorkspaceNode);
            workspaceDoc_rootNode.AppendChild(workstatusNode);
        }
    }

    public void Set_Game()
    {
        XmlNode gameNode = class_XmlHelper.CreateNode(workspaceDoc, "game", "");
        workspaceDoc_rootNode.AppendChild(gameNode);
        XmlNode stageNode = sourceDoc_sence.SelectSingleNode("/sence/stages/stage[@step='" + currentStage + "']");
        foreach (XmlNode activeScriptNode in stageNode.SelectNodes("game/script"))
        {
            XmlNode scriptNode = class_XmlHelper.CreateNode(workspaceDoc, "script", "");
            class_XmlHelper.SetAttribute(scriptNode, "src", class_XmlHelper.GetAttrValue(activeScriptNode, "src"));
            gameNode.AppendChild(scriptNode);
        }
    }

    public void Set_Message()
    {
        XmlNode messageNode = class_XmlHelper.CreateNode(workspaceDoc, "message", "");
        workspaceDoc_rootNode.AppendChild(messageNode);
        XmlNode errNode = class_XmlHelper.CreateNode(workspaceDoc, "faild", "");
        messageNode.AppendChild(errNode);
        class_XmlHelper.SetAttribute(errNode, "msg", Object_LabelController.GetString("message", "ERR_Msg_Workspce_Coding"));
        XmlNode sucNode = class_XmlHelper.CreateNode(workspaceDoc, "suc", "");
        messageNode.AppendChild(sucNode);
        string sucMsg = Object_LabelController.GetString("message", "SC_Param_Workspace_Run");
        sucMsg = sucMsg.Replace("{P1}", currentStage);
        class_XmlHelper.SetAttribute(sucNode, "msg", sucMsg);
    }

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            workspaceDoc = new XmlDocument();
            workspaceDoc.LoadXml("<root></root>");
            workspaceDoc_rootNode = workspaceDoc.SelectSingleNode("/root");
            Object_CommonData.PrepareDataOperation();
            if (!Init_SenceDoc())
                return;
            if (!Init_ProfileDoc())
                return;
            Get_CurrentStage();
            if (!Init_ToolBoxDoc())
                return;


            Set_Basic();
            Set_Tip();
            Set_ToolBox();
            Set_WorkspaceStatus();
            Set_Game();
            Set_Message();
            /*
            stageNode = senceNode.SelectSingleNode("stages/stage[@step='" + currentstage + "']");
            
            symbol_workspacestatus = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("workspacestatus"), "symbol");
            XmlNode messageNode = stageNode.SelectSingleNode("message");
            symbol_message_err = class_XmlHelper.GetAttrValue(messageNode.SelectSingleNode("err"), "code");
            symbol_message_suc = class_XmlHelper.GetAttrValue(messageNode.SelectSingleNode("suc"), "code");

            string tmp_sourceDoc_workspaceStatus;
            tmp_sourceDoc_workspaceStatus = objectWorkspaceProcess.GET_Doc_WorkspaceStatus(symbol, currentstage, symbol_workspacestatus);
            if (tmp_sourceDoc_workspaceStatus != "")
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

            
            XmlNode rootNode = workspaceDoc.SelectSingleNode("/root");
            BuildNode_Basic(rootNode);
            BuildNode_WorkspaceStatus(rootNode);
            BuildNode_Toolbox(rootNode);
            BuildNode_Game(rootNode);
            BuildNode_Word(rootNode);
            BuildNode_Message(rootNode, currentstage);
            RESPONSEDOCUMENT.LoadXml(workspaceDoc.OuterXml);
            */
        }
    }
}