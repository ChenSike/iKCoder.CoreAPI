using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_TeachingMan_api_iKCoder_TeachingMan_Set_Sign : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string logined_mark = class_CommonDefined.enumLoginedMark.mark_tm.ToString();
        string keyLicence = string.Empty;
        string symbol = "";
        string password = "";
        symbol = GetQuerystringParam("symbol");
        password = GetQuerystringParam("password");
        keyLicence = GetQuerystringParam("licence");
        Session.Clear();
        if (string.IsNullOrEmpty(symbol))
        {
            XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            XmlNode licenceNode = REQUESTDOCUMENT.SelectSingleNode("/root/licence");
            symbol = class_XmlHelper.GetNodeValue(symbolNode);
            password = class_XmlHelper.GetNodeValue(passwordNode);
            keyLicence = class_XmlHelper.GetNodeValue(licenceNode);
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
        if (string.IsNullOrEmpty(keyLicence))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Sign_Licence_Empty"), "");
            return;
        }
        bool isLogined = true;
        class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
        string licenceid = objectLicences.GetLicenceID(keyLicence);
        if (string.IsNullOrEmpty(licenceid))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Sign_Licence_Empty"), "");
            return;
        }
        class_Bus_Teacher objTeacher = new class_Bus_Teacher(Object_CommonData);
        if (objTeacher.GetCheckedAccountTeacher(symbol, password, keyLicence))
        {
            isLogined = true;
        }
        else
        {
            isLogined = false;
        }
        if (isLogined)
        {
            string user_name = symbol;
            string user_id = objTeacher.GetTeacherID(symbol);
            string user_loginid = Guid.NewGuid().ToString();
            Session["logined_user_name"] = user_name;
            Session["logined_user_id"] = user_id;
            Session["logined_user_signedid"] = user_loginid;
            Session["logined_marked"] = logined_mark;
            Session.Timeout = Session_TimeOutMinutes;
            Response.Cookies["logined_marked"].Value = logined_mark;
            Response.Cookies["logined_marked"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_name"].Value = user_name;
            Response.Cookies["logined_user_name"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_id"].Value = user_id;
            Response.Cookies["logined_user_id"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_signedid"].Value = user_loginid;
            Response.Cookies["logined_user_signedid"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Response.Cookies["logined_user_licence"].Value = keyLicence;
            Response.Cookies["logined_user_licence"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("logined_user_name", user_name);
            attrs.Add("logined_marked", logined_mark);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Sign_UnSigned"), "");
        }
    }
}