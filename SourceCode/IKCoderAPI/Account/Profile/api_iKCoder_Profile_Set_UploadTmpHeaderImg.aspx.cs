using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;
using System.Xml;
using System.Data;
using System.Drawing;

public partial class Account_Profile_api_iKCoder_Profile_UploadTmpHeaderImg : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        HttpFileCollection requestFiles = Request.Files;        
        HttpPostedFile postFile = requestFiles[0];        
        if (postFile != null)
        {
            string symbol = "img_header_tmp_" + logined_user_name;
            string[] filenameInfos = postFile.FileName.Split('.');
            string filetype = filenameInfos.Length >= 2 ? filenameInfos[filenameInfos.Length - 1] : "";            
            byte[] binBuffer = new byte[postFile.InputStream.Length];
            postFile.InputStream.Read(binBuffer, 0, (int)postFile.InputStream.Length);
            if (binBuffer.Length >= 1024 * 300)
            {
                Bitmap tmpBitMap = class_Util_DrawingServices.CreateImage(binBuffer);
                Bitmap compressImage = class_Util_DrawingServices.CompressImage(tmpBitMap, 10, filetype);
                binBuffer = class_Util_DrawingServices.GetBytesFromBitmap(compressImage, filetype);
            }
            if (binBuffer.Length > 0)
            {
                Object_CommonData.PrepareDataOperation();
                class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
                string userHeaderSymbol = "img_header_" + logined_user_name;
                Object_CommonData.PrepareDataOperation();
                activeSPEntry_binData.ClearAllParamsValues();
                activeSPEntry_binData.ModifyParameterValue("@symbol", userHeaderSymbol);
                DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if (binDataTable != null && binDataTable.Rows.Count > 0)
                {
                    DataRow activeDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                    string userHeaderID = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out userHeaderID);
                    activeSPEntry_binData.ClearAllParamsValues();
                    activeSPEntry_binData.ModifyParameterValue("@id", userHeaderID);
                    Object_CommonData.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                }
                activeSPEntry_binData.ClearAllParamsValues();
                activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if (binDataTable.Rows.Count == 0)
                {
                    activeSPEntry_binData.ClearAllParamsValues();
                    activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                    activeSPEntry_binData.ModifyParameterValue("@type", filetype);
                    activeSPEntry_binData.ModifyParameterValue("@owner", logined_user_name);
                    activeSPEntry_binData.ModifyParameterValue("@data", binBuffer, binBuffer.Length);
                    activeSPEntry_binData.ModifyParameterValue("@isbyte", "1");
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
                else
                {
                    DataRow activeDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                    string id = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                    if (!string.IsNullOrEmpty(id))
                    {
                        activeSPEntry_binData.ClearAllParamsValues();
                        activeSPEntry_binData.ModifyParameterValue("@id", id);
                        activeSPEntry_binData.ModifyParameterValue("@data", binBuffer,binBuffer.Length);
                        Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                    }
                    else
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
                }
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }

        }
    }
}