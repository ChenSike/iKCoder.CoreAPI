using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account
{
    [Route("iKCoder/Account/Create")]
    public class CreateController : BaseController.BaseController
    {
        [HttpGet]
        public void Get(string name,string pwd,string level)
        {
            InitApiConfigs();
        }

    }
}