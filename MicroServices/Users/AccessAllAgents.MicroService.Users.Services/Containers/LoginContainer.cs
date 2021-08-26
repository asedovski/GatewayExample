using System;

namespace AccessAllAgents.MicroService.Users.Services.Containers
{
    public class LoginContainer
    {
        public LoginContainer(int id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid UserId { get; }
        public int Id { get; }

        public bool IsAccountLocked { get; set; }
        public bool IsAccountBeingReset { get; set; }
        public bool IsAccountSuspended { get; set; }
        public string LoginType { get; set; }

        public bool IsFirstTime { get; set; }
        public bool IsProfileComplete { get; set; }
        public bool IsPhoneNumberSet { get; set; }
        public bool IsFullNameSet { get; set; }
        public UserProfileContainer Profile { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is LoginContainer other))
            {
                return false;
            }

            return UserId == other.UserId;
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
    }
}