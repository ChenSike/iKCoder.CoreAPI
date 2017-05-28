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
        string timeout = GetQuerystringParam("timeout");
        int iTimeout = 120;
        int.TryParse(timeout, out iTimeout);
        if (string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
            return;
        }
        string tmpDataSymbol = symbol + "_" + type + "_" + ClientSymbol;
        string content = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), tmpDataSymbol).ToString();
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), content, "");


    }
}