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
        class_Data_SqlSPEntry activeSPEntry_Basic = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_Basic");
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
                    string newGuid = Guid.NewGuid().ToString();
                    decimal id = 0;
                    string strId = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeUserRow, "id", out strId);
                    decimal.TryParse(strId, out id);
                    class_Data_SqlSPEntry activeSPEntry_userLoginInfo = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_LoginInfo");
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@username", username);
                    DataTable dtLoginInfo = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_userLoginInfo, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                    activeSPEntry_userLoginInfo.ClearAllParamsValues();
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@username", username);
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@regtime", DateTime.Now.ToString());
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@requestip", REQUESTIP);
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@loginguid", newGuid);
                    activeSPEntry_userLoginInfo.ModifyParameterValue("@userid", id);
                    if (dtLoginInfo == null || dtLoginInfo.Rows.Count == 0)
                        object_CommonLogic.Object_SqlHelper.ExecuteInsertSP(activeSPEntry_userLoginInfo, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                    else
                    {
                        decimal loginid = 0;
                        string strLoginId = string.Empty;
                        class_Data_SqlDataHelper.GetColumnData(dtLoginInfo.Rows[0], "id", out strLoginId);
                        decimal.TryParse(strLoginId, out loginid);
                        activeSPEntry_userLoginInfo.ModifyParameterValue("@id", loginid);
                        object_CommonLogic.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry_userLoginInfo, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                    }
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), newGuid, "");
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