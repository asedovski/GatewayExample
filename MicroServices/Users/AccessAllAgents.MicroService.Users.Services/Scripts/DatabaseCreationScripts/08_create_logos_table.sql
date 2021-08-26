CREATE TABLE IF NOT EXISTS ##DATABASE##.logos
(
  user_id                       CHAR(36) NOT NULL,
  content_id                    CHAR(36) NOT NULL,
  file_name                     VARCHAR(100) NOT NULL,
  image                         MEDIUMBLOB NOT NULL,
  CONSTRAINT `fk_logos_user_id` FOREIGN KEY (`user_id`) REFERENCES ##DATABASE##.users (`user_id`),
  PRIMARY KEY (user_id)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;