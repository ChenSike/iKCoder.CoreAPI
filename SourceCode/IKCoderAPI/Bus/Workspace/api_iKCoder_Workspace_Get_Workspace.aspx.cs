using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_Workspace_api_iKCoder_Workspace_Get_Workspace : class_WebBase_IKCoderAPI_UA
{
    protected string symbol = string.Empty;


    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            
                        
            objectWorkspaceProcess = new class_WorkspaceProcess(Server_API + Virtul_Folder_API, ClientSymbol, Object_NetRemote, user_name);

            string configsItemSymbol = "workspace_configsitem_common";
            string URL = Server_API + Virtul_Folder_API + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + ClientSymbol + "&symbol=" + configsItemSymbol;
            string returnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
            XmlDocument tmpDoc = new XmlDocument();
            tmpDoc.LoadXml(returnDoc);
            string strMsg = class_XmlHelper.GetAttrValue(tmpDoc.SelectSingleNode("/root/msg"), "msg");
            string decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);
            sourceDoc_configsItem = new XmlDocument();
            sourceDoc_configsItem.LoadXml(decoderDoc);
            senceNode = sourceDoc_configsItem.SelectSingleNode("/root/sence[@symbol='" + symbol + "']");

            CommonLogic_SetCurrentStage();
            symbol_step = currentstage;

            stageNode = senceNode.SelectSingleNode("stages/stage[@step='" + currentstage + "']");
            symbol_tips = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("tips"), "symbol");
            symbol_toolbox = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("toolbox"), "symbol");
            symbol_toolbox_src = class_XmlHelper.GetAttrValue(stageNode.SelectSingleNode("toolbox"), "src");
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

            workspaceDoc = new XmlDocument();
            workspaceDoc.LoadXml("<root></root>");
            XmlNode rootNode = workspaceDoc.SelectSingleNode("/root");
            BuildNode_Basic(rootNode);
            BuildNode_WorkspaceStatus(rootNode);
            BuildNode_Toolbox(rootNode);
            BuildNode_Game(rootNode);
            BuildNode_Word(rootNode);
            BuildNode_Message(rootNode, currentstage);
            RESPONSEDOCUMENT.LoadXml(workspaceDoc.OuterXml);

        }
    }
}