using System;
using System.Threading.Tasks;
using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface ILogoRepository
    {
        Task<LogoRow> InsertLogoAsync(Guid contentId, Guid userId, string url, byte[] fileBytes);
        Task<LogoRow> SelectLogoByContentId(Guid contentId);
    }
}
