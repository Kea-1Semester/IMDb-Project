USE imdb;

-- Create User
CREATE USER 'Oliver'@'%' IDENTIFIED BY 'Oliver123!';

-- Grant SELECT on views
GRANT SELECT ON imdb.titles_view TO 'Oliver'@'%';
GRANT SELECT ON imdb.movie_rating_summary TO 'Oliver'@'%';


-- Grant EXECUTE
GRANT EXECUTE ON imdb.* TO 'Oliver'@'%';


-- Revoke ALTER ROUTINE, so the user cannot modify stored routines
REVOKE ALTER ROUTINE ON imdb.* FROM 'Oliver'@'%';


FLUSH PRIVILEGES;

SHOW GRANTS FOR 'Oliver'@'%';
