using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Core.Controllers.iKCoder.Account.Student
{
    [Produces("application/json")]
    [Route("ikcoder/account/student/login")]
    public class LoginController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string uid,string pwd)
        {
            List<string> lParams = new List<string>();
            lParams.Add(uid);
            lParams.Add(pwd);
            if(VerifyNotEmpty(lParams))
            {
                InitApiConfigs();
                ConnectDB(key_db_basic);
                LoadSPS();
                Dictionary<string, string> dParams = new Dictionary<string, string>();
                dParams.Add("uid", uid);
                dParams.Add("pwd", pwd);
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSController.sp_pool_students, dParams);
                string strResult = string.Empty;
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    string id = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("slid", id);
                    Response.Cookies.Append("slid", id);
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