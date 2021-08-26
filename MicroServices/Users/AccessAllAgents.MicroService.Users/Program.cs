using AccessAllAgents.MicroService.Template;
using AccessAllAgents.MicroService.Users.Services.Interfaces;

namespace AccessAllAgents.MicroService.Users
{
    public class Program : MicroServiceProgram<Startup, IUserConfigService>
    {
        public static void Main(string[] args)
        {
            Run(args);
        }
    }
}