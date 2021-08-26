using System.ComponentModel.DataAnnotations;

namespace AccessAllAgents.MicroService.Users.ViewModels
{
    public class AuthenticateViewModel
    {
        [Required]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
