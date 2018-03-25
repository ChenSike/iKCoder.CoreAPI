using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.iKCoder.Account.Teacher
{
    [Produces("application/json")]
    [Route("ikcoder/account/teacher/login")]
    public class LoginController : BaseController.BaseController
    {
        [HttpGet]
        public void Get(string name,string pwd)
        {
            List<string> lParams = new List<string>();
            lParams.Add(name);
            lParams.Add(pwd);
            if (VerifyNotEmpty(lParams))
            {
                InitApiConfigs();
                ConnectDB();
                LoadSPS();
                Dictionary<string, string> dParams = new Dictionary<string, string>();
                dParams.Add("name", name);
                dParams.Add("pwd", pwd);
                dParams.Add("regfrom", regFrom.ToString());
                dParams.Add("status", "0");
                string result = AssetExecute(ExecuteInsert(key_db_basic, SPSControler.sp_pool_teacher, dParams));
                CloseDB();
                return result;
            }
            else
            {
                return ExecuteFalse();
            }
        }
    }
}