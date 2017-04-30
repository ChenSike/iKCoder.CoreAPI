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
                    XmlNode symbolNode = currentsenceNode.SelectSingleNode("symbol");
                    XmlNode currentStageNode = currentsenceNode.SelectSingleNode("currentstage");
                    XmlNode finishStageNode = currentsenceNode.SelectSingleNode("finishstage");
                    int iCurrentStage = 0;
                    int.TryParse(class_XmlHelper.GetNodeValue(currentStageNode), out iCurrentStage);
                    if (int.Parse(futurestage) > 1 && iCurrentStage < int.Parse(futurestage))
                    {
                        XmlNode activeFinishItemNode = finishStageNode.SelectSingleNode("item[@step='" + (iCurrentStage - 1).ToString() + "']");
                        class_XmlHelper.SetAttribute(activeFinishItemNode, "finish", "1");
                    }
                    class_XmlHelper.SetNodeValue(currentStageNode, futurestage);
                }
                string strBase64Data = class_CommonUtil.Encoder_Base64(sourceDoc_profile.OuterXml);
                StringBuilder strInputDoc = new StringBuilder();
                strInputDoc.Append("<root>");
                strInputDoc.Append("<symbol>");
                strInputDoc.Append(profileSymbol);
                strInputDoc.Append("</symbol>");
                strInputDoc.Append("<data>");
                strInputDoc.Append(strBase64Data);
                strInputDoc.Append("</data>");
                strInputDoc.Append("</root>");
                requestAPI = "/Profile/api_AccountProfile_UpdateBySymbol.aspx";
                URL = Server_API + Virtul_Folder_API + requestAPI;
                returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader(strInputDoc.ToString(), URL, 1000 * 60, 100000);
                if (!returnStrDoc.Contains("err"))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
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