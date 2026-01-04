USE imdb;
-- name: movie_rating_summary
CREATE OR REPLACE VIEW movie_rating_summary AS
SELECT t.title_id,
    t.primary_title,
    t.original_title,
    t.start_year,
    r.average_rating
FROM Titles t
        JOIN Ratings r ON t.title_id = r.title_id;



CREATE OR REPLACE VIEW titles_view AS
SELECT title_id,
       title_type,
       primary_title,
       original_title,
       is_adult,
       start_year,
       runtime_minutes
FROM Titles;
