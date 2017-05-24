using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;


public partial class Bus_Share_api_iKCoder_Share_Clear : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TMPSENCES);
        activeSPEntry.ClearAllParamsValues();
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (textDataTable != null)
        {
            foreach (DataRow activeDataRow in textDataTable.Rows)
            {
                string str_createdtime = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "createdtime", out str_createdtime);
                DateTime dt_createdtime = DateTime.Parse(str_createdtime);
                string id = string.Empty;
                class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                if ((dt_createdtime-DateTime.Now).Days>7)
                {
                    activeSPEntry.ClearAllParamsValues();
                    activeSPEntry.ModifyParameterValue("@id", id);
                    Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                }              
            }
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true","");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "data->empty", "");
            return;
        }

    }
}