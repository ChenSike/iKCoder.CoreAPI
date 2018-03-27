using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Teacher
{
    [Produces("application/json")]
    [Route("api/LoginedCheck")]
    public class LoginedCheckController : Controller
    {
    }
}