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

    protected string Server_API = "http://ikcoder.iok.la:24525/";
    protected string Virtul_Folder_API = "PlatformAPI";
    protected string Produce_Name = "iKCoder";
    protected string Produce_Code = "12345678";
    protected class_Net_RemoteRequest Object_NetRemote = new class_Net_RemoteRequest();
    protected string Token = "";

	public class_WebBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    protected override void DoAction()
    {
        ISRESPONSEDOC = true;
        if (Request.Cookies["token"] != null)
        {
            string requestURL = Server_API + Virtul_Folder_API + "/Token/api_verifyActiveToken.aspx";
            string verifyTokenDoc = "<root><token>" + Request.Cookies["token"].Value + "</token></root>";
            List<Cookie> requestCookieLst = new List<Cookie>();
            foreach(string activeCookieKey in Request.Cookies.Keys)
            {
                HttpCookie activeCookie = Request.Cookies[activeCookieKey];
                Cookie newNetCookie = new Cookie();
                newNetCookie.Name = activeCookie.Name;
                newNetCookie.Value = activeCookie.Value;                
                requestCookieLst.Add(newNetCookie);
            }
            string resultDocFromServer = Object_NetRemote.getRemoteRequestToString(verifyTokenDoc, requestURL, 1000, 10000, requestCookieLst);
            XmlDocument responseFromServerDoc = new XmlDocument();
            responseFromServerDoc.LoadXml(resultDocFromServer);
            if (responseFromServerDoc.SelectSingleNode("/root/err") != null)
            {
                Request.Cookies.Remove("token");
                Response.Cookies.Remove("token");
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
        List<Cookie> activeCookies = Object_NetRemote.getRemoteServerCookie(requestURL, getTokenDoc);
        foreach (Cookie activeNetCookie in activeCookies)
        {
            HttpCookie newCookie = new HttpCookie(activeNetCookie.Name);
            newCookie.Value = activeNetCookie.Value;
            newCookie.Domain = activeNetCookie.Domain;
            Response.Cookies.Add(newCookie);
        }
    }

    protected virtual void ExtendedAction()
    {

    }

}