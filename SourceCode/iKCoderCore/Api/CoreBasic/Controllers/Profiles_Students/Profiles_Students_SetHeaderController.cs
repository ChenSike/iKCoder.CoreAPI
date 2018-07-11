using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/json")]
    [Route("api/Profiles_Students_SetHeader")]
    public class Profiles_Students_SetHeaderController : BaseController_CoreBasic
	{
		[HttpPost]
		public string Action()
		{
			try
			{
				if (verify_logined_token("student_token"))
				{
					var files = Request.Form.Files;
					long fileSize = files.Sum(f => f.Length);
					List<string> filePathResultList = new List<string>();
					foreach (var file in files)
					{
						Global.ItemAccountStudents activeItem = get_SessionObject("student_item") as Global.ItemAccountStudents;
						string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim();
						string filePath = iKCoderComps.FileStore.GetImageStore(activeItem.id);
						fileName = Guid.NewGuid() + "." + fileName.Split('.')[1];
						Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
						paramsMap_for_profle.Add("@id", activeItem.id);
						paramsMap_for_profle.Add("@header", fileName);
						if (ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle))
						{							
							string fileFullName = filePath + fileName;
							using (FileStream fs = System.IO.File.Create(fileFullName))
							{
								file.CopyTo(fs);
								fs.Flush();
							}							
						}
						else
						{
							return MessageHelper.ExecuteFalse();
						}
					}
					return MessageHelper.ExecuteSucessful();
				}
				else
				{
					return MessageHelper.ExecuteFalse();
				}
			}
			catch(Basic_Exceptions err)
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