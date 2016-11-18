using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using iKCoder_Platform_SDK_Kit;
using System.Web;

namespace PlatformAPICodeBehind.Token
{
    public class class_ProductToken
    {        
        public string RegistryToken(System.Web.SessionState.HttpSessionState refSessionPool, class_TokenItem tokenFromClient,XmlNodeList benchmarkTokensList)
        {
            class_Token_Controller _objectTokenController = new class_Token_Controller(refSessionPool);
            if (refSessionPool == null || tokenFromClient == null || benchmarkTokensList == null || benchmarkTokensList.Count <= 0 )
                return "";
            foreach (XmlNode benchmarkTokenNode in benchmarkTokensList)
            {
                string productName = class_XmlHelper.GetAttrValue(benchmarkTokenNode, "name");
                string productCode = class_XmlHelper.GetAttrValue(benchmarkTokenNode, "code");
                string productKey = class_XmlHelper.GetAttrValue(benchmarkTokenNode, "key");
                string productExpired = class_XmlHelper.GetAttrValue(benchmarkTokenNode, "expired");
                int intExpiredValue = 0;
                int.TryParse(productExpired, out intExpiredValue);
                _objectTokenController.AddBenchmarkToken(productName, productCode, productKey, intExpiredValue);
            }
            return _objectTokenController.GetToken(tokenFromClient);
        }

    }
}
