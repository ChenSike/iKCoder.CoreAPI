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
            if(GetSessionValue("RSDOMAIN")!=null)
            {
                string persistanceDomain = GetSessionValue("RSDOMAIN").ToString();
                if (persistanceDomain != domianname)
                    Session["RSDOMAIN"] = domianname;
            }
            else
            {
                Session["RSDOMAIN"] = domianname;
            }
            AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
    }
}