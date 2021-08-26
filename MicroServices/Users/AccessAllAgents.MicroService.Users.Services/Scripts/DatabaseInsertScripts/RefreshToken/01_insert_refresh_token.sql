INSERT IGNORE INTO ##DATABASE##.refresh_tokens (
    refresh_token,
    user_id,
    expiration_date,
    date_created
)
VALUES (
    @RefreshToken,
    @UserId,
    @ExpirationDate,
    @DateCreated
);