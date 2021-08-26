SELECT 
    id                      	AS 'ID',
    user_id                     AS 'UserId',
    attempt                     AS 'Attempt',
    email_address               AS 'EmailAddress'
FROM ##DATABASE##.login_attempts
WHERE user_id = @UserId;