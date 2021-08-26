SELECT 
    id                      	AS 'ID',
    user_id                     AS 'UserId',
    registration_token          AS 'RegistrationToken'
FROM ##DATABASE##.registrations
WHERE registration_token = @RegistrationToken;