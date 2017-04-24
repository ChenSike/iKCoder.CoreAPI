using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Net;
using System.Xml;

/// <summary>
/// Summary description for class_WebBase_IKcoderAPI
/// </summary>
public class class_WebBase_IKCoderAPI : class_Base_WebBaseclass
{

    //protected string Server_API = "http://ikcoder.iok.la:24525/";
    protected string Server_API = "http://localhost/";
    protected string Virtul_Folder_API = "PlatformAPI";
    protected string Produce_Name = "iKCoder";
    protected string Produce_Code = "12345678";
    protected string CookieContainer_Name = "CommonCookieContainer";
    protected class_Net_RemoteRequest Object_NetRemote;
    protected class_Base_Config Object_BaseConfig;
    protected class_Util_LabelsController Object_LabelController;
    protected class_CommonData Object_CommonData;
    protected int Session_TimeOutMinutes = 60;
    protected int Cookie_TimeOutHour = 1;  


    public class_WebBase_IKCoderAPI()
    {
    }

    protected void initServices()
    {
        Object_BaseConfig = new class_Base_Config();
        if (!Object_BaseConfig.DoOpen(APPFOLDERPATH + "\\" + "normaldata.xml"))
            return;
        if ((Object_LabelController = class_Util_LabelsController.CreateInstance(APPFOLDERPATH + "\\" + "labels.xml")) == null)
            return;
        Object_CommonData.InitServices(Object_BaseConfig, APPFOLDERPATH);          
        XmlNodeList RSDomainItems = Object_BaseConfig.GetItemNodes("RSDomain");
        Object_BaseConfig.SwitchToDESModeOFF();
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN") != null)
        {
            string perinstanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN").ToString();
            foreach (XmlNode activeDomainItem in RSDomainItems)
            {
                string itemName = Object_BaseConfig.GetAttrValue(activeDomainItem, "name");
                string domainValue = Object_BaseConfig.GetAttrValue(activeDomainItem, "domain");
                if (perinstanceDomain.Contains(domainValue))
                {
                    RSDoamin = domainValue;
                    break;
                }
            }
        }

    }

    protected override void InitAction()
    {
        initServices();
    }

    protected virtual bool BeforeExtenedAction()
    {
        return false;
    }

    protected override void DoAction()
    {
        CookieContainer activeCookieContainerObject = new CookieContainer();
        Object_DomainPersistance.ClearBuffer();
        Object_DomainPersistance.AddSingle(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), CookieContainer_Name, 3600, activeCookieContainerObject);
        CookieContainer activeCookieContainer = (CookieContainer)Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), CookieContainer_Name);
        Object_NetRemote = new class_Net_RemoteRequest(ref activeCookieContainer);
        ISRESPONSEDOC = true;
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_verifyActiveToken.aspx";
        string requestGetTokenURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "token") != null)
        {
            string verifyTokenDoc = "<root><token>" + Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "token") + "</token></root>";
            string resultDocFromServer = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(verifyTokenDoc, requestURL, 1000, 10000);
            XmlDocument responseFromServerDoc = new XmlDocument();
            responseFromServerDoc.LoadXml(resultDocFromServer);
            if (responseFromServerDoc.SelectSingleNode("/root/err") != null)
                regToken();

        }
        else
        {
            regDomain();
            regToken();
        }
        if (BeforeExtenedAction())
            ExtendedAction();
        if (Object_CommonData.isExecutedConnectedDB)
            Object_CommonData.CloseDBConnection();
    }

    protected void regDomain()
    {
        string requestURL = Server_API + Virtul_Folder_API + "/Domain/api_RegDomain.aspx?domain=" + Server_API;
        Object_NetRemote.getRemoteRequestByGet(requestURL);
    }

    protected void regToken()
    {
        string getTokenDoc = "<root><name>" + Produce_Name + "</name><code>" + Produce_Code + "</code></root>";
        string requestURL = Server_API + Virtul_Folder_API + "/Token/api_getToken.aspx";
        string strResultDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(getTokenDoc, requestURL, 1000 * 20, 1000 * 50);
        XmlDocument resultDoc = new XmlDocument();
        resultDoc.LoadXml(strResultDoc);
        XmlNode msgNode = resultDoc.SelectSingleNode("/root/msg");
        Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "token", 1440, class_XmlHelper.GetAttrValue(msgNode, "msg"));
    }

    protected virtual void ExtendedAction()
    {

    }
}