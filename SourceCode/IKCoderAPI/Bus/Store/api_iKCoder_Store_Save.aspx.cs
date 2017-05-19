using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;


public partial class Bus_Store_api_iKCoder_Store_Save : class_WebBase_IKCoderAPI_NUA
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
        Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), tmpDataSymbol, iTimeout, ISTEXTREQUEST ? REQUESTCONTENT : REQUESTDOCUMENT.OuterXml);
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");

    }
}