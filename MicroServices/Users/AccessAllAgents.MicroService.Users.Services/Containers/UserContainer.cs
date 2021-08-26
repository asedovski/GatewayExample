using System;

namespace AccessAllAgents.MicroService.Users.Services.Containers
{
    public class UserContainer
    {
        public UserContainer(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public override bool Equals(object obj)
        {
            var other = obj as UserContainer;
            if (obj == null)
            {
                return false;
            }
            return Equals(other);
        }

        private bool Equals(UserContainer other)
        {
            return UserId == other.UserId && string.Equals(Email, other.Email);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Guid UserId { get; }
        public string Email { get; }
    }
}
