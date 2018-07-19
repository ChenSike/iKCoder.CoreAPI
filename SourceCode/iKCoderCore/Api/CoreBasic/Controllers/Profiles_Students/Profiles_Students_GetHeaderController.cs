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
	[Produces("application/text")]
	[Route("api/Profiles_Students_GetHeader")]
	public class Profiles_Students_GetHeaderController : ControllerBase_Std
	{
		[HttpGet]
		public ContentResult actionResult(string withpath="1")
		{
			try
			{
				if (Global.LoginServices.verify_logined_token(_appLoader.get_ClientToken(Request, "student_token")))
				{
					Global.ItemAccountStudents activeItem = _appLoader.get_SessionObject(HttpContext.Session, "student_item") as Global.ItemAccountStudents;
					Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
					paramsMap_for_profle.Add("@id", activeItem.id);
					DataTable dt = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					if(dt!=null && dt.Rows.Count>0)
					{
						string header = string.Empty;
						Data_dbDataHelper.GetColumnData(dt.Rows[0], "header", out header);
						if(withpath=="1")
						{
							string filePath = iKCoderComps.FileStore.GetImageStore(activeItem.id);
							return Content(MessageHelper.ExecuteSucessful("800", filePath + "\\" + header));
						}
						else
						{
							return Content(MessageHelper.ExecuteSucessful("800", header));
						}
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]));
				}
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
			finally
			{
				_appLoader.CloseDB();
			}
		}

	}
}