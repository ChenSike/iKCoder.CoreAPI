using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_Store_api_iKCoder_Store_Load : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        string type = GetQuerystringParam("type");
        string tmpDataSymbol = symbol + "_" + type + "_" + ClientSymbol;
        string content = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), tmpDataSymbol).ToString();
        try
        {
            RESPONSEDOCUMENT.LoadXml(content);
        }
        catch
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "response document->empty", "");
            return;
        }
    }
}