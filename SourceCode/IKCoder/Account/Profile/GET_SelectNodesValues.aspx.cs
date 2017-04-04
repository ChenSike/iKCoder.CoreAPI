using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Account_Profile_GET_SelectNodesValues : class_WebBase_UA
{
    protected override void ExtendedAction()
    {
        string account = GetQuerystringParam("account");
        if(string.IsNullOrEmpty(account))        
            account = Session["logined_user_name"].ToString();
                
    }
}