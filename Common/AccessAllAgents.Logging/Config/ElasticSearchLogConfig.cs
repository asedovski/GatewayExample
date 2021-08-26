namespace AccessAllAgents.Logging.Config
{
    public class ElasticSearchLogConfig
    {
        public ElasticSearchLogConfig(string ipAddress, string port)
        {
            IpAddress = ipAddress;
            Port = port;
        }

        public string IpAddress { get; }

        public string Port { get; }

        public string Address => $"{IpAddress}:{Port}";
    }
}