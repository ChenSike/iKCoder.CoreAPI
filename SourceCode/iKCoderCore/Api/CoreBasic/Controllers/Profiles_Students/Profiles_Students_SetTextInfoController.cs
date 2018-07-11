using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Profiles_Students
{
	[Produces("application/json")]
	[Route("api/Profiles_Students_SetTextInfo")]
	public class Profiles_Students_SetTextInfoController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action(string sex, string nickname, string birthday, string state, string city , string country = "China")
		{
			if (verify_logined_token("student_token"))
			{
				Global.ItemAccountStudents activeItem = get_SessionObject("student_item") as Global.ItemAccountStudents;
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@id", activeItem.id);
				if (!string.IsNullOrEmpty(nickname))
					paramsMap_for_profle.Add("@nickname", nickname);
				if (!string.IsNullOrEmpty(birthday))
					paramsMap_for_profle.Add("@birthday", birthday);
				if (!string.IsNullOrEmpty(country))
					paramsMap_for_profle.Add("@country", country);
				if (!string.IsNullOrEmpty(state))
					paramsMap_for_profle.Add("@state", state);
				if (!string.IsNullOrEmpty(city))
					paramsMap_for_profle.Add("@city", city);
				if (ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle))
				{
					return MessageHelper.ExecuteSucessful();
				}
				else
				{
					return MessageHelper.ExecuteFalse();
				}
			}
			else
			{
				return MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]);
			}
		}
	}
}