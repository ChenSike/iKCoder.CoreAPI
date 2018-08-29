CREATE DATABASE  IF NOT EXISTS `ikcoder_appmain` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `ikcoder_appmain`;
-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ikcoder_appmain
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
-- Table structure for table `course_basic`
--

DROP TABLE IF EXISTS `course_basic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_basic` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `course_name` varchar(20) DEFAULT NULL,
  `lesson_title` varchar(45) DEFAULT NULL,
  `isfree` varchar(1) DEFAULT NULL,
  `lesson_code` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_basic`
--

LOCK TABLES `course_basic` WRITE;
/*!40000 ALTER TABLE `course_basic` DISABLE KEYS */;
INSERT INTO `course_basic` VALUES (1,'A','模式识别','1','A_01_001');
/*!40000 ALTER TABLE `course_basic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `course_main`
--

DROP TABLE IF EXISTS `course_main`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_main` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  `isfree` varchar(1) DEFAULT '1',
  `price` int(11) DEFAULT '0',
  `discount` int(11) DEFAULT '0',
  `stem` varchar(10) DEFAULT NULL,
  `udba` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_main`
--

LOCK TABLES `course_main` WRITE;
/*!40000 ALTER TABLE `course_main` DISABLE KEYS */;
INSERT INTO `course_main` VALUES (1,'A','逻辑课程','1',0,0,NULL,NULL);
/*!40000 ALTER TABLE `course_main` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_checkon`
--

DROP TABLE IF EXISTS `students_checkon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_checkon` (
  `id` int(11) NOT NULL,
  `uid` varchar(45) DEFAULT NULL,
  `checkdate` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_checkon`
--

LOCK TABLES `students_checkon` WRITE;
/*!40000 ALTER TABLE `students_checkon` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_checkon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_coursepackage`
--

DROP TABLE IF EXISTS `students_coursepackage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_coursepackage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `courseid` int(11) DEFAULT NULL,
  `overdate` varchar(45) DEFAULT NULL,
  `paymentid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_coursepackage`
--

LOCK TABLES `students_coursepackage` WRITE;
/*!40000 ALTER TABLE `students_coursepackage` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_coursepackage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_exp`
--

DROP TABLE IF EXISTS `students_exp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_exp` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) DEFAULT NULL,
  `uname` varchar(45) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_exp`
--

LOCK TABLES `students_exp` WRITE;
/*!40000 ALTER TABLE `students_exp` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_exp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_learninrecord`
--

DROP TABLE IF EXISTS `students_learninrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_learninrecord` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `lesson_id` int(11) DEFAULT NULL,
  `actions` varchar(20) DEFAULT NULL,
  `datetime` varchar(45) DEFAULT NULL,
  `times` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_learninrecord`
--

LOCK TABLES `students_learninrecord` WRITE;
/*!40000 ALTER TABLE `students_learninrecord` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_learninrecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `titles_defined`
--

DROP TABLE IF EXISTS `titles_defined`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `titles_defined` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `titles` varchar(45) DEFAULT NULL,
  `exp_min` int(11) DEFAULT NULL,
  `exp_max` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `titles_defined`
--

LOCK TABLES `titles_defined` WRITE;
/*!40000 ALTER TABLE `titles_defined` DISABLE KEYS */;
INSERT INTO `titles_defined` VALUES (1,'t_01_001','逻辑战士L1',0,'100'),(2,'t_01_002','逻辑战士L2',100,'300');
/*!40000 ALTER TABLE `titles_defined` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ikcoder_appmain'
--

--
-- Dumping routines for database 'ikcoder_appmain'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_course_basic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_course_basic`(_operation varchar(40),_course_name varchar(20),_id int(11),_isfree varchar(1),_lesson_code varchar(20),_lesson_title varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from course_basic;
elseif _operation='insert' then
insert into ikcoder_appmain.course_basic(course_name,isfree,lesson_code,lesson_title) values(_course_name,_isfree,_lesson_code,_lesson_title);
elseif _operation='update' and _course_name IS NOT NULL then
update course_basic set course_name = _course_name where id = _id;
elseif _operation='update' and _isfree IS NOT NULL then
update course_basic set isfree = _isfree where id = _id;
elseif _operation='update' and _lesson_code IS NOT NULL then
update course_basic set lesson_code = _lesson_code where id = _id;
elseif _operation='update' and _lesson_title IS NOT NULL then
update course_basic set lesson_title = _lesson_title where id = _id;
elseif _operation='selectmixed'then
select * from course_basic where course_name = IFNULL(_course_name,course_name) and id = IFNULL(_id,id) and isfree = IFNULL(_isfree,isfree) and lesson_code = IFNULL(_lesson_code,lesson_code) and lesson_title = IFNULL(_lesson_title,lesson_title);
elseif _operation='delete' then
delete from course_basic where id = _id;
elseif _operation='deletecondition' then
delete from course_basic where course_name = _course_name or id = _id or isfree = _isfree or lesson_code = _lesson_code or lesson_title = _lesson_title;
elseif _operation='deletemixed'then
select * from course_basic where course_name = IFNULL(_course_name,course_name) and id = IFNULL(_id,id) and isfree = IFNULL(_isfree,isfree) and lesson_code = IFNULL(_lesson_code,lesson_code) and lesson_title = IFNULL(_lesson_title,lesson_title);
elseif _operation='selectkey' then
select * from course_basic where id = _id;
elseif _operation='selectcondition' then
select * from course_basic where course_name = _course_name or id = _id or isfree = _isfree or lesson_code = _lesson_code or lesson_title = _lesson_title;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_course_main` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_course_main`(_operation varchar(40),_id int(11),_name varchar(20),_title varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from course_main;
elseif _operation='insert' then
insert into ikcoder_appmain.course_main(name,title) values(_name,_title);
elseif _operation='update' and _name IS NOT NULL then
update course_main set name = _name where id = _id;
elseif _operation='update' and _title IS NOT NULL then
update course_main set title = _title where id = _id;
elseif _operation='selectmixed'then
select * from course_main where id = IFNULL(_id,id) and name = IFNULL(_name,name) and title = IFNULL(_title,title);
elseif _operation='delete' then
delete from course_main where id = _id;
elseif _operation='deletecondition' then
delete from course_main where id = _id or name = _name or title = _title;
elseif _operation='deletemixed'then
select * from course_main where id = IFNULL(_id,id) and name = IFNULL(_name,name) and title = IFNULL(_title,title);
elseif _operation='selectkey' then
select * from course_main where id = _id;
elseif _operation='selectcondition' then
select * from course_main where id = _id or name = _name or title = _title;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_checkon` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_checkon`(_operation varchar(40),_checkdate varchar(45),_id int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_checkon;
elseif _operation='insert' then
insert into ikcoder_appmain.students_checkon(checkdate,id,uid) values(_checkdate,_id,_uid);
elseif _operation='update' and _checkdate IS NOT NULL then
update students_checkon set checkdate = _checkdate where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_checkon set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_checkon where checkdate = IFNULL(_checkdate,checkdate) and id = IFNULL(_id,id) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_checkon where id = _id;
elseif _operation='deletecondition' then
delete from students_checkon where checkdate = _checkdate or id = _id or uid = _uid;
elseif _operation='deletemixed'then
select * from students_checkon where checkdate = IFNULL(_checkdate,checkdate) and id = IFNULL(_id,id) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_checkon where id = _id;
elseif _operation='selectcondition' then
select * from students_checkon where checkdate = _checkdate or id = _id or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_coursepackage` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_coursepackage`(_operation varchar(40),_courseid int(11),_id int(11),_overdate varchar(45),_paymentid int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_coursepackage;
elseif _operation='insert' then
insert into ikcoder_appmain.students_coursepackage(courseid,overdate,paymentid,uid) values(_courseid,_overdate,_paymentid,_uid);
elseif _operation='update' and _courseid IS NOT NULL then
update students_coursepackage set courseid = _courseid where id = _id;
elseif _operation='update' and _overdate IS NOT NULL then
update students_coursepackage set overdate = _overdate where id = _id;
elseif _operation='update' and _paymentid IS NOT NULL then
update students_coursepackage set paymentid = _paymentid where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_coursepackage set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_coursepackage where courseid = IFNULL(_courseid,courseid) and id = IFNULL(_id,id) and overdate = IFNULL(_overdate,overdate) and paymentid = IFNULL(_paymentid,paymentid) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_coursepackage where id = _id;
elseif _operation='deletecondition' then
delete from students_coursepackage where courseid = _courseid or id = _id or overdate = _overdate or paymentid = _paymentid or uid = _uid;
elseif _operation='deletemixed'then
select * from students_coursepackage where courseid = IFNULL(_courseid,courseid) and id = IFNULL(_id,id) and overdate = IFNULL(_overdate,overdate) and paymentid = IFNULL(_paymentid,paymentid) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_coursepackage where id = _id;
elseif _operation='selectcondition' then
select * from students_coursepackage where courseid = _courseid or id = _id or overdate = _overdate or paymentid = _paymentid or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_exp` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_exp`(_operation varchar(40),_id int(11),_uid int(11),_uname varchar(45),_exp int(11))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_exp;
elseif _operation='insert' then
insert into ikcoder_appmain.students_exp(uid,uname,exp) values(_uid,_uname,_exp);
elseif _operation='update' and _uid IS NOT NULL then
update students_exp set uid = _uid where id = _id;
elseif _operation='update' and _uname IS NOT NULL then
update students_exp set uname = _uname where id = _id;
elseif _operation='update' and _exp IS NOT NULL then
update students_exp set exp = _exp where id = _id;
elseif _operation='selectmixed'then
select * from students_exp where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and uname = IFNULL(_uname,uname) and exp = IFNULL(_exp,exp);
elseif _operation='delete' then
delete from students_exp where id = _id;
elseif _operation='deletecondition' then
delete from students_exp where id = _id or uid = _uid or uname = _uname or exp = _exp;
elseif _operation='deletemixed'then
select * from students_exp where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and uname = IFNULL(_uname,uname) and exp = IFNULL(_exp,exp);
elseif _operation='selectkey' then
select * from students_exp where id = _id;
elseif _operation='selectcondition' then
select * from students_exp where id = _id or uid = _uid or uname = _uname or exp = _exp;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_learninrecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_learninrecord`(_operation varchar(40),_actions varchar(20),_datetime varchar(45),_id int(11),_lesson_id int(11),_times int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_learninrecord;
elseif _operation='insert' then
insert into ikcoder_appmain.students_learninrecord(actions,datetime,lesson_id,times,uid) values(_actions,_datetime,_lesson_id,_times,_uid);
elseif _operation='update' and _actions IS NOT NULL then
update students_learninrecord set actions = _actions where id = _id;
elseif _operation='update' and _datetime IS NOT NULL then
update students_learninrecord set datetime = _datetime where id = _id;
elseif _operation='update' and _lesson_id IS NOT NULL then
update students_learninrecord set lesson_id = _lesson_id where id = _id;
elseif _operation='update' and _times IS NOT NULL then
update students_learninrecord set times = _times where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_learninrecord set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_learninrecord where actions = IFNULL(_actions,actions) and datetime = IFNULL(_datetime,datetime) and id = IFNULL(_id,id) and lesson_id = IFNULL(_lesson_id,lesson_id) and times = IFNULL(_times,times) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_learninrecord where id = _id;
elseif _operation='deletecondition' then
delete from students_learninrecord where actions = _actions or datetime = _datetime or id = _id or lesson_id = _lesson_id or times = _times or uid = _uid;
elseif _operation='deletemixed'then
select * from students_learninrecord where actions = IFNULL(_actions,actions) and datetime = IFNULL(_datetime,datetime) and id = IFNULL(_id,id) and lesson_id = IFNULL(_lesson_id,lesson_id) and times = IFNULL(_times,times) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_learninrecord where id = _id;
elseif _operation='selectcondition' then
select * from students_learninrecord where actions = _actions or datetime = _datetime or id = _id or lesson_id = _lesson_id or times = _times or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_titles_defined` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_titles_defined`(_operation varchar(40),_id int(11),_name varchar(45),_titles varchar(45),_exp_min int(11),_exp_max varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from titles_defined;
elseif _operation='insert' then
insert into ikcoder_appmain.titles_defined(name,titles,exp_min,exp_max) values(_name,_titles,_exp_min,_exp_max);
elseif _operation='update' and _name IS NOT NULL then
update titles_defined set name = _name where id = _id;
elseif _operation='update' and _titles IS NOT NULL then
update titles_defined set titles = _titles where id = _id;
elseif _operation='update' and _exp_min IS NOT NULL then
update titles_defined set exp_min = _exp_min where id = _id;
elseif _operation='update' and _exp_max IS NOT NULL then
update titles_defined set exp_max = _exp_max where id = _id;
elseif _operation='selectmixed'then
select * from titles_defined where id = IFNULL(_id,id) and name = IFNULL(_name,name) and titles = IFNULL(_titles,titles) and exp_min = IFNULL(_exp_min,exp_min) and exp_max = IFNULL(_exp_max,exp_max);
elseif _operation='delete' then
delete from titles_defined where id = _id;
elseif _operation='deletecondition' then
delete from titles_defined where id = _id or name = _name or titles = _titles or exp_min = _exp_min or exp_max = _exp_max;
elseif _operation='deletemixed'then
select * from titles_defined where id = IFNULL(_id,id) and name = IFNULL(_name,name) and titles = IFNULL(_titles,titles) and exp_min = IFNULL(_exp_min,exp_min) and exp_max = IFNULL(_exp_max,exp_max);
elseif _operation='selectkey' then
select * from titles_defined where id = _id;
elseif _operation='selectcondition' then
select * from titles_defined where id = _id or name = _name or titles = _titles or exp_min = _exp_min or exp_max = _exp_max;
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

-- Dump completed on 2018-08-13 11:05:05
