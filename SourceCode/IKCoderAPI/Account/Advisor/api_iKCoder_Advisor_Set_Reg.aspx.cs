using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Advisor_api_iKCoder_Advisor_Set_Reg : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string password = GetQuerystringParam("password");
        string centersymbol = GetQuerystringParam("centersymbol");
        class_Bus_Advisor objAdvisor = new class_Bus_Advisor(Object_CommonData);
        if (!objAdvisor.GetISAdvisorExisted(symbol))
        {
            objAdvisor.SetUpdateAdvisor(symbol, password, centersymbol);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_AccountExisted"), "");
            return;
        }
    }
}