using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Common
{
    public interface IServiceRegistrar<in TConfig>
        where TConfig : ICommonConfigService
    {
        void RegisterServices(TConfig configService);

        Task InitialiseServices(string environment);

        T Resolve<T>();
    }
}