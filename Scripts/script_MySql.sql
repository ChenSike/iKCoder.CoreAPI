-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: platformapi
-- ------------------------------------------------------
-- Server version	5.7.17-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account_basic`
--

DROP TABLE IF EXISTS `account_basic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_basic` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(200) DEFAULT NULL,
  `password` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_basic`
--

LOCK TABLES `account_basic` WRITE;
/*!40000 ALTER TABLE `account_basic` DISABLE KEYS */;
INSERT INTO `account_basic` VALUES (21,'13111111111','IC2aiZX2XEtAJc6NYYKE95UNPBXMNfKP');
/*!40000 ALTER TABLE `account_basic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_profile`
--

DROP TABLE IF EXISTS `account_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_profile` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account_name` varchar(200) DEFAULT NULL,
  `profile_data` mediumtext,
  `profile_name` varchar(50) DEFAULT NULL,
  `profile_product` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_profile`
--

LOCK TABLES `account_profile` WRITE;
/*!40000 ALTER TABLE `account_profile` DISABLE KEYS */;
INSERT INTO `account_profile` VALUES (13,'13111111111','<root><docbasic><doc_id></doc_id><doc_symbol>profile_13111111111</doc_symbol></docbasic><usrbasic><usr_name>13111111111</usr_name><usr_nickname>1111</usr_nickname><coins></coins><account_status></account_status><account_limited></account_limited><account_head></account_head><account_childs></account_childs><account_sms></account_sms></usrbasic><studystatus><fiststarttime></fiststarttime><currentsence><symbol></symbol><currentstage></currentstage></currentsence></studystatus><lessons><begin></begin><intermediate></intermediate><senior></senior></lessons><friends></friends><codetime></codetime></root>','profile_13111111111','iKCoder');
/*!40000 ALTER TABLE `account_profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `data_basic`
--

DROP TABLE IF EXISTS `data_basic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `data_basic` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(40) DEFAULT NULL,
  `type` varchar(10) DEFAULT NULL,
  `data` longblob,
  `produce` varchar(45) DEFAULT NULL,
  `isBinary` varchar(2) DEFAULT '0',
  `isBase64` varchar(2) DEFAULT '0',
  `isDES` varchar(2) DEFAULT '0',
  `DESKey` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `data_basic`
--

LOCK TABLES `data_basic` WRITE;
/*!40000 ALTER TABLE `data_basic` DISABLE KEYS */;
INSERT INTO `data_basic` VALUES (7,'config_student_index','text','<root> <carousel> <item color=\"27,189,140\" title=\"吃豆人大冒险\" content=\"控制黄色小英雄，吃掉所有的豆子，小心邪恶小怪兽哦！\" keys=\"\'键盘指令，空间移动\" diff=\"中等\" times=\"45分钟\" img=\"pacmanbanner.png\" btnColor=\"25,161,121\" symbol=\"sence_XXXXX\"/> </carousel> <honorwall> <item map=\"computerprofessor\" title=\"计算机小专家\" condition=\"学习计算机课程超过10课时\" isused =\"1\"/> <item map=\"shareexpert\" title=\"分享达人\" condition=\"学习计算机课程超过10课时\" isused=\"1\"/> <item map=\"mathematician\" title=\"小小数学家\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"arithmeticprofessor\" title=\"算法小达人\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"languagemaster\" title=\"语言大师\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"musician\" title=\"音乐家\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"sciencemastermind\" title=\"科学智多星\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"littlepinter\" title=\"小画家\" condition=\"学习计算机课程超过10课时\" isused=\"0\"/> <item map=\"UAVpilot\" title=\"无人机小飞手\" condition=\"学习计算机课程超过10课时\" isused=\"1\"/> </honorwall> <userinfo> <user name=\"可乐\" level=\"实习工程师\" works=\"18\" course=\"25\" friend=\"30\" head=\"1\"/> <course rank=\"1\" emp=\"2600\" works=\"18\" works_rank=\"2\" code_time=\"175\" code_time_exceed=\"92\" primary_rate=\"85\" middel_rate=\"11\" advanced_rate=\"5\"/> <distribution> <item id=\"science\" title=\"科学\" color=\"rgb(36,90,186)\" exp=\"250\"/> <item id=\"skill\" title=\"技术\" color=\"rgb(236,15,33)\" exp=\"400\"/> <item id=\"engineering\" title=\"工程\" color=\"rgb(165,165,165)\" exp=\"550\"/> <item id=\"math\" title=\"数学\" color=\"rgb(255,191,0)\" exp=\"700\"/> <item id=\"language\" title=\"语言\" color=\"rgb(71,143,208)\" exp=\"700\"/> </distribution> <codetimes> <item date=\"2017-1-1\" time=\"3\"/> <item date=\"2017-1-2\" time=\"2\"/> <item date=\"2017-1-3\" time=\"4\"/> </codetimes> </userinfo> <learning> <item id=\"1\" title=\"Star Wars\" content=\"Learn to program droids, and create your own Star Wars game in a galaxy far, far away.\" img=\"1.png\"/> </learning> <classify> <category id=\"1\" symbol=\"Computer\" title=\"计算机基础\"> <item id=\"1\" title=\"Star Wars\" content=\"Learn to program droids, and create your own Star Wars game in a galaxy far, far away.\" img=\"1.png\"/> </category> <category id=\"2\" symbol=\"Math\" title=\"数学\"> <item id=\"1\" title=\"Star Wars\" content=\"Learn to program droids, and create your own Star Wars game in a galaxy far, far away.\" img=\"1.png\"/> </category> <category id=\"3\" symbol=\"Physics\" title=\"物理\"> <item id=\"1\" title=\"Star Wars\" content=\"Learn to program droids, and create your own Star Wars game in a galaxy far, far away.\" img=\"1.png\"/> </category></classify></root>','iKCoder','0','0','0',''),(21,'config_owner_report','text','PHJvb3Q+PHJlcG9ydCBkYXRlPSIyMDE3LTEtMSIvPjxjaGlsZCBpZD0iMSIgbmFtZT0iVG9tIiByYW5rPSI4MCIgdGl0bGU9Isq1z7C5pLPMyqYiIGltZz0iY2hpbGRfMS5wbmciIGV4cD0iMjYwMCIgd29ya3M9IjE4IiBjb3Vyc2U9IjI1IiBmcmllbmQ9IjMwIi8+IDxhY2hpZXZlPiA8aXRlbSBpZD0iMDEiIHRpdGxlPSK8xsvju/rQodeovNIiIGNvbnRlbnQ9IsuzwPvN6rPJwcu8xsvju/rUrcDttcTL+dPQu/m0ob/Os8wsILbUz9a0+rzGy+O7+rXEz7XNs9fps8ksINTL0NC3vcq9us2x4LPM1K3A7dPQwcvPtc2z0NS1xMjP1qouIi8+IDxpdGVtIGlkPSIwMiIgdGl0bGU9IrfWz+3QobTvyMsiIGNvbnRlbnQ9IrfWz+3ByzE4uPbS0c3qs8nX98a3LCDV4tCp1/fGt9LRsbs1NjXIy7TO5K/AwC4iLz4gPC9hY2hpZXZlPiA8YWJpbGl0eSBjYXRlZ29yeUNvdW50PSI1IiBjb3Vyc2VDb3VudD0iMjUiIGNvdXJzZVRpbWU9IjEyNSIgd29yZENvdW50PSI4OCI+IDxwcm9tb3RlQ2F0ZWdvcnk+IDxpdGVtPr/G0ac8L2l0ZW0+IDxpdGVtPry8yvU8L2l0ZW0+IDxpdGVtPrmks8w8L2l0ZW0+IDxpdGVtPsr90ac8L2l0ZW0+IDxpdGVtPtPv0dQ8L2l0ZW0+IDwvcHJvbW90ZUNhdGVnb3J5PiA8Y29tcGxldGVDb3Vyc2U+IDxpdGVtIGluZGV4PSIxIiBpZD0iMTEiIG5hbWU9IrzGy+O7+tStwO0iLz4gPGl0ZW0gaW5kZXg9IjIiIGlkPSIyIiBuYW1lPSK/1bzkuMXE7rrN09DQ8tLGtq8iLz4gPGl0ZW0gaW5kZXg9IjMiIGlkPSIzIiBuYW1lPSK7+bShyv2+3b3hubkiLz4gPGl0ZW0gaW5kZXg9IjQiIGlkPSIzNCIgbmFtZT0ivPzFzLywyvOx6r/Y1sYiLz4gPGl0ZW0gaW5kZXg9IjUiIGlkPSIzNSIgbmFtZT0iyv3Rp8rkyOvT68rks/YiLz4gPGl0ZW0gaW5kZXg9IjYiIGlkPSI2IiBuYW1lPSLM9bz+0a27tyIvPiA8aXRlbSBpbmRleD0iNyIgaWQ9IjciIG5hbWU9Isz1vP7F0LbP0+++5CIvPiA8aXRlbSBpbmRleD0iOCIgaWQ9IjIiIG5hbWU9ItL0wNaypbfF1K3A7SIvPiA8aXRlbSBpbmRleD0iOSIgaWQ9IjkiIG5hbWU9Irv5sb675s281rjB7iIvPiA8L2NvbXBsZXRlQ291cnNlPiA8Z3JhcGg+IDxpdGVtIG5hbWU9Ir/G0aciIHZhbHVlPSI3MDAiLz4gPGl0ZW0gbmFtZT0ivLzK9SIgdmFsdWU9IjQwMCIvPiA8aXRlbSBuYW1lPSK5pLPMIiB2YWx1ZT0iNTUwIi8+IDxpdGVtIG5hbWU9Isr90aciIHZhbHVlPSI3MDAiLz4gPGl0ZW0gbmFtZT0i0+/R1CIgdmFsdWU9IjQ1MCIvPiA8L2dyYXBoPiA8L2FiaWxpdHk+IDxjb2RldGltZSB0b3RhbD0iMTk1IiBiZXlvbmQ9IjkyIiBtb250aD0iMSI+IDx0aW1lcz4gPGl0ZW0+MzwvaXRlbT4gPGl0ZW0+MjwvaXRlbT4gPGl0ZW0+NDwvaXRlbT4gPGl0ZW0+MTwvaXRlbT4gPGl0ZW0+MzwvaXRlbT4gPGl0ZW0+MjwvaXRlbT4gPGl0ZW0+NDwvaXRlbT4gPGl0ZW0+NTwvaXRlbT4gPGl0ZW0+NjwvaXRlbT4gPGl0ZW0+NzwvaXRlbT4gPGl0ZW0+MjwvaXRlbT4gPGl0ZW0+NTwvaXRlbT4gPGl0ZW0+MzwvaXRlbT4gPGl0ZW0+NzwvaXRlbT4gPGl0ZW0+NDwvaXRlbT4gPGl0ZW0+MTwvaXRlbT4gPC90aW1lcz4gPHJhdGU+IDxpdGVtIG5hbWU9IrP1vLa/zrPMIiBpZD0iUHJpbWFyeSIgcmF0ZT0iODUiLz4gPGl0ZW0gbmFtZT0i1tC8tr/Os8wiIGlkPSJNaWRkbGUiIHJhdGU9IjExIi8+IDxpdGVtIG5hbWU9IrjfvLa/zrPMIiBpZD0iQWR2YW5jZSIgcmF0ZT0iNSIvPiA8L3JhdGU+IDwvY29kZXRpbWU+IDxwb3RlbnRpYWw+IDx0b3A+IDxpdGVtPsr90ac8L2l0ZW0+IDxpdGVtPr/G0ac8L2l0ZW0+IDwvdG9wPiA8ZXZhbHVhdGU+IDxpdGVtIHRpdGxlPSK/xtGnIiB2YWx1ZT0iNDAiLz4gPGl0ZW0gdGl0bGU9Iry8yvUiIHZhbHVlPSIzMCIvPiA8aXRlbSB0aXRsZT0iuaSzzCIgdmFsdWU9IjE1Ii8+IDxpdGVtIHRpdGxlPSLK/dGnIiB2YWx1ZT0iMTAiLz4gPGl0ZW0gdGl0bGU9ItPv0dQiIHZhbHVlPSI1Ii8+IDwvZXZhbHVhdGU+IDwvcG90ZW50aWFsPiA8d29ya3NpdGVtcz4gPGl0ZW0gaWQ9IjEiIGhpdHM9IjgiIGltZz0id29ya3NfMS5wbmciIGNvbnRlbnQ9IkNSRUFUSVZFLCBFTlRFUlRBSU5NRU5UIi8+IDxpdGVtIGlkPSIyIiBoaXRzPSI2IiBpbWc9IndvcmtzXzIucG5nIiBjb250ZW50PSJDT1JQT1JBVEUsIENSRUFUSVZFLCBORVciLz4gPGl0ZW0gaWQ9IjMiIGhpdHM9IjEwIiBpbWc9IndvcmtzXzMucG5nIiBjb250ZW50PSJDUkVBVElWRSwgRU5URVJUQUlOTUVOVCIvPiA8aXRlbSBpZD0iNCIgaGl0cz0iMTIiIGltZz0id29ya3NfNC5wbmciIGNvbnRlbnQ9IkNPUlBPUkFURSwgT05FLVBBR0UsIFRFQ0hOT0xPR1kiLz4gPGl0ZW0gaWQ9IjUiIGhpdHM9IjUiIGltZz0id29ya3NfNS5wbmciIGNvbnRlbnQ9Ik5FVywgQ09SUE9SQVRFLCBURUNITk9MT0dZIi8+IDxpdGVtIGlkPSI2IiBoaXRzPSI3IiBpbWc9IndvcmtzXzYucG5nIiBjb250ZW50PSJCQVNFIChNQUlOIERFTU8pIi8+IDwvd29ya3NpdGVtcz4gPHJlY29tbWVuZD4gPGl0ZW0gaWQ9IjEiIGhpdHM9IjgiIGltZz0id29ya3NfMS5wbmciIGNvbnRlbnQ9IkNSRUFUSVZFLCBFTlRFUlRBSU5NRU5UIi8+IDxpdGVtIGlkPSIyIiBoaXRzPSI2IiBpbWc9IndvcmtzXzIucG5nIiBjb250ZW50PSJDT1JQT1JBVEUsIENSRUFUSVZFLCBORVciLz48aXRlbSBpZD0iMyIgaGl0cz0iMTAiIGltZz0id29ya3NfMy5wbmciIGNvbnRlbnQ9IkNSRUFUSVZFLCBFTlRFUlRBSU5NRU5UIi8+PC9yZWNvbW1lbmQ+PC9yb290Pg==','iKCoder','0','0','0',''),(34,'workspace_default_tkwar_stage_4','text','PHhtbD4gPGJsb2NrIHR5cGU9ImJsb2NrX3RhbmtfZ2FtZV9jb25maWciIGRlbGV0YWJsZT0iZmFsc2UiPiA8c3RhdGVtZW50IG5hbWU9Imxpc3Rfb2ZfbWFwX2xpbmUiPiA8YmxvY2sgdHlwZT0iYmxvY2tfZHJhd19saW5lIiBkZWxldGFibGU9ImZhbHNlIj4gPGZpZWxkIG5hbWU9ImJsb2NrX3hfb3JfeV9zZWxlY3RvciI+MTwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja193YWxsX3R5cGVfc2VsZWN0b3IiPjE8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfZHJhd19mcm9tIj4xPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfdG8iPjQ8L2ZpZWxkPiA8c3RhdGVtZW50IG5hbWU9ImZpeF94X3lfcG9pbnQiPiA8YmxvY2sgdHlwZT0iYmxvY2tfcG9pbnRfY29sbGVjdGlvbiI+IDxmaWVsZCBuYW1lPSJmaWVsZF9wb2ludF9jb2xsZWN0aW9uIj4zLCA0LCA3LCA4LCAxMSwgMTIsIDE1LCAxNiwgMTksIDIwLCAyMywgMjQ8L2ZpZWxkPiA8L2Jsb2NrPiA8L3N0YXRlbWVudD4gPG5leHQ+IDxibG9jayB0eXBlPSJibG9ja19kcmF3X2xpbmUiPiA8ZmllbGQgbmFtZT0iYmxvY2tfeF9vcl95X3NlbGVjdG9yIj4xPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX3dhbGxfdHlwZV9zZWxlY3RvciI+MTwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja19kcmF3X2Zyb20iPjIxPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfdG8iPjI0PC9maWVsZD4gPHN0YXRlbWVudCBuYW1lPSJmaXhfeF95X3BvaW50Ij4gPGJsb2NrIHR5cGU9ImJsb2NrX3BvaW50X2NvbGxlY3Rpb24iPiA8ZmllbGQgbmFtZT0iZmllbGRfcG9pbnRfY29sbGVjdGlvbiI+MywgNCwgNywgOCwgMTEsIDEyLCAxNSwgMTYsIDE5LCAyMCwgMjMsIDI0PC9maWVsZD4gPC9ibG9jaz4gPC9zdGF0ZW1lbnQ+IDxuZXh0PiA8YmxvY2sgdHlwZT0iYmxvY2tfZHJhd19saW5lIj4gPGZpZWxkIG5hbWU9ImJsb2NrX3hfb3JfeV9zZWxlY3RvciI+MDwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja193YWxsX3R5cGVfc2VsZWN0b3IiPjM8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfZHJhd19mcm9tIj4yPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfdG8iPjI1PC9maWVsZD4gPHN0YXRlbWVudCBuYW1lPSJmaXhfeF95X3BvaW50Ij4gPGJsb2NrIHR5cGU9ImJsb2NrX2ZpeF9heGlzX2FycmF5Ij4gPGZpZWxkIG5hbWU9ImlucHV0X2ZpeGVkX3BvaW50Ij4xNzwvZmllbGQ+IDxuZXh0PiA8YmxvY2sgdHlwZT0iYmxvY2tfZml4X2F4aXNfYXJyYXkiPiA8ZmllbGQgbmFtZT0iaW5wdXRfZml4ZWRfcG9pbnQiPjE4PC9maWVsZD4gPG5leHQ+IDxibG9jayB0eXBlPSJibG9ja19maXhfYXhpc19hcnJheSI+IDxmaWVsZCBuYW1lPSJpbnB1dF9maXhlZF9wb2ludCI+MTk8L2ZpZWxkPiA8bmV4dD4gPGJsb2NrIHR5cGU9ImJsb2NrX2ZpeF9heGlzX2FycmF5Ij4gPGZpZWxkIG5hbWU9ImlucHV0X2ZpeGVkX3BvaW50Ij42PC9maWVsZD4gPG5leHQ+IDxibG9jayB0eXBlPSJibG9ja19maXhfYXhpc19hcnJheSI+IDxmaWVsZCBuYW1lPSJpbnB1dF9maXhlZF9wb2ludCI+NzwvZmllbGQ+IDwvYmxvY2s+IDwvbmV4dD4gPC9ibG9jaz4gPC9uZXh0PiA8L2Jsb2NrPiA8L25leHQ+IDwvYmxvY2s+IDwvbmV4dD4gPC9ibG9jaz4gPC9zdGF0ZW1lbnQ+IDxuZXh0PiA8YmxvY2sgdHlwZT0iYmxvY2tfZHJhd19saW5lIj4gPGZpZWxkIG5hbWU9ImJsb2NrX3hfb3JfeV9zZWxlY3RvciI+MTwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja193YWxsX3R5cGVfc2VsZWN0b3IiPjI8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfZHJhd19mcm9tIj40PC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfdG8iPjU8L2ZpZWxkPiA8c3RhdGVtZW50IG5hbWU9ImZpeF94X3lfcG9pbnQiPiA8YmxvY2sgdHlwZT0iYmxvY2tfcG9pbnRfY29sbGVjdGlvbiI+IDxmaWVsZCBuYW1lPSJmaWVsZF9wb2ludF9jb2xsZWN0aW9uIj4zLCA0LCA3LCA4LCAxMSwgMTIsIDE1LCAxNiwgMTksIDIwLCAyMywgMjQ8L2ZpZWxkPiA8L2Jsb2NrPiA8L3N0YXRlbWVudD4gPG5leHQ+IDxibG9jayB0eXBlPSJibG9ja19kcmF3X2xpbmUiPiA8ZmllbGQgbmFtZT0iYmxvY2tfeF9vcl95X3NlbGVjdG9yIj4xPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX3dhbGxfdHlwZV9zZWxlY3RvciI+MjwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja19kcmF3X2Zyb20iPjExPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfdG8iPjEyPC9maWVsZD4gPHN0YXRlbWVudCBuYW1lPSJmaXhfeF95X3BvaW50Ij4gPGJsb2NrIHR5cGU9ImJsb2NrX3BvaW50X2NvbGxlY3Rpb24iPiA8ZmllbGQgbmFtZT0iZmllbGRfcG9pbnRfY29sbGVjdGlvbiI+MSwyLDUsNiw5LDEwLDEzLDE0LDE3LDE4LDIxLDIyPC9maWVsZD4gPC9ibG9jaz4gPC9zdGF0ZW1lbnQ+IDxuZXh0PiA8YmxvY2sgdHlwZT0iYmxvY2tfZHJhd19saW5lIj4gPGZpZWxkIG5hbWU9ImJsb2NrX3hfb3JfeV9zZWxlY3RvciI+MTwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja193YWxsX3R5cGVfc2VsZWN0b3IiPjU8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfZHJhd19mcm9tIj4xNTwvZmllbGQ+IDxmaWVsZCBuYW1lPSJibG9ja19kcmF3X3RvIj4xODwvZmllbGQ+IDxzdGF0ZW1lbnQgbmFtZT0iZml4X3hfeV9wb2ludCI+IDxibG9jayB0eXBlPSJibG9ja19wb2ludF9jb2xsZWN0aW9uIj4gPGZpZWxkIG5hbWU9ImZpZWxkX3BvaW50X2NvbGxlY3Rpb24iPjMsNCwxMiwxMywxNCwxOSwyMCwyMTwvZmllbGQ+IDwvYmxvY2s+IDwvc3RhdGVtZW50PiA8bmV4dD4gPGJsb2NrIHR5cGU9ImJsb2NrX2RyYXdfbGluZSI+IDxmaWVsZCBuYW1lPSJibG9ja194X29yX3lfc2VsZWN0b3IiPjE8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfd2FsbF90eXBlX3NlbGVjdG9yIj4xPC9maWVsZD4gPGZpZWxkIG5hbWU9ImJsb2NrX2RyYXdfZnJvbSI+MTE8L2ZpZWxkPiA8ZmllbGQgbmFtZT0iYmxvY2tfZHJhd190byI+MTQ8L2ZpZWxkPiA8c3RhdGVtZW50IG5hbWU9ImZpeF94X3lfcG9pbnQiPiA8YmxvY2sgdHlwZT0iYmxvY2tfcG9pbnRfY29sbGVjdGlvbiI+IDxmaWVsZCBuYW1lPSJmaWVsZF9wb2ludF9jb2xsZWN0aW9uIj4zLCA0LCA3LCA4LCAxMSwgMTIsIDE1LCAxNiwgMTksIDIwLCAyMywgMjQ8L2ZpZWxkPiA8L2Jsb2NrPiA8L3N0YXRlbWVudD4gPC9ibG9jaz4gPC9uZXh0PiA8L2Jsb2NrPiA8L25leHQ+IDwvYmxvY2s+IDwvbmV4dD4gPC9ibG9jaz4gPC9uZXh0PiA8L2Jsb2NrPiA8L25leHQ+IDwvYmxvY2s+IDwvbmV4dD4gPC9ibG9jaz4gPC9zdGF0ZW1lbnQ+IDwvYmxvY2s+IDwveG1sPg==','iKCoder','0','0','0',''),(35,'config_tips_workspace','text','PHJvb3Q+IDx0aXAgc3ltYm9sPSJ0aXBfdGt3YXJfc3RhZ2VfNCI+IDxpdGVtIGluZGV4ID0gIjEiPiA8Y29udGVudCBjaGluZXNlPSKzo8G/IiBlbmdsaXNoPSIiIGJsb2NrdHlwZT0iVkFSIj4gPGNvbnRlbnQgY2hpbmVzZT0iOs/W1NrMub/L1b2209LRvq2/ydLU1NrE47XEv9jWxs/C0sa2r8HLLCC908/CwLTOqsy5v8vU9rzTJnF1b3Q7yeS79yZxdW90O8TcwaawyS4gx+vU9rzTvPzFzMrCvP4sIMq5JnF1b3Q7v9W48SZxdW90O7z8sLTPwsqxLCDMub/Lv8nS1LeiyeTF2rWvLiI+IDwvaXRlbT4gPGl0ZW0gaW5kZXggPSAiMiI+IDwvaXRlbT4gPC90aXA+IDwvcm9vdD4=','iKCoder','0','0','0',''),(36,'sound_word_computer_us','mp3','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','iKCoder','1','1','0',''),(37,'sound_word_computer_uk','mp3','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kdCVmmoeiji2c5VS//tQwJkACvkPGgeYy4ljIaNA8xlxnalZHSqysFJlT5baQtCzoQxlfv4LR6CP37znxzdSmDds12tdPm3hVTcpgQUXZkYQITs8bhqRUAOilvCgDC4VM0iaVatCoCAgK4YV9VdQwEKYCFMzNX9VVb6JabOepIkQCASM/HnnJUSS7kZw0ijM/nEiVHEt4/gVvHLWI0FN7+XPO4cKBTfioqNOLKLMtQSDAooDECyA8cROEgQGYeQjcZ2dnmpZ2eLNZZZZLLLDJWWWWWyyzJmChgoIGP/7UMCeAAt9BRqnmMuJbqLjVPMZcUcQJAgoYGCDhHMuyyxlayo4wkMFDAwQcDSlaYYgAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACD/+1DAn4AKILUTBIzLST8fDwBjDXEAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAA//tQwKyAAAABLAAAAAAAACWAAAAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAIAAgACAAAA==','iKCoder','1','1','0',''),(38,'workspace_configsitem_common','text','cm9vdD4NCjxzZW5jZSBuYW1lID0gIsy5v8u089W9IiBzeW1ib2w9InRrd2FyIiBpZD0iIj4NCjxzY29yZT48L3Njb3JlPg0KPHN0YWdlcz4NCjxzdGFnZSBpbmRleD0iMSIgc2NvcmU9IjEwMCI+DQo8dGlwcyBzeW1ib2w9InRpcF90a3dhcl9zdGFnZV80Ij48L3RpcHM+DQo8dG9vbGJveD4NCjxpdGVtIHNyYyA9ICJ0YW5rd2FyXEJsb2Nrc1xibG9ja3MuSlMiPjwvaXRlbT4NCjwvdG9vbGJveD4NCjxnYW1lPg0KPHNjcmlwdCBzcmM9InRhbmt3YXJcRW5naW5lXGdhbWVfZW5naW5lLkpTIj48L3NjcmlwdD4NCjxzY3JpcHQgc3JjPSJ0YW5rd2FyXFNjZW5lXHNjZW5lLkpTIj48L3NjcmlwdD4NCjwvZ2FtZT4NCjx3b3Jkcz4NCjxpdGVtIHN5bWJvbD0iY29tcHV0ZXIiPjwvaXRlbT4NCjwvd29yZHM+DQo8L3N0YWdlPg0KPC9zdGFnZXM+DQo8L3NlbmNlPg0KPC9yb290Pg==','iKCoder','0','0','0',''),(41,'profile_ikcoder_template','text','PHJvb3Q+DQogIDxkb2NiYXNpYz4NCiAgICA8ZG9jX2lkPjwvZG9jX2lkPg0KICAgIDxkb2Nfc3ltYm9sPjwvZG9jX3N5bWJvbD4NCiAgPC9kb2NiYXNpYz4NCiAgPHVzcmJhc2ljPg0KICAgIDx1c3JfbmFtZT48L3Vzcl9uYW1lPg0KICAgIDx1c3Jfbmlja25hbWU+PC91c3Jfbmlja25hbWU+DQogICAgPGNvaW5zPjwvY29pbnM+DQogICAgPGFjY291bnRfc3RhdHVzPjwvYWNjb3VudF9zdGF0dXM+DQogICAgPGFjY291bnRfbGltaXRlZD48L2FjY291bnRfbGltaXRlZD4NCiAgICA8YWNjb3VudF9oZWFkPjwvYWNjb3VudF9oZWFkPg0KICAgIDxhY2NvdW50X2NoaWxkcz48L2FjY291bnRfY2hpbGRzPg0KICAgIDxhY2NvdW50X3Ntcz48L2FjY291bnRfc21zPg0KICA8L3VzcmJhc2ljPg0KICA8c3R1ZHlzdGF0dXM+DQogICAgPGZpc3RzdGFydHRpbWU+PC9maXN0c3RhcnR0aW1lPg0KICAgIDxjdXJyZW50c2VuY2U+DQoJICA8c3ltYm9sPjwvc3ltYm9sPg0KICAgICAgPGN1cnJlbnRzdGFnZT48L2N1cnJlbnRzdGFnZT4NCiAgICA8L2N1cnJlbnRzZW5jZT4gICAgDQogIDwvc3R1ZHlzdGF0dXM+DQogIDxsZXNzb25zPiAgICANCiAgICA8YmVnaW4+PC9iZWdpbj4NCiAgICA8aW50ZXJtZWRpYXRlPjwvaW50ZXJtZWRpYXRlPg0KICAgIDxzZW5pb3I+PC9zZW5pb3I+DQogIDwvbGVzc29ucz4NCiAgPGZyaWVuZHM+PC9mcmllbmRzPg0KICA8Y29kZXRpbWU+PC9jb2RldGltZT4NCjwvcm9vdD4=','iKCoder','0','0','0','');
/*!40000 ALTER TABLE `data_basic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `data_relationship`
--

DROP TABLE IF EXISTS `data_relationship`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `data_relationship` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(45) DEFAULT NULL,
  `shiptype` varchar(45) DEFAULT NULL,
  `relationdoc` blob,
  `produce` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `data_relationship`
--

LOCK TABLES `data_relationship` WRITE;
/*!40000 ALTER TABLE `data_relationship` DISABLE KEYS */;
INSERT INTO `data_relationship` VALUES (3,'testdata','child','<root></root>','iKCoder'),(4,'testParentDocument','parent','<root></root>','iKCoder');
/*!40000 ALTER TABLE `data_relationship` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'platformapi'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_account_basic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_account_basic`(_operation varchar(40),_id int(11),_username varchar(200),_password varchar(40))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from account_basic;
elseif _operation='insert' then
insert into platformapi.account_basic(username,password) values(_username,_password);
elseif _operation='update' and _username IS NOT NULL then
update account_basic set username = _username where id = _id;
elseif _operation='update' and _password IS NOT NULL then
update account_basic set password = _password where id = _id;
elseif _operation='selectmixed'then
select * from account_basic where id = IFNULL(_id,id) and username = IFNULL(_username,username) and password = IFNULL(_password,password);
elseif _operation='delete' then
delete from account_basic where id = _id;
elseif _operation='selectkey' then
select * from account_basic where id = _id;
elseif _operation='selectcondition' then
select * from account_basic where id = _id or username = _username or password = _password;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_account_profile` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_account_profile`(_operation varchar(40),_id int(11),_account_name varchar(200),_profile_data mediumtext,_profile_name varchar(50),_profile_product varchar(50))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from account_profile;
elseif _operation='insert' then
insert into platformapi.account_profile(account_name,profile_data,profile_name,profile_product) values(_account_name,_profile_data,_profile_name,_profile_product);
elseif _operation='update' and _account_name IS NOT NULL then
update account_profile set account_name = _account_name where id = _id;
elseif _operation='update' and _profile_data IS NOT NULL then
update account_profile set profile_data = _profile_data where id = _id;
elseif _operation='update' and _profile_name IS NOT NULL then
update account_profile set profile_name = _profile_name where id = _id;
elseif _operation='update' and _profile_product IS NOT NULL then
update account_profile set profile_product = _profile_product where id = _id;
elseif _operation='selectmixed'then
select * from account_profile where id = IFNULL(_id,id) and account_name = IFNULL(_account_name,account_name) and profile_data = IFNULL(_profile_data,profile_data) and profile_name = IFNULL(_profile_name,profile_name) and profile_product = IFNULL(_profile_product,profile_product);
elseif _operation='delete' then
delete from account_profile where id = _id;
elseif _operation='selectkey' then
select * from account_profile where id = _id;
elseif _operation='selectcondition' then
select * from account_profile where id = _id or account_name = _account_name or profile_data = _profile_data or profile_name = _profile_name or profile_product = _profile_product;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_data_basic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_data_basic`(_operation varchar(40),_id int(11),_symbol varchar(40),_type varchar(10),_data longblob,_produce varchar(45),_isBinary varchar(2),_isBase64 varchar(2),_isDES varchar(2),_DESKey varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from data_basic;
elseif _operation='insert' then
insert into platformapi.data_basic(symbol,type,data,produce,isBinary,isBase64,isDES,DESKey) values(_symbol,_type,_data,_produce,_isBinary,_isBase64,_isDES,_DESKey);
elseif _operation='update' and _symbol IS NOT NULL then
update data_basic set symbol = _symbol where id = _id;
elseif _operation='update' and _type IS NOT NULL then
update data_basic set type = _type where id = _id;
elseif _operation='update' and _data IS NOT NULL then
update data_basic set data = _data where id = _id;
elseif _operation='update' and _produce IS NOT NULL then
update data_basic set produce = _produce where id = _id;
elseif _operation='update' and _isBinary IS NOT NULL then
update data_basic set isBinary = _isBinary where id = _id;
elseif _operation='update' and _isBase64 IS NOT NULL then
update data_basic set isBase64 = _isBase64 where id = _id;
elseif _operation='update' and _isDES IS NOT NULL then
update data_basic set isDES = _isDES where id = _id;
elseif _operation='update' and _DESKey IS NOT NULL then
update data_basic set DESKey = _DESKey where id = _id;
elseif _operation='selectmixed'then
select * from data_basic where id = IFNULL(_id,id) and symbol = IFNULL(_symbol,symbol) and type = IFNULL(_type,type) and data = IFNULL(_data,data) and produce = IFNULL(_produce,produce) and isBinary = IFNULL(_isBinary,isBinary) and isBase64 = IFNULL(_isBase64,isBase64) and isDES = IFNULL(_isDES,isDES) and DESKey = IFNULL(_DESKey,DESKey);
elseif _operation='delete' then
delete from data_basic where id = _id;
elseif _operation='selectkey' then
select * from data_basic where id = _id;
elseif _operation='selectcondition' then
select * from data_basic where id = _id or symbol = _symbol or type = _type or data = _data or produce = _produce or isBinary = _isBinary or isBase64 = _isBase64 or isDES = _isDES or DESKey = _DESKey;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_data_relationship` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_data_relationship`(_operation varchar(40),_id int(11),_symbol varchar(45),_shiptype varchar(45),_relationdoc blob,_produce varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from data_relationship;
elseif _operation='insert' then
insert into platformapi.data_relationship(symbol,shiptype,relationdoc,produce) values(_symbol,_shiptype,_relationdoc,_produce);
elseif _operation='update' and _symbol IS NOT NULL then
update data_relationship set symbol = _symbol where id = _id;
elseif _operation='update' and _shiptype IS NOT NULL then
update data_relationship set shiptype = _shiptype where id = _id;
elseif _operation='update' and _relationdoc IS NOT NULL then
update data_relationship set relationdoc = _relationdoc where id = _id;
elseif _operation='update' and _produce IS NOT NULL then
update data_relationship set produce = _produce where id = _id;
elseif _operation='selectmixed'then
select * from data_relationship where id = IFNULL(_id,id) and symbol = IFNULL(_symbol,symbol) and shiptype = IFNULL(_shiptype,shiptype) and relationdoc = IFNULL(_relationdoc,relationdoc) and produce = IFNULL(_produce,produce);
elseif _operation='delete' then
delete from data_relationship where id = _id;
elseif _operation='selectkey' then
select * from data_relationship where id = _id;
elseif _operation='selectcondition' then
select * from data_relationship where id = _id or symbol = _symbol or shiptype = _shiptype or relationdoc = _relationdoc or produce = _produce;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-12 16:35:40
