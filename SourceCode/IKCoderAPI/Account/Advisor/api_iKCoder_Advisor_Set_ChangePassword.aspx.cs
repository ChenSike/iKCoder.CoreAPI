using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;


public partial class Account_Advisor_api_iKCoder_Advisor_Set_ChangePassword : class_WebBase_IKCoderAPI_UA_Advisor
{
    protected override void ExtendedAction()
    {
        string symbol = logined_user_name;
        string password = GetQuerystringParam("password");
        class_Bus_Advisor objAdvisor = new class_Bus_Advisor(Object_CommonData);
        if (!objAdvisor.GetISAdvisorExisted(symbol))
        {
            objAdvisor.SetUpdateAdvisor(symbol, password, string.Empty);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_AccountExisted"), "");
            return;
        }
    }
}