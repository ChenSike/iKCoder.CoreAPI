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
    //public static string Server_API = "http://localhost/";
    protected string Virtul_Folder_API = "PlatformAPI";
    protected string Produce_Name = "iKCoder";
    protected string Produce_Code = "12345678";
    protected string CookieContainer_Name = "CommonCookieContainer";
    protected string HostAddress = "";
    protected class_Net_RemoteRequest Object_NetRemote;
    protected class_Base_Config Object_BaseConfig;
    protected static Store_DomainPersistance Object_DomainPersistance = new Store_DomainPersistance();
      

	public class_WebBase()
	{
    }

    protected void initServices()
    {
        Object_BaseConfig = new class_Base_Config();
        if (!Object_BaseConfig.DoOpen(APPFOLDERPATH + "\\" + "normaldata.xml"))
            return;
        XmlNodeList RSDomainItems = Object_BaseConfig.GetItemNodes("RSDomain");
        Object_BaseConfig.SwitchToDESModeOFF();
        foreach (XmlNode activeDomainItem in RSDomainItems)
        {
            string itemName = Object_BaseConfig.GetAttrValue(activeDomainItem, "name");
            string domainValue = Object_BaseConfig.GetAttrValue(activeDomainItem, "domain");
            if (!RSDoamin.ContainsKey(itemName))
                RSDoamin.Add(itemName, domainValue);
            else
                RSDoamin[itemName] = domainValue;
        }

    }

    protected override void DoAction()
    {
        initServices();
        HostAddress = Page.Request.UserHostAddress;
        CookieContainer activeCookieContainerObject = new CookieContainer();
        Object_DomainPersistance.AddSingle(Object_DomainPersistance.GetKeyName(HostAddress, Produce_Name), CookieContainer_Name, -999, activeCookieContainerObject);
        CookieContainer activeCookieContainer = (CookieContainer)Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(HostAddress, Produce_Name), CookieContainer_Name);
        Object_NetRemote = new class_Net_RemoteRequest(ref activeCookieContainer);
        ISRESPONSEDOC = true;
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_verifyActiveToken.aspx";
        string requestGetTokenURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(HostAddress, Produce_Name), "token") != null)
        {
            string verifyTokenDoc = "<root><token>" + Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(HostAddress, Produce_Name), "token") + "</token></root>";
            string resultDocFromServer = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(verifyTokenDoc, requestURL, 1000, 10000);
            XmlDocument responseFromServerDoc = new XmlDocument();
            responseFromServerDoc.LoadXml(resultDocFromServer);
            if (responseFromServerDoc.SelectSingleNode("/root/err") != null)            
                regToken();           
                
        }
        else
            regToken();
        ExtendedAction();

    }

    protected void regToken()
    {
        string getTokenDoc = "<root><name>" + Produce_Name + "</name><code>" + Produce_Code + "</code></root>";
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        string strResultDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(getTokenDoc, requestURL, 1000 * 20, 1000 * 50);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml(strResultDoc);
        XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
        Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(HostAddress, Produce_Name), "token", 1440, class_XmlHelper.GetAttrValue(msgNode, "msg"));        
    }

    protected virtual void ExtendedAction()
    {

    }

}