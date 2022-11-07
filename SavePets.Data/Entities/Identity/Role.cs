using Microsoft.AspNetCore.Identity;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Entities.Identity;

public class Role : IdentityRole
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }

    public List<UserRole> UserRoles { get; set; }
}