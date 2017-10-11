using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Profile_VerifyAdvisor
/// </summary>
public class class_Bus_Profile_VerifyAdvisor:class_Bus_Profile_Base
{
    class_CommonDefined.enumLoginedMark profile_type = class_CommonDefined.enumLoginedMark.mark_advisor;


    public class_Bus_Profile_VerifyAdvisor(ref class_CommonData refCommonDataObject) : base(refCommonDataObject)
    {

    }

    public void VerifyDoc_Basic(string username)
    {
        XmlDocument doc_basic = new XmlDocument();
        string strDoc = string.Empty;
        bool isUpdated = false;
        if (VerifyProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_basic))
            strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_basic);
        else
        {
            strDoc = "<usrbasic></usrbasic>";
            isUpdated = true;
        }
        doc_basic.LoadXml(strDoc);
        XmlNode node_usrbasic = doc_basic.SelectSingleNode("/usrbasic");
        if (node_usrbasic == null)
        {
            strDoc = "<usrbasic></usrbasic>";
            doc_basic.LoadXml(strDoc);
            node_usrbasic = doc_basic.SelectSingleNode("/usrbasic");
            isUpdated = true;
        }       
        XmlNode node_usrnickname = node_usrbasic.SelectSingleNode("usr_nickname");
        if (node_usrnickname == null)
        {
            node_usrnickname = class_XmlHelper.CreateNode(doc_basic, "usr_nickname", username);
            node_usrbasic.AppendChild(node_usrnickname);
            isUpdated = true;
        }
        XmlNode node_usrlevel = node_usrbasic.SelectSingleNode("usr_level");
        if (node_usrlevel == null)
        {
            class_Bus_Title objectTitle = new class_Bus_Title(Object_CommonData);
            List<string> finisymbols = new List<string>();
            node_usrlevel = class_XmlHelper.CreateNode(doc_basic, "usr_level", "0");
            node_usrbasic.AppendChild(node_usrlevel);
            isUpdated = true;
        }
        XmlNode node_birthday = node_usrbasic.SelectSingleNode("birthday");
        if (node_birthday == null)
        {
            node_birthday = class_XmlHelper.CreateNode(doc_basic, "birthday", "2000-01-01");
            node_usrbasic.AppendChild(node_birthday);
            isUpdated = true;
        }      
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml, class_CommonDefined.GetLoginedMarkType(profile_type).ToString());
    }
}