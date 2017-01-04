using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Net;
using System.Xml;

/// <summary>
/// class_WebBase 的摘要说明
/// </summary>
public class class_WebBase : class_Base_WebBaseclass
{

    //protected string Server_API = "http://ikcoder.iok.la:24525/";
    protected string Server_API = "http://localhost/";
    protected string Virtul_Folder_API = "PlatformAPI";
    protected string Produce_Name = "iKCoder";
    protected string Produce_Code = "12345678";
    protected class_Net_RemoteRequest Object_NetRemote = new class_Net_RemoteRequest();
    protected static Store_DomainPersistance Object_DomainPersistance = new Store_DomainPersistance();
      

	public class_WebBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    protected override void DoAction()
    {
        ISRESPONSEDOC = true;
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_verifyActiveToken.aspx";
        string requestGetTokenURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        foreach(Cookie activeCookie in class_Net_RemoteRequest.active_Cookies.GetCookies(new Uri(requestGetTokenURL)))
        {
            HttpCookie newHttpCookie = new HttpCookie(activeCookie.Name);
            newHttpCookie.Domain = activeCookie.Domain;
            newHttpCookie.Value = activeCookie.Value;
            if (Request.Cookies[activeCookie.Name] != null)
                Request.Cookies.Set(newHttpCookie);
            else
                Request.Cookies.Add(newHttpCookie);
        }
        if (Object_DomainPersistance.Get(Page.Request.UserHostAddress, "token") != null)
        {
            string verifyTokenDoc = "<root><token>" + Object_DomainPersistance.Get(Page.Request.UserHostAddress, "token") + "</token></root>";
            string resultDocFromServer = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(verifyTokenDoc, requestURL, 1000, 10000);
            XmlDocument responseFromServerDoc = new XmlDocument();
            responseFromServerDoc.LoadXml(resultDocFromServer);
            if (responseFromServerDoc.SelectSingleNode("/root/err") != null)
            {             
                regToken();
            }
            else
                ExtendedAction();
        }
        else
            regToken();

    }

    protected void regToken()
    {
        string getTokenDoc = "<root><name>" + Produce_Name + "</name><code>" + Produce_Code + "</code></root>";
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        string strResultDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(getTokenDoc, requestURL, 1000 * 20, 1000 * 50);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml(strResultDoc);
        XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
        Object_DomainPersistance.Add(Page.Request.UserHostAddress, "token", 1440, class_XmlHelper.GetAttrValue(msgNode, "msg"));        
    }

    protected virtual void ExtendedAction()
    {

    }

}