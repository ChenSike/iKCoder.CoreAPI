using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Account_api_ChangePassword : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        ISBINRESPONSE = false;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        XmlNode newpasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/newpassword");
        XmlNode oldpasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/oldpassword");
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string newpassword = class_XmlHelper.GetNodeValue("", newpasswordNode);
        string oldpassword = class_XmlHelper.GetNodeValue("", oldpasswordNode);
        string desNewPassword = string.Empty;
        string desOldPassword = string.Empty;
        object_CommonLogic.Object_DES.DESCoding(newpassword, out desNewPassword);
        object_CommonLogic.Object_DES.DESCoding(oldpassword, out desOldPassword);
        class_Data_SqlSPEntry activeSPEntry_Basic = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        activeSPEntry_Basic.ClearAllParamsValues();
        activeSPEntry_Basic.ModifyParameterValue("@username", username);
        activeSPEntry_Basic.ModifyParameterValue("@password", desOldPassword);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry_Basic, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count >= 1)
        {
            string userID = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(selectResultDT.Rows[0], "id", out userID);
            activeSPEntry_Basic.ClearAllParamsValues();
            activeSPEntry_Basic.ModifyParameterValue("@id", userID);
            activeSPEntry_Basic.ModifyParameterValue("@password", desNewPassword);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_Basic, "update", this.GetType());
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : error password.", "");
    }
        
}