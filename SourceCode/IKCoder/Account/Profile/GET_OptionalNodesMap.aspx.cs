using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

public partial class Account_Profile_GET_OptionalNodesMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sbDoc = new StringBuilder();
        sbDoc.Append("<root>");
        sbDoc.Append("<item name='nickname'></item>");
        sbDoc.Append("</root>");
        
    }
}