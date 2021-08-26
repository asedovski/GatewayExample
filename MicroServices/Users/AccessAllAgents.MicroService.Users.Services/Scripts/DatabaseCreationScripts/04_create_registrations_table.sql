CREATE TABLE IF NOT EXISTS ##DATABASE##.registrations
(
    id                      	int(11)       UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    user_id                 	int(11)       UNSIGNED NOT NULL,
    registration_token          VARCHAR(100)  NOT NULL,
    CONSTRAINT `fk_registrations_user_id` FOREIGN KEY (`user_id`) REFERENCES ##DATABASE##.users (`id`)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;