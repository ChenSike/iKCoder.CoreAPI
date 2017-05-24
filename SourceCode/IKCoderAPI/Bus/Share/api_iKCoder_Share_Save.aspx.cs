using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Bus_Share_api_iKCoder_Share_Save : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        if (REQUESTDOCUMENT != null && !string.IsNullOrEmpty(REQUESTDOCUMENT.OuterXml))
        {
            XmlNode node_sencesymbol = REQUESTDOCUMENT.SelectSingleNode("/root/sencesymbol");
            if (node_sencesymbol == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "sencesymbol->empty", "");
                return;
            }
            string sencesymbol = class_XmlHelper.GetNodeValue(node_sencesymbol);
            XmlNode node_configdoc = REQUESTDOCUMENT.SelectSingleNode("/root/config");
            if (node_configdoc == null)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "config->empty", "");
                return;
            }
            string configdoc = class_XmlHelper.GetNodeValue(node_configdoc);
            string symbol = Guid.NewGuid().ToString();
            Object_CommonData.PrepareDataOperation();
            class_Data_SqlSPEntry activeSPEntry = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_TMPSENCES);
            activeSPEntry.ClearAllParamsValues();
            activeSPEntry.ModifyParameterValue("@symbol", symbol);
            activeSPEntry.ModifyParameterValue("@createdtime", DateTime.Now.ToString());
            activeSPEntry.ModifyParameterValue("@config", symbol);
            activeSPEntry.ModifyParameterValue("@sencesymbol", sencesymbol);
            activeSPEntry.ModifyParameterValue("@sharedlink", "");
            Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "request document->empty", "");
            return;

        }
    }
}