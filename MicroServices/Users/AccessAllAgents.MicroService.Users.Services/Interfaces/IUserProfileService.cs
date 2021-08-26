using AccessAllAgents.MicroService.Users.Services.Containers;
using System;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileContainer> TryUpdateUserProfile(Guid userId, UserProfileUpdateContainer request);

        Task<UserProfileContainer> TryGetUserProfile(Guid userId);

        Task TryUpdatePhoneNumber(Guid userId, int areaCode, string phoneNumber);

        Task<bool> TryUpdateFullName(Guid userId,
            string title,
            string firstName,
            string maidenName,
            string lastName);
    }
}