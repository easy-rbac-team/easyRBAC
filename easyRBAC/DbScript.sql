-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.7.26-log - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  10.2.0.5683
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 导出 easyrbac 的数据库结构
CREATE DATABASE IF NOT EXISTS `easyrbac` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `easyrbac`;

-- 导出  表 easyrbac.application 结构
CREATE TABLE IF NOT EXISTS `application` (
  `Id` bigint(20) NOT NULL,
  `AppName` varchar(45) NOT NULL,
  `Enable` bit(1) NOT NULL DEFAULT b'1',
  `CreateTime` datetime NOT NULL,
  `Descript` varchar(200) DEFAULT NULL,
  `AppUserId` bigint(20) NOT NULL,
  `AppCode` varchar(45) NOT NULL,
  `HomePageUrl` varchar(200) DEFAULT NULL,
  `IconUrl` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `AppName_UNIQUE` (`AppName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.application 的数据：~4 rows (大约)
DELETE FROM `application`;
/*!40000 ALTER TABLE `application` DISABLE KEYS */;
INSERT INTO `application` (`Id`, `AppName`, `Enable`, `CreateTime`, `Descript`, `AppUserId`, `AppCode`, `HomePageUrl`, `IconUrl`) VALUES
	(1202052677035361290, 'easyRBAC', b'1', '2017-09-26 13:56:38', 'easyRBAC', 1235277682896274442, 'easyRBAC', NULL, NULL),
	(1225246028714738698, '微信管理平台', b'1', '2018-01-29 14:00:43', '微信管理平台', 1235277813892776970, 'wechat-console', NULL, NULL),
	(1286177151342610442, 'test1', b'1', '2018-12-23 23:28:28', 'fdsafds', 1286177151342609418, 'test11', NULL, NULL),
	(1286178008188585994, 'test2', b'1', '2018-12-23 23:35:08', 'fdasfdas', 1286178008188584970, 'https://test2.ccc/fdsfds', NULL, NULL);
/*!40000 ALTER TABLE `application` ENABLE KEYS */;

-- 导出  表 easyrbac.application_callback_config 结构
CREATE TABLE IF NOT EXISTS `application_callback_config` (
  `id` bigint(20) NOT NULL,
  `appId` bigint(20) NOT NULL,
  `enviroment` varchar(45) NOT NULL,
  `callbackUrl` varchar(200) NOT NULL,
  `callbackType` smallint(6) NOT NULL,
  `remark` varchar(200) DEFAULT NULL,
  `createBy` varchar(45) NOT NULL,
  `createTime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_unique_app_callback` (`appId`,`enviroment`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.application_callback_config 的数据：~3 rows (大约)
DELETE FROM `application_callback_config`;
/*!40000 ALTER TABLE `application_callback_config` DISABLE KEYS */;
INSERT INTO `application_callback_config` (`id`, `appId`, `enviroment`, `callbackUrl`, `callbackType`, `remark`, `createBy`, `createTime`) VALUES
	(1, 1202052677035361290, 'prod', 'http://localhost:8010/easyRBAC/login', 1, '', 'admin', '2018-12-19 23:19:35'),
	(1286178008188587018, 1286178008188585994, 'dev', 'fdasfdsa', 4, 'dsafdsa', '管理员', '2018-12-23 23:35:08'),
	(1286178008188588042, 1286178008188585994, 'prod', 'fdsafdsa', 4, '', '管理员', '2018-12-23 23:35:08'),
	(1286288852704560138, 1202052677035361290, 'dev', 'http://localhost:8010/easyRBAC/login', 1, '', 'admin', '2018-12-24 13:55:19');
/*!40000 ALTER TABLE `application_callback_config` ENABLE KEYS */;

-- 导出  表 easyrbac.app_resource 结构
CREATE TABLE IF NOT EXISTS `app_resource` (
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

-- 正在导出表  easyrbac.app_resource 的数据：~62 rows (大约)
DELETE FROM `app_resource`;
/*!40000 ALTER TABLE `app_resource` DISABLE KEYS */;
INSERT INTO `app_resource` (`id`, `applicationId`, `resourceCode`, `resourceName`, `enable`, `url`, `resourceType`, `iconUrl`, `parameters`, `describe`) VALUES
	('07', 1202052677035361290, 'easyRBAC_root', 'easyRBAC', b'1', NULL, 0, NULL, NULL, NULL),
	('0701', 1202052677035361290, 'User', '用户', b'1', '', 2, '', '', ''),
	('070101', 1202052677035361290, 'userManage', '用户管理', b'1', '/user', 2, '', '', ''),
	('07010101', 1202052677035361290, 'CreateUser', '创建用户', b'1', '', 1, '', '', ''),
	('07010102', 1202052677035361290, 'ChangeUserPassword', '修改密码', b'1', '', 1, '', '', ''),
	('07010103', 1202052677035361290, 'DisableUser', '删除用户', b'1', '', 1, '', '', ''),
	('07010104', 1202052677035361290, 'GetUserInfo', '获取用户信息', b'1', '', 1, '', '', ''),
	('07010105', 1202052677035361290, 'SearchUsers', '搜索用户', b'1', '', 1, '', '', ''),
	('070102', 1202052677035361290, 'UserResourceManage', '用户资源管理', b'1', '/user/resource', 2, '', '', ''),
	('07010201', 1202052677035361290, 'ChangeResources', '修改用户资源', b'1', '', 1, '', '', ''),
	('07010202', 1202052677035361290, 'GetUserResourceIds', '获取用户资源ID', b'1', '', 1, '', '', ''),
	('0702', 1202052677035361290, 'Role', '角色', b'1', '', 2, '', '', ''),
	('070201', 1202052677035361290, 'roleManage', '角色管理', b'1', '/role', 2, '', '', ''),
	('07020101', 1202052677035361290, 'AddRole', '添加角色', b'1', '', 1, '', '', ''),
	('07020102', 1202052677035361290, 'DisableRole', '删除角色', b'1', '', 1, '', '', ''),
	('07020103', 1202052677035361290, 'EditRoleInfo', '编辑角色信息', b'1', '', 1, '', '', ''),
	('07020104', 1202052677035361290, 'GetRoleInfo', '获取角色信息', b'1', '', 1, '', '', ''),
	('07020105', 1202052677035361290, 'SearchRoles', '搜索角色', b'1', '', 1, '', '', ''),
	('070202', 1202052677035361290, 'roleResourceManage', '角色资源管理', b'1', '/role/roleResource', 2, '', '', ''),
	('07020201', 1202052677035361290, 'ChangeResouces', '修改角色资源', b'1', '', 1, '', '', ''),
	('070203', 1202052677035361290, 'roleUserManage', '角色用户管理', b'1', '/role/user', 2, '', '', ''),
	('07020301', 1202052677035361290, 'ChangeMember', '修改角色成员', b'1', '', 1, '', '', ''),
	('07020302', 1202052677035361290, 'GetUserIdsInRole', '获取角色内用户ID', b'1', '', 1, '', '', ''),
	('0703', 1202052677035361290, 'app', '应用', b'1', '', 2, '', '', ''),
	('070301', 1202052677035361290, 'appManage', '应用管理', b'1', '/application', 2, '', '', ''),
	('070302', 1202052677035361290, 'appResource', '应用资源管理', b'1', '/app/resource', 2, '', '', ''),
	('07030201', 1202052677035361290, 'AddResource', '添加资源', b'1', '', 1, '', '', ''),
	('07030202', 1202052677035361290, 'DisableResource', '删除资源', b'1', '', 1, '', '', ''),
	('07030203', 1202052677035361290, 'EditResource', '编辑资源', b'1', '', 1, '', '', ''),
	('07030204', 1202052677035361290, 'GetResourceInfo', '获取资源信息', b'1', '', 1, '', '', ''),
	('07030205', 1202052677035361290, 'GetAppResource', '获取应用资源', b'1', '', 1, '', '', ''),
	('07030206', 1202052677035361290, 'GetResourceTree', '获取资源树', b'1', '', 1, '', '', ''),
	('07030207', 1202052677035361290, 'GetRoleResourceIds', '获取角色应用资源', b'1', '', 1, '', '', ''),
	('07030208', 1202052677035361290, 'ChangeRoleResources', '修改角色资源', b'1', '', 1, '', '', ''),
	('070303', 1202052677035361290, 'GetAppInfo', '获取应用信息', b'1', '/Application/Get', 1, '', '', ''),
	('070304', 1202052677035361290, 'AddApp', '新增应用', b'1', '/Application/AddApp', 1, '', '', ''),
	('070305', 1202052677035361290, 'UpdateAppInfo', '更新应用信息', b'1', '', 1, '', '', ''),
	('070306', 1202052677035361290, 'DeleteApp', '删除应用', b'1', '/Application', 1, '', '', ''),
	('070307', 1202052677035361290, 'GetAppSecret', '获取AppSecret', b'1', '', 1, '', '', ''),
	('070308', 1202052677035361290, 'ChangeAppSecret', '修改APP Screte', b'1', '', 1, '', '', ''),
	('070309', 1202052677035361290, 'SearchApp', '搜索APP', b'1', '', 1, '', '', ''),
	('0704', 1202052677035361290, 'manager', '负责人管理', b'1', '', 2, '', '', ''),
	('070402', 1202052677035361290, 'managerManage', '负责人权限管理', b'1', '/manager', 2, '', '', ''),
	('07040201', 1202052677035361290, 'SetManagerScope', '设置管理范围', b'1', '', 1, '', '', ''),
	('07040202', 1202052677035361290, 'GetManagedScopeIds', '获取可管理资源范围ID', b'1', '', 1, '', '', ''),
	('07040203', 1202052677035361290, 'ChangeUserResource', '修改用户资源', b'1', '', 1, '', '', ''),
	('07040204', 1202052677035361290, 'GetManagedResourceAndApp', '获取管理的APP和资源', b'1', '', 1, '', '', ''),
	('070403', 1202052677035361290, 'userAuth', '人员授权', b'1', '/manager/userAuthorization', 2, '', '', ''),
	('0705', 1202052677035361290, 'login', '登录', b'1', '', 1, '', '', ''),
	('070501', 1202052677035361290, 'GetMenu', '用户菜单', b'1', '', 1, '', '', ''),
	('0706', 1202052677035361290, 'AppLogin', '应用登录', b'1', '', 1, '', '', ''),
	('070601', 1202052677035361290, 'AppGetUserInfo', '应用获取用户信心', b'1', '', 1, '', '', ''),
	('070602', 1202052677035361290, 'AppGetUserResources', '应用获取用户资源', b'1', '', 1, '', '', ''),
	('08', 1225246028714738698, '微信管理平台_root', '微信管理平台', b'1', NULL, 0, NULL, NULL, NULL),
	('0801', 1225246028714738698, 'accountManager', '账号管理', b'1', 'app', 2, 'solution', '', ''),
	('080101', 1225246028714738698, 'wechat-account', '微信账号', b'1', 'basic', 2, '', '', ''),
	('080102', 1225246028714738698, 'account-authorizat', '账号授权', b'1', 'authorizat', 2, '', '', ''),
	('0802', 1225246028714738698, 'expand', '推广资源管理', b'1', 'service/expand', 2, 'notification', '', ''),
	('080201', 1225246028714738698, 'qrcode', '二维码管理', b'1', 'qrcode', 2, '', '', ''),
	('0803', 1225246028714738698, 'menu', '菜单管理', b'1', 'service/menu', 2, 'menu-fold', '', ''),
	('080301', 1225246028714738698, 'wechat', '微信菜单管理', b'1', 'wechat', 2, '', '', ''),
	('0804', 1225246028714738698, 'admin_apply', '管理员申请', b'1', 'apply', 6, 'form', '', '');
/*!40000 ALTER TABLE `app_resource` ENABLE KEYS */;

-- 导出  表 easyrbac.id_generate 结构
CREATE TABLE IF NOT EXISTS `id_generate` (
  `parentId` varchar(45) NOT NULL,
  `subId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`parentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.id_generate 的数据：~19 rows (大约)
DELETE FROM `id_generate`;
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
	('0706', 2),
	('08', 4),
	('0801', 2),
	('0802', 1),
	('0803', 1),
	('0b', 1);
/*!40000 ALTER TABLE `id_generate` ENABLE KEYS */;

-- 导出  表 easyrbac.login_token 结构
CREATE TABLE IF NOT EXISTS `login_token` (
  `token` varchar(100) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `createOn` datetime NOT NULL,
  `expireIn` int(11) NOT NULL,
  `appCode` varchar(45) NOT NULL,
  PRIMARY KEY (`token`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.login_token 的数据：~0 rows (大约)
DELETE FROM `login_token`;
/*!40000 ALTER TABLE `login_token` DISABLE KEYS */;
/*!40000 ALTER TABLE `login_token` ENABLE KEYS */;

-- 导出  表 easyrbac.role 结构
CREATE TABLE IF NOT EXISTS `role` (
  `id` bigint(20) NOT NULL,
  `roleName` varchar(45) NOT NULL,
  `enable` bit(1) NOT NULL,
  `createTime` datetime NOT NULL,
  `descript` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.role 的数据：~2 rows (大约)
DELETE FROM `role`;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` (`id`, `roleName`, `enable`, `createTime`, `descript`) VALUES
	(1202058400079283210, 'administrator', b'1', '2017-09-26 14:41:02', '管理员'),
	(1227849655069443082, 'wxadmin', b'1', '2018-02-12 14:47:32', '微信管理员');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;

-- 导出  表 easyrbac.role_resource_rel 结构
CREATE TABLE IF NOT EXISTS `role_resource_rel` (
  `id` bigint(20) NOT NULL,
  `roleId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `role_resouce_IX` (`roleId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.role_resource_rel 的数据：~55 rows (大约)
DELETE FROM `role_resource_rel`;
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
	(1269773603939813386, 1202058400079283210, '0802'),
	(1269773603939812362, 1202058400079283210, '0804'),
	(1227849676544279562, 1227849655069443082, '0802'),
	(1227849676544280586, 1227849655069443082, '080201'),
	(1227849676544281610, 1227849655069443082, '0803'),
	(1227849676544282634, 1227849655069443082, '080301');
/*!40000 ALTER TABLE `role_resource_rel` ENABLE KEYS */;

-- 导出  表 easyrbac.role_user_rel 结构
CREATE TABLE IF NOT EXISTS `role_user_rel` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `roleId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `userId_ix` (`userId`),
  KEY `roleId_ix` (`roleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.role_user_rel 的数据：~2 rows (大约)
DELETE FROM `role_user_rel`;
/*!40000 ALTER TABLE `role_user_rel` DISABLE KEYS */;
INSERT INTO `role_user_rel` (`id`, `userId`, `roleId`) VALUES
	(1202058436586505226, 1202054749357081610, 1202058400079283210),
	(1227849693724148746, 1225245702297224202, 1227849655069443082);
/*!40000 ALTER TABLE `role_user_rel` ENABLE KEYS */;

-- 导出  表 easyrbac.user 结构
CREATE TABLE IF NOT EXISTS `user` (
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

-- 正在导出表  easyrbac.user 的数据：~8 rows (大约)
DELETE FROM `user`;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`Id`, `UserName`, `Password`, `Salt`, `RealName`, `Enable`, `MobilePhone`, `CreateTime`, `AccountType`) VALUES
	(1202054749357081610, 'admin', '3DAE8974A1432DECDD04071F084FB55DAF93B99DAF13820074497247C9645C0A', '6CA314B9DFAAC9BE29583F835285DEEBFC620EE129DC904FBDE7DE4DAE37BAEC1C5FD019AF731A997891502D73C48610E50269D401D465D2C1DE916813A6BA6E', '管理员', b'1', '15199999999', '2017-09-26 14:12:42', 0),
	(1225245648610133002, 'wxadmin', '5E16B3A033F9AB62413D1BADFA0EB1F47AE97FA892A0D1021042E27E7F1750D2', '1E1396C806EF5E4ED9F7912F3B3811DEA8198C44312689C44035EECC8B8DAA02BA1C426E12F13868CB38267E45023D33F9EFD287EED80F6AFEE12A88EFA426FE', '陈慎远', b'0', '18516560743', '2018-01-29 13:57:47', 0),
	(1225245702297224202, 'uliian', 'F8B8EAA47FE341558ED02AFEB528BB552FCCC53048E2A12918621961D50A6370', 'AED5E6415E64B381E03CB8A6BCF3C429A92132BE26E65EAE1788C10F97A6A44A827DE54CC0171EFEBC36DBF4C3349DCDE2C3D13C5CB3B0BA7F648CDF55115464', '陈慎远', b'1', '18516560743', '2018-01-29 13:58:11', 0),
	(1235277682896274442, 'easyRBAC', '2C6DD425BB689F0B608C4AD587FE90EEE436B2C0A780E0B92668CFF9DB070B22', '4A4B3A88732F81F934BFD6CE60751B896219CF4FE48F3440CDE247AC0F76F6B13218DC8E10BE8EC8EAA6E6F7B0F80C1CF0F406828CBAD97C863C3D91966BC996', 'easyRBAC', b'1', '', '2018-03-24 15:36:36', 1),
	(1235277813892776970, 'wechat-console', 'D0211C7BBC78C55738F183AC070051A64672BF56B0D477BC110B5027DEE6B136', '8C8C9964B0280E9F12F88C03F49C349B30C88EA1CB827583E684059012124E300A4EAC4B192EE92B65EE0E5DF1B7F7861B455C5458898A06D9FC511B85D5EDA1', '微信管理平台', b'1', '', '2018-03-24 15:37:38', 1),
	(1238668870961595402, 'test1', '63422A993A1158DD67A2C494D22C21614D574D9AAF7A3ED9EB8592A5A3F54F5D', '19D3CD999705F035F46F46390AD2981576C3951206E5E893E5F01B3BF2724D2C7F40A06298929B160AD1B7B898A930922CB65F2FF90E031C2E03F239E81D186A', 'test1', b'0', NULL, '2018-04-11 22:15:42', 1),
	(1286177151342609418, 'test11', '1D7F5457DB8FF5B37B29210A8424327F850D1C5709CDEE30B907E63A36D7E93A', 'C3F9F3F54405F9F118F47BF289C6981B90AFF45AA850040FBC8AB0ADF47B322E8D9F1085F89016804C4EA7645E6EC8DAF657F3AEB93C6CF79E44F4B1CD6BF87D', 'test1', b'1', NULL, '2018-12-23 23:28:28', 1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 导出  表 easyrbac.user_manage_resource_scope 结构
CREATE TABLE IF NOT EXISTS `user_manage_resource_scope` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `user_resource_scop_ix` (`userId`,`resourceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.user_manage_resource_scope 的数据：~0 rows (大约)
DELETE FROM `user_manage_resource_scope`;
/*!40000 ALTER TABLE `user_manage_resource_scope` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_manage_resource_scope` ENABLE KEYS */;

-- 导出  表 easyrbac.user_resource_rel 结构
CREATE TABLE IF NOT EXISTS `user_resource_rel` (
  `id` bigint(20) NOT NULL,
  `userId` bigint(20) NOT NULL,
  `resourceId` varchar(200) NOT NULL,
  `appId` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ix_user_resource` (`userId`,`resourceId`),
  KEY `ix_user_app` (`userId`,`appId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 正在导出表  easyrbac.user_resource_rel 的数据：~6 rows (大约)
DELETE FROM `user_resource_rel`;
/*!40000 ALTER TABLE `user_resource_rel` DISABLE KEYS */;
INSERT INTO `user_resource_rel` (`id`, `userId`, `resourceId`, `appId`) VALUES
	(1225985499676541962, 1202054749357081610, '0801', 1225246028714738698),
	(1225985499676542986, 1202054749357081610, '080102', 1225246028714738698),
	(1225985499676544010, 1202054749357081610, '080101', 1225246028714738698),
	(1235883299054814218, 1235277813892776970, '0706', 1202052677035361290),
	(1235883299054815242, 1235277813892776970, '070601', 1202052677035361290),
	(1235883299054816266, 1235277813892776970, '070602', 1202052677035361290);
/*!40000 ALTER TABLE `user_resource_rel` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
