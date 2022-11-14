using Microsoft.AspNetCore.Identity;

namespace SavePets.Data.Entities.Identity;

public class User : IdentityUser
{
    public bool IsBanned { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? SubscriptionID { get; set; }
    public Subscription? Subscription { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public List<Animal> Animals { get; set; }
}