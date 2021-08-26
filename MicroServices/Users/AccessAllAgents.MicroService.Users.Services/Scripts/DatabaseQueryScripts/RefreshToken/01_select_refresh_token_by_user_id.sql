SELECT 
    refresh_token               AS 'RefreshToken',
    user_id                     AS 'UserId',
    expiration_date             AS 'ExpirationDate',
    date_created                AS 'DateCreated'
FROM ##DATABASE##.refresh_tokens
WHERE user_id = @UserId;