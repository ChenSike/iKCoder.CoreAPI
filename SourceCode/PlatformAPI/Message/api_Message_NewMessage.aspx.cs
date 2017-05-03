using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Message_api_Message_NewMessage : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        switchResponseMode(enumResponseMode.text);
        string title = string.Empty;
        string username = string.Empty;
        string message = string.Empty;
        string type = string.Empty;
        string produce = string.Empty;
        if (REQUESTDOCUMENT!=null && !string.IsNullOrEmpty(REQUESTDOCUMENT.OuterXml))
        {
            XmlNode node_title = REQUESTDOCUMENT.SelectSingleNode("/root/title");
            XmlNode node_username = REQUESTDOCUMENT.SelectSingleNode("/root/username");
            XmlNode node_message = REQUESTDOCUMENT.SelectSingleNode("/root/message");
            XmlNode node_type = REQUESTDOCUMENT.SelectSingleNode("/root/type");
            XmlNode node_produce = REQUESTDOCUMENT.SelectSingleNode("/root/produce");
            if(node_title !=null)            
                title = class_XmlHelper.GetNodeValue(node_title);
            if (node_username != null)
                username = class_XmlHelper.GetNodeValue(node_username);
            if (node_message != null)
                message = class_XmlHelper.GetNodeValue(node_message);
            if (node_type != null)
                type = class_XmlHelper.GetNodeValue(node_type);
            if (node_produce != null)
                produce = class_XmlHelper.GetNodeValue(node_produce);
            if (string.IsNullOrEmpty(title))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "title->empty", "");
                return;
            }
            if (string.IsNullOrEmpty(username))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "title->username", "");
                return;
            }          
            if (string.IsNullOrEmpty(type))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "title->username", "");
                return;
            }
            if(string.IsNullOrEmpty(produce))
            {
                produce = "iKCoder";
            }
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_message_content");
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@title", title);
            activeSPEntry.ModifyParameterValue("@username", username);
            activeSPEntry.ModifyParameterValue("@message", message);
            activeSPEntry.ModifyParameterValue("@createtime", DateTime.Now.ToString());
            activeSPEntry.ModifyParameterValue("@type", type);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "request->empty", "");
            return;
        }
    }
}