using AccessAllAgents.MicroService.Users.Services.Containers;
using System;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserService
    {
        Task Initialise();

        Task<UserContainer> RegisterUser(string emailAddress, string password, string loginType);

        Task<LoginContainer> LoginUser(Guid userId);

        Task<bool> ResendVerificationEmail(string emailAddress);
        Task<string> CompleteRegisterUser(string registrationToken);
        Task<string> BeginResetPasswordWorkflow(string emailAddress);
        Task<bool> ValidateResetToken(string resetToken);
        Task<bool> CompleteResetPasswordWorkflow(string resetToken, string newPassword);
        Task<string> TryChangePassword(Guid userId, string oldPassword, string newPassword);
    }
}