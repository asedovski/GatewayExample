using AccessAllAgents.MicroService.Common;
using AccessAllAgents.MicroService.Template;
using AccessAllAgents.MicroService.Users.Services.Interfaces;
using AccessAllAgents.MicroService.Users.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using Unity;

namespace AccessAllAgents.MicroService.Users
{
    public class Startup : MicroServiceStartup<IUserConfigService>
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IUserConfigService InitialiseConfigurations(IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("SERVER_ENVIRONMENT");
            var configService = new UserConfigService();
            configService.Initialise(environment).Wait();
            return configService;
        }

        protected override bool AddHealthChecks(IUserConfigService configService, IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddMySql(ConfigService.LocalStorageConfig.ConnectionString)
                .AddRabbitMQ(s => new ConnectionFactory
                {
                    Port = configService.QueueConfig.Port,
                    HostName = configService.QueueConfig.IpAddress,
                    UserName = configService.QueueConfig.Username,
                    Password = configService.QueueConfig.Password
                });

            return true;
        }

        protected override void PreConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        protected override void PostConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        protected override IServiceRegistrar<IUserConfigService> CreateServiceRegistrar(IUnityContainer container)
        {
            return new ServiceRegistrar(container);
        }

        protected override Type ControllerType => GetType();
        protected override Documentations Documentation { get; } = new Documentations("Users Micro Service", "v1", "A microservice to allow user management.");
    }
}