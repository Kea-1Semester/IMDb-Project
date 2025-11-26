CREATE TABLE IF NOT EXISTS Attributes
(
    attribute_id char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    attribute    varchar(100)                                            NOT NULL,
    CONSTRAINT attribute_UNIQUE
        UNIQUE (attribute)
);

CREATE TABLE IF NOT EXISTS Genres
(
    genre_id char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    genre    varchar(100)                                            NOT NULL,
    CONSTRAINT genre_UNIQUE
        UNIQUE (genre)
);

CREATE TABLE IF NOT EXISTS Loggings
(
    logging_id  char(36) CHARSET ascii DEFAULT (UUID())             NOT NULL
        PRIMARY KEY,
    table_name  char(36) CHARSET ascii                              NOT NULL,
    command     enum ('INSERT', 'UPDATE', 'DELETE')                 NOT NULL,
    new_value   json                                                NULL,
    old_value   json                                                NULL,
    executed_by varchar(100)                                        NULL,
    executed_at datetime(6)            DEFAULT CURRENT_TIMESTAMP(6) NOT NULL
);

CREATE TABLE IF NOT EXISTS Persons
(
    person_id  char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    name       varchar(255)                            NOT NULL,
    birth_year int                                     NOT NULL,
    end_year   int                                     NULL
);

