using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

/// <summary>
/// class_Workspace 的摘要说明
/// </summary>
public class class_WorkspaceProcess
{
    public static string _PreWorkspace = "workspace_status_";
    public static string _DefaultWorkspaceToolbox = "workspace_toolbox_";
    public string serverPath = string.Empty;
    public string clientSymbol = string.Empty;
    private class_Net_RemoteRequest ref_Object_NetRemote;
    private string username;

    public class_WorkspaceProcess(string activeServerPath, string activeClientSymbol, class_Net_RemoteRequest activeNetObject,string activeUsername)
	{
        serverPath = activeServerPath;
        clientSymbol = activeClientSymbol;
        ref_Object_NetRemote = activeNetObject;
        username = activeUsername;
	}

    public string GET_Doc_WorkspaceStatus(string symbol)
    {        
        if (string.IsNullOrEmpty(symbol))
            return string.Empty;
        string workspaceStatusSymbol = symbol + "_" + username;
        string URL = serverPath + "/Data/api_GetVerifySymbolExisted.aspx?cid=" + clientSymbol + "&symbol=" + workspaceStatusSymbol;
        string returnDoc = ref_Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        string decoderDoc = "";
        if (returnDoc.Contains("false"))
            workspaceStatusSymbol = symbol;
        URL = serverPath + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + clientSymbol + "&symbol=" + workspaceStatusSymbol;
        returnDoc = ref_Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument tmpData = new XmlDocument();
        tmpData.LoadXml(returnDoc);
        string strMsg = class_XmlHelper.GetAttrValue(tmpData.SelectSingleNode("/root/msg"), "msg");
        decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);
        return decoderDoc;                 
    }

    public string GET_Doc_WorkspaceToolbox(string symbol,string currentStage)
    {
        if (string.IsNullOrEmpty(symbol))
            return string.Empty;
        string workspaceStatusSymbol = class_WorkspaceProcess._DefaultWorkspaceToolbox + symbol + "_" + "s" + currentStage;
        string URL = serverPath + "/Data/api_GetVerifySymbolExisted.aspx?cid=" + clientSymbol + "&symbol=" + workspaceStatusSymbol;
        string returnDoc = ref_Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        string decoderDoc = class_CommonUtil.Decoder_Base64(returnDoc);
        if (decoderDoc.Contains("false"))
            return string.Empty;
        URL = serverPath + "/Data/api_GetMetaTextBase64Data.aspx?cid=" + clientSymbol + "&symbol=" + workspaceStatusSymbol;
        returnDoc = ref_Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument tmpData = new XmlDocument();
        tmpData.LoadXml(returnDoc);
        string strMsg = class_XmlHelper.GetAttrValue(tmpData.SelectSingleNode("/root/msg"), "msg");
        decoderDoc = class_CommonUtil.Decoder_Base64(strMsg);
        return decoderDoc;  
    }   
         



}