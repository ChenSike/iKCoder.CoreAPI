using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// class_WebBase_UA 的摘要说明
/// </summary>
public class class_WebBase_UA:class_WebBase
{
    protected override void BeforeExtenedAction()
    {
        ISRESPONSEDOC = true;
        if (Request.Cookies["logined_marked"] != null && Request.Cookies["logined_user_signedid"]!=null)
        {
            string logined_marked = Request.Cookies["logined_marked"].Value;
            string logined_user_loginid = Request.Cookies["logined_user_signedid"].Value;
            //if(Session["logined_user_id"])
        }
        else
        {

        }
        /*Session["logined_user_nmae"] = user_name;
        Session["logined_user_id"] = user_id;
        Session["logined_user_signedid"] = user_loginid;
        Session.Timeout = Session_TimeOutMinutes;
        Response.Cookies["logined_marked"].Value = "1";
        Response.Cookies["logined_marked"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
        Response.Cookies["logined_user_id"].Value = user_id;
        Response.Cookies["logined_user_id"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);
        Response.Cookies["logined_user_signedid"].Value = user_loginid;
        Response.Cookies["logined_user_signedid"].Expires = DateTime.Now.AddHours(Cookie_TimeOutHour);*/
    }
}