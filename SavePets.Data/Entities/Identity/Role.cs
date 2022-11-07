using Microsoft.AspNetCore.Identity;

namespace SavePets.Data.Entities.Identity;

public class Role : IdentityRole
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }

    public List<UserRole> UserRoles { get; set; }
}