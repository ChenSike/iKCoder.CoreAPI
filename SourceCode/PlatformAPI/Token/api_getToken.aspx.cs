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
        class_ProductToken _objectLogicForToken = new class_ProductToken();
        object_CommonLogic.Object_BaseConfig.SwitchToDESModeON(class_CommonLogic.Const_DESKey);
        string result = _objectLogicForToken.RegistryToken(Session, REQUESTDOCUMENT, object_CommonLogic.productBenchmarkNodes,object_CommonLogic.Object_BaseConfig);
        if (result == "")
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_getToken", "Your token is invalidated", "");
        else
        {
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "api_getToken", class_CommonDefined.enumExecutedCode.executed.ToString(), result, "");
            HttpCookie tokenCookie = new HttpCookie("token", result);
            Response.Cookies.Add(tokenCookie);
        }
                    
    }
}