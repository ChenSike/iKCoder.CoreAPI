using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Profile_VerifyEduCenterDocs
/// </summary>
public class class_Bus_Profile_VerifyEduCenterDocs:class_Bus_Profile_Base
{
    class_CommonDefined.enumLoginedMark profile_type = class_CommonDefined.enumLoginedMark.mark_educenter;
                

    public class_Bus_Profile_VerifyEduCenterDocs(ref class_CommonData refCommonDataObject) : base(refCommonDataObject)
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
        XmlNode node_usrname = node_usrbasic.SelectSingleNode("usr_name");
        if (node_usrname == null)
        {
            node_usrname = class_XmlHelper.CreateNode(doc_basic, "usr_name", username);
            node_usrbasic.AppendChild(node_usrname);
            isUpdated = true;
        }
        XmlNode node_usrnickname = node_usrbasic.SelectSingleNode("usr_nickname");
        if (node_usrnickname == null)
        {
            node_usrnickname = class_XmlHelper.CreateNode(doc_basic, "usr_nickname", username);
            node_usrbasic.AppendChild(node_usrnickname);
            isUpdated = true;
        }
        XmlNode node_usrtitle = node_usrbasic.SelectSingleNode("usr_title");
        if (node_usrtitle == null)
        {
            class_Bus_Title objectTitle = new class_Bus_Title(Object_CommonData);
            List<string> finisymbols = new List<string>();
            node_usrtitle = class_XmlHelper.CreateNode(doc_basic, "usr_title", objectTitle.GetTitle(finisymbols));
            node_usrbasic.AppendChild(node_usrtitle);
            isUpdated = true;
        }
        XmlNode node_sex = node_usrbasic.SelectSingleNode("sex");
        if (node_sex == null)
        {
            node_sex = class_XmlHelper.CreateNode(doc_basic, "sex", "1");
            node_usrbasic.AppendChild(node_sex);
            isUpdated = true;
        }
        XmlNode node_birthday = node_usrbasic.SelectSingleNode("birthday");
        if (node_birthday == null)
        {
            node_birthday = class_XmlHelper.CreateNode(doc_basic, "birthday", "2000-01-01");
            node_usrbasic.AppendChild(node_birthday);
            isUpdated = true;
        }
        XmlNode node_state = node_usrbasic.SelectSingleNode("state");
        if (node_state == null)
        {
            node_state = class_XmlHelper.CreateNode(doc_basic, "state", "");
            node_usrbasic.AppendChild(node_state);
            isUpdated = true;
        }
        XmlNode node_city = node_usrbasic.SelectSingleNode("city");
        if (node_city == null)
        {
            node_city = class_XmlHelper.CreateNode(doc_basic, "city", "");
            node_usrbasic.AppendChild(node_city);
            isUpdated = true;
        }
        XmlNode node_school = node_usrbasic.SelectSingleNode("school");
        if (node_school == null)
        {
            node_school = class_XmlHelper.CreateNode(doc_basic, "school", "");
            node_usrbasic.AppendChild(node_school);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml, class_CommonDefined.GetLoginedMarkType(profile_type).ToString());
    }

}