-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema imdb
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `imdb` ;

-- -----------------------------------------------------
-- Schema imdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `imdb` DEFAULT CHARACTER SET utf8 ;
USE `imdb` ;

-- -----------------------------------------------------
-- Table `imdb`.`Titles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Titles` (
  `title_id` VARCHAR(50) NOT NULL,
  `title_type` VARCHAR(100) NOT NULL,
  `primary_title` VARCHAR(255) NOT NULL,
  `original_title` VARCHAR(255) NOT NULL,
  `is_adult` TINYINT(1) NOT NULL DEFAULT (0),
  `start_year` YEAR NOT NULL,
  `end_year` YEAR NULL,
  `runtime_minutes` INT NULL,
  PRIMARY KEY (`title_id`),
  INDEX `title_type_index` (`title_type` ASC) VISIBLE,
  INDEX `primary_title_index` (`primary_title` ASC) INVISIBLE,
  INDEX `original_title_index` (`original_title` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Aliases`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Aliases` (
  `alias_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `title_id` VARCHAR(50) NOT NULL,
  `region` VARCHAR(100) NOT NULL,
  `language` VARCHAR(100) NOT NULL,
  `is_original_title` TINYINT(1) NOT NULL DEFAULT (0),
  `title` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`alias_id`),
  INDEX `fk_title_akas_title_basics_idx` (`title_id` ASC) VISIBLE,
  INDEX `title_index` (`title` ASC) VISIBLE,
  CONSTRAINT `fk_title_akas_title_basics`
    FOREIGN KEY (`title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Attributes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Attributes` (
  `attribute_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `attribute` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`attribute_id`),
  UNIQUE INDEX `attribute_UNIQUE` (`attribute` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Types` (
  `type_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `type` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`type_id`),
  UNIQUE INDEX `type_UNIQUE` (`type` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Genres`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Genres` (
  `genre_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `genre` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`genre_id`),
  UNIQUE INDEX `genre_UNIQUE` (`genre` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Ratings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Ratings` (
  `rating_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `title_id` VARCHAR(50) NOT NULL,
  `average_rating` DOUBLE NOT NULL,
  `num_votes` INT NOT NULL,
  PRIMARY KEY (`rating_id`),
  CONSTRAINT `fk_title_ratings_title_basics1`
    FOREIGN KEY (`title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Comments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Comments` (
  `comment_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `title_id` VARCHAR(50) NOT NULL,
  `comment` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`comment_id`),
  INDEX `fk_title_comments_title_basics1_idx` (`title_id` ASC) VISIBLE,
  CONSTRAINT `fk_title_comments_title_basics1`
    FOREIGN KEY (`title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Episodes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Episodes` (
  `episode_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `title_id_parent` VARCHAR(50) NOT NULL,
  `title_id_child` VARCHAR(50) NOT NULL,
  `season_number` INT NOT NULL,
  `episode_number` INT NOT NULL,
  PRIMARY KEY (`episode_id`),
  INDEX `fk_title_episodes_title_basics2_idx` (`title_id_child` ASC) VISIBLE,
  CONSTRAINT `fk_title_episodes_title_basics1`
    FOREIGN KEY (`title_id_parent`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_title_episodes_title_basics2`
    FOREIGN KEY (`title_id_child`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Persons`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Persons` (
  `person_id` VARCHAR(50) NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `birth_year` YEAR NOT NULL,
  `end_year` YEAR NULL,
  PRIMARY KEY (`person_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Actors`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Actors` (
  `Id` INT NOT NULL,
  `Titles_title_id` VARCHAR(50) NOT NULL,
  `Persons_person_id` VARCHAR(50) NOT NULL,
  `Role` VARCHAR(100) NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Titles_has_Persons_Titles3_idx` (`Titles_title_id` ASC) VISIBLE,
  INDEX `fk_Titles_has_Persons_Persons3_idx` (`Persons_person_id` ASC) VISIBLE,
  CONSTRAINT `fk_Titles_has_Persons_Titles3`
    FOREIGN KEY (`Titles_title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Titles_has_Persons_Persons3`
    FOREIGN KEY (`Persons_person_id`)
    REFERENCES `imdb`.`Persons` (`person_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Aliases_has_Attributes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Aliases_has_Attributes` (
  `Aliases_alias_id` BINARY(16) NOT NULL,
  `Attributes_attribute_id` BINARY(16) NOT NULL,
  PRIMARY KEY (`Aliases_alias_id`, `Attributes_attribute_id`),
  INDEX `fk_Aliases_has_Attributes_Attributes1_idx` (`Attributes_attribute_id` ASC) VISIBLE,
  INDEX `fk_Aliases_has_Attributes_Aliases1_idx` (`Aliases_alias_id` ASC) VISIBLE,
  CONSTRAINT `fk_Aliases_has_Attributes_Aliases1`
    FOREIGN KEY (`Aliases_alias_id`)
    REFERENCES `imdb`.`Aliases` (`alias_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Aliases_has_Attributes_Attributes1`
    FOREIGN KEY (`Attributes_attribute_id`)
    REFERENCES `imdb`.`Attributes` (`attribute_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Aliases_has_Types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Aliases_has_Types` (
  `Aliases_alias_id` BINARY(16) NOT NULL,
  `Types_type_id` BINARY(16) NOT NULL,
  PRIMARY KEY (`Aliases_alias_id`, `Types_type_id`),
  INDEX `fk_Aliases_has_Types_Types1_idx` (`Types_type_id` ASC) VISIBLE,
  INDEX `fk_Aliases_has_Types_Aliases1_idx` (`Aliases_alias_id` ASC) VISIBLE,
  CONSTRAINT `fk_Aliases_has_Types_Aliases1`
    FOREIGN KEY (`Aliases_alias_id`)
    REFERENCES `imdb`.`Aliases` (`alias_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Aliases_has_Types_Types1`
    FOREIGN KEY (`Types_type_id`)
    REFERENCES `imdb`.`Types` (`type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Titles_has_Genres`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Titles_has_Genres` (
  `Titles_title_id` VARCHAR(50) NOT NULL,
  `Genres_genre_id` BINARY(16) NOT NULL,
  PRIMARY KEY (`Titles_title_id`, `Genres_genre_id`),
  INDEX `fk_Titles_has_Genres_Genres1_idx` (`Genres_genre_id` ASC) VISIBLE,
  INDEX `fk_Titles_has_Genres_Titles1_idx` (`Titles_title_id` ASC) VISIBLE,
  CONSTRAINT `fk_Titles_has_Genres_Titles1`
    FOREIGN KEY (`Titles_title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Titles_has_Genres_Genres1`
    FOREIGN KEY (`Genres_genre_id`)
    REFERENCES `imdb`.`Genres` (`genre_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Directors`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Directors` (
  `Titles_title_id` VARCHAR(50) NOT NULL,
  `Persons_person_id` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Titles_title_id`, `Persons_person_id`),
  INDEX `fk_Titles_has_Persons_Persons1_idx` (`Persons_person_id` ASC) VISIBLE,
  INDEX `fk_Titles_has_Persons_Titles1_idx` (`Titles_title_id` ASC) VISIBLE,
  CONSTRAINT `fk_Titles_has_Persons_Titles1`
    FOREIGN KEY (`Titles_title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Titles_has_Persons_Persons1`
    FOREIGN KEY (`Persons_person_id`)
    REFERENCES `imdb`.`Persons` (`person_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Known_for`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Known_for` (
  `Titles_title_id` VARCHAR(50) NOT NULL,
  `Persons_person_id` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Titles_title_id`, `Persons_person_id`),
  INDEX `fk_Titles_has_Persons_Persons2_idx` (`Persons_person_id` ASC) VISIBLE,
  INDEX `fk_Titles_has_Persons_Titles2_idx` (`Titles_title_id` ASC) VISIBLE,
  CONSTRAINT `fk_Titles_has_Persons_Titles2`
    FOREIGN KEY (`Titles_title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Titles_has_Persons_Persons2`
    FOREIGN KEY (`Persons_person_id`)
    REFERENCES `imdb`.`Persons` (`person_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Writers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Writers` (
  `Titles_title_id` VARCHAR(50) NOT NULL,
  `Persons_person_id` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Titles_title_id`, `Persons_person_id`),
  INDEX `fk_Titles_has_Persons_Persons4_idx` (`Persons_person_id` ASC) VISIBLE,
  INDEX `fk_Titles_has_Persons_Titles4_idx` (`Titles_title_id` ASC) VISIBLE,
  CONSTRAINT `fk_Titles_has_Persons_Titles4`
    FOREIGN KEY (`Titles_title_id`)
    REFERENCES `imdb`.`Titles` (`title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Titles_has_Persons_Persons4`
    FOREIGN KEY (`Persons_person_id`)
    REFERENCES `imdb`.`Persons` (`person_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Professions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Professions` (
  `profession_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `person_id` VARCHAR(50) NOT NULL,
  `profession` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`profession_id`),
  INDEX `fk_Professions_Persons1_idx` (`person_id` ASC) VISIBLE,
  UNIQUE INDEX `profession_UNIQUE` (`profession` ASC) VISIBLE,
  CONSTRAINT `fk_Professions_Persons1`
    FOREIGN KEY (`person_id`)
    REFERENCES `imdb`.`Persons` (`person_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `imdb`.`Loggings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `imdb`.`Loggings` (
  `logging_id` BINARY(16) NOT NULL DEFAULT (UUID_TO_BIN(UUID(), 1)),
  `table_name` VARCHAR(100) NOT NULL,
  `command` ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
  `new_value` JSON NULL,
  `old_value` JSON NULL,
  `executed_by` VARCHAR(100) NULL,
  `executed_at` DATETIME(6) NOT NULL DEFAULT (CURRENT_TIMESTAMP(6)),
  PRIMARY KEY (`logging_id`),
  INDEX `table_name_index` (`table_name` ASC) INVISIBLE,
  INDEX `executed_at_index` (`executed_at` ASC) VISIBLE)
ENGINE = InnoDB;

USE `imdb`;

DELIMITER $$
USE `imdb`$$
CREATE DEFINER = CURRENT_USER TRIGGER `imdb`.`Titles_check_end_year` BEFORE INSERT ON `Titles` FOR EACH ROW
BEGIN
	if new.end_year < new.start_year then
		set new.end_year = null;
	elseif new.end_year >= new.start_year then
		set new.end_year = new.end_year;
	end if;
END$$


DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
