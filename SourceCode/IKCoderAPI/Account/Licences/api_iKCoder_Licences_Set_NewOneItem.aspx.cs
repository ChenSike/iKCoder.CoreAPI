using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Account_Licences_api_iKCoder_Licences_Set_NewOneItem : class_WebBase_IKCoderAPI_AO
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string type = GetQuerystringParam("type");
        if (string.IsNullOrEmpty(type))
            type = "0";
        class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
        if (objectLicences.SetCreateNewLicence(type))
        {
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false.", "", enum_MessageType.Exception);
        }
    }
}