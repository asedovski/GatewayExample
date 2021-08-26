using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IResetTokenRepository
    {
        Task Insert(UserRow userRow, string resetToken);

        Task<ResetTokenRow> SelectResetTokenRowByKey(string resetToken);
        Task<ResetTokenRow> SelectResetTokenRow(int userRowId);

        Task Delete(ResetTokenRow resetTokenRow);
    }
}
