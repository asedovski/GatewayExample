CREATE TABLE IF NOT EXISTS ##DATABASE##.versions (
  version_number                VARCHAR(10) NOT NULL,
  date_created 					DATETIME(6) NOT NULL,
  PRIMARY KEY (version_number)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

INSERT IGNORE INTO ##DATABASE##.versions (
    version_number, 
    date_created
)
VALUES ( '1.0', now());
