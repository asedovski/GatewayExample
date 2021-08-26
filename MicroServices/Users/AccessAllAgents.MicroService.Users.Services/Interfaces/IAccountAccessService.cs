using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IAccountAccessService
    {
        Task<bool> AssessAccountLockedStatus(UserRow user);
        Task ResetLoginCounter(int userId);
    }
}
