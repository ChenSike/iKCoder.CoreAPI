using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Data_api_BuildAllSPS : class_WebBase_NWA
{
    protected override void ExtenedFunction()
    {        
        ISRESPONSEDOC = true;
        if (GetQuerystringParam("keyCode") == class_CommonLogic.Const_OperationQueryString)
        {
            if (object_CommonLogic.ConnectToDatabase())
            {
                class_Data_SqlHelper objectSqlHelper = new class_Data_SqlHelper();
                if (objectSqlHelper.ActionAutoCreateSPS(object_CommonLogic.Object_SqlConnectionHelper.Get_ActiveConnection(object_CommonLogic.dbServer)))
                    AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + "execute_buildALLSPS", class_CommonDefined.enumExecutedCode.executed.ToString(), "complete executing api.", "");
                else
                    AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_buildALLSPS", "failed to execute task : build all SPS.", "");
                object_CommonLogic.CloseDBConnection();
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + "api_buildALLSPS", "Can not connect to Data Center.", "");
            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api+ "Invalidate operation from client.", "Invalidate operation", "");
    }
}