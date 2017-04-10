using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Domain_api_RegDomain : class_WebBase_NWA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string domianname = GetQuerystringParam("domain");
        if (!string.IsNullOrEmpty(domianname))
        {
            if (Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN") != null)
            {
                string persistanceDomain = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN").ToString();
                if (persistanceDomain != domianname)
                    Object_DomainPersistance.FlushValue(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN", domianname);
            }
            else
                Object_DomainPersistance.AddSingle(Object_DomainPersistance.GetKeyName(REQUESTIP, "PlatformAPI", ClientSymbol), "RSDOMAIN", -1, domianname);
            AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
    }
}