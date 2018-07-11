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
	[Produces("application/json")]
	[Route("api/Account_Students_Logout")]
	public class Account_Students_LogoutController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action()
		{
			if (verify_logined_token("student_token"))
			{
				Global.LoginServices.Clear();
				return MessageHelper.ExecuteSucessful();
			}
			else
			{
				return MessageHelper.ExecuteFalse();
			}
		}
	}
}