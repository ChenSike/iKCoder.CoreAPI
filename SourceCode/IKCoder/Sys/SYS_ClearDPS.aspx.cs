using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_SYS_ClearDPS : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        string operation = GetQuerystringParam("operation");
        if(operation == class_CommonDefined._AllowSysOperation)
        {
            Object_DomainPersistance.ClearAll();
            AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "Finished Clearing The DomianPersistance Store.", "");            
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Operation", "", enum_MessageType.Exception);
    }
}