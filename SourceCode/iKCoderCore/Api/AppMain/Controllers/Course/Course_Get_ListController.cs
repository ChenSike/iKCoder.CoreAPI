using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Course
{
	[Route("api/Course_Get_List")]
	[ApiController]
    public class Course_Get_ListController : BaseController.BaseController_AppMain
    {
		[HttpGet]
		public ContentResult Action(string course_name)
		{
			try
			{
				if (VerifyToken())
				{
					_appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					_appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN);
					Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
					paramsForBasic.Add("@course_name", course_name);
					DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_main, paramsForBasic);
					return Content(Data_dbDataHelper.ActionConvertDTtoXMLString(dtData));
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]));
				}
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}
			finally
			{
				CloseDB();
			}
		}
    }
}