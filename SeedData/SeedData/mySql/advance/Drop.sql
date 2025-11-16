## Functions
DROP FUNCTION IF EXISTS get_avg_rating;
## Procedures
DROP PROCEDURE IF EXISTS search_movies_by_primaryTitle_using_like_function;
DROP PROCEDURE IF EXISTS get_top_rated_movies;
DROP PROCEDURE IF EXISTS update_primary_title;
DROP PROCEDURE IF EXISTS search_movies_flexible;
DROP PROCEDURE IF EXISTS create_movie_with_genre;
DROP PROCEDURE IF EXISTS search_movies_fulltext_on_primary_title_and_original_title;
## Triggers
DROP TRIGGER IF EXISTS trg_after_insert_titles;
DROP TRIGGER IF EXISTS trg_before_insert_titles_check_title_type;
DROP TRIGGER IF EXISTS trg_before_update_titles;
DROP TRIGGER IF EXISTS trg_after_update_titles;
