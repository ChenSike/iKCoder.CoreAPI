using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for class_Bus_Exp
/// </summary>
public class class_Bus_Exp
{
    Dictionary<string, List<string>> typedSymbols = new Dictionary<string, List<string>>();
    Dictionary<string, DataRow> sourceRows_sence = new Dictionary<string, DataRow>();
    List<string> symbolList = new List<string>();
    List<string> finishedSymbolList = new List<string>();
    class_CommonData Object_CommonData;
    class_Bus_ProfileDocs Object_RefProfile;
    string activeUserName = string.Empty;

    string strPrimerKey = "primer";
    string strPrimaryKey = "primary";
    string strMiddlekey = "middle";
    string strSeniorKey = "senior";
    string strAdvancedKey = "advanced";

    double total_exvalue_primer = 0;
    double total_exvalue_primary = 0;
    double total_exvalue_middle = 0;
    double total_exvalue_senior = 0;
    double total_exvalue_advanced = 0;
    double u_exvalue_primer = 0;
    double u_exvalue_primary = 0;
    double u_exvalue_middle = 0;
    double u_exvalue_senior = 0;
    double u_exvalue_advanced = 0;

    public class_Bus_Exp(class_CommonData refCommonDataObject,List<string> finishedSymbolList,class_Bus_ProfileDocs refActiveObject,string username)
    {
        activeUserName = username;
        Object_CommonData = refCommonDataObject;
        Object_RefProfile = refActiveObject;
        init_sourceDoc_Sence();
        init_typedSymbols();
        init_Calc(finishedSymbolList);
        //
        // TODO: Add constructor logic here
        //
    }

