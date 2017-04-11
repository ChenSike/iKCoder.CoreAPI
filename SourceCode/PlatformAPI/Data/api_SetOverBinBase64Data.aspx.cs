using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_SetOverBinBase64Data : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        ISRESPONSEDOC = true;
        string data = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/data");
        string type = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/type");
        string symbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/symbol");
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        string guid = symbol;
        if (guid == "")
            guid = Guid.NewGuid().ToString();
        activeSPEntry.ModifyParameterValue("@symbol", guid);
        DataTable binDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (binDataTable.Rows.Count == 0)
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", guid);
            activeSPEntry.ModifyParameterValue("@type", type);
            activeSPEntry.ModifyParameterValue("@data", data);
            activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
            activeSPEntry.ModifyParameterValue("@isBinary", "1");
            activeSPEntry.ModifyParameterValue("@isBase64", "1");
            activeSPEntry.ModifyParameterValue("@isDES", "0");
            activeSPEntry.ModifyParameterValue("@DESKey", "");
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");

        }
        else
        {
            string id = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(binDataTable.Rows[0], "id", out id);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@id", id);           
            activeSPEntry.ModifyParameterValue("@data", data);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
        }
        object_CommonLogic.CloseDBConnection();        
    }
}