using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_api_iKCoder_Sys_RegDomain : class_WebBase_IKCoderAPI_NUA
{
    protected override void BeforeLoad()
    {
        switchResponseMode(enumResponseMode.text);
        string domianname = GetQuerystringParam("domain");
        if (!string.IsNullOrEmpty(domianname))
        {
            string persistanceDomain = "";
            if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), "RSDOMAIN") != null)
                persistanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), "RSDOMAIN").ToString();
            if (string.IsNullOrEmpty(persistanceDomain))            
                Object_DomainPersistance.AddSingle(Object_DomainPersistance.GetKeyName(REQUESTIP), "RSDOMAIN", 60, domianname);
            else
            {
                if (persistanceDomain != domianname)
                    Object_DomainPersistance.FlushValue(Object_DomainPersistance.GetKeyName(REQUESTIP), "RSDOMAIN", domianname);
            }
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");

    }
}