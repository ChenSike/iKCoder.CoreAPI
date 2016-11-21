using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

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
            string type = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/type");
            string operation = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/operation");
            class_Data_SqlHelper _objectSqlHelper = new class_Data_SqlHelper();            
            if(class_CommonDefined.enumDataOperaqtionType.insert.ToString()==operation)
            {
                string guid="";
                guid = Guid.NewGuid().ToString();
                activeSPEntry.ModifyParameterValue("@symbol",guid);
                activeSPEntry.ModifyParameterValue("@type", type);
                activeSPEntry.ModifyParameterValue("@data", data);
                if(_objectSqlHelper.ExecuteInsertSP(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_OperationMetaTextData", class_CommonDefined.enumExecutedCode.executed.ToString(), "Insert action complete.", "");

            }
            else if (class_CommonDefined.enumDataOperaqtionType.select.ToString() == operation)
            {

            }
            object_CommonLogic.CloseDBConnection();
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to execute api : api_OperationMetaTextData.", "");
        }
    }    
}