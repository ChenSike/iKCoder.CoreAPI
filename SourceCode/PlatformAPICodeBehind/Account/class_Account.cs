using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Data.SqlClient;

namespace PlatformAPICodeBehind.Account
{
    public class class_Account
    {

        SqlConnection _activeSqlConnection;
        Dictionary<string, class_Data_SqlSPEntry> _activeSPList;
        
        public class_Account(SqlConnection activeSqlConnection,Dictionary<string,class_Data_SqlSPEntry> activeSPList)
        {
            _activeSqlConnection = activeSqlConnection;
            _activeSPList = activeSPList;
        }

        public bool Action_CreateNewAccount(string userName , string userPassword)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))
                return false;
            else
            {
                return true;
            }
        }
    }
}
