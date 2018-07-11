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
    [Route("api/Account_Students_LoginWithCheckCode")]
    public class Account_Students_LoginWithCheckCodeController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action(string name, string pwd,string checkcode)
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", name);
				activeParams.Add("password", pwd);
				if (VerifyNotEmpty(activeParams))
				{
					string checkcodefromsession = string.Empty;
					if (HttpContext.Session.Keys.Contains("checkcode"))
					{
						checkcodefromsession = HttpContext.Session.GetString("checkcode");
					}
					else
					{
						return MessageHelper.ExecuteFalse("400", "null checkcode");
					}
					if (checkcodefromsession != checkcode)
					{
						return MessageHelper.ExecuteFalse("400", "wrong checkcode");
					}
					InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
					DataTable dtUser = new DataTable();
					dtUser = ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					CloseDB();
					if (dtUser != null && dtUser.Rows.Count == 1)
					{
						string uid = string.Empty;
						Data_dbDataHelper.GetColumnData(dtUser.Rows[0], "id", out uid);
						Global.ItemAccountStudents newItem = Global.ItemAccountStudents.CreateNewItem(uid, name, pwd, "");
						Global.LoginServices.Push(newItem);
						Response.Cookies.Append("student_token", newItem.token);
						return MessageHelper.ExecuteSucessful();
					}
					else
						return MessageHelper.ExecuteFalse();
				}
				else
					return MessageHelper.ExecuteSucessful();
			}
			catch (Basic_Exceptions err)
			{
				return MessageHelper.ExecuteFalse();
			}
			finally
			{
				CloseDB();
			}
		}
	}
}