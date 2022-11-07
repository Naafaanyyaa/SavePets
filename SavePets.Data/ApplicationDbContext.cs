﻿using Microsoft.AspNetCore.Identity;
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
    public DbSet<Tag> Tags { get; set; }

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
            .Entity<AnimalTag>()
            .HasKey(t => t.Id);
        modelBuilder
            .Entity<Tag>()
            .HasMany(t => t.AnimalTag)
            .WithOne(t => t.Tags)
            .HasForeignKey(t => t.TagId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Animal>()
            .HasMany(t => t.AnimalTags)
            .WithOne(t => t.Animals)
            .HasForeignKey(t => t.AnimalId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<AnimalPhoto>()
            .HasKey(t => t.Id);
        modelBuilder
            .Entity<Animal>()
            .HasMany(t => t.AnimalPhotos)
            .WithOne(t => t.Animals)
            .HasForeignKey(t => t.AnimalId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Photo>()
            .HasMany(t => t.AnimalPhotos)
            .WithOne(t => t.Photos)
            .HasForeignKey(t => t.PhotoId)
            .HasPrincipalKey(t => t.Id)
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
            .Entity<UserAnimal>()
            .HasKey(t => t.Id);
        modelBuilder
            .Entity<User>()
            .HasMany(t => t.UserAnimals)
            .WithOne(t => t.Users)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(t => t.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .Entity<Animal>()
            .HasMany(t => t.UserAnimals)
            .WithOne(t => t.Animals)
            .HasForeignKey(t => t.AnimalId)
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
            .OnDelete(DeleteBehavior.Cascade);
    }
}