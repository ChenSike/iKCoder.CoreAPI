using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Profile_api_AccountProfile_SelectNodesValues : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        if (REQUESTDOCUMENT != null)
        {
            string accountName = string.Empty;
            if (REQUESTDOCUMENT.SelectSingleNode("/root/account") != null)
                accountName = class_XmlHelper.GetNodeValue(REQUESTDOCUMENT.SelectSingleNode("/root/account"));
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidated account name for select profile.", "");
                return;
            }
            List<string> searchItems = new List<string>();
            if(REQUESTDOCUMENT.SelectSingleNode("/root/select")!=null)
            {
                foreach(XmlNode activeItem in REQUESTDOCUMENT.SelectNodes("/root/select/items"))
                {
                    searchItems.Add(class_XmlHelper.GetAttrValue(activeItem, "value"));
                }
            }
            string profileSymbol = class_CommonName.GetProfileName(accountName);
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");
            activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
            DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (activeAccountProfileDataTable != null)
            {
                if (activeAccountProfileDataTable.Rows.Count > 0)
                {
                    string strDoc = "";
                    string data = "";
                    class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out strDoc);
                    try
                    {
                        XmlDocument resultDoc = new XmlDocument();
                        resultDoc.LoadXml(strDoc);
                        foreach (string selectXpath in searchItems)
                        {
                            XmlNode selectedNode = resultDoc.SelectSingleNode(selectXpath);
                            if (selectedNode != null)
                            {
                                data = class_XmlHelper.GetNodeValue(selectedNode);
                                Dictionary<string, string> attrs = new Dictionary<string, string>();
                                attrs.Add("xpath", selectXpath);
                                attrs.Add("value", data);
                                AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), attrs);
                            }                            
                        }
                    }
                    catch
                    {
                        AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : invalidate document.", "");
                    }
                }
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : no data.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : Null Request Document.", "");
    }
}