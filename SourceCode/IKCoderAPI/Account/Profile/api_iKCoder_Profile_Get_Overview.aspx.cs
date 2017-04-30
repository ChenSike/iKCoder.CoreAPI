using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Text;

public partial class Account_Profile_api_iKCoder_Profile_Get_Overview : class_WebBase_IKCoderAPI_UA
{
    

    protected XmlDocument returnDoc = new XmlDocument();
    protected override void ExtendedAction()
    {
        StringBuilder strInputDoc = new StringBuilder();
        strInputDoc.Append("<root>");
        strInputDoc.Append("<account>");
        strInputDoc.Append("</account>");
        strInputDoc.Append("</root>");
        switchResponseMode(enumResponseMode.text);
    }
}