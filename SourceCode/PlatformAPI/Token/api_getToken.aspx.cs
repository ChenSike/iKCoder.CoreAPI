using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Token_api_getToken : class_WebBase_NWA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC=true;
        PlatformAPICodeBehind.Token.class_ProductToken _objectLogicForToken = new PlatformAPICodeBehind.Token.class_ProductToken();
        string result = _objectLogicForToken.RegistryToken(Session, REQUESTDOCUMENT, object_CommonLogic.productBenchmarkNodes);
        if (result == "")
            AddErrMessageToResponseDOC("BadToken", "Your token is invalidated", "");
        else
            AddResponseMessageToResponseDOC("Token", result, result, "");
                    
    }
}