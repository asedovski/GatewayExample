DELETE FROM ##DATABASE##.refresh_tokens
WHERE refresh_token = @RefreshToken or expiration_date < UNIX_TIMESTAMP(NOW());