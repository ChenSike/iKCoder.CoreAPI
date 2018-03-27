using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Core.Controllers.iKCoder.Account.Teacher
{
    [Produces("application/json")]
    [Route("ikcoder/account/teacher/login")]
    public class LoginController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string name,string pwd)
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
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSController.sp_pool_teacher, dParams);
                string strResult = string.Empty;
                if(dtResult!=null && dtResult.Rows.Count>0)
                {
                    string id = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("tlid", id);
                    strResult = MessageHelper.MessageHelper.ExecuteSucessful();
                }
                else
                {
                    strResult = MessageHelper.MessageHelper.ExecuteFalse();
                }
                CloseDB();
                return strResult;
            }
            else
            {
                return MessageHelper.MessageHelper.ExecuteFalse();
            }
        }
    }
}