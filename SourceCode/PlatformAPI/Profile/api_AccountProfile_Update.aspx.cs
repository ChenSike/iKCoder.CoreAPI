using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Data;

public partial class Buss_api_AccountProfile_Update : class_WebClass_WLA
{
    protected override void AfterExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string profileSymbol = "profile_" + activeUserName;
        class_Data_SqlSPEntry activeSPEntry = object_CommonLogic.GetActiveSP(object_CommonLogic.dbServer, "SPA_Operation_Account_Basic");
        activeSPEntry.ModifyParameterValue("@profile_name", profileSymbol);
        DataTable activeAccountProfileDataTable = object_CommonLogic.Object_SqlHelper.ExecuteSelectSPKeyForDT(activeSPEntry, object_CommonLogic.Object_SqlConnectionHelper, object_CommonLogic.dbServer);
        if (activeAccountProfileDataTable != null && activeAccountProfileDataTable.Rows.Count>0)
        {
            if (REQUESTDOCUMENT != null)
            {
                string profile_data = "";
                class_Data_SqlDataHelper.GetColumnData(activeAccountProfileDataTable.Rows[0], "profile_data", out profile_data);
                XmlNode rootNode = REQUESTDOCUMENT.SelectSingleNode("/root");
                
            }
            else
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : empty request document for updating profile.", "");
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : empty request document for updating profile.", "");

    }

    public void degMethod(XmlNode activeNode)
    {

    }

}