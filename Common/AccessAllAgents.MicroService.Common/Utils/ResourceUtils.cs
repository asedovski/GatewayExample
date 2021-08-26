using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AccessAllAgents.MicroService.Common.Utils
{
    public static class ResourceUtils
    {
        public static string ReadResource(Assembly assembly, string resourcePath)
        {
            try
            {
                // Determine path
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream == null)
                    {
                        throw new ServiceException(ErrorCodes.InternalServerError, $"Unable to locate manifest resource file at {resourcePath}");
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCodes.InternalServerError, $"Unable to locate manifest resource file {resourcePath}", ex);
            }
        }

        public static string ReadResource(Assembly assembly, string resourceTemplate, string name)
        {
            try
            {
                // Determine path
                string resourcePath = $"{resourceTemplate}.{name.Replace("/", ".")}";
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream == null)
                    {
                        throw new ServiceException(ErrorCodes.InternalServerError, $"Unable to locate manifest resource file at {resourcePath}");
                    }
                     
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCodes.InternalServerError, $"Unable to locate manifest resource file {name}", ex);
            }
        }

        public static IEnumerable<string> GetResourceNames(Assembly assembly, string resourceTemplate, string prefix)
        {
            // Determine path
            string resourcePath = $"{resourceTemplate}.{prefix.Replace("/", ".")}";
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.StartsWith(resourcePath))
                {
                    yield return resourceName;
                }
            }
        }
    }
}
