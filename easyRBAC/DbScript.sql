-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        8.0.19 - MySQL Community Server - GPL
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  10.3.0.5771
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 easyrbac 的数据库结构
CREATE DATABASE IF NOT EXISTS `easyrbac` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `easyrbac`;

-- 导出  表 easyrbac.application 结构
CREATE TABLE IF NOT EXISTS `application` (
  `Id` bigint NOT NULL,
  `AppName` varchar(45) NOT NULL,
  `Enable` bit(1) NOT NULL DEFAULT b'1',
  `CreateTime` datetime NOT NULL,
  `Descript` varchar(200) DEFAULT NULL,
  `AppUserId` bigint NOT NULL,
  `AppCode` varchar(45) NOT NULL,
  `HomePageUrl` varchar(200) DEFAULT NULL,
  `IconUrl` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `AppName_UNIQUE` (`AppName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.application 的数据：~5 rows (大约)
/*!40000 ALTER TABLE `application` DISABLE KEYS */;
INSERT INTO `application` (`Id`, `AppName`, `Enable`, `CreateTime`, `Descript`, `AppUserId`, `AppCode`, `HomePageUrl`, `IconUrl`) VALUES
	(1202052677035361290, 'easyRBAC', b'1', '2017-09-26 13:56:38', 'easyRBAC', 1235277682896274442, 'easyRBAC', NULL, NULL);
/*!40000 ALTER TABLE `application` ENABLE KEYS */;

-- 导出  表 easyrbac.application_callback_config 结构
CREATE TABLE IF NOT EXISTS `application_callback_config` (
  `id` bigint NOT NULL,
  `appId` bigint NOT NULL,
  `enviroment` varchar(45) NOT NULL,
  `callbackUrl` varchar(200) NOT NULL,
  `callbackType` smallint NOT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `createBy` varchar(45) NOT NULL,
  `createTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_unique_app_callback` (`appId`,`enviroment`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.application_callback_config 的数据：~5 rows (大约)
/*!40000 ALTER TABLE `application_callback_config` DISABLE KEYS */;
INSERT INTO `application_callback_config` (`id`, `appId`, `enviroment`, `callbackUrl`, `callbackType`, `remark`, `createBy`, `createTime`) VALUES
	(1286288852704560138, 1202052677035361290, 'dev', 'http://localhost:8010/easyRBAC/login', 1, '', 'admin', '2018-12-24 13:55:19');
/*!40000 ALTER TABLE `application_callback_config` ENABLE KEYS */;

-- 导出  表 easyrbac.app_resource 结构
CREATE TABLE IF NOT EXISTS `app_resource` (
  `id` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `applicationId` bigint NOT NULL,
  `resourceCode` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `resourceName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `enable` bit(1) NOT NULL,
  `url` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `resourceType` tinyint NOT NULL,
  `iconUrl` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `parameters` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `describe` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `app_resouce` (`applicationId`,`resourceCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- 正在导出表  easyrbac.app_resource 的数据：~62 rows (大约)
/*!40000 ALTER TABLE `app_resource` DISABLE KEYS */;
INSERT INTO `app_resource` (`id`, `applicationId`, `resourceCode`, `resourceName`, `enable`, `url`, `resourceType`, `iconUrl`, `parameters`, `describe`) VALUES
	('07', 1202052677035361290, 'easyRBAC_root', 'easyRBAC', b'1', NULL, 0, NULL, NULL, NULL),
	('0701', 1202052677035361290, 'User', '用户', b'1', '', 2, '', '', ''),
	('070101', 1202052677035361290, 'userManage', '用户管理', b'1', '/admin/user', 2, '', '', ''),
	('07010101', 1202052677035361290, 'CreateUser', '创建用户', b'1', '', 1, '', '', ''),
	('07010102', 1202052677035361290, 'ChangeUserPassword', '修改密码', b'1', '', 1, '', '', ''),
	('07010103', 1202052677035361290, 'DisableUser', '删除用户', b'1', '', 1, '', '', ''),
	('07010104', 1202052677035361290, 'GetUserInfo', '获取用户信息', b'1', '', 1, '', '', ''),
	('07010105', 1202052677035361290, 'SearchUsers', '搜索用户', b'1', '', 1, '', '', ''),
	('070102', 1202052677035361290, 'UserResourceManage', '用户资源管理', b'1', '/admin/user/resource', 2, '', '', ''),
	('07010201', 1202052677035361290, 'ChangeResources', '修改用户资源', b'1', '', 1, '', '', ''),
	('07010202', 1202052677035361290, 'GetUserResourceIds', '获取用户资源ID', b'1', '', 1, '', '', ''),
	('0702', 1202052677035361290, 'Role', '角色', b'1', '', 2, '', '', ''),
	('070201', 1202052677035361290, 'roleManage', '角色管理', b'1', '/admin/role', 2, '', '', ''),
	('07020101', 1202052677035361290, 'AddRole', '添加角色', b'1', '', 1, '', '', ''),
	('07020102', 1202052677035361290, 'DisableRole', '删除角色', b'1', '', 1, '', '', ''),
	('07020103', 1202052677035361290, 'EditRoleInfo', '编辑角色信息', b'1', '', 1, '', '', ''),
	('07020104', 1202052677035361290, 'GetRoleInfo', '获取角色信息', b'1', '', 1, '', '', ''),
	('07020105', 1202052677035361290, 'SearchRoles', '搜索角色', b'1', '', 1, '', '', ''),
	('070202', 1202052677035361290, 'roleResourceManage', '角色资源管理', b'1', '/admin/role/roleResource', 2, '', '', ''),
	('07020201', 1202052677035361290, 'ChangeResouces', '修改角色资源', b'1', '', 1, '', '', ''),
	('070203', 1202052677035361290, 'roleUserManage', '角色用户管理', b'1', '/admin/role/user', 2, '', '', ''),
	('07020301', 1202052677035361290, 'ChangeMember', '修改角色成员', b'1', '', 1, '', '', ''),
	('07020302', 1202052677035361290, 'GetUserIdsInRole', '获取角色内用户ID', b'1', '', 1, '', '', ''),
	('0703', 1202052677035361290, 'app', '应用', b'1', '', 2, '', '', ''),
	('070301', 1202052677035361290, 'appManage', '应用管理', b'1', '/admin/application', 2, '', '', ''),
	('070302', 1202052677035361290, 'appResource', '应用资源管理', b'1', '/admin/app/resource', 2, '', '', ''),
	('07030201', 1202052677035361290, 'AddResource', '添加资源', b'1', '', 1, '', '', ''),
	('07030202', 1202052677035361290, 'DisableResource', '删除资源', b'1', '', 1, '', '', ''),
	('07030203', 1202052677035361290, 'EditResource', '编辑资源', b'1', '', 1, '', '', ''),
	('07030204', 1202052677035361290, 'GetResourceInfo', '获取资源信息', b'1', '', 1, '', '', ''),
	('07030205', 1202052677035361290, 'GetAppResource', '获取应用资源', b'1', '', 1, '', '', ''),
	('07030206', 1202052677035361290, 'GetResourceTree', '获取资源树', b'1', '', 1, '', '', ''),
	('07030207', 1202052677035361290, 'GetRoleResourceIds', '获取角色应用资源', b'1', '', 1, '', '', ''),
	('07030208', 1202052677035361290, 'ChangeRoleResources', '修改角色资源', b'1', '', 1, '', '', ''),
	('070303', 1202052677035361290, 'GetAppInfo', '获取应用信息', b'1', '/admin/Application/Get', 1, '', '', ''),
	('070304', 1202052677035361290, 'AddApp', '新增应用', b'1', '/admin/Application/AddApp', 1, '', '', ''),
	('070305', 1202052677035361290, 'UpdateAppInfo', '更新应用信息', b'1', '', 1, '', '', ''),
	('070306', 1202052677035361290, 'DeleteApp', '删除应用', b'1', '/admin/Application', 1, '', '', ''),
	('070307', 1202052677035361290, 'GetAppSecret', '获取AppSecret', b'1', '', 1, '', '', ''),
	('070308', 1202052677035361290, 'ChangeAppSecret', '修改APP Screte', b'1', '', 1, '', '', ''),
	('070309', 1202052677035361290, 'SearchApp', '搜索APP', b'1', '', 1, '', '', ''),
	('0704', 1202052677035361290, 'manager', '负责人管理', b'1', '', 2, '', '', ''),
	('070402', 1202052677035361290, 'managerManage', '负责人权限管理', b'1', '/admin/manager', 2, '', '', ''),
	('07040201', 1202052677035361290, 'SetManagerScope', '设置管理范围', b'1', '', 1, '', '', ''),
	('07040202', 1202052677035361290, 'GetManagedScopeIds', '获取可管理资源范围ID', b'1', '', 1, '', '', ''),
	('07040203', 1202052677035361290, 'ChangeUserResource', '修改用户资源', b'1', '', 1, '', '', ''),
	('07040204', 1202052677035361290, 'GetManagedResourceAndApp', '获取管理的APP和资源', b'1', '', 1, '', '', ''),
	('070403', 1202052677035361290, 'userAuth', '人员授权', b'1', '/admin/manager/userAuthorization', 2, '', '', ''),
	('0705', 1202052677035361290, 'login', '登录', b'1', '', 1, '', '', ''),
	('070501', 1202052677035361290, 'GetMenu', '用户菜单', b'1', '', 1, '', '', ''),
	('0706', 1202052677035361290, 'AppLogin', '应用登录', b'1', '', 1, '', '', ''),
	('070601', 1202052677035361290, 'AppGetUserInfo', '应用获取用户信心', b'1', '', 1, '', '', ''),
	('070602', 1202052677035361290, 'AppGetUserResources', '应用获取用户资源', b'1', '', 1, '', '', '');
/*!40000 ALTER TABLE `app_resource` ENABLE KEYS */;

-- 导出  表 easyrbac.id_generate 结构
CREATE TABLE IF NOT EXISTS `id_generate` (
  `parentId` varchar(45) NOT NULL,
  `subId` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`parentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.id_generate 的数据：~20 rows (大约)
/*!40000 ALTER TABLE `id_generate` DISABLE KEYS */;
INSERT INTO `id_generate` (`parentId`, `subId`) VALUES
	('0', 8),
	('07', 6),
	('0701', 2),
	('070101', 5),
	('070102', 2),
	('0702', 3),
	('070201', 5),
	('070202', 1),
	('070203', 2),
	('0703', 9),
	('070302', 8),
	('0704', 3),
	('070402', 4),
	('0705', 1),
	('0706', 2);
/*!40000 ALTER TABLE `id_generate` ENABLE KEYS */;

-- 导出  表 easyrbac.login_token 结构
CREATE TABLE IF NOT EXISTS `login_token` (
  `token` varchar(100) NOT NULL,
  `userId` bigint NOT NULL,
  `createOn` datetime NOT NULL,
  `expireIn` int NOT NULL,
  `appCode` varchar(45) NOT NULL,
  PRIMARY KEY (`token`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.login_token 的数据：~3 rows (大约)
/*!40000 ALTER TABLE `login_token` DISABLE KEYS */;
/*!40000 ALTER TABLE `login_token` ENABLE KEYS */;

-- 导出  表 easyrbac.role 结构
CREATE TABLE IF NOT EXISTS `role` (
  `id` bigint NOT NULL,
  `roleName` varchar(45) NOT NULL,
  `enable` bit(1) NOT NULL,
  `createTime` datetime NOT NULL,
  `descript` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.role 的数据：~2 rows (大约)
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` (`id`, `roleName`, `enable`, `createTime`, `descript`) VALUES
	(1202058400079283210, 'administrator', b'1', '2017-09-26 14:41:02', '管理员'),
	(1363174896221291530, 'application', b'1', '2020-02-11 23:09:37', '应用组');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;

-- 导出  表 easyrbac.role_resource_rel 结构
CREATE TABLE IF NOT EXISTS `role_resource_rel` (
  `id` bigint NOT NULL,
  `roleId` bigint NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `role_resouce_IX` (`roleId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.role_resource_rel 的数据：~55 rows (大约)
/*!40000 ALTER TABLE `role_resource_rel` DISABLE KEYS */;
INSERT INTO `role_resource_rel` (`id`, `roleId`, `resourceId`) VALUES
	(1202058477388694538, 1202058400079283210, '07'),
	(1202058479536185354, 1202058400079283210, '0701'),
	(1202058479536186378, 1202058400079283210, '070101'),
	(1206316832236008458, 1202058400079283210, '07010101'),
	(1206316832236007434, 1202058400079283210, '07010102'),
	(1206316832236009482, 1202058400079283210, '07010103'),
	(1206316832236010506, 1202058400079283210, '07010104'),
	(1206316832236011530, 1202058400079283210, '07010105'),
	(1202058479536187402, 1202058400079283210, '070102'),
	(1206316832236012554, 1202058400079283210, '07010201'),
	(1206316832236013578, 1202058400079283210, '07010202'),
	(1202058479536181258, 1202058400079283210, '0702'),
	(1202058479536182282, 1202058400079283210, '070201'),
	(1206316832235999242, 1202058400079283210, '07020101'),
	(1206316832236000266, 1202058400079283210, '07020102'),
	(1206316832236001290, 1202058400079283210, '07020103'),
	(1206316832236002314, 1202058400079283210, '07020104'),
	(1206316832236003338, 1202058400079283210, '07020105'),
	(1202058479536183306, 1202058400079283210, '070202'),
	(1206316832236004362, 1202058400079283210, '07020201'),
	(1202058479536184330, 1202058400079283210, '070203'),
	(1206316832236005386, 1202058400079283210, '07020301'),
	(1206316832236006410, 1202058400079283210, '07020302'),
	(1202058477388695562, 1202058400079283210, '0703'),
	(1202058477388696586, 1202058400079283210, '070301'),
	(1202058477388697610, 1202058400079283210, '070302'),
	(1206316832235980810, 1202058400079283210, '07030201'),
	(1206316832235982858, 1202058400079283210, '07030202'),
	(1206316832235983882, 1202058400079283210, '07030203'),
	(1206316832235985930, 1202058400079283210, '07030204'),
	(1206316832235984906, 1202058400079283210, '07030205'),
	(1206316832235986954, 1202058400079283210, '07030206'),
	(1206316832235987978, 1202058400079283210, '07030207'),
	(1206316832235981834, 1202058400079283210, '07030208'),
	(1206316832235991050, 1202058400079283210, '070303'),
	(1206316832235979786, 1202058400079283210, '070304'),
	(1206316832235994122, 1202058400079283210, '070305'),
	(1206316832235990026, 1202058400079283210, '070306'),
	(1206316832235992074, 1202058400079283210, '070307'),
	(1206316832235989002, 1202058400079283210, '070308'),
	(1206316832235993098, 1202058400079283210, '070309'),
	(1202058479536178186, 1202058400079283210, '0704'),
	(1202058479536179210, 1202058400079283210, '070402'),
	(1206316832235998218, 1202058400079283210, '07040201'),
	(1206316832235997194, 1202058400079283210, '07040202'),
	(1206316832235995146, 1202058400079283210, '07040203'),
	(1206316832235996170, 1202058400079283210, '07040204'),
	(1202058479536180234, 1202058400079283210, '070403'),
	(1205222483158893578, 1202058400079283210, '070501'),
	(1363175826081711114, 1363174896221291530, '0705'),
	(1363175826081712138, 1363174896221291530, '070501'),
	(1363175826081713162, 1363174896221291530, '0706'),
	(1363175826081714186, 1363174896221291530, '070601'),
	(1363175826081715210, 1363174896221291530, '070602');
/*!40000 ALTER TABLE `role_resource_rel` ENABLE KEYS */;

-- 导出  表 easyrbac.role_user_rel 结构
CREATE TABLE IF NOT EXISTS `role_user_rel` (
  `id` bigint NOT NULL,
  `userId` bigint NOT NULL,
  `roleId` bigint NOT NULL,
  PRIMARY KEY (`id`),
  KEY `userId_ix` (`userId`),
  KEY `roleId_ix` (`roleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.role_user_rel 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `role_user_rel` DISABLE KEYS */;
INSERT INTO `role_user_rel` (`id`, `userId`, `roleId`) VALUES
	(1202058436586505226, 1202054749357081610, 1202058400079283210),
	(1363174956350833674, 1235277682896274442, 1363174896221291530);
/*!40000 ALTER TABLE `role_user_rel` ENABLE KEYS */;

-- 导出  表 easyrbac.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `Id` bigint NOT NULL,
  `UserName` varchar(45) NOT NULL,
  `Password` varchar(512) NOT NULL,
  `Salt` varchar(128) NOT NULL,
  `RealName` varchar(45) DEFAULT NULL,
  `Enable` bit(1) NOT NULL DEFAULT b'1',
  `MobilePhone` varchar(30) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `AccountType` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.user 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`Id`, `UserName`, `Password`, `Salt`, `RealName`, `Enable`, `MobilePhone`, `CreateTime`, `AccountType`) VALUES
	(1202054749357081610, 'admin', '3DAE8974A1432DECDD04071F084FB55DAF93B99DAF13820074497247C9645C0A', '6CA314B9DFAAC9BE29583F835285DEEBFC620EE129DC904FBDE7DE4DAE37BAEC1C5FD019AF731A997891502D73C48610E50269D401D465D2C1DE916813A6BA6E', '管理员', b'1', '15199999999', '2017-09-26 14:12:42', 0),
	(1235277682896274442, 'easyRbac', '2C6DD425BB689F0B608C4AD587FE90EEE436B2C0A780E0B92668CFF9DB070B22', '4A4B3A88732F81F934BFD6CE60751B896219CF4FE48F3440CDE247AC0F76F6B13218DC8E10BE8EC8EAA6E6F7B0F80C1CF0F406828CBAD97C863C3D91966BC996', 'easyRBAC', b'1', '', '2020-02-11 23:07:48', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  表 easyrbac.user_manage_resource_scope 结构
CREATE TABLE IF NOT EXISTS `user_manage_resource_scope` (
  `id` bigint NOT NULL,
  `userId` bigint NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `user_resource_scop_ix` (`userId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.user_manage_resource_scope 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `user_manage_resource_scope` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_manage_resource_scope` ENABLE KEYS */;

-- 导出  表 easyrbac.user_resource_rel 结构
CREATE TABLE IF NOT EXISTS `user_resource_rel` (
  `id` bigint NOT NULL,
  `userId` bigint NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_user_resource` (`userId`,`resourceId`),
  KEY `ix_user_app` (`userId`,`appId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 正在导出表  easyrbac.user_resource_rel 的数据：~0 rows (大约)
/*!40000 ALTER TABLE `user_resource_rel` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_resource_rel` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
