using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Text;

public partial class Buss_api_AccountProfile_Create : class_WebClass_WLA
{
    protected override void AfterExtenedFunction()
    {
        StringBuilder strProDoc = new StringBuilder();
        strProDoc.Append("<root>");
        strProDoc.Append("<docbasic>");
        strProDoc.Append("<doc_id>");
        strProDoc.Append("</doc_id>");
        strProDoc.Append("<doc_symbol");
        strProDoc.Append("</doc_symbol>");
        strProDoc.Append("</docbasic>");
        strProDoc.Append("<usrbasic>");
        strProDoc.Append("<usr_name>");
        strProDoc.Append(activeUserName);
        strProDoc.Append("</usr_name>");
        strProDoc.Append("<usr_nickname>");
        strProDoc.Append("</usr_nickname>");
        strProDoc.Append("<coins>");
        strProDoc.Append("0");
        strProDoc.Append("<coins>");
        strProDoc.Append("<account_status>");
        strProDoc.Append("L0");
        strProDoc.Append("</account_status>");
        strProDoc.Append("<account_limited>");
        strProDoc.Append("</account_limited>");
        strProDoc.Append("<account_head>");
        strProDoc.Append("</account_head>");
        strProDoc.Append("</usrbasic>");
        strProDoc.Append("<lessons>");
        strProDoc.Append("<begin></begin>");
        strProDoc.Append("<intermediate></intermediate>");
        strProDoc.Append("<senior></senior>");
        strProDoc.Append("</lessions>");
        strProDoc.Append("<friends></friends>");
        strProDoc.Append("</root>");      
        XmlDocument proDoc = new XmlDocument();
        proDoc.LoadXml(strProDoc.ToString());        
    }
}