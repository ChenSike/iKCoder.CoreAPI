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
                ConnectDB(key_db_basic);
                LoadSPS();
                Dictionary<string, string> dParams = new Dictionary<string, string>();
                dParams.Add("name", name);
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSController.sp_pool_teacher, dParams);
                string strResult = string.Empty;
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    CloseDB();
                    return MessageHelper.MessageHelper.ExecuteFalse(MessageHelper.MsgCode.MsgCode_Teacher.MSGCODE_ACCOUNT_TEACHER_EXISTED, MessageHelper.MsgCode.MsgCode_Teacher.MSG_ACCOUNT_TEACHER_EXISTED);
                }
                dParams.Clear();
                dParams.Add("name", name);
                dParams.Add("pwd", pwd);
                dParams.Add("regfrom", regFrom.ToString());
                dParams.Add("status", "0");
                string result = MessageHelper.MessageHelper.AssetExecute(ExecuteInsert(key_db_basic, SPSController.sp_pool_teacher, dParams));
                CloseDB();
                return result;
            }
            else
            {
                return MessageHelper.MessageHelper.ExecuteFalse();
            }
        }
    }
}