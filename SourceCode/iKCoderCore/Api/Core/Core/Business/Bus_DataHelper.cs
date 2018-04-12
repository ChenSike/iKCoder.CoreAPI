using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iKCoderSDK;
using System.Data;

namespace Core.Business
{
    public class Bus_StoreOperationHelper
    {

        private string key_db_store = "ikcoder_store";

        private string spa_operation_db_reg = "spa_operation_db_reg";
        

        protected Dictionary<String, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();
        public Data_dbSqlHelper refSqlHelperObject
        {
            set;
            get;
        }

        public class_Data_SqlConnectionHelper refConnectionObject
        {
            set
            {
                this.refConnectionObject = value;
                Map_SPS = this.refSqlHelperObject.ActionAutoLoadingAllSPS(refConnectionObject.Get_ActiveConnection(key_db_store), "");
            }
            get
            {
                return refConnectionObject;
            }
        }

        


        public string GetIndex_RegDB()
        {

            refSqlHelperObject.
        }

    }
}
