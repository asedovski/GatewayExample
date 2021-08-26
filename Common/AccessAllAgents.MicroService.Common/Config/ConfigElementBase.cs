using System;

namespace AccessAllAgents.MicroService.Common.Config
{
    public abstract class ConfigElementBase
    {
        protected string Resolve(string value)
        {
            if (value.StartsWith("{") && value.EndsWith("}"))
            {
                return Environment.GetEnvironmentVariable(value.Substring(1, value.Length - 2));
            }

            return value;
        }
    }
}
