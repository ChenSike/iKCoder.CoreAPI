using System;
using System.Collections.Generic;
using System.Text;

namespace CoreInit
{
    public class R_Config:R_Base
    {
        M_Config _object_M_Config = new M_Config();

        public override void Process(string command)
        {
            string[] paramsResult = splitCommand(command);
            if (paramsResult.Length > 0)
            {
                switch (paramsResult[0])
                {
                    case "save":
                        _object_M_Config.Save();
                        break;
                    case "init":
                        _object_M_Config.init();
                        break;
                    case "session":
                        switch(paramsResult[1])
                        {
                            case "new":
                                    _object_M_Config.NewSession(paramsResult[2], paramsResult[3]);
                                break;
                            case "newattr":
                                _object_M_Config.SetSessionAttr(paramsResult[2], paramsResult[3], paramsResult[4]);
                                break;
                            case "getvalue":
                                displayResult(_object_M_Config.GetSessionValue(paramsResult[2]));
                                break;
                                   

                        }
                        break;
                }
            }
           
        }
    }
}
