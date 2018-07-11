using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBasic.Global
{

	public class ItemAccountStudents
	{

		public static ItemAccountStudents CreateNewItem(string uid, string name, string password, object value)
		{
			ItemAccountStudents newStudentsItem = new ItemAccountStudents();
			newStudentsItem.id = uid;
			newStudentsItem.token = Guid.NewGuid().ToString();
			newStudentsItem.password = password;
			newStudentsItem.regedtime = DateTime.Now;
			newStudentsItem.lastVisted = DateTime.Now;
			newStudentsItem.value = value;
			return newStudentsItem;
		}

		public string name
		{
			set;
			get;
		}

		public string id
		{
			set;
			get;
		}

		public string token
		{
			set;
			get;
		}

		public string password
		{
			set;
			get;
		}

		public DateTime regedtime
		{
			set;
			get;
		}

		public DateTime lastVisted
		{
			set;
			get;
		}

		public Dictionary<string, object> valuesMap
		{
			set;
			get;
		}

		public object value
		{
			set;
			get;
		}

	}

	public class LoginServices
    {
		public static Dictionary<string, ItemAccountStudents> Pool_Logined = new Dictionary<string, ItemAccountStudents>();
		public static int SPAN_EXPIRED_HOURS = 4;
		public static int SPAN_REG_HOURS = 24;

		public static void Flush(ref ItemAccountStudents ActiveItem)
		{
			if(ActiveItem!=null)
			{
				ActiveItem.lastVisted = DateTime.Now;
				Pool_Logined[ActiveItem.token] = ActiveItem;
			}
		}

		public static void Clear(string token)
		{
			if(Verify(token))
			{
				Pool_Logined.Remove(token);
			}
		}

		public static void Clear()
		{
			foreach(string token in Pool_Logined.Keys)
			{
				ItemAccountStudents activeItem = Pool_Logined[token];
				if((activeItem.regedtime - DateTime.Now).Hours > SPAN_REG_HOURS)
				{
					lock(Pool_Logined)
					{
						Pool_Logined.Remove(activeItem.token);
					}
				}
				else if((activeItem.lastVisted - DateTime.Now).Minutes>SPAN_EXPIRED_HOURS)
				{
					lock (Pool_Logined)
					{
						Pool_Logined.Remove(activeItem.token);
					}
				}
			}
		}

		public static void Push(ItemAccountStudents ActiveItem)
		{
			if (!Pool_Logined.ContainsKey(ActiveItem.token))
				Pool_Logined.Add(ActiveItem.token, ActiveItem);
			else
			{
				Flush(ref ActiveItem);
				Pool_Logined[ActiveItem.token] = ActiveItem;
			}
		}

		public static ItemAccountStudents Pull(string token)
		{
			if (!Pool_Logined.ContainsKey(token))
				return Pool_Logined[token];
			else
				return null;
		}

		public static bool Verify(string token)
		{
			Clear();
			if (Pool_Logined.ContainsKey(token))
				return true;
			else
				return false;
		}

    }

}
