using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_User_api_iKCoder_User_Set_SignWithCheckCode : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
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
        if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), codeName) != null)
            codeValueFromServer = (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), codeName)).ToString();
        if (codeValueFromServer != codeValue)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Equal_Param_Reg_Code"), "");
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
            Response.Cookies["logined_marked"].Value = "1";
            Response.Cookies["logined_marked"].Value = "1";
            Response.Cookies["logined_marked"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_name"].Value = user_name;
            Response.Cookies["logined_user_name"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_id"].Value = user_id;
            Response.Cookies["logined_user_id"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_signedid"].Value = user_loginid;
            Response.Cookies["logined_user_signedid"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("logined_user_name", user_name);
            attrs.Add("logined_marked", "1");
            Object_CommonData.PrepareDataOperation();
            Object_ProfileDocs.VerifyAll(user_name);
            string value_nickname = Object_ProfileDocs.GetDocNoteValue(user_name, class_CommonDefined.enumProfileDoc.doc_basic, "/usrbasic/usr_nickname");            
            Session["logined_user_nickname"] = value_nickname;
            Response.Cookies["logined_user_nickname"].Value = value_nickname;
            attrs.Add("logined_user_nickname", value_nickname); 
            List<string> finishedSencesSymbols = Object_ProfileDocs.GetFinishedSymbols(user_name);
            XmlDocument sourceDoc_StudyStatus = Object_ProfileDocs.GetProfileDocObject(user_name, class_CommonDefined.enumProfileDoc.doc_studystatus);
            class_Bus_Exp ObjectEXP = new class_Bus_Exp(Object_CommonData, finishedSencesSymbols);
            double totalExp = ObjectEXP.Get_UserTotalExp();
            Object_ProfileDocs.SetTotalExp(user_name, totalExp);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Sign_UnSigned"), "");
        }
    }
}