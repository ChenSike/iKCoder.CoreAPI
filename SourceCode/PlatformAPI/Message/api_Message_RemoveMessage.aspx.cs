using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Message_api_Message_RemoveMessage : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);
        string id = string.Empty;
        id = GetQuerystringParam("id");
        if (string.IsNullOrEmpty(id))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "id->empty", "");
            return;
        }
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_content");
        activeSPEntry.ClearAllParamsValues();
        activeSPEntry.ModifyParameterValue("@id", id);
        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.delete.ToString(), this.GetType());
    }
}