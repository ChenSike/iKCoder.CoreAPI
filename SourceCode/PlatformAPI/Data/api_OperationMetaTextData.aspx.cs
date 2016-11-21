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
            
            object_CommonLogic.CloseDBConnection();
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to execute api : api_OperationMetaTextData.", "");
        }
    }    
}