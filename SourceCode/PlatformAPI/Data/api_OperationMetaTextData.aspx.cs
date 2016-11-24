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
             }            
            else if (class_CommonDefined.enumDataOperaqtionType.delete.ToString() == operation)
            {
                string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
                if (!string.IsNullOrEmpty(id))                
                    activeSPEntry.ModifyParameterValue("@id", id);                   
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : delete -> empty ID.", "");
            }
            else if (class_CommonDefined.enumDataOperaqtionType.update.ToString() == operation)
            {
                string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
                if (!string.IsNullOrEmpty(id))
                    activeSPEntry.ModifyParameterValue("@id", id);  
                if (!string.IsNullOrEmpty(type))
                    activeSPEntry.ModifyParameterValue("@type", type);
                if (!string.IsNullOrEmpty(data))
                    activeSPEntry.ModifyParameterValue("@data", data);
            }
            else if (class_CommonDefined.enumDataOperaqtionType.selectkey.ToString() == operation)
            {
                string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
                if (!string.IsNullOrEmpty(id))
                    activeSPEntry.ModifyParameterValue("@id", id);
            }
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, operation, this.GetType());
            object_CommonLogic.CloseDBConnection();
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to execute api : api_OperationMetaTextData.", "");
        }
    }    
}