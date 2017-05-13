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

    public static int GetFinishedTotalSpendTime(XmlDocument resourceDoc_Profile,class_CommonDefined.enumSenceType senceType)
    {
        if (resourceDoc_Profile == null)
            return 0;
        else
        {
            int totalSpendTime = 0;
            List<XmlNode> finishedItems = GetFinishedItems(resourceDoc_Profile);
            foreach(XmlNode activeItem in finishedItems)
            {
                bool isCalc = false;
                XmlNode symbolNode = activeItem.SelectSingleNode("symbol");
                string strSymbol = class_XmlHelper.GetNodeValue(symbolNode);
                class_CommonDefined.enumSenceType activeSenceType = class_Bus_SenceDoc.GetSenceType(strSymbol);
                if (activeSenceType == senceType)
                    isCalc = true;
                else
                    isCalc = false;
                if(isCalc)
                {
                    string strSpendTime = string.Empty;
                    XmlNode spendTime = activeItem.SelectSingleNode("spendtime");
                    strSpendTime = class_XmlHelper.GetNodeValue(spendTime);
                    int iSpendTime = 0;
                    int.TryParse(strSpendTime, out iSpendTime);
                    totalSpendTime = totalSpendTime + iSpendTime;
                }
            }
            return totalSpendTime;
        }
    }

    public static List<XmlNode> GetCodetimeLineItems(XmlDocument resourceDoc_Profile)
    {
        List<XmlNode> resultList = new List<XmlNode>();
        XmlNodeList codetimelineItemNodes = resourceDoc_Profile.SelectNodes("/root/studystatus/codetimeline/item");
        foreach(XmlNode activeItem in codetimelineItemNodes)        
            resultList.Add(activeItem);
        return resultList;
    }

    public static void SetCodetimeLineHours(XmlDocument resourceDoc_Profile,string symbol)
    {
        if (resourceDoc_Profile == null || string.IsNullOrEmpty(resourceDoc_Profile.OuterXml))
            return;
        XmlNode codetimelineNode = resourceDoc_Profile.SelectSingleNode("/root/studystatus/codetimeline");
        if (codetimelineNode == null)
        {
            codetimelineNode = class_XmlHelper.CreateNode(resourceDoc_Profile, "codetimeline", "");
            class_XmlHelper.SetAttribute(codetimelineNode, "recordtime", DateTime.Now.ToString());
            resourceDoc_Profile.SelectSingleNode("/root/studystatus").AppendChild(codetimelineNode);
        }
        else
            class_XmlHelper.SetAttribute(codetimelineNode, "recordtime", DateTime.Now.ToString());        
        XmlNodeList itemNodes = codetimelineNode.SelectNodes("item");
        if (itemNodes.Count >= 10)
        {
            XmlNode lastNode = codetimelineNode.SelectSingleNode("item[@index='10']");
            if (lastNode != null)
                codetimelineNode.RemoveChild(lastNode);
        }
        XmlNode activeItemNode = codetimelineNode.SelectSingleNode("item[@date='" + DateTime.Now.ToString("yyyy-MM-dd") + "']");
        if (activeItemNode==null)
        {            
            activeItemNode = class_XmlHelper.CreateNode(resourceDoc_Profile, "item", "");
            class_XmlHelper.SetAttribute(activeItemNode, "index", (itemNodes.Count+1).ToString());
            class_XmlHelper.SetAttribute(activeItemNode, "date", DateTime.Now.ToString("yyyy-MM-dd"));
            codetimelineNode.AppendChild(activeItemNode);
        }
        string strRecordTime = class_XmlHelper.GetAttrValue(codetimelineNode, "recordtime");
        DateTime dtRecordTime = DateTime.Parse(strRecordTime);
        string strValue = class_XmlHelper.GetAttrValue(activeItemNode, "value");
        int iValue = 0;
        int.TryParse(strValue, out iValue);
        int iTotalMinutesDate = iValue + (DateTime.Now - dtRecordTime).Minutes;
        class_XmlHelper.SetAttribute(activeItemNode, "value", iTotalMinutesDate.ToString());
    }

    public static string GetTmpEntryTime(string symbol,XmlDocument resourceDoc_Profile)
    {
        if (resourceDoc_Profile == null || string.IsNullOrEmpty(resourceDoc_Profile.OuterXml))
            return string.Empty;
        XmlNode finishedNode = resourceDoc_Profile.SelectSingleNode("/root/studystatus/finished");
        if (finishedNode == null)
            return string.Empty;
        XmlNode activeFinishedItemNode = finishedNode.SelectSingleNode("item[symbol[text()='" + symbol + "']]");
        if (activeFinishedItemNode != null)
        {
            XmlNode tmpentryNode = activeFinishedItemNode.SelectSingleNode("tmpentry");
            string strtmpentry = class_XmlHelper.GetNodeValue(tmpentryNode);
            return strtmpentry;
        }
        else
            return string.Empty;
    }

    public static List<XmlNode> GetFinishedItems(XmlDocument resourceDoc_Profile)
    {
        List<XmlNode> result = new List<XmlNode>();
        if (resourceDoc_Profile == null)
            return result;
        else
        {
            XmlNodeList finishItems = resourceDoc_Profile.SelectNodes("/root/studystatus/finished/item");
            if(finishItems!=null)
            {
                foreach(XmlNode item in finishItems)
                {
                    result.Add(item);
                }                
            }
            return result;

        }
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