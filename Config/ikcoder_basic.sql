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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pool_centers`
--

LOCK TABLES `pool_centers` WRITE;
/*!40000 ALTER TABLE `pool_centers` DISABLE KEYS */;
/*!40000 ALTER TABLE `pool_centers` ENABLE KEYS */;
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

-- Dump completed on 2018-03-27  0:23:44
