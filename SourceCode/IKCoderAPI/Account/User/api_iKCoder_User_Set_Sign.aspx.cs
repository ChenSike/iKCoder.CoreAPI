﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_User_api_iKCoder_User_Set_Sign : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = "";
        string password = "";
        symbol = GetQuerystringParam("symbol");
        password = GetQuerystringParam("password");
        if (string.IsNullOrEmpty(symbol))
        {
            XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            symbol = class_XmlHelper.GetNodeValue(symbolNode);
        }
        if (string.IsNullOrEmpty(password))
        {
            XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            password = class_XmlHelper.GetNodeValue(passwordNode);
        }        
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Sign_Symbol"), "");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Sign_Password"), "");
            return;
        }
        XmlDocument returnFromAPIServer = new XmlDocument();
        string inputDoc = "<root><username>" + symbol + "</username><password>" + password + "</password></root>";
        string URL = Server_API + Virtul_Folder_API + "/Account/api_LoginAccount.aspx";
        string strReturnDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(inputDoc, URL, 1000 * 60, 100000);
        returnFromAPIServer.LoadXml(strReturnDoc);
        XmlNode msgNode = returnFromAPIServer.SelectSingleNode("/root/msg");
        bool isLogined = true;
        if (msgNode == null)
        {
            isLogined = false;
        }
        if (isLogined)
        {
            string user_name = class_XmlHelper.GetAttrValue(msgNode, "logined_username");
            string user_id = class_XmlHelper.GetAttrValue(msgNode, "logined_userid");
            string user_loginid = class_XmlHelper.GetAttrValue(msgNode, "logined_loginid");
            Session["logined_user_name"] = user_name;
            Session["logined_user_id"] = user_id;
            Session["logined_user_signedid"] = user_loginid;
            Session["logined_marked"] = "1";
            Session.Timeout = Session_TimeOutMinutes;
            Response.Cookies["logined_user_name"].Value = user_name;
            Response.Cookies["logined_user_name"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_marked"].Value = "1";
            Response.Cookies["logined_marked"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_id"].Value = user_id;
            Response.Cookies["logined_user_id"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_signedid"].Value = user_loginid;
            Response.Cookies["logined_user_signedid"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            string requestAPI = "/Profile/api_AccountProfile_SelectNodeValue.aspx?account=" + user_name + "&produce=" + Produce_Name + "&xpath=/root/usrbasic/usr_nickname";
            URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            if (!returnStrDoc.Contains("err"))
            {
                XmlDocument returnDoc = new XmlDocument();
                returnDoc.LoadXml(returnStrDoc);
                XmlNode msgNod = returnDoc.SelectSingleNode("/root/msg");
                string msg = class_XmlHelper.GetAttrValue(msgNod, "msg");
                Session["logined_user_nickname"] = msg;
                Response.Cookies["logined_user_nickname"].Value = msg;
                Dictionary<String, String> attrs = new Dictionary<string, string>();
                attrs.Add("logined_user_nickname", msg);
                attrs.Add("logined_user_name", user_name);
                attrs.Add("logined_marked", "1");
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Sign_UnSigned"), "");
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Sign_UnSigned"), "");
        }
    }
}