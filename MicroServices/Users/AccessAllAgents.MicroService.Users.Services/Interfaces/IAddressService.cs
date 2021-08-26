using AccessAllAgents.MicroService.Users.Services.Containers;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IAddressService
    {
        Address GetAddressDetails(string postCode);
    }
}
