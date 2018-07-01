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
    [Route("api/Account_Students_Login")]
    public class Account_Students_LoginController : ControllerBase_Std
    {
        [HttpGet]
        public string Action(string name, string pwd)
        {
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", name);
				activeParams.Add("password", pwd);
				if (VerifyNotEmpty(activeParams))
				{
					InitApiConfigs();
					LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
					DataTable dtUser = new DataTable();
					dtUser = ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					if (dtUser != null && dtUser.Rows.Count == 1)
					{
						string uid = string.Empty;
						Data_dbDataHelper.GetColumnData(dtUser.Rows[0], "id", out uid);
						Global.ItemAccountStudents activeAccountItem = Global.PoolsLogined.CreateNewItem(uid, name, pwd, "");
						Response.Cookies.Append("token", activeAccountItem.token);
						Global.PoolsLogined.push(activeAccountItem);
						return MessageHelper.ExecuteSucessful();
					}
					else
						return MessageHelper.ExecuteFalse();
				}
				else
					return MessageHelper.ExecuteSucessful();
			}
			catch(Basic_Exceptions err)
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