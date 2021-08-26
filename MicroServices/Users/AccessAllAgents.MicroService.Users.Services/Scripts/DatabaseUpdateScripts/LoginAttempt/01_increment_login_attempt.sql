UPDATE ##DATABASE##.login_attempts
SET attempt = attempt + 1
WHERE user_id = @UserId;