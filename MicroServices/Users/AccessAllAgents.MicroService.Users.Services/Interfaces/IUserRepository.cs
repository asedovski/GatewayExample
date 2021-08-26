using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmpty();

        Task<UserRow> InsertUserAsync(UserRow newUserRow);
        Task<UserRow> InsertUserAndRegistrationAsync(UserRow newUserRow, string registrationToken);

        Task<List<UserRow>> SelectAllUsers();
        Task<UserRow> SelectUserByIdAsync(int id);
        Task<UserRow> SelectUserByEmailAddressAsync(string emailAddress);
        Task<UserRow> SelectUserByUserIdAsync(Guid userId);

        Task UpdateAsync(UserRow usersRow);
    }
}