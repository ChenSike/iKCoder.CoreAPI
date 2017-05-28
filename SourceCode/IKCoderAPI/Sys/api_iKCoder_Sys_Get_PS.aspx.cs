using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Sys_api_iKCoder_Sys_Get_PS : class_WebBase_IKCoderAPI_NUA
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (string clientKey in Object_DomainPersistance.DataBuffer.Keys)
            foreach (string domainKey in Object_DomainPersistance.DataBuffer[clientKey].Keys)
            {
                Response.Write("[PKey:" + clientKey + "][DomainKey:" + domainKey + "].....Value:" + Object_DomainPersistance.DataBuffer[clientKey][domainKey].Data.ToString() + "<br>");
                Response.Write("[PKey:" + clientKey + "][DomainKey:" + domainKey + "].....Expired:" + Object_DomainPersistance.DataBuffer[clientKey][domainKey].Expeired.ToString() + "<br>");
            }
    }
}