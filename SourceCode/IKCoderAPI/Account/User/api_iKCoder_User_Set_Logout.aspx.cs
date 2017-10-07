using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Account_User_api_iKCoder_User_Set_Logout : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;                  
        for(int i=0;i<Request.Cookies.Count;i++)
        {
            if (!Request.Cookies[i].Name.Contains("_SessionId"))
            {
                Response.Cookies[Request.Cookies[i].Name].Expires = DateTime.Now.AddSeconds(-10);
            }
        }
        Session.RemoveAll();
        Request.Cookies.Clear();
        Dictionary<String, String> attrs = new Dictionary<string, string>();
        attrs.Add("logined_marked", "0");
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
    }
}