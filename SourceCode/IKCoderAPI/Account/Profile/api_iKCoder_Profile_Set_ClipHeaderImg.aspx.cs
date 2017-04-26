using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Drawing;

public partial class Account_Profile_api_iKCoder_Profile_Set_ClipHeaderImg : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);        
        int startX = 0;
        int startY = 0;
        int width = 0;
        int height = 0;
        int.TryParse(GetQuerystringParam("startx"), out startX);
        int.TryParse(GetQuerystringParam("starty"), out startY);
        int.TryParse(GetQuerystringParam("width"), out width);
        int.TryParse(GetQuerystringParam("height"), out height);
        string herderImgTmpsymbol = "img_header_tmp_" + logined_user_name;
        string herderImgSymbol = "img_header_" + logined_user_name;
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
        activeSPEntry_binData.ClearAllParamsValues();
        activeSPEntry_binData.ModifyParameterValue("@symbol", herderImgTmpsymbol);
        DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (binDataTable != null && binDataTable.Rows.Count == 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
            byte[] headerImageData = class_Data_SqlDataHelper.GetArrBytesColumnData(activeDataRow, "data");
            string type = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "type",out type);
            string id = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
            if (headerImageData != null && headerImageData.Length > 0)
            {                
                Bitmap sourceBitMap = class_Util_DrawingServices.CreateImage(headerImageData);
                Bitmap clippedBitMap = class_Util_DrawingServices.ClipImage(sourceBitMap, startX, startY, width, height);
                byte[] clipperImageData = class_Util_DrawingServices.GetBytesFromBitmap(clippedBitMap, type);
                activeSPEntry_binData.ClearAllParamsValues();
                activeSPEntry_binData.ModifyParameterValue("@symbol", herderImgSymbol);
                binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if(binDataTable!=null && binDataTable.Rows.Count>0)
                {
                    activeSPEntry_binData.ClearAllParams();
                    activeSPEntry_binData.ModifyParameterValue("@id", id);
                    activeSPEntry_binData.ModifyParameterValue("@data", clipperImageData);
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
                else
                {
                    activeSPEntry_binData.ClearAllParams();
                    activeSPEntry_binData.ModifyParameterValue("@symbol", herderImgSymbol);
                    activeSPEntry_binData.ModifyParameterValue("@type", type);
                    activeSPEntry_binData.ModifyParameterValue("@owner", logined_user_name);
                    activeSPEntry_binData.ModifyParameterValue("@data", clipperImageData);
                    activeSPEntry_binData.ModifyParameterValue("@isbyte", "1");
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "", enum_MessageType.Exception);

    }
}