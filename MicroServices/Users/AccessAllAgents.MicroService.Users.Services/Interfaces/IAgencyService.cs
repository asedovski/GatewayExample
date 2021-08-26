using AccessAllAgents.MicroService.Users.Services.Containers;
using System;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IAgencyService
    {
        Task TryRegisterAgencyAsync(string emailAddress,
            string password,
            string name,
            string areaCode,
            string phoneNumber,
            string postCode,
            string address);

        Task RegisterAgencyInterest(string name, 
            string agencyName, 
            string emailAddress, 
            string areaCode, 
            string phoneNumber);

        Task<FileSaveContainer> UpdateLogoAsync(Guid userId,
            string context, 
            Guid guid, 
            byte[] fileData, 
            string fileType, 
            string extension);

        Task<byte[]> LoadImageFromStorage(Guid contentId);
    }
}
