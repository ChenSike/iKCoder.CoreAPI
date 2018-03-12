using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_TeachingMan_M1_api_iKCoder_Teacher_SetNewClass : class_WebBase_IKCoderAPI_UA_Tm
{
    protected override void ExtendedAction()
    {
        if (REQUESTDOCUMENT != null)
        {

            string symbol = string.Empty;
            string teachersymbol = string.Empty;
            string classroom = string.Empty;
            string startdate = string.Empty;
            string personcount = string.Empty;
            XmlNode symbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode teachersymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/teachersymbol");
            XmlNode classroomNode = REQUESTDOCUMENT.SelectSingleNode("/root/classroom");
            XmlNode startdateNode = REQUESTDOCUMENT.SelectSingleNode("/root/startdate");
            XmlNode personcountNode = REQUESTDOCUMENT.SelectSingleNode("/root/personcount");
            if (!VerifyObjectNull(symbolNode))
                return;
            if (!VerifyObjectNull(teachersymbolNode))
                return;
            if (!VerifyObjectNull(classroomNode))
                return;
            if (!VerifyObjectNull(startdateNode))
                return;
            if (!VerifyObjectNull(startdateNode))
                personcount = "10";
            class_Bus_Classes objectClass = new class_Bus_Classes(Object_CommonData, logined_centersymbol);
            objectClass.SetUpdateClass(symbol);
            XmlDocument basicDoc = objectClass.GetClassBasicDoc(symbol);
            XmlNode rootNode = basicDoc.SelectSingleNode("/root");
            class_XmlHelper.SetAttribute(rootNode, "symbol", symbol);
            class_XmlHelper.SetAttribute(rootNode, "classroom", classroom);
            class_XmlHelper.SetAttribute(rootNode, "teacher", teachersymbol);
            class_XmlHelper.SetAttribute(rootNode, "start", startdate);
            class_XmlHelper.SetAttribute(rootNode, "persons", personcount);
            objectClass.SetUpdateClass(symbol);
            AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_New_Class"),"");
        }
        else
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Empty->Request Document", "");
        }
    }
}