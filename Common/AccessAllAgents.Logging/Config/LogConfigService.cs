using AccessAllAgents.Logging.Config.Containers;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AccessAllAgents.Logging.Config
{
    public class LogConfigService
    {
        public async Task Initialise(string environment)
        {
            string yamlFile = string.IsNullOrEmpty(environment) ? "config.yaml" : $"config.{environment}.yaml";
            using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Config", "Log", yamlFile)))
            {
                var input = new StringReader(await streamReader.ReadToEndAsync());
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .WithTagMapping("elastic_search_log", typeof(ElasticSearchLogConfigElement))
                    .Build();

                var configElement = deserializer.Deserialize<LogConfigElement>(input);
                ElasticSearchLogConfig = configElement.ElasticSearchConfig.ToElasticSearchConfig();
            }
        }

        public ElasticSearchLogConfig ElasticSearchLogConfig { get; private set; }
    }
}
