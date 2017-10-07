using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Account
/// </summary>
public class class_Bus_Account
{

    public static Dictionary<string,Dictionary<string,string>> GetAccountList(string Server_API, string Virtul_Folder_API, class_Net_RemoteRequest Object_NetRemote)
    {
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        string requestAPI = "/Account/api_GetList.aspx";
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 1024 * 1024);
        XmlDocument tmpData = new XmlDocument();
        tmpData.LoadXml(returnStrDoc);
        XmlNodeList msgNodes = tmpData.SelectNodes("/root/msg");
        if (msgNodes != null)
        {
            string username = string.Empty;
            foreach (XmlNode activeMsgNode in msgNodes)
            {
                Dictionary<string, string> attrsList = new Dictionary<string, string>();
                username = class_XmlHelper.GetAttrValue(activeMsgNode, "name");
                string id = class_XmlHelper.GetAttrValue(activeMsgNode, "id");
                string password = class_XmlHelper.GetAttrValue(activeMsgNode, "password");
                attrsList.Add("id", id);
                attrsList.Add("username", username);
                attrsList.Add("password",password);
                if(!result.ContainsKey(username))
                    result.Add(username, attrsList);
            }
        }
        return result;
    }

    public class_Bus_Account()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool GetCheckedAccountStudents(string symbol,string password)
    {
        return true;
    }
}