using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_api_iKCoder_Sys_Set_ReloadSPS : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        Object_CommonData.SPS_FlushMark = "1";
        Object_CommonData.PrepareDataOperation();
    }
}