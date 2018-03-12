using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_GetLessonSymbol : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string typeValue = GetQuerystringParam("typevalue");
        if (string.IsNullOrEmpty(typeValue))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->typevalue", "");
        }
        else
        {
            class_CommonDefined.enumLessonLevel activeSenceType = class_CommonDefined.GetSenceType(typeValue);
            class_Bus_Classes objectClasses = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            int count = objectClasses.GetCountOfClasses(typeValue);
            string startSymbol = class_CommonDefined.GetSymbolStartChar(typeValue);
            string symbol = startSymbol + "_" + count.ToString();
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), symbol, "");
        }
    }
}