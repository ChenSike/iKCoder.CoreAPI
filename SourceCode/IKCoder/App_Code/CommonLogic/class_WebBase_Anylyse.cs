using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// Summary description for class_WebBase_Anylyse
/// </summary>
public class class_WebBase_Anylyse:class_WebBase_UA
{

    protected virtual void AfterExtened()
    {

    }

    protected override void ExtendedAction()
    {
        string username = Session["logined_user_name"].ToString();
        string symbol = "config_analyse_detail_" + username;
        string requestAPI = "/Data/api_GetVerifySymbolExisted.aspx?cid=" + cid + "&symbol=" + symbol;
        string URL = Server_API + Virtul_Folder_API + requestAPI;
        string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
        if(returnStrDoc.Contains("err"))
        {

        }
    }
}