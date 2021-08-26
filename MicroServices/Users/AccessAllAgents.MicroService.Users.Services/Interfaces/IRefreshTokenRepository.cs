using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task InsertAsync(string refreshToken, int userId, DateTime expiry);

        Task<IEnumerable<RefreshTokenRow>> SelectRefreshTokens(int userId);

        Task Delete(string refreshToken);

        Task Delete(int userId);
    }
}