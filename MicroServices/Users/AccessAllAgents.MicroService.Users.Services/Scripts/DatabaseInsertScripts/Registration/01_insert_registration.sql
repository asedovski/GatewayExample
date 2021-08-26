INSERT IGNORE INTO ##DATABASE##.registrations (
    user_id,
    registration_token
)
VALUES (
    @UserId,
    @RegistrationToken
);