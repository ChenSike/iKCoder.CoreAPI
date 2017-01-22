using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

public partial class Profile_api_AccountProfile_VerifyNodesExisted : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC=true;
        List<string> checkList = new List<string>();
        if (REQUESTDOCUMENT != null)
        {
            XmlNodeList checkNodes = REQUESTDOCUMENT.SelectNodes("/root/item");
            foreach (XmlNode activeCheckNode in checkNodes)
            {
                string xpath = class_XmlHelper.GetNodeValue(activeCheckNode);
                checkList.Add(xpath);
            }            
            object_CommonLogic.ConnectToDatabase();
            object_CommonLogic.LoadStoreProcedureList();
            class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "spa_operation_account_profile");            
            DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
            if (activeAccountProfileDataTable != null)
            {
                foreach(DataRow activeDataRow in activeAccountProfileDataTable.Rows)
                {
                    string strDoc = "";
                    class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out strDoc);
                    XmlDocument profileDocument = new XmlDocument();
                    profileDocument.LoadXml(strDoc);
                    bool testResult = true;
                    foreach (string activeXpath in checkList)
                    {
                        XmlNode activeNode = profileDocument.SelectSingleNode(activeXpath);
                        if (activeNode == null)
                        {
                            testResult = false;
                            break;
                        }                        
                    }
                    if (testResult)
                    {
                        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), "true", "");
                    }
                }                
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