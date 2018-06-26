﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.IO;

namespace CoreBasic.Controllers.Common_Services
{
    [Produces("application/json")]
    [Route("api/Common_Services_NewCheckCode")]
    public class Common_Services_NewCheckCodeController : Controller
    {
		public IActionResult actionResult()
		{
			string checkCode = string.Empty;
			MemoryStream streamMem = Basic_CheckCode.NewCheckCode(out checkCode);
			return File(streamMem.ToArray(), @"image/png");
		}
    }
}