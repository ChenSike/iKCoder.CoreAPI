using System;
using System.Collections.Generic;
using System.Text;
using iKCoderSDK;

namespace CoreInit
{
    public class M_DB:M_Base
    {

        private class_Data_SqlConnectionHelper _connectionHelper = new class_Data_SqlConnectionHelper();
        private Data_dbSqlHelper _sqlHelper = new Data_dbSqlHelper();
        private class_data_PlatformDBConnection _activeConnection;

        public override void Input()
        {
            Console.Write("M_DB@server:");
            this.db_server = Console.ReadLine();
            Console.Write("M_DB@user:");
            this.db_uid = Console.ReadLine();
            Console.Write("M_DB@passowrd:");
            this.db_pwd = Console.ReadLine();
            Console.Write("M_DB@database:");
            this.db_database = Console.ReadLine();
			Console.Write("M_DB@want to create sps map (yes/no):");
			if(Console.ReadLine()=="yes")
			{
				Console.Write("M_DB@spsmap file path:");
				this.db_spsmappath = Console.ReadLine();
			}
        }

        public string db_server
        {
            set;
            get;
        }

        public string db_uid
        {
            set;
            get;
        }

        public string db_pwd
        {
            set;
            get;
        }

        public string db_database
        {
            set;
            get;
        }

		public string db_spsmappath
		{
			set;
			get;
		}

        public bool ConnectTo()
        {
            string flagForConnection = db_server + "@" + db_database;
            if (_connectionHelper.Set_NewConnectionItem(flagForConnection, db_server, db_uid, db_pwd, db_database, enum_DatabaseType.MySql))
            {
                _activeConnection = _connectionHelper.Get_ActiveConnection(flagForConnection);
                return true;
            }
            else
                return false;
        }

        public bool SwitchActiveConnection(string server,string database)
        {
            string flagForConnection = server + "@" + database;
            _activeConnection = _connectionHelper.Get_ActiveConnection(flagForConnection);
            if (_activeConnection != null)
            {               
                return true;
            }
            else
                return false;
        }

        public bool CreateAutoSPS()
        {
			if(this.db_spsmappath==string.Empty)		
				return _sqlHelper.ActionAutoCreateSPS(_activeConnection);
			else
				return _sqlHelper.ActionAutoCreateSPS(_activeConnection,this.db_spsmappath);
		}



    }
}
