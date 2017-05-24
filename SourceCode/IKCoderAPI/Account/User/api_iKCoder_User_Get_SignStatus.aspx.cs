using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_User_api_iKCoder_User_Get_SignStatus : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        Object_DomainPersistance.ClearBuffer();
        base.ExtendedAction();
    }
}