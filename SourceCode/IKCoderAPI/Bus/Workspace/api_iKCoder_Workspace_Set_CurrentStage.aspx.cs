using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_CurrentStage : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        string futurestage = GetQuerystringParam("stage");
        string symbol = GetQuerystringParam("symbol");
        XmlDocument sourceDoc_profile = new XmlDocument();
        if (!string.IsNullOrEmpty(futurestage))
        {
            string profileSymbol = "profile_" + "ikcoder_" + logined_user_name;
            string requestAPI = "/Profile/api_AccountProfile_SelectProfileBySymbol.aspx?symbol=" + profileSymbol;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            XmlDocument tmpData = new XmlDocument();
            tmpData.LoadXml(returnStrDoc);
            if (tmpData.SelectSingleNode("/root/err")==null)
            {                
                XmlNode msgNode = tmpData.SelectSingleNode("/root/msg");
                sourceDoc_profile.LoadXml(class_XmlHelper.GetAttrValue(msgNode, "msg"));
                XmlNode currentsenceNode = sourceDoc_profile.SelectSingleNode("/root/studystatus/currentsence[symbol[text()='" + symbol + "']]");
                if (currentsenceNode != null)
                {                    
                    XmlNode currentStageNode = currentsenceNode.SelectSingleNode("currentstage");
                    class_XmlHelper.SetNodeValue(currentStageNode, futurestage);
                }
                if (class_Bus_ProfileDoc.SetUserProfile(Server_API, Virtul_Folder_API, Object_NetRemote, logined_user_name, sourceDoc_profile.OuterXml))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "empty stage in querystring", "");

    }
}