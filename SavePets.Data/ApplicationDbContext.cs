using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SavePets.Data.Entities;
using SavePets.Data.Entities.Identity;

namespace SavePets.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Contacts> Contacts { get; set; }
    public DbSet<Photo> Photo { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Location> Locations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Subscription>()
            .HasOne(t => t.User)
            .WithOne(t => t.Subscription)
            .HasForeignKey<User>(t => t.SubscriptionID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Role>()
            .HasMany(t => t.UserRoles)
            .WithOne(t => t.Role)
            .HasForeignKey(t => t.RoleId)
            .IsRequired();
        modelBuilder
            .Entity<User>()
            .HasMany(t => t.UserRoles)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .IsRequired();

        modelBuilder
            .Entity<Animal>()
            .HasMany(t => t.Photos)
            .WithOne(t => t.Animal)
            .HasForeignKey(t => t.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(t => t.Animals)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Contacts>()
            .HasOne(t => t.Animal)
            .WithOne(t => t.Contacts)
            .HasForeignKey<Animal>(t => t.ContactsId)
            .HasPrincipalKey<Contacts>(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder
            .Entity<Location>()
            .HasOne(t => t.Animal)
            .WithOne(t => t.Location)
            .HasForeignKey<Animal>(t => t.LocationId)
            .HasPrincipalKey<Location>(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}