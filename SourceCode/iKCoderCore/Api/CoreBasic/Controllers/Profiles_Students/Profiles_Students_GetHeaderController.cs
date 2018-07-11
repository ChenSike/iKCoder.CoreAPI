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
	[Route("api/Profiles_Students_GetHeader")]
	public class Profiles_Students_GetHeaderController : BaseController_CoreBasic
	{
		[HttpGet]
		public string Action(string withpath="1")
		{
			try
			{
				if (verify_logined_token("student_token"))
				{
					Global.ItemAccountStudents activeItem = get_SessionObject("student_item") as Global.ItemAccountStudents;
					Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
					paramsMap_for_profle.Add("@id", activeItem.id);
					DataTable dt = ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					if(dt!=null && dt.Rows.Count>0)
					{
						string header = string.Empty;
						Data_dbDataHelper.GetColumnData(dt.Rows[0], "header", out header);
						if(withpath=="1")
						{
							string filePath = iKCoderComps.FileStore.GetImageStore(activeItem.id);
							return MessageHelper.ExecuteSucessful("800", filePath + "\\" + header);
						}
						else
						{
							return MessageHelper.ExecuteSucessful("800", header);
						}
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
			catch (Basic_Exceptions err)
			{
				return MessageHelper.ExecuteFalse();
			}
			finally
			{
				CloseDB();
			}
		}

	}
}