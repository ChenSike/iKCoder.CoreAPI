using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Centers
{
    [Produces("application/json")]
    [Route("ikcoder/center/get")]
    public class GetController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string name)
        {
            if(VerifyNotEmpty())
        }
    }
}