using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Bus_AppStudio_api_iKCoder_AppSudio_Set_Save : class_WebBase_IKCoderAPI_UA
{
    protected override void ExtendedAction()
    {
        if (REQUESTDOCUMENT != null)
        {
            XmlNode rootNode = REQUESTDOCUMENT.SelectSingleNode("/root");
            if(rootNode==null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No root node", "");
                return;
            }
            XmlNode contentNode = rootNode.SelectSingleNode("content");
            if (contentNode == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No contentNode node", "");
                return;
            }
            string content = class_XmlHelper.GetNodeValue(contentNode);
            string base64Content = class_CommonUtil.Encoder_Base64(content);
            if(!string.IsNullOrEmpty(base64Content))
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No contentNode node", "");
                return;
            }
            else
            {
                string symbol = string.Empty;
                symbol = Guid.NewGuid().ToString();
                Object_CommonData.PrepareDataOperation();
                class_Data_SqlSPEntry activeSPEntry_resourceAppstudio = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_MESSAGE);
                activeSPEntry_resourceAppstudio.ClearAllParamsValues();
                activeSPEntry_resourceAppstudio.ModifyParameterValue("@symbol", symbol);
                activeSPEntry_resourceAppstudio.ModifyParameterValue("@modified", DateTime.Now.ToString());
                activeSPEntry_resourceAppstudio.ModifyParameterValue("@owner",logined_user_name);
                activeSPEntry_resourceAppstudio.ModifyParameterValue("@data", base64Content);
                Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_resourceAppstudio, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                Dictionary<string, string> attrs = new Dictionary<string, string>();
                attrs.Add("symbol", symbol);
                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
            }
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "No requestment", "");
        }
    }
}