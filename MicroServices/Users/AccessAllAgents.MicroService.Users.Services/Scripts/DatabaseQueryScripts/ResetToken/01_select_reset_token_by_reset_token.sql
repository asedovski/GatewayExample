SELECT 
    reset_token                 AS 'ResetToken',
    user_id                     AS 'UserId',
    registration_token          AS 'RegistrationToken',
	email_address               AS 'EmailAddress',
FROM ##DATABASE##.reset_tokens
WHERE reset_token = @ResetToken;