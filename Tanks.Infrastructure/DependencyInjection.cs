using Microsoft.Extensions.DependencyInjection;
using Tanks.Application.Repositories;
using Tanks.Infrastructure.Repositories;

namespace Tanks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITankRepository, TankRepository>();

        return services;
    }

}