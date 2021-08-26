using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Common.Config
{
    public class EmptyConfigService : ICommonConfigService
    {
        public Task Initialise(string environment)
        {
            return Task.CompletedTask;
        }
    }
}