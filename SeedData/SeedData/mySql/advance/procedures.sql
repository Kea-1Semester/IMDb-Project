USE imdb;

DROP PROCEDURE IF EXISTS search_movies_by_primaryTitle_using_like_function;
DROP PROCEDURE IF EXISTS get_top_rated_movies;
DROP PROCEDURE IF EXISTS update_primary_title;
DROP PROCEDURE IF EXISTS search_movies_flexible;
DROP PROCEDURE IF EXISTS create_movie_with_genre;
DROP PROCEDURE IF EXISTS search_movies_fulltext_on_primary_title_and_original_title;

ALTER TABLE Titles
    DROP INDEX idx_Titles_primary_title_original_title;

CREATE FULLTEXT INDEX idx_Titles_primary_title_original_title
    ON Titles (primary_title, original_title);

-- Stored Procedure: Search Movies by Primary Title Keyword
CREATE PROCEDURE IF NOT EXISTS search_movies_by_primaryTitle_using_like_function(IN p_primary_title VARCHAR(50))
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


-- Stored Procedure: Search Movies Fulltext on Primary Title and Original Title using pagination
CREATE PROCEDURE IF NOT EXISTS search_movies_fulltext_on_primary_title_and_original_title(
    IN p_keyword VARCHAR(50),
    IN PageNumber INT,
    IN PageSize INT
)
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
    -- using symbol ? to bind the variable and prevent SQL Injection
    SET @sql = '
        SELECT *
        FROM titles_view
        WHERE MATCH(primary_title, original_title) AGAINST(? IN NATURAL LANGUAGE MODE)
        ORDER BY start_year DESC
        LIMIT ? OFFSET ?';
    PREPARE stmt FROM @sql;
    EXECUTE stmt USING @keyword, @Limit, @Offest;
    DEALLOCATE PREPARE stmt; -- clean up the prepared statement

END;



-- Stored Procedure: Get Top Rated Movies (limit N)
CREATE PROCEDURE IF NOT EXISTS get_top_rated_movies(IN p_limit INT)
BEGIN
    SELECT title_id,
           primary_title,
           original_title,
           start_year,
           average_rating
    FROM movie_rating_summary
    WHERE average_rating > 80
    ORDER BY average_rating DESC
    LIMIT p_limit;
END;


-- Stored Procedure: Flexible Search for Movies with Optional Filters
CREATE PROCEDURE IF NOT EXISTS search_movies_flexible(
    IN p_primary_title VARCHAR(50),
    IN p_title_type VARCHAR(50), -- e.g., 'movie', 'tvMovie'
    IN p_is_adult INT -- 0 or 1
)
BEGIN
    SELECT title_id,
           title_type,
           primary_title,
           original_title,
           is_adult,
           start_year,
           runtime_minutes
    FROM titles_view
    WHERE (p_primary_title IS NULL OR primary_title LIKE CONCAT('%', p_primary_title, '%'))
      AND (p_title_type IS NULL OR title_type = p_title_type)
      AND (p_is_adult IS NULL OR is_adult = p_is_adult)
    ORDER BY start_year DESC
    LIMIT 20;
END;

-- Stored Procedure: Update Primary Title
CREATE PROCEDURE IF NOT EXISTS update_primary_title(
    IN p_title_id CHAR(36),
    IN p_new_primary_title VARCHAR(255)
)
BEGIN
    -- Perform the update
    UPDATE Titles
    SET primary_title = p_new_primary_title
    WHERE title_id = p_title_id;
    IF ROW_COUNT() = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Title not found';
    END IF;
END;

-- Create Movie with Genre
CREATE PROCEDURE IF NOT EXISTS create_movie_with_genre(
    IN p_primary_title VARCHAR(255),
    IN p_original_title VARCHAR(255),
    IN p_title_type VARCHAR(50),
    IN p_is_adult INT,
    IN p_start_year INT,
    IN p_end_year INT,
    IN p_runtime_minutes INT,
    IN p_genre_name VARCHAR(100)
)
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






