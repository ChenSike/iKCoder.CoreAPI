using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Relation_api_SetUpdateRelationDoc : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string relationid = GetQuerystringParam("id");
        string base64data = string.Empty;
        if (string.IsNullOrEmpty(relationid))
        {
            XmlNode relationSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/id");
            if (relationSymbolNode != null)
                relationid = class_XmlHelper.GetNodeValue(relationSymbolNode);
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "faild to update realationship : empty->id", "");
                return;
            }
        }
        XmlNode relationDocNode = REQUESTDOCUMENT.SelectSingleNode("/root/doc");
        if (relationDocNode != null)
            base64data = class_XmlHelper.GetNodeValue(relationDocNode);
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "faild to update realationship : empty->doc", "");
            return;
        }
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_relationship");
        activeSPEntry.ModifyParameterValue("@id", relationid);
        activeSPEntry.ModifyParameterValue("@relationdoc", class_CommonUtil.Decoder_Base64(base64data));
        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");


    }
}