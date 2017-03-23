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
  `password` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_basic`
--

LOCK TABLES `account_basic` WRITE;
/*!40000 ALTER TABLE `account_basic` DISABLE KEYS */;
INSERT INTO `account_basic` VALUES (5,'18675521735','01070624'),(6,'13632623840','111111'),(7,'18676781672','123456'),(8,'18603052346','ikcoder');
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_profile`
--

LOCK TABLES `account_profile` WRITE;
/*!40000 ALTER TABLE `account_profile` DISABLE KEYS */;
INSERT INTO `account_profile` VALUES (2,'18675521735','<root><docbasic><doc_id></doc_id><doc_symbol>profile_18675521735</doc_symbol></docbasic><usrbasic><usr_name>18675521735</usr_name><usr_nickname></usr_nickname><coins>0</coins><account_status>L0</account_status><account_limited></account_limited><account_childs></account_childs><account_head></account_head></usrbasic><lessons><begin></begin><intermediate></intermediate><senior></senior></lessons><friends></friends></root>','profile_18675521735','iKCoder'),(3,'13632623840','<root><docbasic><doc_id></doc_id><doc_symbol>profile_13632623840</doc_symbol></docbasic><usrbasic><usr_name>13632623840</usr_name><usr_nickname></usr_nickname><coins>0</coins><account_status>L0</account_status><account_limited></account_limited><account_childs></account_childs><account_head></account_head></usrbasic><lessons><begin></begin><intermediate></intermediate><senior></senior></lessons><friends></friends></root>','profile_13632623840','iKCoder'),(4,'18676781672','<root><docbasic><doc_id></doc_id><doc_symbol>profile_18676781672</doc_symbol></docbasic><usrbasic><usr_name>18676781672</usr_name><usr_nickname></usr_nickname><coins>0</coins><account_status>L0</account_status><account_limited></account_limited><account_childs></account_childs><account_head></account_head></usrbasic><lessons><begin></begin><intermediate></intermediate><senior></senior></lessons><friends></friends></root>','profile_18676781672','iKCoder'),(5,'18603052346','<root><docbasic><doc_id></doc_id><doc_symbol>profile_18603052346</doc_symbol></docbasic><usrbasic><usr_name>18603052346</usr_name><usr_nickname></usr_nickname><coins>0</coins><account_status>L0</account_status><account_limited></account_limited><account_childs></account_childs><account_head></account_head></usrbasic><lessons><begin></begin><intermediate></intermediate><senior></senior></lessons><friends></friends></root>','profile_18603052346','iKCoder');
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `data_basic`
--

