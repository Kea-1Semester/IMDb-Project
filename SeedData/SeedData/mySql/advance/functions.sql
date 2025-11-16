#######################FUNCTIONS##########################
############################################################
-- Stored Function: Get Average Rating for a Movie
DROP FUNCTION IF EXISTS get_avg_rating;
CREATE FUNCTION get_avg_rating(p_title_id VARCHAR(36)) RETURNS decimal(5, 2)
DETERMINISTIC
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