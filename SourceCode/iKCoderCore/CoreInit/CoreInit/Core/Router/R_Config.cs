using System;
using System.Collections.Generic;
using System.Text;

namespace CoreInit
{
    public class R_Config:R_Base
    {
        M_Config _object_M_Config = new M_Config();

        public override void Process(string commnad)
        {
            switch(commnad)
            {
                case "input":
                    _object_M_Config.Input();
                    break;
                case "init":
                    _object_M_Config.init();
                    break;
                case "save":
                    _object_M_Config.Save();
                    break;
            }
        }
    }
}
