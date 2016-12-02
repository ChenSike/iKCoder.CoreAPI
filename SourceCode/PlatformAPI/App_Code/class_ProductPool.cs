using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.IO;

/// <summary>
/// class_ProductPool 的摘要说明
/// </summary>
///

public static class class_ProductPool
{
    public static List<string> _activeProductList = new List<string>();

    public static void loadProduct(XmlNodeList productList,ref class_Base_Config refObjectConfig )
    {
        foreach (XmlNode activeProductNode in productList)
        {            
            string productName = refObjectConfig.GetAttrValue(activeProductNode, "name");
            if(!_activeProductList.Contains(productName))
            {
                _activeProductList.Add(productName);
            }
        }
    }   
	
}