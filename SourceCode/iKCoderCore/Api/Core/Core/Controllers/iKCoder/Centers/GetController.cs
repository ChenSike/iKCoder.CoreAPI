using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Core.Controllers.iKCoder.Centers
{
    [Produces("application/json")]
    [Route("ikcoder/center/get")]
    public class GetController : BaseController.BaseController
    {
        [HttpGet]
        public string Get(string name)
        {
            List<string> lParams = new List<string>();
            lParams.Add(name);
            if(VerifyNotEmpty(lParams))
            {
                InitApiConfigs();
                ConnectDB(key_db_basic);
                LoadSPS();
                Dictionary<string, string> dParams = new Dictionary<string, string>();
                dParams.Add("name", name);
                DataTable dtResult = ExecuteSelectWithMixedConditions(key_db_basic, SPSController.sp_pool_center, dParams);
                if (dtResult == null)
                    return MessageHelper.MessageHelper.ExecuteFalse();
                else
                {
                    return MessageHelper.MessageHelper.TransDatatableToXML(dtResult);
                }
            }
            else
            {
                return MessageHelper.MessageHelper.ExecuteFalse(MessageHelper.MsgCode.MsgCode_Centers.MSGCODE_CENTERS_GET_EMPTY_NAME, MessageHelper.MsgCode.MsgCode_Centers.MSG_CENTERS_GET_EMPTY_NAME);
            }
        }
    }
}