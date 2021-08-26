using AccessAllAgents.Logging.Config.Containers;
using YamlDotNet.Serialization;

namespace AccessAllAgents.Logging.Config
{
    internal class LogConfigElement
    {
        [YamlMember(Alias = "elastic_search_log", ApplyNamingConventions = false)]
        public ElasticSearchLogConfigElement ElasticSearchConfig { get; set; }
    }
}