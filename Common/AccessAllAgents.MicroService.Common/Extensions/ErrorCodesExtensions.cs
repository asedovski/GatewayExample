using AccessAllAgents.MicroService.Common.Constants;

namespace AccessAllAgents.MicroService.Common.Extensions
{
    public static class ErrorCodesExtensions
    {
        public static string ToFailureReason(this ErrorCodes errorCode)
        {
            switch (errorCode)
            {
                case ErrorCodes.ActionNotAllowed:
                    return "Current action is not valid for this user type";
                case ErrorCodes.AdminNotAuthenticated:
                    return "Admin user is not authenticated";
                case ErrorCodes.AvatarIsNull:
                    return "Avatar is null";
                case ErrorCodes.ChallengeExpired:
                    return "Reset token has expired.";
                case ErrorCodes.DeactivateActionNotAllowedWorkflowComplete:
                    return "Unable to deactivate listing: workflow is complete";
                case ErrorCodes.InternalServerError:
                    return "Internal error";
                case ErrorCodes.InvalidChallenge:
                    return "Invalid reset token";
                case ErrorCodes.InvalidContentId:
                    return "Invalid content id";
                case ErrorCodes.Unknown:
                    return "Unknown error";
                case ErrorCodes.UserAlreadyExists:
                    return "User name has already been registered";
                case ErrorCodes.UsernameAlreadyTaken:
                    return "User name has already been taken by another user. Please choose another name";
                case ErrorCodes.NotAuthenticated:
                    return "Not authenticated";
                case ErrorCodes.InvalidEmailAddress:
                    return "Email address is invalid";
                case ErrorCodes.PasswordTooShort:
                    return "Password is too short";
                case ErrorCodes.InvalidPassword:
                    return "Current password was incorrect";
                case ErrorCodes.TooManyChangePasswordAttempts:
                    return "Account locked because of too many attempts at changing password. Please reset your password.";
                case ErrorCodes.MissingEmailAddress:
                    return "Email address is missing";
                case ErrorCodes.InvalidUsernameOrPassword:
                    return "Invalid email address or password";
                case ErrorCodes.InvalidUserId:
                    return "Invalid user id";
                case ErrorCodes.InvalidTopic:
                    return "Invalid topic";
                case ErrorCodes.InvalidUserName:
                    return "Invalid user name";
                case ErrorCodes.InvalidUserGroupId:
                    return "Invalid user group";
                case ErrorCodes.InvalidFile:
                    return "Invalid file";
                case ErrorCodes.InvalidImageEncoding:
                    return "Invalid image encoding";
                case ErrorCodes.InvalidObjectId:
                    return "Invalid object id";
                case ErrorCodes.PropertyNotFound:
                    return "Invalid property URL";
                case ErrorCodes.PropertyInvalid:
                    return "Invalid property details. Please ensure you've added address, images, price, EPC and commission.";
                case ErrorCodes.PropertyListed:
                    return "Property has already been listed";
                case ErrorCodes.AgencyAlreadyRegistered:
                    return "Agency has already been registered";
                case ErrorCodes.InvalidPostCode:
                    return "Unrecognizable post code";
                case ErrorCodes.ProfileInvalid:
                    return "Invalid profile details";
                case ErrorCodes.TitleFirstNameRequired:
                    return "Title, first name and last name are required";
                case ErrorCodes.InvalidResetPasswordEmail:
                    return "Email address was not recognised.";
                case ErrorCodes.InvalidResetPasswordRequest:
                    return "Invalid request.";
                case ErrorCodes.InvalidResetPasswordRequestToken:
                    return "Token expired or invalid.";
                case ErrorCodes.InvalidResetPasswordRequestPasswordError:
                    return "Password cannot be empty and should be at least 8 characters";
                case ErrorCodes.AccountLocked:
                    return "Your account has been locked.";
                case ErrorCodes.EmailAddressExists:
                    return "Email address has alreayd been registered";
                case ErrorCodes.EmailNotVerified:
                    return "You have not verified your email address. Please check your email and follow the instructions to validate your email address.";
                case ErrorCodes.InvalidRequest:
                    return "Invalid request";
                case ErrorCodes.LanguageIdInvalid:
                    return "Invalid language id";
                case ErrorCodes.MissingPassword:
                    return "Password is missing from the request";
                case ErrorCodes.None:
                    return "Unknown";
                case ErrorCodes.UserAlreadyAuthenticated:
                    return "This email address has already been authenticated";
                case ErrorCodes.UserNotAuthenticated:
                    return "User has not been authenticated";
                case ErrorCodes.UserSuspended:
                    return "Your account has been suspended";
                case ErrorCodes.UserNotFound:
                    return "Unable to find user details";
                default:
                    return "Unknown error";
            }
        }
    }
}
