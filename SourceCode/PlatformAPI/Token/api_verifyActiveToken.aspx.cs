using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Token_api_verifyActiveToken : class_WebBase_NWA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string persistanceDomain = "http://localhost";
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientID), "RSDOMAIN") != null)
            persistanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientID), "RSDOMAIN").ToString();
        object_CommonLogic.InitServices(APPFOLDERPATH, RSDoamin,persistanceDomain);
        HttpCookie cookieFromRequest = GetRequestCookie("token");
        if (cookieFromRequest == null ? object_TokenLogic.CheckRegistiedToken(Session, REQUESTDOCUMENT, out _fromProduct) : object_TokenLogic.CheckRegistiedToken(Session, cookieFromRequest, out _fromProduct))
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "api_getToken", class_CommonDefined.enumExecutedCode.executed.ToString(),"verified token" , "");
        else
            AddErrMessageToResponseDOC("BadToken", "Your token is invalidated", "");
    }
}