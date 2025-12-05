CREATE EVENT IF NOT EXISTS update_end_year_weekly
    ON SCHEDULE EVERY 1 WEEK
    DO
    BEGIN
        UPDATE Titles
        SET end_year = start_year
        WHERE end_year IS NULL;
    END;