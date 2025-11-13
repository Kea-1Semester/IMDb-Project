DROP TRIGGER IF EXISTS trg_before_update_titles;
DROP TRIGGER IF EXISTS trg_after_update_titles;


-- Validating the new values before the update operation is executed on the Titles table
CREATE TRIGGER trg_before_update_titles
    BEFORE UPDATE
    ON Titles
    FOR EACH ROW
BEGIN
    -- Defining the SQLSTATE for the exception
    DECLARE sql_state CONDITION FOR SQLSTATE '45000';

    -- primary_title must not be null, empty, or shorter than 3 characters, longer than 150
    IF NEW.primary_title IS NULL
        OR TRIM(NEW.primary_title) = ''
        OR CHAR_LENGTH(TRIM(NEW.primary_title)) < 3
        OR CHAR_LENGTH(TRIM(NEW.primary_title)) > 150 THEN
        SIGNAL sql_state SET MESSAGE_TEXT =
                'primary_title must not be null, empty, shorter than 3, or longer than 150 characters';

    END IF;

    -- start_year must be before end_year
    IF NEW.start_year IS NOT NULL AND
       NEW.end_year IS NOT NULL AND
       NEW.start_year > NEW.end_year THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'start_year must be before end_year';
    END IF;

    -- start_year must not be null and must be after 1900
    IF New.start_year IS NULL OR NEW.start_year < 1900 THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'start_year must not be null and must be after 1900';
    END IF;

    -- runtime_minutes must be between 0 and 1440 (24 hours)
    IF NEW.runtime_minutes IS NOT NULL AND (NEW.runtime_minutes < 0 OR NEW.runtime_minutes > 1440) THEN
        SIGNAL sql_state SET MESSAGE_TEXT = 'runtime_minutes must be between 0 and 1440';
    END IF;
END;

-- Logging changes after the update operation is complete
CREATE TRIGGER trg_after_update_titles
    AFTER UPDATE
    ON Titles
    FOR EACH ROW
BEGIN
    DECLARE v_table_name VARCHAR(100) DEFAULT 'Titles';
    DECLARE v_command VARCHAR(10) DEFAULT 'UPDATE';

    IF OLD.primary_title <> NEW.primary_title THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('primary_title', NEW.primary_title),
                JSON_OBJECT('primary_title', OLD.primary_title), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF OLD.start_year <> NEW.start_year THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('start_year', NEW.start_year),
                JSON_OBJECT('start_year', OLD.start_year), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF (OLD.end_year <> NEW.end_year) OR
       (OLD.end_year IS NULL AND NEW.end_year IS NOT NULL) OR
       (OLD.end_year IS NOT NULL AND NEW.end_year IS NULL) THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('end_year', NEW.end_year),
                JSON_OBJECT('end_year', OLD.end_year), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
    IF OLD.runtime_minutes <> NEW.runtime_minutes THEN
        INSERT INTO Loggings(table_name, command, new_value, old_value, executed_by)
        VALUES (v_table_name, v_command, JSON_OBJECT('runtime_minutes', NEW.runtime_minutes),
                JSON_OBJECT('runtime_minutes', OLD.runtime_minutes), SUBSTRING_INDEX(USER(), '@', 1));
    END IF;
END;
