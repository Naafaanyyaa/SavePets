using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePets.Business.Models.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { set; get; } = string.Empty;
        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string Password { set; get; } = string.Empty;
    }
}
