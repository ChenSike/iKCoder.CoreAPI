using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Data_api_SetMetaTextData : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        ISRESPONSEDOC = true;
        string data = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/data");
        string type = class_CommonDefined.enumDataItemType.text.ToString();
        string operation = class_CommonDefined.enumDataOperaqtionType.insert.ToString();
        string symbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/symbol");
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_basic");
        string guid = symbol;
        if(guid=="")
            guid = Guid.NewGuid().ToString();
        activeSPEntry.ModifyParameterValue("@symbol", guid);     
        DataTable activeDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeDataTable.Rows.Count == 0)
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", guid);
            activeSPEntry.ModifyParameterValue("@type", type);
            activeSPEntry.ModifyParameterValue("@data", data);
            activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
            activeSPEntry.ModifyParameterValue("@isBinary", "0");
            activeSPEntry.ModifyParameterValue("@isBase64", "0");
            activeSPEntry.ModifyParameterValue("@isDES", "0");
            activeSPEntry.ModifyParameterValue("@DESKey", "");            
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, operation, this.GetType());
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");

        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : symbol existed or guid existed.", "");
        }       
    }
}