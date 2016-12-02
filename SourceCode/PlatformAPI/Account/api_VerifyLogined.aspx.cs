using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_api_VerifyLogined : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string activeUserName = "";
        string activeLoginedID = "";
        HttpCookie activeUserCookie = GetRequestCookie("userName");
        HttpCookie activeLoginedIDCookie = GetRequestCookie("loginedID");
        string tmpUserName = GetQuerystringParam("username");
        string tmpLoginedID = GetQuerystringParam("loginedId");        
        if (activeUserCookie != null && activeLoginedIDCookie != null)
        {
            activeUserName = activeUserCookie.Value;
            activeLoginedID = activeLoginedIDCookie.Value;
        }
        else if(tmpUserName != "" && tmpLoginedID!="")
        {
            activeUserName = tmpUserName;
            activeLoginedID = tmpLoginedID;
        }
        if(class_LoginedPool.verifyLoginedAccount(activeUserName, activeLoginedID, Request.UserHostAddress))
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
    }
}