using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Tanks.Domain.Factories;
using Tanks.Domain.Simulation;
using Tanks.Domain.Simulation.PathFinding;
using Tanks.Domain.Simulation.TanksAIs;

namespace Tanks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddDependencies()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddMapster();
    }

    private static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapFactory, LabyrinthMapFactory>()
            .AddSingleton<IPathFinder, AStarPathFinder>()
            .AddSingleton<ITankAIChooser, InitialTankAIChooser>()
            .AddSingleton<Simulator>();
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        return services
            .AddScoped<IMapper, Mapper>(_ => new Mapper(config));
    }
}