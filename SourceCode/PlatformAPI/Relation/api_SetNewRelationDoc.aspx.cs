using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Relation_api_SetNewRelationDoc : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string relationSymbol = GetQuerystringParam("symbol");
        string shipType = GetQuerystringParam("type");
        string produce = _fromProduct;
        XmlDocument relationDoc = new XmlDocument();
        relationDoc.LoadXml("<root></root>");        
        if(string.IsNullOrEmpty(relationSymbol))
        {
            XmlNode relationSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            if (relationSymbolNode != null)
                relationSymbol = class_XmlHelper.GetNodeValue(relationSymbolNode);
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "faild to create realationship : empty->symbol", "");
                return;
            }
        }
        if (string.IsNullOrEmpty(shipType))
        {
            XmlNode shipTypeNode = REQUESTDOCUMENT.SelectSingleNode("/root/shipType");
            if (shipTypeNode != null)
                shipType = class_XmlHelper.GetNodeValue(shipTypeNode);
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "faild to create realationship : empty->shiptype", "");
                return;
            }
        }
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_relationship");
        activeSPEntry.ModifyParameterValue("@symbol", relationSymbol);
        activeSPEntry.ModifyParameterValue("@produce", produce);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count >= 1)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : insert -> symbol existed.", "");
        }
        else
        {
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", relationSymbol);
            activeSPEntry.ModifyParameterValue("@shiptype", shipType);
            activeSPEntry.ModifyParameterValue("@relationdoc", relationDoc.OuterXml);
            activeSPEntry.ModifyParameterValue("@produce", produce);
            object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
        }
        
    }
}