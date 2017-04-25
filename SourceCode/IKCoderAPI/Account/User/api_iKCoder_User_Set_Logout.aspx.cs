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
        Session.RemoveAll();
        Response.Cookies.Clear();
        Dictionary<String, String> attrs = new Dictionary<string, string>();
        attrs.Add("logined_marked", "0");
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
    }
}