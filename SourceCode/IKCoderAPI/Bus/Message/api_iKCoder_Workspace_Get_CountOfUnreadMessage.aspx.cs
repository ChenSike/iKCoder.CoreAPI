using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Message_api_iKCoder_Workspace_Get_CountOfUnreadMessage : class_WebBase_IKCoderAPI_UA
{
    protected XmlDocument sourceDoc_profile = new XmlDocument();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        Object_CommonData.PrepareDataOperation();
        class_Bus_ProfileDocs Object_ProfileDocs = new class_Bus_ProfileDocs(ref Object_CommonData);
        int countOfmessage = 0;
        string strCountOfMessage = "0";
        DateTime dtSessionWriteCountTime = new DateTime();
        string symbol_SessionCountOfMessage = "COUNT_OF_UNREADMESSAGE";
        string symbol_SessionWriteCountTime = "WRITETIME_OF_UNREADMESSAGE";
        if (GetSessionValue(symbol_SessionCountOfMessage) != null && GetSessionValue(symbol_SessionWriteCountTime) != null)
        {
            dtSessionWriteCountTime = DateTime.Parse(GetSessionValue(symbol_SessionWriteCountTime).ToString());
            if ((DateTime.Now - dtSessionWriteCountTime).Minutes < 2)
            {
                strCountOfMessage = GetSessionValue(symbol_SessionCountOfMessage).ToString();
                int.TryParse(strCountOfMessage, out countOfmessage);
            }
            else
            {
                countOfmessage = Object_ProfileDocs.GetCountOfUnreadMessage(logined_user_name);
                Session[symbol_SessionCountOfMessage] = countOfmessage;
                Session[symbol_SessionWriteCountTime] = DateTime.Now.ToString();
            }
        }
        else
        {
            countOfmessage = Object_ProfileDocs.GetCountOfUnreadMessage(logined_user_name);
            Session[symbol_SessionCountOfMessage] = countOfmessage;
            Session[symbol_SessionWriteCountTime] = DateTime.Now.ToString();
        }
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), countOfmessage.ToString(), "");
    }
}