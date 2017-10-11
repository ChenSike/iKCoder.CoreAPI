using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Data;

/// <summary>
/// Summary description for class_Bus_Profile_Verify
/// </summary>
public class class_Bus_Profile_VerifyStudentDocs : class_Bus_Profile_Base
{

    class_CommonDefined.enumLoginedMark profile_type = class_CommonDefined.enumLoginedMark.mark_student;

    public class_Bus_Profile_VerifyStudentDocs(ref class_CommonData refCommonDataObject) : base(refCommonDataObject)
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
        XmlNode node_usrname = node_usrbasic.SelectSingleNode("name");
        if (node_usrname == null)
        {
            node_usrname = class_XmlHelper.CreateNode(doc_basic, "name", username);
            node_usrbasic.AppendChild(node_usrname);
            isUpdated = true;
        }
        XmlNode node_country = node_usrbasic.SelectSingleNode("country");
        if (node_country == null)
        {
            node_country = class_XmlHelper.CreateNode(doc_basic, "country", "");
            node_usrbasic.AppendChild(node_country);
            isUpdated = true;
        }
        XmlNode node_city = node_usrbasic.SelectSingleNode("city");
        if (node_city == null)
        {           
            node_city = class_XmlHelper.CreateNode(doc_basic, "city", "");
            node_usrbasic.AppendChild(node_city);
            isUpdated = true;
        }
        XmlNode node_province = node_usrbasic.SelectSingleNode("province");
        if (node_province == null)
        {
            node_province = class_XmlHelper.CreateNode(doc_basic, "province", "");
            node_usrbasic.AppendChild(node_province);
            isUpdated = true;
        }
        XmlNode node_level = node_usrbasic.SelectSingleNode("level");
        if (node_level == null)
        {
            node_level = class_XmlHelper.CreateNode(doc_basic, "level", "1");
            node_usrbasic.AppendChild(node_level);
            isUpdated = true;
        }
        XmlNode node_address = node_usrbasic.SelectSingleNode("address");
        if (node_address == null)
        {
            node_address = class_XmlHelper.CreateNode(doc_basic, "address", "");
            node_usrbasic.AppendChild(node_address);
            isUpdated = true;
        }
        XmlNode node_tel = node_usrbasic.SelectSingleNode("tel");
        if (node_tel == null)
        {
            node_tel = class_XmlHelper.CreateNode(doc_basic, "tel", "");
            node_usrbasic.AppendChild(node_tel);
            isUpdated = true;
        }
        XmlNode node_contact = node_usrbasic.SelectSingleNode("contact");
        if (node_contact == null)
        {
            node_contact = class_XmlHelper.CreateNode(doc_basic, "contact", "");
            node_usrbasic.AppendChild(node_contact);
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
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_basic, doc_basic.OuterXml, class_CommonDefined.GetLoginedMarkType(profile_type).ToString());
    }
    
    public void VerifyDoc_Message(string username)
    {
        XmlDocument doc_message = new XmlDocument();
        bool isUpdated = false;
        string strDoc = string.Empty;
        if (VerifyProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_message))
            strDoc = GetProfileDoc(username, class_CommonDefined.enumProfileDoc.doc_message);
        else
        {
            strDoc = "<message></message>";
            isUpdated = true;
        }
        doc_message.LoadXml(strDoc);
        XmlNode node_messsage = doc_message.SelectSingleNode("/message");
        XmlNode node_readsys = node_messsage.SelectSingleNode("read_sys");
        if (node_readsys == null)
        {
            node_readsys = class_XmlHelper.CreateNode(doc_message, "read_sys", "");
            node_messsage.AppendChild(node_readsys);
            isUpdated = true;
        }
        XmlNode node_rmvsys = node_messsage.SelectSingleNode("rmv_sys");
        if (node_rmvsys == null)
        {
            node_rmvsys = class_XmlHelper.CreateNode(doc_message, "rmv_sys", "");
            node_messsage.AppendChild(node_rmvsys);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_message, doc_message.OuterXml, class_CommonDefined.GetLoginedMarkType(profile_type).ToString());
    }

    public void VerifyDoc_DataStore(string username)
    {
        XmlDocument doc_datastore = new XmlDocument();
        bool isUpdated = false;
        string strDoc = string.Empty;
        if (VerifyProfileDocExisted(username, class_CommonDefined.enumProfileDoc.doc_datastore))
            doc_datastore = GetProfileDocObject(username, class_CommonDefined.enumProfileDoc.doc_datastore);
        else
        {
            strDoc = "<datastore></datastore>";
            isUpdated = true;
            doc_datastore.LoadXml(strDoc);
        }
        XmlNode node_datastore = doc_datastore.SelectSingleNode("/datastore");
        XmlNode node_access = node_datastore.SelectSingleNode("access");
        if (node_access == null)
        {
            node_access = class_XmlHelper.CreateNode(doc_datastore, "access", "");
            class_XmlHelper.SetAttribute(node_access, "isallow", "1");
            class_XmlHelper.SetAttribute(node_access, "maxitem", "15");
            class_XmlHelper.SetAttribute(node_access, "maxfilesize", "3");
            node_datastore.AppendChild(node_access);
            isUpdated = true;
        }
        XmlNode node_datalist = node_datastore.SelectSingleNode("datalist");
        if (node_datalist == null)
        {
            node_datalist = class_XmlHelper.CreateNode(doc_datastore, "datalist", "");
            node_datastore.AppendChild(node_datalist);
            isUpdated = true;
        }
        if (isUpdated)
            SetUpdateProfileItem(username, class_CommonDefined.enumProfileDoc.doc_datastore, doc_datastore.OuterXml,class_CommonDefined.GetLoginedMarkType(profile_type).ToString());
    }

    public void VerifyAll(string username)
    {
        VerifyProfileItem(username);
        VerifyDoc_Basic(username);
        VerifyDoc_Message(username);
        VerifyDoc_DataStore(username);
    }

}