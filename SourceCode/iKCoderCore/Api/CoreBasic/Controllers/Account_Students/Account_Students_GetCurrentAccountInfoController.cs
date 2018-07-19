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
    [Route("api/Account_Students_GetCurrentAccountInfo")]
    public class Account_Students_GetCurrentAccountInfoController : ControllerBase_Std
    {
		[HttpGet]
		public ContentResult actionResult()
		{
			if (Global.LoginServices.verify_logined_token(_appLoader. get_ClientToken(Request, "student_token")))
			{
				string token = _appLoader.get_ClientToken(Request, "student_token");
				if (!string.IsNullOrEmpty(token))
				{
					Global.ItemAccountStudents activeStudentItem = Global.LoginServices.Pull(token);
					return Content(activeStudentItem.createXmlDoc().OuterXml);
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]));
				}
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]));
			}
		}
    }
}