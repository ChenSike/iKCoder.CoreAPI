using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Student
{
    [Produces("application/json")]
    [Route("ikcoder/account/student/VerifyLStatus")]
    public class VerifyLStatusController : BaseController.BaseController
    {
        [HttpGet]
        public string Get()
        {
            if (Request.Cookies.ContainsKey("slid"))
            {
                string slid_fromcookie = Request.Cookies["slid"].ToString();
                if (HttpContext.Session.Keys.Contains("slid"))
                {
                    string slid_fromsession = HttpContext.Session.GetString("slid");
                    if (slid_fromcookie == slid_fromsession)
                    {
                        return MessageHelper.MessageHelper.ExecuteSucessful();
                    }
                    else
                    {
                        return MessageHelper.MessageHelper.ExecuteFalse();
                    }
                }
                else
                {
                    return MessageHelper.MessageHelper.ExecuteFalse();
                }
            }
            else
            {
                return MessageHelper.MessageHelper.ExecuteFalse();
            }
        }

    }
}