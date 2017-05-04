using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for class_Bus_GetProfileDoc
/// </summary>
public class class_Bus_ProfileDoc
{
    public class_Bus_ProfileDoc()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static XmlDocument GetProfileDocument(string Server_API,string Virtul_Folder_API, class_Net_RemoteRequest Object_NetRemote, string userName)
    {
        string profileSymbol = "profile_" + "ikcoder_" + userName;
        string requestAPI = "/Profile/api_AccountProfile_SelectProfileBySymbol.aspx?symbol=" + profileSymbol;
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
        XmlDocument tmpData = new XmlDocument();
        tmpData.LoadXml(returnStrDoc);
        XmlDocument sourceDoc_profile = new XmlDocument();
        if (tmpData.SelectSingleNode("/root/err")==null)
        {            
            XmlNode msgNode = tmpData.SelectSingleNode("/root/msg");
            if (msgNode != null)
            {
                string strDourceDoc = class_XmlHelper.GetAttrValue(msgNode, "msg");
                if (!string.IsNullOrEmpty(strDourceDoc))
                {
                    sourceDoc_profile.LoadXml(strDourceDoc);
                    if (sourceDoc_profile.SelectSingleNode("/root/err")==null)
                    {                        
                        return sourceDoc_profile;
                    }
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }            
        }
        else
        {
            return null;
        }
    }

    
    public static bool SetUserProfile(string Server_API, string Virtul_Folder_API, class_Net_RemoteRequest Object_NetRemote, string userName,string dataDocument)
    {
        string profileSymbol = "profile_" + "ikcoder_" + userName;
        string strBase64Data = class_CommonUtil.Encoder_Base64(dataDocument);
        StringBuilder strInputDoc = new StringBuilder();
        strInputDoc.Append("<root>");
        strInputDoc.Append("<symbol>");
        strInputDoc.Append(profileSymbol);
        strInputDoc.Append("</symbol>");
        strInputDoc.Append("<data>");
        strInputDoc.Append(strBase64Data);
        strInputDoc.Append("</data>");
        strInputDoc.Append("</root>");
        string requestAPI = "/Profile/api_AccountProfile_UpdateBySymbol.aspx";
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strInputDoc.ToString(), URL, 1000 * 60, 1024 * 1024);
        if (!returnStrDoc.Contains("err"))
            return true;
        else
            return false;
    }    

}