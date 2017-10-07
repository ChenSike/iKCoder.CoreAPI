using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// Summary description for class_WebBase_IKCoderAPI_AO
/// </summary>
public class class_WebBase_IKCoderAPI_AO: class_WebBase_IKCoderAPI
{
    protected override bool BeforeExtenedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string password = GetQuerystringParam("password");
        class_Bus_AccountAdmin adminObject = new class_Bus_AccountAdmin(ref Object_CommonData);
        if (adminObject.GetCheckedAccountAdmin(symbol, password))
            return true;
        else
            return false;
    }
}