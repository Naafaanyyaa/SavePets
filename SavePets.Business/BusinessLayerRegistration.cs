using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavePets.Business.Infrastructure;
using SavePets.Business.Interfaces;
using SavePets.Business.Services;
using SavePets.Data;

namespace SavePets.Business;

public static class BusinessLayerRegistration
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataLayer(configuration);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IRoleInitializer, RoleInitializer>();
        
        services.AddScoped<IRegistrationService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<JwtHandler>();

        return services;
    }
}