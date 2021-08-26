using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IRegistrationRepository
    {
        Task InsertRegistrationRowAsync(int userId, string registrationToken);

        Task<RegistrationRow> SelectRegistrationRow(string registrationToken);

        Task Delete(RegistrationRow registrationRow);
    }
}