using AccessAllAgents.MicroService.Common.Constants;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserValidator
    {
        bool ValidatePassword(string password, out ErrorCodes errorCode);
        bool ValidateUserEmail(string emailAddress);
        bool IsValidEmailAddress(string emailAddress);
    }
}
