using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// class_WebClass_WA 的摘要说明
/// </summary>
public class class_WebClass_WA:class_Base_WebBaseclass
{

    protected class_CommonLogic object_CommonLogic = new class_CommonLogic();
    protected class_ProductToken object_TokenLogic = new class_ProductToken();

    protected override void DoAction()
    {
        ISRESPONSEDOC = true;
        object_CommonLogic.InitServices(APPFOLDERPATH);
        if (object_TokenLogic.CheckRegistiedToken(Session, REQUESTDOCUMENT))        
            ExtenedFunction();   
        else
            AddErrMessageToResponseDOC("BadToken", "Your token is invalidated", "");
    }

    protected virtual void ExtenedFunction()
    {

    }

}