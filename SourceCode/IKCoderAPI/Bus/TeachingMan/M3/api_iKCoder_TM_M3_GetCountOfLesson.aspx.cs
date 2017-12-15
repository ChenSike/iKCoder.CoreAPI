using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_TeachingMan_M3_api_iKCoder_TM_M3_GetCountOfLesson : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        if(string.IsNullOrEmpty(symbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No querystring paramter->symbol", "");
        }
        else
        {
            class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            string levelValue = objectClass.GetClassLevel(symbol);
            class_Bus_Lessons objectLessons = new class_Bus_Lessons(Object_CommonData);
            int returnValue = objectLessons.GetCountOfLessons(levelValue);
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("value", returnValue.ToString());
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
        }
    }
}