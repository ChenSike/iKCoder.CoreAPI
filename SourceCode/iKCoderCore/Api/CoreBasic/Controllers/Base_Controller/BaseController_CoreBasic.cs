using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.Xml;
using System.Data;
using MySql.Data;
using iKCoderComps;

namespace CoreBasic.Controllers
{
	public class BaseController_CoreBasic : ControllerBase_Std
	{
		public bool verify_logined_token(string token_name)
		{
			string tokenFromClient = get_ClientToken(token_name);
			return Global.LoginServices.Verify(tokenFromClient);
		}
	}
}
