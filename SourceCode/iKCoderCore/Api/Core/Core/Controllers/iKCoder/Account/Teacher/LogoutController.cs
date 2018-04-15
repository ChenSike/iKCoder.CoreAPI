using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Teacher
{
    [Produces("application/json")]
    [Route("api/Logout")]
    public class LogoutController : Controller
    {
        [HttpGet]
        public string Get()
        {
            if (Request.Cookies.Keys.Contains("tlid"))
            {
                if (HttpContext.Session.Keys.Contains("tlid"))
                {
                    string tlid_session = HttpContext.Session.Get("tlid").ToString();
                    string tlid_cookie = Request.Cookies["tlid"].ToString();
                    if (tlid_cookie == tlid_session)
                    {
                        HttpContext.Session.Remove("tlid");
                        Response.Cookies.Delete("tlid");
                        return MessageHelper.MessageHelper.ExecuteSucessful();
                    }
                    else
                    {
                        HttpContext.Session.Remove("tlid");
                        return MessageHelper.MessageHelper.ExecuteFalse();
                    }

                }
                else
                    return MessageHelper.MessageHelper.ExecuteFalse();
            }
            else
            {
                return MessageHelper.MessageHelper.ExecuteFalse();
            }
        }

    }
}