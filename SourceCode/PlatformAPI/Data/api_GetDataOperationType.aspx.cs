using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Data_api_GetDataOperationType : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string queryType = GetQuerystringParam("type");
        class_CommonDefined.enumDataOperaqtionType operationType;
        switch (queryType)
        {
            case "insert":
            case "Insert":
            case "INSERT":
                operationType = class_CommonDefined.enumDataOperaqtionType.insert;
                break;
            case "Select":
            case "select":
            case "SELECT":
                operationType = class_CommonDefined.enumDataOperaqtionType.select;
                break;
            case "Delete":
            case "delete":
            case "DELETE":
                operationType = class_CommonDefined.enumDataOperaqtionType.delete;
                break;
            case "update":
            case "Update":
            case "UPDATE":
                operationType = class_CommonDefined.enumDataOperaqtionType.update;
                break;
            case "selectkey":
            case "Selectkey":
            case "SELECTKEY":
                operationType = class_CommonDefined.enumDataOperaqtionType.selectkey;
                break;
            default:
                operationType = class_CommonDefined.enumDataOperaqtionType.select;
                break;
        }
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_GetDataItemType", class_CommonDefined.enumExecutedCode.executed.ToString(), operationType.ToString(), "");

    }
}