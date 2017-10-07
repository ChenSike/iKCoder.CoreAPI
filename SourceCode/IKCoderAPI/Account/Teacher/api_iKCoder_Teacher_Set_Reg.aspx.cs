using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Account_Teacher_api_iKCoder_Teacher_Set_Reg : class_WebBase_IKCoderAPI_AO
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string password = GetQuerystringParam("password");
        string centersymbol = GetQuerystringParam("centersymbol");
        class_Bus_Teacher objTeacher = new class_Bus_Teacher(Object_CommonData);
        if (!objTeacher.GetISTeacherExisted(symbol))
        {
            objTeacher.SetUpdateTeacher(symbol, password, centersymbol);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "ERR_Param_Reg_AccountExisted"), "");
            return;
        }
    }
}