using System;
using YamlDotNet.Serialization;

namespace AccessAllAgents.Logging.Config.Containers
{
    internal class ElasticSearchLogConfigElement
    {
        public ElasticSearchLogConfig ToElasticSearchConfig()
        {
            return new ElasticSearchLogConfig(Resolve(IpAddress), Resolve(Port));
        }

        [YamlMember(Alias = "ip_address", ApplyNamingConventions = false)]
        public string IpAddress { get; set; }

        [YamlMember(Alias = "port", ApplyNamingConventions = false)]
        public string Port { get; set; }

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