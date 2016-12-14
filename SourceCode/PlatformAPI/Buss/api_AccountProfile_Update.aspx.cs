using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

public partial class Buss_api_AccountProfile_Update : class_WebClass_WLA
{
    protected override void AfterExtenedFunction()
    {
        ISRESPONSEDOC = true;
        if (REQUESTDOCUMENT != null)
        {
            XmlNode rootNode = REQUESTDOCUMENT.SelectSingleNode("/root");
            XmlNodeList l2_Nodes = rootNode.ChildNodes;
            foreach(XmlNode tmp_l2_Node in l2_Nodes)
            {

            }
        }
        else
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "failed to do action : empty request document for updating profile.", "");        
    }
}