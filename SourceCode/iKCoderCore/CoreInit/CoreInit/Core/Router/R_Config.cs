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
                    case "input":
                        _object_M_Config.Input();
                        break;
                    case "save":
                        assetResult(_object_M_Config.Save());
                        break;
                    case "new":
                        assetResult(_object_M_Config.NewDocument());
                        break;
                    case "init":
                        assetResult(_object_M_Config.init());
                        break;
                    case "AESon":
                        _object_M_Config.AESMode();
                        executedResult();
                        break;
                    case "AESoff":
                        _object_M_Config.NormalMode();
                        executedResult();
                        break;
					case "fullsession":
						displayResult(_object_M_Config.GetFullSession(paramsResult[1]));
						break;
                    case "session":
                        if (paramsResult.Length > 1)
                        {
                            switch (paramsResult[1])
                            {
                                case "new":
                                    assetResult(_object_M_Config.NewSession(paramsResult[2], paramsResult[3]));
                                    break;
                                case "newattr":
                                    assetResult(_object_M_Config.SetSessionAttr(paramsResult[2], paramsResult[3], paramsResult[4]));
                                    break;
                                case "updateattr":
                                    assetResult(_object_M_Config.SetSessionAttr(paramsResult[2], paramsResult[3], paramsResult[4]));
                                    break;
                                case "getvalue":
                                    displayResult(_object_M_Config.GetSessionValue(paramsResult[2]));
                                    break;
                                case "getattr":
                                    displayResult(_object_M_Config.GetSessionAttrValue(paramsResult[2], paramsResult[3]));
                                    break;
                            }
                        }
                        break;
                }
            }
           
        }
    }
}
