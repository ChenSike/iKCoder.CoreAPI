using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_AppStudio_api_iKCoder_AppSudio_Get_Load : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        if (string.IsNullOrEmpty(symbol))
        {
            class_Data_SqlSPEntry activeSPEntry_resourceAppstudio = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
            activeSPEntry_resourceAppstudio.ClearAllParamsValues();
            activeSPEntry_resourceAppstudio.ModifyParameterValue("@owner", logined_user_name);
            activeSPEntry_resourceAppstudio.ModifyParameterValue("@symbol", symbol);
            DataTable dtResourceAppStudio = Object_CommonData.Object_SqlHelper.ExecuteSelectSPMixedConditionsForDT(activeSPEntry_resourceAppstudio, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
            if (dtResourceAppStudio != null && dtResourceAppStudio.Rows.Count > 0)
            {
                DataRow activeDataRow = dtResourceAppStudio.Rows[0];
                string modified = string.Empty;
                string base64Data = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "modified", out modified);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "data", out base64Data);
                string contentData = class_CommonUtil.Decoder_Base64(base64Data);
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("symbol", symbol);
                attrs.Add("modified", modified);
                attrs.Add("data", contentData);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No data for user", "");
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No symbol", "");
        }
    }
}