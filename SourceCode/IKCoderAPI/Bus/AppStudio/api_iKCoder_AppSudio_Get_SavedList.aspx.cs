using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_AppStudio_api_iKCoder_AppSudio_Get_SavedList : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        class_Data_SqlSPEntry activeSPEntry_resourceAppstudio = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
        activeSPEntry_resourceAppstudio.ClearAllParamsValues();
        activeSPEntry_resourceAppstudio.ModifyParameterValue("@owner", logined_user_name);
        DataTable dtResourceAppStudio = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_resourceAppstudio, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if(dtResourceAppStudio!=null && dtResourceAppStudio.Rows.Count>0)
        {
            foreach(DataRow activeDataRow in dtResourceAppStudio.Rows)
            {
                string symbol = string.Empty;
                string modified = string.Empty;
                string base64Data = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "symbol", out symbol);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "modified", out modified);
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "data", out base64Data);
                string contentData = class_CommonUtil.Decoder_Base64(base64Data);
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("symbol", symbol);
                attrs.Add("modified", modified);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No data", "");
        }
    }
}