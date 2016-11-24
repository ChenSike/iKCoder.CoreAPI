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
            object_CommonLogic.CloseDBConnection();
            return;
        }
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        if (passwordNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad request document.No password node.", "");
            object_CommonLogic.CloseDBConnection();
            return;
        }
        string username = class_XmlHelper.GetNodeValue("", usernameNode);        
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_Basic");
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count>=1)
        {
            DataRow activeUserRow = selectResultDT.Rows[0];
            string valueUserFromDB = string.Empty;
            string valuePasswordFromDB = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeUserRow, "name", out valueUserFromDB);
            class_Data_SqlDataHelper.GetColumnData(activeUserRow,"password",out valuePasswordFromDB);
            if (string.IsNullOrEmpty(valueUserFromDB))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad Login:username is not existed.", "");
                object_CommonLogic.CloseDBConnection();
                return;
            }
            else
            {
                string password = class_XmlHelper.GetNodeValue("", passwordNode);
                if(password == valuePasswordFromDB)                
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "user checked", "");                
                else                
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad Login:user info is not matched.", "");                
            }
        }
        else           
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select.", "");
        object_CommonLogic.CloseDBConnection();
    }
}