using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Bus_Report_api_iKCoder_Report_Set_ExpReport : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        string tmpUserName = GetQuerystringParam("tmpname");
        if(!string.IsNullOrEmpty(tmpUserName))
        {
            Object_CommonData.PrepareDataOperation();
            string hostName = HttpContext.Current.Request.Url.Host;
            string serverPath = hostName + "/ikcoder/";
            string shareLink = serverPath + "samplereport.html?tmpname=" + tmpUserName;
            Util_QRCoder objectQRCoder = new Util_QRCoder();
            byte[] linkQRData = objectQRCoder.CreateQRToByteArr(shareLink, System.Drawing.Imaging.ImageFormat.Png);
            string linkQRDataBase64 = class_CommonUtil.Encoder_Base64(linkQRData);
            string symbolQRData = Guid.NewGuid().ToString();
            class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
            activeSPEntry_binData.ClearAllParamsValues();
            activeSPEntry_binData.ModifyParameterValue("@symbol", symbolQRData);
            activeSPEntry_binData.ModifyParameterValue("@type", "png");
            activeSPEntry_binData.ModifyParameterValue("@owner", "system");
            activeSPEntry_binData.ModifyParameterValue("@data", linkQRDataBase64);
            activeSPEntry_binData.ModifyParameterValue("@isbyte", "0");
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            Dictionary<string, string> resultMap = new Dictionary<string, string>();
            resultMap.Add("qrsymbol", symbolQRData);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), resultMap);
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "tmpname->empty", "");
            return;
        }
    }
}