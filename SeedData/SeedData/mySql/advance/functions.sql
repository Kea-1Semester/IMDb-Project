USE imdb;

-- Stored Function: Get Average Rating for a Movie
DROP FUNCTION IF EXISTS get_avg_rating;
CREATE
    DEFINER = "avnadmin"@"%" FUNCTION "get_avg_rating"(p_title_id VARCHAR(255)) RETURNS decimal(5, 2)
BEGIN
    DECLARE avg_rating DECIMAL(5, 2);
    SELECT AVG(average_rating)
    INTO avg_rating
    FROM Ratings
    WHERE title_id = p_title_id;
    RETURN IFNULL(avg_rating, 0);
END;


