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
	[Route("api/Account_Students_Existed")]
	public class Account_Students_ExistedController : ControllerBase_Std
	{
		[HttpGet]
		public ContentResult actionResult(string name)
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", name);
				if (_appLoader.VerifyNotEmpty(activeParams))
				{
					_appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					_appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					_appLoader.LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
					DataTable activeDataTable = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					_appLoader.CloseDB();
					if (activeDataTable == null)
						return Content(MessageHelper.ExecuteFalse());
					else
					{
						if (activeDataTable.Rows.Count > 0)
						{
							return Content(MessageHelper.ExecuteSucessful());
						}
						else
						{
							return Content(MessageHelper.ExecuteFalse());
						}
					}
				}
				else
					return Content(MessageHelper.ExecuteFalse());
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
			finally
			{
				_appLoader.CloseDB();
			}
		}
	}
}