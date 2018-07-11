using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Account_Students
{
    [Produces("application/json")]
    [Route("api/Account_Students_ChangePWD")]
    public class Account_Students_ChangePWDController : BaseController_CoreBasic
	{
		[HttpPost]
		public string Action(string oldpwd,string newpwd)
		{
			if (verify_logined_token("student_token"))
			{
				Global.ItemAccountStudents activeItem = get_SessionObject("student_item") as Global.ItemAccountStudents;
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@id", activeItem.id);
				paramsMap_for_profle.Add("@password", oldpwd);
				DataTable dtData = ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				if (dtData != null && dtData.Rows.Count > 0)
				{
					paramsMap_for_profle["@password"] = newpwd;
					ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
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