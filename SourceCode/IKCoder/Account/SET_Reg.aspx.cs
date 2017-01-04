using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Account_SET_Reg : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        if (REQUESTDOCUMENT != null)
        {
            string userSymbol = "";
            string userPassword = "";
            string codeName = "";
            string codeValue = "";
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");            
            XmlNode userPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            XmlNode codeNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/codename");
            XmlNode codeValueNode = REQUESTDOCUMENT.SelectSingleNode("/root/codevalue");
            if (userSymbolNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty Parameter->symbol", "");
                return;
            }
            if(userPasswordNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty Parameter->password", "");
                return;
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Input Document", "");
        
    }
}