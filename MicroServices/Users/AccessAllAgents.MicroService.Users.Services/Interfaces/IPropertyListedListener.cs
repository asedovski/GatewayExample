using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IPropertyListedListener
    {
        Task ListenToNotificationsAsync();
    }
}
