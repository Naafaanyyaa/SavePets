using Microsoft.AspNetCore.Identity;

namespace SavePets.Data.Entities.Identity
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
