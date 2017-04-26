using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;


public partial class Bus_Workspace_api_iKCoder_Data_Set_Sence : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string operation = GetQuerystringParam("operation");
        if (operation == class_CommonDefined._AllowSysOperation)
        {
            if(REQUESTDOCUMENT!=null)
            {
                XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
                XmlNode configNode = REQUESTDOCUMENT.SelectSingleNode("/root/config");
                XmlNode isfreeNode = REQUESTDOCUMENT.SelectSingleNode("/root/isfree");
                string symbol = string.Empty;
                string config = string.Empty;
                string isfree = string.Empty;
                symbol = class_XmlHelper.GetNodeValue(symbolNode);
                if(string.IsNullOrEmpty(symbol))
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
                    return;
                }
                config = class_XmlHelper.GetAttrValue(configNode,"doc");
                isfree = class_XmlHelper.GetNodeValue(isfreeNode);
                if (string.IsNullOrEmpty(isfree))
                    isfree = "1";
                Object_CommonData.PrepareDataOperation();
                class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
                activeSPEntry_configSence.ClearAllParamsValues();
                activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);
                string configBase64Doc = class_CommonUtil.Decoder_Base64(config);
                DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if(textDataTable!=null && textDataTable.Rows.Count>0)
                {
                    DataRow activeDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeDataRow);
                    string id = string.Empty;
                    activeSPEntry_configSence.ClearAllParamsValues();
                    activeSPEntry_configSence.ModifyParameterValue("@id", id);
                    activeSPEntry_configSence.ModifyParameterValue("@config", configBase64Doc);
                    activeSPEntry_configSence.ModifyParameterValue("@isfree", isfree);
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }
                else
                {
                    activeSPEntry_configSence.ClearAllParams();
                    activeSPEntry_configSence.ModifyParameterValue("@symbol", symbol);                    
                    activeSPEntry_configSence.ModifyParameterValue("@config", configBase64Doc);
                    activeSPEntry_configSence.ModifyParameterValue("@isfree", isfree);                    
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                }

            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty Requirement", "");
        }
    }
}