using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Text;
using System.Xml;

public partial class Account_User_api_iKCoder_User_Set_Reg : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        switchResponseMode(enumResponseMode.text);
        string userSymbol = "";
        string userPassword = "";
        string nickname = "";
        string centersymbol = "";
        if (REQUESTDOCUMENT != null)
        {
            XmlNode userSymbolNode = REQUESTDOCUMENT.SelectSingleNode("/root/symbol");
            XmlNode userPasswordNode = REQUESTDOCUMENT.SelectSingleNode("/root/password");
            XmlNode nickNameNode = REQUESTDOCUMENT.SelectSingleNode("/root/nickname");
            userSymbol = class_XmlHelper.GetNodeValue(userSymbolNode);
            userPassword = class_XmlHelper.GetNodeValue(userPasswordNode);
            nickname = class_XmlHelper.GetNodeValue(nickNameNode);

        }
        if (string.IsNullOrEmpty(userSymbol))
        {
            userSymbol = GetQuerystringParam("symbol");
        }
        if (string.IsNullOrEmpty(userSymbol))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Symbol"), "");
            return;
        }

        if (string.IsNullOrEmpty(userPassword))
        {
            userPassword = GetQuerystringParam("password");
        }
        if (string.IsNullOrEmpty(userPassword))
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_Password"), "");
            return;
        }
        if (userPassword.Length < 0 || userPassword.Length > 12)
        {
            AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, Object_LabelController.GetString("message", "Empty_Param_Reg_PasswordLength"), "");
            return;
        }

        if (string.IsNullOrEmpty(nickname))
        {
            nickname = GetQuerystringParam("nickname");
        }
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Spuerman";
        }
        class_Bus_Student objectStudent = new class_Bus_Student(Object_CommonData);
        objectStudent.SetUpdateStudent(userSymbol, userPassword, centersymbol);
        AddResponseMessageToResponseDOC(class_CommonDefined._Executed_Api + this.GetType().FullName, class_CommonDefined.enumExecutedCode.executed.ToString(), Object_LabelController.GetString("message", "SC_Param_Reg_Account"), "");
    }
        
}