using AccessAllAgents.Logging.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Unity.Microsoft.DependencyInjection;

namespace AccessAllAgents.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("SERVER_ENVIRONMENT");
            var logConfigService = new LogConfigService();
            logConfigService.Initialise(environment).Wait();
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
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = Environment.GetEnvironmentVariable("SERVER_ENVIRONMENT");
                    string jsonFileName;
                    string appSettingsFileName;
                    if (environment != null)
                    {
                        switch (environment.ToLowerInvariant())
                        {
                            case "docker":
                                jsonFileName = "ocelot.docker.json";
                                appSettingsFileName = "appsettings.docker.json";
                                break;
                            case "production":
                                jsonFileName = "ocelot.production.json";
                                appSettingsFileName = "appsettings.production.json";
                                break;
                            case "nuc":
                                jsonFileName = "ocelot.nuc.json";
                                appSettingsFileName = "appsettings.nuc.json";
                                break;
                            default:
                                throw new Exception($"Invalid environment: {environment}");
                        }
                    }
                    else
                    {
                        jsonFileName = "ocelot.json";
                        appSettingsFileName = "appsettings.json";
                        Log.Warning($"Environment was null. Defaulting to {jsonFileName}");
                    }

                    Log.Information($"Starting Ocelot using {jsonFileName}");

                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"{appSettingsFileName}", true, true)
                        .AddJsonFile($"./Configurations/{jsonFileName}")
                        .AddEnvironmentVariables();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}