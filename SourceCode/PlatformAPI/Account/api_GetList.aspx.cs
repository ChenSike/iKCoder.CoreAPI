using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Account_api_GetList : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);     
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry_Basic = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");
        activeSPEntry_Basic.ClearAllParamsValues();        
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_Basic, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count >= 1)
        {
            foreach (DataRow activeUserRow in selectResultDT.Rows)
            {
                string valueUserFromDB = string.Empty;
                string valuePasswordFromDB = string.Empty;
                string valueIDFromDB = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeUserRow, "username", out valueUserFromDB);
                class_Data_SqlDataHelper.GetColumnData(activeUserRow, "password", out valuePasswordFromDB);
                class_Data_SqlDataHelper.GetColumnData(activeUserRow, "id", out valueIDFromDB);
                Dictionary<string, string> returnDoc = new Dictionary<string, string>();
                returnDoc.Add("id", valueIDFromDB);
                returnDoc.Add("name", valueUserFromDB);
                returnDoc.Add("password", valuePasswordFromDB);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), returnDoc);
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty document.", "");
        }
}