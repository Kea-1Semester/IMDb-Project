-- Modifying the Loggings table to avoid errors with the UUID_TO_BIN function
DROP TABLE IF EXISTS Loggings;
CREATE TABLE Loggings
(
    -- I have changed a type of colum from char(36) to binary(16) to avoid errors with the UUID_TO_BIN function
    logging_id  binary(16)  DEFAULT (UUID_TO_BIN(UUID(), 1)) NOT NULL
        PRIMARY KEY,
    table_name  char(36) CHARSET ascii                       NOT NULL,
    command     enum ('INSERT', 'UPDATE', 'DELETE')          NOT NULL,
    new_value   json                                         NULL,
    old_value   json                                         NULL,
    executed_by varchar(100)                                 NULL,
    executed_at datetime(6) DEFAULT CURRENT_TIMESTAMP(6)     NOT NULL
);

CREATE INDEX executed_at_index
    ON Loggings (executed_at);

CREATE INDEX table_name_index
    ON Loggings (table_name);

-- Table to store title types and using them in the trigger to check the validity of the inserted title type
CREATE TABLE title_types
(
    id   CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    type VARCHAR(50) UNIQUE NOT NULL
);

INSERT INTO title_types (type)
VALUES ('movie'),
       ('short'),
       ('tvEpisode'),
       ('tvMiniSeries'),
       ('tvMovie'),
       ('tvSeries'),
       ('tvShort'),
       ('tvSpecial'),
       ('video'),
       ('videoGame');
