using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SavePets.Data.Entities;

namespace SavePets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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
                .HasOne(t => t.User)
                .WithOne(t => t.Role)
                .HasForeignKey<User>(t => t.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

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
}
