CREATE DATABASE  IF NOT EXISTS `ikcoder` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ikcoder`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: ikcoder
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
-- Table structure for table `config_blockly`
--

DROP TABLE IF EXISTS `config_blockly`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `config_blockly` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `config` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `config_blockly`
--

LOCK TABLES `config_blockly` WRITE;
/*!40000 ALTER TABLE `config_blockly` DISABLE KEYS */;
/*!40000 ALTER TABLE `config_blockly` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `config_sence`
--

DROP TABLE IF EXISTS `config_sence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `config_sence` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `config` longblob,
  `isfree` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `symbol` (`symbol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `config_sence`
--

LOCK TABLES `config_sence` WRITE;
/*!40000 ALTER TABLE `config_sence` DISABLE KEYS */;
/*!40000 ALTER TABLE `config_sence` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `config_tips`
--

DROP TABLE IF EXISTS `config_tips`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `config_tips` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `config` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `config_tips`
--

LOCK TABLES `config_tips` WRITE;
/*!40000 ALTER TABLE `config_tips` DISABLE KEYS */;
/*!40000 ALTER TABLE `config_tips` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `config_workspace_status`
--

DROP TABLE IF EXISTS `config_workspace_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `config_workspace_status` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `config` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `config_workspace_status`
--

LOCK TABLES `config_workspace_status` WRITE;
/*!40000 ALTER TABLE `config_workspace_status` DISABLE KEYS */;
/*!40000 ALTER TABLE `config_workspace_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resource_binary`
--

DROP TABLE IF EXISTS `resource_binary`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `resource_binary` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `owner` varchar(200) DEFAULT NULL,
  `data` longblob,
  `type` varchar(20) DEFAULT NULL,
  `isbyte` varchar(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `symbol` (`symbol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resource_binary`
--

LOCK TABLES `resource_binary` WRITE;
/*!40000 ALTER TABLE `resource_binary` DISABLE KEYS */;
/*!40000 ALTER TABLE `resource_binary` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resource_text`
--

DROP TABLE IF EXISTS `resource_text`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `resource_text` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `owner` varchar(205) DEFAULT NULL,
  `data` longblob,
  PRIMARY KEY (`id`),
  KEY `symbol` (`symbol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resource_text`
--

LOCK TABLES `resource_text` WRITE;
/*!40000 ALTER TABLE `resource_text` DISABLE KEYS */;
/*!40000 ALTER TABLE `resource_text` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status_documents`
--

DROP TABLE IF EXISTS `status_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `status_documents` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(100) DEFAULT NULL,
  `data` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status_documents`
--

LOCK TABLES `status_documents` WRITE;
/*!40000 ALTER TABLE `status_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `status_documents` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-28 11:00:42
