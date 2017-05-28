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
        int countOfmessage = 0;
        string strCountOfMessage = "0";
        string symbol_strCountOfMeeage = "COUNT_OF_UNREADMESSAGE";        
        if (Object_DomainPersistance.IsDomainKeyExisted(Object_DomainPersistance.GetKeyName(REQUESTIP), symbol_strCountOfMeeage))
        {
            try
            {
                strCountOfMessage = Object_DomainPersistance.Get(Object_DomainPersistance.GetKeyName(REQUESTIP), symbol_strCountOfMeeage).ToString();
                int.TryParse(strCountOfMessage, out countOfmessage);
            }
            catch
            {
                countOfmessage = Object_ProfileDocs.GetCountOfUnreadMessage(logined_user_name);
                Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(REQUESTIP), symbol_strCountOfMeeage, 2, countOfmessage.ToString());
            }
        }
        else
        {
            countOfmessage = Object_ProfileDocs.GetCountOfUnreadMessage(logined_user_name);
            Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(REQUESTIP), symbol_strCountOfMeeage, 2, countOfmessage.ToString());
        }
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), countOfmessage.ToString(), "");
    }
}