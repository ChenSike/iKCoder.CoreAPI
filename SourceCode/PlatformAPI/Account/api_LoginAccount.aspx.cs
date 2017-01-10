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
        string username = string.Empty;
        string password = string.Empty;
        if (usernameNode == null)
        {
            username = GetQuerystringParam("username");
        }
        else
            username = class_XmlHelper.GetNodeValue("", usernameNode);
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        if (passwordNode == null)
        {
            password = GetQuerystringParam("password");
        }
        else
            password = class_XmlHelper.GetNodeValue("", passwordNode);
        if (class_LoginedPool.verifyLoginedAccountExisted(username))
            class_LoginedPool.removeLoginedAccount(username);
        class_Data_SqlSPEntry activeSPEntry_Basic = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_Basic, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count >= 1)
        {
            DataRow activeUserRow = selectResultDT.Rows[0];
            string valueUserFromDB = string.Empty;
            string valuePasswordFromDB = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeUserRow, "name", out valueUserFromDB);
            class_Data_SqlDataHelper.GetColumnData(activeUserRow, "password", out valuePasswordFromDB);
            if (string.IsNullOrEmpty(valueUserFromDB))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad Login:username is not existed.", "");
                object_CommonLogic.CloseDBConnection();
                return;
            }
            else
            {
                if (password == valuePasswordFromDB)
                {
                    string strId = string.Empty;
                    string newGuid = Guid.NewGuid().ToString();
                    class_Data_SqlDataHelper.GetColumnData(activeUserRow, "id", out strId);
                    class_AccountItem newLoginedAccount = new class_AccountItem();
                    newLoginedAccount.UserNmae = username;
                    newLoginedAccount.UserID = strId;
                    newLoginedAccount.LoginedID = newGuid;
                    newLoginedAccount.LastRequestIP = Request.UserHostAddress.ToString();
                    newLoginedAccount.LastLoginedTime = DateTime.Now;
                    newLoginedAccount.ExperiedPeriod = class_CommonDefined.ExperiedPeriodOfLoginedAccount;
                    class_LoginedPool.insertNewLoginAccountItem(newLoginedAccount);
                    HttpCookie newLoginIDCookie = new HttpCookie("loginedID");
                    newLoginIDCookie.Value = newGuid;
                    Response.Cookies.Add(newLoginIDCookie);
                    Dictionary<string, string> returnDoc = new Dictionary<string, string>();
                    returnDoc.Add("logined_username", newLoginedAccount.UserNmae);
                    returnDoc.Add("logined_userid", newLoginedAccount.UserID);
                    returnDoc.Add("logined_loginid", newLoginedAccount.LoginedID);
                    returnDoc.Add("logined_loginedtime", newLoginedAccount.LastLoginedTime.ToString());
                    returnDoc.Add("logined_experied", newLoginedAccount.ExperiedPeriod.ToString());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), returnDoc);
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Bad Login:user info is not matched.", "");
            }

        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select.", "");
        object_CommonLogic.CloseDBConnection();
    }
}