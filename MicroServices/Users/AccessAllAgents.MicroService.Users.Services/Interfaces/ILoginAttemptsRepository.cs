using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface ILoginAttemptsRepository
    {
        Task InsertAsync(int userId, int attempt, string emailAddress);

        Task<LoginAttemptsRow> SelectLoginAttempts(int userId);

        Task Delete(int userId);

        Task UpdateAsync(LoginAttemptsRow loginAttempts, long attempt);
    }
}