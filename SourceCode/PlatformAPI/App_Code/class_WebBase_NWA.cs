﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;

/// <summary>
/// class_WebBase_NWA 的摘要说明
/// </summary>
public class class_WebBase_NWA:class_Base_WebBaseclass
{

    protected class_CommonLogic object_CommonLogic = new class_CommonLogic();

    protected override void DoAction()
    {
        object_CommonLogic.InitServices(APPFOLDERPATH);
        ExtenedFunction();
    }

    protected virtual void ExtenedFunction()
    {

    }

	public class_WebBase_NWA()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
}