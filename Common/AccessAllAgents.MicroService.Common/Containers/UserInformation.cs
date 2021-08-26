using System;

namespace AccessAllAgents.MicroService.Common.Containers
{
    public class UserInformation
    {
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
        public string LoginType { get; set; }

        public bool IsAccountLocked { get; set; }
        public bool IsProfileComplete { get; set; }
        public bool IsAccountBeingReset { get; set; }

        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
    }
}