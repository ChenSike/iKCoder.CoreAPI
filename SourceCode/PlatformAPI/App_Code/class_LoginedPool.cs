using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// class_LoginedPool 的摘要说明
/// </summary>
/// 

public class class_AccountItem
{
    public string UserID
    {
        set;
        get;
    }

    public string UserNmae
    {
        set;
        get;
    }

    public string LoginTime
    {
        set;
        get;
    }

    public string LastRequestIP
    {
        set;
        get;
    }

    public DateTime LastLoginedTime
    {
        set;
        get;
    }

    public int ExperiedPeriod
    {
        set;
        get;
    }

    public string LoginedID
    {
        set;
        get;
    }

    public class_CommonDefined.enumDevices ActiveDevices
    {
        set;
        get;
    }  
    
    
}



public static class class_LoginedPool
{   

    private static Dictionary<string, class_AccountItem> _accountLoginedPool = new Dictionary<string, class_AccountItem>();

	public static class_LoginedPool()
	{
		
	}

    public static bool insertNewLoginAccountItem(class_AccountItem activeAccountItem)
    {
        if(activeAccountItem!=null)
        {

        }
    }

}