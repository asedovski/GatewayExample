using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Linq;
using AccessAllAgents.Logging.Config;

namespace AccessAllAgents.Logging
{
    public class Log : ILog
    {
        public static void Configure(ElasticSearchLogConfig logConfig)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Filter.ByExcluding(c => c.Properties.Any(p =>
                {
                    var value = p.Value.ToString().ToLowerInvariant();
                    return value.Contains("swagger") || value.Contains("health");
                }))
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(logConfig.Address))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = "aaa-log-{0:yyyy.MM}"
                })
                .WriteTo.Console();

            Serilog.Log.Logger = loggerConfiguration.CreateLogger();
            Serilog.Log.Information($"Log was successfully configured using {logConfig.Address}.");
        }

        public void Debug(string message)
        {
            Serilog.Log.Debug(message);
        }

        public void Error(string message)
        {
            Serilog.Log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            Serilog.Log.Error(message, exception);
        }

        public void Fatal(string message)
        {
            Serilog.Log.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Serilog.Log.Fatal(message, exception);
        }

        public void Info(string message, Exception exception)
        {
            Serilog.Log.Information(message, exception);
        }

        public void Info(string message)
        {
            Serilog.Log.Information(message);
        }

        public void Warn(string message)
        {
            Serilog.Log.Warning(message);
        }

        public void Warn(string message, Exception exception)
        {
            Serilog.Log.Warning(message, exception);
        }

        public static void CloseAndFlush()
        {
            Serilog.Log.CloseAndFlush();
        }
    }
}