LOCK TABLES `data_basic` WRITE;
/*!40000 ALTER TABLE `data_basic` DISABLE KEYS */;
INSERT INTO `data_basic` VALUES (1,'TestData','text','','iKCoder','0','0','0',''),(2,'img_testdata_1','png','iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAQAAABpN6lAAAAACXBIWXMAAAsTAAALEwEAmpwYAAAABGdBTUEAALGOfPtRkwAAACBjSFJNAAB6JQAAgIMAAPn/AACA6QAAdTAAAOpgAAA6mAAAF2+SX8VGAAAFJklEQVR42uycfWiWVRTAf+593fyg/EDnBgUFVlqGqH1Rgh+lEpbumE7FVEKCogxMamW1ZiZ9iRRWZOZH6HRZdqaFpdM5MEZrVKCQfa0P06aSbiGh5ab9s4h8n+V73Z49j+e95897z+G558e59zn3ec69nc6Q2ZKFB+ABeAAegAfgAXgAHoAH4AF4AJkoyTgNRvozjivII5vD/MIuavR02M/sFI/doCSYSREDzmo+ynKWaIN5ADKU0hTn/5Hfma8rTQOQQlbT7X9VlvGwNhldBKWQsnO4D3NZYTQCZAjVdElLtUhfNAdAsqjhujSVm7hav7M2Baan7T4kKbG3BtznpD1N+pkCIPkMdxzrOFsRcKOzxW22AAx0trjMFoBezha21gAScdi6RQngN2eLw7YAfO9scdAWgE+cLapMAdB6ahxNPrKWCS530t6hP9jKBLtyt5PBU6b2AtKVLYx2MFijnxoCIF0od0psa7k/nJFEAkByKGesg8EeJupJMwAkB3Xa15UzXOvDGk2HA5BsNnF7QMdOTgS07mOSih4PbzzJCNwfH9BRrIukN5MZyWD6kU0jP1LDB7o77BF16DdB6cx7TAjoKNGFUb2NsjrU/Y2B7j8TnfsdCECSlFEQ0LFIn44yGc3qMPc3MCmgY7EWR5qMdwwASVDK5ICO5/RJMA9AEpRSGNDxgi4A8wAkwVqmBnS8pI8RA2nHPEAGMpLryeMiGjnIbir0iGTxNtMDlJfoo8RC2ikPkAIe4eazGk/xLsnA4F+q88EMAOnHysDsrjV5WecRG0m2Q+Bvdfph8Uqc3G9zBMil1Dr9rlimDxEradNbQLqx2cn9V+Pmfltfg08wxEH7dZ1L7KQNU0DyqDtndc+/0kBuWIVOUUXAgw7uQ6/AneAFDWCio/5UUwDkEgY5moy2FQH9nS36SK4lAPnnYdPHEoDzySETlgAcOg+bBksAfna2OB5GgUNkAPRbZwTVesZSBMCHjvqbrCVCr9HsoN1IqTEAuo+1Duo9HMshLojd4AJ+TX/bxRtyrzEAWk8BJx0QLJc5tiIAreUu/nBAsELuMQUAdCsjqHNA8JbMNgUA9HMGUcLRgK69fBHwxFUyKz4A2q0+QHKYwAiGkUt3GjhANdu0Wi5mGzelKJ9mtq4zBqBVMD3Yzg0pzc3M0vUZAQCkBzsCDkc1M1M3ZAQAkJ7sYFgAghn6joFFMI1lspExActhgnUyJSMAgDYwhi9TmpOsl8kZMAVaJkJvKhmc0tzEVH3ffAQA6DFuZU9AFJRJQUZEAID0oZJrU5pPMUU3gwxlFIPJpXNLoeROPWYMAEhfKgP+KPzFUu7kmrNaT7KJhWEcmY4QAEgulSmuti5/8iyLw/uYFsnxecmlyunc6EZmGyqXBz3CKL52MChktYG3wH8QHGYU3zgYTJPHDU2BlomQTxVXpq3exACtMxMBAFrvdCdEkmJDU6BF3D6MzJC+pgBIT6eDU5CwdoPELc5PH2sLgPsNEpfbAuBeL5LxN0hk2QLQ6GyR8TdI7LcF4DNni522MsE6vnI0+dhaJvimk3a5HrAH4Ke0dc8Y3AvoCR4g3e3oUt1rbzOEbiW9s4PbKQpnBJHfKarPU3LOKKigUJvDeX48rtUtZBXdW+1exryw3I/Pxcp5lDAnoP54F0VaG+aTO8WneFPyuYPxXEU+2RxiPxVsCWfhiymAaMRfr+8BeAAegAfgAXgAHoAH4AF4AB6AB+ABeAAegAfgAXgAHoAHkEny9wC4Mjyulpx6GwAAAABJRU5ErkJggg==','iKCoder','1','1','0',''),(3,'profile_template_ikcoder','text','PHJvb3Q+PGRvY2Jhc2ljPjxkb2NfaWQ+PC9kb2NfaWQ+PGRvY19zeW1ib2w+PC9kb2Nfc3ltYm9sPjwvZG9jYmFzaWM+PHVzcmJhc2ljPjx1c3JfbmFtZT48L3Vzcl9uYW1lPjx1c3Jfbmlja25hbWU+PC91c3Jfbmlja25hbWU+PGNvaW5zPjwvY29pbnM+PGFjY291bnRfc3RhdHVzPjwvYWNjb3VudF9zdGF0dXM+PGFjY291bnRfbGltaXRlZD48L2FjY291bnRfbGltaXRlZD48YWNjb3VudF9oZWFkPjwvYWNjb3VudF9oZWFkPjxhY2NvdW50X2NoaWxkcz48L2FjY291bnRfY2hpbGRzPjxhY2NvdW50X3Ntcz48L2FjY291bnRfc21zPjwvdXNyYmFzaWM+PGxlc3NvbnM+PGJlZ2luPjwvYmVnaW4+PGludGVybWVkaWF0ZT48L2ludGVybWVkaWF0ZT48c2VuaW9yPjwvc2VuaW9yPjwvbGVzc29ucz48ZnJpZW5kcz48L2ZyaWVuZHM+PGNvZGV0aW1lPjwvY29kZXRpbWU+PC9yb290Pg==','iKCoder','0','0','0','');
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_account_basic`(_operation varchar(40),_id int(11),_username varchar(200),_password varchar(20))
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

-- Dump completed on 2017-03-24  0:38:01
