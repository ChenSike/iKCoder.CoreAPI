using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class System_api_ClearDomainPersistance : class_WebBase_NWA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (GetQuerystringParam("keyCode") == class_CommonLogic.Const_OperationQueryString)
        {
            Object_DomainPersistance.ClearBuffer();
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Invalidate operation from client.", "Invalidate operation", "");
    }
}