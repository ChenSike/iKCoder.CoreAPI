using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Account_api_OperationUserAccount : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode operationNode = REQUESTDOCUMENT.SelectSingleNode("/root/operation");
        if(operationNode==null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No operation node.", "");
            return;
        }
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");
        if (usernameNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No username node.", "");
            return;
        }
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
        if (passwordNode == null)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationUserAccount", "Bad request document.No password node.", "");
            return;
        }
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string operation = class_XmlHelper.GetNodeValue("", operationNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_Basic");        
        if(operation == class_CommonDefined.enumDataOperaqtionType.insert.ToString())
        {
            activeSPEntry.ModifyParameterValue("@name", username);
            activeSPEntry.ModifyParameterValue("@password", password);          
        }
        else if (class_CommonDefined.enumDataOperaqtionType.delete.ToString() == operation)
        {
            string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
            if (!string.IsNullOrEmpty(id))            
                activeSPEntry.ModifyParameterValue("@id", id);
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_OperationMetaTextData", "failed to do action : delete -> empty ID.", "");
        }
        else if (class_CommonDefined.enumDataOperaqtionType.update.ToString() == operation)
        {
            string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
            if (!string.IsNullOrEmpty(id))
                activeSPEntry.ModifyParameterValue("@id", id);
            activeSPEntry.ModifyParameterValue("@password", password); 
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
}