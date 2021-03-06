﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Account_api_OperationUserAccount : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        XmlNode operationNode = REQUESTDOCUMENT.SelectSingleNode("/root/operation");       
        XmlNode usernameNode = REQUESTDOCUMENT.SelectSingleNode("/root/username");       
        XmlNode passwordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");           
        string username = class_XmlHelper.GetNodeValue("", usernameNode);
        string operation = class_XmlHelper.GetNodeValue("", operationNode);
        string password = class_XmlHelper.GetNodeValue("", passwordNode);
        string desPassword = string.Empty;
        object_CommonLogic.Object_DES.DESCoding(password, out desPassword);
        if (!string.IsNullOrEmpty(desPassword))
            password = desPassword;
        if (string.IsNullOrEmpty(operation))
            operation = class_CommonDefined.enumDataOperaqtionType.insert.ToString();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_basic");        
        if(operation == class_CommonDefined.enumDataOperaqtionType.insert.ToString())
        {
            activeSPEntry.ModifyParameterValue("@username", username);
            DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (selectResultDT != null && selectResultDT.Rows.Count == 1)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : delete -> username existed.", "");
                return;
            }
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@username", username);
            activeSPEntry.ModifyParameterValue("@password", password);          
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
            activeSPEntry.ModifyParameterValue("@password", password); 
        }
        else if (class_CommonDefined.enumDataOperaqtionType.selectkey.ToString() == operation)
        {
            string id = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT, "/root/id");
            if (!string.IsNullOrEmpty(id))
                activeSPEntry.ModifyParameterValue("@id", id);
        }
        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, operation, this.GetType());
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        object_CommonLogic.CloseDBConnection();
    }    
}