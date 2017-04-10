using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_SYS_RegServer : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        string domianname = GetQuerystringParam("domain");
        if(!string.IsNullOrEmpty(domianname))
        {
            string persistanceDomain = "";
            if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN") != null)
                persistanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN").ToString();
            if(string.IsNullOrEmpty(persistanceDomain))
                Object_DomainPersistance.AddSingle(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN", -1, domianname);
            else
            {
                if (persistanceDomain != domianname)
                    Object_DomainPersistance.FlushValue(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), "RSDOMAIN", domianname);
            }
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");

    }
}
 



