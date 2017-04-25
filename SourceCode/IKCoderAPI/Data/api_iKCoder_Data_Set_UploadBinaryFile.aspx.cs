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

public partial class Data_api_iKCoder_Data_UploadBinaryFile : class_WebBase_IKCoderAPI_UA
{    
    protected override void ExtendedAction()
    {
        string symbol = GetQuerystringParam("symbol");
        string filetype = GetQuerystringParam("filetype");
        if (!string.IsNullOrEmpty(symbol))
        {
            HttpFileCollection requestFiles = Request.Files;
            HttpPostedFile postFile = requestFiles[0];
            if (postFile != null)
            {
                byte[] binBuffer = new byte[postFile.InputStream.Length];
                postFile.InputStream.Read(binBuffer, 0, (int)postFile.InputStream.Length);
                if(binBuffer.Length>0)
                {
                    Object_CommonData.PrepareDataOperation();
                    class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
                    activeSPEntry_binData.ClearAllParamsValues();
                    activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                    DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                    if (binDataTable.Rows.Count == 0)
                    {
                        activeSPEntry_binData.ClearAllParams();
                        activeSPEntry_binData.ModifyParameterValue("@symbol", symbol);
                        activeSPEntry_binData.ModifyParameterValue("@type", filetype);
                        activeSPEntry_binData.ModifyParameterValue("@owner", logined_user_name);
                        activeSPEntry_binData.ModifyParameterValue("@data", binBuffer);
                        Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                    }
                    else
                    {
                        DataRow activeDataRow = null;
                        class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
                        string id = string.Empty;
                        class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                        if(!string.IsNullOrEmpty(id))
                        {
                            activeSPEntry_binData.ClearAllParams();
                            activeSPEntry_binData.ModifyParameterValue("@id", id);
                            activeSPEntry_binData.ModifyParameterValue("@data", binBuffer);
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
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }
        }
    }
}