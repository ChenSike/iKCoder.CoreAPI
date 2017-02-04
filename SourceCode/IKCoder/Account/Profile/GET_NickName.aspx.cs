﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Account_Profile_GET_NickName : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        string account = GetQuerystringParam("nickname");
        if (string.IsNullOrEmpty(account))
            account = Session["logined_user_name"].ToString();
        if (!string.IsNullOrEmpty(account))
        {
            string requestAPI = "/Profile/api_AccountProfile_SelectNodeValue.aspx?cid=" + cid + "&account=" + account + "&produce=" + Produce_Name + "&xpath=/root/usrbasic/usr_nickname";
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            if (Object_NetRemote.getRemoteRequestByGet(requestAPI))
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty account", "");
    }
}