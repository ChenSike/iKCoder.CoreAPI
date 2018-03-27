using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using iKCoderSDK;

namespace Core.Controllers.iKCoder.Account.Student
{
    [Produces("application/json")]
    [Route("ikcoder/account/student/create")]
    public class CreateController :BaseController.BaseController
    {
        [HttpGet]
        public string Get(string uid,string pwd,string nickname,string realname,string sex,string birthdate,string school,string tel,string cid,string pid)
        {
            List<string> lParams = new List<string>();
            lParams.Add(uid);
            lParams.Add(pwd);
            lParams.Add(nickname);
            lParams.Add(realname);
            lParams.Add(sex);
            lParams.Add(school);
            lParams.Add(tel);
            lParams.Add(cid);
            lParams.Add(pid);
            if (VerifyNotEmpty(lParams))
            {
                InitApiConfigs();
                ConnectDB();
                LoadSPS();
                Dictionary<string, string> dParams = new Dictionary<string, string>();
                dParams.Add("name", uid);
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSController.sp_pool_students, dParams);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    CloseDB();
                    return MessageHelper.MessageHelper.ExecuteFalse(MessageHelper.MsgCode.MsgCode_Student.MSGCODE_ACCOUNT_STUDENT_CREATE_EXISTED, MessageHelper.MsgCode.MsgCode_Student.MSG_ACCOUNT_STUDENT_CREATE_EXISTED);
                }
                dParams.Clear();
                dParams.Add("sid", Util_Common.GuidToLongID().ToString());
                dParams.Add("uid", uid);
                dParams.Add("pwd", pwd);
                dParams.Add("nickname",nickname);
                dParams.Add("realname", realname);
                dParams.Add("sex", sex);
                dParams.Add("birthdate", birthdate);
                dParams.Add("school", school);
                dParams.Add("tel", tel);
                dParams.Add("cid", cid);
                dParams.Add("pid", pid);
                dParams.Add("status", "0");
                string result = MessageHelper.MessageHelper.AssetExecute(ExecuteInsert(key_db_basic, SPSController.sp_pool_students, dParams));
                CloseDB();
                return result;
            }
            else
                return MessageHelper.MessageHelper.ExecuteFalse();
        }
    }
}