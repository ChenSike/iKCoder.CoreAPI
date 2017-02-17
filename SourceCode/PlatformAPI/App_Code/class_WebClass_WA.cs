using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// class_WebClass_WA 的摘要说明
/// </summary>
public class class_WebClass_WA:class_Base_WebBaseclass
{

    protected class_CommonLogic object_CommonLogic = new class_CommonLogic();
    protected class_ProductToken object_TokenLogic = new class_ProductToken();
    protected string _fromProduct = "iKCoder";

    protected override void DoAction()
    {
        ISRESPONSEDOC = true;
        string persistanceDomain = "http://localhost";
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN") != null)        
            persistanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN").ToString();
        object_CommonLogic.InitServices(APPFOLDERPATH, RSDoamin, persistanceDomain);
        HttpCookie cookieFromRequest = GetRequestCookie("token");
        if (cookieFromRequest == null ? object_TokenLogic.CheckRegistiedToken(Session, REQUESTDOCUMENT, out _fromProduct) : object_TokenLogic.CheckRegistiedToken(Session, cookieFromRequest, out _fromProduct))
            ExtenedFunction();
        else
            AddErrMessageToResponseDOC("BadToken", "Your token is invalidated", "");
        if (object_CommonLogic.isExecutedConnectedDB)
            object_CommonLogic.CloseDBConnection();
    }

    protected virtual void ExtenedFunction()
    {

    }

}