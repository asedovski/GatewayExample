namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface ISuspiciousActivityService
    {
        void ReportSuspiciousActivity(in int userId, string action, string userEmailFoundButNotPresentInTheUsersTable);
    }
}
