using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Relation_api_SetNewParantGroup : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string relationSymbol = GetQuerystringParam("symbol");
        string relationID = GetQuerystringParam("id");
        string relationProduce = GetQuerystringParam("produce");
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_relationship");
        if (string.IsNullOrEmpty(relationID))
        {
            if (string.IsNullOrEmpty(relationSymbol))
            {
                if (REQUESTDOCUMENT != null && (REQUESTDOCUMENT.SelectSingleNode("/root/symbol")) != null)
                    relationSymbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/symbol"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> symbol empty.", "");
                    return;
                }
            }
            if (string.IsNullOrEmpty(relationProduce))
            {
                if (REQUESTDOCUMENT != null && (REQUESTDOCUMENT.SelectSingleNode("/root/produce")) != null)
                    relationProduce = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/produce"));
                else
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> produce empty.", "");
                    return;
                }
            }
            activeSPEntry.ModifyParameterValue("@symbol", relationSymbol);
            activeSPEntry.ModifyParameterValue("@produce", relationProduce);
        }
        else        
            activeSPEntry.ModifyParameterValue("@id", relationID);
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT == null || selectResultDT.Rows.Count < 1)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> relation item not existed.", "");
            return;
        }
        else
        {
            string strRelationShipDoc = "";
            class_Data_SqlDataHelper.GetColumnData(selectResultDT.Rows[0], "relationdoc", out strRelationShipDoc);
            XmlDocument relationShipDoc = new XmlDocument();
            if (!string.IsNullOrEmpty(strRelationShipDoc))
            {
                try
                {
                    relationShipDoc.LoadXml(strRelationShipDoc);
                    XmlNode groupNode = relationShipDoc.SelectSingleNode("/root/group[@name='" + relationSymbol + "']");
                    if (groupNode != null)
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> relation parent group existed", "");
                        return;
                    }
                    else
                    {
                        groupNode = class_XmlHelper.CreateNode(relationShipDoc, "group","");
                        class_XmlHelper.SetAttribute(groupNode, "name", relationSymbol);
                    }
                }
                catch
                {
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : load -> relation documnet.", "");
                    return;
                }
            }
        }       

    }
}