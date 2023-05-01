using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Tanks.Domain.Factories;

namespace Tanks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddFactories()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddMapster();
    }

    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapFactory>(_ => new LabyrinthMapFactory());
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        return services
            .AddScoped<IMapper, Mapper>(_ => new Mapper(config));
    }
}