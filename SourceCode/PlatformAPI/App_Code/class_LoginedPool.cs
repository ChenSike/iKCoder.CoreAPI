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

    public List<string> AllowsProduce
    {
        set;
        get;
    }

    public class_AccountItem()
    {
        this.AllowsProduce = new List<string>();
    }

}



public static class class_LoginedPool
{   

    private static Dictionary<string, class_AccountItem> _accountLoginedPool = new Dictionary<string, class_AccountItem>();

    public static bool insertNewLoginAccountItem(class_AccountItem activeAccountItem)
    {
        if (activeAccountItem != null && _accountLoginedPool.Count < class_CommonDefined.CountOfLoginedAccount)
        {
            lock (_accountLoginedPool)
            {
                if (!_accountLoginedPool.ContainsKey(activeAccountItem.UserNmae))
                    _accountLoginedPool.Add(activeAccountItem.UserNmae, activeAccountItem);
                else
                    _accountLoginedPool[activeAccountItem.UserNmae] = activeAccountItem;
                return true;                 
            }
        }
        else
            return false;
    }

    public static class_AccountItem getActiveAccountItem(string userName)
    {
        if (_accountLoginedPool != null && _accountLoginedPool.ContainsKey(userName))
            return _accountLoginedPool[userName];
        else
            return null;
    }

    public static void clearLoginedAccountPool()
    {
        _accountLoginedPool.Clear();
    }

    public static void removeLoginedAccount(string userName)
    {
        if(_accountLoginedPool.ContainsKey(userName))        
            _accountLoginedPool.Remove(userName);        
    }

    public static bool verifyLoginedAccountExisted(string userName)
    {
        if (_accountLoginedPool.ContainsKey(userName))
        {
            return true;
        }
        else
            return false;
    }

    public static int getCountOfLoginedAccount()
    {
        return _accountLoginedPool.Count;
    }

    public static bool verifyLoginedAccount(string userName,string loginedID,string requestIP)
    {
        if (verifyLoginedAccountExisted(userName))
        {
            if (_accountLoginedPool[userName].LoginedID == loginedID && _accountLoginedPool[userName].LastRequestIP == requestIP)
                return true;
            else
                return false;
        }
        else
            return false;
    }

}