      protected void init_sourceDoc_Sence()
    {
        class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_CONFIG_SENCE);
        DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        foreach (DataRow activeRow in textDataTable.Rows)
        {
            string symbol = string.Empty;
            class_Data_SqlDataHelper.GetColumnData(activeRow, "symbol", out symbol);
            symbolList.Add(symbol);
            sourceRows_sence.Add(symbol, activeRow);
        }
    }

    protected void init_typedSymbols()
    {
        List<string> primerList = new List<string>();
        List<string> primaryList = new List<string>();
        List<string> middleList = new List<string>();
        List<string> seniorList = new List<string>();
        List<string> advancedList = new List<string>();
        typedSymbols.Add(strPrimerKey, primerList);
        typedSymbols.Add(strPrimaryKey, primaryList);
        typedSymbols.Add(strMiddlekey, middleList);
        typedSymbols.Add(strSeniorKey, seniorList);
        typedSymbols.Add(strAdvancedKey, advancedList);

        foreach (string strSymbol in symbolList)
        {
            if (strSymbol.StartsWith("a") || strSymbol.StartsWith("A"))
                primerList.Add(strSymbol);
            else if (strSymbol.StartsWith("b") || strSymbol.StartsWith("B"))
                primaryList.Add(strSymbol);
            else if (strSymbol.StartsWith("c") || strSymbol.StartsWith("C"))
                middleList.Add(strSymbol);
            else if (strSymbol.StartsWith("d") || strSymbol.StartsWith("D"))
                seniorList.Add(strSymbol);
            else
                advancedList.Add(strSymbol);
        }
    }

    public double Get_TotalExp()
    {
        return total_exvalue_advanced + total_exvalue_middle + total_exvalue_primer + total_exvalue_senior + total_exvalue_primary;
    }

    public double Get_UserTotalExp()
    {
        return u_exvalue_primer + u_exvalue_primary + u_exvalue_middle + u_exvalue_senior + u_exvalue_advanced;
    }

    public double Get_TypedTotalExp(string strType)
    {
        if (strType == strPrimerKey)
            return total_exvalue_primer;
        else if (strType == strPrimaryKey)
            return total_exvalue_primary;
        else if (strType == strMiddlekey)
            return total_exvalue_middle;
        else if (strType == strSeniorKey)
            return total_exvalue_senior;
        else if (strType == strAdvancedKey)
            return total_exvalue_advanced;
        else
            return 0.0;
    }

    public double Get_TypedUserTotalExp(string strType)
    {
        if (strType == strPrimerKey)
            return u_exvalue_primer;
        else if (strType == strPrimaryKey)
            return u_exvalue_primary;
        else if (strType == strMiddlekey)
            return u_exvalue_middle;
        else if (strType == strSeniorKey)
            return u_exvalue_senior;
        else if (strType == strAdvancedKey)
            return u_exvalue_advanced;
        else
            return 0.0;
    }

    public void init_Calc(List<string> finishedSymbolList)
    {
        foreach (string typeKey in typedSymbols.Keys)
        {
            List<string> tmpList = typedSymbols[typeKey];
            foreach (string activeTypedSymbol in tmpList)
            {
                if (sourceRows_sence.ContainsKey(activeTypedSymbol))
                {
                    DataRow activeDataRow = sourceRows_sence[activeTypedSymbol];
                    string configDoc = string.Empty;
                    class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configDoc);
                    XmlDocument activeSourceDoc_sence = new XmlDocument();
                    activeSourceDoc_sence.LoadXml(class_CommonUtil.Decoder_Base64(configDoc));
                    XmlNode scoreNode = activeSourceDoc_sence.SelectSingleNode("/sence/score");
                    string strBasicScore = class_XmlHelper.GetAttrValue(scoreNode, "score");
                    string strDiffScore = class_XmlHelper.GetAttrValue(scoreNode, "diff");
                    double iBasicScore = 0;
                    double.TryParse(strBasicScore, out iBasicScore);
                    double iDiffScore = 0;
                    double.TryParse(strDiffScore, out iDiffScore);
                    if (typeKey.Contains("primer"))
                        total_exvalue_primer = total_exvalue_primer + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("primary"))
                        total_exvalue_primary = total_exvalue_primary + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("middle"))
                        total_exvalue_middle = total_exvalue_middle + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("senior"))
                        total_exvalue_senior = total_exvalue_senior + (iBasicScore * (1 + iDiffScore));
                    else if (typeKey.Contains("advanced"))
                        total_exvalue_advanced = total_exvalue_advanced + (iBasicScore * (1 + iDiffScore));
                }
            }
        }
        foreach (string activeFinishSymbol in finishedSymbolList)
        {
            if (sourceRows_sence.ContainsKey(activeFinishSymbol))
            {
                DataRow activeDataRow = sourceRows_sence[activeFinishSymbol];
                string configDoc = string.Empty;
                class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "config", out configDoc);
                XmlDocument tmpConfigDoc = new XmlDocument();
                tmpConfigDoc.LoadXml(class_CommonUtil.Decoder_Base64(configDoc));
                XmlNode scoreNode = tmpConfigDoc.SelectSingleNode("/sence/score");
                string strBasicScore = class_XmlHelper.GetAttrValue(scoreNode, "score");
                string strDiffScore = class_XmlHelper.GetAttrValue(scoreNode, "diff");
                double iBasicScore = 0;
                double.TryParse(strBasicScore, out iBasicScore);
                double iDiffScore = 0;
                double.TryParse(strDiffScore, out iDiffScore);
                if (activeFinishSymbol.StartsWith("a") || activeFinishSymbol.StartsWith("A"))
                    u_exvalue_primer = u_exvalue_primer + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("b") || activeFinishSymbol.StartsWith("B"))
                    u_exvalue_primary = u_exvalue_primary + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("c") || activeFinishSymbol.StartsWith("C"))
                    u_exvalue_middle = u_exvalue_middle + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("d") || activeFinishSymbol.StartsWith("D"))
                    u_exvalue_senior = u_exvalue_senior + (iBasicScore * (1 + iDiffScore));
                else if (activeFinishSymbol.StartsWith("e") || activeFinishSymbol.StartsWith("E"))
                    u_exvalue_advanced = u_exvalue_advanced + (iBasicScore * (1 + iDiffScore));
            }
        }
        double u_TotalExp = 0.0;
        u_TotalExp = u_exvalue_primer + u_exvalue_primary + u_exvalue_middle + u_exvalue_senior + u_exvalue_advanced;
        Object_RefProfile.SetTotalExp(activeUserName, u_TotalExp);
    }
}