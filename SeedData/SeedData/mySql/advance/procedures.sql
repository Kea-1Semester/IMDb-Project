USE imdb;

-- Stored Procedure: Search Movies by Primary Title Keyword
DROP PROCEDURE IF EXISTS search_movies_by_primaryTitle;
CREATE PROCEDURE search_movies_by_primaryTitle(IN p_primary_title VARCHAR(50))
BEGIN
    SELECT *
    FROM titles_view
    WHERE primary_title LIKE CONCAT('%', p_primary_title, '%')
    ORDER BY start_year DESC
    LIMIT 10;
END;

-- Stored Procedure: Get Top Rated Movies (limit N)
DROP PROCEDURE IF EXISTS get_top_rated_movies;
CREATE PROCEDURE get_top_rated_movies(IN p_limit INT)
BEGIN
    SELECT *
    FROM movie_rating_summary
    WHERE average_rating > 80
    ORDER BY average_rating DESC
    LIMIT p_limit;
END;

-- Stored Procedure: Search Movies Flexible and Saving logs with JSON values within User().
DROP PROCEDURE IF EXISTS search_movies_flexible;
CREATE PROCEDURE search_movies_flexible(
    IN p_primary_title VARCHAR(50),
    IN p_title_type VARCHAR(50), -- e.g., 'movie', 'tvMovie'
    IN p_is_adult INT -- 0 or 1, or NULL for ignoring
)
BEGIN
    SELECT *
    FROM titles_view
    WHERE (p_primary_title IS NULL OR primary_title LIKE CONCAT('%', p_primary_title, '%'))
      AND (p_title_type IS NULL OR title_type = p_title_type)
      AND (p_is_adult IS NULL OR is_adult = p_is_adult)
    ORDER BY start_year DESC
    LIMIT 20;
END;

DROP PROCEDURE IF EXISTS UpdatePrimaryTitle;
CREATE PROCEDURE UpdatePrimaryTitle(
    IN p_title_id CHAR(36),
    IN p_new_primary_title VARCHAR(255)
)
BEGIN
    -- Perform the update
    UPDATE Titles
    SET primary_title = p_new_primary_title
    WHERE title_id = p_title_id;

END;
