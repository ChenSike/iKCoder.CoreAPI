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
        List<Struct_InitPage_CarouselItem> carouselItemsLst = new List<Struct_InitPage_CarouselItem>();
        carouselItemsLst = InitData_carouselItems();        
        BuildNode_carousel(rootNode, carouselItemsLst);
        List<Struct_InitPage_HonorItem> honorItemLst  = new List<Struct_InitPage_HonorItem>();
        honorItemLst = InitData_honorItems();
        BuildNode_honorwall(rootNode, honorItemLst);
        BuildNode_userinfo(rootNode);

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

    protected List<Struct_InitPage_CodeTimeItem> InitData_userinfoCodeTime()
    {
        List<Struct_InitPage_CodeTimeItem> resultLst = new List<Struct_InitPage_CodeTimeItem>();
        Struct_InitPage_CodeTimeItem newItem = new Struct_InitPage_CodeTimeItem();
        newItem.date = "2017-1-1";
        newItem.time = "3";
        resultLst.Add(newItem);
        return resultLst;
    }

    protected List<Structs_InitPage_LearningItem> InitData_learningItem()
    {
        List<Structs_InitPage_LearningItem> resultLst = new List<Structs_InitPage_LearningItem>();
        Structs_InitPage_LearningItem newItem = new Structs_InitPage_LearningItem();
        newItem.id = "1";
        newItem.title = "Star Wars";
        newItem.content = "Learn to program droids, and create your own Star Wars game in a galaxy far, far away.";
        newItem.img = "";
    }

    protected Struct_InitPage_UserinfoUser InitData_userinfoUser()
    {
        Struct_InitPage_UserinfoUser result = new Struct_InitPage_UserinfoUser();
        result.name = "可乐";
        result.level = "实习工程师";
        result.works = "18";
        result.course = "25";
        result.friend = "30";
        result.head = "1";
        return result;
    }

    protected Struct_InitPage_UserinfoCourse InitData_userinfoCourse()
    {
        Struct_InitPage_UserinfoCourse result = new Struct_InitPage_UserinfoCourse();
        result.rank = "1";
        result.emp = "3000";
        result.works = "18";
        result.works_rank = "2";
        result.code_time = "175";
        result.code_time_exceed = "92";
        result.primary_rate = "85";
        result.middel_rate = "11";
        result.advanced_rate = "5";
        return result;
    }

    protected List<Struct_InitPage_UserinfoDistributionItems> InitData_userinfoDistributionItems()
    {
        List<Struct_InitPage_UserinfoDistributionItems> result = new List<Struct_InitPage_UserinfoDistributionItems>();
        Struct_InitPage_UserinfoDistributionItems newItem = new Struct_InitPage_UserinfoDistributionItems();
        newItem.id="science";
        newItem.title = "科学";
        newItem.color = "rgb(36,90,186)";
        newItem.exp = "250";
        result.Add(newItem);
        newItem = new Struct_InitPage_UserinfoDistributionItems();
        newItem.id="skill";
        newItem.title = "技术";
        newItem.color = "rgb(236,15,33)";
        newItem.exp = "400";
        result.Add(newItem);
        newItem = new Struct_InitPage_UserinfoDistributionItems();
        newItem.id="engineering";
        newItem.title = "工程";
        newItem.color = "rgb(165,165,165)";
        newItem.exp = "550";
        result.Add(newItem);
        newItem = new Struct_InitPage_UserinfoDistributionItems();
        newItem.id="math";
        newItem.title = "数学";
        newItem.color = "rgb(255,191,0)";
        newItem.exp = "700";
        result.Add(newItem);
        newItem = new Struct_InitPage_UserinfoDistributionItems();
        newItem.id="language";
        newItem.title = "语言";
        newItem.color = "rgb(71,143,208)";
        newItem.exp = "700";
        result.Add(newItem);
        return result;
    }

    protected List<Struct_InitPage_HonorItem> InitData_honorItems()
    {
        List<Struct_InitPage_HonorItem> result = new List<Struct_InitPage_HonorItem>();
        class_HonorWallItems definedForHonorItems = new class_HonorWallItems();
        List<string> propertiesLst = definedForHonorItems.GetPropertiesNameForGet();
        foreach(string activeProperty in propertiesLst)
        {
            Struct_InitPage_HonorItem newItem = new Struct_InitPage_HonorItem();
            newItem.map = definedForHonorItems.GetValue(activeProperty);
            newItem.title = Object_LabelController.GetString("labels", newItem.map+"_title");
            newItem.condition = Object_LabelController.GetString("labels", newItem.map + "_condition");
            result.Add(newItem);
        }
        return result;
    }

    protected void BuildNode_learning(XmlNode rootNode)
    {
        XmlNode learningNode = class_XmlHelper.CreateNode(dataDocument, "learning", "");
        rootNode.AppendChild(learningNode);
    }

    protected void BuildNode_carousel(XmlNode rootNode,List<Struct_InitPage_CarouselItem> carouseItemLst)
    {
        XmlNode carouselNode = class_XmlHelper.CreateNode(dataDocument, "carousel", "");
        foreach (Struct_InitPage_CarouselItem activeItem in carouseItemLst)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(dataDocument, "item", "");
            List<string> setPropertiesLst = activeItem.GetPropertiesNameForSet();
            foreach(string activeProperty in setPropertiesLst)            
                class_XmlHelper.SetAttribute(itemNode, activeProperty, activeItem.GetValue(activeProperty).ToString());
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
            List<string> setPropertiesLst = activeItem.GetPropertiesNameForSet();
            foreach(string activeProperty in setPropertiesLst)            
                class_XmlHelper.SetAttribute(itemNode, activeProperty, activeItem.GetValue(activeProperty).ToString());
            honorwallNode.AppendChild(itemNode);
        }
        rootNode.AppendChild(honorwallNode);
    }
         

    protected void BuildNode_userinfo(XmlNode rootNode)
    {
        Struct_InitPage_UserinfoUser objUserinfoUser = InitData_userinfoUser();
        XmlNode userinfoNode = class_XmlHelper.CreateNode(dataDocument, "userinfo", "");
        List<string> setPropertiesLst = objUserinfoUser.GetPropertiesNameForSet();
        XmlNode userNode = class_XmlHelper.CreateNode(dataDocument, "user", "");
        foreach (string activeProperty in setPropertiesLst)
            class_XmlHelper.SetAttribute(userNode, activeProperty, objUserinfoUser.GetValue(activeProperty).ToString());        
        userinfoNode.AppendChild(userNode);
        rootNode.AppendChild(userinfoNode);
        XmlNode courseNode = class_XmlHelper.CreateNode(dataDocument, "course", "");
        userinfoNode.AppendChild(courseNode);
        Struct_InitPage_UserinfoCourse courseItem = InitData_userinfoCourse();
        setPropertiesLst = courseItem.GetPropertiesNameForSet();
        foreach (string activeProperty in setPropertiesLst)
            class_XmlHelper.SetAttribute(courseNode, activeProperty, objUserinfoUser.GetValue(activeProperty).ToString());
        userinfoNode.AppendChild(courseNode);
        XmlNode distributionNode = class_XmlHelper.CreateNode(dataDocument, "distribution", "");
        userinfoNode.AppendChild(distributionNode);
        List<Struct_InitPage_UserinfoDistributionItems> distributionItems = InitData_userinfoDistributionItems();
        foreach (Struct_InitPage_UserinfoDistributionItems distributionItem in distributionItems)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(dataDocument, "item", "");
            distributionNode.AppendChild(itemNode);
            setPropertiesLst = distributionItem.GetPropertiesNameForSet();
            foreach (string activeProperty in setPropertiesLst)
                class_XmlHelper.SetAttribute(itemNode, activeProperty, objUserinfoUser.GetValue(activeProperty).ToString());
        }
        List<Struct_InitPage_CodeTimeItem> codeTimeLst = InitData_userinfoCodeTime();
        XmlNode codetimeNode = class_XmlHelper.CreateNode(dataDocument, "codetimes", "");
        userinfoNode.AppendChild(codetimeNode);
        foreach(Struct_InitPage_CodeTimeItem codeTimeItem in codeTimeLst)
        {
            XmlNode itemNode = class_XmlHelper.CreateNode(dataDocument, "item", "");
            codetimeNode.AppendChild(itemNode);
            setPropertiesLst = codeTimeItem.GetPropertiesNameForSet();
            foreach (string activeProperty in setPropertiesLst)
                class_XmlHelper.SetAttribute(itemNode, activeProperty, objUserinfoUser.GetValue(activeProperty).ToString());
        }
    }

}