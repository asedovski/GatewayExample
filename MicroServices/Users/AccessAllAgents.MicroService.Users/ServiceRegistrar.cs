using AccessAllAgents.Logging;
using AccessAllAgents.MicroService.Common;
using AccessAllAgents.MicroService.Users.Services.Interfaces;
using AccessAllAgents.MicroService.Users.Services.Services;
using AccessAllAgents.MicroService.Users.Services.Services.DataAccess;
using AccessAllAgents.MicroService.Users.Services.Services.Listeners;
using AccessAllAgents.Queue;
using AccessAllAgents.Queue.Interfaces;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace AccessAllAgents.MicroService.Users
{
    public class ServiceRegistrar : IServiceRegistrar<IUserConfigService>
    {
        private static readonly ILog Log = LogManager.GetLogger<ServiceRegistrar>();
        private readonly IUnityContainer _container;
        private QueueServiceRegistrar _queueServiceRegistrar;

        public ServiceRegistrar(IUnityContainer container)
        {
            _container = container;
        }

        public void RegisterServices(IUserConfigService configService)
        {
            Log.Info("Registering services");
            _container.RegisterInstance(configService);
            _container.RegisterInstance<ICommonConfigService>(configService);
            _container.RegisterInstance<IQueueConfigService>(configService);

            _queueServiceRegistrar = _container.Resolve<QueueServiceRegistrar>();
            _queueServiceRegistrar.RegisterServices(configService);

            _container.RegisterType<IDatabaseInitialisationService, DatabaseInitialisationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDatabaseUpgradeService, DatabaseUpgradeService>(new ContainerControlledLifetimeManager());
            
            _container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRegistrationRepository, RegistrationRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILoginAttemptsRepository, LoginAttemptsRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IResetTokenRepository, ResetTokenRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRefreshTokenRepository, RefreshTokenRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILogoRepository, LogoRepository>(new ContainerControlledLifetimeManager());
            
            _container.RegisterType<IImageService, ImageService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserAuthenticationService, UserAuthenticationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserProfileService, UserProfileService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAgencyService, AgencyService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAddressService, AddressService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAccountAccessService, AccountAccessService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserEventService, UserEventService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IImageValidationService, ImageValidationService>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IUserValidator, UserValidator>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISuspiciousActivityService, SuspiciousActivityService>(new ContainerControlledLifetimeManager());

            // listeners
            _container.RegisterType<IPropertyListedListener, PropertyListedListener>(new ContainerControlledLifetimeManager());
            _container.RegisterType<INotificationUpdatedEventListener, NotificationUpdatedEventListener>(new ContainerControlledLifetimeManager());
        }

        public async Task InitialiseServices(string environment)
        {
            await _queueServiceRegistrar.InitialiseServices(environment);

            await _container.Resolve<IDatabaseInitialisationService>().Initialise();
            await _container.Resolve<IDatabaseUpgradeService>().RunUpdatesIfNecessary();

            await _container.Resolve<IUserService>().Initialise();

            await _container.Resolve<IPropertyListedListener>().ListenToNotificationsAsync();
            await _container.Resolve<INotificationUpdatedEventListener>().ListenToNotificationsAsync();
        }

        public T Resolve<T>()
        {
            return default;
        }
    }
}