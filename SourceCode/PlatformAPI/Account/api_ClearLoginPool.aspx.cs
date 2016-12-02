using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_api_ClearLoginPool : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (GetQuerystringParam("keyCode") == class_CommonLogic.Const_OperationQueryString)
        {
            class_LoginedPool.clearLoginedAccountPool();
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "complete executing api.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "Invalidate operation from client.", "Invalidate operation", "");

    }    
}