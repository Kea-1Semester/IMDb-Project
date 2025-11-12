DROP TRIGGER IF EXISTS trg_after_insert_titles;
DROP TRIGGER IF EXISTS trg_before_insert_titles_check_title_type;

-- Trigger to log changes in the Titles table
CREATE TRIGGER trg_after_insert_titles
    AFTER INSERT
    ON Titles
    FOR EACH ROW
BEGIN
    INSERT INTO Loggings (table_name,
                          command,
                          new_value,
                          old_value,
                          executed_by)
    VALUES ('Titles',
            'INSERT',
            JSON_OBJECT(
                    'title_id', NEW.title_id,
                    'title_type', NEW.title_type,
                    'primary_title', NEW.primary_title,
                    'original_title', NEW.original_title,
                    'is_adult', New.is_adult,
                    'start_year', NEW.start_year,
                    'end_year', NEW.end_year,
                    'runtime_minutes', NEW.runtime_minutes
            ),
            NULL,
            SUBSTRING_INDEX(USER(), '@', 1));
END;


-- Trigger to check the validity of the inserted title type
CREATE TRIGGER trg_before_insert_titles_check_title_type
    BEFORE INSERT
    ON Titles
    FOR EACH ROW
BEGIN
    IF NOT EXISTS (SELECT 1 FROM title_types WHERE type = NEW.title_type) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid title type';
    END IF;
END;


-- Adding a new title with an invalid title type
INSERT INTO Titles (title_id, title_type, primary_title, original_title, is_adult, start_year, end_year,
                    runtime_minutes)
VALUES (UUID(),
        'movie',
        'Galadrielaptor',
        'Galadrielaptor',
        0,
        2023,
        NULL,
        120);

-- Adding a new title with a valid title type
INSERT INTO Titles (title_id, title_type, primary_title, original_title, is_adult, start_year, end_year,
                    runtime_minutes)
VALUES (UUID(),
        'movieTV',
        'Galadrielaptor',
        'Galadrielaptor',
        0,
        2023,
        NULL,
        120);




