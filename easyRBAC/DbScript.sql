-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: easyrbac
-- ------------------------------------------------------
-- Server version	5.7.21-log

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
-- Table structure for table `app_resource`
--

DROP TABLE IF EXISTS `app_resource`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `app_resource` (
  `id` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
  `applicationId` bigint(20) NOT NULL,
  `resourceCode` varchar(45) CHARACTER SET utf8mb4 NOT NULL,
  `resourceName` varchar(45) CHARACTER SET utf8mb4 NOT NULL,
  `enable` bit(1) NOT NULL,
  `url` varchar(100) CHARACTER SET utf8mb4 DEFAULT NULL,
  `resourceType` tinyint(4) NOT NULL,
  `iconUrl` varchar(45) CHARACTER SET utf8mb4 DEFAULT NULL,
  `parameters` varchar(45) CHARACTER SET utf8mb4 DEFAULT NULL,
  `describe` varchar(200) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `app_resouce` (`applicationId`,`resourceCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `app_resource`
--

LOCK TABLES `app_resource` WRITE;
/*!40000 ALTER TABLE `app_resource` DISABLE KEYS */;
INSERT INTO `app_resource` VALUES ('07',1202052677035361290,'easyRBAC_root','easyRBAC','',NULL,0,NULL,NULL,NULL),('0701',1202052677035361290,'User','用户','','',2,'','',''),('070101',1202052677035361290,'userManage','用户管理','','/user',2,'','',''),('07010101',1202052677035361290,'CreateUser','创建用户','','',1,'','',''),('07010102',1202052677035361290,'ChangeUserPassword','修改密码','','',1,'','',''),('07010103',1202052677035361290,'DisableUser','删除用户','','',1,'','',''),('07010104',1202052677035361290,'GetUserInfo','获取用户信息','','',1,'','',''),('07010105',1202052677035361290,'SearchUsers','搜索用户','','',1,'','',''),('070102',1202052677035361290,'UserResourceManage','用户资源管理','','/user/resource',2,'','',''),('07010201',1202052677035361290,'ChangeResources','修改用户资源','','',1,'','',''),('07010202',1202052677035361290,'GetUserResourceIds','获取用户资源ID','','',1,'','',''),('0702',1202052677035361290,'Role','角色','','',2,'','',''),('070201',1202052677035361290,'roleManage','角色管理','','/role',2,'','',''),('07020101',1202052677035361290,'AddRole','添加角色','','',1,'','',''),('07020102',1202052677035361290,'DisableRole','删除角色','','',1,'','',''),('07020103',1202052677035361290,'EditRoleInfo','编辑角色信息','','',1,'','',''),('07020104',1202052677035361290,'GetRoleInfo','获取角色信息','','',1,'','',''),('07020105',1202052677035361290,'SearchRoles','搜索角色','','',1,'','',''),('070202',1202052677035361290,'roleResourceManage','角色资源管理','','/role/roleResource',2,'','',''),('07020201',1202052677035361290,'ChangeResouces','修改角色资源','','',1,'','',''),('070203',1202052677035361290,'roleUserManage','角色用户管理','','/role/user',2,'','',''),('07020301',1202052677035361290,'ChangeMember','修改角色成员','','',1,'','',''),('07020302',1202052677035361290,'GetUserIdsInRole','获取角色内用户ID','','',1,'','',''),('0703',1202052677035361290,'app','应用','','',2,'','',''),('070301',1202052677035361290,'appManage','应用管理','','/application',2,'','',''),('070302',1202052677035361290,'appResource','应用资源管理','','/app/resource',2,'','',''),('07030201',1202052677035361290,'AddResource','添加资源','','',1,'','',''),('07030202',1202052677035361290,'DisableResource','删除资源','','',1,'','',''),('07030203',1202052677035361290,'EditResource','编辑资源','','',1,'','',''),('07030204',1202052677035361290,'GetResourceInfo','获取资源信息','','',1,'','',''),('07030205',1202052677035361290,'GetAppResource','获取应用资源','','',1,'','',''),('07030206',1202052677035361290,'GetResourceTree','获取资源树','','',1,'','',''),('07030207',1202052677035361290,'GetRoleResourceIds','获取角色应用资源','','',1,'','',''),('070303',1202052677035361290,'GetAppInfo','获取应用信息','','/Application/Get',1,'','',''),('070304',1202052677035361290,'AddApp','新增应用','','/Application/AddApp',1,'','',''),('070305',1202052677035361290,'UpdateAppInfo','更新应用信息','','',1,'','',''),('070306',1202052677035361290,'DeleteApp','删除应用','','/Application',1,'','',''),('070307',1202052677035361290,'GetAppSecret','获取AppSecret','','',1,'','',''),('070309',1202052677035361290,'SearchApp','搜索APP','','',1,'','',''),('0704',1202052677035361290,'manager','负责人管理','','',2,'','',''),('070402',1202052677035361290,'managerManage','负责人权限管理','','/manager',2,'','',''),('07040201',1202052677035361290,'SetManagerScope','设置管理范围','','',1,'','',''),('07040202',1202052677035361290,'GetManagedScopeIds','获取可管理资源范围ID','','',1,'','',''),('07040203',1202052677035361290,'ChangeUserResource','修改用户资源','','',1,'','',''),('07040204',1202052677035361290,'GetManagedResourceAndApp','获取管理的APP和资源','','',1,'','',''),('070403',1202052677035361290,'userAuth','人员授权','','/manager/userAuthorization',2,'','',''),('0705',1202052677035361290,'login','登录','','',1,'','',''),('070501',1202052677035361290,'GetMenu','用户菜单','','',1,'','',''),('0706',1202052677035361290,'AppLogin','应用登录','','',1,'','',''),('070601',1202052677035361290,'AppGetUserInfo','应用获取用户信心','','',1,'','',''),('070602',1202052677035361290,'AppGetUserResources','应用获取用户资源','','',1,'','',''),('0801',1225246028714738698,'accountManager','账号管理','','app',2,'solution','',''),('080101',1225246028714738698,'wechat-account','微信账号','','basic',2,'','',''),('080102',1225246028714738698,'account-authorizat','账号授权','','authorizat',2,'','',''),('0802',1225246028714738698,'expand','推广资源管理','','service/expand',2,'notification','',''),('080201',1225246028714738698,'qrcode','二维码管理','','qrcode',2,'','',''),('0803',1225246028714738698,'menu','菜单管理','','service/menu',2,'menu-fold','',''),('080301',1225246028714738698,'wechat','微信菜单管理','','wechat',2,'','',''),('0804',1225246028714738698,'admin_apply','管理员申请','','apply',6,'form','','');
/*!40000 ALTER TABLE `app_resource` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `application`
--

DROP TABLE IF EXISTS `application`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `application` (
  `Id` bigint(20) NOT NULL,
  `AppName` varchar(45) NOT NULL,
  `Enable` bit(1) NOT NULL DEFAULT b'1',
  `CreateTime` datetime NOT NULL,
  `Descript` varchar(200) DEFAULT NULL,
  `CallbackUrl` varchar(200) DEFAULT NULL,
  `CallbackType` smallint(6) NOT NULL DEFAULT '1',
  `AppUserId` bigint(20) NOT NULL,
  `AppCode` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `AppName_UNIQUE` (`AppName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `application`
--

LOCK TABLES `application` WRITE;
/*!40000 ALTER TABLE `application` DISABLE KEYS */;
INSERT INTO `application` VALUES (1202052677035361290,'easyRBAC','','2017-09-26 13:56:38','easyRBAC','http://localhost:8813/easyRBAC/login',1,1235277682896274442,'easyRBAC');
/*!40000 ALTER TABLE `application` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `id_generate`
--

DROP TABLE IF EXISTS `id_generate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `id_generate` (
  `parentId` varchar(45) NOT NULL,
  `subId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`parentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `id_generate`
--

LOCK TABLES `id_generate` WRITE;
/*!40000 ALTER TABLE `id_generate` DISABLE KEYS */;
INSERT INTO `id_generate` VALUES ('0',8),('07',6),('0701',2),('070101',5),('070102',2),('0702',3),('070201',5),('070202',1),('070203',2),('0703',9),('070302',8),('0704',3),('070402',4),('0705',1),('0706',2);
/*!40000 ALTER TABLE `id_generate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login_token`
--

DROP TABLE IF EXISTS `login_token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `login_token` (
  `token` varchar(100) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `createOn` datetime NOT NULL,
  `expireIn` int(11) NOT NULL,
  `appCode` varchar(45) NOT NULL,
  PRIMARY KEY (`token`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login_token`
--

LOCK TABLES `login_token` WRITE;
/*!40000 ALTER TABLE `login_token` DISABLE KEYS */;
INSERT INTO `login_token` VALUES ('User-1202054749357081610-0411222233-cd0d155271c64d64adb6e5b4329dd429',1202054749357081610,'2018-04-11 22:22:33',3600,'easyRBAC');
/*!40000 ALTER TABLE `login_token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` bigint(20) NOT NULL,
  `roleName` varchar(45) NOT NULL,
  `enable` bit(1) NOT NULL,
  `createTime` datetime NOT NULL,
  `descript` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1202058400079283210,'administrator','','2017-09-26 14:41:02','管理员'),(1227849655069443082,'wxadmin','','2018-02-12 14:47:32','微信管理员');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_resource_rel`
--

DROP TABLE IF EXISTS `role_resource_rel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_resource_rel` (
  `id` bigint(20) NOT NULL,
  `roleId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `role_resouce_IX` (`roleId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_resource_rel`
--

LOCK TABLES `role_resource_rel` WRITE;
/*!40000 ALTER TABLE `role_resource_rel` DISABLE KEYS */;
INSERT INTO `role_resource_rel` VALUES (1202058477388694538,1202058400079283210,'07'),(1202058479536185354,1202058400079283210,'0701'),(1202058479536186378,1202058400079283210,'070101'),(1206316832236008458,1202058400079283210,'07010101'),(1206316832236007434,1202058400079283210,'07010102'),(1206316832236009482,1202058400079283210,'07010103'),(1206316832236010506,1202058400079283210,'07010104'),(1206316832236011530,1202058400079283210,'07010105'),(1202058479536187402,1202058400079283210,'070102'),(1206316832236012554,1202058400079283210,'07010201'),(1206316832236013578,1202058400079283210,'07010202'),(1202058479536181258,1202058400079283210,'0702'),(1202058479536182282,1202058400079283210,'070201'),(1206316832235999242,1202058400079283210,'07020101'),(1206316832236000266,1202058400079283210,'07020102'),(1206316832236001290,1202058400079283210,'07020103'),(1206316832236002314,1202058400079283210,'07020104'),(1206316832236003338,1202058400079283210,'07020105'),(1202058479536183306,1202058400079283210,'070202'),(1206316832236004362,1202058400079283210,'07020201'),(1202058479536184330,1202058400079283210,'070203'),(1206316832236005386,1202058400079283210,'07020301'),(1206316832236006410,1202058400079283210,'07020302'),(1202058477388695562,1202058400079283210,'0703'),(1202058477388696586,1202058400079283210,'070301'),(1202058477388697610,1202058400079283210,'070302'),(1206316832235980810,1202058400079283210,'07030201'),(1206316832235982858,1202058400079283210,'07030202'),(1206316832235983882,1202058400079283210,'07030203'),(1206316832235985930,1202058400079283210,'07030204'),(1206316832235984906,1202058400079283210,'07030205'),(1206316832235986954,1202058400079283210,'07030206'),(1206316832235987978,1202058400079283210,'07030207'),(1206316832235981834,1202058400079283210,'07030208'),(1206316832235991050,1202058400079283210,'070303'),(1206316832235979786,1202058400079283210,'070304'),(1206316832235994122,1202058400079283210,'070305'),(1206316832235990026,1202058400079283210,'070306'),(1206316832235992074,1202058400079283210,'070307'),(1206316832235989002,1202058400079283210,'070308'),(1206316832235993098,1202058400079283210,'070309'),(1202058479536178186,1202058400079283210,'0704'),(1202058479536179210,1202058400079283210,'070402'),(1206316832235998218,1202058400079283210,'07040201'),(1206316832235997194,1202058400079283210,'07040202'),(1206316832235995146,1202058400079283210,'07040203'),(1206316832235996170,1202058400079283210,'07040204'),(1202058479536180234,1202058400079283210,'070403'),(1205222483158893578,1202058400079283210,'070501');
/*!40000 ALTER TABLE `role_resource_rel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_user_rel`
--

DROP TABLE IF EXISTS `role_user_rel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_user_rel` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `roleId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `userId_ix` (`userId`),
  KEY `roleId_ix` (`roleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_user_rel`
--

LOCK TABLES `role_user_rel` WRITE;
/*!40000 ALTER TABLE `role_user_rel` DISABLE KEYS */;
INSERT INTO `role_user_rel` VALUES (1202058436586505226,1202054749357081610,1202058400079283210),(1227849693724148746,1225245702297224202,1227849655069443082);
/*!40000 ALTER TABLE `role_user_rel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Id` bigint(20) NOT NULL,
  `UserName` varchar(45) NOT NULL,
  `Password` varchar(512) NOT NULL,
  `Salt` varchar(128) NOT NULL,
  `RealName` varchar(45) DEFAULT NULL,
  `Enable` bit(1) NOT NULL DEFAULT b'1',
  `MobilePhone` varchar(30) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `AccountType` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1202054749357081610,'admin','3DAE8974A1432DECDD04071F084FB55DAF93B99DAF13820074497247C9645C0A','6CA314B9DFAAC9BE29583F835285DEEBFC620EE129DC904FBDE7DE4DAE37BAEC1C5FD019AF731A997891502D73C48610E50269D401D465D2C1DE916813A6BA6E','管理员','','15199999999','2017-09-26 14:12:42',0),(1225245702297224202,'uliian','F8B8EAA47FE341558ED02AFEB528BB552FCCC53048E2A12918621961D50A6370','AED5E6415E64B381E03CB8A6BCF3C429A92132BE26E65EAE1788C10F97A6A44A827DE54CC0171EFEBC36DBF4C3349DCDE2C3D13C5CB3B0BA7F648CDF55115464','陈慎远','','18516560743','2018-01-29 13:58:11',0),(1235277682896274442,'easyRBAC','2C6DD425BB689F0B608C4AD587FE90EEE436B2C0A780E0B92668CFF9DB070B22','4A4B3A88732F81F934BFD6CE60751B896219CF4FE48F3440CDE247AC0F76F6B13218DC8E10BE8EC8EAA6E6F7B0F80C1CF0F406828CBAD97C863C3D91966BC996','easyRBAC','','','2018-03-24 15:36:36',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_manage_resource_scope`
--

DROP TABLE IF EXISTS `user_manage_resource_scope`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_manage_resource_scope` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `user_resource_scop_ix` (`userId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_manage_resource_scope`
--

LOCK TABLES `user_manage_resource_scope` WRITE;
/*!40000 ALTER TABLE `user_manage_resource_scope` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_manage_resource_scope` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_resource_rel`
--

DROP TABLE IF EXISTS `user_resource_rel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_resource_rel` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_user_resource` (`userId`,`resourceId`),
  KEY `ix_user_app` (`userId`,`appId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_resource_rel`
--

LOCK TABLES `user_resource_rel` WRITE;
/*!40000 ALTER TABLE `user_resource_rel` DISABLE KEYS */;
INSERT INTO `user_resource_rel` VALUES (1235883299054814218,1235277813892776970,'0706',1202052677035361290),(1235883299054815242,1235277813892776970,'070601',1202052677035361290),(1235883299054816266,1235277813892776970,'070602',1202052677035361290);
/*!40000 ALTER TABLE `user_resource_rel` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-11 22:24:23
