using AccessAllAgents.MicroService.Common.Utils;
using System.Collections.Concurrent;
using System.Reflection;

namespace AccessAllAgents.MicroService.Common.Repository
{
    public class RepositoryBase<T> 
        where T: RepositoryBase<T>
    {
        private readonly ConcurrentDictionary<string, string> _resourceTemplateDictionary;
        private readonly string _resourceTemplate;
        private readonly Assembly _assembly;

        public RepositoryBase()
        {
            _assembly = Assembly.GetAssembly(typeof(T));
            _resourceTemplate = _assembly.ManifestModule.Name.Replace(".dll", "");
            _resourceTemplateDictionary = new ConcurrentDictionary<string, string>();
        }

        protected string GetResource(string resourceName, string database)
        {
            if (_resourceTemplateDictionary.TryGetValue(resourceName, out string sqlString))
            {
                return sqlString;
            }

            sqlString = ResourceUtils.ReadResource(_assembly, _resourceTemplate, resourceName);
            sqlString = sqlString.Replace("##DATABASE##", database);
            _resourceTemplateDictionary.TryAdd(resourceName, sqlString);
            return sqlString;
        }
    }
}
