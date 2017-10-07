using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_api_iKCoder_Sys_Get_PS : class_WebBase_IKCoderAPI_UA 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("[Count Of Session:" + Session.Count + "]<br>");
        Response.Write("[Count Of Application:" + Application.Count + "]<br>");
        Response.Write("[IKCoder SPS Flush Time:" + Application[Object_CommonData.APPLICATION_SYMBOL_SPSWRTIME].ToString() + "]<br>");
        foreach (string clientKey in Session.Keys)
            Response.Write("[PKey:" + clientKey + "].....Value:" + Session[clientKey].ToString() + "<br>");
    }
}