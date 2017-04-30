using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iKCoder_Platform_SDK_Kit;
using System.Xml;
using System.Drawing;

public partial class Util_api_iKCoder_Util_Get_CheckCode : class_WebBase_IKCoderAPI_NUA
{

    class_Security_CheckCode _objectCheckCode = new class_Security_CheckCode();

    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.bin);
        int activeCodeLength = 5;
        int imageWidth = 80;
        int imageHeight = 30;
        string codeLength = "";
        string codeName = "";
        string strImageWidth = "";
        string strImageHeight = "";
        if (REQUESTDOCUMENT != null)
        {
            XmlNode lengthNode = REQUESTDOCUMENT.SelectSingleNode("/root/length");
            codeLength = class_XmlHelper.GetNodeValue(lengthNode);
            XmlNode nameNode = REQUESTDOCUMENT.SelectSingleNode("/root/name");
            codeName = class_XmlHelper.GetNodeValue(nameNode);
            XmlNode widthNode = REQUESTDOCUMENT.SelectSingleNode("/root/width");
            strImageWidth = class_XmlHelper.GetNodeValue(widthNode);
            XmlNode heightNode = REQUESTDOCUMENT.SelectSingleNode("/root/height");
            strImageHeight = class_XmlHelper.GetNodeValue(heightNode);
        }
        if (string.IsNullOrEmpty(codeLength))
            codeLength = GetQuerystringParam("length");
        if (string.IsNullOrEmpty(codeName))
            codeName = GetQuerystringParam("name");
        if (string.IsNullOrEmpty(strImageWidth))
            strImageWidth = GetQuerystringParam("width");
        if (string.IsNullOrEmpty(strImageHeight))
            strImageHeight = GetQuerystringParam("height");
        int.TryParse(codeLength, out activeCodeLength);
        int.TryParse(strImageWidth, out imageWidth);
        int.TryParse(strImageHeight, out imageHeight);
        _objectCheckCode.NextCode(activeCodeLength);
        RESPONSEBUFFER = _objectCheckCode.CreateImage(Color.Transparent, imageWidth, imageHeight);
        Object_DomainPersistance.Add(Object_DomainPersistance.GetKeyName(REQUESTIP, Produce_Name, ClientSymbol), codeName, -1, _objectCheckCode.CheckCode);
        Response.ContentType = "image/Gif";
    }
}