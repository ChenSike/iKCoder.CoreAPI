using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_EduCenter_api_iKCoder_EduCenter_Set_Update : class_WebBase_IKCoderAPI_UA_EduCenter
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string password = GetQuerystringParam("password");
        class_Bus_EduCenter objEduCenter = new class_Bus_EduCenter(Object_CommonData);
        objEduCenter.SetUpdateCenter(symbol, password);
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");

    }
}