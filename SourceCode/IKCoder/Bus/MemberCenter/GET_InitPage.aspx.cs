using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Bus_MemberCenter_Get_AggReport : class_WebBase_UA
{      

    protected XmlDocument dataDocument = new XmlDocument();

    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;       
        dataDocument.LoadXml("<root></root>");
        XmlNode rootNode = dataDocument.SelectSingleNode("/root");
        BuildNode_carousel(rootNode,InitData_carouselItems());
    }

    protected List<Struct_InitPage_CarouselItem> InitData_carouselItems()
    {
        List<Struct_InitPage_CarouselItem> resultLst = new List<Struct_InitPage_CarouselItem>();
        Struct_InitPage_CarouselItem obj = new Struct_InitPage_CarouselItem();
        obj.color = "27,189,140";
        obj.title = "吃豆人大冒险";
        obj.content = "控制黄色小英雄，吃掉所有的豆子，小心邪恶小怪兽哦！";
        obj.keys = "键盘指令，空间移动";
        obj.diff = "中等";
        obj.times = "45分钟";
        obj.btncolor = "25,161,121";
        resultLst.Add(obj);
        return resultLst;
    }

    protected void BuildNode_carousel(XmlNode rootNode,List<Struct_InitPage_CarouselItem> carouseItemLst)
    {
        XmlNode carouselNode = class_XmlHelper.CreateNode(dataDocument, "carousel", "");
        foreach (Struct_InitPage_CarouselItem activeItem in carouseItemLst)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(dataDocument, "item", "");
            class_XmlHelper.SetAttribute(itemNode, "color", activeItem.color);
            class_XmlHelper.SetAttribute(itemNode, "title", activeItem.title);
            class_XmlHelper.SetAttribute(itemNode, "content", activeItem.content);
            class_XmlHelper.SetAttribute(itemNode, "keys", activeItem.keys);
            class_XmlHelper.SetAttribute(itemNode, "diff", activeItem.diff);
            class_XmlHelper.SetAttribute(itemNode, "times", activeItem.times);
            class_XmlHelper.SetAttribute(itemNode, "img", activeItem.img);
            class_XmlHelper.SetAttribute(itemNode, "btnColor", "25,161,121");
            carouselNode.AppendChild(itemNode);
        }
        rootNode.AppendChild(carouselNode);
    }

    protected void BuildNode_honorwall(XmlNode rootNode,List<Struct_InitPage_HonorItem> honorItemLst)
    {
        XmlNode honorwallNode = class_XmlHelper.CreateNode(dataDocument, "honorwall", "");
        foreach (Struct_InitPage_HonorItem activeItem in honorItemLst)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(dataDocument, "item", "");
            class_XmlHelper.SetAttribute(itemNode, "map", activeItem.map);
            class_XmlHelper.SetAttribute(itemNode, "title", activeItem.title);
            class_XmlHelper.SetAttribute(itemNode, "condition", activeItem.condition);
            honorwallNode.AppendChild(itemNode);
        }
        rootNode.AppendChild(honorwallNode);
    }

    protected void BuildNode_userinfo(XmlNode rootNode)
    {
        XmlNode userinfoNode = class_XmlHelper.CreateNode(dataDocument, "userinfo", "");
        
        rootNode.AppendChild(userinfoNode);
    }

}