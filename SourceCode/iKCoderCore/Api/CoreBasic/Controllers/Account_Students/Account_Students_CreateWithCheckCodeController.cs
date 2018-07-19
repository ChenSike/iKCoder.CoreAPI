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
	[Produces("application/text")]
	[Route("api/Account_Students_CreateWithCheckCode")]
	public class Account_Students_CreateWithCheckCodeController : ControllerBase_Std
	{
		[HttpGet]
		public string actionResult(string uid, string pwd, string checkcode, string status = "0", string level = "0")
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", uid);
				activeParams.Add("password", pwd);
				
				if (_appLoader.VerifyNotEmpty(activeParams))
				{
					_appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
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
					_appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					_appLoader.LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
					DataTable activeDataTable = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					_appLoader.CloseDB();
					if (activeDataTable == null)
						return MessageHelper.ExecuteFalse();
					else
					{
						if (activeDataTable.Rows.Count > 0)
						{
							return MessageHelper.ExecuteFalse("400", "Account Existed");
						}
					}
					if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams))
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
				_appLoader.CloseDB();
			}
		}
	}
}