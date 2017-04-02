using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Buss_api_AccountProfile_Update : class_WebClass_WLA
{
    protected override void AfterExtenedFunction()
    {
        string accountName = GetQuerystringParam("account");
        string produceName = GetQuerystringParam("produce");
        string profileData = "";
        if (string.IsNullOrEmpty(accountName))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/account") != null)
                    accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/account"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for updating profile.", "");
                    return;
                }

            }
        }
        if (string.IsNullOrEmpty(produceName))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/produce") != null)
                    accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/produce"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated produce name for updating profile.", "");
                    return;
                }

            }
        }
        if(REQUESTDOCUMENT!=null)
        {
            if(REQUESTDOCUMENT.SelectSingleNode("/root/data")!=null)
                profileData = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/data"));
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : empty profile doc for updating profile.", "");
                return;
            }
        }
        string profileSymbol = "profile_" + accountName;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
        activeSPEntry.ModifyParameterValue("@profile_product", produceName);        
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null)
        {
            if (activeAccountProfileDataTable.Rows.Count > 0)
            {
                string id = "";
                class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "id", out id);
                activeSPEntry.ClearAllParamsValues();
                activeSPEntry.ModifyParameterValue("@id", id);
                profileData = class_CommonUtil.Decoder_Base64(profileData);
                activeSPEntry.ModifyParameterValue("@profile_data", profileData);
                object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
    }

    public void degMethod(XmlNode activeNode)
    {

    }

}