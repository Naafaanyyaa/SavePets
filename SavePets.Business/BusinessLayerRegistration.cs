using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavePets.Data;

namespace SavePets.Business;

public static class BusinessLayerRegistration
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataLayer(configuration);

        return services;
    }
}