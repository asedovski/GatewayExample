using System;
using AccessAllAgents.Logging.Config;
using AccessAllAgents.MicroService.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Unity.Microsoft.DependencyInjection;

namespace AccessAllAgents.MicroService.Template
{
    public class MicroServiceProgram<TStartup, TConfig>
        where TStartup : MicroServiceStartup<TConfig>
        where TConfig : ICommonConfigService
    {
        protected static void Run(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("SERVER_ENVIRONMENT");
            var logConfigService = new LogConfigService();
            try
            {
                logConfigService.Initialise(environment).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to initialise log config service. Are you missing the ElasticSearch settings in your configuration file?", ex);
            }

            Logging.Log.Configure(logConfigService.ElasticSearchLogConfig);

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Logging.Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseUnityServiceProvider()
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<TStartup>(); });
    }
}