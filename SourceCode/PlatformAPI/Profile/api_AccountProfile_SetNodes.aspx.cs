using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

public partial class Profile_api_AccountProfile_SetNodes : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {
            string accountName = "";
            string produceName = "";
            string parentXPATH = "";
            Dictionary<string, Dictionary<string, string>> newNodesLst = new Dictionary<string, Dictionary<string, string>>();
            if (REQUESTDOCUMENT.SelectSingleNode("/root/account") != null)
                accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/account"));
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for create profile node.", "");
                return;
            }
            if (REQUESTDOCUMENT.SelectSingleNode("/root/produce") != null)
                accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/produce"));
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated produce name for create profile node.", "");
                return;
            }
            if (REQUESTDOCUMENT.SelectSingleNode("/root/parent") != null)
                parentXPATH = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/parent"));
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated parent xpath for create profile node.", "");
                return;
            }
            XmlNodeList nodes = REQUESTDOCUMENT.SelectNodes("/root/newnodes/item");
            if(nodes==null && nodes.Count<=0)
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated produce name for create profile node.", "");
                return;
            }
            foreach(XmlNode activeNode in nodes)
            {
                Dictionary<string, string> nodeContentLst = new Dictionary<string, string>();
                string nodeName = class_XmlHelper.GetAttrValue(activeNode, "name");
                string nodeValue = class_XmlHelper.GetAttrValue(activeNode, "value");                
                nodeContentLst.Add("nodevalue", nodeValue);
                XmlNodeList attrItemList = activeNode.SelectNodes("attrs/item");
                foreach (XmlNode activeAttr in attrItemList)
                {
                    string attrName = class_XmlHelper.GetAttrValue(activeAttr, "name");
                    string attrValue = class_XmlHelper.GetAttrValue(activeAttr, "value");
                    if (!nodeContentLst.ContainsKey(attrName))
                        nodeContentLst.Add(attrName, nodeValue);
                }
                newNodesLst.Add(nodeName, nodeContentLst);
            }           
            string profileSymbol = "profile_" + accountName;
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
            activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
            activeSPEntry.ModifyParameterValue("@profile_product", produceName);
            DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (activeAccountProfileDataTable != null)
            {
                if (activeAccountProfileDataTable.Rows.Count > 0)
                {
                    string strDoc = "";
                    class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out strDoc);
                    string id = "";
                    class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "id", out id);
                    XmlDocument profileDoc = new XmlDocument();                                     
                    profileDoc.LoadXml(strDoc);
                    XmlNode parentNode = profileDoc.SelectSingleNode(parentXPATH);
                    if(parentNode!=null)
                    {
                        foreach (string activeNodeName in newNodesLst.Keys)
                        {
                            XmlNode tmpNewNode = parentNode.SelectSingleNode(activeNodeName);
                            if (tmpNewNode == null)
                                tmpNewNode = class_XmlHelper.CreateNode(profileDoc, activeNodeName, "");
                            foreach (string activeNodeContentKey in newNodesLst[activeNodeName].Keys)
                            {
                                if (activeNodeContentKey == "nodevalue")
                                    class_XmlHelper.SetNodeValue(tmpNewNode, newNodesLst[activeNodeName][activeNodeContentKey]);
                                else
                                {
                                    class_XmlHelper.SetAttrValue(tmpNewNode, activeNodeContentKey, newNodesLst[activeNodeName][activeNodeContentKey]);
                                }
                            }
                        }
                        activeSPEntry.ClearAllParamsValues();
                        activeSPEntry.ModifyParameterValue("@id", id);
                        activeSPEntry.ModifyParameterValue("@profile_data", profileDoc.OuterXml);
                        object_CommonLogic.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                    }
                    else                    
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no parent node.", "");                    
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no input document.", "");
        }
    }
}