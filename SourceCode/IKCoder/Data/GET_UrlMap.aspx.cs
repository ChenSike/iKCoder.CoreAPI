using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Xml;

public partial class Data_GET_UrlMap : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        string mapkey = GetQuerystringParam("mapkey");
        bool isfulurl = GetQuerystringParam("fulurl") == "1"?true:false;
        if(string.IsNullOrEmpty(mapkey))
        {            
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Parameter", "", enum_MessageType.Exception);
            return;
        }
        try
        {
            XmlDocument roadMapDoc = new XmlDocument();
            roadMapDoc.Load(APPFOLDERPATH + "\\" + "roadmap.xml");
            XmlNode activeNode = roadMapDoc.SelectSingleNode("/root/item[@key='"+mapkey+"']");
            if(activeNode!=null)
            {
                string value = class_XmlHelper.GetAttrValue(activeNode,"value");
                string fullurl = "";
                if (isfulurl)
                    fullurl = Server_API + Virtul_Folder_API + value;
                else
                    fullurl = value;
                AddResponseMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), fullurl, "");
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Can not find the correct map node", "", enum_MessageType.Exception);
            }
                
        }
        catch
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "Invalidated Roadmap", "", enum_MessageType.Exception);
        }

        
    }
}