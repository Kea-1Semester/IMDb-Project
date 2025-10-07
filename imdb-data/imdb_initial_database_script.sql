-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema imdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema imdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `imdb` DEFAULT CHARACTER SET utf8 ;
USE `imdb` ;

-- -----------------------------------------------------
-- Table `imdb`.`title_basics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_basics` (
  `tconst` VARCHAR(100) NOT NULL,
  `title_type` VARCHAR(100) NOT NULL,
  `primary_title` VARCHAR(255) NOT NULL,
  `original_title` VARCHAR(255) NOT NULL,
  `is_adult` TINYINT NOT NULL,
  `start_year` DATE NOT NULL,
  `end_year` DATE NULL,
  `runtime_minutes` INT NULL,
  PRIMARY KEY (`tconst`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_akas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_akas` (
  `id_akas` INT NOT NULL AUTO_INCREMENT,
  `tconst` VARCHAR(100) NOT NULL,
  `region` VARCHAR(100) NOT NULL,
  `language` VARCHAR(100) NOT NULL,
  `is_original_title` TINYINT NOT NULL,
  PRIMARY KEY (`id_akas`),
  INDEX `fk_title_akas_title_basics_idx` (`tconst` ASC) VISIBLE,
  CONSTRAINT `fk_title_akas_title_basics`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_attributes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_attributes` (
  `id_attribute` INT NOT NULL AUTO_INCREMENT,
  `attribute` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_attribute`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_akas_attributes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_akas_attributes` (
  `id_attribute` INT NOT NULL,
  `id_akas` INT NOT NULL,
  PRIMARY KEY (`id_attribute`, `id_akas`),
  INDEX `fk_title_attributes_has_title_akas_title_akas1_idx` (`id_akas` ASC) VISIBLE,
  INDEX `fk_title_attributes_has_title_akas_title_attributes1_idx` (`id_attribute` ASC) VISIBLE,
  CONSTRAINT `fk_title_attributes_has_title_akas_title_attributes1`
    FOREIGN KEY (`id_attribute`)
    REFERENCES `imdb`.`title_attributes` (`id_attribute`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_attributes_has_title_akas_title_akas1`
    FOREIGN KEY (`id_akas`)
    REFERENCES `imdb`.`title_akas` (`id_akas`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_types` (
  `id_types` INT NOT NULL AUTO_INCREMENT,
  `type` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_types`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_akas_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_akas_types` (
  `id_types` INT NOT NULL,
  `id_akas` INT NOT NULL,
  PRIMARY KEY (`id_types`, `id_akas`),
  INDEX `fk_title_types_has_title_akas_title_akas1_idx` (`id_akas` ASC) VISIBLE,
  INDEX `fk_title_types_has_title_akas_title_types1_idx` (`id_types` ASC) VISIBLE,
  CONSTRAINT `fk_title_types_has_title_akas_title_types1`
    FOREIGN KEY (`id_types`)
    REFERENCES `imdb`.`title_types` (`id_types`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_types_has_title_akas_title_akas1`
    FOREIGN KEY (`id_akas`)
    REFERENCES `imdb`.`title_akas` (`id_akas`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_genres`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_genres` (
  `id_genre` INT NOT NULL AUTO_INCREMENT,
  `genre` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_genre`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_basics_genres`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_basics_genres` (
  `id_genre` INT NOT NULL,
  `tconst` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id_genre`, `tconst`),
  INDEX `fk_title_genres_has_title_basics_title_basics1_idx` (`tconst` ASC) VISIBLE,
  INDEX `fk_title_genres_has_title_basics_title_genres1_idx` (`id_genre` ASC) VISIBLE,
  CONSTRAINT `fk_title_genres_has_title_basics_title_genres1`
    FOREIGN KEY (`id_genre`)
    REFERENCES `imdb`.`title_genres` (`id_genre`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_genres_has_title_basics_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_ratings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_ratings` (
  `id_rating` INT NOT NULL AUTO_INCREMENT,
  `tconst` VARCHAR(100) NOT NULL,
  `average_rating` DOUBLE NOT NULL,
  `num_votes` INT NOT NULL,
  PRIMARY KEY (`id_rating`),
  CONSTRAINT `fk_title_ratings_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_comments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_comments` (
  `id_comment` INT NOT NULL AUTO_INCREMENT,
  `tconst` VARCHAR(100) NOT NULL,
  `comment` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`id_comment`),
  INDEX `fk_title_comments_title_basics1_idx` (`tconst` ASC) VISIBLE,
  CONSTRAINT `fk_title_comments_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_episodes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_episodes` (
  `tconst` VARCHAR(100) NOT NULL,
  `parent_tconst` VARCHAR(100) NOT NULL,
  `season_number` INT NOT NULL,
  `episode_number` INT NOT NULL,
  PRIMARY KEY (`parent_tconst`, `tconst`),
  INDEX `fk_title_episodes_title_basics2_idx` (`parent_tconst` ASC) VISIBLE,
  CONSTRAINT `fk_title_episodes_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_episodes_title_basics2`
    FOREIGN KEY (`parent_tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`name_basics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`name_basics` (
  `nconst` VARCHAR(100) NOT NULL,
  `primary_name` VARCHAR(255) NOT NULL,
  `birth_year` DATE NOT NULL,
  `end_year` DATE NULL,
  PRIMARY KEY (`nconst`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_crew`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_crew` (
  `nconst` VARCHAR(100) NOT NULL,
  `tconst` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`nconst`, `tconst`),
  INDEX `fk_name_basics_has_title_basics_title_basics1_idx` (`tconst` ASC) VISIBLE,
  INDEX `fk_name_basics_has_title_basics_name_basics1_idx` (`nconst` ASC) VISIBLE,
  CONSTRAINT `fk_name_basics_has_title_basics_name_basics1`
    FOREIGN KEY (`nconst`)
    REFERENCES `imdb`.`name_basics` (`nconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_name_basics_has_title_basics_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`title_principals`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`title_principals` (
  `nconst` VARCHAR(100) NOT NULL,
  `tconst` VARCHAR(100) NOT NULL,
  `category` VARCHAR(100) NOT NULL,
  `job` VARCHAR(100) NOT NULL,
  `characters` VARCHAR(100) NULL,
  PRIMARY KEY (`nconst`, `tconst`),
  INDEX `fk_title_principals_title_basics1_idx` (`tconst` ASC) VISIBLE,
  CONSTRAINT `fk_title_principals_name_basics1`
    FOREIGN KEY (`nconst`)
    REFERENCES `imdb`.`name_basics` (`nconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_principals_title_basics1`
    FOREIGN KEY (`tconst`)
    REFERENCES `imdb`.`title_basics` (`tconst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
