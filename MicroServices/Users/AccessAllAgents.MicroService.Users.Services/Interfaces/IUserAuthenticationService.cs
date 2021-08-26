using AccessAllAgents.MicroService.Common.Containers;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<UserInformation> AuthenticateAsync(string emailAddress, string password);
        Task<UserInformation> RefreshAsync(string accessToken, string refreshToken);
    }
}
