using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBasic.Global
{
    public class PoolsLogined
    {
        public const int warning_line = 1000;        
        public static int cleard_span_minutes = 10;
        public static DateTime cleared_time;
        public static Dictionary<string, ItemAccountStudents> Pools_Account_Students = new Dictionary<string, ItemAccountStudents>();

        public static int expired_hours = 12;

        public static ItemAccountStudents CreateNewItem(string uid,string name,string password,object value)
        {
            ItemAccountStudents newStudentsItem = new ItemAccountStudents();
            newStudentsItem.id = uid;
            newStudentsItem.token = Guid.NewGuid().ToString();
            newStudentsItem.password = password;
            newStudentsItem.regedtime = DateTime.Now;
            newStudentsItem.value = value;
            return newStudentsItem;
        }

        public static void push(ItemAccountStudents newItem)
        {
            lock (Pools_Account_Students)
            {
                if (Pools_Account_Students.Count == 0)
                    cleared_time = DateTime.Now;
                TimeSpan clearSpan = DateTime.Now - cleared_time;
                if (clearSpan.Minutes >= cleard_span_minutes)
                {
                    int tryTimes = 1;
                    while(Pools_Account_Students.Count>=warning_line)
                    {
                        clear(expired_hours / tryTimes);
                        cleared_time = DateTime.Now;
                        tryTimes++;
                    }
                }
                Pools_Account_Students.Add(newItem.token, newItem);
            }
        }

		public static bool setvalue(string token,object newvalue)
		{
			ItemAccountStudents exsitedItem = pull(token);
			if (exsitedItem != null)
			{
				exsitedItem.value = newvalue;
				return true;
			}
			else
				return false;
		}

		public static object getvalue(string token)
		{
			ItemAccountStudents exsitedItem = pull(token);
			if (exsitedItem != null)
			{
				return exsitedItem.value;
			}
			else
			{
				return null;
			}
		}

		public static bool setvaluemap(string token,string mapname,string mapvalue)
		{
			ItemAccountStudents exsitedItem = pull(token);
			if (exsitedItem != null)
			{
				if(exsitedItem.valuesMap.ContainsKey(mapname))
				{
					exsitedItem.valuesMap[mapname] = mapvalue;
				}
				else
				{
					exsitedItem.valuesMap.Add(mapname, mapvalue);
				}
				return true;
			}
			else
				return false;
		}

		public static object getvaluemap(string token,string mapname)
		{
			ItemAccountStudents exsitedItem = pull(token);
			if (exsitedItem != null)
			{
				if (exsitedItem.valuesMap.ContainsKey(mapname))
				{
					return exsitedItem.valuesMap[mapname];
				}
				else
				{
					return null;
				}
			}
			else
				return null;
		}

        public static ItemAccountStudents pull(string token)
        {
            if (Pools_Account_Students.ContainsKey(token))
                return Pools_Account_Students[token];
            else
                return null;
        }

     
        public static bool existed(string token)
        {
            if (Pools_Account_Students.ContainsKey(token))
                return true;
            else
                return false;
        }

        public static void clear(int expired_hours)
        {
            foreach (string activeAccountItem in Pools_Account_Students.Keys)
            {
                TimeSpan ts = DateTime.Now - Pools_Account_Students[activeAccountItem].regedtime;
                if (ts.Hours > expired_hours)
                {
                    Pools_Account_Students.Remove(activeAccountItem);
                }
            }
            
        }


    }
}
