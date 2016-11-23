using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Account_api_LoginAccount : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        if (usernameNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad request document.No username node.", "");
            return;
        }
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        if (passwordNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "this.GetType().FullName", "Bad request document.No password node.", "");
            return;
        }
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_Basic");
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count==1)
        {
            
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select.", "");
        object_CommonLogic.CloseDBConnection();
    }
}