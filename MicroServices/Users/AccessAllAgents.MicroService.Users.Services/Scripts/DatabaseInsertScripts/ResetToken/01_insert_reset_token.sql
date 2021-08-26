INSERT IGNORE INTO ##DATABASE##.reset_tokens (
    reset_token,
    user_id,
    registration_token,
	email_address,
    
)
VALUES (
    @ResetToken,
    @UserId,
    @RegistrationToken,
    @EmailAddress
);