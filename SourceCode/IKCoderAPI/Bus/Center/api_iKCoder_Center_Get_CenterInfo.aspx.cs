using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_Center_api_iKCoder_Center_Get_CenterInfo : class_WebBase_IKCoderAPI_NUA
{
    XmlDocument centerDocument = new XmlDocument();

    protected override void ExtendedAction()
    {
        centerDocument.LoadXml("<root></root>");    
    }
}