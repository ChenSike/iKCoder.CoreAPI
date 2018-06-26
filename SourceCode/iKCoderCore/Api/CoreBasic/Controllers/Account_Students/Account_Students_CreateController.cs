using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers
{
    [Produces("application/json")]
    [Route("api/Account_Students_Create")]
    public class Account_Students_CreateController : ControllerBase_Std
    {
        [HttpGet]
        public string Action(string uid, string pwd, string status = "0", string level = "0")
        {
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", uid);
				activeParams.Add("password", pwd);
				if (VerifyNotEmpty(activeParams))
				{
					InitApiConfigs();
					ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					LoadSPS(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					if (ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams))
						return MessageHelper.ExecuteSucessful();
					else
						return MessageHelper.ExecuteFalse();
				}
				else
					return MessageHelper.ExecuteFalse();
			}
			catch(Basic_Exceptions err)
			{
				return MessageHelper.ExecuteFalse();
			}

        }
    }
}