CREATE TABLE IF NOT EXISTS ##DATABASE##.reset_tokens
(
    reset_token               	VARCHAR(50)   NOT NULL PRIMARY KEY,
    user_id                 	int(11)       UNSIGNED NOT NULL,
    registration_token          VARCHAR(100)  NOT NULL,
	email_address          		VARCHAR(255)  NOT NULL,
    CONSTRAINT `fk_reset_tokens_user_id` FOREIGN KEY (`user_id`) REFERENCES ##DATABASE##.users (`id`)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;