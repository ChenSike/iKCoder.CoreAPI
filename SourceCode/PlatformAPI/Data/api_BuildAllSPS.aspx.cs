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
                if (objectSqlHelper.ActionAutoCreateSPS(object_CommonLogic.Object_SqlConnectionHelper.Get_ActiveConnection(class_CommonLogic.Const_PlatformDBSymbol)))
                    AddResponseMessageToResponseDOC("response_from_api", "execute_buildALLSPS", "complete executing api.", "");
                else
                    AddErrMessageToResponseDOC("faild to execut api ： execute_buildALLSPS", "Please check the api : Data_api_BuildAllSPS", "");
                object_CommonLogic.CloseDBConnection();
            }
            else
            {
                AddErrMessageToResponseDOC("faild to execut api ： execute_buildALLSPS", "Can not connect to Data Center.", "");
            }
        }
        else
            AddErrMessageToResponseDOC("Invalidate operation from client.", "Invalidate operation", "");
    }
}