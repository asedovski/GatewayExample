INSERT IGNORE INTO ##DATABASE##.login_attempts (
    user_id,
    attempt,
    email_address
)
VALUES (
    @UserId,
    @Attempt,
    @EmailAddress
);