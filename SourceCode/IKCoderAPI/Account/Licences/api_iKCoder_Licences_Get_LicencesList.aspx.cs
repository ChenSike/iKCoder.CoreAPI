using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Account_Licences_api_iKCoder_Licences_Get_LicencesList : class_WebBase_IKCoderAPI_AO
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switchResponseMode(enumResponseMode.text);
        class_Bus_Licences objectLicences = new class_Bus_Licences(Object_CommonData);
        Dictionary<string, Dictionary<string, string>> result = objectLicences.GetAllList();
        foreach (string id in result.Keys)
        {
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), result[id]);
        }
    }
}