using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// Summary description for class_Bus_Profile_VerifyTeacher
/// </summary>
public class class_Bus_Profile_VerifyTeacherDocs : class_Bus_Profile_Base
{
    string profile_type = "2";

    public class_Bus_Profile_VerifyTeacherDocs(ref class_CommonData refCommonDataObject) : base(refCommonDataObject)
    {
        //
        // TODO: Add constructor logic here
        //
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
            strDoc = "<basic></basic>";
            isUpdated = true;
        }
        doc_basic.LoadXml(strDoc);
        XmlNode node_usrbasic = doc_basic.SelectSingleNode("/basic");
        if (node_usrbasic == null)
        {
            strDoc = "<basic></basic>";
            doc_basic.LoadXml(strDoc);
            node_usrbasic = doc_basic.SelectSingleNode("/basic");
            isUpdated = true;
        }
        XmlNode node_name = node_usrbasic.SelectSingleNode("name");
        if (node_name == null)
        {
            node_name = class_XmlHelper.CreateNode(doc_basic, "name", username);
            node_usrbasic.AppendChild(node_name);
            isUpdated = true;
        }
        XmlNode node_school = node_usrbasic.SelectSingleNode("school");
        if (node_school == null)
        {
            node_school = class_XmlHelper.CreateNode(doc_basic, "school", "");
            node_usrbasic.AppendChild(node_school);
            isUpdated = true;
        }
        XmlNode node_major = node_usrbasic.SelectSingleNode("major");
        if (node_major == null)
        {
            node_major = class_XmlHelper.CreateNode(doc_basic, "major", "");
            node_usrbasic.AppendChild(node_major);
            isUpdated = true;
        }
        XmlNode node_national = node_usrbasic.SelectSingleNode("national");
        if (node_national == null)
        {
            node_national = class_XmlHelper.CreateNode(doc_basic, "national", "");
            node_usrbasic.AppendChild(node_national);
            isUpdated = true;
        }
        XmlNode node_tel = node_usrbasic.SelectSingleNode("tel");
        if (node_tel == null)
        {
            node_tel = class_XmlHelper.CreateNode(doc_basic, "tel", "");
            node_usrbasic.AppendChild(node_tel);
            isUpdated = true;
        }
        XmlNode node_email = node_usrbasic.SelectSingleNode("email");
        if (node_email == null)
        {
            node_email = class_XmlHelper.CreateNode(doc_basic, "email", "");
            node_usrbasic.AppendChild(node_email);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml, profile_type);
    }

}