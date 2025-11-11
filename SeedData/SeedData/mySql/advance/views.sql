-- name: movie_rating_summary
DROP VIEW IF EXISTS movie_rating_summary;
CREATE VIEW movie_rating_summary AS
SELECT t.title_id,
       t.primary_title,
       t.original_title,
       t.start_year,
       average_rating
FROM Titles t
         JOIN Ratings r ON t.title_id = r.title_id
GROUP BY t.title_id, t.primary_title, t.original_title, t.start_year, average_rating;


DROP VIEW IF EXISTS titles_view;
CREATE VIEW titles_view AS
SELECT *
FROM (SELECT title_id,
             title_type,
             primary_title,
             original_title,
             is_adult,
             start_year,
             runtime_minutes
      FROM Titles
      GROUP BY title_id, primary_title, original_title, start_year) T;


