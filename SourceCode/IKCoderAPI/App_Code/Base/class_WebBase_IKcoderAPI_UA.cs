using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// Summary description for class_WebBase_IKcoderAPI_WA
/// </summary>
public class class_WebBase_IKCoderAPI_UA:class_WebBase_IKCoderAPI
{
    protected string logined_user_id;
    protected string logined_user_name;

    protected override bool BeforeExtenedAction()
    {
        switchResponseMode(enumResponseMode.text);        
        if (Request.Cookies["logined_marked"] != null && Request.Cookies["logined_user_signedid"] != null && Request.Cookies["logined_user_id"] != null && Session["logined_user_name"] != null)
        {
            string logined_marked = Request.Cookies["logined_marked"].Value;
            string logined_user_loginid = Request.Cookies["logined_user_signedid"].Value;
            logined_user_id = Request.Cookies["logined_user_id"].Value;
            logined_user_name = Session["logined_user_name"].ToString();
            if (Session["logined_user_id"].ToString() == logined_user_id)
            {
                if (Session["logined_user_signedid"].ToString() == logined_user_loginid)
                {
                    if (Session["logined_marked"].ToString() == "1")
                    {
                        Dictionary<String, String> attrs = new Dictionary<string, string>();
                        attrs.Add("logined_user_name", Session["logined_user_name"].ToString());
                        attrs.Add("logined_marked", "1");
                        attrs.Add("type", "1");
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
                        return true;
                    }
                    else
                    {
                        Response.Cookies.Clear();
                        Session.Clear();
                        Dictionary<string, string> errAttrs = new Dictionary<string, string>();
                        errAttrs.Add("issigndneeded", "1");
                        errAttrs.Add("type", "1");
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Unsigned"), errAttrs);
                        return false;
                    }
                }
                else
                {
                    Response.Cookies.Clear();
                    Session.Clear();
                    Dictionary<string, string> errAttrs = new Dictionary<string, string>();
                    errAttrs.Add("issigndneeded", "1");
                    errAttrs.Add("type", "1");
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Unsigned"), errAttrs);
                    return false;
                }
            }
            else
            {
                Response.Cookies.Clear();
                Session.Clear();
                Dictionary<string, string> errAttrs = new Dictionary<string, string>();
                errAttrs.Add("issigndneeded", "1");
                errAttrs.Add("type", "1");
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Unsigned"), errAttrs);
                return false;
            }
        }
        else
        {
            Response.Cookies.Clear();
            Session.Clear();
            Dictionary<string, string> errAttrs = new Dictionary<string, string>();
            errAttrs.Add("issigndneeded", "1");
            errAttrs.Add("type", "1");
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Msg_Unsigned"), "");
            return false;
        }
    }

}