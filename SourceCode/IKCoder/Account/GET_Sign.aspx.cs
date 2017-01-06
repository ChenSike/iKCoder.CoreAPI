using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Account_GET_Sign : class_WebBase
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        if(REQUESTDOCUMENT!=null)
        {
            string symbol = "";
            string password = "";
            symbol = GetQuerystringParam("symbol");
            password = GetQuerystringParam("password");
            if(string.IsNullOrEmpty(symbol))
            {
                XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
                XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
                symbol = class_XmlHelper.GetNodeValue(symbolNode);
                password = class_XmlHelper.GetNodeValue(passwordNode);
            }
            //if(string.IsNullOrEmpty())
        }
        else        
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Input Document", "", enum_MessageType.Exception);
        
    }
}