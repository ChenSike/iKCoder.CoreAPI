using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Center_api_iKCoder_Center_Get_SencesList : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        List<DataRow> symbolList = class_Bus_SenceDoc.GetAllSences(Object_CommonData);
        if (symbolList != null && symbolList.Count > 0)
        {
            foreach (DataRow activeDataRow in symbolList)
            {
                string symbol = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "symbol", out symbol);
                string type = string.Empty;
                if (symbol.StartsWith("a") || symbol.StartsWith("A"))
                    type = "primer";
                else if (symbol.StartsWith("b") || symbol.StartsWith("B"))
                    type = "primary";
                else if (symbol.StartsWith("c") || symbol.StartsWith("C"))
                    type = "middle";
                else if (symbol.StartsWith("d") || symbol.StartsWith("D"))
                    type = "senior";
                else if (symbol.StartsWith("e") || symbol.StartsWith("E"))
                    type = "advanced";
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("symbol", symbol);
                attrs.Add("type", type);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }

    }
}