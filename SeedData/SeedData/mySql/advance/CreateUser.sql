USE imdb;

-- SET VARIABLE for username
SET @username = 'Oliver'@'%';
-- DECLARE @username = 'Oliver'@'%';

-- Create User
CREATE USER 'Oliver'@'%' IDENTIFIED BY 'Oliver123!';

-- Grant SELECT on views
GRANT SELECT ON imdb.titles_view TO @username;
GRANT SELECT ON imdb.movie_rating_summary TO  @username;


-- Grant EXECUTE
GRANT EXECUTE ON imdb.* TO @username;


-- Revoke ALTER ROUTINE, so the user cannot modify stored routines
REVOKE ALTER ROUTINE ON imdb.* FROM @username;


FLUSH PRIVILEGES;

SHOW GRANTS FOR @username;
