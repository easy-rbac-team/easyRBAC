CREATE SCHEMA IF NOT EXISTS `easyrbac` DEFAULT CHARACTER SET utf8mb4 ;

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
DEFAULT CHARACTER SET = utf8mb4;

CREATE TABLE IF NOT EXISTS `easyrbac`.`application` (
  `Id` BIGINT NOT NULL,
  `AppName` VARCHAR(45) NOT NULL,
  `AppCode` VARCHAR(45) NOT NULL,
  `Enable` BIT NOT NULL DEFAULT 1,
  `CreateTime` DATETIME NOT NULL,
  `Descript` VARCHAR(200) NULL,
  `CallbackUrl` VARCHAR(200) NULL,
  `AppScret` VARCHAR(100) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `AppCode_UNIQUE` (`AppCode` ASC),
  UNIQUE INDEX `AppName_UNIQUE` (`AppName` ASC))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `easyrbac`.`id_generate` (
  `parentId` VARCHAR(45) NOT NULL,
  `subId` INT(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`parentId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;

CREATE TABLE IF NOT EXISTS `easyrbac`.`app_resouce` (
  `id` VARCHAR(200) NOT NULL,
  `applicationId` BIGINT(20) NOT NULL,
  `resouceCode` VARCHAR(45) NOT NULL,
  `resouceName` VARCHAR(45) NOT NULL,
  `enable` BIT(1) NOT NULL,
  `url` VARCHAR(100) NULL DEFAULT NULL,
  `resouceType` TINYINT(4) NOT NULL,
  `iconUrl` VARCHAR(45) NULL DEFAULT NULL,  
  `parameters` VARCHAR(45) NULL DEFAULT NULL,
  `describe` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `app_resouce` (`applicationId` ASC, `resouceCode` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4;

CREATE TABLE IF NOT EXISTS `easyrbac`.`role` (
  `id` BIGINT(20) NOT NULL,
  `roleName` VARCHAR(45) NOT NULL,
  `enable` BIT(1) NOT NULL,
  `createTime` DATETIME NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4

