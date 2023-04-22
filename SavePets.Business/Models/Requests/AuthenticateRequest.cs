using System.ComponentModel.DataAnnotations;

namespace SavePets.Business.Models.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { set; get; } = string.Empty;
        [Required]
        public string Password { set; get; } = string.Empty;
    }
}
