-- MySQL Script generated by MySQL Workbench
-- Wed Jan 15 14:18:27 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema BDGesFin
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema BDGesFin
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `BDGesFin` DEFAULT CHARACTER SET utf8 ;
USE `BDGesFin` ;

-- -----------------------------------------------------
-- Table `BDGesFin`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BDGesFin`.`Usuario` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(45) NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Senha` VARCHAR(255) NOT NULL,
  `Data_Nascimento` DATE NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BDGesFin`.`Conta`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BDGesFin`.`Conta` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(45) NOT NULL,
  `Saldo` DECIMAL(9,2) NOT NULL,
  `Usuario_Id` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Conta_Usuario_idx` (`Usuario_Id` ASC),
  CONSTRAINT `fk_Conta_Usuario`
    FOREIGN KEY (`Usuario_Id`)
    REFERENCES `BDGesFin`.`Usuario` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BDGesFin`.`PlanoContas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BDGesFin`.`PlanoContas` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Descricao` VARCHAR(45) NOT NULL,
  `Tipo` CHAR(1) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BDGesFin`.`Transacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BDGesFin`.`Transacao` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Data` DATETIME NOT NULL,
  `Tipo` CHAR(1) NOT NULL,
  `Valor` DECIMAL(9,2) NOT NULL,
  `Descricao` VARCHAR(45) NULL,
  `Conta_Id` INT NOT NULL,
  `PlanoContas_Id` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Transacao_Conta1_idx` (`Conta_Id` ASC),
  INDEX `fk_Transacao_PlanoContas1_idx` (`PlanoContas_Id` ASC),
  CONSTRAINT `fk_Transacao_Conta1`
    FOREIGN KEY (`Conta_Id`)
    REFERENCES `BDGesFin`.`Conta` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Transacao_PlanoContas1`
    FOREIGN KEY (`PlanoContas_Id`)
    REFERENCES `BDGesFin`.`PlanoContas` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
