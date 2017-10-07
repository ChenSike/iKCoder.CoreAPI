using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Account_EduCenter_api_iKCoder_EduCenter_Set_Reg : class_WebBase_IKCoderAPI_AO
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string password = GetQuerystringParam("password");
        class_Bus_EduCenter objEduCenter = new class_Bus_EduCenter(Object_CommonData);
        if (!objEduCenter.GetISCenterExisted(symbol))
        {
            objEduCenter.SetUpdateCenter(symbol, password);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_AccountExisted"), "");
            return;
        }
    }
}