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
	[Route("api/Course_Get_ListController")]
	[ApiController]
    public class Course_Get_ListController : BaseController.BaseController_AppMain
    {
		[HttpGet]
		public string Action()
		{
			try
			{
				if (VerifyToken())
				{
					InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN);
					DataTable dtData = ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_basic.spa_operation_course_basic);
					return Data_dbDataHelper.ActionConvertDTtoXMLString(dtData);
				}
				else
				{
					return MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]);
				}
			}
			catch
			{
				return MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]);
			}
			finally
			{
				CloseDB();
			}
		}
    }
}