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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_students`
--

LOCK TABLES `account_students` WRITE;
/*!40000 ALTER TABLE `account_students` DISABLE KEYS */;
INSERT INTO `account_students` VALUES (5,'test_1','12345678',NULL,NULL);
/*!40000 ALTER TABLE `account_students` ENABLE KEYS */;
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
INSERT INTO `profile_students` VALUES (1,'test_1',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `profile_students` ENABLE KEYS */;
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-07-16 17:20:50
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
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_main`
--

LOCK TABLES `course_main` WRITE;
/*!40000 ALTER TABLE `course_main` DISABLE KEYS */;
INSERT INTO `course_main` VALUES (1,'A','逻辑课程');
/*!40000 ALTER TABLE `course_main` ENABLE KEYS */;
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

-- Dump completed on 2018-07-16 17:20:51
