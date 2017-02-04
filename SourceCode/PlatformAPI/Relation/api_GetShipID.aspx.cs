using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Relation_api_GetShipID : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string relationSymbol = GetQuerystringParam("symbol");
        string relationProduce = GetQuerystringParam("produce");
        if(string.IsNullOrEmpty(relationSymbol))
        {
            if (REQUESTDOCUMENT != null && (REQUESTDOCUMENT.SelectSingleNode("/root/symbol")) != null)
                relationSymbol = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/symbol"));                           
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> symbol empty.", "");
                return;
            }
        }
        if(string.IsNullOrEmpty(relationProduce))
        {
             if (REQUESTDOCUMENT != null && (REQUESTDOCUMENT.SelectSingleNode("/root/produce")) != null)             
                 relationProduce = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/produce"));            
             else
             {
                 AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> produce empty.", "");
                 return;
             }
        }
        object_CommonLogic.ConnectToDatabase();
        object_CommonLogic.LoadStoreProcedureList();
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_data_relationship");
        activeSPEntry.ModifyParameterValue("@symbol", relationSymbol);
        activeSPEntry.ModifyParameterValue("@produce", relationProduce);
        DataTable selectResultDT = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (selectResultDT != null && selectResultDT.Rows.Count >= 1)
        {
            string relationID = "";
            class_Data_SqlDataHelper.GetColumnData(selectResultDT.Rows[0],"id",out relationID);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), relationID, "");            
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : select -> No data.", "");
        }
        object_CommonLogic.CloseDBConnection();
    }
}