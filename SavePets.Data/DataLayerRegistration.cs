using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavePets.Data.Entities.Identity;
using SavePets.Data.Interfaces;
using SavePets.Data.Repositories;

namespace SavePets.Data;

public static class DataLayerRegistration
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.UseNetTopologySuite()));

        services.AddIdentity<User, Role>()
            .AddUserStore<UserStore<User, Role, ApplicationDbContext, string, IdentityUserClaim<string>, UserRole,
                IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
            .AddRoleStore<RoleStore<Role, ApplicationDbContext, string, UserRole, IdentityRoleClaim<string>>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>();


        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<IContactsRepository, ContactsRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        return services;
    }
}