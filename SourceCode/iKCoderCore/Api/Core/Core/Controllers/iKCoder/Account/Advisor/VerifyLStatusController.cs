using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Advisor
{
    [Produces("application/json")]
    [Route("ikcoder/account/advisor/VerifyLStatus")]
    public class VerifyLStatusController : BaseController.BaseController
    {
        [HttpGet]
        public string Get()
        {
            if (Request.Cookies.ContainsKey("alid"))
            {
                string alid_fromcookie = Request.Cookies["alid"].ToString();
                if (HttpContext.Session.Keys.Contains("alid"))
                {
                    string alid_fromsession = HttpContext.Session.GetString("alid");
                    if (alid_fromcookie == alid_fromsession)
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