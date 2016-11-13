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
        
        public class_Account(SqlConnection activeSqlConnection)
        {
            _activeSqlConnection=activeSqlConnection;
        }

        public bool Action_CreateNewAccount(string userName , string userPassword)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPassword))
                return false;
        }
    }
}
