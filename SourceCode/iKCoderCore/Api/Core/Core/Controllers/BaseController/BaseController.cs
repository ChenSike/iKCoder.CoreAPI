using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.BaseController
{
    public class BaseController : Controller
    {
        
        public string ExecuteSucessful()
        {
            return "Executed";
        }
    }
}