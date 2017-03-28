using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_GET_SignWithCheckCode : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        string symbol = "";
        string password = "";
        string codeName = "";
        string codeValue = "";
        symbol = GetQuerystringParam("symbol");
        password = GetQuerystringParam("password");
        XmlNode codeNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/codename");
        XmlNode codeValueNode = REQUESTDOCUMENT.SelectSingleNode("/root/codevalue");
        if (string.IsNullOrEmpty(symbol))
        {
            XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            symbol = class_XmlHelper.GetNodeValue(symbolNode);
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

        codeValue = class_XmlHelper.GetNodeValue(codeValueNode);
        if (string.IsNullOrEmpty(codeValue))
        {
            codeValue = GetQuerystringParam("codevalue");
        }
        if (string.IsNullOrEmpty(codeValue))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Checkcode"), "");
            return;
        }

        codeName = class_XmlHelper.GetNodeValue(codeNameNode);
        if (string.IsNullOrEmpty(codeName))
        {
            codeName = GetQuerystringParam("codename");
        }
        if (string.IsNullOrEmpty(codeName))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_CodeName"), "");
            return;
        }

        string codeValueFromServer = "";
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), codeName) != null)
            codeValueFromServer = (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), codeName)).ToString();
        if (codeValueFromServer != codeValue)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Equal_Param_Reg_Code"), "");
            return;
        }
        XmlDocument returnFromAPIServer = new XmlDocument();
        string inputDoc = "<root><username>" + symbol + "</username><password>" + password + "</password></root>";
        string URL = Server_API + Virtul_Folder_API + "/Account/api_LoginAccount.aspx?cid=" + cid;
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
            Response.Cookies["logined_marked"].Value = "1";
            Response.Cookies["logined_marked"].Value = "1";
            Response.Cookies["logined_marked"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_id"].Value = user_id;
            Response.Cookies["logined_user_id"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_signedid"].Value = user_loginid;
            Response.Cookies["logined_user_signedid"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Dictionary<string, string> attrsValue = new Dictionary<string, string>();
            attrsValue.Add("logined_user_name", user_name);
            attrsValue.Add("logined_marked", "1");
            AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrsValue);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Sign_UnSigned"), "");
        }

    }
}