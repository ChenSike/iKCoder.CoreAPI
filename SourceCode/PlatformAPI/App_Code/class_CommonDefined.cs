using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// class_CommonDefined 的摘要说明
/// </summary>
public class class_CommonDefined
{
    public const string _Faild_Execute_Api = "Faild to execute api : ";
    public const string _Executed_Api = "Executed api : ";
    public const int _MaxCount_AccountLogined = 5000;

    public enum enumExecutedCode
    {
        executed = 1001,
        failedExecuted = 4001
    }

    public enum enumDataItemType
    {
        text = 2001,
        bin = 2002
    }

    public enum enumDataOperaqtionType
    {
        insert = 3001,
        update = 3002,
        delete = 3003,
        select = 3004,
        selectkey = 3005
    }

    public enum enumDevices
    {
        pc = 4001,
        mobil = 4002,
        pad = 4003,
        other = 4004
    }
       
}