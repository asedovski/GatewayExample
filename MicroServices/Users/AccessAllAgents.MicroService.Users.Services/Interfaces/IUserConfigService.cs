using AccessAllAgents.MicroService.Users.Services.Services.Config;
using AccessAllAgents.Queue.Interfaces;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserConfigService : IQueueConfigService
    {
        LocalStorageConfig LocalStorageConfig { get; }
        ImageConfig ImageConfig { get; }

        bool ForceResetApplicationDatabase { get; }
    }
}
