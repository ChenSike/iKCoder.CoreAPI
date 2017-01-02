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
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Data_Basic");
        string guid = symbol;
        if(guid=="")
            guid = Guid.NewGuid().ToString();
        activeSPEntry.ModifyParameterValue("@symbol", guid);
        activeSPEntry.ModifyParameterValue("@type", type);
        activeSPEntry.ModifyParameterValue("@data", data);
        string Data64 = class_CommonUtil.Decoder_Base64(data);
        class_Data_SqlSPEntry activeSPEntry_Data = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Data_Basic");
        activeSPEntry_Data.ModifyParameterValue("@symbol", guid);
        DataTable binDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_Data, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (binDataTable.Rows.Count == 0)
        {
            activeSPEntry.ModifyParameterValue("@symbol", guid);
            activeSPEntry.ModifyParameterValue("@type", type);
            activeSPEntry.ModifyParameterValue("@data", data);
            activeSPEntry.ModifyParameterValue("@produce", _fromProduct);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, operation, this.GetType());
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : symbol existed or guid existed.", "");
        }
        object_CommonLogic.CloseDBConnection();        
    }
}