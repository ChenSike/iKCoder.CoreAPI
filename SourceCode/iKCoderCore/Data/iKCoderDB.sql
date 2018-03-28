CREATE DATABASE  IF NOT EXISTS `ikcoder_store` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ikcoder_store`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: ikcoder_store
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
-- Dumping events for database 'ikcoder_store'
--

--
-- Dumping routines for database 'ikcoder_store'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-03-28 17:45:01
CREATE DATABASE  IF NOT EXISTS `ikcoder_basic` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ikcoder_basic`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: ikcoder_basic
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
-- Table structure for table `pool_advisor`
--

DROP TABLE IF EXISTS `pool_advisor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_advisor` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(40) DEFAULT NULL,
  `pwd` varchar(20) DEFAULT NULL,
  `cid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_advisor`
--

LOCK TABLES `pool_advisor` WRITE;
/*!40000 ALTER TABLE `pool_advisor` DISABLE KEYS */;
INSERT INTO `pool_advisor` VALUES (1,'advisor01','123789',1);
/*!40000 ALTER TABLE `pool_advisor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pool_centers`
--

DROP TABLE IF EXISTS `pool_centers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_centers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `isschool` varchar(1) DEFAULT NULL,
  `address` varchar(200) DEFAULT NULL,
  `tel` varchar(100) DEFAULT NULL,
  `level` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_centers`
--

LOCK TABLES `pool_centers` WRITE;
/*!40000 ALTER TABLE `pool_centers` DISABLE KEYS */;
INSERT INTO `pool_centers` VALUES (1,'sz01','0','ShenZhen Bay',NULL,'1');
/*!40000 ALTER TABLE `pool_centers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pool_students`
--

DROP TABLE IF EXISTS `pool_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sid` varchar(40) DEFAULT NULL,
  `uid` varchar(40) DEFAULT NULL,
  `pwd` varchar(20) DEFAULT NULL,
  `nickname` varchar(40) DEFAULT NULL,
  `realname` varchar(40) DEFAULT NULL,
  `sex` varchar(1) DEFAULT NULL,
  `birthdate` varchar(20) DEFAULT NULL,
  `school` varchar(40) DEFAULT NULL,
  `tel` varchar(40) DEFAULT NULL,
  `extended` longblob,
  `cid` int(11) DEFAULT NULL,
  `pid` varchar(45) DEFAULT NULL,
  `status` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_students`
--

LOCK TABLES `pool_students` WRITE;
/*!40000 ALTER TABLE `pool_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `pool_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pool_teachers`
--

DROP TABLE IF EXISTS `pool_teachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pool_teachers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(200) DEFAULT NULL,
  `pwd` varchar(20) DEFAULT NULL,
  `regfrom` int(11) DEFAULT NULL,
  `status` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_teachers`
--

LOCK TABLES `pool_teachers` WRITE;
/*!40000 ALTER TABLE `pool_teachers` DISABLE KEYS */;
INSERT INTO `pool_teachers` VALUES (2,'sike.chen','12345678',0,'0');
/*!40000 ALTER TABLE `pool_teachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'ikcoder_basic'
--

--
-- Dumping routines for database 'ikcoder_basic'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_advisor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_advisor`(_operation varchar(40),_id int(11),_name varchar(40),_pwd varchar(20),_cid int(11))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_advisor;
elseif _operation='insert' then
insert into ikcoder_basic.pool_advisor(name,pwd,cid) values(_name,_pwd,_cid);
elseif _operation='update' and _name IS NOT NULL then
update pool_advisor set name = _name where id = _id;
elseif _operation='update' and _pwd IS NOT NULL then
update pool_advisor set pwd = _pwd where id = _id;
elseif _operation='update' and _cid IS NOT NULL then
update pool_advisor set cid = _cid where id = _id;
elseif _operation='selectmixed'then
select * from pool_advisor where id = IFNULL(_id,id) and name = IFNULL(_name,name) and pwd = IFNULL(_pwd,pwd) and cid = IFNULL(_cid,cid);
elseif _operation='delete' then
delete from pool_advisor where id = _id;
elseif _operation='deletecondition' then
delete from pool_advisor where id = _id or name = _name or pwd = _pwd or cid = _cid;
elseif _operation='deletemixed'then
select * from pool_advisor where id = IFNULL(_id,id) and name = IFNULL(_name,name) and pwd = IFNULL(_pwd,pwd) and cid = IFNULL(_cid,cid);
elseif _operation='selectkey' then
select * from pool_advisor where id = _id;
elseif _operation='selectcondition' then
select * from pool_advisor where id = _id or name = _name or pwd = _pwd or cid = _cid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_centers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_centers`(_operation varchar(40),_id int(11),_name varchar(100),_isschool varchar(1),_address varchar(200),_tel varchar(100),_level varchar(2))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_centers;
elseif _operation='insert' then
insert into ikcoder_basic.pool_centers(name,isschool,address,tel,level) values(_name,_isschool,_address,_tel,_level);
elseif _operation='update' and _name IS NOT NULL then
update pool_centers set name = _name where id = _id;
elseif _operation='update' and _isschool IS NOT NULL then
update pool_centers set isschool = _isschool where id = _id;
elseif _operation='update' and _address IS NOT NULL then
update pool_centers set address = _address where id = _id;
elseif _operation='update' and _tel IS NOT NULL then
update pool_centers set tel = _tel where id = _id;
elseif _operation='update' and _level IS NOT NULL then
update pool_centers set level = _level where id = _id;
elseif _operation='selectmixed'then
select * from pool_centers where id = IFNULL(_id,id) and name = IFNULL(_name,name) and isschool = IFNULL(_isschool,isschool) and address = IFNULL(_address,address) and tel = IFNULL(_tel,tel) and level = IFNULL(_level,level);
elseif _operation='delete' then
delete from pool_centers where id = _id;
elseif _operation='deletecondition' then
delete from pool_centers where id = _id or name = _name or isschool = _isschool or address = _address or tel = _tel or level = _level;
elseif _operation='deletemixed'then
select * from pool_centers where id = IFNULL(_id,id) and name = IFNULL(_name,name) and isschool = IFNULL(_isschool,isschool) and address = IFNULL(_address,address) and tel = IFNULL(_tel,tel) and level = IFNULL(_level,level);
elseif _operation='selectkey' then
select * from pool_centers where id = _id;
elseif _operation='selectcondition' then
select * from pool_centers where id = _id or name = _name or isschool = _isschool or address = _address or tel = _tel or level = _level;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_students`(_operation varchar(40),_id int(11),_sid varchar(40),_uid varchar(40),_pwd varchar(20),_nickname varchar(40),_realname varchar(40),_sex varchar(1),_birthdate varchar(20),_school varchar(40),_tel varchar(40),_extended longblob,_cid int(11),_pid varchar(45),_status varchar(2))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_students;
elseif _operation='insert' then
insert into ikcoder_basic.pool_students(sid,uid,pwd,nickname,realname,sex,birthdate,school,tel,extended,cid,pid,status) values(_sid,_uid,_pwd,_nickname,_realname,_sex,_birthdate,_school,_tel,_extended,_cid,_pid,_status);
elseif _operation='update' and _sid IS NOT NULL then
update pool_students set sid = _sid where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update pool_students set uid = _uid where id = _id;
elseif _operation='update' and _pwd IS NOT NULL then
update pool_students set pwd = _pwd where id = _id;
elseif _operation='update' and _nickname IS NOT NULL then
update pool_students set nickname = _nickname where id = _id;
elseif _operation='update' and _realname IS NOT NULL then
update pool_students set realname = _realname where id = _id;
elseif _operation='update' and _sex IS NOT NULL then
update pool_students set sex = _sex where id = _id;
elseif _operation='update' and _birthdate IS NOT NULL then
update pool_students set birthdate = _birthdate where id = _id;
elseif _operation='update' and _school IS NOT NULL then
update pool_students set school = _school where id = _id;
elseif _operation='update' and _tel IS NOT NULL then
update pool_students set tel = _tel where id = _id;
elseif _operation='update' and _extended IS NOT NULL then
update pool_students set extended = _extended where id = _id;
elseif _operation='update' and _cid IS NOT NULL then
update pool_students set cid = _cid where id = _id;
elseif _operation='update' and _pid IS NOT NULL then
update pool_students set pid = _pid where id = _id;
elseif _operation='update' and _status IS NOT NULL then
update pool_students set status = _status where id = _id;
elseif _operation='selectmixed'then
select * from pool_students where id = IFNULL(_id,id) and sid = IFNULL(_sid,sid) and uid = IFNULL(_uid,uid) and pwd = IFNULL(_pwd,pwd) and nickname = IFNULL(_nickname,nickname) and realname = IFNULL(_realname,realname) and sex = IFNULL(_sex,sex) and birthdate = IFNULL(_birthdate,birthdate) and school = IFNULL(_school,school) and tel = IFNULL(_tel,tel) and extended = IFNULL(_extended,extended) and cid = IFNULL(_cid,cid) and pid = IFNULL(_pid,pid) and status = IFNULL(_status,status);
elseif _operation='delete' then
delete from pool_students where id = _id;
elseif _operation='deletecondition' then
delete from pool_students where id = _id or sid = _sid or uid = _uid or pwd = _pwd or nickname = _nickname or realname = _realname or sex = _sex or birthdate = _birthdate or school = _school or tel = _tel or extended = _extended or cid = _cid or pid = _pid or status = _status;
elseif _operation='deletemixed'then
select * from pool_students where id = IFNULL(_id,id) and sid = IFNULL(_sid,sid) and uid = IFNULL(_uid,uid) and pwd = IFNULL(_pwd,pwd) and nickname = IFNULL(_nickname,nickname) and realname = IFNULL(_realname,realname) and sex = IFNULL(_sex,sex) and birthdate = IFNULL(_birthdate,birthdate) and school = IFNULL(_school,school) and tel = IFNULL(_tel,tel) and extended = IFNULL(_extended,extended) and cid = IFNULL(_cid,cid) and pid = IFNULL(_pid,pid) and status = IFNULL(_status,status);
elseif _operation='selectkey' then
select * from pool_students where id = _id;
elseif _operation='selectcondition' then
select * from pool_students where id = _id or sid = _sid or uid = _uid or pwd = _pwd or nickname = _nickname or realname = _realname or sex = _sex or birthdate = _birthdate or school = _school or tel = _tel or extended = _extended or cid = _cid or pid = _pid or status = _status;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_teachers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_teachers`(_operation varchar(40),_id int(11),_name varchar(200),_pwd varchar(20),_regfrom int(11),_status varchar(2))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_teachers;
elseif _operation='insert' then
insert into ikcoder_basic.pool_teachers(name,pwd,regfrom,status) values(_name,_pwd,_regfrom,_status);
elseif _operation='update' and _name IS NOT NULL then
update pool_teachers set name = _name where id = _id;
elseif _operation='update' and _pwd IS NOT NULL then
update pool_teachers set pwd = _pwd where id = _id;
elseif _operation='update' and _regfrom IS NOT NULL then
update pool_teachers set regfrom = _regfrom where id = _id;
elseif _operation='update' and _status IS NOT NULL then
update pool_teachers set status = _status where id = _id;
elseif _operation='selectmixed'then
select * from pool_teachers where id = IFNULL(_id,id) and name = IFNULL(_name,name) and pwd = IFNULL(_pwd,pwd) and regfrom = IFNULL(_regfrom,regfrom) and status = IFNULL(_status,status);
elseif _operation='delete' then
delete from pool_teachers where id = _id;
elseif _operation='deletecondition' then
delete from pool_teachers where id = _id or name = _name or pwd = _pwd or regfrom = _regfrom or status = _status;
elseif _operation='deletemixed'then
select * from pool_teachers where id = IFNULL(_id,id) and name = IFNULL(_name,name) and pwd = IFNULL(_pwd,pwd) and regfrom = IFNULL(_regfrom,regfrom) and status = IFNULL(_status,status);
elseif _operation='selectkey' then
select * from pool_teachers where id = _id;
elseif _operation='selectcondition' then
select * from pool_teachers where id = _id or name = _name or pwd = _pwd or regfrom = _regfrom or status = _status;
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

-- Dump completed on 2018-03-28 17:45:02
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
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_map_course` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_map_course`(_operation varchar(40),_id int(11),_cap varchar(20),_code varchar(10))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from map_course;
elseif _operation='insert' then
insert into ikcoder_core.map_course(cap,code) values(_cap,_code);
elseif _operation='update' and _cap IS NOT NULL then
update map_course set cap = _cap where id = _id;
elseif _operation='update' and _code IS NOT NULL then
update map_course set code = _code where id = _id;
elseif _operation='selectmixed'then
select * from map_course where id = IFNULL(_id,id) and cap = IFNULL(_cap,cap) and code = IFNULL(_code,code);
elseif _operation='delete' then
delete from map_course where id = _id;
elseif _operation='deletecondition' then
delete from map_course where id = _id or cap = _cap or code = _code;
elseif _operation='deletemixed'then
select * from map_course where id = IFNULL(_id,id) and cap = IFNULL(_cap,cap) and code = IFNULL(_code,code);
elseif _operation='selectkey' then
select * from map_course where id = _id;
elseif _operation='selectcondition' then
select * from map_course where id = _id or cap = _cap or code = _code;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_map_lessons` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_map_lessons`(_operation varchar(40),_id int(11),_symbol varchar(20),_course varchar(20),_conf_basic longblob,_conf_tips longblob,_conf_words longblob,_conf_blockly longblob,_conf_toolbox longblob)
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from map_lessons;
elseif _operation='insert' then
insert into ikcoder_core.map_lessons(symbol,course,conf_basic,conf_tips,conf_words,conf_blockly,conf_toolbox) values(_symbol,_course,_conf_basic,_conf_tips,_conf_words,_conf_blockly,_conf_toolbox);
elseif _operation='update' and _symbol IS NOT NULL then
update map_lessons set symbol = _symbol where id = _id;
elseif _operation='update' and _course IS NOT NULL then
update map_lessons set course = _course where id = _id;
elseif _operation='update' and _conf_basic IS NOT NULL then
update map_lessons set conf_basic = _conf_basic where id = _id;
elseif _operation='update' and _conf_tips IS NOT NULL then
update map_lessons set conf_tips = _conf_tips where id = _id;
elseif _operation='update' and _conf_words IS NOT NULL then
update map_lessons set conf_words = _conf_words where id = _id;
elseif _operation='update' and _conf_blockly IS NOT NULL then
update map_lessons set conf_blockly = _conf_blockly where id = _id;
elseif _operation='update' and _conf_toolbox IS NOT NULL then
update map_lessons set conf_toolbox = _conf_toolbox where id = _id;
elseif _operation='selectmixed'then
select * from map_lessons where id = IFNULL(_id,id) and symbol = IFNULL(_symbol,symbol) and course = IFNULL(_course,course) and conf_basic = IFNULL(_conf_basic,conf_basic) and conf_tips = IFNULL(_conf_tips,conf_tips) and conf_words = IFNULL(_conf_words,conf_words) and conf_blockly = IFNULL(_conf_blockly,conf_blockly) and conf_toolbox = IFNULL(_conf_toolbox,conf_toolbox);
elseif _operation='delete' then
delete from map_lessons where id = _id;
elseif _operation='deletecondition' then
delete from map_lessons where id = _id or symbol = _symbol or course = _course or conf_basic = _conf_basic or conf_tips = _conf_tips or conf_words = _conf_words or conf_blockly = _conf_blockly or conf_toolbox = _conf_toolbox;
elseif _operation='deletemixed'then
select * from map_lessons where id = IFNULL(_id,id) and symbol = IFNULL(_symbol,symbol) and course = IFNULL(_course,course) and conf_basic = IFNULL(_conf_basic,conf_basic) and conf_tips = IFNULL(_conf_tips,conf_tips) and conf_words = IFNULL(_conf_words,conf_words) and conf_blockly = IFNULL(_conf_blockly,conf_blockly) and conf_toolbox = IFNULL(_conf_toolbox,conf_toolbox);
elseif _operation='selectkey' then
select * from map_lessons where id = _id;
elseif _operation='selectcondition' then
select * from map_lessons where id = _id or symbol = _symbol or course = _course or conf_basic = _conf_basic or conf_tips = _conf_tips or conf_words = _conf_words or conf_blockly = _conf_blockly or conf_toolbox = _conf_toolbox;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_asaccount` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_asaccount`(_operation varchar(40),_id int(11),_uname varchar(40),_level int(11),_contribution int(11),_income int(11),_payment int(11),_apps_bm longblob,_apps_buy longblob,_apps_release longblob)
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_asaccount;
elseif _operation='insert' then
insert into ikcoder_core.pool_asaccount(uname,level,contribution,income,payment,apps_bm,apps_buy,apps_release) values(_uname,_level,_contribution,_income,_payment,_apps_bm,_apps_buy,_apps_release);
elseif _operation='update' and _uname IS NOT NULL then
update pool_asaccount set uname = _uname where id = _id;
elseif _operation='update' and _level IS NOT NULL then
update pool_asaccount set level = _level where id = _id;
elseif _operation='update' and _contribution IS NOT NULL then
update pool_asaccount set contribution = _contribution where id = _id;
elseif _operation='update' and _income IS NOT NULL then
update pool_asaccount set income = _income where id = _id;
elseif _operation='update' and _payment IS NOT NULL then
update pool_asaccount set payment = _payment where id = _id;
elseif _operation='update' and _apps_bm IS NOT NULL then
update pool_asaccount set apps_bm = _apps_bm where id = _id;
elseif _operation='update' and _apps_buy IS NOT NULL then
update pool_asaccount set apps_buy = _apps_buy where id = _id;
elseif _operation='update' and _apps_release IS NOT NULL then
update pool_asaccount set apps_release = _apps_release where id = _id;
elseif _operation='selectmixed'then
select * from pool_asaccount where id = IFNULL(_id,id) and uname = IFNULL(_uname,uname) and level = IFNULL(_level,level) and contribution = IFNULL(_contribution,contribution) and income = IFNULL(_income,income) and payment = IFNULL(_payment,payment) and apps_bm = IFNULL(_apps_bm,apps_bm) and apps_buy = IFNULL(_apps_buy,apps_buy) and apps_release = IFNULL(_apps_release,apps_release);
elseif _operation='delete' then
delete from pool_asaccount where id = _id;
elseif _operation='deletecondition' then
delete from pool_asaccount where id = _id or uname = _uname or level = _level or contribution = _contribution or income = _income or payment = _payment or apps_bm = _apps_bm or apps_buy = _apps_buy or apps_release = _apps_release;
elseif _operation='deletemixed'then
select * from pool_asaccount where id = IFNULL(_id,id) and uname = IFNULL(_uname,uname) and level = IFNULL(_level,level) and contribution = IFNULL(_contribution,contribution) and income = IFNULL(_income,income) and payment = IFNULL(_payment,payment) and apps_bm = IFNULL(_apps_bm,apps_bm) and apps_buy = IFNULL(_apps_buy,apps_buy) and apps_release = IFNULL(_apps_release,apps_release);
elseif _operation='selectkey' then
select * from pool_asaccount where id = _id;
elseif _operation='selectcondition' then
select * from pool_asaccount where id = _id or uname = _uname or level = _level or contribution = _contribution or income = _income or payment = _payment or apps_bm = _apps_bm or apps_buy = _apps_buy or apps_release = _apps_release;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_pool_lessonstatus` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_pool_lessonstatus`(_operation varchar(40),_id int(11),_uid int(11),_status longblob,_symbol varchar(20))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from pool_lessonstatus;
elseif _operation='insert' then
insert into ikcoder_core.pool_lessonstatus(uid,status,symbol) values(_uid,_status,_symbol);
elseif _operation='update' and _uid IS NOT NULL then
update pool_lessonstatus set uid = _uid where id = _id;
elseif _operation='update' and _status IS NOT NULL then
update pool_lessonstatus set status = _status where id = _id;
elseif _operation='update' and _symbol IS NOT NULL then
update pool_lessonstatus set symbol = _symbol where id = _id;
elseif _operation='selectmixed'then
select * from pool_lessonstatus where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and status = IFNULL(_status,status) and symbol = IFNULL(_symbol,symbol);
elseif _operation='delete' then
delete from pool_lessonstatus where id = _id;
elseif _operation='deletecondition' then
delete from pool_lessonstatus where id = _id or uid = _uid or status = _status or symbol = _symbol;
elseif _operation='deletemixed'then
select * from pool_lessonstatus where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and status = IFNULL(_status,status) and symbol = IFNULL(_symbol,symbol);
elseif _operation='selectkey' then
select * from pool_lessonstatus where id = _id;
elseif _operation='selectcondition' then
select * from pool_lessonstatus where id = _id or uid = _uid or status = _status or symbol = _symbol;
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

-- Dump completed on 2018-03-28 17:45:02