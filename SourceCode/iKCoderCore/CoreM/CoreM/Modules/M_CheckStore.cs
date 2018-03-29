using System;
using System.Collections.Generic;
using System.Text;
using iKCoderSDK;
using System.Data;
using System.Xml;
using System.Threading;

namespace CoreM.Modules
{
    public class M_CheckStore
    {
        class_Data_SqlConnectionHelper db_objectConnectionHelper = new class_Data_SqlConnectionHelper();
        Data_dbSqlHelper db_objectSqlHelper = new Data_dbSqlHelper();
        Dictionary<String, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();
        Thread currentThread;

        public void Init(Basic_Config refConfigObject)
        {
            XmlNode sessionNode = refConfigObject.GetSessionNode("coreMDB");
            string server = refConfigObject.GetAttrValue(sessionNode, "server");
            string uid = refConfigObject.GetAttrValue(sessionNode, "uid");
            string pwd = refConfigObject.GetAttrValue(sessionNode, "pwd");
            string db = refConfigObject.GetAttrValue(sessionNode, "db");
            ConsoleMessageItem newMessageItem = new ConsoleMessageItem();
            newMessageItem.Message = "[Connect to store database]";
            newMessageItem.Result = true;
            Program.obj_message.set_newMessage(newMessageItem);
            db_objectConnectionHelper.Set_NewConnectionItem(Program.key_db_ikcoder_store, server, uid, pwd, db, enum_DatabaseType.MySql);
            newMessageItem.Message = "[Loading SPS for store]";
            newMessageItem.Result = true;
            Program.obj_message.set_newMessage(newMessageItem);
            Map_SPS = db_objectSqlHelper.ActionAutoLoadingAllSPS(db_objectConnectionHelper.Get_ActiveConnection(Program.key_db_ikcoder_store), "");
            
        }

        public void Start()
        {
            class_Data_SqlSPEntry = Map_SPS[""];
            while(true)
            {
                
            }
        }



    }
}
