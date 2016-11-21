using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Data_api_GetDataItemType : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string queryType = GetQuerystringParam("type");
        class_CommonDefined.enumDataItemType dataItemType;
        switch(queryType)
        {
            case "text":
            case "Text":
            case "TEXT":
            case "xml":
            case "Xml":
            case "XML":
            case "string":
            case "String":
            case "STRING":
                dataItemType = class_CommonDefined.enumDataItemType.text;
                break;
            case "Bin":
            case "bin":
            case "BIN":
            case "binary":
            case "Binary":
            case "BINARY":
                dataItemType = class_CommonDefined.enumDataItemType.bin;
                break;
            default:
                dataItemType = class_CommonDefined.enumDataItemType.text;
                break;
        }
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "Data_api_GetDataItemType", class_CommonDefined.enumExecutedCode.executed.ToString(), dataItemType.ToString(), "");

    }
}