﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;
using System.Xml;

namespace AppMain.Controllers.Title
{
    [Route("api/Title_Get_List")]
    [ApiController]
    public class Titles_Get_ListController : BaseController.BaseController_AppMain
    {
		public ContentResult Action()
		{
			try
			{
				if (VerifyToken())
				{
					InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN);
					string accoutInfo = RequestForString("Account_Students_GetCurrentAccountInfo", true);
					XmlDocument accountInfoDoc = new XmlDocument();
					accountInfoDoc.LoadXml(accoutInfo);
					XmlNode rootNode = accountInfoDoc.SelectSingleNode("/root");
					if (rootNode != null)
					{
						string id = Util_XmlOperHelper.GetAttrValue(rootNode, "id");
						Dictionary<string, string> lstParams = new Dictionary<string, string>();
						lstParams.Add("uid", id);
						DataTable dtExp = ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_exp, lstParams);
						if (dtExp != null && dtExp.Rows.Count > 0)
						{
							DataRow currentRow = null;
							Data_dbDataHelper.GetActiveRow(dtExp, 0, out currentRow);
							string strExp = string.Empty;
							Data_dbDataHelper.GetColumnData(currentRow, "exp", out strExp);
							int currentExp = 0;
							int.TryParse(strExp, out currentExp);
							DataTable dtData = ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_titles_defined);
							


							return Content(Data_dbDataHelper.ActionConvertDTtoXMLString(dtData));
						}
						else
						{
							return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
						}
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
					}
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