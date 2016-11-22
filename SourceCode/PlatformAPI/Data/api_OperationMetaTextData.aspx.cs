using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Data_api_OperationMetaTextData : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Data_Basic");
            string data = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/data");
            string type = class_CommonDefined.enumDataItemType.text.ToString();
            string operation = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/operation");            
                  
            if(class_CommonDefined.enumDataOperaqtionType.insert.ToString()==operation)
            {
                string guid="";
                guid = Guid.NewGuid().ToString();
                activeSPEntry.ModifyParameterValue("@symbol",guid);
                activeSPEntry.ModifyParameterValue("@type", type);
                activeSPEntry.ModifyParameterValue("@data", data);
                if (object_CommonLogic.Object_SqlHelper.ExecuteInsertSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_OperationMetaTextData", class_CommonDefined.enumExecutedCode.executed.ToString(), guid, "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : insert.", "");
            }
            else if (class_CommonDefined.enumDataOperaqtionType.select.ToString() == operation)
            {
                DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                if (selectResultDT != null)
                {
                    string strXMLResult = class_Data_SqlDataHelper.ActionConvertDTtoXMLString(selectResultDT);
                    RESPONSEDOCUMENT.LoadXml(strXMLResult);
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : select.", "");

            }
            else if (class_CommonDefined.enumDataOperaqtionType.delete.ToString() == operation)
            {
                string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
                if (!string.IsNullOrEmpty(id))
                {
                    activeSPEntry.ModifyParameterValue("@id", id);
                    if (object_CommonLogic.Object_SqlHelper.ExecuteDeleteSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_OperationMetaTextData", class_CommonDefined.enumExecutedCode.executed.ToString(), "Delete action complete.", "");
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : delete -> empty ID.", "");
            }
            else if (class_CommonDefined.enumDataOperaqtionType.update.ToString() == operation)
            {
                if (!string.IsNullOrEmpty(type))
                    activeSPEntry.ModifyParameterValue("@type", type);
                if (!string.IsNullOrEmpty(data))
                    activeSPEntry.ModifyParameterValue("@data", data);
                if (object_CommonLogic.Object_SqlHelper.ExecuteUpdateSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_OperationMetaTextData", class_CommonDefined.enumExecutedCode.executed.ToString(), "Update action complete.", "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : update.", "");
            }
            else if (class_CommonDefined.enumDataOperaqtionType.selectkey.ToString() == operation)
            {
                DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
                if (selectResultDT != null)
                {
                    string strXMLResult = class_Data_SqlDataHelper.ActionConvertDTtoXMLString(selectResultDT);
                    RESPONSEDOCUMENT.LoadXml(strXMLResult);
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : select.", "");

            }
            object_CommonLogic.CloseDBConnection();
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to execute api : api_OperationMetaTextData.", "");
        }
    }    
}