using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Bus_Workspace_api_iKCoder_Workspace_Set_FinishSence : class_WebBase_IKCoderAPI_UA
{
    XmlDocument sourceDoc_profile = new XmlDocument();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string symbol = GetQuerystringParam("symbol");
        if (!string.IsNullOrEmpty(symbol))
        {
            string profileSymbol = "profile_" + "ikcoder_" + logined_user_name;
            string requestAPI = "/Profile/api_AccountProfile_SelectProfileBySymbol.aspx?symbol=" + profileSymbol;
            string URL = Server_API + Virtul_Folder_API + requestAPI;
            string returnStrDoc = Object_NetRemote.getRemoteRequestToStringWithCookieHeader("<root></root>", URL, 1000 * 60, 100000);
            XmlDocument tmpData = new XmlDocument();
            tmpData.LoadXml(returnStrDoc);
            if (tmpData.SelectSingleNode("/root/err") == null)
            {
                XmlNode msgNode = tmpData.SelectSingleNode("/root/msg");
                sourceDoc_profile.LoadXml(class_XmlHelper.GetAttrValue(msgNode, "msg"));
                XmlNode studystatusNode = sourceDoc_profile.SelectSingleNode("/root/studystatus");
                XmlNode finishedSenceNode = sourceDoc_profile.SelectSingleNode("/root/studystatus/finished");
                if (finishedSenceNode == null)
                {
                    finishedSenceNode = class_XmlHelper.CreateNode(sourceDoc_profile, "finished", "");
                    studystatusNode.AppendChild(finishedSenceNode);
                }
                XmlNode item = finishedSenceNode.SelectSingleNode("item[symbol[text()='" + symbol + "']]");
                if (item == null)
                {
                    XmlNode newItem = class_XmlHelper.CreateNode(sourceDoc_profile, "item", "");
                    finishedSenceNode.AppendChild(newItem);
                    XmlNode symbolNode = class_XmlHelper.CreateNode(sourceDoc_profile, "symbol", symbol);
                    newItem.AppendChild(symbolNode);
                    XmlNode startNode = class_XmlHelper.CreateNode(sourceDoc_profile, "start", DateTime.Now.ToString());
                    newItem.AppendChild(startNode);
                    XmlNode playNode = class_XmlHelper.CreateNode(sourceDoc_profile, "play", "1");
                    newItem.AppendChild(playNode);
                    XmlNode spendtimeNode = class_XmlHelper.CreateNode(sourceDoc_profile, "spendtime", "");
                    newItem.AppendChild(spendtimeNode);
                    XmlNode tmpEntryNode = class_XmlHelper.CreateNode(sourceDoc_profile, "tmpentry", DateTime.Now.ToString());
                    newItem.AppendChild(tmpEntryNode);
                }
                else
                {
                    XmlNode tmpEntryNode = item.SelectSingleNode("tmpentry");
                    DateTime tmpEntryDT = DateTime.Now;
                    DateTime.TryParse(class_XmlHelper.GetNodeValue(tmpEntryNode), out tmpEntryDT);
                    int minutes = (DateTime.Now - tmpEntryDT).Minutes;
                    int iSpendtime = 0;
                    XmlNode spendtimeNode = item.SelectSingleNode("spendtime");
                    int.TryParse(class_XmlHelper.GetNodeValue(spendtimeNode), out iSpendtime);
                    class_XmlHelper.SetAttribute(spendtimeNode, "spendtime", (iSpendtime + minutes).ToString());
                }
                class_Bus_ProfileDoc.SetCodetimeLineHours(sourceDoc_profile, symbol);

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
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "symbol->empty", "");
            }
        }
    }
}