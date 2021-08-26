using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Common
{
    public interface ICommonConfigService
    {
        Task Initialise(string environment);
    }
}
