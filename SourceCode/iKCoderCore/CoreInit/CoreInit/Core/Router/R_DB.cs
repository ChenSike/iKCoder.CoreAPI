using System;
using System.Collections.Generic;
using System.Text;

namespace CoreInit
{
    public class R_DB:R_Base
    {

        M_DB _object_M_DB = new M_DB();
        
        public override void Process(string commnad)
        {
            switch(commnad)
            {
                case "input":                
                    _object_M_DB.Input();
                    break;
                case "connect":
                    assetResult(_object_M_DB.ConnectTo());
                    break;
                case "createSPS":
                    assetResult(_object_M_DB.CreateAutoSPS());
                    break;
                default:
                    invalidateCommand();
                    break;
            }
        }
    }
}
