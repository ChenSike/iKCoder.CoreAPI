using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iKCoderSDK;
using System.Data;

namespace Core.Business
{

    public class Bus_StoreRegServerItem
    {
        public string db_key
        {
            set;
            get;
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

        public string db_db
        {
            set;
            get;
        }

        public string db_tindex
        {
            set;
            get;
        }

        public int db_rows
        {
            set;
            get;
        }

    }

    public class Bus_StoreOperationHelper
    {
        
        private string key_db_store = "ikcoder_store";

        private string spa_operation_db_reg = "spa_operation_db_reg";
        private string spa_operation_router_t1 = "spa_operation_router_t1";
        private string spa_operation_router_t2 = "spa_operation_router_t2";
        private string spa_operation_router_t3 = "spa_operation_router_t3";
        private string spa_operation_router_t4 = "spa_operation_router_t4";
        private DataTable regDataTable;

        protected Dictionary<string, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();
        protected Dictionary<string, Bus_StoreRegServerItem> Map_RegServers = new Dictionary<string, Bus_StoreRegServerItem>();


        public Data_dbSqlHelper refSqlHelperObject
        {
            set;
            get;
        }

        public class_Data_SqlConnectionHelper Map_ConnectionsForREG = new class_Data_SqlConnectionHelper();

        public class_Data_SqlConnectionHelper refConnectionObject
        {
            set
            {
                this.refConnectionObject = value;
                Map_SPS = this.refSqlHelperObject.ActionAutoLoadingAllSPS(refConnectionObject.Get_ActiveConnection(key_db_store), "");
                class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spa_operation_db_reg];
                objSPEntry.ClearAllParamsValues();
                regDataTable = refSqlHelperObject.ExecuteSelectSPForDT(objSPEntry, refConnectionObject, key_db_store);   
                foreach(DataRow regRow in regDataTable.Rows)
                {
                    Bus_StoreRegServerItem newItem = new Bus_StoreRegServerItem();
                    string db_key = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "dbkey", out db_key);
                    string db_server = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "dbserver", out db_server);
                    string db_uid = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "dbuid", out db_uid);
                    string db_pwd = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "dbpwd", out db_pwd);
                    string db_db = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "dbdatabase", out db_db);
                    string db_tindex = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "tindex", out db_tindex);
                    string db_rows = string.Empty;
                    Data_dbDataHelper.GetColumnData(regRow, "db_rows", out db_rows);
                    newItem.db_key = db_key;
                    newItem.db_db = db_db;
                    newItem.db_pwd = db_pwd;
                    newItem.db_server = db_server;
                    newItem.db_tindex = db_tindex;
                    newItem.db_uid = db_uid;
                    int idbRows = 0;
                    int.TryParse(db_rows, out idbRows);
                    newItem.db_rows = idbRows;
                    Map_RegServers.Add(newItem.db_tindex, newItem);
                }
            }
            get
            {
                return refConnectionObject;
            }
        }



        public string GetIndex_RegDB(int uid)
        {
            int uidIndex = uid % 4;
            string currentSP = GetCurrentSP(uidIndex);
            class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[currentSP];
            objSPEntry.ClearAllParamsValues();
            objSPEntry.ModifyParameterValue("uid", uid);
            regDataTable = refSqlHelperObject.ExecuteSelectSPForDT(objSPEntry, refConnectionObject, key_db_store);
            if (regDataTable == null && regDataTable.Rows.Count == 0)
                return string.Empty;
            else
            {
                string tIndex = string.Empty;
                Data_dbDataHelper.GetColumnData(regDataTable.Rows[0], "tindex", out tIndex);
                return tIndex;
            }

        }

        public void SetFlushIndex_StoreTable(int uid,string tIndex)
        {
            int uidIndex = uid % 4;
            string currentSP = GetCurrentSP(uidIndex);
            class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[currentSP];
            objSPEntry.ClearAllParamsValues();
            objSPEntry.ModifyParameterValue("uid", uid);
            objSPEntry.ModifyParameterValue("tindex", tIndex);
            refSqlHelperObject.ExecuteInsertSP(objSPEntry, refConnectionObject, key_db_store);
        }

        

        public string GetCurrentSP(int uidIndex)
        {
            switch(uidIndex)
            {
                case 0:
                    return spa_operation_router_t1;
                case 1:
                    return spa_operation_router_t2;
                case 2:
                    return spa_operation_router_t3;
                case 3:
                    return spa_operation_router_t4;
                default:
                    return spa_operation_router_t1;
            }
        }

        public string GetNewIndex_RegDB()
        {
            var dicSort = from objDic in Map_RegServers orderby objDic.Value ascending select objDic.Value;
            List<Bus_StoreRegServerItem> resultList = dicSort.ToList<Bus_StoreRegServerItem>();
            return resultList[0].db_tindex;
        }

        public bool PushData(int uid,string symbol,byte[] dataBytes,string additional)
        {
            try
            {
                string tmpData = System.Text.Encoding.Default.GetString(dataBytes);
                string tmpBase64Data = Util_Common.Encoder_Base64(tmpData);
                return PushData(uid, symbol, tmpBase64Data, additional);
            }
            catch
            {
                return false;
            }
        }

        public bool PushData(int uid, string symbol,string dataBase64,string additional)
        {
            if (string.IsNullOrEmpty(dataBase64))
                return false;
            else
            {
                string spa_operation_store = "spa_operation_store";
                string tIndex = GetIndex_RegDB(uid);
                if (string.IsNullOrEmpty(tIndex))
                {
                    tIndex = GetNewIndex_RegDB();
                    SetFlushIndex_StoreTable(uid, tIndex);
                }
                Bus_StoreRegServerItem regItem = Map_RegServers[tIndex];
                Map_ConnectionsForREG.Set_NewConnectionItem(regItem.db_key, regItem.db_server, regItem.db_uid, regItem.db_pwd, regItem.db_db, enum_DatabaseType.MySql);
                Dictionary<string, class_Data_SqlSPEntry> Map_SPS_CurrentStore = new Dictionary<string, class_Data_SqlSPEntry>();
                Map_SPS_CurrentStore = this.refSqlHelperObject.ActionAutoLoadingAllSPS(Map_ConnectionsForREG.Get_ActiveConnection(regItem.db_key), "");
                class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spa_operation_store];
                objSPEntry.ClearAllParamsValues();
                objSPEntry.ModifyParameterValue("symbol", symbol);
                objSPEntry.ModifyParameterValue("uid", uid);
                objSPEntry.ModifyParameterValue("data", dataBase64);
                objSPEntry.ModifyParameterValue("additional", additional);
                bool result = refSqlHelperObject.ExecuteInsertSP(objSPEntry, refConnectionObject, key_db_store);
                Map_ConnectionsForREG.Action_CloseAllActionConnection();
                return result;
            }
        }

        public void DisConnectServices()
        {
            Map_ConnectionsForREG.Action_CloseAllActionConnection();
        }

        



    }
}
