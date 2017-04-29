using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Profile_api_AccountProfile_SelectNodeValue : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        string accountName = GetQuerystringParam("account");
        string xpathFrom = GetQuerystringParam("xpath");
        if (string.IsNullOrEmpty(accountName))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/account") != null)
                    accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/account"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for select profile.", "");
                    return;
                }

            }
        }        
        string profileSymbol = class_CommonName.GetProfileName(accountName);
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null)
        {
            if (activeAccountProfileDataTable.Rows.Count > 0)
            {
                string strDoc = "";
                string data = "";
                class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out strDoc);
                try
                {
                    XmlDocument resultDoc = new XmlDocument();
                    resultDoc.LoadXml(strDoc);
                    XmlNode selectedNode = resultDoc.SelectSingleNode(xpathFrom);
                    if(selectedNode!=null)
                    {
                        data = class_XmlHelper.GetNodeValue(selectedNode);
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), data, "");
                    }
                    else
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
                    }
                }
                catch
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidate document.", "");
                }                
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
    }
}