UPDATE ##DATABASE##.users 
SET 
    email_address = @EmailAddress,
    password_hash = @PasswordHash,
    user_id = @UserId,
    is_first_time = @IsFirstTime,
    is_inactive = 	@IsInactive,
    is_suspended = 	@IsSuspended,
    is_reset_required = @IsResetRequired,
    is_account_being_reset = @IsAccountBeingReset,
    is_profile_complete = @IsProfileComplete,
    title = @Title,
    first_name = @FirstName,
    last_name = @LastName,
    agency_name = @AgencyName,
    is_full_name_set = @IsFullNameSet,
    phone_number = @PhoneNumber,
    is_phone_number_set = @IsPhoneNumberSet,
    post_code = @PostCode,
    address = @Address,
    city = @City,
    country = @Country,
    logo_url = @LogoUrl,
    login_type = @LoginType,
    date_created = @DateCreated,
    refresh_token = @RefreshToken
WHERE id = @Id


   
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
   