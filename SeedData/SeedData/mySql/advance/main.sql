USE imdb;

CALL get_top_rated_movies(10);


#####################Fulltext search for Star Wars######################################
-- Using fx '"Star Wars"' to search for the exact phrase "Star Wars"
-- Using fx 'Star Wars' to search for the word "Star Wars"
CALL search_movies_fulltext_on_primary_title_and_original_title(
        '"Star Wars"',
        1,
        5);

#####################################################################
SELECT get_avg_rating('98148c9b-d4f0-4026-9f4b-088a1cba9579');
######################################################################

###########################
CALL search_movies_flexible(
        'star wars',
        NULL,
        NULL
     );
############################

#####################################################
SELECT *
FROM titles_view
WHERE title_id = '0000543b-3710-4be0-aa87-baa39c9e2f55';

-- Crime on a Summer Morning
CALL update_primary_title(
        '0000543b-3710-4be0-aa87-baa39c9e2f55',
        'Crime on a Summer Morning');

-- check if it worked using the logging table
SELECT *
FROM Loggings;

SELECT *
FROM titles_view
WHERE title_id = '0000543b-3710-4be0-aa87-baa39c9e2f55';
#######################################################


########Create a movie##################
CALL create_movie_with_genre(
        'A New Movie Title',
        'An Original Movie Title',
        'movie',
        0,
        2025,
        0,
        120,
        'Comedy'
     );
########################################
###################Search for the movie we just created####################
CALL search_movies_fulltext_on_primary_title_and_original_title('"A New Movie Title"', 1, 5);

############################################################################

##############Delete the movie we just created######################
SELECT *
FROM Titles
WHERE primary_title = 'A New Movie Title';

DELETE
FROM Titles
WHERE primary_title = 'A New Movie Title';

DELETE
FROM Titles_has_Genres
WHERE Titles_title_id IN (SELECT title_id
                          FROM Titles
                          WHERE primary_title = 'A New Movie Title');

########################################################################