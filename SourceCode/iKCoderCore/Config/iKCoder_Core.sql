CREATE DATABASE  IF NOT EXISTS `ikcoder_core` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ikcoder_core`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: ikcoder_core
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
-- Table structure for table `map_course`
--

DROP TABLE IF EXISTS `map_course`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `map_course` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `cap` varchar(20) DEFAULT NULL,
  `code` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `map_course`
--

LOCK TABLES `map_course` WRITE;
/*!40000 ALTER TABLE `map_course` DISABLE KEYS */;
INSERT INTO `map_course` VALUES (1,'A','A0'),(2,'B','B0'),(3,'C','C0'),(4,'S-J','SJ0'),(5,'S-M','SM0');
/*!40000 ALTER TABLE `map_course` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `map_lessons`
--

DROP TABLE IF EXISTS `map_lessons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `map_lessons` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(20) DEFAULT NULL,
  `course` varchar(20) DEFAULT NULL,
  `conf_basic` longblob,
  `conf_tips` longblob,
  `conf_words` longblob,
  `conf_blockly` longblob,
  `conf_toolbox` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `map_lessons`
--

LOCK TABLES `map_lessons` WRITE;
/*!40000 ALTER TABLE `map_lessons` DISABLE KEYS */;
/*!40000 ALTER TABLE `map_lessons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pool_asaccount`
--

DROP TABLE IF EXISTS `pool_asaccount`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_asaccount` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uname` varchar(40) DEFAULT NULL,
  `level` int(11) DEFAULT NULL,
  `contribution` int(11) DEFAULT NULL,
  `income` int(11) DEFAULT NULL,
  `payment` int(11) DEFAULT NULL,
  `apps_bm` longblob,
  `apps_buy` longblob,
  `apps_release` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_asaccount`
--

LOCK TABLES `pool_asaccount` WRITE;
/*!40000 ALTER TABLE `pool_asaccount` DISABLE KEYS */;
/*!40000 ALTER TABLE `pool_asaccount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pool_lessonstatus`
--

DROP TABLE IF EXISTS `pool_lessonstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_lessonstatus` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) DEFAULT NULL,
  `status` longblob,
  `symbol` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_lessonstatus`
--

LOCK TABLES `pool_lessonstatus` WRITE;
/*!40000 ALTER TABLE `pool_lessonstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `pool_lessonstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ikcoder_core'
--

--
-- Dumping routines for database 'ikcoder_core'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-27 16:31:43