CREATE TABLE IF NOT EXISTS PrivilegeLog
(
    log_id      char(36)  DEFAULT (UUID())          NOT NULL
        PRIMARY KEY,
    action      varchar(50)                         NULL,
    object      varchar(100)                        NULL,
    grantee     varchar(100)                        NULL,
    executed_at timestamp DEFAULT CURRENT_TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Professions
(
    profession_id char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    person_id     char(36) CHARSET ascii                  NOT NULL,
    profession    varchar(45)                             NOT NULL,
    CONSTRAINT profession_UNIQUE
        UNIQUE (profession),
    CONSTRAINT fk_Professions_Persons1
        FOREIGN KEY (person_id) REFERENCES Persons (person_id)
);

CREATE INDEX fk_Professions_Persons1_idx
    ON Professions (person_id);

CREATE TABLE IF NOT EXISTS Titles
(
    title_id        char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    title_type      varchar(100)                            NOT NULL,
    primary_title   varchar(255)                            NOT NULL,
    original_title  varchar(255)                            NOT NULL,
    is_adult        tinyint(1)                              NOT NULL,
    start_year      int                                     NOT NULL,
    end_year        int                                     NULL,
    runtime_minutes int                                     NULL
);

CREATE TABLE IF NOT EXISTS Actors
(
    actor_id          char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL,
    Titles_title_id   char(36) CHARSET ascii                                  NOT NULL,
    Persons_person_id char(36) CHARSET ascii                                  NOT NULL,
    Role              varchar(255)                                            NOT NULL,
    PRIMARY KEY (actor_id, Titles_title_id, Persons_person_id),
    CONSTRAINT fk_Titles_has_Persons_Persons3
        FOREIGN KEY (Persons_person_id) REFERENCES Persons (person_id),
    CONSTRAINT fk_Titles_has_Persons_Titles3
        FOREIGN KEY (Titles_title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_Titles_has_Persons_Persons3_idx
    ON Actors (Persons_person_id);

CREATE INDEX fk_Titles_has_Persons_Titles3_idx
    ON Actors (Titles_title_id);

CREATE TABLE IF NOT EXISTS Aliases
(
    alias_id          char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    title_id          char(36) CHARSET ascii                                  NOT NULL,
    region            varchar(100)                                            NOT NULL,
    language          varchar(100)                                            NOT NULL,
    is_original_title tinyint(1)                                              NOT NULL,
    title             varchar(255)                                            NOT NULL,
    CONSTRAINT fk_title_akas_title_basics
        FOREIGN KEY (title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_title_akas_title_basics_idx
    ON Aliases (title_id);

CREATE INDEX title_index
    ON Aliases (title);

CREATE TABLE IF NOT EXISTS Aliases_has_Attributes
(
    Aliases_alias_id        char(36) CHARSET ascii NOT NULL,
    Attributes_attribute_id char(36) CHARSET ascii NOT NULL,
    PRIMARY KEY (Aliases_alias_id, Attributes_attribute_id),
    CONSTRAINT fk_Aliases_has_Attributes_Aliases1
        FOREIGN KEY (Aliases_alias_id) REFERENCES Aliases (alias_id),
    CONSTRAINT fk_Aliases_has_Attributes_Attributes1
        FOREIGN KEY (Attributes_attribute_id) REFERENCES Attributes (attribute_id)
);

CREATE INDEX fk_Aliases_has_Attributes_Aliases1_idx
    ON Aliases_has_Attributes (Aliases_alias_id);

CREATE INDEX fk_Aliases_has_Attributes_Attributes1_idx
    ON Aliases_has_Attributes (Attributes_attribute_id);

CREATE TABLE IF NOT EXISTS Comments
(
    comment_id char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    title_id   char(36) CHARSET ascii                                  NOT NULL,
    comment    varchar(255)                                            NOT NULL,
    CONSTRAINT fk_title_comments_title_basics1
        FOREIGN KEY (title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_title_comments_title_basics1_idx
    ON Comments (title_id);

CREATE TABLE IF NOT EXISTS Directors
(
    directors_id      char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL,
    Titles_title_id   char(36) CHARSET ascii                                  NOT NULL,
    Persons_person_id char(36) CHARSET ascii                                  NOT NULL,
    PRIMARY KEY (directors_id, Titles_title_id, Persons_person_id),
    CONSTRAINT fk_Titles_has_Persons_Persons1
        FOREIGN KEY (Persons_person_id) REFERENCES Persons (person_id),
    CONSTRAINT fk_Titles_has_Persons_Titles1
        FOREIGN KEY (Titles_title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_Titles_has_Persons_Persons1_idx
    ON Directors (Persons_person_id);

CREATE INDEX fk_Titles_has_Persons_Titles1_idx
    ON Directors (Titles_title_id);

CREATE TABLE IF NOT EXISTS Episodes
(
    episode_id      char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    title_id_parent char(36) CHARSET ascii                                  NOT NULL,
    title_id_child  char(36) CHARSET ascii                                  NOT NULL,
    season_number   int                                                     NOT NULL,
    episode_number  int                                                     NOT NULL,
    CONSTRAINT fk_title_episodes_title_basics1
        FOREIGN KEY (title_id_parent) REFERENCES Titles (title_id),
    CONSTRAINT fk_title_episodes_title_basics2
        FOREIGN KEY (title_id_child) REFERENCES Titles (title_id)
);

CREATE INDEX fk_title_episodes_title_basics2_idx
    ON Episodes (title_id_child);

CREATE TABLE IF NOT EXISTS Known_for
(
    known_for_id      char(36) CHARSET ascii DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL,
    Titles_title_id   char(36) CHARSET ascii                                  NOT NULL,
    Persons_person_id char(36) CHARSET ascii                                  NOT NULL,
    PRIMARY KEY (known_for_id, Titles_title_id, Persons_person_id),
    CONSTRAINT fk_Titles_has_Persons_Persons2
        FOREIGN KEY (Persons_person_id) REFERENCES Persons (person_id),
    CONSTRAINT fk_Titles_has_Persons_Titles2
        FOREIGN KEY (Titles_title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_Titles_has_Persons_Persons2_idx
    ON Known_for (Persons_person_id);

CREATE INDEX fk_Titles_has_Persons_Titles2_idx
    ON Known_for (Titles_title_id);

CREATE TABLE IF NOT EXISTS Ratings
(
    rating_id      char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    title_id       char(36) CHARSET ascii                  NOT NULL,
    average_rating double                                  NOT NULL,
    num_votes      int                                     NOT NULL,
    CONSTRAINT fk_title_ratings_title_basics1
        FOREIGN KEY (title_id) REFERENCES Titles (title_id)
);

CREATE FULLTEXT INDEX idx_Titles_primary_title_original_title
    ON Titles (primary_title, original_title);

CREATE INDEX original_title_index
    ON Titles (original_title);

CREATE INDEX primary_title_index
    ON Titles (primary_title);

CREATE INDEX title_type_index
    ON Titles (title_type);

CREATE TRIGGER trg_after_insert_titles
    AFTER INSERT
    ON Titles
    FOR EACH ROW
BEGIN

    INSERT INTO Loggings (table_name,
                          command,
                          new_value,
                          old_value,
                          executed_by)
    VALUES ('Titles',
            'INSERT',
            JSON_OBJECT(
                    'title_id', NEW.title_id,
                    'title_type', NEW.title_type,
                    'primary_title', NEW.primary_title,
                    'original_title', NEW.original_title,
                    'is_adult', New.is_adult,
                    'start_year', NEW.start_year,
                    'end_year', NEW.end_year,
                    'runtime_minutes', NEW.runtime_minutes
            ),
            NULL,
            SUBSTRING_INDEX(USER(), '@', 1));
END;

CREATE TRIGGER trg_after_update_titles
    AFTER UPDATE
    ON Titles
    FOR EACH ROW
BEGIN
    DECLARE  v_table_name VARCHAR(100) DEFAULT 'Titles';
    DECLARE v_command VARCHAR(10) DEFAULT 'UPDATE';

    IF OLD.primary_title <> NEW.primary_title THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('primary_title', NEW.primary_title),
                JSON_OBJECT('primary_title', OLD.primary_title), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF OLD.start_year <> NEW.start_year THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('start_year', NEW.start_year),
                JSON_OBJECT('start_year', OLD.start_year), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF OLD.end_year <> NEW.end_year THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('end_year', NEW.end_year),
                JSON_OBJECT('end_year', OLD.end_year), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF OLD.runtime_minutes <> NEW.runtime_minutes THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('runtime_minutes', NEW.runtime_minutes),
                JSON_OBJECT('runtime_minutes', OLD.runtime_minutes), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
END;

CREATE TRIGGER trg_before_insert_titles_check_title_type
    BEFORE INSERT
    ON Titles
    FOR EACH ROW
BEGIN
    IF NOT EXISTS (SELECT 1
                   FROM (SELECT title_type, COUNT(title_type) AS type_count
                         FROM Titles
                         GROUP BY title_type) AS subquery
                   WHERE title_type = NEW.title_type) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid title type';
    END IF;
END;

CREATE TRIGGER trg_before_update_titles
    BEFORE UPDATE
    ON Titles
    FOR EACH ROW
BEGIN
    DECLARE sql_state CONDITION FOR SQLSTATE '45000';

    -- primary_title must not be null, empty, or shorter than 3 characters, longer than 150
    IF NEW.primary_title IS NULL
        OR TRIM(NEW.primary_title) = ''
        OR CHAR_LENGTH(TRIM(NEW.primary_title)) < 3
        OR CHAR_LENGTH(TRIM(NEW.primary_title)) > 150 THEN
        SIGNAL sql_state SET MESSAGE_TEXT =
                'primary_title must not be null, empty, shorter than 3, or longer than 150 characters';

    END IF;

    -- start_year must be before end_year
    IF NEW.start_year IS NOT NULL AND NEW.end_year IS NOT NULL AND NEW.start_year > NEW.end_year THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'start_year must be before end_year';
    END IF;

    -- start_year must be after 1900
    IF NEW.start_year < 1900 THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'start_year must be after 1900';
    END IF;

    -- runtime_minutes must be between 0 and 1440 (24 hours)
    IF NEW.runtime_minutes IS NOT NULL AND (NEW.runtime_minutes < 0 OR NEW.runtime_minutes > 1440) THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'runtime_minutes must be between 0 and 1440';
    END IF;

END;

CREATE TABLE IF NOT EXISTS Titles_has_Genres
(
    Titles_title_id char(36) CHARSET ascii NOT NULL,
    Genres_genre_id char(36) CHARSET ascii NOT NULL,
    PRIMARY KEY (Titles_title_id, Genres_genre_id),
    CONSTRAINT fk_Titles_has_Genres_Genres1
        FOREIGN KEY (Genres_genre_id) REFERENCES Genres (genre_id)
);

CREATE INDEX fk_Titles_has_Genres_Genres1_idx
    ON Titles_has_Genres (Genres_genre_id);

CREATE INDEX fk_Titles_has_Genres_Titles1_idx
    ON Titles_has_Genres (Titles_title_id);

CREATE TABLE IF NOT EXISTS Types
(
    type_id char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    type    varchar(100)                            NOT NULL,
    CONSTRAINT type_UNIQUE
        UNIQUE (type)
);

CREATE TABLE IF NOT EXISTS Aliases_has_Types
(
    Aliases_alias_id char(36) CHARSET ascii NOT NULL,
    Types_type_id    char(36) CHARSET ascii NOT NULL,
    PRIMARY KEY (Aliases_alias_id, Types_type_id),
    CONSTRAINT fk_Aliases_has_Types_Aliases1
        FOREIGN KEY (Aliases_alias_id) REFERENCES Aliases (alias_id),
    CONSTRAINT fk_Aliases_has_Types_Types1
        FOREIGN KEY (Types_type_id) REFERENCES Types (type_id)
);

CREATE INDEX fk_Aliases_has_Types_Aliases1_idx
    ON Aliases_has_Types (Aliases_alias_id);

CREATE INDEX fk_Aliases_has_Types_Types1_idx
    ON Aliases_has_Types (Types_type_id);

CREATE TABLE IF NOT EXISTS Writers
(
    writers_id        char(36) CHARSET ascii DEFAULT (UUID()) NOT NULL,
    Titles_title_id   char(36) CHARSET ascii                  NOT NULL,
    Persons_person_id char(36) CHARSET ascii                  NOT NULL,
    PRIMARY KEY (writers_id, Titles_title_id, Persons_person_id),
    CONSTRAINT fk_Titles_has_Persons_Persons4
        FOREIGN KEY (Persons_person_id) REFERENCES Persons (person_id),
    CONSTRAINT fk_Titles_has_Persons_Titles4
        FOREIGN KEY (Titles_title_id) REFERENCES Titles (title_id)
);

CREATE INDEX fk_Titles_has_Persons_Persons4_idx
    ON Writers (Persons_person_id);

CREATE INDEX fk_Titles_has_Persons_Titles4_idx
    ON Writers (Titles_title_id);

CREATE TABLE IF NOT EXISTS __EFMigrationsHistory
(
    MigrationId    varchar(150) NOT NULL
        PRIMARY KEY,
    ProductVersion varchar(32)  NOT NULL
)
    CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS title_types
(
    id   char(36) DEFAULT (UUID()) NOT NULL
        PRIMARY KEY,
    type varchar(50)               NOT NULL,
    CONSTRAINT type
        UNIQUE (type)
);

CREATE OR REPLACE VIEW movie_rating_summary AS
SELECT `t`.`title_id`       AS `title_id`,
       `t`.`primary_title`  AS `primary_title`,
       `t`.`original_title` AS `original_title`,
       `t`.`start_year`     AS `start_year`,
       `r`.`average_rating` AS `average_rating`
FROM (`imdb`.`Titles` `t` JOIN `imdb`.`Ratings` `r` ON ((`t`.`title_id` = `r`.`title_id`)));

GRANT SELECT ON TABLE movie_rating_summary TO avnuser;

CREATE OR REPLACE VIEW titles_view AS
SELECT `imdb`.`Titles`.`title_id`        AS `title_id`,
       `imdb`.`Titles`.`title_type`      AS `title_type`,
       `imdb`.`Titles`.`primary_title`   AS `primary_title`,
       `imdb`.`Titles`.`original_title`  AS `original_title`,
       `imdb`.`Titles`.`is_adult`        AS `is_adult`,
       `imdb`.`Titles`.`start_year`      AS `start_year`,
       `imdb`.`Titles`.`runtime_minutes` AS `runtime_minutes`
FROM `imdb`.`Titles`;

GRANT SELECT ON TABLE titles_view TO avnuser;

CREATE PROCEDURE create_movie_with_genre(IN p_primary_title varchar(255), IN p_original_title varchar(255),
                                         IN p_title_type varchar(50), IN p_is_adult int, IN p_start_year int,
                                         IN p_end_year int, IN p_runtime_minutes int, IN p_genre_name varchar(100))
BEGIN
    DECLARE v_title_id CHAR(36);
    DECLARE v_genre_id CHAR(36);

    DECLARE EXIT HANDLER FOR SQLEXCEPTION -- This is required to roll back the transaction, when error happens in of the steps below.
        BEGIN
            ROLLBACK;
            RESIGNAL; -- to rethrow the exception from the trigger.
        END;

    START TRANSACTION;

    -- Generate UUID for the new title
    SET v_title_id = UUID();

    INSERT INTO imdb.Titles (title_id,
                             primary_title,
                             original_title,
                             title_type,
                             is_adult,
                             start_year,
                             end_year,
                             runtime_minutes)
    VALUES (v_title_id,
            p_primary_title,
            p_original_title,
            p_title_type,
            p_is_adult,
            p_start_year,
            p_end_year,
            p_runtime_minutes);

    -- Get genre_id from Genres table
    SET v_genre_id = (SELECT genre_id FROM imdb.Genres WHERE genre = p_genre_name);
    IF v_genre_id IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Genre not found';
    END IF;

    -- Link title and genre
    INSERT INTO imdb.Titles_has_Genres (titles_title_id, Genres_genre_id)
    VALUES (v_title_id, v_genre_id);
    COMMIT;
END;

CREATE FUNCTION get_avg_rating(p_title_id varchar(36)) RETURNS decimal(5, 2)
BEGIN
    DECLARE avg_rating DECIMAL(5, 2);
    SELECT average_rating
    INTO avg_rating
    FROM Ratings
    WHERE title_id = p_title_id
    LIMIT 1;
    IF avg_rating IS NULL THEN
        RETURN 0;
    END IF;
    RETURN avg_rating;
END;

CREATE PROCEDURE get_top_rated_movies(IN p_limit int)
BEGIN
    SELECT
        title_id,
        primary_title,
        original_title,
        start_year,
        average_rating
    FROM movie_rating_summary
    WHERE average_rating > 80
    ORDER BY average_rating DESC
    LIMIT p_limit;
END;

CREATE PROCEDURE search_movies_by_primaryTitle_using_like_function(IN p_primary_title varchar(50))
BEGIN
    SELECT title_id,
           title_type,
           primary_title,
           original_title,
           is_adult,
           start_year,
           runtime_minutes
    FROM titles_view
    WHERE primary_title LIKE CONCAT(p_primary_title, '%')
    ORDER BY start_year DESC
    LIMIT 10;
END;

CREATE PROCEDURE search_movies_flexible(IN p_primary_title varchar(50), IN p_title_type varchar(50), IN p_is_adult int)
BEGIN
    SELECT *
    FROM titles_view
    WHERE (p_primary_title IS NULL OR primary_title LIKE CONCAT('%', p_primary_title, '%'))
      AND (p_title_type IS NULL OR title_type = p_title_type)
      AND (p_is_adult IS NULL OR is_adult = p_is_adult)
    ORDER BY start_year DESC
    LIMIT 20;
END;

CREATE PROCEDURE search_movies_fulltext_on_primary_title_and_original_title(IN p_keyword varchar(50), IN PageNumber int, IN PageSize int)
BEGIN
    -- Define the maximum allowed page size
    SET @MaxPageSize = 100;

    -- Validate the Pagesize
    IF PageSize IS NULL OR
       PageSize > @MaxPageSize THEN
        SET PageSize = @MaxPageSize;
    ELSEIF PageSize <= 0 THEN
        SET PageSize = 1;
    END IF;

    -- Prepare variables for the query
    SET @keyword = p_keyword;
    SET @Offest = (IFNULL(PageNumber, 1) - 1) * PageSize;
    SET @Limit = PageSize;
    -- Prepare and execute the query with pagination using fulltext Search
    SET @sql = '
        SELECT *
        FROM titles_view
        WHERE MATCH(primary_title, original_title) AGAINST(? IN NATURAL LANGUAGE MODE)
        ORDER BY start_year DESC
        LIMIT ? OFFSET ?';
    PREPARE stmt FROM @sql;
    EXECUTE stmt USING @keyword, @Limit, @Offest;
    DEALLOCATE PREPARE stmt;
END;

CREATE PROCEDURE update_primary_title(IN p_title_id char(36), IN p_new_primary_title varchar(255))
BEGIN
    -- Perform the update
    UPDATE Titles
    SET primary_title = p_new_primary_title
    WHERE title_id = p_title_id;
END;


