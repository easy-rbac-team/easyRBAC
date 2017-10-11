CREATE TABLE IF NOT EXISTS `easyrbac`.`app_resource` (
  `id` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  `applicationId` BIGINT(20) NOT NULL,
  `resourceCode` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  `resourceName` VARCHAR(45) CHARACTER SET 'utf8mb4' NOT NULL,
  `enable` BIT(1) NOT NULL,
  `url` VARCHAR(100) CHARACTER SET 'utf8mb4' NULL DEFAULT NULL,
  `resourceType` TINYINT(4) NOT NULL,
  `iconUrl` VARCHAR(45) CHARACTER SET 'utf8mb4' NULL DEFAULT NULL,
  `parameters` VARCHAR(45) CHARACTER SET 'utf8mb4' NULL DEFAULT NULL,
  `describe` VARCHAR(200) CHARACTER SET 'utf8mb4' NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `app_resouce` (`applicationId` ASC, `resourceCode` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin

CREATE TABLE IF NOT EXISTS `easyrbac`.`app_resource_rel` (
  `id` BIGINT(20) NOT NULL,
  `appId` BIGINT(20) NOT NULL,
  `resourceId` VARCHAR(200) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `appId` (`appId` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin

CREATE TABLE IF NOT EXISTS `easyrbac`.`application` (
  `Id` BIGINT(20) NOT NULL,
  `AppName` VARCHAR(45) NOT NULL,
  `AppCode` VARCHAR(45) NOT NULL,
  `Enable` BIT(1) NOT NULL DEFAULT b'1',
  `CreateTime` DATETIME NOT NULL,
  `Descript` VARCHAR(200) NULL DEFAULT NULL,
  `CallbackUrl` VARCHAR(200) NULL DEFAULT NULL,
  `AppScret` VARCHAR(100) NULL DEFAULT NULL,
  `CallbackType` SMALLINT(6) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `AppCode_UNIQUE` (`AppCode` ASC),
  UNIQUE INDEX `AppName_UNIQUE` (`AppName` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`id_generate` (
  `parentId` VARCHAR(45) NOT NULL,
  `subId` INT(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`parentId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`login_token` (
  `token` VARCHAR(100) NOT NULL,
  `userId` BIGINT(20) NOT NULL,
  `createOn` DATETIME NOT NULL,
  `expireIn` INT(11) NOT NULL,
  PRIMARY KEY (`token`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`role` (
  `id` BIGINT(20) NOT NULL,
  `roleName` VARCHAR(45) NOT NULL,
  `enable` BIT(1) NOT NULL,
  `createTime` DATETIME NOT NULL,
  `descript` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`role_resource_rel` (
  `id` BIGINT(20) NOT NULL,
  `roleId` BIGINT(20) NOT NULL,
  `resourceId` VARCHAR(200) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `role_resouce_IX` (`roleId` ASC, `resourceId` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`role_user_rel` (
  `id` BIGINT(20) NOT NULL,
  `userId` BIGINT(20) NOT NULL,
  `roleId` BIGINT(20) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `userId_ix` (`userId` ASC),
  INDEX `roleId_ix` (`roleId` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`user` (
  `Id` BIGINT(20) NOT NULL,
  `UserName` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(512) NOT NULL,
  `Salt` VARCHAR(128) NOT NULL,
  `RealName` VARCHAR(45) NULL DEFAULT NULL,
  `Enable` BIT(1) NOT NULL DEFAULT b'1',
  `MobilePhone` VARCHAR(30) NULL DEFAULT NULL,
  `CreateTime` DATETIME NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserName_UNIQUE` (`UserName` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`user_manage_resource_scope` (
  `id` BIGINT(20) NOT NULL,
  `userId` BIGINT(20) NOT NULL,
  `resourceId` VARCHAR(200) NOT NULL,
  `appId` BIGINT(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `user_resource_scop_ix` (`userId` ASC, `resourceId` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

CREATE TABLE IF NOT EXISTS `easyrbac`.`user_resource_rel` (
  `id` BIGINT(20) NOT NULL,
  `userId` BIGINT(20) NOT NULL,
  `resourceId` VARCHAR(200) NOT NULL,
  `appId` BIGINT(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `ix_user_resource` (`userId` ASC, `resourceId` ASC),
  INDEX `ix_user_app` (`userId` ASC, `appId` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4