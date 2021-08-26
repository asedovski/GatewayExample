namespace AccessAllAgents.MicroService.Common.Constants
{
    public enum ErrorCodes
    {
        AdminNotAuthenticated = -3,
        NotAuthenticated = -2,
        Unknown = -1,
        InternalServerError = 1,
        ActionNotAllowed = 2,

        None = 0,

        MissingPassword = 10,
        MissingEmailAddress = 11,
        InvalidEmailAddress = 12,
        PasswordTooShort = 13,
        EmailNotVerified = 14,
        InvalidPassword = 15,
        TooManyChangePasswordAttempts = 16,

        UserAlreadyExists = 100,
        InvalidUserGroupId = 101,
        UsernameAlreadyTaken = 102,

        InvalidUserId = 200,
        InvalidToken = 201,
        InvalidImageEncoding = 202,
        InvalidUsernameOrPassword = 203,
        InvalidObjectId = 204,
        InvalidUserName = 205,
        TitleFirstNameRequired = 206,
        UserNotFound = 207,
        
        InvalidPhoneNumber = 300,
        InvalidFullName = 301,
        PropertyNotFound = 302,
        PropertyInvalid = 303,
        PropertyListed = 304,
        InvalidPostCode = 305,
        MessageExists = 306,
        MessageNotExists = 307,
        AgencyAlreadyRegistered = 308,
        AgencyInvalid = 309,
        AgencyAccountLocked = 310,
        InvalidAgencyId = 311,
        DeactivationFailure = 312,
        ProfileInvalid = 313,
        DeactivateActionNotAllowedWorkflowComplete = 314,

        InvalidFile = 700,
        InvalidLanguage = 702,
        InvalidTopic = 703,
        InvalidContentId = 704,
        InvalidVideoLink = 705,

        AvatarIsNull = 1201,

        InvalidResetPasswordRequest = 1300,
        InvalidChallenge = 1301,
        ChallengeExpired = 1302,
        InvalidRequest = 1304,
        ContentIsNull = 1305,
        InvalidResetPasswordEmail = 1306,
        InvalidResetPasswordRequestPasswordError = 1307,
        InvalidResetPasswordRequestToken = 1308,

        EmailAddressExists = 1008,
        UserNotAuthenticated = 1010,
        UserAlreadyAuthenticated = 10012,
        LanguageIdInvalid = 10013,
        UserSuspended = 10014,
        AccountLocked = 10015,
    }
}
