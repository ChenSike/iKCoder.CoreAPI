using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Message
/// </summary>
public class class_Bus_Message
{

    XmlDocument resource_profile = new XmlDocument();

    public XmlDocument ActiveResourceDoc_Profile
    {
        get
        {
            return resource_profile;
        }
    }

    public class_Bus_Message(XmlDocument activeResourceProfile)
    {
        //
        // TODO: Add constructor logic here
        //
        resource_profile = activeResourceProfile;
    }

    public void checkMessageStatus()
    {
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        if (messagestatusNode == null)
        {
            messagestatusNode = class_XmlHelper.CreateNode(resource_profile, "messagestatus", "");
            XmlNode rootNOde = resource_profile.SelectSingleNode("/root");
            rootNOde.AppendChild(messagestatusNode);
        }
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        if(read_sys_Node==null)
        { 
            read_sys_Node = class_XmlHelper.CreateNode(resource_profile, "read_sys", "");
            messagestatusNode.AppendChild(read_sys_Node);
        }
        XmlNode rmv_msg_node = messagestatusNode.SelectSingleNode("rmv_msg");
        if(rmv_msg_node==null)
        {
            rmv_msg_node = class_XmlHelper.CreateNode(resource_profile, "rmv_msg","");
            messagestatusNode.AppendChild(rmv_msg_node);
        }
    }

    public void switchMessageToRemove(string operationID)
    {
        removeMessageFromRead(operationID);
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        XmlNode rmv_msg_node = messagestatusNode.SelectSingleNode("rmv_msg");
        XmlNode item = rmv_msg_node.SelectSingleNode("item[@id='" + operationID + "']");
        if(item==null)
        {
            item = class_XmlHelper.CreateNode(resource_profile, "item", "");
            class_XmlHelper.SetAttribute(item, "id", operationID);
            rmv_msg_node.AppendChild(item);
        }
    }

    public void switchMessageToRead(string operationID)
    {
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        XmlNode item = read_sys_Node.SelectSingleNode("item[@id='" + operationID + "']");
        if(item==null)
        {
            item = class_XmlHelper.CreateNode(resource_profile, "item", "");
            class_XmlHelper.SetAttribute(item, "id", operationID);
            read_sys_Node.AppendChild(item);
        }
    }

    public void removeMessageFromRead(string operationID)
    {
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        XmlNode item = read_sys_Node.SelectSingleNode("item[@id='" + operationID + "']");
        if(item!=null)
            read_sys_Node.RemoveChild(item);
    }

    public bool isRemoved(string operationID)
    {
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        XmlNode rmv_msg_node = messagestatusNode.SelectSingleNode("rmv_msg");
        XmlNode item = rmv_msg_node.SelectSingleNode("item[@id='" + operationID + "']");
        if (item != null)
            return true;
        else
            return false;
    }

    public bool isRead(string operationID)
    {
        XmlNode messagestatusNode = resource_profile.SelectSingleNode("/root/messagestatus");
        XmlNode read_sys_Node = messagestatusNode.SelectSingleNode("read_sys");
        XmlNode item = read_sys_Node.SelectSingleNode("item[@id='" + operationID + "']");
        if (item != null)
            return true;
        else
            return false;
    }    

}