using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;
using System.Data;
using System.Xml;
using System.IO;

public partial class Sys_api_iKCoder_Sys_Set_WordAudio : class_WebBase_IKCoderAPI_NUA
{
    protected override void ExtendedAction()
    {
        Object_CommonData.PrepareDataOperation();
        class_Data_SqlSPEntry activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
        activeSPEntry_binData.ClearAllParamsValues();
        activeSPEntry_binData.ModifyParameterValue("@symbol", "data_list_words");
        DataTable binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
        if (binDataTable != null && binDataTable.Rows.Count > 0)
        {
            DataRow activeDataRow = null;
            class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow);
            string strContentText = string.Empty;
            class_Data_SqlDataHelper.GetArrByteColumnDataToString(activeDataRow, "data", out strContentText);
            if (!string.IsNullOrEmpty(strContentText))
            {
                string strDecodeBase64 = class_CommonUtil.Decoder_Base64(strContentText);
                XmlDocument wordList = new XmlDocument();
                wordList.LoadXml(strDecodeBase64);
                XmlNodeList listNodes = wordList.SelectNodes("/root/list");
                foreach(XmlNode activeListNode in listNodes)
                {
                    XmlNodeList stageNodesLst = activeListNode.SelectNodes("stage");
                    foreach(XmlNode activeStage in stageNodesLst)
                    {
                        XmlNodeList wordNodesLst = activeStage.SelectNodes("word");
                        foreach (XmlNode activeWordNode in wordNodesLst)
                        {
                            string wordValue = class_XmlHelper.GetAttrValue(activeWordNode, "value");
                            string word_symbol_uk = "sound_word_" + wordValue + "_uk";
                            string word_symbol_us = "sound_word_" + wordValue + "_us";
                            List<string> wordLst = new List<string>();
                            wordLst.Add(word_symbol_us);
                            wordLst.Add(word_symbol_uk);
                            XmlNodeList soundmarkItems = activeWordNode.SelectNodes("soundmark/item");
                            foreach(XmlNode activeSoundmarkItem in soundmarkItems)
                            {
                                string type = class_XmlHelper.GetAttrValue(activeSoundmarkItem, "type");
                                if (type == "us")
                                    class_XmlHelper.SetAttribute(activeSoundmarkItem, "sound", word_symbol_us);
                                if (type == "uk")
                                    class_XmlHelper.SetAttribute(activeSoundmarkItem, "sound", word_symbol_uk);
                            }                          
                            foreach (string strWord in wordLst)
                            {
                                string requestWordVoiceUrl = "http://dict.youdao.com/dictvoice?type=1&audio=" + wordValue;
                                byte[] wordVoiceBuffer = Object_NetRemote.getRemoteRequestToBytesByGet(requestWordVoiceUrl);
                                activeSPEntry_binData = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_BINARY);
                                activeSPEntry_binData.ClearAllParamsValues();
                                activeSPEntry_binData.ModifyParameterValue("@symbol", strWord);
                                binDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_binData, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                                string base64Data = class_CommonUtil.Encoder_Base64(wordVoiceBuffer);
                                if (binDataTable.Rows.Count == 0)
                                {
                                    activeSPEntry_binData.ClearAllParams();
                                    activeSPEntry_binData.ModifyParameterValue("@symbol", strWord);
                                    activeSPEntry_binData.ModifyParameterValue("@type", "mp3");
                                    activeSPEntry_binData.ModifyParameterValue("@owner", "sys");
                                    activeSPEntry_binData.ModifyParameterValue("@data", base64Data);
                                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.insert.ToString(), this.GetType());
                                }
                                else
                                {
                                    DataRow activeDataRow_wordVoice = null;
                                    class_Data_SqlDataHelper.GetActiveRow(binDataTable, 0, out activeDataRow_wordVoice);
                                    string id = string.Empty;
                                    class_Data_SqlDataHelper.GetColumnData(activeDataRow_wordVoice, "id", out id);
                                    if (!string.IsNullOrEmpty(id))
                                    {
                                        activeSPEntry_binData.ClearAllParams();
                                        activeSPEntry_binData.ModifyParameterValue("@id", id);
                                        activeSPEntry_binData.ModifyParameterValue("@data", base64Data);
                                        Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_binData, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                                    }
                                }
                            }
                        }
                    }
                }
                class_Data_SqlSPEntry activeSPEntry_configSence = Object_CommonData.GetActiveSP(Object_CommonData.dbServer, class_SPSMap.SP_OPERATION_RESOURCE_TEXT);
                activeSPEntry_configSence.ClearAllParamsValues();
                activeSPEntry_configSence.ModifyParameterValue("@symbol", "data_list_words");
                DataTable textDataTable = Object_CommonData.Object_SqlHelper.ExecuteSelectSPConditionForDT(activeSPEntry_configSence, Object_CommonData.Object_SqlConnectionHelper, Object_CommonData.dbServer);
                if (textDataTable != null && textDataTable.Rows.Count > 0)
                {
                    DataRow activeTextDataRow = null;
                    class_Data_SqlDataHelper.GetActiveRow(textDataTable, 0, out activeTextDataRow);
                    string id = string.Empty;
                    class_Data_SqlDataHelper.GetColumnData(activeDataRow, "id", out id);
                    activeSPEntry_configSence.ClearAllParamsValues();
                    string strBase64Data = class_CommonUtil.Encoder_Base64(wordList.OuterXml);
                    activeSPEntry_configSence.ModifyParameterValue("@id", id);
                    activeSPEntry_configSence.ModifyParameterValue("@data", strBase64Data);
                    Object_CommonData.CommonSPOperation(AddErrMessageToResponseDOC, AddResponseMessageToResponseDOC, ref RESPONSEDOCUMENT, activeSPEntry_configSence, class_CommonDefined.enumDataOperaqtionType.update.ToString(), this.GetType());
                }
            }
            else
            {
                AddErrMessageToResponseDOC(class_CommonDefined._Faild_Execute_Api + this.GetType().FullName, "false", "");
            }
        }
        
        string requestUrl = "http://dict.youdao.com/dictvoice?type=1&audio=computer";
        byte[] dataBuffer = Object_NetRemote.getRemoteRequestToBytesByGet(requestUrl);
        switchResponseMode(enumResponseMode.bin);
        Response.ContentType = "audio/mp3";
        RESPONSEBUFFER = dataBuffer;
        
    }
}