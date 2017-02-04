using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// class_WebClass_WLA 的摘要说明
/// </summary>
public class class_WebClass_WLA : class_WebClass_WA
{

    protected string activeUserName = "";
    protected string activeLoginedID = "";

	public class_WebClass_WLA()
	{
		
	}

    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        HttpCookie activeUserCookie = GetRequestCookie("userName");
        HttpCookie activeLoginedIDCookie = GetRequestCookie("loginedID");
        if (activeUserCookie != null && activeLoginedIDCookie != null)
        {
            activeUserName = activeUserCookie.Value;
            activeLoginedID = activeLoginedIDCookie.Value;
        }
        if(string.IsNullOrEmpty(activeUserName))
        {
            activeUserName = GetQuerystringParam("username");
            activeLoginedID = class_LoginedPool.getActiveAccountItem(activeUserName).LoginedID;
        }
        if (class_LoginedPool.verifyLoginedAccount(activeUserName, activeLoginedID, Request.UserHostAddress))
        {
            AfterExtenedFunction();
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
        if (object_CommonLogic.isExecutedConnectedDB)
            object_CommonLogic.CloseDBConnection();
    }

    protected virtual void AfterExtenedFunction()
    {

    }

}