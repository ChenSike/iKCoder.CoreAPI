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
    public const string _AllowSysOperation = "AllowedOperation";
    public static int CountOfLoginedAccount = 5000;
    public static int ExperiedPeriodOfLoginedAccount = 180;

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
        selectkey = 3005,
        selectcondition = 3006,
        selectmixed = 3007,
        deletecondition = 3008,
        deletemixed = 3009
    }

    public enum enumDevices
    {
        pc = 4001,
        mobil = 4002,
        pad = 4003,
        other = 4004
    }

    public enum enumSenceType
    {
        primer = 5001,
        primary = 5002,
        middle = 5003,
        senior = 5004,
        advanced = 5005
    }

    public enum enumSTEMLType
    {
        science = 6001,
        technology = 6002,
        engineering = 6003,
        math = 6004,
        language = 6005
    }

    public enum enumProfileDoc
    {
        doc_basic = 7001,
        doc_studystatus = 7002,
        doc_commnunication = 7003,
        doc_payment = 7004,
        doc_datastore = 7005,
        doc_recored = 7006,
        doc_message = 7007
    }

    public enum enumLoginedMark
    {
       mark_educenter = 1,
       mark_teacher = 2,
       mark_student = 3,
       mark_sales = 4
    }

}