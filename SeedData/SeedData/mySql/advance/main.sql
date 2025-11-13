CALL get_top_rated_movies(10);

CALL search_movies_by_primaryTitle('Star Wars');

SELECT get_avg_rating('98148c9b-d4f0-4026-9f4b-088a1cba9579');

SELECT *
FROM Ratings
WHERE title_id = '98148c9b-d4f0-4026-9f4b-088a1cba9579';

CALL search_movies_flexible(
        'star wars',
        NULL,
        NULL
     );

SELECT CURRENT_USER();

SELECT CONCAT('Connected: ', USER(), ', Authenticated: ', CURRENT_USER());

SELECT USER();

SELECT SUBSTRING_INDEX(CURRENT_USER(), '@', 1);

SELECT *
FROM titles_view
WHERE title_id = '0000543b-3710-4be0-aa87-baa39c9e2f55';
-- Crime on a Summer Morning
CALL UpdatePrimaryTitle(
        '0000543b-3710-4be0-aa87-baa39c9e2f55',
        'Crime on a Summer Morning');


SELECT *
FROM Loggings;