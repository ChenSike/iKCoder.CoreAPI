using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Relation_api_SetNewParantGroup : class_WebClass_WA
{
    protected override void ExtenedFunction()
    {
        ISRESPONSEDOC = true;
        string relationSymbol = GetQuerystringParam("symbol");
        string relationID = GetQuerystringParam("id");

    }
}