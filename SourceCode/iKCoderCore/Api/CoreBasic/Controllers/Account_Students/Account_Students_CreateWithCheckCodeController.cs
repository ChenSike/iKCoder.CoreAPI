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
	[Route("api/Account_Students_CreateWithCheckCode")]
	public class Account_Students_CreateWithCheckCodeController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action(string uid, string pwd, string checkcode, string status = "0", string level = "0")
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", uid);
				activeParams.Add("password", pwd);
				if (VerifyNotEmpty(activeParams))
				{
					InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
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
					ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
					DataTable activeDataTable = ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					CloseDB();
					if (activeDataTable == null)
						return MessageHelper.ExecuteFalse();
					else
					{
						if (activeDataTable.Rows.Count > 0)
						{
							return MessageHelper.ExecuteFalse("400", "Account Existed");
						}
					}
					if (ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams))
						return MessageHelper.ExecuteSucessful();
					else
						return MessageHelper.ExecuteFalse();
				}
				else
					return MessageHelper.ExecuteFalse();
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