using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.Data;

namespace Core.Controllers.iKCoder.Account.Teacher
{
    [Produces("application/json")]
    [Route("ikcoder/account/teacher/create")]
    public class CreateController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string name, string pwd,int regFrom)
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
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSControler.sp_pool_teacher, dParams);
                string strResult = string.Empty;
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    return ExecuteFalse();
                }
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