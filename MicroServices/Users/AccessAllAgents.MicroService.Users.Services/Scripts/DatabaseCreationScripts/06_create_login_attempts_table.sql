CREATE TABLE IF NOT EXISTS ##DATABASE##.login_attempts
(
    id INT(11) 					NOT NULL PRIMARY KEY AUTO_INCREMENT,
    user_id                 	int(11)  UNSIGNED NOT NULL,
    attempt          			int(2)   NOT NULL,
	email_address          		VARCHAR(255)  NOT NULL,
    CONSTRAINT `fk_login_attempts_user_id` FOREIGN KEY (`user_id`) REFERENCES ##DATABASE##.users (`id`)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;