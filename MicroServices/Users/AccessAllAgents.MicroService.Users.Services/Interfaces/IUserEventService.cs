using AccessAllAgents.Email.Containers;
using AccessAllAgents.MicroService.Users.Services.Services.DataAccess.Rows;

namespace AccessAllAgents.MicroService.Users.Services.Interfaces
{
    public interface IUserEventService
    {
        void PublishUserRegisteredEvents(UserRow userRow, bool sendEmail);
        void SendEmail(EmailDetails toJson);
    }
}
