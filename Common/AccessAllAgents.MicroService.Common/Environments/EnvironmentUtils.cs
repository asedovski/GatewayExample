using System;

namespace AccessAllAgents.MicroService.Common.Environments
{
    public static class EnvironmentUtils
    {
        private const string ServerEnvironment = "SERVER_ENVIRONMENT";

        public static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable(ServerEnvironment);
        }

        public static bool IsNuc()
        {
            return Environment.GetEnvironmentVariable(ServerEnvironment)?.ToLowerInvariant() == "nuc";
        }

        public static bool IsProduction()
        {
            return Environment.GetEnvironmentVariable(ServerEnvironment)?.ToLowerInvariant() == "production";
        }
    }
}
