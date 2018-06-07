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
        public string Action(string uid, string pwd)
        {
            Dictionary<string, string> activeParams = new Dictionary<string, string>();
            activeParams.Add("name", uid);
            activeParams.Add("password", pwd);
            if (VerifyNotEmpty(activeParams))
            {
                InitApiConfigs();
                LoadSPS(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
                DataTable dtUser = new DataTable();
                dtUser = ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
                if (dtUser != null && dtUser.Rows.Count == 1)
                {
                    
                    Response.Cookies
                    return MessageHelper.ExecuteSucessful();
                }
                else
                    return MessageHelper.ExecuteFalse();
            }
            else
                return MessageHelper.ExecuteSucessful();
        }
    }
}