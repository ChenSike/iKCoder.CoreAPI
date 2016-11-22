using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Web;


public class class_ProductToken
{
    public string RegistryToken(System.Web.SessionState.HttpSessionState refSessionPool, XmlDocument requestDoc, XmlNodeList benchmarkTokensList, class_Base_Config refObjectConfig)
    {
        class_Token_Controller _objectTokenController = new class_Token_Controller(refSessionPool);
        if (refSessionPool == null || requestDoc == null || benchmarkTokensList == null || benchmarkTokensList.Count <= 0)
            return "";
        class_TokenItem tokenFromClient = new class_TokenItem();
        string productNameFromClient = class_XmlHelper.GetNodeValue(requestDoc, "/root/name");
        string productCodeFromClient = class_XmlHelper.GetNodeValue(requestDoc, "/root/code");
        tokenFromClient.productName = productNameFromClient;
        tokenFromClient.productCode = productCodeFromClient;
        foreach (XmlNode benchmarkTokenNode in benchmarkTokensList)
        {
            refObjectConfig.SwitchToDESModeOFF();
            string productName = refObjectConfig.GetAttrValue(benchmarkTokenNode, "name");
            string productExpired = class_XmlHelper.GetAttrValue(benchmarkTokenNode, "expired");
            refObjectConfig.SwitchToDESModeON();
            string productCode = refObjectConfig.GetAttrValue(benchmarkTokenNode, "code");
            int intExpiredValue = 0;
            int.TryParse(productExpired, out intExpiredValue);
            _objectTokenController.AddBenchmarkToken(productName, productCode, intExpiredValue);
        }
        return _objectTokenController.GetToken(tokenFromClient);
    }

    public bool CheckRegistiedToken(System.Web.SessionState.HttpSessionState refSessionPool, XmlDocument requestDoc)
    {
        if (refSessionPool == null || requestDoc == null)
            return false;
        else
        {
            string tokenGuid = class_XmlHelper.GetNodeValue(requestDoc, "/root/token");
            class_Token_Controller _objectTokenController = new class_Token_Controller(refSessionPool);
            return _objectTokenController.VerifyToken(tokenGuid);
        }
    }

}

