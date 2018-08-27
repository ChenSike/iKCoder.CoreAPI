CREATE DATABASE  IF NOT EXISTS `ikcoder_basic` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `ikcoder_basic`;
-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ikcoder_basic
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account_students`
--

DROP TABLE IF EXISTS `account_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `account_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  `status` varchar(2) DEFAULT '0',
  `type` varchar(2) DEFAULT '0',
  `realname` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_students`
--

LOCK TABLES `account_students` WRITE;
/*!40000 ALTER TABLE `account_students` DISABLE KEYS */;
INSERT INTO `account_students` VALUES (5,'test_1','12345678',NULL,NULL,NULL);
/*!40000 ALTER TABLE `account_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groupmessages_students`
--

DROP TABLE IF EXISTS `groupmessages_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `groupmessages_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from` varchar(45) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  `tolist` longblob,
  `isremoved` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groupmessages_students`
--

LOCK TABLES `groupmessages_students` WRITE;
/*!40000 ALTER TABLE `groupmessages_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `groupmessages_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `message_students`
--

DROP TABLE IF EXISTS `message_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `message_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from` varchar(45) DEFAULT NULL,
  `to` varchar(45) DEFAULT NULL,
  `content` longblob,
  `isremoved` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `message_students`
--

LOCK TABLES `message_students` WRITE;
/*!40000 ALTER TABLE `message_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `message_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messages_students`
--

DROP TABLE IF EXISTS `messages_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `messages_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from` varchar(45) DEFAULT NULL,
  `to` varchar(45) DEFAULT NULL,
  `lastupdate` varchar(45) DEFAULT NULL,
  `content` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messages_students`
--

LOCK TABLES `messages_students` WRITE;
/*!40000 ALTER TABLE `messages_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `messages_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_students`
--

DROP TABLE IF EXISTS `payment_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `payment_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_students`
--

LOCK TABLES `payment_students` WRITE;
/*!40000 ALTER TABLE `payment_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profile_students`
--

DROP TABLE IF EXISTS `profile_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `profile_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(40) DEFAULT NULL,
  `sex` varchar(1) DEFAULT NULL,
  `nickname` varchar(45) DEFAULT NULL,
  `birthday` varchar(20) DEFAULT NULL,
  `header` varchar(50) DEFAULT NULL,
  `country` varchar(20) DEFAULT NULL,
  `state` varchar(40) DEFAULT NULL,
  `city` varchar(40) DEFAULT NULL,
  `schoolmap` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profile_students`
--

LOCK TABLES `profile_students` WRITE;
/*!40000 ALTER TABLE `profile_students` DISABLE KEYS */;
INSERT INTO `profile_students` VALUES (1,'test_1','1','tom','1981-01-01',NULL,'china',NULL,NULL,NULL);
/*!40000 ALTER TABLE `profile_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `relations_students`
--

DROP TABLE IF EXISTS `relations_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `relations_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `puname` varchar(45) DEFAULT NULL,
  `suname` varchar(45) DEFAULT NULL,
  `accectped` varchar(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `relations_students`
--

LOCK TABLES `relations_students` WRITE;
/*!40000 ALTER TABLE `relations_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `relations_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `signin_students`
--

DROP TABLE IF EXISTS `signin_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `signin_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uname` varchar(45) DEFAULT NULL,
  `product` varchar(45) DEFAULT NULL,
  `sdate` varchar(45) DEFAULT NULL,
  `stime` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `signin_students`
--

LOCK TABLES `signin_students` WRITE;
/*!40000 ALTER TABLE `signin_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `signin_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ikcoder_basic'
--

--
-- Dumping routines for database 'ikcoder_basic'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_account_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_account_students`(_operation varchar(40),_id int(11),_name varchar(45),_password varchar(20),_status varchar(2),_type varchar(2))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from account_students;
elseif _operation='insert' then
insert into ikcoder_basic.account_students(name,password,status,type) values(_name,_password,_status,_type);
elseif _operation='update' and _name IS NOT NULL then
update account_students set name = _name where id = _id;
elseif _operation='update' and _password IS NOT NULL then
update account_students set password = _password where id = _id;
elseif _operation='update' and _status IS NOT NULL then
update account_students set status = _status where id = _id;
elseif _operation='update' and _type IS NOT NULL then
update account_students set type = _type where id = _id;
elseif _operation='selectmixed'then
select * from account_students where id = IFNULL(_id,id) and name = IFNULL(_name,name) and password = IFNULL(_password,password) and status = IFNULL(_status,status) and type = IFNULL(_type,type);
elseif _operation='delete' then
delete from account_students where id = _id;
elseif _operation='deletecondition' then
delete from account_students where id = _id or name = _name or password = _password or status = _status or type = _type;
elseif _operation='deletemixed'then
select * from account_students where id = IFNULL(_id,id) and name = IFNULL(_name,name) and password = IFNULL(_password,password) and status = IFNULL(_status,status) and type = IFNULL(_type,type);
elseif _operation='selectkey' then
select * from account_students where id = _id;
elseif _operation='selectcondition' then
select * from account_students where id = _id or name = _name or password = _password or status = _status or type = _type;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_profile_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_profile_students`(_operation varchar(40),_birthday varchar(20),_city varchar(40),_country varchar(20),_header varchar(50),_id int(11),_nickname varchar(45),_schoolmap longblob,_sex varchar(1),_state varchar(40),_uid varchar(40))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from profile_students;
elseif _operation='insert' then
insert into ikcoder_basic.profile_students(birthday,city,country,header,nickname,schoolmap,sex,state,uid) values(_birthday,_city,_country,_header,_nickname,_schoolmap,_sex,_state,_uid);
elseif _operation='update' and _birthday IS NOT NULL then
update profile_students set birthday = _birthday where id = _id;
elseif _operation='update' and _city IS NOT NULL then
update profile_students set city = _city where id = _id;
elseif _operation='update' and _country IS NOT NULL then
update profile_students set country = _country where id = _id;
elseif _operation='update' and _header IS NOT NULL then
update profile_students set header = _header where id = _id;
elseif _operation='update' and _nickname IS NOT NULL then
update profile_students set nickname = _nickname where id = _id;
elseif _operation='update' and _schoolmap IS NOT NULL then
update profile_students set schoolmap = _schoolmap where id = _id;
elseif _operation='update' and _sex IS NOT NULL then
update profile_students set sex = _sex where id = _id;
elseif _operation='update' and _state IS NOT NULL then
update profile_students set state = _state where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update profile_students set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from profile_students where birthday = IFNULL(_birthday,birthday) and city = IFNULL(_city,city) and country = IFNULL(_country,country) and header = IFNULL(_header,header) and id = IFNULL(_id,id) and nickname = IFNULL(_nickname,nickname) and schoolmap = IFNULL(_schoolmap,schoolmap) and sex = IFNULL(_sex,sex) and state = IFNULL(_state,state) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from profile_students where id = _id;
elseif _operation='deletecondition' then
delete from profile_students where birthday = _birthday or city = _city or country = _country or header = _header or id = _id or nickname = _nickname or schoolmap = _schoolmap or sex = _sex or state = _state or uid = _uid;
elseif _operation='deletemixed'then
select * from profile_students where birthday = IFNULL(_birthday,birthday) and city = IFNULL(_city,city) and country = IFNULL(_country,country) and header = IFNULL(_header,header) and id = IFNULL(_id,id) and nickname = IFNULL(_nickname,nickname) and schoolmap = IFNULL(_schoolmap,schoolmap) and sex = IFNULL(_sex,sex) and state = IFNULL(_state,state) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from profile_students where id = _id;
elseif _operation='selectcondition' then
select * from profile_students where birthday = _birthday or city = _city or country = _country or header = _header or id = _id or nickname = _nickname or schoolmap = _schoolmap or sex = _sex or state = _state or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_relations_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_relations_students`(_operation varchar(40),_accectped varchar(1),_id int(11),_puname varchar(45),_suname varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from relations_students;
elseif _operation='insert' then
insert into ikcoder_basic.relations_students(accectped,puname,suname) values(_accectped,_puname,_suname);
elseif _operation='update' and _accectped IS NOT NULL then
update relations_students set accectped = _accectped where id = _id;
elseif _operation='update' and _puname IS NOT NULL then
update relations_students set puname = _puname where id = _id;
elseif _operation='update' and _suname IS NOT NULL then
update relations_students set suname = _suname where id = _id;
elseif _operation='selectmixed'then
select * from relations_students where accectped = IFNULL(_accectped,accectped) and id = IFNULL(_id,id) and puname = IFNULL(_puname,puname) and suname = IFNULL(_suname,suname);
elseif _operation='delete' then
delete from relations_students where id = _id;
elseif _operation='deletecondition' then
delete from relations_students where accectped = _accectped or id = _id or puname = _puname or suname = _suname;
elseif _operation='deletemixed'then
select * from relations_students where accectped = IFNULL(_accectped,accectped) and id = IFNULL(_id,id) and puname = IFNULL(_puname,puname) and suname = IFNULL(_suname,suname);
elseif _operation='selectkey' then
select * from relations_students where id = _id;
elseif _operation='selectcondition' then
select * from relations_students where accectped = _accectped or id = _id or puname = _puname or suname = _suname;
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

-- Dump completed on 2018-08-13 12:15:16
