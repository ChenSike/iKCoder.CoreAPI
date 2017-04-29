using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for class_CommonName
/// </summary>
public class class_CommonName
{
    public static string GetProfileName(string account,string produce = "ikcoder")
    {
        return "profile_" + produce + "_" + account;
    }
}