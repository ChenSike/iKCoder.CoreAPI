using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Account_Students
{
	[Produces("application/text")]
	[Route("api/Account_Students_Logout")]
	public class Account_Students_LogoutController : ControllerBase_Std
	{
		[HttpGet]
		public ContentResult actionResult()
		{
			if (Global.LoginServices.verify_logined_token(_appLoader. get_ClientToken(Request,"student_token")))
			{
				Global.LoginServices.Clear();
				return Content(MessageHelper.ExecuteSucessful());
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
	}
}