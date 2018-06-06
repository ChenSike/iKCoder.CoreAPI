using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.Xml;
using System.Data;
using MySql.Data;
using Microsoft.AspNetCore.Cors;

namespace iKCoderComps
{
    [EnableCors("AllowSameDomain")]
    public class ControllerBase_Std:Controller
    {
        
        protected Dictionary<string, Dictionary<string, string>> Map_ApiConfigs = new Dictionary<string, Dictionary<string, string>>();
        protected Dictionary<String, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();

        protected string Path_Api = string.Empty;

        protected class_Data_SqlConnectionHelper db_objectConnectionHelper = new class_Data_SqlConnectionHelper();
        protected Data_dbSqlHelper db_objectSqlHelper = new Data_dbSqlHelper();


        public void InitApiConfigs()
        {

            Path_Api = AppContext.BaseDirectory;
            Basic_Config objApiConfig = new Basic_Config();
            objApiConfig.DoOpen(Path_Api + "\\config\\ikcoder.configs.xml");
            XmlNodeList sessionNodes = objApiConfig.GetSessionNodes();
            foreach (XmlNode activeSessionNode in sessionNodes)
            {
                string name = objApiConfig.GetAttrValue(activeSessionNode, "name");                                
                Dictionary<string, string> mapAttrs = new Dictionary<string, string>();
                List<XmlAttribute> sessionAttrs = objApiConfig.GetSessionAttrs(name);
                objApiConfig.SwitchToAESModeON();
                foreach (XmlAttribute activeAttr in sessionAttrs)
                {
                    if (activeAttr.Name != "name")
                        mapAttrs.Add(activeAttr.Name, objApiConfig.GetAttrValue(activeSessionNode, activeAttr.Name));
                }
                Map_ApiConfigs.Add(name, mapAttrs);
            }
        }


        public bool ExecuteInsert(string connectionkey, string spname, Dictionary<string, string> mapparams)
        {
            if (Map_SPS.ContainsKey(spname))
            {
                class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
                objSPEntry.ClearAllParamsValues();
                foreach (string columnname in mapparams.Keys)
                {
                    objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
                }
                return db_objectSqlHelper.ExecuteInsertSP(objSPEntry, db_objectConnectionHelper, connectionkey);
            }
            else
            {
                return false;
            }
        }

        public DataTable ExecuteSelectWithMixedConditions(string connectionkey, string spname, Dictionary<string, string> mapparams)
        {
            if (Map_SPS.ContainsKey(spname))
            {
                class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
                objSPEntry.ClearAllParamsValues();
                foreach (string columnname in mapparams.Keys)
                {
                    objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
                }
                return db_objectSqlHelper.ExecuteSelectSPMixedConditionsForDT(objSPEntry, db_objectConnectionHelper, connectionkey);
            }
            else
            {
                return null;
            }
        }

        public class_data_PlatformDBDataReader ExecuteSelectWithMixedConditions(string connectionkey, string spname, Dictionary<string, string> mapparams)
        {
            if (Map_SPS.ContainsKey(spname))
            {
                class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
                objSPEntry.ClearAllParamsValues();
                foreach (string columnname in mapparams.Keys)
                {
                    objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
                }
                return db_objectSqlHelper.ExecuteSelectSPMixedConditionsForDT(objSPEntry, db_objectConnectionHelper, connectionkey);
            }
            else
            {
                return null;
            }
        }

        public void ConnectDB(string keyname)
        {
            string server = Map_ApiConfigs[keyname]["server"];
            string uid = Map_ApiConfigs[keyname]["uid"];
            string pwd = Map_ApiConfigs[keyname]["pwd"];
            string db = Map_ApiConfigs[keyname]["db"];
            db_objectConnectionHelper.Set_NewConnectionItem(keyname, server, uid, pwd, db, enum_DatabaseType.MySql);
        }

        public void LoadSPS(string keyname)
        {
            Map_SPS = db_objectSqlHelper.ActionAutoLoadingAllSPS(db_objectConnectionHelper.Get_ActiveConnection(keyname), "");
        }

        public void CloseDB()
        {
            db_objectConnectionHelper.Action_CloseAllActionConnection();
        }


        public bool VerifyNotEmpty(List<string> list)
        {
            foreach (string item in list)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
            }
            return true;
        }
    }
}
