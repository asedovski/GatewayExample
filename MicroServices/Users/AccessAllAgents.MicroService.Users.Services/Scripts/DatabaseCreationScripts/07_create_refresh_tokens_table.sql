CREATE TABLE IF NOT EXISTS ##DATABASE##.refresh_tokens
(
    refresh_token               VARCHAR(100)   NOT NULL PRIMARY KEY,
    user_id                 	int(11)       UNSIGNED NOT NULL,
    expiration_date             DATETIME(6) NOT NULL,
    date_created                DATETIME(6) NOT NULL,
	CONSTRAINT `fk_refresh_tokens_user_id` FOREIGN KEY (`user_id`) REFERENCES ##DATABASE##.users (`id`)
)
ENGINE = INNODB,
CHARACTER SET utf8,
COLLATE utf8_general_ci;