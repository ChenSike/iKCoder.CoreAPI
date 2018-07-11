using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/json")]
    [Route("api/Profiles_Students_GetTextInfo")]
    public class Profiles_Students_GetTextInfoController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action()
		{
			if(verify_logined_token("student_token"))
			{
				Global.ItemAccountStudents activeItem = get_SessionObject("student_item") as Global.ItemAccountStudents;
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@id", activeItem.id);
				DataTable dtData = ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				if (dtData!=null && dtData.Rows.Count>0)
				{
					string nickName = string.Empty;
					string birthday = string.Empty;
					string country = string.Empty;
					string state = string.Empty;
					string city = string.Empty;
					Dictionary<string, string> resturnMap = new Dictionary<string, string>();
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "nickname", out nickName);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "birthday", out birthday);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "country", out country);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "state", out state);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "city", out city);
					if (!string.IsNullOrEmpty(nickName))
						resturnMap.Add("nickname", nickName);
					if (!string.IsNullOrEmpty(birthday))
						resturnMap.Add("birthday", birthday);
					if (!string.IsNullOrEmpty(country))
						resturnMap.Add("country", country);
					if (!string.IsNullOrEmpty(state))
						resturnMap.Add("state", state);
					if (!string.IsNullOrEmpty(city))
						resturnMap.Add("city", city);
					return MessageHelper.ExecuteSucessfulDoc(resturnMap);
				}
				else
				{
					return MessageHelper.ExecuteFalse();
				}
			}
			else
			{
				return MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]);
			}
		}
    }
}