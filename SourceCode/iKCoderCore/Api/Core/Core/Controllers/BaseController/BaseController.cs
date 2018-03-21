using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;

namespace Core.Controllers.BaseController
{
    public class BaseController : Controller
    {

        protected Dictionary<string, Dictionary<string, string>> Map_ApiConfigs = new Dictionary<string, Dictionary<string, string>>();
        protected string Path_Api = string.Empty;

        public void InitApiConfigs()
        {
            Basic_Config objApiConfig = new Basic_Config();
            objApiConfig.DoOpen("\\config\\ikcodercore.basic.xml");
        }

        public string ExecuteSucessful()
        {
            return "Executed";
        }
    }
}