using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Profile_api_AccountProfile_UpdateBySymbol : class_WebClass_WLA
{
    protected override void ExtenedFunction()
    {
        string symbol = GetQuerystringParam("symbol");
        string profileData = "";
        if (string.IsNullOrEmpty(symbol))
        {
            if (REQUESTDOCUMENT != null)
            {
                if (REQUESTDOCUMENT.SelectSingleNode("/root/symbol") != null)
                    symbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/symbol"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for updating profile.", "");
                    return;
                }

            }
        }
        if (REQUESTDOCUMENT != null)
        {
            if (REQUESTDOCUMENT.SelectSingleNode("/root/data") != null)
            {
                profileData = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/data"));
                profileData = class_CommonUtil.Decoder_Base64(profileData);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : empty profile doc for updating profile.", "");
                return;
            }
        }        
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
        activeSPEntry.ModifyParameterValue("@profile_name", symbol);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
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
}