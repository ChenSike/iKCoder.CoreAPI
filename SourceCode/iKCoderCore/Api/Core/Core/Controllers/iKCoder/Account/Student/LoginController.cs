using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Student
{
    [Produces("application/json")]
    [Route("ikcoder/account/student/login")]
    public class LoginController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string name,string pwd)
        {
            List<string> lParams = new List<string>();
            lParams.Add(name);
            lParams.Add(pwd);
            if(VerifyNotEmpty(lParams))
            {

            }
            else
            {
                MessageHelper.MessageHelper.ExecuteFalse();
            }
        }
    }
}